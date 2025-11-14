//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Data.SqlClient;
//using System.Globalization;
//using System.Linq;
//using System.Threading;
//using System.Transactions;
//using System.Data.OleDb;

//namespace ServerModule
//{

//    //example 
//    // DatabaseHelper _dbHelper;
//    //_dbHelper.GetStoredProcCommand("enateon.up_AddBuddy")
//    //using(var dbCommand = GetStoredProcCommand("enateon.up_AddBuddy"))
//    //{
//    //    AddInParameter(dbCommand, "@p_a_cmn", DbType.Int32, cmn);
//    //    ExcuteNonQuery(dbCommand)
//    //    var returnCode = Convert.ToInt32(GetParameterValue(dbCommand, "@r_code")).ToString();
//    //    var returnMsg = Convert.ToString(GetParameterValue(dbCommand, "@r_cmsg"));
//    //}

//    //adhoc Query example 1
//    //StringBuilder sql = new StringBuilder();
//    //sql.Append("SELECT cache_ver");
//    //sql.Append("FROM enateon.GROUP_CACHE WITH(NOLOCK)");
//    //sql.Append("WHERE cmn = @CMN");
//    //using (var dbCommand = GetSqlStrinGCommand(sql.ToString()))
//    //{
//    //    addInParameter(dbCommand, "@CMN", DbType.Int32, cmn);
//    //    var ds = ExcuteDataSet(dbCommand);
//    //    if(DataSet != null && ds.Table[0].Rows.Count > )
//    //    {
//    //        DataRow dr = DataSet.Table[0].Row[0];
//    //        var groupCacheVersion = Convert.ToInt32(DataRelation["cache_ver"]);
//    //    }
//    //}

//    //adhoc Query example 2
//    //StringBuilder sql = new StringBuilder();
//    //sql.Append("SELECT cache_ver");
//    //sql.Append("FROM enateon.GROUP_CACHE WITH(NOLOCK)");
//    //sql.Append("WHERE cmn = @CMN");
//    //using (var dbCommand = GetSqlStrinGCommand(sql.ToString()))
//    //{
//    //    AddInParameter(dbCommand, "@CMN", DbType.Int32, cmn);
//    //    ExctuteNonQuery(dbCommand);
//    //}
    
//    // example 3
//    //SqlConnetion con = new SqlConnection(connectstring);
//    //con.Open();
//    // SqlCommand cmd = new sqlCommand(query, con);
//    // cmd.Parameters.Addwithvale("@owser_cmn", cmn);
//    //cmd.ExcuteNonQuery();
//    // SqlCommand cmd = new sqlCommand(query, con);
//    // cmd.Parameters.Addwithvale("@owser_cmn", cmn);
//    //sqlDataReader dataReader = cmd.ExcuteReader();
//    //dataReader.GetSqlString(0).Value;
//    //dataReader.GetSqlDataTime(1).Value;

//    public class DatabaseHelper
//    {
//        readonly string connectionString;
//        readonly DbProviderFactory dbProviderFactory;

//        public DatabaseHelper(string connectionString)
//            : this(connectionString, SqlClientFactory.Instance)
//        {
//            // ole
//            //OleDbConnection oleCon = new OleDbConnection();
//            //oleCon.Open();
//            //OleDbCommand cmn = new OleDbCommand(query, con);
//            //cmn.CommandType = CommandType.StoredProcedure
//            //var ret = cmn.ExecuteNonQuery();
//            // SqlServer
//            //OleDbConnecton con = new OleDbConnecton;
//            // "Provider=SQLOLEDB.1;Server=;database=;user id=;password=;"
//        }

//        //<Connection type="Memo" connection="Server=210.211.78.216\NOBDBA,1433;Database=NateOnBizMemo;Trusted_Connection=no;Uid=imuser;Pwd=imuser12#$;Pooling=true;Min Pool Size=5;Max Pool Size=2000" />"
//        // connectionString = "Server=210.211.78.216\NOBDBA,1433;Database=NateOnBizMemo;Trusted_Connection=no;Uid=imuser;Pwd=imuser12#$;Pooling=true;Min Pool Size=5;Max Pool Size=2000";
//        protected DatabaseHelper(string connectionString, DbProviderFactory dbProviderFactory)
//        {
//            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("The value can not be null or an empty string.", "connectionString");
//            if (dbProviderFactory == null) throw new ArgumentNullException("dbProviderFactory");

//            this.connectionString = connectionString;
//            this.dbProviderFactory = dbProviderFactory;
//        }

