using System . Data;

namespace LineProductMesBll . Bll
{
    public class InjectionBoardBll
    {
        readonly private Dao.InjectionBoardDao dal=null;
        public InjectionBoardBll ( )
        {
            dal = new Dao . InjectionBoardDao ( );
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
