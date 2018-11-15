using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class SemiProductPlanBll
    {
        readonly private Dao.SemiProductPlanDao dal=null;
        public SemiProductPlanBll ( )
        {
            dal = new Dao . SemiProductPlanDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<LineProductMesEntityu . SemiProductPlanEntity> getListModel ( )
        {
            return dal . getListModel ( );
        }


        /// <summary>
        /// 获取厂内生产件和委外件
        /// </summary>
        /// <returns></returns>
        public DataTable getListAll ( string proName,DateTime dtStart,DateTime dtEnd )
        {
            return dal . getListAll ( proName ,dtStart ,dtEnd );
        }

        /// <summary>
        /// 获取采购件
        /// </summary>
        /// <returns></returns>
        public List<LineProductMesEntityu . SemiProductPlanEntity> getListModelPur ( )
        {
            return dal . getListModelPur ( );
        }

        /// <summary>
        /// 获取采购件
        /// </summary>
        /// <returns></returns>
        public DataTable getListPurAll ( string proName ,DateTime dtStart ,DateTime dtEnd )
        {
            return dal . getListPurAll (  proName , dtStart , dtEnd );
        }

        /// <summary>
        /// 获取所有采购件
        /// </summary>
        /// <returns></returns>
        public DataTable getProTableP ( )
        {
            return dal . getProTableP ( );
        }


        /// <summary>
        /// 获取所有采购件
        /// </summary>
        /// <returns></returns>
        public DataTable getProTableMS ( )
        {
            return dal . getProTableMS ( );
        }

    }
}
