using System;
using System . Collections . Generic;
using System . Text;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class LEDNewsDao
    {
        /// <summary>
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0506' AND RAA020='N' AND RAA024='T'" );
            strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0506' AND RAA020='N' AND RAA024='T'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
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
        /// <param name="workId">车间编号</param>
        /// <param name="gruopId">班组编号</param>
        /// <returns></returns>
        public DataTable getUserInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1 " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取岗位
        /// </summary>
        /// <returns></returns>
        public DataTable getUserLocal ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT EMP007 FROM MIKEMP WHERE EMP007 IS NOT NULL AND EMP007!=''" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(LEF001) LEF001 FROM MIKLEF" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 0 )
                return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "LEF001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
                else
                {
                    if ( code . Substring ( 0 ,8 ) . Equals ( UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) ) )
                        return ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
                    else
                        return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
                }
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,LEG001,LEG002,LEG003,LEG004,LEG005,LEG006,LEG007,LEG008,LEG009,LEG010,LEG011,LEG012,LEG016,LEG013,LEG014,LEG015 FROM MIKLEG WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewOne ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT 1 idx,LEH001,LEH002,LEH003,LEH004,LEH005,LEH006,LEH007,LEH008,LEH009,0 U4 from MIKLEH  WHERE {0}" ,strWhere );

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
            strSql . AppendFormat ( "DELETE FROM MIKLEF WHERE LEF001='{0}'" ,oddNum );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
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
            strSql . Append ( "UPDATE MIKLEF SET LEF017=@LEF017 WHERE LEF001=@LEF001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@LEF001",SqlDbType.NVarChar,20),
                new SqlParameter("@LEF017",SqlDbType.Bit)
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
            strSql . Append ( "UPDATE MIKLEF SET LEF018=@LEF018 WHERE LEF001=@LEF001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@LEF001",SqlDbType.NVarChar,20),
                new SqlParameter("@LEF018",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu.LEDNewsHeaderEntity model,DataTable tableView ,DataTable tableViewTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            model . LEF001 = getOddNum ( );
            UserInfoMation . oddNum = model . LEF001;
            AddHeader ( SQLString ,model );

            LineProductMesEntityu . LEDNewsBodyEntity modelBody = new LineProductMesEntityu . LEDNewsBodyEntity ( );
            modelBody . LEG001 = model . LEF001;
            foreach ( DataRow row in tableView . Rows )
            {
                modelBody . LEG002 = row [ "LEG002" ] . ToString ( );
                modelBody . LEG003 = row [ "LEG003" ] . ToString ( );
                modelBody . LEG004 = row [ "LEG004" ] . ToString ( );
                if ( row [ "LEG005" ] == null || row [ "LEG005" ] . ToString ( ) == string . Empty )
                    modelBody . LEG005 = null;
                else
                    modelBody . LEG005 = Convert . ToDateTime ( row [ "LEG005" ] . ToString ( ) );
                if ( row [ "LEG006" ] == null || row [ "LEG006" ] . ToString ( ) == string . Empty )
                    modelBody . LEG006 = null;
                else
                    modelBody . LEG006 = Convert . ToDateTime ( row [ "LEG006" ] . ToString ( ) );
                modelBody . LEG007 = string . IsNullOrEmpty ( row [ "LEG007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG007" ] . ToString ( ) );
                if ( row [ "LEG008" ] == null || row [ "LEG008" ] . ToString ( ) == string . Empty )
                    modelBody . LEG008 = null;
                else
                    modelBody . LEG008 = Convert . ToDateTime ( row [ "LEG008" ] . ToString ( ) );
                if ( row [ "LEG009" ] == null || row [ "LEG009" ] . ToString ( ) == string . Empty )
                    modelBody . LEG009 = null;
                else
                    modelBody . LEG009 = Convert . ToDateTime ( row [ "LEG009" ] . ToString ( ) );
                modelBody . LEG010 = string . IsNullOrEmpty ( row [ "LEG010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG010" ] . ToString ( ) );
                modelBody . LEG011 = row [ "LEG011" ] . ToString ( );
                modelBody . LEG012 = row [ "LEG012" ] . ToString ( );
                modelBody . LEG013 = row [ "LEG013" ] . ToString ( );
                modelBody . LEG014 = string . IsNullOrEmpty ( row [ "LEG014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG014" ] . ToString ( ) );
                modelBody . LEG015 = string . IsNullOrEmpty ( row [ "LEG015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG015" ] . ToString ( ) );
                modelBody . LEG016 = string . IsNullOrEmpty ( row [ "LEG016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG016" ] . ToString ( ) );
                AddBody ( SQLString ,modelBody );
            }

            LineProductMesEntityu . LEHNewsBodyEntity body = new LineProductMesEntityu . LEHNewsBodyEntity ( );
            body . LEH001 = model . LEF001;
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                body . LEH002 = row [ "LEH002" ] . ToString ( );
                body . LEH003 = row [ "LEH003" ] . ToString ( );
                body . LEH004 = row [ "LEH004" ] . ToString ( );
                body . LEH005 = row [ "LEH005" ] . ToString ( );
                body . LEH006 = row [ "LEH006" ] . ToString ( );
                body . LEH007 = string . IsNullOrEmpty ( row [ "LEH007" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEH007" ] . ToString ( ) );
                body . LEH008 = string . IsNullOrEmpty ( row [ "LEH008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEH008" ] . ToString ( ) );
                body . LEH009 = string . IsNullOrEmpty ( row [ "LEH009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEH009" ] . ToString ( ) );
                AddBodyOne ( SQLString ,body );
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
        public bool Edit ( LineProductMesEntityu . LEDNewsHeaderEntity model ,DataTable tableView ,DataTable tableViewTwo ,List<string> idxList ,List<string> idxListOne )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            EditHeader ( SQLString ,model );

            LineProductMesEntityu . LEDNewsBodyEntity modelBody = new LineProductMesEntityu . LEDNewsBodyEntity ( );
            modelBody . LEG001 = model . LEF001;
            foreach ( DataRow row in tableView . Rows )
            {
                modelBody . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                modelBody . LEG002 = row [ "LEG002" ] . ToString ( );
                modelBody . LEG003 = row [ "LEG003" ] . ToString ( );
                modelBody . LEG004 = row [ "LEG004" ] . ToString ( );
                if ( row [ "LEG005" ] == null || row [ "LEG005" ] . ToString ( ) == string . Empty )
                    modelBody . LEG005 = null;
                else
                    modelBody . LEG005 = Convert . ToDateTime ( row [ "LEG005" ] . ToString ( ) );
                if ( row [ "LEG006" ] == null || row [ "LEG006" ] . ToString ( ) == string . Empty )
                    modelBody . LEG006 = null;
                else
                    modelBody . LEG006 = Convert . ToDateTime ( row [ "LEG006" ] . ToString ( ) );
                modelBody . LEG007 = string . IsNullOrEmpty ( row [ "LEG007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG007" ] . ToString ( ) );
                if ( row [ "LEG008" ] == null || row [ "LEG008" ] . ToString ( ) == string . Empty )
                    modelBody . LEG008 = null;
                else
                    modelBody . LEG008 = Convert . ToDateTime ( row [ "LEG008" ] . ToString ( ) );
                if ( row [ "LEG009" ] == null || row [ "LEG009" ] . ToString ( ) == string . Empty )
                    modelBody . LEG009 = null;
                else
                    modelBody . LEG009 = Convert . ToDateTime ( row [ "LEG009" ] . ToString ( ) );
                modelBody . LEG010 = string . IsNullOrEmpty ( row [ "LEG010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG010" ] . ToString ( ) );
                modelBody . LEG011 = row [ "LEG011" ] . ToString ( );
                modelBody . LEG012 = row [ "LEG012" ] . ToString ( );
                modelBody . LEG013 = row [ "LEG013" ] . ToString ( );
                modelBody . LEG014 = string . IsNullOrEmpty ( row [ "LEG014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG014" ] . ToString ( ) );
                modelBody . LEG015 = string . IsNullOrEmpty ( row [ "LEG015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG015" ] . ToString ( ) );
                modelBody . LEG016 = string . IsNullOrEmpty ( row [ "LEG016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG016" ] . ToString ( ) );
                if ( modelBody . idx < 1 )
                    AddBody ( SQLString ,modelBody );
                else
                    EditBody ( SQLString ,modelBody );
            }

            foreach ( string s in idxList )
            {
                Delete ( SQLString ,s );
            }

            LineProductMesEntityu . LEHNewsBodyEntity body = new LineProductMesEntityu . LEHNewsBodyEntity ( );
            body . LEH001 = model . LEF001;
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                body . LEH002 = row [ "LEH002" ] . ToString ( );
                body . LEH003 = row [ "LEH003" ] . ToString ( );
                body . LEH004 = row [ "LEH004" ] . ToString ( );
                body . LEH005 = row [ "LEH005" ] . ToString ( );
                body . LEH006 = row [ "LEH006" ] . ToString ( );
                body . LEH007 = string . IsNullOrEmpty ( row [ "LEH007" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEH007" ] . ToString ( ) );
                body . LEH008 = string . IsNullOrEmpty ( row [ "LEH008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEH008" ] . ToString ( ) );
                body . LEH009 = string . IsNullOrEmpty ( row [ "LEH009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEH009" ] . ToString ( ) );
                body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( body . idx < 1 )
                    AddBodyOne ( SQLString ,body );
                else
                    EidtBodyOne ( SQLString ,body );
            }

            foreach ( string s in idxListOne )
            {
                DeleteOne ( SQLString ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString,LineProductMesEntityu . LEDNewsHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLEF(" );
            strSql . Append ( "LEF001,LEF009,LEF010,LEF011,LEF012,LEF013,LEF015,LEF016,LEF017,LEF018,LEF019,LEF020)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LEF001,@LEF009,@LEF010,@LEF011,@LEF012,@LEF013,@LEF015,@LEF016,@LEF017,@LEF018,@LEF019,@LEF020)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEF001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF009", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF010", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF011", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF012", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF013", SqlDbType.Date,3),
                    new SqlParameter("@LEF015", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEF016", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEF017", SqlDbType.Bit,1),
                    new SqlParameter("@LEF018", SqlDbType.Bit,1),
                    new SqlParameter("@LEF019", SqlDbType.Decimal),
                    new SqlParameter("@LEF020", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . LEF001;
            parameters [ 1 ] . Value = model . LEF009;
            parameters [ 2 ] . Value = model . LEF010;
            parameters [ 3 ] . Value = model . LEF011;
            parameters [ 4 ] . Value = model . LEF012;
            parameters [ 5 ] . Value = model . LEF013;
            parameters [ 6 ] . Value = model . LEF015;
            parameters [ 7 ] . Value = model . LEF016;
            parameters [ 8 ] . Value = model . LEF017;
            parameters [ 9 ] . Value = model . LEF018;
            parameters [ 10 ] . Value = model . LEF019;
            parameters [ 11 ] . Value = model . LEF020;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEDNewsBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLEG(" );
            strSql . Append ( "LEG001,LEG002,LEG003,LEG004,LEG005,LEG006,LEG007,LEG008,LEG009,LEG010,LEG011,LEG012,LEG013,LEG014,LEG015,LEG016)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LEG001,@LEG002,@LEG003,@LEG004,@LEG005,@LEG006,@LEG007,@LEG008,@LEG009,@LEG010,@LEG011,@LEG012,@LEG013,@LEG014,@LEG015,@LEG016)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEG001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG005", SqlDbType.DateTime),
                    new SqlParameter("@LEG006", SqlDbType.DateTime),
                    new SqlParameter("@LEG007", SqlDbType.Decimal,9),
                    new SqlParameter("@LEG008", SqlDbType.DateTime),
                    new SqlParameter("@LEG009", SqlDbType.DateTime),
                    new SqlParameter("@LEG010", SqlDbType.Decimal,9),
                    new SqlParameter("@LEG011", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEG012", SqlDbType.NVarChar,5),
                    new SqlParameter("@LEG013", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG014", SqlDbType.Decimal,9),
                    new SqlParameter("@LEG015", SqlDbType.Decimal,9),
                    new SqlParameter("@LEG016", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . LEG001;
            parameters [ 1 ] . Value = model . LEG002;
            parameters [ 2 ] . Value = model . LEG003;
            parameters [ 3 ] . Value = model . LEG004;
            parameters [ 4 ] . Value = model . LEG005;
            parameters [ 5 ] . Value = model . LEG006;
            parameters [ 6 ] . Value = model . LEG007;
            parameters [ 7 ] . Value = model . LEG008;
            parameters [ 8 ] . Value = model . LEG009;
            parameters [ 9 ] . Value = model . LEG010;
            parameters [ 10 ] . Value = model . LEG011;
            parameters [ 11 ] . Value = model . LEG012;
            parameters [ 12 ] . Value = model . LEG013;
            parameters [ 13 ] . Value = model . LEG014;
            parameters [ 14 ] . Value = model . LEG015;
            parameters [ 15 ] . Value = model . LEG016;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEHNewsBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLEH(" );
            strSql . Append ( "LEH001,LEH002,LEH003,LEH004,LEH005,LEH006,LEH007,LEH008,LEH009)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LEH001,@LEH002,@LEH003,@LEH004,@LEH005,@LEH006,@LEH007,@LEH008,@LEH009)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEH001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEH002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEH003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEH004", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEH005", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEH006", SqlDbType.NVarChar,10),
                    new SqlParameter("@LEH007", SqlDbType.Int,4),
                    new SqlParameter("@LEH008", SqlDbType.Decimal,9),
                    new SqlParameter("@LEH009", SqlDbType.Int,4),
                    new SqlParameter("@LEH010", SqlDbType.NChar,10),
                    new SqlParameter("@LEH011", SqlDbType.NChar,10),
                    new SqlParameter("@LEH012", SqlDbType.NChar,10)};
            parameters [ 0 ] . Value = model . LEH001;
            parameters [ 1 ] . Value = model . LEH002;
            parameters [ 2 ] . Value = model . LEH003;
            parameters [ 3 ] . Value = model . LEH004;
            parameters [ 4 ] . Value = model . LEH005;
            parameters [ 5 ] . Value = model . LEH006;
            parameters [ 6 ] . Value = model . LEH007;
            parameters [ 7 ] . Value = model . LEH008;
            parameters [ 8 ] . Value = model . LEH009;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEDNewsHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLEF set " );
            strSql . Append ( "LEF009=@LEF009," );
            strSql . Append ( "LEF010=@LEF010," );
            strSql . Append ( "LEF011=@LEF011," );
            strSql . Append ( "LEF012=@LEF012," );
            strSql . Append ( "LEF013=@LEF013," );
            //strSql . Append ( "LEF014=@LEF014," );
            strSql . Append ( "LEF015=@LEF015," );
            strSql . Append ( "LEF016=@LEF016," );
            strSql . Append ( "LEF017=@LEF017," );
            strSql . Append ( "LEF018=@LEF018," );
            strSql . Append ( "LEF019=@LEF019," );
            strSql . Append ( "LEF020=@LEF020 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEF009", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF010", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF011", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF012", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEF013", SqlDbType.Date,3),
                    //new SqlParameter("@LEF014", SqlDbType.Int,4),
                    new SqlParameter("@LEF015", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEF016", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEF017", SqlDbType.Bit,1),
                    new SqlParameter("@LEF018", SqlDbType.Bit,1),
                    new SqlParameter("@LEF019", SqlDbType.Decimal),
                    new SqlParameter("@LEF020", SqlDbType.Decimal),
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . LEF009;
            parameters [ 1 ] . Value = model . LEF010;
            parameters [ 2 ] . Value = model . LEF011;
            parameters [ 3 ] . Value = model . LEF012;
            parameters [ 4 ] . Value = model . LEF013;
            parameters [ 5 ] . Value = model . LEF015;
            parameters [ 6 ] . Value = model . LEF016;
            parameters [ 7 ] . Value = model . LEF017;
            parameters [ 8 ] . Value = model . LEF018;
            parameters [ 9 ] . Value = model . LEF019;
            parameters [ 10 ] . Value = model . LEF020;
            parameters [ 11 ] . Value = model . idx;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEDNewsBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLEG set " );
            strSql . Append ( "LEG002=@LEG002," );
            strSql . Append ( "LEG003=@LEG003," );
            strSql . Append ( "LEG004=@LEG004," );
            strSql . Append ( "LEG005=@LEG005," );
            strSql . Append ( "LEG006=@LEG006," );
            strSql . Append ( "LEG007=@LEG007," );
            strSql . Append ( "LEG008=@LEG008," );
            strSql . Append ( "LEG009=@LEG009," );
            strSql . Append ( "LEG010=@LEG010," );
            strSql . Append ( "LEG011=@LEG011," );
            strSql . Append ( "LEG012=@LEG012," );
            strSql . Append ( "LEG013=@LEG013," );
            strSql . Append ( "LEG014=@LEG014," );
            strSql . Append ( "LEG015=@LEG015," );
            strSql . Append ( "LEG016=@LEG016 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEG003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG005", SqlDbType.DateTime),
                    new SqlParameter("@LEG006", SqlDbType.DateTime),
                    new SqlParameter("@LEG007", SqlDbType.Decimal,9),
                    new SqlParameter("@LEG008", SqlDbType.DateTime),
                    new SqlParameter("@LEG009", SqlDbType.DateTime),
                    new SqlParameter("@LEG010", SqlDbType.Decimal,9),
                    new SqlParameter("@LEG011", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEG012", SqlDbType.NVarChar,5),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@LEG002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG013", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEG014", SqlDbType.Decimal),
                    new SqlParameter("@LEG015", SqlDbType.Decimal),
                    new SqlParameter("@LEG016", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . LEG003;
            parameters [ 1 ] . Value = model . LEG004;
            parameters [ 2 ] . Value = model . LEG005;
            parameters [ 3 ] . Value = model . LEG006;
            parameters [ 4 ] . Value = model . LEG007;
            parameters [ 5 ] . Value = model . LEG008;
            parameters [ 6 ] . Value = model . LEG009;
            parameters [ 7 ] . Value = model . LEG010;
            parameters [ 8 ] . Value = model . LEG011;
            parameters [ 9 ] . Value = model . LEG012;
            parameters [ 10 ] . Value = model . idx;
            parameters [ 11 ] . Value = model . LEG002;
            parameters [ 12 ] . Value = model . LEG013;
            parameters [ 13 ] . Value = model . LEG014;
            parameters [ 14 ] . Value = model . LEG015;
            parameters [ 15 ] . Value = model . LEG016;
            SQLString . Add ( strSql ,parameters );
        }
        void EidtBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEHNewsBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLEH set " );
            strSql . Append ( "LEH002=@LEH002," );
            strSql . Append ( "LEH003=@LEH003," );
            strSql . Append ( "LEH004=@LEH004," );
            strSql . Append ( "LEH005=@LEH005," );
            strSql . Append ( "LEH006=@LEH006," );
            strSql . Append ( "LEH007=@LEH007," );
            strSql . Append ( "LEH008=@LEH008," );
            strSql . Append ( "LEH009=@LEH009 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEH002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEH003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEH004", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEH005", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEH006", SqlDbType.NVarChar,10),
                    new SqlParameter("@LEH007", SqlDbType.Int,4),
                    new SqlParameter("@LEH008", SqlDbType.Decimal,9),
                    new SqlParameter("@LEH009", SqlDbType.Int,4),
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . LEH002;
            parameters [ 1 ] . Value = model . LEH003;
            parameters [ 2 ] . Value = model . LEH004;
            parameters [ 3 ] . Value = model . LEH005;
            parameters [ 4 ] . Value = model . LEH006;
            parameters [ 5 ] . Value = model . LEH007;
            parameters [ 6 ] . Value = model . LEH008;
            parameters [ 7 ] . Value = model . LEH009;
            parameters [ 8 ] . Value = model . idx;
            SQLString . Add ( strSql ,parameters );
        }

        void Delete ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKLEG WHERE idx={0}" ,s );

            SQLString . Add ( strSql ,null );
        }
        void DeleteOne ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "delete from MIKLEH " );
            strSql . AppendFormat ( " where idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取查询数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableQuery ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LEF001,LEH002,LEH003,LEH004,LEF017,LEF018,LEF013,LEH007,LEH009 FROM MIKLEF A INNER JOIN MIKLEH B ON A.LEF001=B.LEH001 WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . LEDNewsHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,LEF001,LEF002,LEF003,LEF004,LEF005,LEF006,LEF007,LEF008,LEF009,LEF010,LEF011,LEF012,LEF013,LEF014,LEF015,LEF016,LEF017,LEF018,LEF019,LEF020 FROM MIKLEF WHERE LEF001='{0}'" ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
                return getModel ( table . Rows [ 0 ] );
            else
                return null;
        }

        public LineProductMesEntityu . LEDNewsHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . LEDNewsHeaderEntity model = new LineProductMesEntityu . LEDNewsHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "idx" ] != null && row [ "idx" ] . ToString ( ) != "" )
                {
                    model . idx = int . Parse ( row [ "idx" ] . ToString ( ) );
                }
                if ( row [ "LEF001" ] != null )
                {
                    model . LEF001 = row [ "LEF001" ] . ToString ( );
                }
                if ( row [ "LEF002" ] != null )
                {
                    model . LEF002 = row [ "LEF002" ] . ToString ( );
                }
                if ( row [ "LEF003" ] != null )
                {
                    model . LEF003 = row [ "LEF003" ] . ToString ( );
                }
                if ( row [ "LEF004" ] != null )
                {
                    model . LEF004 = row [ "LEF004" ] . ToString ( );
                }
                if ( row [ "LEF005" ] != null )
                {
                    model . LEF005 = row [ "LEF005" ] . ToString ( );
                }
                if ( row [ "LEF006" ] != null )
                {
                    model . LEF006 = row [ "LEF006" ] . ToString ( );
                }
                if ( row [ "LEF007" ] != null && row [ "LEF007" ] . ToString ( ) != "" )
                {
                    model . LEF007 = int . Parse ( row [ "LEF007" ] . ToString ( ) );
                }
                if ( row [ "LEF008" ] != null && row [ "LEF008" ] . ToString ( ) != "" )
                {
                    model . LEF008 = decimal . Parse ( row [ "LEF008" ] . ToString ( ) );
                }
                if ( row [ "LEF009" ] != null )
                {
                    model . LEF009 = row [ "LEF009" ] . ToString ( );
                }
                if ( row [ "LEF010" ] != null )
                {
                    model . LEF010 = row [ "LEF010" ] . ToString ( );
                }
                if ( row [ "LEF011" ] != null )
                {
                    model . LEF011 = row [ "LEF011" ] . ToString ( );
                }
                if ( row [ "LEF012" ] != null )
                {
                    model . LEF012 = row [ "LEF012" ] . ToString ( );
                }
                if ( row [ "LEF013" ] != null && row [ "LEF013" ] . ToString ( ) != "" )
                {
                    model . LEF013 = DateTime . Parse ( row [ "LEF013" ] . ToString ( ) );
                }
                if ( row [ "LEF014" ] != null && row [ "LEF014" ] . ToString ( ) != "" )
                {
                    model . LEF014 = int . Parse ( row [ "LEF014" ] . ToString ( ) );
                }
                if ( row [ "LEF015" ] != null )
                {
                    model . LEF015 = row [ "LEF015" ] . ToString ( );
                }
                if ( row [ "LEF016" ] != null )
                {
                    model . LEF016 = row [ "LEF016" ] . ToString ( );
                }
                if ( row [ "LEF017" ] != null && row [ "LEF017" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "LEF017" ] . ToString ( ) == "1" ) || ( row [ "LEF017" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . LEF017 = true;
                    }
                    else
                    {
                        model . LEF017 = false;
                    }
                }
                if ( row [ "LEF018" ] != null && row [ "LEF018" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "LEF018" ] . ToString ( ) == "1" ) || ( row [ "LEF018" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . LEF018 = true;
                    }
                    else
                    {
                        model . LEF018 = false;
                    }
                }
                if ( row [ "LEF019" ] != null && row [ "LEF019" ] . ToString ( ) != "" )
                {
                    model . LEF019 = decimal . Parse ( row [ "LEF019" ] . ToString ( ) );
                }
                if ( row [ "LEF020" ] != null && row [ "LEF020" ] . ToString ( ) != "" )
                {
                    model . LEF020 = decimal . Parse ( row [ "LEF020" ] . ToString ( ) );
                }
            }
            return model;
        }

        /// <summary>
        /// 总完工数量是否大于工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists ( LineProductMesEntityu.LEDNewsHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT SUM(LEF014) LEF014 FROM MIKLEF WHERE LEF001!='{0}' AND LEF002='{1}' AND LEF018=0" ,model . LEF001 ,model . LEF002 );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return true;
            else
            {
                decimal result = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "LEF014" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "LEF014" ] . ToString ( ) );
                if ( result + model . LEF014 > model . LEF007 )
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LEF001 ANW001,LEF010 ANW011,LEF012 ANW013,LEF013 ANW022,GETDATE() dat FROM MIKLEF WHERE LEF001='{0}'" ,oddNum );

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
            strSql . AppendFormat ( "SELECT DISTINCT LEH002 ANW002,LEH003 ANW003,LEH004 ANW004,LEH005 ANW005,LEH006 ANW007,LEH007 ANW006,LEH009 ANW009,DDA003 DEA008 FROM MIKLEG A INNER JOIN MIKLEF B ON A.LEG001=B.LEF001 INNER JOIN MIKLEH D ON A.LEG001=D.LEH001 LEFT JOIN TPADEA C ON D.LEH003=C.DEA001 INNER JOIN TPADDA E ON C.DEA008=E.DDA001 WHERE LEF001 ='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( string orderNum ,string proNum ,string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LEH002,LEH003,LEH007-SUM(LEH009) LEH FROM MIKLEH WHERE LEH001!='{0}' AND LEH002='{1}' AND LEH003='{2}' GROUP BY LEH002,LEH003,LEH007" ,oddNum ,orderNum ,proNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
