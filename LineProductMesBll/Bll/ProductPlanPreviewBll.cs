using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class ProductPlanPreviewBll
    {
        readonly private Dao.ProductPlanPreviewDao dal=null;
        public ProductPlanPreviewBll ( )
        {
            dal = new Dao . ProductPlanPreviewDao ( );
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( string proName,DateTime dtStart,DateTime dtEnd )
        {
            return dal . getTableView ( proName ,dtStart ,dtEnd );
        }

        /// <summary>
        /// 读取排产数据
        /// </summary>
        /// <returns></returns>
        public bool ReadPlanData ( )
        {
            return dal . ReadPlanData ( );
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( DataTable table )
        {
            return dal . Save ( table );
        }

        /// <summary>
        /// 获取品号等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getProPlanInfo ( )
        {
            return dal . getProPlanInfo ( );
        }

        /// <summary>
        /// 批量增加排产天数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool SaveDay ( DataTable table )
        {
            return dal . SaveDay ( table );
        }

    }
}
