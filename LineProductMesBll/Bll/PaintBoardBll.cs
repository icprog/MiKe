using System . Data;

namespace LineProductMesBll . Bll
{
    public class PaintBoardBll
    {
        readonly private Dao.PaintBoardDao dal=null;
        public PaintBoardBll ( )
        {
            dal = new Dao . PaintBoardDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( string check )
        {
            return dal . getTableView ( check );
        }

    }
}
