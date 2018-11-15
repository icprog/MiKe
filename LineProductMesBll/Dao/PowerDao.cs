using StudentMgr;
using System;
using System . Collections;
using System . Collections . Generic;
using System . Data;
using System . Data . SqlClient;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Dao
{
    public class PowerDao
    {
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetPerson ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001,EMP002 FROM MIKEMP WHERE EMP001!='DS' AND EMP034=0" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取程序信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetProgram ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT FOR004,FOR003 FROM MIKFOR WHERE FOR006='1'" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetDataTable ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT A.idx,A.POW002,B.EMP001,A.POW003,FOR004,B.EMP002,POW004,POW005,POW006,POW007,POW008,POW009,POW010,POW011,POW012,POW013,POW016,POW017 FROM MIKPOW A LEFT JOIN MIKEMP B ON A.POW002=B.EMP001 LEFT JOIN MIKFOR C ON A.POW003=C.FOR003 " );
            strSql . Append ( "WHERE " + strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( int idx )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM MIKPOW " );
            strSql . Append ( "WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = idx;

            int row = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            if ( row > 0 )
                return true;
            else
                return false;

        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public bool Exsits ( LineProductMesEntityu . PowerEntity _model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT COUNT(1) COUN FROM MIKPOW " );
            strSql . Append ( "WHERE POW002=@POW002 AND POW003=@POW003" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@POW002",SqlDbType.NVarChar,50),
                new SqlParameter("@POW003",SqlDbType.NVarChar,50)
            };
            parameter [ 0 ] . Value = _model . POW002;
            parameter [ 1 ] . Value = _model . POW003;

            return SqlHelper . Exists ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public int Add ( LineProductMesEntityu . PowerEntity _model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKPOW (" );
            strSql . Append ( "POW002,POW003,POW004,POW005,POW006,POW007,POW008,POW009,POW010,POW011,POW012,POW013,POW016,POW017) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@POW002,@POW003,@POW004,@POW005,@POW006,@POW007,@POW008,@POW009,@POW010,@POW011,@POW012,@POW013,@POW016,@POW017);" );
            strSql . Append ( "SELECT SCOPE_IDENTITY();" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@POW002",SqlDbType.NVarChar,20),
                new SqlParameter("@POW003",SqlDbType.NVarChar,50),
                new SqlParameter("@POW004",SqlDbType.Bit),
                new SqlParameter("@POW005",SqlDbType.Bit),
                new SqlParameter("@POW006",SqlDbType.Bit),
                new SqlParameter("@POW007",SqlDbType.Bit),
                new SqlParameter("@POW008",SqlDbType.Bit),
                new SqlParameter("@POW009",SqlDbType.Bit),
                new SqlParameter("@POW010",SqlDbType.Bit),
                new SqlParameter("@POW011",SqlDbType.Bit),
                new SqlParameter("@POW012",SqlDbType.Bit),
                new SqlParameter("@POW013",SqlDbType.Bit),
                new SqlParameter("@POW016",SqlDbType.Bit),
                new SqlParameter("@POW017",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = _model . POW002;
            parameter [ 1 ] . Value = _model . POW003;
            parameter [ 2 ] . Value = _model . POW004;
            parameter [ 3 ] . Value = _model . POW005;
            parameter [ 4 ] . Value = _model . POW006;
            parameter [ 5 ] . Value = _model . POW007;
            parameter [ 6 ] . Value = _model . POW008;
            parameter [ 7 ] . Value = _model . POW009;
            parameter [ 8 ] . Value = _model . POW010;
            parameter [ 9 ] . Value = _model . POW011;
            parameter [ 10 ] . Value = _model . POW012;
            parameter [ 11 ] . Value = _model . POW013;
            parameter [ 12 ] . Value = _model . POW016;
            parameter [ 13 ] . Value = _model . POW017;

            return SqlHelper . ExecuteSqlReturnId ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 编辑一条记录
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . PowerEntity _model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKPOW SET " );
            strSql . Append ( "POW002=@POW002," );
            strSql . Append ( "POW003=@POW003," );
            strSql . Append ( "POW004=@POW004," );
            strSql . Append ( "POW005=@POW005," );
            strSql . Append ( "POW006=@POW006," );
            strSql . Append ( "POW007=@POW007," );
            strSql . Append ( "POW008=@POW008," );
            strSql . Append ( "POW009=@POW009," );
            strSql . Append ( "POW010=@POW010," );
            strSql . Append ( "POW011=@POW011," );
            strSql . Append ( "POW012=@POW012," );
            strSql . Append ( "POW013=@POW013," );
            strSql . Append ( "POW016=@POW016," );
            strSql . Append ( "POW017=@POW017 " );
            strSql . Append ( "WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@POW002",SqlDbType.NVarChar,20),
                new SqlParameter("@POW003",SqlDbType.NVarChar,50),
                new SqlParameter("@POW004",SqlDbType.Bit),
                new SqlParameter("@POW005",SqlDbType.Bit),
                new SqlParameter("@POW006",SqlDbType.Bit),
                new SqlParameter("@POW007",SqlDbType.Bit),
                new SqlParameter("@POW008",SqlDbType.Bit),
                new SqlParameter("@POW009",SqlDbType.Bit),
                new SqlParameter("@POW010",SqlDbType.Bit),
                new SqlParameter("@POW011",SqlDbType.Bit),
                new SqlParameter("@POW012",SqlDbType.Bit),
                new SqlParameter("@POW013",SqlDbType.Bit),
                new SqlParameter("@POW016",SqlDbType.Bit),
                new SqlParameter("@POW017",SqlDbType.Bit),
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = _model . POW002;
            parameter [ 1 ] . Value = _model . POW003;
            parameter [ 2 ] . Value = _model . POW004;
            parameter [ 3 ] . Value = _model . POW005;
            parameter [ 4 ] . Value = _model . POW006;
            parameter [ 5 ] . Value = _model . POW007;
            parameter [ 6 ] . Value = _model . POW008;
            parameter [ 7 ] . Value = _model . POW009;
            parameter [ 8 ] . Value = _model . POW010;
            parameter [ 9 ] . Value = _model . POW011;
            parameter [ 10 ] . Value = _model . POW012;
            parameter [ 11 ] . Value = _model . POW013;
            parameter [ 12 ] . Value = _model . POW016;
            parameter [ 13 ] . Value = _model . POW017;
            parameter [ 14 ] . Value = _model . idx;

            int row = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            if ( row > 0 )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( DataTable table )
        {
            Hashtable SQLString = new Hashtable ( );
            LineProductMesEntityu . PowerEntity _model = new LineProductMesEntityu . PowerEntity ( );

            foreach ( DataRow row in table . Rows )
            {
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] );
                _model . POW002 = row [ "POW002" ] . ToString ( );
                _model . POW003 = row [ "POW003" ] . ToString ( );
                _model . POW004 = string . IsNullOrEmpty ( row [ "POW004" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW004" ] );
                _model . POW005 = string . IsNullOrEmpty ( row [ "POW005" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW005" ] );
                _model . POW006 = string . IsNullOrEmpty ( row [ "POW006" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW006" ] );
                _model . POW007 = string . IsNullOrEmpty ( row [ "POW007" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW007" ] );
                _model . POW008 = string . IsNullOrEmpty ( row [ "POW008" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW008" ] );
                _model . POW009 = string . IsNullOrEmpty ( row [ "POW009" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW009" ] );
                _model . POW010 = string . IsNullOrEmpty ( row [ "POW010" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW010" ] );
                _model . POW011 = string . IsNullOrEmpty ( row [ "POW011" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW011" ] );
                _model . POW012 = string . IsNullOrEmpty ( row [ "POW012" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW012" ] );
                _model . POW013 = string . IsNullOrEmpty ( row [ "POW013" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW013" ] );
                _model . POW016 = string . IsNullOrEmpty ( row [ "POW016" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW016" ] );
                _model . POW017 = string . IsNullOrEmpty ( row [ "POW017" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "POW017" ] );
                if ( _model . idx > 0 )
                    edit ( SQLString ,_model );
                else
                    add ( SQLString ,_model );
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void edit ( Hashtable SQLString ,LineProductMesEntityu . PowerEntity _model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKPOW SET " );
            strSql . Append ( "POW002=@POW002," );
            strSql . Append ( "POW003=@POW003," );
            strSql . Append ( "POW004=@POW004," );
            strSql . Append ( "POW005=@POW005," );
            strSql . Append ( "POW006=@POW006," );
            strSql . Append ( "POW007=@POW007," );
            strSql . Append ( "POW008=@POW008," );
            strSql . Append ( "POW009=@POW009," );
            strSql . Append ( "POW010=@POW010," );
            strSql . Append ( "POW011=@POW011," );
            strSql . Append ( "POW012=@POW012," );
            strSql . Append ( "POW013=@POW013," );
            strSql . Append ( "POW016=@POW016," );
            strSql . Append ( "POW017=@POW017 " );
            strSql . Append ( "WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@POW002",SqlDbType.NVarChar,20),
                new SqlParameter("@POW003",SqlDbType.NVarChar,50),
                new SqlParameter("@POW004",SqlDbType.Bit),
                new SqlParameter("@POW005",SqlDbType.Bit),
                new SqlParameter("@POW006",SqlDbType.Bit),
                new SqlParameter("@POW007",SqlDbType.Bit),
                new SqlParameter("@POW008",SqlDbType.Bit),
                new SqlParameter("@POW009",SqlDbType.Bit),
                new SqlParameter("@POW010",SqlDbType.Bit),
                new SqlParameter("@POW011",SqlDbType.Bit),
                new SqlParameter("@POW012",SqlDbType.Bit),
                new SqlParameter("@POW013",SqlDbType.Bit),
                new SqlParameter("@POW016",SqlDbType.Bit),
                new SqlParameter("@POW017",SqlDbType.Bit),
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = _model . POW002;
            parameter [ 1 ] . Value = _model . POW003;
            parameter [ 2 ] . Value = _model . POW004;
            parameter [ 3 ] . Value = _model . POW005;
            parameter [ 4 ] . Value = _model . POW006;
            parameter [ 5 ] . Value = _model . POW007;
            parameter [ 6 ] . Value = _model . POW008;
            parameter [ 7 ] . Value = _model . POW009;
            parameter [ 8 ] . Value = _model . POW010;
            parameter [ 9 ] . Value = _model . POW011;
            parameter [ 10 ] . Value = _model . POW012;
            parameter [ 11 ] . Value = _model . POW013;
            parameter [ 12 ] . Value = _model . POW016;
            parameter [ 13 ] . Value = _model . POW017;
            parameter [ 14 ] . Value = _model . idx;
            SQLString . Add ( strSql ,parameter );
        }
        void add ( Hashtable SQLString ,LineProductMesEntityu . PowerEntity _model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKPOW (" );
            strSql . Append ( "POW002,POW003,POW004,POW005,POW006,POW007,POW008,POW009,POW010,POW011,POW012,POW013,POW016,POW017) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@POW002,@POW003,@POW004,@POW005,@POW006,@POW007,@POW008,@POW009,@POW010,@POW011,@POW012,@POW013,@POW016,@POW017);" );
            strSql . Append ( "SELECT SCOPE_IDENTITY();" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@POW002",SqlDbType.NVarChar,20),
                new SqlParameter("@POW003",SqlDbType.NVarChar,50),
                new SqlParameter("@POW004",SqlDbType.Bit),
                new SqlParameter("@POW005",SqlDbType.Bit),
                new SqlParameter("@POW006",SqlDbType.Bit),
                new SqlParameter("@POW007",SqlDbType.Bit),
                new SqlParameter("@POW008",SqlDbType.Bit),
                new SqlParameter("@POW009",SqlDbType.Bit),
                new SqlParameter("@POW010",SqlDbType.Bit),
                new SqlParameter("@POW011",SqlDbType.Bit),
                new SqlParameter("@POW012",SqlDbType.Bit),
                new SqlParameter("@POW013",SqlDbType.Bit),
                new SqlParameter("@POW016",SqlDbType.Bit),
                new SqlParameter("@POW017",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = _model . POW002;
            parameter [ 1 ] . Value = _model . POW003;
            parameter [ 2 ] . Value = _model . POW004;
            parameter [ 3 ] . Value = _model . POW005;
            parameter [ 4 ] . Value = _model . POW006;
            parameter [ 5 ] . Value = _model . POW007;
            parameter [ 6 ] . Value = _model . POW008;
            parameter [ 7 ] . Value = _model . POW009;
            parameter [ 8 ] . Value = _model . POW010;
            parameter [ 9 ] . Value = _model . POW011;
            parameter [ 10 ] . Value = _model . POW012;
            parameter [ 11 ] . Value = _model . POW013;
            parameter [ 12 ] . Value = _model . POW016;
            parameter [ 13 ] . Value = _model . POW017;
            SQLString . Add ( strSql ,parameter );
        }
    }
}
