using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBll.Bll
{
    public class ConnecBll
    {
        Dao.ConnecDao _dao = new Dao.ConnecDao( );

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable ( string connstr )
        {
            return _dao.GetDataTable( connstr );
        }


        /// <summary>
        /// 测试数据库是否连接成功
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public bool TestConnection ( string connstr )
        {
            return _dao.TestConnection( connstr );
        }
    }
}
