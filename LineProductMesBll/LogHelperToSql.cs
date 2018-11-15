using System . Text;
using StudentMgr;
using System . Data . SqlClient;

namespace LineProductMesBll
{
    public static class LogHelperToSql
    {
        /// <summary>
        /// 写操作日志到数据库
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void SaveLog ( string cmdText ,string parameters )
        {
            if ( cmdText . Contains ( "MIKLOG" ) )
                return;
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKLOG (" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005,LOG006) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}',GETDATE(),'{2}','{3}','{4}') " ,UserInfoMation . programName ,UserInfoMation . userName ,cmdText . Replace ( "'" ,"''" ) ,parameters ,UserInfoMation . TypeOfOper );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 写操作日志到数据库
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void SaveLog ( string cmdText ,params SqlParameter [ ] parameters )
        {
            if ( cmdText . Contains ( "MIKLOG" ) )
                return;
            string param = string . Empty;
            if ( parameters . Length > 0 )
            {
                for ( int i = 0 ; i < parameters . Length ; i++ )
                {
                    if ( param == string . Empty )
                        param = " [ " + parameters [ i ] . ToString ( ) + ":" + parameters [ i ] . Value + " ] ";
                    else
                        param = param + " [ " + parameters [ i ] . ToString ( ) + ":" + parameters [ i ] . Value + " ] ";
                }
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKLOG (" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005,LOG006) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}',GETDATE(),'{2}','{3}','{4}') " ,UserInfoMation . programName ,UserInfoMation . userName ,cmdText . Replace ( "'" ,"''" ) ,param ,UserInfoMation . TypeOfOper );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 写操作日志到数据库
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void SaveLog ( string cmdText ,params object [ ] parameters )
        {
            SqlParameter [ ] parame = ( SqlParameter [ ] ) parameters;

            if ( cmdText . Contains ( "MIKLOG" ) )
                return;
            string param = string . Empty;
            if ( parame . Length > 0 )
            {
                for ( int i = 0 ; i < parame . Length ; i++ )
                {
                    if ( param == string . Empty )
                        param = " [ " + parame [ i ] . ToString ( ) + ":" + parame [ i ] . Value + " ] ";
                    else
                        param = param + " [ " + parame [ i ] . ToString ( ) + ":" + parame [ i ] . Value + " ] ";
                }
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKLOG (" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005,LOG006) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}',GETDATE(),'{2}','{3}','{4}') " ,UserInfoMation . programName ,UserInfoMation . userName ,cmdText . Replace ( "'" ,"''" ) ,param ,UserInfoMation . TypeOfOper );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

    }
}