//        public string ConnectionString
//        {
//            get { return connectionString; }
//        }

//        public DbProviderFactory DbProviderFactory
//        {
//            get { return dbProviderFactory; }
//        }

//        public void AddInParameter(DbCommand command,
//            string name,
//            DbType dbType)
//        {
//            AddParameter(command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, null);
//        }

//        public void AddInParameter(DbCommand command,
//            string name,
//            DbType dbType,
//            object value)
//        {
//            AddParameter(command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
//        }

//        public void AddInParameter(DbCommand command,
//            string name,
//            DbType dbType,
//            string sourceColumn,
//            DataRowVersion sourceVersion)
//        {
//            AddParameter(command, name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion, null);
//        }

//        public void AddOutParameter(DbCommand command,
//            string name,
//            DbType dbType,
//            int size)
//        {
//            AddParameter(command, name, dbType, size, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
//        }

//        public void AddParameter(DbCommand command,
//            string name,
//            DbType dbType,
//            ParameterDirection direction,
//            string sourceColumn,
//            DataRowVersion sourceVersion,
//            object value)
//        {
//            AddParameter(command, name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
//        }

//        public void AddParameter(DbCommand command,
//            string name,
//            DbType dbType,
//            int size,
//            ParameterDirection direction,
//            bool nullable,
//            byte precision,
//            byte scale,
//            string sourceColumn,
//            DataRowVersion sourceVersion,
//            object value)
//        {
//            if (command == null) throw new ArgumentNullException("command");
//            if (name == null) throw new ArgumentNullException("name");

//            DbParameter parameter = dbProviderFactory.CreateParameter();
//            // SqlServer
//            if (name[0] != '@')
//            {
//                name.Insert(0, new string('@', 1));
//            }
//            parameter.ParameterName = name;
//            parameter.DbType = dbType;
//            parameter.Size = size;
//            parameter.Value = value ?? DBNull.Value;
//            parameter.Direction = direction;
//            parameter.IsNullable = nullable;
//            parameter.SourceColumn = sourceColumn;
//            parameter.SourceVersion = sourceVersion;
//            command.Parameters.Add(parameter);
//        }

//        public DbConnection CreateConnection()
//        {
//            DbConnection newConnection = dbProviderFactory.CreateConnection();
//            newConnection.ConnectionString = ConnectionString;

//            return newConnection;
//        }

//        internal DbConnection GetNewOpenConnection()
//        {
//            DbConnection connection = null;
//            try
//            {
//                connection = CreateConnection();
//                connection.Open();
//            }
//            catch
//            {
//                if (connection != null)
//                    connection.Close();

//                throw;
//            }

//            return connection;
//        }

//        int DoExecuteNonQuery(DbCommand command)
//        {
//            if (command == null) throw new ArgumentNullException("command");

//            int rowsAffected = command.ExecuteNonQuery();
//            return rowsAffected;
//        }

//        IDataReader DoExecuteReader(DbCommand command,
//            CommandBehavior cmdBehavior)
//        {
//            IDataReader reader = command.ExecuteReader(cmdBehavior);

//            return reader;
//        }

//        object DoExecuteScalar(IDbCommand command)
//        {
//            object returnValue = command.ExecuteScalar();
//            return returnValue;
//        }

//        void DoLoadDataSet(IDbCommand command,
//            DataSet dataSet,
//            string[] tableNames)
//        {
//            if (tableNames == null) throw new ArgumentNullException("tableNames");
//            if (tableNames.Length == 0)
//            {
//                throw new ArgumentException("The table name array used to map results to user-specified table names cannot be empty.", "tableNames");
//            }
//            for (int i = 0; i < tableNames.Length; i++)
//            {
//                if (string.IsNullOrEmpty(tableNames[i])) throw new ArgumentException("The value can not be null or an empty string.", string.Concat("tableNames[", i, "]"));
//            }

//            using (DbDataAdapter adapter = GetDataAdapter())
//            {
//                ((IDbDataAdapter)adapter).SelectCommand = command;

//                string systemCreatedTableNameRoot = "Table";
//                for (int i = 0; i < tableNames.Length; i++)
//                {
//                    string systemCreatedTableName = (i == 0)
//                        ? systemCreatedTableNameRoot
//                        : systemCreatedTableNameRoot + i;

//                    adapter.TableMappings.Add(systemCreatedTableName, tableNames[i]);
//                }

//                adapter.Fill(dataSet);
//            }
//        }

