using System;
using System.Data.SQLite;

namespace Data.Framework
{
    public class InMemoryDatabase
    {
        public static SQLiteConnection Instance { get; private set; } = Initialize();
        
        private static SQLiteConnection Initialize()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();
            Repository.InitializeSchema(connection);

            return connection;
        }

        public static void EraseAll()
        {
            Instance = Initialize();
        }
    }
}
