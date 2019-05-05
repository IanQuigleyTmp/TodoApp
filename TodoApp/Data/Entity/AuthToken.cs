using Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class AuthToken : EntitySqlLite
    {
        public long OwnerId { get; set; }
        public string Token { get; set; }

        internal override List<string> FieldNames => new List<string>() { "OwnerId", "Token" };

        internal override Action<SQLiteCommand> SetParameters
        {
            get
            {
                return cmd =>
                {
                    cmd.Parameters.Add("@OwnerId", System.Data.DbType.Int32);
                    cmd.Parameters.Add("@Token", System.Data.DbType.String);
                    cmd.Parameters["@Token"].Value = Token;
                    cmd.Parameters["@OwnerId"].Value = OwnerId;
                };
            }
        }

        internal override Func<SQLiteDataReader, EntitySqlLite> FromReader
        {
            get
            {
                return reader =>
                {
                    var authToken = new AuthToken();
                    authToken.Id = (long)reader["Id"];
                    authToken.OwnerId = (long)reader["OwnerId"];
                    authToken.Token = (string)reader["Token"];
                    return authToken;
                };
            }
        }
    }
}
