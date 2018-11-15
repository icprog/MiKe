using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data;

namespace LineProductMesBll . Dao
{
    public class LEEBoardDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            SqlHelper . StoreProcedure ( "CNSMB" );
            return SqlHelper . ExecuteNoStoreTable ( null );
        }
    }
}
