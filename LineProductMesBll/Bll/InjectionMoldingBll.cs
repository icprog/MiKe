using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class InjectionMoldingBll
    {
        private readonly Dao.InjectionMoldingDao dal=null;

        public InjectionMoldingBll ( )
        {
            dal = new Dao . InjectionMoldingDao ( );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            return dal . getDepart ( );
        }

        /// <summary>
        /// 获取组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataTable getDepart ( string num )
        {
            return dal . getDepart ( num );
        }

        /// <summary>
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            return dal . getTablePInfo ( );
        }


        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="workId">车间编号</param>
        /// <param name="gruopId">班组编号</param>
        /// <returns></returns>
        public DataTable getUserInfo ( )
        {
            return dal . getUserInfo ( );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Delete ( string oddNum )
        {
            return dal . Delete ( oddNum );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Exanmie ( LineProductMesEntityu . InjectionMoldingHeaderEntity model )
        {
            return dal . Exanmie ( model );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool CancelTion ( string oddNum ,bool result )
        {
            return dal . CancelTion ( oddNum ,result );
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
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . InjectionMoldingHeaderEntity getModel ( string oddNum )
        {
            return dal . getModel ( oddNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOne ( string oddNum )
        {
            return dal . getTableOne ( oddNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableTwo ( string oddNum )
        {
            return dal . getTableTwo ( oddNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableTre ( string oddNum )
        {
            return dal . getTableTre ( oddNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableHeader ( string strWhere ,string strWhere1 ,string strWhere2 )
        {
            return dal . getTableHeader ( strWhere ,strWhere1 ,strWhere2 );
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="tableViewTre"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . InjectionMoldingHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo ,DataTable tableViewTre )
        {
            return dal . Save ( model ,tableViewOne ,tableViewTwo ,tableViewTre );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="tableViewTre"></param>
        /// <param name="idxOne"></param>
        /// <param name="idxTwo"></param>
        /// <param name="idxTre"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . InjectionMoldingHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo ,DataTable tableViewTre ,List<string> idxOne ,List<string> idxTwo ,List<string> idxTre )
        {
            return dal . Edit ( model ,tableViewOne ,tableViewTwo ,tableViewTre ,idxOne ,idxTwo ,idxTre );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            return dal . getTablePrintOne ( oddNum );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            return dal . getTablePrintTwo ( oddNum );
        }


        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTre ( string oddNum )
        {
            return dal . getTablePrintTre ( oddNum );
        }


        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int getTableSur ( string orderNum ,string proNum ,string oddNum ,int? nums)
        {
            return dal . getTableSur ( orderNum ,proNum ,oddNum ,nums );
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int getTableSurTime ( string oddNum ,string orderNum ,string proNum ,int? nums )
        {
            return dal . getTableSurTime ( oddNum ,orderNum ,proNum ,nums );
        }


        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintTre ( string oddNum )
        {
            return dal . getPrintTre ( oddNum );
        }

        /// <summary>
        /// 获取打印列表  报工单  计件
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFor ( string oddNum )
        {
            return dal . getPrintFor ( oddNum );
        }

        /// <summary>
        /// 获取打印列表  报工单  计时
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFiv ( string oddNum )
        {
            return dal . getPrintFiv ( oddNum );
        }

        /// <summary>
        /// 获取打印列表  报工单  计时
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintSix ( string oddNum )
        {
            return dal . getPrintSix ( oddNum );
        }



        }

}
