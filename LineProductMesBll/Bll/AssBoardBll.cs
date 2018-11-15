using System . Data;

namespace LineProductMesBll . Bll
{
    public class AssBoardBll
    {
        readonly private Dao.AssBoardDao dal=null;
        public AssBoardBll ( )
        {
            dal = new Dao . AssBoardDao ( );
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