//        public DbDataAdapter GetDataAdapter()
//        {
//            DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter();
//            return adapter;
//        }

//        public object GetParameterValue(DbCommand command, string name)
//        {
//            if (command == null) throw new ArgumentNullException("command");

//            // SqlServer
//            var parameterName = name;
//            if (parameterName[0] != '@')
//            {
//                parameterName = parameterName.Insert(0, new string('@', 1));
//            }

//            return command.Parameters[parameterName].Value;
//        }

//        public DbCommand GetSqlStringCommand(string query)
//        {
//            if (string.IsNullOrEmpty(query)) throw new ArgumentException("The value can not be null or an empty string.", "query");

//            return CreateCommandByCommandType(CommandType.Text, query);
//        }

//        public DbCommand GetStoredProcCommand(string storedProcedureName)
//        {
//            if (string.IsNullOrEmpty(storedProcedureName)) throw new ArgumentException("The value can not be null or an empty string.", "storedProcedureName");

//            return CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);
//        }

//        public DbCommand GetStoredProcCommand(string storedProcedureName,
//            params object[] parameterValues)
//        {
//            if (string.IsNullOrEmpty(storedProcedureName)) throw new ArgumentException("The value can not be null or an empty string.", "storedProcedureName");

//            DbCommand command = CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);
//            if ((command.Parameters.Count - 1) != parameterValues.Length)
//            {
//                throw new InvalidOperationException("The number of parameters does not match number of values for stored procedure.");
//            }
//            for (int i = 0; i < parameterValues.Length; i++)
//            {
//                // SqlServer
//                IDataParameter parameter = command.Parameters[i + 1];
//                var parameterName = parameter.ParameterName;
//                // SqlServer
//                if (parameterName[0] != '@')
//                {
//                    parameterName = parameterName.Insert(0, new string('@', 1));
//                }
//                command.Parameters[parameterName].Value = parameterValues[i] ?? DBNull.Value;
//            }

//            return command;
//        }

//        public void LoadDataSet(DbCommand command,
//            DataSet dataSet,
//            string tableName)
//        {
//            LoadDataSet(command, dataSet, new[] { tableName });
//        }

//        public void LoadDataSet(DbCommand command,
//            DataSet dataSet,
//            string tableName,
//            DbTransaction transaction)
//        {
//            LoadDataSet(command, dataSet, new[] { tableName }, transaction);
//        }

//        public void LoadDataSet(DbCommand command,
//            DataSet dataSet,
//            string[] tableNames)
//        {
//            using (var wrapper = GetOpenConnection())
//            {
//                PrepareCommand(command, wrapper.Connection);
//                DoLoadDataSet(command, dataSet, tableNames);
//            }
//        }

//        public void LoadDataSet(DbCommand command,
//            DataSet dataSet,
//            string[] tableNames,
//            DbTransaction transaction)
//        {
//            PrepareCommand(command, transaction);
//            DoLoadDataSet(command, dataSet, tableNames);
//        }

//        public void LoadDataSet(string storedProcedureName,
//            DataSet dataSet,
//            string[] tableNames,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                LoadDataSet(command, dataSet, tableNames);
//            }
//        }

//        public void LoadDataSet(DbTransaction transaction,
//            string storedProcedureName,
//            DataSet dataSet,
//            string[] tableNames,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                LoadDataSet(command, dataSet, tableNames, transaction);
//            }
//        }

//        public void LoadDataSet(CommandType commandType,
//            string commandText,
//            DataSet dataSet,
//            string[] tableNames)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                LoadDataSet(command, dataSet, tableNames);
//            }
//        }

//        public void LoadDataSet(DbTransaction transaction,
//            CommandType commandType,
//            string commandText,
//            DataSet dataSet,
//            string[] tableNames)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                LoadDataSet(command, dataSet, tableNames, transaction);
//            }
//        }

//        static void PrepareCommand(DbCommand command,
//            DbConnection connection)
//        {
//            if (command == null) throw new ArgumentNullException("command");
//            if (connection == null) throw new ArgumentNullException("connection");

//            command.Connection = connection;
//        }

//        static void PrepareCommand(DbCommand command,
//            DbTransaction transaction)
//        {
//            if (command == null) throw new ArgumentNullException("command");
//            if (transaction == null) throw new ArgumentNullException("transaction");

//            PrepareCommand(command, transaction.Connection);
//            command.Transaction = transaction;
//        }

