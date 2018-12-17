using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class PaintNewspaperBll
    {
        private readonly Dao.PaintNewspaperDao dal;
        public PaintNewspaperBll ( )
        {
            dal = new Dao . PaintNewspaperDao ( );
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
        public DataTable tableViewOne ( string strWhere )
        {
            return dal . tableViewOne ( strWhere );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable tableViewTwo ( string strWhere )
        {
            return dal . tableViewTwo ( strWhere );
        }

        /// <summary>
        /// 删除记录
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
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . PaintNewspaperHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo )
        {
            return dal . Save ( model ,tableViewOne ,tableViewTwo );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="idxListOne"></param>
        /// <param name="idxListTwo"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . PaintNewspaperHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo ,List<string> idxListOne ,List<string> idxListTwo )
        {
            return dal . Edit ( model ,tableViewOne ,tableViewTwo ,idxListOne ,idxListTwo );
        }

        /// <summary>
        /// 获取查询数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable tableViewQuery ( string strWhere )
        {
            return dal . tableViewQuery ( strWhere );
        }

        /// <summary>
        /// 获取工艺
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable tableArt ( string piNum )
        {
            return dal . tableArt ( piNum );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . PaintNewspaperHeaderEntity getModel ( string oddNum )
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
        /// 获取数据列表
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableArt ( string piNum )
        {
            return dal . getTableArt ( piNum );
        }

        public DataTable getTableArtForAll ( string piNum )
        {
            return dal . getTableArtForAll ( piNum );
        }

        /// <summary>
        /// 获取工艺信息数据
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableA ( string piNum )
        {
            return dal . getTableA ( piNum );
        }


        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( LineProductMesEntityu . PaintNewspaperBodyOneEntity model )
        {
            return dal . getTableOtherSur ( model );
        }

    }
}
