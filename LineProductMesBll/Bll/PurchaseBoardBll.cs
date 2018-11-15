using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class PurchaseBoardBll
    {
        readonly private Dao.PurchaseBoardDao dal=null;
        public PurchaseBoardBll ( )
        {
            dal = new Dao . PurchaseBoardDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView (   )
        {
            return dal . getTableView (  );
        }

        /// <summary>
        /// 获取导出数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewExport ( string startTime ,string endTime )
        {
            return dal . getTableViewExport (  startTime , endTime );
        }

        /// <summary>
        /// 获取超期采购单
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewOne ( )
        {
            return dal . getTableViewOne ( );
        }
    }
}
