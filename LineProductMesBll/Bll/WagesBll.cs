using System;
using System . Data;

namespace LineProductMesBll . Bll
{
    public class WagesBll
    {
        readonly private Dao.WagesDao dal=null;
        public WagesBll ( )
        {
            dal = new Dao . WagesDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable getTableView ( string code )
        {
            return dal . getTableView ( code );
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
        /// 编辑保存数据
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Save ( DataTable tableView ,string code )
        {
            return dal . Save ( tableView ,code );
        }

        /// <summary>
        /// 读取工资
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Read ( DateTime dt ,string code )
        {
            return dal . Read ( dt ,code );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Examine ( string code ,bool result )
        {
            return dal . Examine ( code ,result );
        }


        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableQuery ( string strWhere )
        {
            return dal . getTableQuery ( strWhere );
        }


        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LineProductMesEntityu . WagesHeaderEntity getModel ( string code )
        {
            return dal . getModel ( code );
        }

        /// <summary>
        /// 获取提留工资
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable getTableView ( DateTime dt )
        {
            return dal . getTableView ( dt );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool getCode ( string date )
        {
            return dal . getCode ( date );
        }


        }
}
