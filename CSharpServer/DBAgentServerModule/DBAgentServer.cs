using DBAgentProtocol;
using ProtoBuf;
using ServerModule;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;

namespace DBAgentServerModule
{
    public class DBAgentServer : ServerBaseModule
    {
        DBAgentServerConfig config = null;
        public DBAgentServer()
            : base()
        {
        }

        public bool Init(DBAgentServerConfig config)
        {
            this.config = config;

            var methods = typeof(DBAgentPacketReceiver).GetRuntimeMethods().Where(e => e.GetParameters().Length == 2
            && e.GetParameters()[0].ParameterType == typeof(DBAgentUser)
            && e.GetParameters()[1].ParameterType.IsDefined(typeof(ProtoContractAttribute))).ToDictionary(e => e.GetParameters()[1].ParameterType);

            //OleDbEnumerator oe = new OleDbEnumerator();
            //var dt = oe.GetElements();
            //for (int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    Console.WriteLine("Provider: {0}", dt.Rows[i][0].ToString());
            //}

            //SqlConnection conn = new SqlConnection("Driver={MySQL ODBC 8.0 Unicode Driver};Server=127.0.0.1:3306\\SQLEXPRESS;Database=o2o;Uid=admin;Pwd=0000;Trusted_Connection=False;");
            //conn.Open();
            //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True; just sql
            //"Provider=MSDAORA; Data Source=ORACLE8i7;Persist Security Info=False;Integrated Security=Yes"   for orcale
            //Server = 210.211.78.216\NOBDBA,1433; Database = NateOnBizMemo; Trusted_Connection = no; Uid = imuser; Pwd = imuser12#$;Pooling=true;Min Pool Size=5;Max Pool Size=2000
            // ole db
            //OleDbConnection con = new OleDbConnection("Provider=SQLOLEDB;Data Source=localhost\\SQLEXPRESS;Database=master;Integrated Security=SSPI;");
            //OleDbConnection con = new OleDbConnection("Provider=MSOLEDBSQL;Data Source=localhost\\SQLEXPRESS;Database=master;Integrated Security=SSPI;");
            //m_pDBConnector->ConnectDB("Driver={MySQL ODBC 8.0 Unicode Driver};Server=127.0.0.1:3306;Database = o2o;Trusted_Connection=False;Uid=admin;Pwd=0000");
            //"Provider=SQLOLEDB;Data Source=(local);Integrated Security=SSPI"
            //try
            //{
            //    con.Open();
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}

            //OleDbCommand cmd = conn.CreateCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "sp_test";
            //OleDbParameter paramText = cmd.Parameters.Add("test_text", OleDbType.VarChar, 200);
            //OleDbParameter paramId = cmd.Parameters.Add("test_id", OleDbType.Integer);
            //paramId.Direction = ParameterDirection.Output;
            //paramText.Value = "Sent from C# code";
            //cmd.ExecuteNonQuery();
            //Console.WriteLine(paramId.Value);

            var cmd = new OleDbCommand();

            var packets = Assembly.GetAssembly(typeof(DBPacketHeader)).GetTypes().Where(e => e.IsDefined(typeof(ProtoContractAttribute)) &&
            e.IsDefined(typeof(DBPacketAttribute)));

            var packetTypes = new Dictionary<int, Type>();
            foreach (var packet in packets)
            {
                var customAttributeTypedArguments = packet.CustomAttributes.Where(e => e.AttributeType == typeof(DBPacketAttribute)).Select(j => j.ConstructorArguments.Select(k => k));
                if (customAttributeTypedArguments.Count() < 1)
                    continue;

                var arguments = customAttributeTypedArguments.ElementAt(0);
                if (arguments.Count() != 2)
                    continue;

                var packetMode = (eDBPacketMode)arguments.ElementAt(0).Value;
                var packetId = (int)arguments.ElementAt(1).Value;

                if (packetMode == eDBPacketMode.ReceiveDB)
                {
                    packetTypes.Add(packetId, packet);
                }
            }

            var packetInvoker = new ProtoNetPacketInvoker();
            packetInvoker.Init(methods, packetTypes, new DBAgentPacketReceiver());

            return base.Init(config.connectionPoolSize, receiveBufferSize, packetInvoker, typeof(DBAgentUser), config);
        }
    }
}
