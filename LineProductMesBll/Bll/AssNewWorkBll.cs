using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class AssNewWorkBll
    {
        private readonly Dao.AssNewWorkDao dal=null;
        Dao . EmployeeDao dalE=null;
        public AssNewWorkBll ( )
        {
            dal = new Dao . AssNewWorkDao ( );
            dalE = new Dao . EmployeeDao ( );
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
            return dalE . getDepart ( num );
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
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            return dal . getTablePInfo ( );
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <returns></returns>
        public DataTable getTableColumn ( )
        {
            return dal . getTableColumn ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewQuery ( string strWhere )
        {
            return dal . getTableViewQuery ( strWhere );
        }

        /// <summary>
        /// 获取单头信息
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . AssNewWorkHeaderEntity getHeader ( string oddNum )
        {
            return dal . getHeader ( oddNum );
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
        /// 保存数据
        /// </summary>
        /// <param name="_header"></param>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . AssNewWorkHeaderEntity _header ,DataTable tableView ,DataTable tableViewTwo )
        {
            return dal . Save ( _header ,tableView ,tableViewTwo );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="_header"></param>
        /// <param name="tableView"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . AssNewWorkHeaderEntity _header ,DataTable tableView ,List<string> idxList ,DataTable tableViewTwo ,List<string> idxListOne )
        {
            return dal . Edit ( _header ,tableView ,idxList ,tableViewTwo ,idxListOne );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Exanmie ( string oddNum ,bool result ,string department)
        {
            return dal . Exanmie ( oddNum ,result ,department );
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
        /// 完工数量总和是否少于工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ExistsNum ( LineProductMesEntityu . AssNewWorkHeaderEntity model )
        {
            return dal . ExistsNum ( model );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            return dal . getTablePrintOne ( oddNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            return dal . getTablePrintTwo ( oddNum );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTre ( string oddNum )
        {
            return dal . getTablePrintTre ( oddNum );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintFor ( string oddNum )
        {
            return dal . getTablePrintFor ( oddNum );
        }


        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintFiv ( string oddNum )
        {
            return dal . getTablePrintFiv ( oddNum );
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

    }
}