//        public DataSet ExecuteDataSet(DbCommand command)
//        {
//            DataSet dataSet = new DataSet();
//            dataSet.Locale = CultureInfo.InvariantCulture;
//            LoadDataSet(command, dataSet, "Table");
//            return dataSet;
//        }

//        public DataSet ExecuteDataSet(DbCommand command,
//            DbTransaction transaction)
//        {
//            var dataSet = new DataSet();
//            dataSet.Locale = CultureInfo.InvariantCulture;
//            LoadDataSet(command, dataSet, "Table", transaction);
//            return dataSet;
//        }

//        public DataSet ExecuteDataSet(string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteDataSet(command);
//            }
//        }

//        public DataSet ExecuteDataSet(DbTransaction transaction,
//            string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteDataSet(command, transaction);
//            }
//        }

//        DbCommand CreateCommandByCommandType(CommandType commandType,
//            string commandText)
//        {
//            DbCommand command = dbProviderFactory.CreateCommand();
//            command.CommandType = commandType;
//            command.CommandText = commandText;

//            return command;
//        }

//        public DataSet ExecuteDataSet(CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteDataSet(command);
//            }
//        }

//        public DataSet ExecuteDataSet(DbTransaction transaction,
//            CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteDataSet(command, transaction);
//            }
//        }

//        public int ExecuteNonQuery(DbCommand command)
//        {
//            using (var wrapper = GetOpenConnection())
//            {
//                PrepareCommand(command, wrapper.Connection);
//                return DoExecuteNonQuery(command);
//            }
//        }

//        public int ExecuteNonQuery(DbCommand command,
//            DbTransaction transaction)
//        {
//            PrepareCommand(command, transaction);
//            return DoExecuteNonQuery(command);
//        }

//        public int ExecuteNonQuery(string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteNonQuery(command);
//            }
//        }

//        public int ExecuteNonQuery(DbTransaction transaction,
//            string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteNonQuery(command, transaction);
//            }
//        }

//        public int ExecuteNonQuery(CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteNonQuery(command);
//            }
//        }

//        public int ExecuteNonQuery(DbTransaction transaction,
//            CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteNonQuery(command, transaction);
//            }
//        }

//        public IDataReader ExecuteReader(DbCommand command)
//        {
//            using (DatabaseConnectionWrapper wrapper = GetOpenConnection())
//            {
//                PrepareCommand(command, wrapper.Connection);
//                IDataReader realReader = DoExecuteReader(command, CommandBehavior.Default);
//                return CreateWrappedReader(wrapper, realReader);
//            }
//        }

//        public IDataReader ExecuteReader(DbCommand command,
//            DbTransaction transaction)
//        {
//            PrepareCommand(command, transaction);
//            return DoExecuteReader(command, CommandBehavior.Default);
//        }

//        public IDataReader ExecuteReader(string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteReader(command);
//            }
//        }

//        public IDataReader ExecuteReader(DbTransaction transaction,
//            string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteReader(command, transaction);
//            }
//        }

//        public IDataReader ExecuteReader(CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteReader(command);
//            }
//        }

//        public IDataReader ExecuteReader(DbTransaction transaction,
//            CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteReader(command, transaction);
//            }
//        }

//        public object ExecuteScalar(DbCommand command)
//        {
//            if (command == null) throw new ArgumentNullException("command");

//            using (var wrapper = GetOpenConnection())
//            {
//                PrepareCommand(command, wrapper.Connection);
//                return DoExecuteScalar(command);
//            }
//        }

//        public object ExecuteScalar(DbCommand command,
//            DbTransaction transaction)
//        {
//            PrepareCommand(command, transaction);
//            return DoExecuteScalar(command);
//        }

//        public object ExecuteScalar(string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteScalar(command);
//            }
//        }

//        public object ExecuteScalar(DbTransaction transaction,
//            string storedProcedureName,
//            params object[] parameterValues)
//        {
//            using (DbCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
//            {
//                return ExecuteScalar(command, transaction);
//            }
//        }

//        public object ExecuteScalar(CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteScalar(command);
//            }
//        }

//        public object ExecuteScalar(DbTransaction transaction,
//            CommandType commandType,
//            string commandText)
//        {
//            using (DbCommand command = CreateCommandByCommandType(commandType, commandText))
//            {
//                return ExecuteScalar(command, transaction);
//            }
//        }

//        protected DatabaseConnectionWrapper GetWrappedConnection()
//        {
//            return new DatabaseConnectionWrapper(GetNewOpenConnection());
//        }

