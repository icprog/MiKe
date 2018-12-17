using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class LogisticsNewDao
    {
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableEMP ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP037=1 AND EMP004='生产部' AND EMP007='成品搬运工'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 销货单数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableKEB ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT KEB001,KEB002,KEB003,KEB004,CONVERT(FLOAT,KEB007) KEB007,CONVERT(FLOAT,KEB010) KEB010,ARS011 FROM JSKKEB A LEFT JOIN MIKARS B ON A.KEB003=B.ARS001 WHERE KEB021='T'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable getTableViewOne ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,LOG001,LOG002,LOG003,LOG004,LOG005,LOG006,LOG007,LOG008,LOG009,0 U4 FROM MIKLGO WHERE LOG001='{0}'" ,code );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable getTableViewTwo ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,LGP001,LGP002,LGP003,LGP004,LGP005,LGP006,LGP007,LGP008,LGP009,LGP010,LGP011,LGP012,LGP013,LGP014 FROM MIKLGP WHERE LGP001='{0}'" ,code );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getCode ( )
        {
            DateTime dt = UserInfoMation . sysTime;
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(LGN001) LGN001 FROM MIKLGN WHERE LGN001 LIKE '{0}%'" ,dt . ToString ( "yyyyMMdd" ) );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return dt . ToString ( "yyyyMMdd" ) + "001";
            else if ( string . IsNullOrEmpty ( table . Rows [ 0 ] [ "LGN001" ] . ToString ( ) ) )
                return dt . ToString ( "yyyyMMdd" ) + "001";
            else
                return ( Convert . ToInt64 ( table . Rows [ 0 ] [ "LGN001" ] . ToString ( ) ) + 1 ) . ToString ( );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Delete ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKLGN WHERE LGN001='{0}'" ,code );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 完工数量是否多于数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists ( LineProductMesEntityu.LogisticsNewBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT SUM(LOG008) LOG008 FROM MIKLGO WHERE LOG001!='{0}' AND LOG002='{1}' AND LOG003='{2}' " ,model . LOG001 ,model . LOG002 ,model . LOG003 );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return false;
            int resu = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "LOG008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "LOG008" ] . ToString ( ) );
            if ( resu + model . LOG008 > model . LOG006 )
                return true;
            return false;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableOne"></param>
        /// <param name="tableTwo"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . LogisticsNewHeaderEntity model ,DataTable tableOne ,DataTable tableTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            model . LGN001 = getCode ( );
            model . LGN008 = UserInfoMation . userName;
            AddHeader ( SQLString ,model );
            UserInfoMation . oddNum = model . LGN001;

            LineProductMesEntityu . LogisticsNewBodyOneEntity bodyOne = new LineProductMesEntityu . LogisticsNewBodyOneEntity ( );
            bodyOne . LOG001 = model . LGN001;
            foreach ( DataRow row in tableOne . Rows )
            {
                bodyOne . LOG002 = row [ "LOG002" ] . ToString ( );
                bodyOne . LOG003 = row [ "LOG003" ] . ToString ( );
                bodyOne . LOG004 = row [ "LOG004" ] . ToString ( );
                bodyOne . LOG005 = row [ "LOG005" ] . ToString ( );
                bodyOne . LOG006 = string . IsNullOrEmpty ( row [ "LOG006" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LOG006" ] . ToString ( ) );
                bodyOne . LOG007 = string . IsNullOrEmpty ( row [ "LOG007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG007" ] . ToString ( ) );
                bodyOne . LOG008 = string . IsNullOrEmpty ( row [ "LOG008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LOG008" ] . ToString ( ) );
                bodyOne . LOG009 = string . IsNullOrEmpty ( row [ "LOG009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG009" ] . ToString ( ) );
                AddBodyOne ( SQLString ,bodyOne );
            }

            LineProductMesEntityu . LogisticsNewBodyTwoEntity bodyTwo = new LineProductMesEntityu . LogisticsNewBodyTwoEntity ( );
            bodyTwo . LGP001 = model . LGN001;
            foreach ( DataRow row in tableTwo . Rows )
            {
                bodyTwo . LGP002 = row [ "LGP002" ] . ToString ( );
                bodyTwo . LGP003 = row [ "LGP003" ] . ToString ( );
                bodyTwo . LGP004 = row [ "LGP004" ] . ToString ( );
                bodyTwo . LGP005 = row [ "LGP005" ] . ToString ( );
                bodyTwo . LGP006 = row [ "LGP006" ] . ToString ( );
                if ( row [ "LGP007" ] == null || row [ "LGP007" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP007 = null;
                else
                    bodyTwo . LGP007 = Convert . ToDateTime ( row [ "LGP007" ] );
                if ( row [ "LGP008" ] == null || row [ "LGP008" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP008 = null;
                else
                    bodyTwo . LGP008 = Convert . ToDateTime ( row [ "LGP008" ] );
                if ( row [ "LGP009" ] == null || row [ "LGP009" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP009 = null;
                else
                    bodyTwo . LGP009 = Convert . ToDateTime ( row [ "LGP009" ] );
                if ( row [ "LGP010" ] == null || row [ "LGP010" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP010 = null;
                else
                    bodyTwo . LGP010 = Convert . ToDateTime ( row [ "LGP010" ] );
                if ( row [ "LGP011" ] == null || row [ "LGP011" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP011 = null;
                else
                    bodyTwo . LGP011 = Convert . ToDecimal ( row [ "LGP011" ] );
                if ( row [ "LGP012" ] == null || row [ "LGP012" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP012 = null;
                else
                    bodyTwo . LGP012 = Convert . ToDecimal ( row [ "LGP012" ] );
                if ( row [ "LGP013" ] == null || row [ "LGP013" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP013 = null;
                else
                    bodyTwo . LGP013 = Convert . ToDecimal ( row [ "LGP013" ] );
                if ( row [ "LGP014" ] == null || row [ "LGP014" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP014 = null;
                else
                    bodyTwo . LGP014 = Convert . ToDecimal ( row [ "LGP014" ] );
                AddBodyTwo ( SQLString ,bodyTwo );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 编辑保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableOne"></param>
        /// <param name="tableTwo"></param>
        /// <param name="listOne"></param>
        /// <param name="listTwo"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . LogisticsNewHeaderEntity model ,DataTable tableOne ,DataTable tableTwo ,List<string> listOne ,List<string> listTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            EditHeader ( SQLString ,model );
            LineProductMesEntityu . LogisticsNewBodyOneEntity bodyOne = new LineProductMesEntityu . LogisticsNewBodyOneEntity ( );
            bodyOne . LOG001 = model . LGN001;
            foreach ( DataRow row in tableOne . Rows )
            {
                bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                bodyOne . LOG002 = row [ "LOG002" ] . ToString ( );
                bodyOne . LOG003 = row [ "LOG003" ] . ToString ( );
                bodyOne . LOG004 = row [ "LOG004" ] . ToString ( );
                bodyOne . LOG005 = row [ "LOG005" ] . ToString ( );
                bodyOne . LOG006 = string . IsNullOrEmpty ( row [ "LOG006" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LOG006" ] . ToString ( ) );
                bodyOne . LOG007 = string . IsNullOrEmpty ( row [ "LOG007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG007" ] . ToString ( ) );
                bodyOne . LOG008 = string . IsNullOrEmpty ( row [ "LOG008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LOG008" ] . ToString ( ) );
                bodyOne . LOG009 = string . IsNullOrEmpty ( row [ "LOG009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG009" ] . ToString ( ) );
                if ( bodyOne . idx < 0 )
                    AddBodyOne ( SQLString ,bodyOne );
                else
                    EditBodyOne ( SQLString ,bodyOne );
            }

            LineProductMesEntityu . LogisticsNewBodyTwoEntity bodyTwo = new LineProductMesEntityu . LogisticsNewBodyTwoEntity ( );
            bodyTwo . LGP001 = model . LGN001;
            foreach ( DataRow row in tableTwo . Rows )
            {
                bodyTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                bodyTwo . LGP002 = row [ "LGP002" ] . ToString ( );
                bodyTwo . LGP003 = row [ "LGP003" ] . ToString ( );
                bodyTwo . LGP004 = row [ "LGP004" ] . ToString ( );
                bodyTwo . LGP005 = row [ "LGP005" ] . ToString ( );
                bodyTwo . LGP006 = row [ "LGP006" ] . ToString ( );
                if ( row [ "LGP007" ] == null || row [ "LGP007" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP007 = null;
                else
                    bodyTwo . LGP007 = Convert . ToDateTime ( row [ "LGP007" ] );
                if ( row [ "LGP008" ] == null || row [ "LGP008" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP008 = null;
                else
                    bodyTwo . LGP008 = Convert . ToDateTime ( row [ "LGP008" ] );
                if ( row [ "LGP009" ] == null || row [ "LGP009" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP009 = null;
                else
                    bodyTwo . LGP009 = Convert . ToDateTime ( row [ "LGP009" ] );
                if ( row [ "LGP010" ] == null || row [ "LGP010" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP010 = null;
                else
                    bodyTwo . LGP010 = Convert . ToDateTime ( row [ "LGP010" ] );
                if ( row [ "LGP011" ] == null || row [ "LGP011" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP011 = null;
                else
                    bodyTwo . LGP011 = Convert . ToDecimal ( row [ "LGP011" ] );
                if ( row [ "LGP012" ] == null || row [ "LGP012" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP012 = null;
                else
                    bodyTwo . LGP012 = Convert . ToDecimal ( row [ "LGP012" ] );
                if ( row [ "LGP013" ] == null || row [ "LGP013" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP013 = null;
                else
                    bodyTwo . LGP013 = Convert . ToDecimal ( row [ "LGP013" ] );
                if ( row [ "LGP014" ] == null || row [ "LGP014" ] . ToString ( ) == string . Empty )
                    bodyTwo . LGP014 = null;
                else
                    bodyTwo . LGP014 = Convert . ToDecimal ( row [ "LGP014" ] );
                if ( bodyTwo . idx < 0 )
                    AddBodyTwo ( SQLString ,bodyTwo );
                else
                    EditBodyTwo ( SQLString ,bodyTwo );
            }

            foreach ( string s in listOne )
            {
                DeleteBodyOne ( SQLString ,s );
            }
            foreach ( string s in listTwo )
            {
                DeleteBodyTwo ( SQLString ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString,LineProductMesEntityu.LogisticsNewHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLGN(" );
            strSql . Append ( "LGN001,LGN002,LGN005,LGN006,LGN007,LGN008)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LGN001,@LGN002,@LGN005,@LGN006,@LGN007,@LGN008)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LGN001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGN002", SqlDbType.Date,3),
                    new SqlParameter("@LGN005", SqlDbType.Decimal,9),
                    new SqlParameter("@LGN006", SqlDbType.Decimal,9),
                    new SqlParameter("@LGN007", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGN008", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . LGN001;
            parameters [ 1 ] . Value = model . LGN002;
            parameters [ 2 ] . Value = model . LGN005;
            parameters [ 3 ] . Value = model . LGN006;
            parameters [ 4 ] . Value = model . LGN007;
            parameters [ 5 ] . Value = model . LGN008;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LogisticsNewBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLGO(" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005,LOG006,LOG007,LOG008,LOG009)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LOG001,@LOG002,@LOG003,@LOG004,@LOG005,@LOG006,@LOG007,@LOG008,@LOG009)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LOG001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LOG002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LOG003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LOG004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LOG005", SqlDbType.NVarChar,50),
                    new SqlParameter("@LOG006", SqlDbType.Int,4),
                    new SqlParameter("@LOG007", SqlDbType.Decimal,9),
                    new SqlParameter("@LOG008", SqlDbType.Int,4),
                    new SqlParameter("@LOG009", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . LOG001;
            parameters [ 1 ] . Value = model . LOG002;
            parameters [ 2 ] . Value = model . LOG003;
            parameters [ 3 ] . Value = model . LOG004;
            parameters [ 4 ] . Value = model . LOG005;
            parameters [ 5 ] . Value = model . LOG006;
            parameters [ 6 ] . Value = model . LOG007;
            parameters [ 7 ] . Value = model . LOG008;
            parameters [ 8 ] . Value = model . LOG009;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LogisticsNewBodyTwoEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLGP(" );
            strSql . Append ( "LGP001,LGP002,LGP003,LGP004,LGP005,LGP006,LGP007,LGP008,LGP009,LGP010,LGP011,LGP012,LGP013,LGP014)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LGP001,@LGP002,@LGP003,@LGP004,@LGP005,@LGP006,@LGP007,@LGP008,@LGP009,@LGP010,@LGP011,@LGP012,@LGP013,@LGP014)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LGP001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP005", SqlDbType.NVarChar,5),
                    new SqlParameter("@LGP006", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP007", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP008", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP009", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP010", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP011", SqlDbType.Decimal,9),
                    new SqlParameter("@LGP012", SqlDbType.Decimal,9),
                    new SqlParameter("@LGP013", SqlDbType.Decimal,9),
                    new SqlParameter("@LGP014", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . LGP001;
            parameters [ 1 ] . Value = model . LGP002;
            parameters [ 2 ] . Value = model . LGP003;
            parameters [ 3 ] . Value = model . LGP004;
            parameters [ 4 ] . Value = model . LGP005;
            parameters [ 5 ] . Value = model . LGP006;
            parameters [ 6 ] . Value = model . LGP007;
            parameters [ 7 ] . Value = model . LGP008;
            parameters [ 8 ] . Value = model . LGP009;
            parameters [ 9 ] . Value = model . LGP010;
            parameters [ 10 ] . Value = model . LGP011;
            parameters [ 11 ] . Value = model . LGP012;
            parameters [ 12 ] . Value = model . LGP013;
            parameters [ 13 ] . Value = model . LGP014;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LogisticsNewHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKLGN SET LGN005=@LGN005,LGN006=@LGN006,LGN007=@LGN007  WHERE LGN001=@LGN001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LGN001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGN005", SqlDbType.Decimal,9),
                    new SqlParameter("@LGN006", SqlDbType.Decimal,9),
                    new SqlParameter("@LGN007", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . LGN001;
            parameters [ 1 ] . Value = model . LGN005;
            parameters [ 2 ] . Value = model . LGN006;
            parameters [ 3 ] . Value = model . LGN007;
            SQLString . Add ( strSql ,parameters );
        }

        void EditBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LogisticsNewBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLGO set " );
            strSql . Append ( "LOG002=@LOG002," );
            strSql . Append ( "LOG003=@LOG003," );
            strSql . Append ( "LOG004=@LOG004," );
            strSql . Append ( "LOG005=@LOG005," );
            strSql . Append ( "LOG006=@LOG006," );
            strSql . Append ( "LOG007=@LOG007," );
            strSql . Append ( "LOG008=@LOG008," );
            strSql . Append ( "LOG009=@LOG009" );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LOG004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LOG005", SqlDbType.NVarChar,50),
                    new SqlParameter("@LOG006", SqlDbType.Int,4),
                    new SqlParameter("@LOG007", SqlDbType.Decimal,9),
                    new SqlParameter("@LOG008", SqlDbType.Int,4),
                    new SqlParameter("@LOG009", SqlDbType.Decimal,9),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@LOG002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LOG003", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . LOG004;
            parameters [ 1 ] . Value = model . LOG005;
            parameters [ 2 ] . Value = model . LOG006;
            parameters [ 3 ] . Value = model . LOG007;
            parameters [ 4 ] . Value = model . LOG008;
            parameters [ 5 ] . Value = model . LOG009;
            parameters [ 6 ] . Value = model . idx;
            parameters [ 7 ] . Value = model . LOG002;
            parameters [ 8 ] . Value = model . LOG003;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LogisticsNewBodyTwoEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLGP set " );
            strSql . Append ( "LGP002=@LGP002," );
            strSql . Append ( "LGP003=@LGP003," );
            strSql . Append ( "LGP004=@LGP004," );
            strSql . Append ( "LGP005=@LGP005," );
            strSql . Append ( "LGP006=@LGP006," );
            strSql . Append ( "LGP007=@LGP007," );
            strSql . Append ( "LGP008=@LGP008," );
            strSql . Append ( "LGP009=@LGP009," );
            strSql . Append ( "LGP010=@LGP010," );
            strSql . Append ( "LGP011=@LGP011," );
            strSql . Append ( "LGP012=@LGP012," );
            strSql . Append ( "LGP013=@LGP013," );
            strSql . Append ( "LGP014=@LGP014 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LGP003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP005", SqlDbType.NVarChar,5),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@LGP002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP006", SqlDbType.NVarChar,20),
                    new SqlParameter("@LGP007", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP008", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP009", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP010", SqlDbType.DateTime,3),
                    new SqlParameter("@LGP011", SqlDbType.Decimal,9),
                    new SqlParameter("@LGP012", SqlDbType.Decimal,9),
                    new SqlParameter("@LGP013", SqlDbType.Decimal,9),
                    new SqlParameter("@LGP014", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . LGP003;
            parameters [ 1 ] . Value = model . LGP004;
            parameters [ 2 ] . Value = model . LGP005;
            parameters [ 3 ] . Value = model . idx;
            parameters [ 4 ] . Value = model . LGP002;
            parameters [ 5 ] . Value = model . LGP006;
            parameters [ 6 ] . Value = model . LGP007;
            parameters [ 7 ] . Value = model . LGP008;
            parameters [ 8 ] . Value = model . LGP009;
            parameters [ 9 ] . Value = model . LGP010;
            parameters [ 10 ] . Value = model . LGP011;
            parameters [ 11 ] . Value = model . LGP012;
            parameters [ 12 ] . Value = model . LGP013;
            parameters [ 13 ] . Value = model . LGP014;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteBodyOne ( Dictionary<object ,object> SQLString , string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKLGO WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }
        void DeleteBodyTwo ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKLGP WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取查询数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableColumn ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT LGN001,LGN002,LGN003,LGN004,LOG002,LOG003,LOG004,LOG005,LOG006,LOG008 FROM MIKLGN A INNER JOIN MIKLGO B ON A.LGN001=B.LOG001 " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取查询数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LGN001,LGN002,LGN003,LGN004,LOG002,LOG003,LOG004,LOG005,LOG006,LOG008 FROM MIKLGN A INNER JOIN MIKLGO B ON A.LGN001=B.LOG001 WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LineProductMesEntityu . LogisticsNewHeaderEntity getModel ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LGN001,LGN002,LGN003,LGN004,LGN005,LGN006,LGN007 FROM MIKLGN WHERE LGN001='{0}'" ,code );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            return getModel ( table . Rows [ 0 ] );
        }

        public LineProductMesEntityu . LogisticsNewHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . LogisticsNewHeaderEntity model = new LineProductMesEntityu . LogisticsNewHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "LGN001" ] != null && row [ "LGN001" ] . ToString ( ) != string . Empty )
                    model . LGN001 = row [ "LGN001" ] . ToString ( );
                if ( row [ "LGN002" ] != null && row [ "LGN002" ] . ToString ( ) != string . Empty )
                    model . LGN002 = Convert . ToDateTime ( row [ "LGN002" ] . ToString ( ) );
                if ( row [ "LGN003" ] != null && row [ "LGN003" ] . ToString ( ) != string . Empty )
                    model . LGN003 = Convert . ToBoolean ( row [ "LGN003" ] . ToString ( ) );
                if ( row [ "LGN004" ] != null && row [ "LGN004" ] . ToString ( ) != string . Empty )
                    model . LGN004 = Convert . ToBoolean ( row [ "LGN004" ] . ToString ( ) );
                if ( row [ "LGN005" ] != null && row [ "LGN005" ] . ToString ( ) != string . Empty )
                    model . LGN005 = Convert . ToDecimal ( row [ "LGN005" ] . ToString ( ) );
                if ( row [ "LGN006" ] != null && row [ "LGN006" ] . ToString ( ) != string . Empty )
                    model . LGN006 = Convert . ToDecimal ( row [ "LGN006" ] . ToString ( ) );
                if ( row [ "LGN007" ] != null && row [ "LGN007" ] . ToString ( ) != string . Empty )
                    model . LGN007 = row [ "LGN007" ] . ToString ( );
            }
            return model;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Exanmie ( string oddNum ,bool result )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKLGN SET LGN003=@LGN003 WHERE LGN001=@LGN001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@LGN001",SqlDbType.NVarChar,20),
                new SqlParameter("@LGN003",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;
            SQLString . Add ( strSql ,parameter );

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool CancelTion ( string oddNum ,bool result )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKLGN SET LGN004=@LGN004 WHERE LGN001=@LGN001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@LGN001",SqlDbType.NVarChar,20),
                new SqlParameter("@LGN004",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( string orderNum,string proNum,string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LOG002,LOG004,LOG006-SUM(LOG008) LOG FROM MIKLGO WHERE LOG001!='{0}' AND LOG002='{1}' AND LOG004='{2}' GROUP BY LOG002,LOG004,LOG006" ,oddNum ,orderNum ,proNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
