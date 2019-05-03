using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Data.Framework
{
    public interface IEntity
    {
        long Id { get; }
    }

    public abstract class EntitySqlLite : IEntity
    {
        public long Id { get; internal set; }

        internal abstract List<String> FieldNames { get; }
        internal abstract Action<SQLiteCommand> SetParameters { get; }
        internal abstract Func<SQLiteDataReader, EntitySqlLite> FromReader { get; }
    }
}


