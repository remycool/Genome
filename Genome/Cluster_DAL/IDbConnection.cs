using System;
using System.Data.Common;

namespace Cluster_DAL
{
    public interface IDbConnection:IDisposable
    {
            void Open();
            DbCommand GetCommand();
            DbDataAdapter GetAdapter();
            DbParameter GetDbParameter(string name, object value);
            DbDataReader GetDataReader(DbCommand command);


    }
}