//        protected DatabaseConnectionWrapper GetOpenConnection()
//        {
//            DatabaseConnectionWrapper connection = TransactionScopeConnections.GetConnection(this);
//            return connection ?? GetWrappedConnection();
//        }

//        protected IDataReader CreateWrappedReader(DatabaseConnectionWrapper connection, IDataReader innerReader)
//        {
//            return new RefCountingDataReader(connection, innerReader);
//        }

//        protected class DatabaseConnectionWrapper : IDisposable
//        {
//            private int refCount;

//            public DatabaseConnectionWrapper(DbConnection connection)
//            {
//                Connection = connection;
//                refCount = 1;
//            }

//            public DbConnection Connection { get; private set; }

//            public bool IsDisposed
//            {
//                get { return refCount == 0; }
//            }

//            public void Dispose()
//            {
//                Dispose(true);
//            }

//            protected virtual void Dispose(bool disposing)
//            {
//                if (disposing)
//                {
//                    int count = Interlocked.Decrement(ref refCount);
//                    if (count == 0)
//                    {
//                        Connection.Dispose();
//                        Connection = null;
//                        GC.SuppressFinalize(this);
//                    }
//                }
//            }

//            public DatabaseConnectionWrapper AddRef()
//            {
//                Interlocked.Increment(ref refCount);
//                return this;
//            }
//        }

//        static class TransactionScopeConnections
//        {
//            static readonly Dictionary<Transaction, Dictionary<string, DatabaseConnectionWrapper>> transactionConnections =
//                new Dictionary<Transaction, Dictionary<string, DatabaseConnectionWrapper>>();

//            public static DatabaseConnectionWrapper GetConnection(DatabaseHelper dbHelper)
//            {
//                Transaction currentTransaction = Transaction.Current;

//                if (currentTransaction == null)
//                    return null;

//                Dictionary<string, DatabaseConnectionWrapper> connectionList;
//                DatabaseConnectionWrapper connection;

//                lock (transactionConnections)
//                {
//                    if (!transactionConnections.TryGetValue(currentTransaction, out connectionList))
//                    {
//                        // We don't have a list for this transaction, so create a new one
//                        connectionList = new Dictionary<string, DatabaseConnectionWrapper>();
//                        transactionConnections.Add(currentTransaction, connectionList);

//                        // We need to know when this previously unknown transaction is completed too
//                        currentTransaction.TransactionCompleted += OnTransactionCompleted;
//                    }
//                }

//                lock (connectionList)
//                {
//                    // Next we'll see if there is already a connection. If not, we'll create a new connection and add it
//                    // to the transaction's list of connections.
//                    // This collection should only be modified by the thread where the transaction scope was created
//                    // while the transaction scope is active.
//                    // However there's no documentation to confirm this, so we err on the safe side and lock.
//                    if (!connectionList.TryGetValue(dbHelper.ConnectionString, out connection))
//                    {
//                        // we're betting the cost of acquiring a new finer-grained lock is less than 
//                        // that of opening a new connection, and besides this allows threads to work in parallel
//                        var dbConnection = dbHelper.GetNewOpenConnection();
//                        connection = new DatabaseConnectionWrapper(dbConnection);
//                        connectionList.Add(dbHelper.ConnectionString, connection);
//                    }
//                    connection.AddRef();
//                }

//                return connection;
//            }

//            static void OnTransactionCompleted(object sender, TransactionEventArgs e)
//            {
//                Dictionary<string, DatabaseConnectionWrapper> connectionList;

//                lock (transactionConnections)
//                {
//                    if (!transactionConnections.TryGetValue(e.Transaction, out connectionList))
//                    {
//                        // we don't know about this transaction. odd.
//                        return;
//                    }

//                    // we know about this transaction - remove it from the mappings
//                    transactionConnections.Remove(e.Transaction);
//                }

//                lock (connectionList)
//                {
//                    // acquiring this lock should not be necessary unless there's a possibility for this event to be fired
//                    // while the transaction involved in the event is still set as the current transaction for a 
//                    // different thread.
//                    foreach (var connectionWrapper in connectionList.Values)
//                    {
//                        connectionWrapper.Dispose();
//                    }
//                }
//            }
//        }

//        protected abstract class DataReaderWrapper : MarshalByRefObject, IDataReader
//        {
//            private readonly IDataReader innerReader;

//            protected DataReaderWrapper(IDataReader innerReader)
//            {
//                this.innerReader = innerReader;
//            }

//            public IDataReader InnerReader { get { return innerReader; } }

//            public virtual int FieldCount
//            {
//                get { return innerReader.FieldCount; }
//            }

