using StudentMgr;
using System . Data;

namespace LineProductMesBll . Dao
{
    public class InjectionBoardDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            SqlHelper . StoreProcedure ( "CNSZS" );
            return SqlHelper . ExecuteNoStoreTable ( null );
        }
    }
}
