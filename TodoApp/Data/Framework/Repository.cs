using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Framework
{
    public class Repository
    {
        internal static void InitializeSchema(SQLiteConnection connection)
        {
            ExecuteNonQuery(connection, $"CREATE TABLE {TableName<Login>()}     (ID INTEGER PRIMARY KEY autoincrement, Username Text, Password Text);");
            ExecuteNonQuery(connection, $"CREATE TABLE {TableName<TodoEntry>()} (ID INTEGER PRIMARY KEY autoincrement, OwnerId INTEGER, IsCompleted boolean, Description Text, LastUpdated date);");
            ExecuteNonQuery(connection, $"CREATE TABLE {TableName<AuthToken>()} (ID INTEGER PRIMARY KEY autoincrement, OwnerId INTEGER, Token Text);");
        }

        public static List<T> Select<T>(params SqlParameter[] where) where T : EntitySqlLite
        {
            var list = new List<T>();
            var mapper = Activator.CreateInstance<T>().FromReader;

            var sql = $"SELECT * FROM {TableName<T>()}"; 
            if (where.Length > 0)
                sql += " WHERE " + where[0].Where;

            using (var command = new SQLiteCommand(sql, InMemoryDatabase.Instance))
            {
                if (where.Length> 0)
                    foreach (var p in where[0].Parameters)
                        command.Parameters.AddWithValue(p.Key, p.Value);


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((T)mapper(reader));
                }
            }

            return list;
        }

        public static void Delete<T>(params SqlParameter[] where) where T : EntitySqlLite
        {            
            var sql = $"DELETE FROM {TableName<T>()}";
            if (where.Length > 0)
                sql += " WHERE " + where[0].Where;

            using (var command = new SQLiteCommand(sql, InMemoryDatabase.Instance))
            {
                if (where.Length > 0)
                    foreach (var p in where[0].Parameters)
                        command.Parameters.AddWithValue(p.Key, p.Value);

                command.ExecuteNonQuery();
            }
        }

        public static void SaveOrUpdate<T>(T entity) where T : EntitySqlLite
        {
            if (entity.Id == 0)
                ExecuteInsert(InMemoryDatabase.Instance, entity);
            else
                ExecuteUpdate(InMemoryDatabase.Instance, entity);            
        }
                        
        private static string TableName<T>() where T: IEntity
        {
            return typeof(T).Name;
        }

        private static void ExecuteInsert<T>(SQLiteConnection connection, T entity) where T : EntitySqlLite
        {
            var fields = string.Join(", ", entity.FieldNames);
            var parameters = string.Join(", ", from fn in entity.FieldNames select "@" + fn);
                       
            var sql = $"INSERT INTO {TableName<T>()} ({fields}) VALUES ({parameters}); SELECT last_insert_rowid();";
    
            using (var command = new SQLiteCommand(sql, connection))
            {
                entity.SetParameters(command);                
                entity.Id = (long)command.ExecuteScalar();
            }
        }


        private static void ExecuteUpdate<T>(SQLiteConnection connection, T entity) where T : EntitySqlLite
        {
            var sql = $"UPDATE {TableName<T>()} SET {string.Join(", ", from fieldName in entity.FieldNames select $"{fieldName} = @{fieldName}")} WHERE ID = {entity.Id}";
            using (var command = new SQLiteCommand(sql, connection))
            {
                entity.SetParameters(command);
                command.ExecuteNonQuery();
            }
        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string sql)
        {
            using (var command = new SQLiteCommand(sql, connection))
                command.ExecuteNonQuery();
        }        
    }
}
