using Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace Data.Entity
{
    public class Login : EntitySqlLite
    {
        public string Username { get; set; }
        public string Password { get; set; }

        internal override List<string> FieldNames => new List<string>() { "Username", "Password" };

        internal override Action<SQLiteCommand>  SetParameters
        {
            get
            {
                return cmd =>
                    {
                        cmd.Parameters.Add("@Username", System.Data.DbType.String);
                        cmd.Parameters.Add("@Password", System.Data.DbType.String);
                        cmd.Parameters["@Username"].Value = Username;
                        cmd.Parameters["@Password"].Value = Password;
                    };            
            }
        }

        internal override Func<SQLiteDataReader, EntitySqlLite> FromReader
        {
            get
            {
                return reader =>
                {
                    var login = new Login();
                    login.Id = (long)reader["Id"];
                    login.Username = (string)reader["Username"];
                    login.Password = (string)reader["Password"];
                    return login;
                };
            }
        }
    }
}
