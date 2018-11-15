using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class ProControlBll
    {
        private readonly Dao.ProControlDao dal=null;
        public ProControlBll ( )
        {
            dal = new Dao . ProControlDao ( );
        }

        /// <summary>
        /// 获取根节点信息
        /// </summary>
        /// <returns></returns>
        public DataTable getPInfo ( )
        {
            return dal . getPInfo ( );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . MainEntity model )
        {
            return dal . Save ( model );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete ( LineProductMesEntityu . MainEntity model )
        {
            return dal . Delete ( model );
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
