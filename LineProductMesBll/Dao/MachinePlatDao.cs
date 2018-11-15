using System . Data;
using System . Text;
using StudentMgr;
using System;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class MachinePlatDao
    {
        /// <summary>
        /// 获取机台信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,MAP001,MAP002,MAP003,MAP004,MAP005,MAP006,MAP007 FROM MIKMAP ORDER BY MAP003,MAP001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( int idx )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKMAP WHERE idx={0}" ,idx );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取车间信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableWork ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '05%' AND DAA001!='05'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取机台编号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(MAP001) MAP001 FROM MIKMAP " );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
                return string . IsNullOrEmpty ( table . Rows [ 0 ] [ "MAP001" ] . ToString ( ) ) == true ? "1001" : ( Convert . ToInt32 ( table . Rows [ 0 ] [ "MAP001" ] . ToString ( ) ) + 1 ) . ToString ( );
            else
                return "1001";
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save ( LineProductMesEntityu . MachinePlatEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            if ( model . idx < 1 )
            {
                model . MAP001 = getOddNum ( );
                return Add ( strSql ,model );
            }
            else
                return Edit ( strSql ,model );
        }

        int Add (StringBuilder strSql ,LineProductMesEntityu . MachinePlatEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKMAP(" );
            strSql . Append ( "MAP001,MAP002,MAP003,MAP004,MAP005,MAP006)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@MAP001,@MAP002,@MAP003,@MAP004,@MAP005,@MAP006)" );
            strSql . Append ( ";select @@IDENTITY" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@MAP001", SqlDbType.NVarChar,20),
                    new SqlParameter("@MAP002", SqlDbType.NVarChar,50),
                    new SqlParameter("@MAP003", SqlDbType.NVarChar,20),
                    new SqlParameter("@MAP004", SqlDbType.NVarChar,20),
                    new SqlParameter("@MAP005", SqlDbType.NVarChar,50),
                    new SqlParameter("@MAP006", SqlDbType.NVarChar,150)
            };
            parameters [ 0 ] . Value = model . MAP001;
            parameters [ 1 ] . Value = model . MAP002;
            parameters [ 2 ] . Value = model . MAP003;
            parameters [ 3 ] . Value = model . MAP004;
            parameters [ 4 ] . Value = model . MAP005;
            parameters [ 5 ] . Value = model . MAP006;

            return SqlHelper . ExecuteSqlReturnId ( strSql . ToString ( ) ,parameters );
        }

        int Edit ( StringBuilder strSql ,LineProductMesEntityu . MachinePlatEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKMAP set " );
            strSql . Append ( "MAP001=@MAP001," );
            strSql . Append ( "MAP002=@MAP002," );
            strSql . Append ( "MAP003=@MAP003," );
            strSql . Append ( "MAP004=@MAP004," );
            strSql . Append ( "MAP005=@MAP005," );
            strSql . Append ( "MAP006=@MAP006 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@MAP001", SqlDbType.NVarChar,20),
                    new SqlParameter("@MAP002", SqlDbType.NVarChar,50),
                    new SqlParameter("@MAP003", SqlDbType.NVarChar,20),
                    new SqlParameter("@MAP004", SqlDbType.NVarChar,20),
                    new SqlParameter("@MAP005", SqlDbType.NVarChar,50),
                    new SqlParameter("@MAP006", SqlDbType.NVarChar,150),
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . MAP001;
            parameters [ 1 ] . Value = model . MAP002;
            parameters [ 2 ] . Value = model . MAP003;
            parameters [ 3 ] . Value = model . MAP004;
            parameters [ 4 ] . Value = model . MAP005;
            parameters [ 5 ] . Value = model . MAP006;
            parameters [ 6 ] . Value = model . idx;

            return SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameters );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu . MachinePlatEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKMAP SET MAP007=@MAP007 WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@MAP007",SqlDbType.Bit),
                new SqlParameter("@idx",SqlDbType.Int,4)
            };
            parameter [ 0 ] . Value = model . MAP007;
            parameter [ 1 ] . Value = model . idx;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

    }
}