//            public virtual int Depth
//            {
//                get { return innerReader.Depth; }
//            }

//            public virtual bool IsClosed
//            {
//                get { return innerReader.IsClosed; }
//            }

//            public virtual int RecordsAffected
//            {
//                get { return innerReader.RecordsAffected; }
//            }

//            public virtual void Close()
//            {
//                if (!innerReader.IsClosed)
//                {
//                    innerReader.Close();
//                }
//            }

//            public void Dispose()
//            {
//                Dispose(true);
//                GC.SuppressFinalize(this);
//            }

//            protected virtual void Dispose(bool disposing)
//            {
//                if (disposing)
//                {
//                    if (!innerReader.IsClosed)
//                    {
//                        innerReader.Dispose();
//                    }
//                }
//            }

//            public virtual string GetName(int i)
//            {
//                return innerReader.GetName(i);
//            }

//            public virtual string GetDataTypeName(int i)
//            {
//                return innerReader.GetDataTypeName(i);
//            }

//            public virtual Type GetFieldType(int i)
//            {
//                return innerReader.GetFieldType(i);
//            }

//            public virtual object GetValue(int i)
//            {
//                return innerReader.GetValue(i);
//            }

//            public virtual int GetValues(object[] values)
//            {
//                return innerReader.GetValues(values);
//            }

//            public virtual int GetOrdinal(string name)
//            {
//                return innerReader.GetOrdinal(name);
//            }

//            public virtual bool GetBoolean(int i)
//            {
//                return innerReader.GetBoolean(i);
//            }

//            public virtual byte GetByte(int i)
//            {
//                return innerReader.GetByte(i);
//            }

//            public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
//            {
//                return innerReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
//            }

//            public virtual char GetChar(int i)
//            {
//                return innerReader.GetChar(i);
//            }

//            public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
//            {
//                return innerReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
//            }

//            public virtual Guid GetGuid(int i)
//            {
//                return innerReader.GetGuid(i);
//            }

//            public virtual short GetInt16(int i)
//            {
//                return innerReader.GetInt16(i);
//            }

//            public virtual int GetInt32(int i)
//            {
//                return innerReader.GetInt32(i);
//            }

//            public virtual long GetInt64(int i)
//            {
//                return innerReader.GetInt64(i);
//            }

//            public virtual float GetFloat(int i)
//            {
//                return innerReader.GetFloat(i);
//            }

//            public virtual double GetDouble(int i)
//            {
//                return innerReader.GetDouble(i);
//            }

//            public virtual string GetString(int i)
//            {
//                return innerReader.GetString(i);
//            }

//            public virtual decimal GetDecimal(int i)
//            {
//                return innerReader.GetDecimal(i);
//            }

//            public virtual DateTime GetDateTime(int i)
//            {
//                return innerReader.GetDateTime(i);
//            }

//            public virtual IDataReader GetData(int i)
//            {
//                return innerReader.GetData(i);
//            }

//            public virtual bool IsDBNull(int i)
//            {
//                return innerReader.IsDBNull(i);
//            }

//            object IDataRecord.this[int i]
//            {
//                get { return innerReader[i]; }
//            }

//            object IDataRecord.this[string name]
//            {
//                get { return innerReader[name]; }
//            }

//            public virtual DataTable GetSchemaTable()
//            {
//                return innerReader.GetSchemaTable();
//            }

//            public virtual bool NextResult()
//            {
//                return innerReader.NextResult();
//            }

//            public virtual bool Read()
//            {
//                return innerReader.Read();
//            }
//        }

//        class RefCountingDataReader : DataReaderWrapper
//        {
//            private readonly DatabaseConnectionWrapper connectionWrapper;

//            public RefCountingDataReader(DatabaseConnectionWrapper connection, IDataReader innerReader)
//                : base(innerReader)
//            {
//                if (connection == null) throw new ArgumentNullException("connection");
//                if (innerReader == null) throw new ArgumentNullException("innerReader");

//                connectionWrapper = connection;
//                connectionWrapper.AddRef();
//            }

//            public override void Close()
//            {
//                if (!IsClosed)
//                {
//                    base.Close();
//                    connectionWrapper.Dispose();
//                }
//            }

//            protected override void Dispose(bool disposing)
//            {
//                if (disposing)
//                {
//                    if (!IsClosed)
//                    {
//                        base.Dispose(true);
//                        connectionWrapper.Dispose();
//                    }
//                }
//            }
//        }
//    }
//}
