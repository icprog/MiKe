using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class HardWareWorkBll
    {
        private readonly Dao.HardWareWorkDao dal=null;

        public HardWareWorkBll ( )
        {
            dal = new Dao . HardWareWorkDao ( );
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
        /// 获取工艺信息
        /// </summary>
        /// <returns></returns>
        public DataTable getArtInfo ( string piNum )
        {
            return dal . getArtInfo ( piNum);
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
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . HardWareWorkHeaderEntity getModel ( string oddNum )
        {
            return dal . getModel ( oddNum );
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
        /// 完工数量之和是否多余工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ExistsNum ( LineProductMesEntityu . HardWareWorkHeaderEntity model )
        {
            return dal . ExistsNum ( model );
        }

        /// <summary>
        /// 工序完工数量之和是否多于工单数量  同工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ExistsNums ( LineProductMesEntityu . HardWareWorkHeaderEntity model ,string partNum ,int num )
        {
            return dal . ExistsNums ( model ,partNum ,num );
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . HardWareWorkHeaderEntity model ,DataTable tableView )
        {
            return dal . Save ( model ,tableView );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . HardWareWorkHeaderEntity model ,DataTable tableView ,List<string> idxList )
        {
            return dal . Edit ( model ,tableView ,idxList );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool Exanmie ( string oddNum ,bool state )
        {
            return dal . Exanmie ( oddNum ,state );
        }


        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool CancelTion ( string oddNum ,bool state )
        {
            return dal . CancelTion ( oddNum ,state );
        }

        /// <summary>
        /// 获取数据列
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableColumn ( string strWhere )
        {
            return dal . getTableColumn ( strWhere );
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
        /// 获取剩余数量
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
