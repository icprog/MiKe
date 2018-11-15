using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class MouldInformationBll
    {
        private readonly Dao.MouldInformationDao dal=null;
        public MouldInformationBll ( )
        {
            dal = new Dao . MouldInformationDao ( );
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
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public DataTable getTableWork ( )
        {
            return dal . getTableWork ( );
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns></returns>
        public DataTable getTableSupp ( )
        {
            return dal . getTableSupp ( );
        }

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePart ( )
        {
            return dal . getTablePart ( );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( string codePrefix )
        {
            return dal . getOddNum ( codePrefix );
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save ( LineProductMesEntityu . MouldInformationEntity model,string codePrefix )
        {
            return dal . Save ( model ,codePrefix );
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
        public bool CancellationBool ( LineProductMesEntityu . MouldInformationEntity model )
        {
            return dal . CancellationBool ( model );
        }

    }
}
