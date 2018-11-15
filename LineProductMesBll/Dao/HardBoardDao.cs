using System . Data;
using StudentMgr;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class HardBoardDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( string check )
        {
            SqlHelper . StoreProcedure ( "CNSWJ" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@ART10",check)
            };
            return SqlHelper . ExecuteNoStoreTable ( parameter );
        }

    }
}
