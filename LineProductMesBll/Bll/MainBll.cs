using System . Collections . Generic;

namespace LineProductMesBll . Bll
{
    public class MainBll
    {
        private readonly Dao.MainDao dal=null;
        public MainBll ( )
        {
            dal = new Dao . MainDao ( );
        }

        /// <summary>
        /// 获取系统目录
        /// </summary>
        /// <returns></returns>
        public List<LineProductMesEntityu . MainEntity> GetModel ( string userNum )
        {
            return dal . GetModel ( userNum );
        }

        /// <summary>
        /// 获取人员权限信息
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="programNum"></param>
        /// <returns></returns>
        public List<LineProductMesEntityu . PowerEntity> getPowerList ( )
        {
            return dal . getPowerList ( );
        }

    }
}
