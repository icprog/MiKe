using System;
using System . Collections . Generic;
using System . Data;

namespace LineProductMesBll . Bll
{
    public class LineForAssPlanBll
    {
        readonly private Dao.LineForAssPlanDao dal=null;
        public LineForAssPlanBll ( )
        {
            dal = new Dao . LineForAssPlanDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( DateTime dtStart ,DateTime dtEnd ,Dictionary<string ,string> strDic )
        {
            return dal . getTableView ( dtStart ,dtEnd,strDic );
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

        /// <summary>
        /// 获取同品号,同日期的排产总量
        /// </summary>
        /// <returns></returns>
        public DataTable getTableGroupSum ( )
        {
            return dal . getTableGroupSum ( );
        }

    }
}
