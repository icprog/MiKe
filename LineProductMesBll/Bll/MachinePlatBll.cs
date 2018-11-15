using System . Data;

namespace LineProductMesBll . Bll
{
    public class MachinePlatBll
    {
        private readonly Dao.MachinePlatDao dal=null;
        public MachinePlatBll ( )
        {
            dal = new Dao . MachinePlatDao ( );
        }

        /// <summary>
        /// 获取机台信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            return dal . getTableView ( );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( int idx )
        {
            return dal . Delete ( idx );
        }

        /// <summary>
        /// 获取车间信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableWork ( )
        {
            return dal . getTableWork ( );
        }

        /// <summary>
        /// 获取机台编号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            return dal . getOddNum ( );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save ( LineProductMesEntityu . MachinePlatEntity model )
        {
            return dal . Save ( model );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu . MachinePlatEntity model )
        {
            return dal . CancellationBool ( model );
        }

    }
}
