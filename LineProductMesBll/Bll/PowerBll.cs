using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
   
    public class PowerBll
    {
        private readonly Dao.PowerDao dal=null;
        public PowerBll ( )
        {
            dal = new Dao . PowerDao ( );
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetPerson ( )
        {
            return dal . GetPerson ( );
        }

        /// <summary>
        /// 获取程序信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetProgram ( )
        {
            return dal . GetProgram ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetDataTable ( string strWhere )
        {
            return dal . GetDataTable ( strWhere );
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( int idx )
        {
            return dal . Delete ( idx );
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public bool Exsits ( LineProductMesEntityu . PowerEntity _model )
        {
            return dal . Exsits ( _model );
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public int Add ( LineProductMesEntityu . PowerEntity _model )
        {
            return dal . Add ( _model );
        }


        /// <summary>
        /// 编辑一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . PowerEntity _model )
        {
            return dal . Edit ( _model );
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
