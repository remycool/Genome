using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;

namespace Cluster_DAL
{

    public class MySqlConnect : IDbConnection
    {
        public MySqlConnection Connection { get; set; }


        /// <summary>
        /// Créer la connexion
        /// </summary>
        /// <param name="connectionString"></param>
        public MySqlConnect(string connectionString)
        {
            Connection = new MySqlConnection { ConnectionString = connectionString };
        }
        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        public void Open()
        {
            Connection.Open();
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Connection.Dispose();
        }

        /// <summary>
        /// Récupère la commande
        /// </summary>
        /// <returns></returns>
        public DbCommand GetCommand()
        {
            var command = new MySqlCommand()
            {
                Connection = Connection
            };
            return command;
        }

        /// <summary>
        /// Récupère l'adapter
        /// </summary>
        /// <returns></returns>
        public DbDataAdapter GetAdapter()
        {
            return new MySqlDataAdapter();
        }

        /// <summary>
        /// Récupère les paramètres de la base de données
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DbParameter GetDbParameter(string name, object value)
        {
            return new MySqlParameter(name, value);
        }

        /// <summary>
        /// Executer la lecteur des données
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public DbDataReader GetDataReader(DbCommand command)
        {
            return command.ExecuteReader();
        }

        #region NOT_IMPLEMENTED

        public string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ConnectionState State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
