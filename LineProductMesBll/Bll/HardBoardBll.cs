using System . Data;

namespace LineProductMesBll . Bll
{
    public class HardBoardBll
    {
        readonly private Dao.HardBoardDao dal=null;
        public HardBoardBll ( )
        {
            dal = new Dao . HardBoardDao ( );
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
