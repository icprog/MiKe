using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class LEEBoardBll
    {
        readonly private Dao.LEEBoardDao dal=null;
        public LEEBoardBll ( )
        {
            dal = new Dao . LEEBoardDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            return dal . getTableView ( );
        }

    }
}
