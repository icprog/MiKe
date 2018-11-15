using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class AssNewWorkEnclosureBll
    {
        private readonly Dao.EmployeeDao dalE=null;
        private readonly Dao.AssNewWorkEnclosureDao dal=null;
        public AssNewWorkEnclosureBll ( )
        {
            dal = new Dao . AssNewWorkEnclosureDao ( );
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
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            return dal . getTablePInfo ( );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getCode ( )
        {
            return dal . getCode ( );
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
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewTwo ( string strWhere )
        {
            return dal . getTableViewTwo ( strWhere );
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
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo )
        {
            return dal . Save ( model ,tableViewOne ,tableViewTwo );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="idxOne"></param>
        /// <param name="idxTwo"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo ,List<string> idxOne ,List<string> idxTwo )
        {
            return dal . Edit ( model ,tableViewOne ,tableViewTwo ,idxOne ,idxTwo );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Examine ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model )
        {
            return dal . Examine ( model );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Cancella ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model )
        {
            return dal . Cancella ( model );
        }

        /// <summary>
        /// 获取组装报工单的单号
        /// </summary>
        /// <returns></returns>
        public DataTable getTableOrder ( string orderNum,string proNum )
        {
            return dal . getTableOrder ( orderNum ,proNum );
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable getPersonInfo ( )
        {
            return dal . getPersonInfo ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
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
        public LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity getModel ( string oddNum )
        {
            return dal . getModel ( oddNum );
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

    }
}
