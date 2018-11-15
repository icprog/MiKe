using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
   public class LogisticsNewBll
    {
        readonly private Dao.LogisticsNewDao dal=null;
        public LogisticsNewBll ( )
        {
            dal = new Dao . LogisticsNewDao ( );
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableEMP ( )
        {
            return dal . getTableEMP ( );
        }

        /// <summary>
        /// 销货单数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableKEB ( )
        {
            return dal . getTableKEB ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable getTableViewOne ( string code )
        {
            return dal . getTableViewOne ( code );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable getTableViewTwo ( string code )
        {
            return dal . getTableViewTwo ( code );
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
        /// 删除数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Delete ( string code )
        {
            return dal . Delete ( code );
        }

        /// <summary>
        /// 完工数量是否多于数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists ( LineProductMesEntityu . LogisticsNewBodyOneEntity model )
        {
            return dal . Exists ( model );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableOne"></param>
        /// <param name="tableTwo"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . LogisticsNewHeaderEntity model ,DataTable tableOne ,DataTable tableTwo )
        {
            return dal . Save ( model ,tableOne ,tableTwo );
        }

        /// <summary>
        /// 编辑保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableOne"></param>
        /// <param name="tableTwo"></param>
        /// <param name="listOne"></param>
        /// <param name="listTwo"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . LogisticsNewHeaderEntity model ,DataTable tableOne ,DataTable tableTwo ,List<string> listOne ,List<string> listTwo )
        {
            return dal . Edit ( model ,tableOne ,tableTwo ,listOne ,listTwo );
        }

        /// <summary>
        /// 获取查询数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableColumn ( )
        {
            return dal . getTableColumn ( );
        }

        /// <summary>
        /// 获取查询数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            return dal . getTableView ( strWhere );
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LineProductMesEntityu . LogisticsNewHeaderEntity getModel ( string code )
        {
            return dal . getModel ( code );
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
