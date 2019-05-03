using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Framework
{
    public class SqlParameter
    {
        internal string Where { get; set; }
        internal List<KeyValuePair<string, object>> Parameters { get; set; } = new List<KeyValuePair<string, object>>();
    }

    public class Where
    {
        public static SqlParameter And(SqlParameter left, SqlParameter right)
        {
            var param = new SqlParameter() { Where = left.Where + " AND " + right.Where };
            param.Parameters.AddRange(left.Parameters);
            param.Parameters.AddRange(right.Parameters);
            left.Parameters.Clear();
            right.Parameters.Clear();
            return param;
        }

        public static SqlParameter EqualStr(string field, string value)
        {
            var param = new SqlParameter() { Where = $"{field} = @{field}" };
            param.Parameters.Add(new KeyValuePair<string, object>(field, value));
            return param;
        }
    }
}
