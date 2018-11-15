using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class MouldInformationDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,MOI001,MOI002,MOI003,MOI004,MOI005,MOI006,MOI007,MOI008,MOI009,MOI010,MOI011,MOI012,MOI013,MOI014,MOI015,MOI016 FROM MIKMOI ORDER BY MOI001 " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public DataTable getTableWork ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '05%' AND DAA001!='05'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns></returns>
        public DataTable getTableSupp ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT SFM001,SFM002,SFM010 FROM MIKSFM" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePart ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DEA001,DEA002,DEA057 FROM TPADEA" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( string codePrefix )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(MOI001) MOI001 FROM MIKMOI WHERE MOI001 LIKE '{0}%'" ,codePrefix );

            string code = string . Empty;
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                code = table . Rows [ 0 ] [ "MOI001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return codePrefix + "001";
                else
                    return codePrefix + ( Convert . ToInt32 ( code . Substring ( 4 ,3 ) ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
            }
            else
                return codePrefix + "001";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save ( LineProductMesEntityu . MouldInformationEntity model ,string codePrefix )
        {
            if ( model . idx < 1 )
            {
                model . MOI001 = getOddNum ( codePrefix );
                return Add ( model );
            }
            else
                return Edit ( model );
        }

        int Add (LineProductMesEntityu.MouldInformationEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKMOI(" );
            strSql . Append ( "MOI001,MOI002,MOI003,MOI004,MOI005,MOI006,MOI007,MOI008,MOI009,MOI010,MOI011,MOI012,MOI013,MOI014,MOI015,MOI016,MOI017,MOI018)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@MOI001,@MOI002,@MOI003,@MOI004,@MOI005,@MOI006,@MOI007,@MOI008,@MOI009,@MOI010,@MOI011,@MOI012,@MOI013,@MOI014,@MOI015,@MOI016,@MOI017,@MOI018)" );
            strSql . Append ( ";select @@IDENTITY" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@MOI001", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI002", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI003", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI004", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI005", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI006", SqlDbType.NVarChar,5),
                    new SqlParameter("@MOI007", SqlDbType.NVarChar,5),
                    new SqlParameter("@MOI008", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI009", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI010", SqlDbType.NVarChar,200),
                    new SqlParameter("@MOI011", SqlDbType.Date,3),
                    new SqlParameter("@MOI012", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI013", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI014", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI015", SqlDbType.NVarChar,200),
                    new SqlParameter("@MOI016", SqlDbType.Bit,1),
                    new SqlParameter("@MOI017", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI018", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . MOI001;
            parameters [ 1 ] . Value = model . MOI002;
            parameters [ 2 ] . Value = model . MOI003;
            parameters [ 3 ] . Value = model . MOI004;
            parameters [ 4 ] . Value = model . MOI005;
            parameters [ 5 ] . Value = model . MOI006;
            parameters [ 6 ] . Value = model . MOI007;
            parameters [ 7 ] . Value = model . MOI008;
            parameters [ 8 ] . Value = model . MOI009;
            parameters [ 9 ] . Value = model . MOI010;
            parameters [ 10 ] . Value = model . MOI011;
            parameters [ 11 ] . Value = model . MOI012;
            parameters [ 12 ] . Value = model . MOI013;
            parameters [ 13 ] . Value = model . MOI014;
            parameters [ 14 ] . Value = model . MOI015;
            parameters [ 15 ] . Value = model . MOI016;
            parameters [ 16 ] . Value = model . MOI017;
            parameters [ 17 ] . Value = model . MOI018;

            return SqlHelper . ExecuteSqlReturnId ( strSql . ToString ( ) ,parameters );
        }

        int Edit ( LineProductMesEntityu . MouldInformationEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKMOI set " );
            strSql . Append ( "MOI001=@MOI001," );
            strSql . Append ( "MOI002=@MOI002," );
            strSql . Append ( "MOI003=@MOI003," );
            strSql . Append ( "MOI004=@MOI004," );
            strSql . Append ( "MOI005=@MOI005," );
            strSql . Append ( "MOI006=@MOI006," );
            strSql . Append ( "MOI007=@MOI007," );
            strSql . Append ( "MOI008=@MOI008," );
            strSql . Append ( "MOI009=@MOI009," );
            strSql . Append ( "MOI010=@MOI010," );
            strSql . Append ( "MOI011=@MOI011," );
            strSql . Append ( "MOI012=@MOI012," );
            strSql . Append ( "MOI013=@MOI013," );
            strSql . Append ( "MOI014=@MOI014," );
            strSql . Append ( "MOI015=@MOI015," );
            strSql . Append ( "MOI016=@MOI016" );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@MOI001", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI002", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI003", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI004", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI005", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI006", SqlDbType.NVarChar,5),
                    new SqlParameter("@MOI007", SqlDbType.NVarChar,5),
                    new SqlParameter("@MOI008", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI009", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI010", SqlDbType.NVarChar,200),
                    new SqlParameter("@MOI011", SqlDbType.Date,3),
                    new SqlParameter("@MOI012", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI013", SqlDbType.NVarChar,20),
                    new SqlParameter("@MOI014", SqlDbType.NVarChar,50),
                    new SqlParameter("@MOI015", SqlDbType.NVarChar,200),
                    new SqlParameter("@MOI016", SqlDbType.Bit,1),
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . MOI001;
            parameters [ 1 ] . Value = model . MOI002;
            parameters [ 2 ] . Value = model . MOI003;
            parameters [ 3 ] . Value = model . MOI004;
            parameters [ 4 ] . Value = model . MOI005;
            parameters [ 5 ] . Value = model . MOI006;
            parameters [ 6 ] . Value = model . MOI007;
            parameters [ 7 ] . Value = model . MOI008;
            parameters [ 8 ] . Value = model . MOI009;
            parameters [ 9 ] . Value = model . MOI010;
            parameters [ 10 ] . Value = model . MOI011;
            parameters [ 11 ] . Value = model . MOI012;
            parameters [ 12 ] . Value = model . MOI013;
            parameters [ 13 ] . Value = model . MOI014;
            parameters [ 14 ] . Value = model . MOI015;
            parameters [ 15 ] . Value = model . MOI016;
            parameters [ 16 ] . Value = model . idx;

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
            strSql . AppendFormat ( "DELETE FROM MIKMOI WHERE idx={0}" ,idx );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu . MouldInformationEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKMOI SET MOI016=@MOI016 WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@MOI016",SqlDbType.Bit),
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = model . MOI016;
            parameter [ 1 ] . Value = model . idx;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

    }
}
