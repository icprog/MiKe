using System;
using System . Data;
using System . Text;
using StudentMgr;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class SupplierForMouldDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,SFM001,SFM002,SFM003,SFM004,SFM005,SFM006,SFM007,SFM008,SFM009,SFM005+' '+SFM007+' '+SFM008+' '+SFM009 U0,SFM010 FROM MIKSFM ORDER BY SFM001" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取供应商编号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(SFM001) SFM001 FROM MIKSFM " );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return "1001";
            else
                return string . IsNullOrEmpty ( table . Rows [ 0 ] [ "SFM001" ] . ToString ( ) ) == true ? "1001" : ( Convert . ToInt32 ( table . Rows [ 0 ] [ "SFM001" ] . ToString ( ) ) + 1 ) . ToString ( );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save ( LineProductMesEntityu . SupplierForMouldEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            if ( model . idx < 1 )
            {
                model . SFM001 = getOddNum ( );
                return Add ( strSql ,model );
            }
            else
                return Edit ( strSql ,model );
        }

        int Add ( StringBuilder strSql ,LineProductMesEntityu . SupplierForMouldEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKSFM(" );
            strSql . Append ( "SFM001,SFM002,SFM003,SFM004,SFM005,SFM007,SFM008,SFM009,SFM006,SFM010)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@SFM001,@SFM002,@SFM003,@SFM004,@SFM005,@SFM007,@SFM008,@SFM009,@SFM006,@SFM010)" );
            strSql . Append ( ";select @@IDENTITY" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@SFM001", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM002", SqlDbType.NVarChar,50),
                    new SqlParameter("@SFM003", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM004", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM005", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM007", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM008", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM009", SqlDbType.NVarChar,50),
                    new SqlParameter("@SFM006", SqlDbType.Bit,1),
                    new SqlParameter("@SFM010", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . SFM001;
            parameters [ 1 ] . Value = model . SFM002;
            parameters [ 2 ] . Value = model . SFM003;
            parameters [ 3 ] . Value = model . SFM004;
            parameters [ 4 ] . Value = model . SFM005;
            parameters [ 5 ] . Value = model . SFM007;
            parameters [ 6 ] . Value = model . SFM008;
            parameters [ 7 ] . Value = model . SFM009;
            parameters [ 8 ] . Value = model . SFM006;
            parameters [ 9 ] . Value = model . SFM010;

            return SqlHelper . ExecuteSqlReturnId ( strSql . ToString ( ) ,parameters );
        }

        int Edit ( StringBuilder strSql ,LineProductMesEntityu . SupplierForMouldEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKSFM set " );
            strSql . Append ( "SFM001=@SFM001," );
            strSql . Append ( "SFM002=@SFM002," );
            strSql . Append ( "SFM003=@SFM003," );
            strSql . Append ( "SFM004=@SFM004," );
            strSql . Append ( "SFM005=@SFM005," );
            strSql . Append ( "SFM007=@SFM007," );
            strSql . Append ( "SFM008=@SFM008," );
            strSql . Append ( "SFM009=@SFM009," );
            strSql . Append ( "SFM006=@SFM006," );
            strSql . Append ( "SFM010=@SFM010 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@SFM001", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM002", SqlDbType.NVarChar,50),
                    new SqlParameter("@SFM003", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM004", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM005", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM007", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM008", SqlDbType.NVarChar,20),
                    new SqlParameter("@SFM009", SqlDbType.NVarChar,50),
                    new SqlParameter("@SFM006", SqlDbType.Bit,1),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@SFM010", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . SFM001;
            parameters [ 1 ] . Value = model . SFM002;
            parameters [ 2 ] . Value = model . SFM003;
            parameters [ 3 ] . Value = model . SFM004;
            parameters [ 4 ] . Value = model . SFM005;
            parameters [ 5 ] . Value = model . SFM007;
            parameters [ 6 ] . Value = model . SFM008;
            parameters [ 7 ] . Value = model . SFM009;
            parameters [ 8 ] . Value = model . SFM006;
            parameters [ 9 ] . Value = model . idx;
            parameters [ 10 ] . Value = model . SFM010;
            int rows = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameters );
            if ( rows > 0 )
                return model . idx;
            else
                return -1;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( int idx )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKSFM WHERE idx={0}" ,idx );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu.SupplierForMouldEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKSFM SET SFM006=@SFM006 WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@SFM006",SqlDbType.Bit),
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = model . SFM006;
            parameter [ 1 ] . Value = model . idx;
            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

    }
}
