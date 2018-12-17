using System . Data;

namespace LineProductMesBll . Bll
{
    public class EmployeeBll
    {
        private readonly Dao.EmployeeDao dal=null;
        public EmployeeBll ( )
        {
            dal = new Dao . EmployeeDao ( );
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="pwdMD5"></param>
        /// <returns></returns>
        public bool yesOrNoLogin ( string userName ,string pwdMD5 )
        {
            return dal . yesOrNoLogin ( userName ,pwdMD5 );
        }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public string GetUserNum ( string username )
        {
            return dal . GetUserNum ( username );
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="numOfPerson"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool EditPw ( string numOfPerson ,string pw )
        {
            return dal . EditPw ( numOfPerson ,pw );
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete ( int id )
        {
            return dal . Delete ( id );
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool Exists ( string num )
        {
            return dal . Exists ( num );
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public int Add ( LineProductMesEntityu . EmployeeEntity _model )
        {
            return dal . Add ( _model );
        }

        /// <summary>
        /// 编辑一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . EmployeeEntity _model )
        {
            return dal . Edit ( _model );
        }

        /// <summary>
        /// 编辑注销状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool Cancel ( string num ,bool sign )
        {
            return dal . Cancel ( num ,sign );
        }

        /// <summary>
        /// 获取人员编号
        /// </summary>
        /// <returns></returns>
        public string GetNum ( )
        {
            return dal . GetNum ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableAll ( )
        {
            return dal . GetDataTableAll ( );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            return dal . getDepart ( );
        }

        /// <summary>
        /// 获取组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataTable getDepart ( string num )
        {
            return dal . getDepart ( num );
        }

        /// <summary>
        /// 获取所有班组
        /// </summary>
        /// <returns></returns>
        public DataTable getDepartPower ( )
        {
            return dal . getDepartPower ( );
        }

    }
}
