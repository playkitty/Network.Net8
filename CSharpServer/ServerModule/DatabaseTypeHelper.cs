using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerModule
{
    //interface abstract  class DacHelper
    //{
    //    public abstract DbType GetDbType(short paramType, string typeName);
    //}

    internal class SqlServerHelper// : DacHelper
    {
        //public override DbType GetDbType(short paramType, string typeName)
        public DbType GetDbType(short paramType, string typeName)
        {
            DbType dbType = DbType.String;
            switch ((OleDbType)paramType)
            {
                case OleDbType.Guid:
                    dbType = DbType.Guid;
                    break;
                case OleDbType.Binary:
                    dbType = DbType.Binary;
                    break;
                case OleDbType.Char:
                    dbType = DbType.SByte;
                    break;
                case OleDbType.WChar:
                    dbType = DbType.Byte;
                    break;
                case OleDbType.Numeric:
                    dbType = DbType.Decimal;
                    break;
                case OleDbType.DBDate:
                    dbType = DbType.DateTime;
                    break;
                case OleDbType.DBTime:
                    dbType = DbType.DateTime;
                    break;
                case OleDbType.DBTimeStamp:
                    dbType = DbType.DateTime;
                    break;
                case OleDbType.VarChar:
                    dbType = DbType.String;
                    break;
                case OleDbType.LongVarChar:
                    dbType = DbType.String;
                    break;
                case OleDbType.VarWChar:
                    dbType = DbType.String;
                    break;
                case OleDbType.LongVarWChar:
                    dbType = DbType.String;
                    break;
                case OleDbType.VarBinary:
                    dbType = DbType.Binary;
                    break;
                case OleDbType.LongVarBinary:
                    dbType = DbType.Binary;
                    break;
                case OleDbType.SmallInt:
                    dbType = DbType.Int16;
                    break;
                case OleDbType.Integer:
                    dbType = DbType.Int32;
                    break;
                case OleDbType.Single:
                    dbType = DbType.Single;
                    break;
                case OleDbType.Double:
                    dbType = DbType.Double;
                    break;
                case OleDbType.Currency:
                    dbType = DbType.Currency;
                    break;
                case OleDbType.Date:
                    dbType = DbType.DateTime;
                    break;
                case OleDbType.BSTR:
                    dbType = DbType.String;
                    break;
                case OleDbType.Boolean:
                    dbType = DbType.Boolean;
                    break;
                case OleDbType.Variant:
                    dbType = DbType.String;
                    break;
                case OleDbType.Decimal:
                    dbType = DbType.Decimal;
                    break;
                case OleDbType.TinyInt:
                    dbType = DbType.Int16;
                    break;
                case OleDbType.UnsignedTinyInt:
                    dbType = DbType.UInt16;
                    break;
                case OleDbType.UnsignedSmallInt:
                    dbType = DbType.UInt16;
                    break;
                case OleDbType.BigInt:
                    dbType = DbType.Decimal;
                    break;
                case OleDbType.Filetime:
                    dbType = DbType.DateTime;
                    break;
            }
            return dbType;
        }
    }
}
