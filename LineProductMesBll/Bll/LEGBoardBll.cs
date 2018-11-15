using System . Data;

namespace LineProductMesBll . Bll
{
    public class LEGBoardBll
    {
        readonly private Dao.LEGBoardDao dal=null;
        public LEGBoardBll ( )
        {
            dal = new Dao . LEGBoardDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            return dal . getTableView ( );
        }

    }
}
