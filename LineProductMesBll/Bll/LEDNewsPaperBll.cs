using System . Collections . Generic;
using System . Data;

namespace LineProductMesBll . Bll
{
    public class LEDNewsPaperBll
    {
        private readonly Dao.LEDNewsPaperDao dal=null;

        public LEDNewsPaperBll ( )
        {
            dal = new Dao . LEDNewsPaperDao ( );
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
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( string num )
        {
            return dal . getDepart ( num );
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
        /// 获取岗位
        /// </summary>
        /// <returns></returns>
        public DataTable getUserLocal ( )
        {
            return dal . getUserLocal ( );
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
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            return dal . getTableView ( strWhere );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewOne ( string strWhere )
        {
            return dal . getTableViewOne ( strWhere );
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
        public bool Exanmie ( string oddNum ,bool result )
        {
            return dal . Exanmie ( oddNum ,result );
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
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . LEDNewsPaperHeaderEntity model ,DataTable tableView ,DataTable tableViewOne )
        {
            return dal . Save ( model ,tableView ,tableViewOne );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . LEDNewsPaperHeaderEntity model ,DataTable tableView ,DataTable tableViewOne ,List<string> idxList ,List<string> idxListOne )
        {
            return dal . Edit ( model ,tableView ,tableViewOne ,idxList ,idxListOne );
        }

        /// <summary>
        /// 获取查询数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableQuery ( string strWhere )
        {
            return dal . getTableQuery ( strWhere );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . LEDNewsPaperHeaderEntity getModel ( string oddNum )
        {
            return dal . getModel ( oddNum );
        }

        /// <summary>
        /// 总完工数量是否大于工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists ( LineProductMesEntityu . LEDNewsPaperHeaderEntity model )
        {
            return dal . Exists ( model );
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
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( string orderNum ,string proNum ,string oddNum )
        {
            return dal . getTableOtherSur ( orderNum ,proNum ,oddNum );
        }

        /// <summary>
        /// 获取打印列表 报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintTre ( string oddNum )
        {
            return dal . getPrintTre ( oddNum );
        }

        /// <summary>
        /// 获取打印列表 报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFor ( string oddNum )
        {
            return dal . getPrintFor ( oddNum );
        }

        /// <summary>
        /// 获取打印列表 报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFiv ( string oddNum )
        {
            return dal . getPrintFiv ( oddNum );
        }

    }
}
