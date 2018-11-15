using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class SupplierForMouldBll
    {
        private readonly Dao.SupplierForMouldDao dal=null;
        public SupplierForMouldBll ( )
        {
            dal = new Dao . SupplierForMouldDao ( );
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
        /// 获取供应商编号
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
        public int Save ( LineProductMesEntityu . SupplierForMouldEntity model )
        {
            return dal . Save ( model );
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
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu . SupplierForMouldEntity model )
        {
            return dal . CancellationBool ( model );
        }

    }
}
