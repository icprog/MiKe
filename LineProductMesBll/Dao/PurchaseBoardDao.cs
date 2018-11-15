using System . Text;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class PurchaseBoardDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            SqlHelper . StoreProcedure ( "CNSCGBO" );
            return SqlHelper . ExecuteNoStoreTable ( null );
        }
        /// <summary>
        /// 获取导出数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewExport ( string startTime,string endTime )
        {
            SqlHelper . StoreProcedure ( "CNSCGBOEX" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@STARTDATE",startTime),
                new SqlParameter("@ENDDATE",endTime)
            };
            return SqlHelper . ExecuteNoStoreTable ( parameter );
        }

        /// <summary>
        /// 获取超期采购单
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewOne ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT HDB003,HDB004,DEA057,CONVERT(FLOAT,HDB006) HDB006,CONVERT(DATE,HDB010,112) HDB010,DATEDIFF(DAY,HDB010,GETDATE()) CQ,CONVERT(FLOAT,HDB006-HDB018) HDB FROM DCSHDB A INNER JOIN TPADEA B ON A.HDB003=B.DEA001 WHERE HDB012='N' ORDER BY HDB003,HDB010" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
