using System . Text;
using StudentMgr;
using System . Data . SqlClient;
using System . Data;
using System;
using System . Threading;

namespace LineProductMesBll . Dao
{
    public class EmployeeDao
    {
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="pwdMD5"></param>
        /// <returns></returns>
        public bool yesOrNoLogin ( string userName ,string pwdMD5 )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT COUNT(1) COUN FROM MIKEMP " );
            strSql . Append ( "WHERE EMP002=@EMP002 AND EMP033=@EMP033 AND EMP034=0" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@EMP002",SqlDbType.NVarChar,20),
                new SqlParameter("@EMP033",SqlDbType.NVarChar,50)
            };
            parameter [ 0 ] . Value = userName;
            parameter [ 1 ] . Value = pwdMD5;

            return SqlHelper . Exists ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public string GetUserNum ( string username )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001 FROM MIKEMP " );
            strSql . Append ( "WHERE EMP002=@EMP002 AND EMP034=0" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@EMP002",SqlDbType.NVarChar,20)
            };
            parameter [ 0 ] . Value = username;

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) ,parameter );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                if ( string . IsNullOrEmpty ( dt . Rows [ 0 ] [ "EMP001" ] . ToString ( ) ) )
                    return string . Empty;
                else
                    return dt . Rows [ 0 ] [ "EMP001" ] . ToString ( );
            }
            else
                return string . Empty;
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="numOfPerson"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool EditPw ( string numOfPerson ,string pw )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKEMP SET " );
            strSql . Append ( "EMP033=@EMP033 " );
            strSql . Append ( "WHERE EMP001=@EMP001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@EMP001",SqlDbType.NVarChar,20),
                new SqlParameter("@EMP033",SqlDbType.NVarChar,50)
            };
            parameter [ 0 ] . Value = numOfPerson;
            parameter [ 1 ] . Value = pw;

            int row = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            if ( row > 0 )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete ( int id )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM MIKEMP " );
            strSql . Append ( "WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = id;

            int row = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            if ( row > 0 )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool Exists ( string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT COUNT(1) FROM MIKEMP " );
            strSql . AppendFormat ( "WHERE EMP001='{0}'" ,num );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public int Add ( LineProductMesEntityu . EmployeeEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKEMP(" );
            strSql . Append ( "EMP001,EMP002,EMP003,EMP004,EMP005,EMP006,EMP007,EMP008,EMP009,EMP010,EMP011,EMP012,EMP013,EMP014,EMP015,EMP016,EMP017,EMP018,EMP019,EMP020,EMP021,EMP022,EMP023,EMP024,EMP025,EMP026,EMP027,EMP028,EMP029,EMP030,EMP031,EMP032,EMP033,EMP034,EMP035,EMP036,EMP037,EMP038)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@EMP001,@EMP002,@EMP003,@EMP004,@EMP005,@EMP006,@EMP007,@EMP008,@EMP009,@EMP010,@EMP011,@EMP012,@EMP013,@EMP014,@EMP015,@EMP016,@EMP017,@EMP018,@EMP019,@EMP020,@EMP021,@EMP022,@EMP023,@EMP024,@EMP025,@EMP026,@EMP027,@EMP028,@EMP029,@EMP030,@EMP031,@EMP032,@EMP033,@EMP034,@EMP035,@EMP036,@EMP037,@EMP038)" );
            strSql . Append ( ";select @@IDENTITY" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@EMP001", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP002", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP003", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP004", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP005", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP006", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP007", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP008", SqlDbType.Bit,1),
                    new SqlParameter("@EMP009", SqlDbType.Date,3),
                    new SqlParameter("@EMP010", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP011", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP012", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP013", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP014", SqlDbType.NVarChar,50),
                    new SqlParameter("@EMP015", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP016", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP017", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP018", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP019", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP020", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP021", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP022", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP023", SqlDbType.Date,3),
                    new SqlParameter("@EMP024", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP025", SqlDbType.Bit,1),
                    new SqlParameter("@EMP026", SqlDbType.NVarChar,200),
                    new SqlParameter("@EMP027", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP028", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP029", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP030", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP031", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP032", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP033", SqlDbType.NVarChar,50),
                    new SqlParameter("@EMP034", SqlDbType.Bit,1),
                    new SqlParameter("@EMP035", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP036", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP037", SqlDbType.Bit,1),
                    new SqlParameter("@EMP038", SqlDbType.NVarChar,100)
            };
            parameters [ 0 ] . Value = model . EMP001;
            parameters [ 1 ] . Value = model . EMP002;
            parameters [ 2 ] . Value = model . EMP003;
            parameters [ 3 ] . Value = model . EMP004;
            parameters [ 4 ] . Value = model . EMP005;
            parameters [ 5 ] . Value = model . EMP006;
            parameters [ 6 ] . Value = model . EMP007;
            parameters [ 7 ] . Value = model . EMP008;
            parameters [ 8 ] . Value = model . EMP009;
            parameters [ 9 ] . Value = model . EMP010;
            parameters [ 10 ] . Value = model . EMP011;
            parameters [ 11 ] . Value = model . EMP012;
            parameters [ 12 ] . Value = model . EMP013;
            parameters [ 13 ] . Value = model . EMP014;
            parameters [ 14 ] . Value = model . EMP015;
            parameters [ 15 ] . Value = model . EMP016;
            parameters [ 16 ] . Value = model . EMP017;
            parameters [ 17 ] . Value = model . EMP018;
            parameters [ 18 ] . Value = model . EMP019;
            parameters [ 19 ] . Value = model . EMP020;
            parameters [ 20 ] . Value = model . EMP021;
            parameters [ 21 ] . Value = model . EMP022;
            parameters [ 22 ] . Value = model . EMP023;
            parameters [ 23 ] . Value = model . EMP024;
            parameters [ 24 ] . Value = model . EMP025;
            parameters [ 25 ] . Value = model . EMP026;
            parameters [ 26 ] . Value = model . EMP027;
            parameters [ 27 ] . Value = model . EMP028;
            parameters [ 28 ] . Value = model . EMP029;
            parameters [ 29 ] . Value = model . EMP030;
            parameters [ 30 ] . Value = model . EMP031;
            parameters [ 31 ] . Value = model . EMP032;
            parameters [ 32 ] . Value = model . EMP033;
            parameters [ 33 ] . Value = model . EMP034;
            parameters [ 34 ] . Value = model . EMP035;
            parameters [ 35 ] . Value = model . EMP036;
            parameters [ 36 ] . Value = model . EMP037;
            parameters [ 37 ] . Value = model . EMP038;

            return SqlHelper . ExecuteSqlReturnId ( strSql . ToString ( ) ,parameters );
        }

        /// <summary>
        /// 编辑一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . EmployeeEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKEMP set " );
            strSql . Append ( "EMP002=@EMP002," );
            strSql . Append ( "EMP003=@EMP003," );
            strSql . Append ( "EMP004=@EMP004," );
            strSql . Append ( "EMP005=@EMP005," );
            strSql . Append ( "EMP006=@EMP006," );
            strSql . Append ( "EMP007=@EMP007," );
            strSql . Append ( "EMP008=@EMP008," );
            strSql . Append ( "EMP009=@EMP009," );
            strSql . Append ( "EMP010=@EMP010," );
            strSql . Append ( "EMP011=@EMP011," );
            strSql . Append ( "EMP012=@EMP012," );
            strSql . Append ( "EMP013=@EMP013," );
            strSql . Append ( "EMP014=@EMP014," );
            strSql . Append ( "EMP015=@EMP015," );
            strSql . Append ( "EMP016=@EMP016," );
            strSql . Append ( "EMP017=@EMP017," );
            strSql . Append ( "EMP018=@EMP018," );
            strSql . Append ( "EMP019=@EMP019," );
            strSql . Append ( "EMP020=@EMP020," );
            strSql . Append ( "EMP021=@EMP021," );
            strSql . Append ( "EMP022=@EMP022," );
            strSql . Append ( "EMP023=@EMP023," );
            strSql . Append ( "EMP024=@EMP024," );
            strSql . Append ( "EMP025=@EMP025," );
            strSql . Append ( "EMP026=@EMP026," );
            strSql . Append ( "EMP027=@EMP027," );
            strSql . Append ( "EMP028=@EMP028," );
            strSql . Append ( "EMP029=@EMP029," );
            strSql . Append ( "EMP030=@EMP030," );
            strSql . Append ( "EMP031=@EMP031," );
            strSql . Append ( "EMP032=@EMP032," );
            strSql . Append ( "EMP034=@EMP034," );
            strSql . Append ( "EMP035=@EMP035," );
            strSql . Append ( "EMP036=@EMP036," );
            strSql . Append ( "EMP037=@EMP037," );
            strSql . Append ( "EMP038=@EMP038" );
            strSql . Append ( " where EMP001=@EMP001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@EMP002", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP003", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP004", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP005", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP006", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP007", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP008", SqlDbType.Bit,1),
                    new SqlParameter("@EMP009", SqlDbType.Date,3),
                    new SqlParameter("@EMP010", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP011", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP012", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP013", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP014", SqlDbType.NVarChar,50),
                    new SqlParameter("@EMP015", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP016", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP017", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP018", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP019", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP020", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP021", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP022", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP023", SqlDbType.Date,3),
                    new SqlParameter("@EMP024", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP025", SqlDbType.Bit,1),
                    new SqlParameter("@EMP026", SqlDbType.NVarChar,200),
                    new SqlParameter("@EMP027", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP028", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP029", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP030", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP031", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP032", SqlDbType.Decimal,9),
                    new SqlParameter("@EMP034", SqlDbType.Bit,1),
                    new SqlParameter("@EMP001", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP035", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP036", SqlDbType.NVarChar,20),
                    new SqlParameter("@EMP037", SqlDbType.Bit,1),
                    new SqlParameter("@EMP038", SqlDbType.NVarChar,100)
            };
            parameters [ 0 ] . Value = model . EMP002;
            parameters [ 1 ] . Value = model . EMP003;
            parameters [ 2 ] . Value = model . EMP004;
            parameters [ 3 ] . Value = model . EMP005;
            parameters [ 4 ] . Value = model . EMP006;
            parameters [ 5 ] . Value = model . EMP007;
            parameters [ 6 ] . Value = model . EMP008;
            parameters [ 7 ] . Value = model . EMP009;
            parameters [ 8 ] . Value = model . EMP010;
            parameters [ 9 ] . Value = model . EMP011;
            parameters [ 10 ] . Value = model . EMP012;
            parameters [ 11 ] . Value = model . EMP013;
            parameters [ 12 ] . Value = model . EMP014;
            parameters [ 13 ] . Value = model . EMP015;
            parameters [ 14 ] . Value = model . EMP016;
            parameters [ 15 ] . Value = model . EMP017;
            parameters [ 16 ] . Value = model . EMP018;
            parameters [ 17 ] . Value = model . EMP019;
            parameters [ 18 ] . Value = model . EMP020;
            parameters [ 19 ] . Value = model . EMP021;
            parameters [ 20 ] . Value = model . EMP022;
            parameters [ 21 ] . Value = model . EMP023;
            parameters [ 22 ] . Value = model . EMP024;
            parameters [ 23 ] . Value = model . EMP025;
            parameters [ 24 ] . Value = model . EMP026;
            parameters [ 25 ] . Value = model . EMP027;
            parameters [ 26 ] . Value = model . EMP028;
            parameters [ 27 ] . Value = model . EMP029;
            parameters [ 28 ] . Value = model . EMP030;
            parameters [ 29 ] . Value = model . EMP031;
            parameters [ 30 ] . Value = model . EMP032;
            parameters [ 31 ] . Value = model . EMP034;
            parameters [ 32 ] . Value = model . EMP001;
            parameters [ 33 ] . Value = model . EMP035;
            parameters [ 34 ] . Value = model . EMP036;
            parameters [ 35 ] . Value = model . EMP037;
            parameters [ 36 ] . Value = model . EMP038;

            int row = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameters );
            if ( row > 0 )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 编辑注销状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool Cancel ( string num ,bool sign )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKEMP SET " );
            strSql . Append ( "EMP034=@EMP034 " );
            strSql . Append ( "WHERE EMP001=@EMP001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@EMP034",SqlDbType.Bit),
                new SqlParameter("@EMP001",SqlDbType.NVarChar)
            };
            parameter [ 0 ] . Value = sign;
            parameter [ 1 ] . Value = num;

            int row = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            if ( row > 0 )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取人员编号
        /// </summary>
        /// <returns></returns>
        public string GetNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(EMP001) EMP001 FROM MIKEMP WHERE EMP001!='DS' AND EMP034=0 " );

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                if ( string . IsNullOrEmpty ( dt . Rows [ 0 ] [ "EMP001" ] . ToString ( ) ) )
                    return "10001";
                else
                    return ( Convert . ToInt32 ( dt . Rows [ 0 ] [ "EMP001" ] . ToString ( ) ) + 1 ) . ToString ( );
            }
            else
                return "10001";
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableAll ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,EMP001,EMP002,EMP003,EMP004,EMP005,EMP006,EMP007,CASE WHEN EMP008=0 THEN '男' ELSE '女' END EMP008,EMP009,EMP010,EMP011,EMP012,EMP013,EMP014,EMP015,EMP016,EMP017,EMP018,EMP019,EMP020,EMP021,EMP022,EMP023,EMP024,CASE WHEN EMP025=0 THEN '离职' ELSE '在职' END EMP025,EMP026,CONVERT(FLOAT,EMP027) EMP027,CONVERT(FLOAT,EMP028) EMP028,CONVERT(FLOAT,EMP029) EMP029,CONVERT(FLOAT,EMP030) EMP030,CONVERT(FLOAT,EMP031) EMP031,CONVERT(FLOAT,EMP032) EMP032,EMP034,EMP035,EMP036,ISNULL(EMP010,'')+' '+ISNULL(EMP011,'')+' '+ISNULL(EMP035,'')+''+EMP014 U2,EMP012+' '+EMP013+' '+EMP036 U3,DATEDIFF(YEAR,EMP009,GETDATE()) U0,DATEDIFF(YEAR,EMP023,GETDATE()) U1,EMP037,EMP038 FROM MIKEMP WHERE EMP001!='DS'  ORDER BY EMP001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE LEN(DAA001)=2 ORDER BY DAA001" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataTable getDepart ( string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '{0}%' ORDER BY DAA001" ,num );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        /// <summary>
        /// 获取所有班组
        /// </summary>
        /// <returns></returns>
        public DataTable getDepartPower ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA002 FROM TPADAA WHERE DAA001 like '050%' ORDER BY DAA001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 是否存在多个身份证
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool checkCode ( string card )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) c FROM MIKEMP WHERE EMP016='{0}'" ,card );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                int cards = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "c" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "c" ] );
                return cards > 1 ? true : false;
            }
            else
                return false;
        }
    }
}
