using Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Data.Entity
{
    public class TodoEntry : EntitySqlLite
    {
        public long OwnerId { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
        
        internal override List<string> FieldNames => new List<string>() { "OwnerId", "IsCompleted", "Description", "LastUpdated" };
        internal override Action<SQLiteCommand> SetParameters
        {
            get
            {
                return cmd =>
                    {
                        cmd.Parameters.Add("@OwnerId", System.Data.DbType.Int64);
                        cmd.Parameters.Add("@IsCompleted", System.Data.DbType.Boolean);
                        cmd.Parameters.Add("@Description", System.Data.DbType.String);
                        cmd.Parameters.Add("@LastUpdated", System.Data.DbType.DateTime);
                        cmd.Parameters["@OwnerId"].Value = OwnerId;
                        cmd.Parameters["@IsCompleted"].Value = IsCompleted;
                        cmd.Parameters["@Description"].Value = Description;
                        cmd.Parameters["@LastUpdated"].Value = LastUpdated;
                    };
            }
        }

        internal override Func<SQLiteDataReader, EntitySqlLite> FromReader
        {
            get
            {
                return reader =>
                {
                    var todo = new TodoEntry();
                    todo.Id = (long)reader["Id"];
                    todo.OwnerId = (long)reader["OwnerId"];
                    todo.IsCompleted = (bool)reader["IsCompleted"];
                    todo.Description = (string)reader["Description"];
                    todo.LastUpdated = (DateTime)reader["LastUpdated"];
                    return todo;
                };
            }
        }
    }
}
