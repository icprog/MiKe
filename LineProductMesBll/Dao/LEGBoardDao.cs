using StudentMgr;
using System . Data;

namespace LineProductMesBll . Dao
{
    public class LEGBoardDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            SqlHelper . StoreProcedure ( "CNSLED" );
            return SqlHelper . ExecuteNoStoreTable ( null );
        }
    }
}
