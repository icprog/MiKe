using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class ProductPlanBll
    {
        readonly private Dao.ProductPlanDao dal=null;
        public ProductPlanBll ( )
        {
            dal = new Dao . ProductPlanDao ( );
        }

        /// <summary>
        /// 获取排计划的订单
        /// </summary>
        /// <returns></returns>
        public DataTable getOrder ( )
        {
            return dal . getOrder ( );
        }


        /// <summary>
        /// 获取已排的订单，序号，品号，日期信息
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable getArrayOrderAndProduct ( DataTable table )
        {
            return dal . getArrayOrderAndProduct ( table );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( string oddNum )
        {
            return dal . getTableView ( oddNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableViewFor ( string oddNum )
        {
            return dal . getTableViewFor ( oddNum );
        }


        /// <summary>
        /// 回写数量
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool EditOrder ( DataTable table )
        {
            return dal . EditOrder ( table );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            return dal . getOddNum ( );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Delete ( string oddNum ,DataTable table)
        {
            return dal . Delete ( oddNum ,table );
        }

        /// <summary>
        /// 取消  删除已排数量
        /// </summary>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Cancel ( DataTable tableView )
        {
            return dal . Cancel ( tableView );
        }

        /// <summary>
        /// 获取已经排产且启用的计划
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable getTableAllOrder ( DataTable table ,string oddNum )
        {
            return dal . getTableAllOrder ( table ,oddNum );
        }


        /// <summary>
        /// 获取已经排产日期
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable getTableAllTime ( DataTable table )
        {
            return dal . getTableAllTime ( table );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . ProductPlanHeaderEntity model ,DataTable table )
        {
            return dal . Save ( model ,table );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . ProductPlanHeaderEntity model ,DataTable table ,List<string> idxList )
        {
            return dal . Edit ( model ,table ,idxList );
        }

        /// <summary>
        /// 获取需要复制的数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableCopyOne ( string oddNum )
        {
            return dal . getTableCopyOne ( oddNum );
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Copy ( DataTable table ,string oddNum )
        {
            return dal . Copy ( table ,oddNum );
        }

        /// <summary>
        /// 启用或弃用
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Examine ( string oddNum ,int resuState )
        {
            return dal . Examine ( oddNum ,resuState );
        }

        /// <summary>
        /// 获取查询数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableColumn ( string strWhere )
        {
            return dal . getTableColumn ( strWhere );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . ProductPlanHeaderEntity getModel ( string oddNum )
        {
            return dal . getModel ( oddNum );
        }

        /// <summary>
        /// 标准系统的ERP计划和回写的是否一致
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int CheckResult ( DataTable table )
        {
            return dal . CheckResult ( table );
        }

        /// <summary>
        /// 生成ERP计划
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool AddPlan ( string oddNum )
        {
            return dal . AddPlan ( oddNum );
        }

    } 
}
