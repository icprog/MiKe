using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMgr;

namespace ConnectionBll.Dao
{
    public class ConnecDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable ( string connstr )
        {
            StringBuilder strSql = new StringBuilder( );
            strSql.Append( "SELECT name FROM master..sysdatabases ORDER BY name " );

            return SqlHelper.ExecuteDataTable( connstr ,strSql.ToString( ) );
        }

        /// <summary>
        /// 测试数据库是否连接成功
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public bool TestConnection ( string connstr )
        {
            return SqlHelper.connectionTest( connstr );
        }
    }
}
