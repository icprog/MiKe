using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data . SqlClient;
using System . Collections;

namespace LineProductMesBll . Dao
{
   public class HardWareWorkDao
    {
        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getCode ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(HAW001) HAW001 FROM MIKHAW " );

            DateTime dt = UserInfoMation . sysTime;

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return dt . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "HAW001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return dt . ToString ( "yyyyMMdd" ) + "001";
                else if ( code . Substring ( 0 ,8 ) . Equals ( dt . ToString ( "yyyyMMdd" ) ) )
                    return ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
                else
                    return dt . ToString ( "yyyyMMdd" ) + "001";
            }
        }

        /// <summary>
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0501' AND RAA020='N' AND RAA024='T'" );
            strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0501' AND RAA020='N' AND RAA024='T'" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '05%' ORDER BY DAA001" );

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
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable getUserInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1 " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取工艺信息
        /// </summary>
        /// <returns></returns>
        public DataTable getArtInfo ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ART001,ART002,ART003,CONVERT(FLOAT,ART004) ART004,ART011,ART010 FROM MIKART WHERE ART001='{0}'" ,piNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT  idx,HAX001,HAX002,HAX003,HAX004,HAX005,HAX006,HAX007,HAX008,HAX009,HAX010,HAX011,HAX012,HAX013,HAX014,HAX015,HAX016,HAX017,HAX018,HAX019,HAX020,0 U4,0.00 U1 FROM MIKHAX WHERE {0} ORDER BY HAX016" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Delete ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKHAW WHERE HAW001='{0}'" ,oddNum );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 完工数量之和是否多余工单数量  同工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ExistsNum ( LineProductMesEntityu . HardWareWorkHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT SUM(HAW009) HAW009 FROM MIKHAW WHERE HAW001!='{0}' AND HAW019=0 AND HAW002='{1}'" ,model . HAW001 ,model . HAW002 );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 0 )
                return true;
            else
            {
                int result = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "HAW009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "HAW009" ] . ToString ( ) );
                if ( result + model . HAW009 > model . HAW007 )
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 工序完工数量之和是否多于工单数量  同工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ExistsNums ( LineProductMesEntityu . HardWareWorkHeaderEntity model ,string partNum ,int num)
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT SUM(HAX008) HAX008 FROM MIKHAX A INNER JOIN MIKHAW B ON A.HAX001=B.HAW001 WHERE HAX001!='{0}' AND HAW019=0 AND HAW002='{1}' AND HAX016='{2}'" ,model . HAW001 ,model . HAW002 ,partNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 0 )
                return true;
            else
            {
                int result = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "HAX008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "HAX008" ] . ToString ( ) );
                if ( result + num > model . HAW007 )
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . HardWareWorkHeaderEntity model ,DataTable tableView  )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            model . HAW001 = getCode ( );
            UserInfoMation . oddNum = model . HAW001;
            model . HAW023 = UserInfoMation . userName;
            AddHeader ( SQLString ,strSql ,model );
            
            LineProductMesEntityu . HardWareWorkBodyEntity body = new LineProductMesEntityu . HardWareWorkBodyEntity ( );
            foreach ( DataRow row in tableView . Rows )
            {
                body . HAX001 = model . HAW001;
                body . HAX002 = row [ "HAX002" ] . ToString ( );
                body . HAX003 = row [ "HAX003" ] . ToString ( );
                body . HAX004 = row [ "HAX004" ] . ToString ( );
                body . HAX005 = row [ "HAX005" ] . ToString ( );
                body . HAX006 = row [ "HAX006" ] . ToString ( );
                body . HAX007 = string . IsNullOrEmpty ( row [ "HAX007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX007" ] . ToString ( ) );
                body . HAX008 = string . IsNullOrEmpty ( row [ "HAX008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "HAX008" ] . ToString ( ) );
                if ( row [ "HAX009" ] == null || row [ "HAX009" ] . ToString ( ) == string . Empty )
                    body . HAX009 = null;
                else
                    body . HAX009 = Convert . ToDateTime ( row [ "HAX009" ] . ToString ( ) );
                if ( row [ "HAX010" ] == null || row [ "HAX010" ] . ToString ( ) == string . Empty )
                    body . HAX010 = null;
                else
                    body . HAX010 = Convert . ToDateTime ( row [ "HAX010" ] . ToString ( ) );
                if ( row [ "HAX011" ] == null || row [ "HAX011" ] . ToString ( ) == string . Empty )
                    body . HAX011 = null;
                else
                    body . HAX011 = Convert . ToDateTime ( row [ "HAX011" ] . ToString ( ) );
                if ( row [ "HAX012" ] == null || row [ "HAX012" ] . ToString ( ) == string . Empty )
                    body . HAX012 = null;
                else
                    body . HAX012 = Convert . ToDateTime ( row [ "HAX012" ] . ToString ( ) );
                body . HAX013 = string . IsNullOrEmpty ( row [ "HAX013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX013" ] . ToString ( ) );
                body . HAX014 = row [ "HAX014" ] . ToString ( );
                body . HAX015 = row [ "HAX015" ] . ToString ( );
                body . HAX016 = row [ "HAX016" ] . ToString ( );
                body . HAX017 = row [ "HAX017" ] . ToString ( );
                body . HAX018 = string . IsNullOrEmpty ( row [ "HAX018" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX018" ] . ToString ( ) );
                body . HAX019 = string . IsNullOrEmpty ( row [ "HAX019" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX019" ] . ToString ( ) );
                body . HAX020 = string . IsNullOrEmpty ( row [ "HAX020" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX020" ] . ToString ( ) );
                AddBody ( SQLString ,strSql ,body );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . HardWareWorkHeaderEntity model ,DataTable tableView ,List<string> idxList )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            EditHeader ( SQLString ,strSql ,model );

            LineProductMesEntityu . HardWareWorkBodyEntity body = new LineProductMesEntityu . HardWareWorkBodyEntity ( );
            foreach ( DataRow row in tableView . Rows )
            {
                body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                body . HAX001 = model . HAW001;
                body . HAX002 = row [ "HAX002" ] . ToString ( );
                body . HAX003 = row [ "HAX003" ] . ToString ( );
                body . HAX004 = row [ "HAX004" ] . ToString ( );
                body . HAX005 = row [ "HAX005" ] . ToString ( );
                body . HAX006 = row [ "HAX006" ] . ToString ( );
                body . HAX007 = string . IsNullOrEmpty ( row [ "HAX007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX007" ] . ToString ( ) );
                body . HAX008 = string . IsNullOrEmpty ( row [ "HAX008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "HAX008" ] . ToString ( ) );
                if ( row [ "HAX009" ] == null || row [ "HAX009" ] . ToString ( ) == string . Empty )
                    body . HAX009 = null;
                else
                    body . HAX009 = Convert . ToDateTime ( row [ "HAX009" ] . ToString ( ) );
                if ( row [ "HAX010" ] == null || row [ "HAX010" ] . ToString ( ) == string . Empty )
                    body . HAX010 = null;
                else
                    body . HAX010 = Convert . ToDateTime ( row [ "HAX010" ] . ToString ( ) );
                if ( row [ "HAX011" ] == null || row [ "HAX011" ] . ToString ( ) == string . Empty )
                    body . HAX011 = null;
                else
                    body . HAX011 = Convert . ToDateTime ( row [ "HAX011" ] . ToString ( ) );
                if ( row [ "HAX012" ] == null || row [ "HAX012" ] . ToString ( ) == string . Empty )
                    body . HAX012 = null;
                else
                    body . HAX012 = Convert . ToDateTime ( row [ "HAX012" ] . ToString ( ) );
                body . HAX013 = string . IsNullOrEmpty ( row [ "HAX013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX013" ] . ToString ( ) );
                body . HAX014 = row [ "HAX014" ] . ToString ( );
                body . HAX015 = row [ "HAX015" ] . ToString ( );
                body . HAX016 = row [ "HAX016" ] . ToString ( );
                body . HAX017 = row [ "HAX017" ] . ToString ( );
                body . HAX018 = string . IsNullOrEmpty ( row [ "HAX018" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX018" ] . ToString ( ) );
                body . HAX019 = string . IsNullOrEmpty ( row [ "HAX019" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX019" ] . ToString ( ) );
                body . HAX020 = string . IsNullOrEmpty ( row [ "HAX020" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX020" ] . ToString ( ) );
                if ( body . idx < 1 )
                    AddBody ( SQLString ,strSql ,body );
                else
                    EditBody ( SQLString ,strSql ,body );
            }

            foreach ( string s in idxList )
            {
                DeleteBody ( SQLString ,strSql ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . HardWareWorkHeaderEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKHAW(" );
            strSql . Append ( "HAW001,HAW002,HAW003,HAW004,HAW005,HAW006,HAW007,HAW008,HAW009,HAW010,HAW011,HAW012,HAW013,HAW014,HAW015,HAW016,HAW017,HAW018,HAW019,HAW020,HAW021,HAW023)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@HAW001,@HAW002,@HAW003,@HAW004,@HAW005,@HAW006,@HAW007,@HAW008,@HAW009,@HAW010,@HAW011,@HAW012,@HAW013,@HAW014,@HAW015,@HAW016,@HAW017,@HAW018,@HAW019,@HAW020,@HAW021,@HAW023)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@HAW001", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW002", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW003", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW004", SqlDbType.NVarChar,50),
                    new SqlParameter("@HAW005", SqlDbType.NVarChar,50),
                    new SqlParameter("@HAW006", SqlDbType.NVarChar,5),
                    new SqlParameter("@HAW007", SqlDbType.Int,4),
                    new SqlParameter("@HAW008", SqlDbType.Decimal,9),
                    new SqlParameter("@HAW009", SqlDbType.Int,4),
                    new SqlParameter("@HAW010", SqlDbType.Date,3),
                    new SqlParameter("@HAW011", SqlDbType.NVarChar,5),
                    new SqlParameter("@HAW012", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW013", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW014", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW015", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW016", SqlDbType.NVarChar,200),
                    new SqlParameter("@HAW017", SqlDbType.NVarChar,200),
                    new SqlParameter("@HAW018", SqlDbType.Bit,1),
                    new SqlParameter("@HAW019", SqlDbType.Bit,1),
                    new SqlParameter("@HAW020", SqlDbType.Decimal),
                    new SqlParameter("@HAW021", SqlDbType.Decimal),
                    new SqlParameter("@HAW023", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . HAW001;
            parameters [ 1 ] . Value = model . HAW002;
            parameters [ 2 ] . Value = model . HAW003;
            parameters [ 3 ] . Value = model . HAW004;
            parameters [ 4 ] . Value = model . HAW005;
            parameters [ 5 ] . Value = model . HAW006;
            parameters [ 6 ] . Value = model . HAW007;
            parameters [ 7 ] . Value = model . HAW008;
            parameters [ 8 ] . Value = model . HAW009;
            parameters [ 9 ] . Value = model . HAW010;
            parameters [ 10 ] . Value = model . HAW011;
            parameters [ 11 ] . Value = model . HAW012;
            parameters [ 12 ] . Value = model . HAW013;
            parameters [ 13 ] . Value = model . HAW014;
            parameters [ 14 ] . Value = model . HAW015;
            parameters [ 15 ] . Value = model . HAW016;
            parameters [ 16 ] . Value = model . HAW017;
            parameters [ 17 ] . Value = model . HAW018;
            parameters [ 18 ] . Value = model . HAW019;
            parameters [ 19 ] . Value = model . HAW020;
            parameters [ 20 ] . Value = model . HAW021;
            parameters [ 21 ] . Value = model . HAW023;

            SQLString . Add ( strSql ,parameters );
        }
        void AddBody ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . HardWareWorkBodyEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKHAX(" );
            strSql . Append ( "HAX001,HAX002,HAX003,HAX004,HAX005,HAX006,HAX007,HAX008,HAX009,HAX010,HAX011,HAX012,HAX013,HAX014,HAX015,HAX016,HAX017,HAX018,HAX019,HAX020)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@HAX001,@HAX002,@HAX003,@HAX004,@HAX005,@HAX006,@HAX007,@HAX008,@HAX009,@HAX010,@HAX011,@HAX012,@HAX013,@HAX014,@HAX015,@HAX016,@HAX017,@HAX018,@HAX019,@HAX020)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@HAX001", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX002", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX003", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX004", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX005", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX006", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX007", SqlDbType.Decimal,9),
                    new SqlParameter("@HAX008", SqlDbType.Int,4),
                    new SqlParameter("@HAX009", SqlDbType.DateTime),
                    new SqlParameter("@HAX010", SqlDbType.DateTime),
                    new SqlParameter("@HAX011", SqlDbType.DateTime),
                    new SqlParameter("@HAX012", SqlDbType.DateTime),
                    new SqlParameter("@HAX013", SqlDbType.Decimal,9),
                    new SqlParameter("@HAX014", SqlDbType.NVarChar,200),
                    new SqlParameter("@HAX015", SqlDbType.NVarChar,10),
                    new SqlParameter("@HAX016", SqlDbType.NVarChar,10),
                    new SqlParameter("@HAX017", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX018", SqlDbType.Decimal),
                    new SqlParameter("@HAX019", SqlDbType.Decimal),
                    new SqlParameter("@HAX020", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . HAX001;
            parameters [ 1 ] . Value = model . HAX002;
            parameters [ 2 ] . Value = model . HAX003;
            parameters [ 3 ] . Value = model . HAX004;
            parameters [ 4 ] . Value = model . HAX005;
            parameters [ 5 ] . Value = model . HAX006;
            parameters [ 6 ] . Value = model . HAX007;
            parameters [ 7 ] . Value = model . HAX008;
            parameters [ 8 ] . Value = model . HAX009;
            parameters [ 9 ] . Value = model . HAX010;
            parameters [ 10 ] . Value = model . HAX011;
            parameters [ 11 ] . Value = model . HAX012;
            parameters [ 12 ] . Value = model . HAX013;
            parameters [ 13 ] . Value = model . HAX014;
            parameters [ 14 ] . Value = model . HAX015;
            parameters [ 15 ] . Value = model . HAX016;
            parameters [ 16 ] . Value = model . HAX017;
            parameters [ 17 ] . Value = model . HAX018;
            parameters [ 18 ] . Value = model . HAX019;
            parameters [ 19 ] . Value = model . HAX020;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . HardWareWorkHeaderEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKHAW set " );
            strSql . Append ( "HAW002=@HAW002," );
            strSql . Append ( "HAW003=@HAW003," );
            strSql . Append ( "HAW004=@HAW004," );
            strSql . Append ( "HAW005=@HAW005," );
            strSql . Append ( "HAW006=@HAW006," );
            strSql . Append ( "HAW007=@HAW007," );
            strSql . Append ( "HAW008=@HAW008," );
            strSql . Append ( "HAW009=@HAW009," );
            strSql . Append ( "HAW010=@HAW010," );
            strSql . Append ( "HAW011=@HAW011," );
            strSql . Append ( "HAW012=@HAW012," );
            strSql . Append ( "HAW013=@HAW013," );
            strSql . Append ( "HAW014=@HAW014," );
            strSql . Append ( "HAW015=@HAW015," );
            strSql . Append ( "HAW016=@HAW016," );
            strSql . Append ( "HAW017=@HAW017," );
            strSql . Append ( "HAW018=@HAW018," );
            strSql . Append ( "HAW019=@HAW019," );
            strSql . Append ( "HAW020=@HAW020," );
            strSql . Append ( "HAW021=@HAW021 " );
            strSql . Append ( " where HAW001=@HAW001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@HAW002", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW003", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW004", SqlDbType.NVarChar,50),
                    new SqlParameter("@HAW005", SqlDbType.NVarChar,50),
                    new SqlParameter("@HAW006", SqlDbType.NVarChar,5),
                    new SqlParameter("@HAW007", SqlDbType.Int,4),
                    new SqlParameter("@HAW008", SqlDbType.Decimal,9),
                    new SqlParameter("@HAW009", SqlDbType.Int,4),
                    new SqlParameter("@HAW010", SqlDbType.Date,3),
                    new SqlParameter("@HAW011", SqlDbType.NVarChar,5),
                    new SqlParameter("@HAW012", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW013", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW014", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW015", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW016", SqlDbType.NVarChar,200),
                    new SqlParameter("@HAW017", SqlDbType.NVarChar,200),
                    new SqlParameter("@HAW018", SqlDbType.Bit,1),
                    new SqlParameter("@HAW019", SqlDbType.Bit,1),
                    new SqlParameter("@HAW001", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAW020", SqlDbType.Decimal),
                    new SqlParameter("@HAW021", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . HAW002;
            parameters [ 1 ] . Value = model . HAW003;
            parameters [ 2 ] . Value = model . HAW004;
            parameters [ 3 ] . Value = model . HAW005;
            parameters [ 4 ] . Value = model . HAW006;
            parameters [ 5 ] . Value = model . HAW007;
            parameters [ 6 ] . Value = model . HAW008;
            parameters [ 7 ] . Value = model . HAW009;
            parameters [ 8 ] . Value = model . HAW010;
            parameters [ 9 ] . Value = model . HAW011;
            parameters [ 10 ] . Value = model . HAW012;
            parameters [ 11 ] . Value = model . HAW013;
            parameters [ 12 ] . Value = model . HAW014;
            parameters [ 13 ] . Value = model . HAW015;
            parameters [ 14 ] . Value = model . HAW016;
            parameters [ 15 ] . Value = model . HAW017;
            parameters [ 16 ] . Value = model . HAW018;
            parameters [ 17 ] . Value = model . HAW019;
            parameters [ 18 ] . Value = model . HAW001;
            parameters [ 19 ] . Value = model . HAW020;
            parameters [ 20 ] . Value = model . HAW021;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBody ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . HardWareWorkBodyEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKHAX set " );
            strSql . Append ( "HAX003=@HAX003," );
            strSql . Append ( "HAX004=@HAX004," );
            strSql . Append ( "HAX005=@HAX005," );
            strSql . Append ( "HAX006=@HAX006," );
            strSql . Append ( "HAX007=@HAX007," );
            strSql . Append ( "HAX008=@HAX008," );
            strSql . Append ( "HAX009=@HAX009," );
            strSql . Append ( "HAX010=@HAX010," );
            strSql . Append ( "HAX011=@HAX011," );
            strSql . Append ( "HAX012=@HAX012," );
            strSql . Append ( "HAX013=@HAX013," );
            strSql . Append ( "HAX014=@HAX014," );
            strSql . Append ( "HAX015=@HAX015," );
            strSql . Append ( "HAX016=@HAX016," );
            strSql . Append ( "HAX017=@HAX017," );
            strSql . Append ( "HAX018=@HAX018," );
            strSql . Append ( "HAX019=@HAX019," );
            strSql . Append ( "HAX020=@HAX020 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@HAX003", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX004", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX005", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX006", SqlDbType.NVarChar,20),
                    new SqlParameter("@HAX007", SqlDbType.Decimal,9),
                    new SqlParameter("@HAX008", SqlDbType.Int,4),
                    new SqlParameter("@HAX009", SqlDbType.DateTime),
                    new SqlParameter("@HAX010", SqlDbType.DateTime),
                    new SqlParameter("@HAX011", SqlDbType.DateTime),
                    new SqlParameter("@HAX012", SqlDbType.DateTime),
                    new SqlParameter("@HAX013", SqlDbType.Decimal,9),
                    new SqlParameter("@HAX014", SqlDbType.NVarChar,200),
                    new SqlParameter("@HAX015", SqlDbType.NVarChar,10),
                    new SqlParameter("@HAX016", SqlDbType.NVarChar,10),
                    new SqlParameter("@HAX017", SqlDbType.NVarChar,20),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@HAX018", SqlDbType.Decimal,9),
                    new SqlParameter("@HAX019", SqlDbType.Decimal,9),
                    new SqlParameter("@HAX020", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . HAX003;
            parameters [ 1 ] . Value = model . HAX004;
            parameters [ 2 ] . Value = model . HAX005;
            parameters [ 3 ] . Value = model . HAX006;
            parameters [ 4 ] . Value = model . HAX007;
            parameters [ 5 ] . Value = model . HAX008;
            parameters [ 6 ] . Value = model . HAX009;
            parameters [ 7 ] . Value = model . HAX010;
            parameters [ 8 ] . Value = model . HAX011;
            parameters [ 9 ] . Value = model . HAX012;
            parameters [ 10 ] . Value = model . HAX013;
            parameters [ 11 ] . Value = model . HAX014;
            parameters [ 12 ] . Value = model . HAX015;
            parameters [ 13 ] . Value = model . HAX016;
            parameters [ 14 ] . Value = model . HAX017;
            parameters [ 15 ] . Value = model . idx;
            parameters [ 16 ] . Value = model . HAX018;
            parameters [ 17 ] . Value = model . HAX019;
            parameters [ 18 ] . Value = model . HAX020;
            SQLString . Add ( strSql ,parameters );
        }
        void DeleteBody ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,string s )
        {
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKHAX WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool Exanmie ( string oddNum ,bool state,int numbers ,LineProductMesEntityu . HardWareWorkHeaderEntity _header )
        {
            Dictionary<Object ,Object> SQLString = new Dictionary<Object ,Object> ( );

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKHAW SET HAW018=@HAW018 WHERE HAW001=@HAW001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@HAW001",SqlDbType.NVarChar,20),
                new SqlParameter("@HAW018",SqlDbType.Bit,1)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = state;
            SQLString . Add ( strSql ,parameter );

            if ( state )
            {
                if ( string . IsNullOrEmpty ( _header . HAW022 ) )
                    addSGM ( SQLString ,numbers ,_header );
            }
            else
            {
                if ( !string . IsNullOrEmpty ( _header . HAW022 ) )
                {
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "DELETE FROM SGMRBA WHERE RBA002='{0}'" ,_header . HAW022 );
                    SQLString . Add ( strSql ,null );
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "UPDATE MIKHAW SET HAW022=NULL WHERE HAW001='{0}'" ,_header . HAW001 );
                    SQLString . Add ( strSql ,null );
                    UserInfoMation . oddForSGMRBA = null;
                    //SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
                    //addSGM ( SQLString ,numbers ,_header );
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 领料单是否已经审核
        /// </summary>
        /// <param name="oddNumForSGM"></param>
        /// <returns></returns>
        public bool boolExamineSGM ( string oddNumForSGM )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM SGMRBA WHERE RBA002='{0}' AND RBA012='T'" ,oddNumForSGM );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 生成领料单
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="numbers"></param>
        /// <param name="model"></param>
        void addSGM ( Dictionary<Object ,Object> SQLString ,int numbers ,LineProductMesEntityu . HardWareWorkHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT RAB003 RBB004,RAB004 RBB005,DEA003 RBB006,CONVERT(FLOAT,RAB007/RAA018) RAB007,RAB001 RBB010,RAB002 RBB011,DEA057,DEA008 FROM SGMRAA A INNER JOIN SGMRAB B ON A.RAA001=B.RAB001 INNER JOIN TPADEA C ON B.RAB003=C.DEA001 WHERE DEA984=1 AND RAA001='{0}' " ,model . HAW002 );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return;

            DateTime dt = ObtainInfo . getTime ( );

            LineProductMesEntityu . SRMRBAEntity rba = new LineProductMesEntityu . SRMRBAEntity ( );
            rba . RBA001 = "82";
            rba . RBA002 = getOddForRBA ( dt );
            rba . RBA003 = string . Empty;
            rba . RBA004 = dt . ToString ( "yyyyMMdd" );
            rba . RBA005 = "DS";
            rba . RBA006 = model . HAW002;
            rba . RBA012 = "F";
            rba . RBA022 = "F";

            addRBA ( SQLString ,rba );

            UserInfoMation . oddForSGMRBA = rba . RBA002;

            LineProductMesEntityu . SGMRBBEntity rbb = new LineProductMesEntityu . SGMRBBEntity ( );
            int i = 0;
            foreach ( DataRow row in table . Rows )
            {
                i++;
                rbb . RBB001 = "82";
                rbb . RBB002 = rba . RBA002;
                rbb . RBB003 = i . ToString ( ) . PadLeft ( 3 ,'0' );
                rbb . RBB004 = row [ "RBB004" ] . ToString ( );
                rbb . RBB005 = row [ "RBB005" ] . ToString ( );
                rbb . RBB006 = row [ "RBB006" ] . ToString ( );
                rbb . RBB007 = row [ "DEA008" ] . ToString ( );
                rbb . RBB008 = "XC15";
                rbb . RBB009 = numbers * ( string . IsNullOrEmpty ( row [ "RAB007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "RAB007" ] ) );
                rbb . RBB010 = row [ "RBB010" ] . ToString ( );
                rbb . RBB011 = row [ "RBB011" ] . ToString ( );
                rbb . RBB013 = "F";
                rbb . RBB021 = "********************";
                rbb . RBB022 = row [ "DEA057" ] . ToString ( );
                rbb . RBB023 = "81";
                rbb . RBB012 = "倒扣料:" + model . HAW001;
                addRBB ( SQLString ,rbb );
            }

            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKHAW SET HAW022='{0}' WHERE HAW001='{1}'" ,rbb . RBB002 ,model . HAW001 );
            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取领料单单号
        /// </summary>
        /// <returns></returns>
        string getOddForRBA ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(RBA002) RBA002 FROM SGMRBA WHERE RBA002 LIKE 'LL'+CONVERT(NVARCHAR,GETDATE(),112)+'%'" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            if ( table == null && table . Rows . Count < 1 )
                return "LL" + dt . ToString ( "yyyyMMdd" ) + "0001";
            else
            {
                string code = table . Rows [ 0 ] [ "RBA002" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return "LL" + dt . ToString ( "yyyyMMdd" ) + "0001";
                else
                    return "LL" + ( Convert . ToInt64 ( code . Substring ( 2 ,12 ) ) + 1 ) . ToString ( );
            }
        }

        /// <summary>
        /// 领料单单头新增
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="model"></param>
        void addRBA ( Dictionary<Object ,Object> SQLString ,LineProductMesEntityu . SRMRBAEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO SGMRBA (" );
            strSql . Append ( "RBA001,RBA002,RBA003,RBA004,RBA005,RBA006,RBA012,RBA022)" );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@RBA001,@RBA002,@RBA003,@RBA004,@RBA005,@RBA006,@RBA012,@RBA022)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@RBA001", SqlDbType.VarChar,2),
                    new SqlParameter("@RBA002", SqlDbType.VarChar,14),
                    new SqlParameter("@RBA003", SqlDbType.VarChar,4),
                    new SqlParameter("@RBA004", SqlDbType.VarChar,8),
                    new SqlParameter("@RBA005", SqlDbType.VarChar,8),
                    new SqlParameter("@RBA006", SqlDbType.VarChar,14),
                    new SqlParameter("@RBA012", SqlDbType.VarChar,1),
                    new SqlParameter("@RBA022", SqlDbType.VarChar,1)
            };
            parameters [ 0 ] . Value = model . RBA001;
            parameters [ 1 ] . Value = model . RBA002;
            parameters [ 2 ] . Value = model . RBA003;
            parameters [ 3 ] . Value = model . RBA004;
            parameters [ 4 ] . Value = model . RBA005;
            parameters [ 5 ] . Value = model . RBA006;
            parameters [ 6 ] . Value = model . RBA012;
            parameters [ 7 ] . Value = model . RBA022;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 领料单单身新增
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="model"></param>
        void addRBB ( Dictionary<Object ,Object> SQLString ,LineProductMesEntityu . SGMRBBEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into SGMRBB(" );
            strSql . Append ( "RBB001,RBB002,RBB003,RBB004,RBB005,RBB006,RBB007,RBB008,RBB009,RBB010,RBB011,RBB013,RBB021,RBB022,RBB023,RBB012)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@RBB001,@RBB002,@RBB003,@RBB004,@RBB005,@RBB006,@RBB007,@RBB008,@RBB009,@RBB010,@RBB011,@RBB013,@RBB021,@RBB022,@RBB023,@RBB012)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@RBB001", SqlDbType.VarChar,2),
                    new SqlParameter("@RBB002", SqlDbType.VarChar,14),
                    new SqlParameter("@RBB003", SqlDbType.VarChar,3),
                    new SqlParameter("@RBB004", SqlDbType.VarChar,20),
                    new SqlParameter("@RBB005", SqlDbType.VarChar,60),
                    new SqlParameter("@RBB006", SqlDbType.VarChar,4),
                    new SqlParameter("@RBB007", SqlDbType.VarChar,6),
                    new SqlParameter("@RBB008", SqlDbType.VarChar,6),
                    new SqlParameter("@RBB009", SqlDbType.Decimal,9),
                    new SqlParameter("@RBB010", SqlDbType.VarChar,14),
                    new SqlParameter("@RBB011", SqlDbType.VarChar,3),
                    new SqlParameter("@RBB013", SqlDbType.VarChar,1),
                    new SqlParameter("@RBB021", SqlDbType.VarChar,20),
                    new SqlParameter("@RBB022", SqlDbType.VarChar,60),
                    new SqlParameter("@RBB023", SqlDbType.VarChar,20),
                    new SqlParameter("@RBB012", SqlDbType.VarChar,255)
            };
            parameters [ 0 ] . Value = model . RBB001;
            parameters [ 1 ] . Value = model . RBB002;
            parameters [ 2 ] . Value = model . RBB003;
            parameters [ 3 ] . Value = model . RBB004;
            parameters [ 4 ] . Value = model . RBB005;
            parameters [ 5 ] . Value = model . RBB006;
            parameters [ 6 ] . Value = model . RBB007;
            parameters [ 7 ] . Value = model . RBB008;
            parameters [ 8 ] . Value = model . RBB009;
            parameters [ 9 ] . Value = model . RBB010;
            parameters [ 10 ] . Value = model . RBB011;
            parameters [ 11 ] . Value = model . RBB013;
            parameters [ 12 ] . Value = model . RBB021;
            parameters [ 13 ] . Value = model . RBB022;
            parameters [ 14 ] . Value = model . RBB023;
            parameters [ 15 ] . Value = model . RBB012;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool CancelTion ( string oddNum ,bool state )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKHAW SET HAW019=@HAW019 WHERE HAW001=@HAW001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@HAW001",SqlDbType.NVarChar,20),
                new SqlParameter("@HAW019",SqlDbType.Bit,1)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = state;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取数据列
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableColumn ( string strWhere  )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . AppendFormat ( "SELECT HAW001,HAW002,HAW003,HAW004,HAW007,HAW010,HAW018,HAW019,(SELECT SUM(HAX008) FROM MIKHAX A INNER JOIN (SELECT HAX001,MAX(HAX016) HAX016 FROM MIKHAX WHERE HAX001=HAW001 GROUP BY HAX001) B ON A.HAX001=B.HAX001 AND A.HAX016=B.HAX016) HAX008 FROM MIKHAW WHERE  {0}" ,strWhere );
            strSql . AppendFormat ( "SELECT HAW001,HAW002,HAW003,HAW004,HAW007,HAW010,HAW018,HAW019,HAW009 FROM MIKHAW WHERE  {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . HardWareWorkHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "select idx,HAW001,HAW002,HAW003,HAW004,HAW005,HAW006,HAW007,HAW008,HAW009,HAW010,HAW011,HAW012,HAW013,HAW014,HAW015,HAW016,HAW017,HAW018,HAW019,HAW020,HAW021,HAW022 FROM MIKHAW " );
            strSql . Append ( " where HAW001=@HAW001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@HAW001", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = oddNum;

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) ,parameters );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
                return getModel ( table . Rows [ 0 ] );
        }

        public LineProductMesEntityu . HardWareWorkHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . HardWareWorkHeaderEntity model = new LineProductMesEntityu . HardWareWorkHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "idx" ] != null && row [ "idx" ] . ToString ( ) != "" )
                {
                    model . idx = int . Parse ( row [ "idx" ] . ToString ( ) );
                }
                if ( row [ "HAW001" ] != null )
                {
                    model . HAW001 = row [ "HAW001" ] . ToString ( );
                }
                if ( row [ "HAW002" ] != null )
                {
                    model . HAW002 = row [ "HAW002" ] . ToString ( );
                }
                if ( row [ "HAW003" ] != null )
                {
                    model . HAW003 = row [ "HAW003" ] . ToString ( );
                }
                if ( row [ "HAW004" ] != null )
                {
                    model . HAW004 = row [ "HAW004" ] . ToString ( );
                }
                if ( row [ "HAW005" ] != null )
                {
                    model . HAW005 = row [ "HAW005" ] . ToString ( );
                }
                if ( row [ "HAW006" ] != null )
                {
                    model . HAW006 = row [ "HAW006" ] . ToString ( );
                }
                if ( row [ "HAW007" ] != null && row [ "HAW007" ] . ToString ( ) != "" )
                {
                    model . HAW007 = int . Parse ( row [ "HAW007" ] . ToString ( ) );
                }
                if ( row [ "HAW008" ] != null && row [ "HAW008" ] . ToString ( ) != "" )
                {
                    model . HAW008 = decimal . Parse ( row [ "HAW008" ] . ToString ( ) );
                }
                if ( row [ "HAW009" ] != null && row [ "HAW009" ] . ToString ( ) != "" )
                {
                    model . HAW009 = int . Parse ( row [ "HAW009" ] . ToString ( ) );
                }
                if ( row [ "HAW010" ] != null && row [ "HAW010" ] . ToString ( ) != "" )
                {
                    model . HAW010 = DateTime . Parse ( row [ "HAW010" ] . ToString ( ) );
                }
                if ( row [ "HAW011" ] != null )
                {
                    model . HAW011 = row [ "HAW011" ] . ToString ( );
                }
                if ( row [ "HAW012" ] != null )
                {
                    model . HAW012 = row [ "HAW012" ] . ToString ( );
                }
                if ( row [ "HAW013" ] != null )
                {
                    model . HAW013 = row [ "HAW013" ] . ToString ( );
                }
                if ( row [ "HAW014" ] != null )
                {
                    model . HAW014 = row [ "HAW014" ] . ToString ( );
                }
                if ( row [ "HAW015" ] != null )
                {
                    model . HAW015 = row [ "HAW015" ] . ToString ( );
                }
                if ( row [ "HAW016" ] != null )
                {
                    model . HAW016 = row [ "HAW016" ] . ToString ( );
                }
                if ( row [ "HAW017" ] != null )
                {
                    model . HAW017 = row [ "HAW017" ] . ToString ( );
                }
                if ( row [ "HAW018" ] != null && row [ "HAW018" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "HAW018" ] . ToString ( ) == "1" ) || ( row [ "HAW018" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . HAW018 = true;
                    }
                    else
                    {
                        model . HAW018 = false;
                    }
                }
                if ( row [ "HAW019" ] != null && row [ "HAW019" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "HAW019" ] . ToString ( ) == "1" ) || ( row [ "HAW019" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . HAW019 = true;
                    }
                    else
                    {
                        model . HAW019 = false;
                    }
                }
                if ( row [ "HAW020" ] != null && row [ "HAW020" ] . ToString ( ) != "" )
                {
                    model . HAW020 = decimal . Parse ( row [ "HAW020" ] . ToString ( ) );
                }
                if ( row [ "HAW021" ] != null && row [ "HAW021" ] . ToString ( ) != "" )
                {
                    model . HAW021 = decimal . Parse ( row [ "HAW021" ] . ToString ( ) );
                }
                if ( row [ "HAW022" ] != null )
                {
                    model . HAW022 = row [ "HAW022" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT HAW001 ANW001,HAW013 ANW011,HAW015 ANW013,HAW010 ANW022,GETDATE() dat,HAW023 ANW025 FROM MIKHAW WHERE HAW001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT HAW002 ANW002,HAW003 ANW003,HAW004 ANW004,HAW005 ANW005,HAW006 ANW007,HAW007 ANW006,HAW009 ANW009,DDA003 DEA008 FROM MIKHAX A INNER JOIN MIKHAW B ON A.HAX001=B.HAW001 LEFT JOIN TPADEA C ON B.HAW003=C.DEA001 INNER JOIN MIKART D ON B.HAW003=D.ART001 AND A.HAX016=D.ART011 INNER JOIN TPADDA E ON C.DEA008=E.DDA001 WHERE ART010='是'AND HAX008 > 0 AND HAW001 ='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取剩余数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( string orderNum,string proNum,string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT HAX016,HAW007-SUM(HAX008) HAX008,SUM(HAX008) HAX FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 WHERE HAW002='{0}' AND HAW003='{1}' AND HAW001!='{2}' GROUP BY HAX016,HAW007" ,orderNum ,proNum ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取同来源单号，不同单号的工序序号和数量
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="codeNum"></param>
        /// <returns></returns>
        public DataTable getTableALLArt ( string oddNum ,string codeNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT HAX016,SUM(HAX008) HAX008 FROM MIKHAX A INNER JOIN MIKHAW B ON A.HAX001=B.HAW001 WHERE HAW002='{0}' AND HAX001!='{1}'GROUP BY HAX016 ORDER BY HAX016" ,codeNum ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印数据  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "select HAW001,HAW002,HAW003,HAW004,HAW005,HAW006,HAW007,convert(float,HAW008) HAW008,HAW009,HAW010,HAW011,HAW013,HAW015,HAW016,HAW017,convert(float,HAW020) HAW020,CONVERT(FLOAT,HAW021) HAW021,CONVERT(FLOAT,HAW008*HAW009) U0 FROM MIKHAW WHERE HAW001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印数据  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFor ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT HAX002,HAX003,HAX004,HAX005,HAX006,CONVERT(FLOAT,HAX007) HAX007,HAX008,Datename(HOUR,HAX009)+':'+DATENAME(MINUTE,HAX009) HAX009,Datename(HOUR,HAX010)+':'+DATENAME(MINUTE,HAX010) HAX010,Datename(HOUR,HAX011)+':'+DATENAME(MINUTE,HAX011) HAX011,Datename(HOUR,HAX012)+':'+DATENAME(MINUTE,HAX012) HAX012,CONVERT(FLOAT,HAX013) HAX013,HAX014,HAX015,HAX016,HAX017,CONVERT(FLOAT,HAX018) HAX018,CONVERT(FLOAT,HAX019) HAX019,CONVERT(FLOAT,HAX020) HAX020,CONVERT(FLOAT,HAX013*HAX019) U3,CONVERT(FLOAT,CASE HAW011 WHEN '计件' THEN HAX007*HAX008 WHEN '计时' THEN 0 ELSE 0 END) U1 FROM MIKHAX A INNER JOIN MIKHAW B ON A.HAX001=B.HAW001 WHERE HAX001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
