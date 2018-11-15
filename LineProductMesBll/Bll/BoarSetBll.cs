using System . Data;

namespace LineProductMesBll . Bll
{
    public class BoarSetBll
    {
        readonly private Dao.BoarSetDao dal=null;
        public BoarSetBll ( )
        {
            dal = new Dao . BoarSetDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            return dal . getTableView ( );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( DataTable table )
        {
            return dal . Save ( table );
        }

    }
}
