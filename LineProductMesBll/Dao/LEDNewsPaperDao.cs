using System;
using System . Collections . Generic;
using System . Text;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class LEDNewsPaperDao
    {
        /// <summary>
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0504' AND RAA020='N' AND RAA024='T'" );
            strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0504' AND RAA020='N' AND RAA024='T'" );
            
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
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP005,DAA002 FROM MIKEMP  A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1 " );

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
            strSql . Append ( "SELECT MAX(LEC001) LEC001 FROM MIKLEC" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 0 )
                return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "LEC001" ] . ToString ( );
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
            strSql . AppendFormat ( "SELECT idx,LED001,LED002,LED003,LED004,LED005,LED006,LED007,LED008,LED009,LED010,LED011,LED012,LED016,LED013,LED014,LED015 FROM MIKLED WHERE {0}" ,strWhere );

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
            strSql . AppendFormat ( "SELECT idx,LEE001,LEE002,LEE003,LEE004,LEE005,LEE006,LEE007,LEE008,LEE009,0 U4 FROM MIKLEE WHERE {0}" ,strWhere );

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
            strSql . AppendFormat ( "DELETE FROM MIKLEC WHERE LEC001='{0}'" ,oddNum );

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
            strSql . Append ( "UPDATE MIKLEC SET LEC017=@LEC017 WHERE LEC001=@LEC001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@LEC001",SqlDbType.NVarChar,20),
                new SqlParameter("@LEC017",SqlDbType.Bit)
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
            strSql . Append ( "UPDATE MIKLEC SET LEC018=@LEC018 WHERE LEC001=@LEC001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@LEC001",SqlDbType.NVarChar,20),
                new SqlParameter("@LEC018",SqlDbType.Bit)
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
        public bool Save ( LineProductMesEntityu.LEDNewsPaperHeaderEntity model,DataTable tableView,DataTable tableViewOne )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            model . LEC001 = getOddNum ( );
            UserInfoMation . oddNum = model . LEC001;
            model . LEC022 = UserInfoMation . userName;
            AddHeader ( SQLString ,model );

            LineProductMesEntityu . LEDNewsPaperBodyEntity modelBody = new LineProductMesEntityu . LEDNewsPaperBodyEntity ( );
            modelBody . LED001 = model . LEC001;
            foreach ( DataRow row in tableView . Rows )
            {
                modelBody . LED002 = row [ "LED002" ] . ToString ( );
                modelBody . LED003 = row [ "LED003" ] . ToString ( );
                modelBody . LED004 = row [ "LED004" ] . ToString ( );
                if ( row [ "LED005" ] == null || row [ "LED005" ] . ToString ( ) == string . Empty )
                    modelBody . LED005 = null;
                else
                    modelBody . LED005 = Convert . ToDateTime ( row [ "LED005" ] . ToString ( ) );
                if ( row [ "LED006" ] == null || row [ "LED006" ] . ToString ( ) == string . Empty )
                    modelBody . LED006 = null;
                else
                    modelBody . LED006 = Convert . ToDateTime ( row [ "LED006" ] . ToString ( ) );
                modelBody . LED007 = string . IsNullOrEmpty ( row [ "LED007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED007" ] . ToString ( ) );
                if ( row [ "LED008" ] == null || row [ "LED008" ] . ToString ( ) == string . Empty )
                    modelBody . LED008 = null;
                else
                    modelBody . LED008 = Convert . ToDateTime ( row [ "LED008" ] . ToString ( ) );
                if ( row [ "LED009" ] == null || row [ "LED009" ] . ToString ( ) == string . Empty )
                    modelBody . LED009 = null;
                else
                    modelBody . LED009 = Convert . ToDateTime ( row [ "LED009" ] . ToString ( ) );
                modelBody . LED010 = string . IsNullOrEmpty ( row [ "LED010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED010" ] . ToString ( ) );
                modelBody . LED011 = row [ "LED011" ] . ToString ( );
                modelBody . LED012 = row [ "LED012" ] . ToString ( );
                modelBody . LED013 = row [ "LED013" ] . ToString ( );
                modelBody . LED014 = string . IsNullOrEmpty ( row [ "LED014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED014" ] . ToString ( ) );
                modelBody . LED015 = string . IsNullOrEmpty ( row [ "LED015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED015" ] . ToString ( ) );
                modelBody . LED016 = string . IsNullOrEmpty ( row [ "LED016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED016" ] . ToString ( ) );
                AddBody ( SQLString ,modelBody );
            }

            LineProductMesEntityu . LEENewsPaperBodyEntity body = new LineProductMesEntityu . LEENewsPaperBodyEntity ( );
            body . LEE001 = model . LEC001;
            foreach ( DataRow row in tableViewOne . Rows )
            {
                body . LEE002 = row [ "LEE002" ] . ToString ( );
                body . LEE003 = row [ "LEE003" ] . ToString ( );
                body . LEE004 = row [ "LEE004" ] . ToString ( );
                body . LEE005 = row [ "LEE005" ] . ToString ( );
                body . LEE006 = row [ "LEE006" ] . ToString ( );
                body . LEE007 = string . IsNullOrEmpty ( row [ "LEE007" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEE007" ] . ToString ( ) );
                body . LEE008 = string . IsNullOrEmpty ( row [ "LEE008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEE008" ] . ToString ( ) );
                body . LEE009 = string . IsNullOrEmpty ( row [ "LEE009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEE009" ] . ToString ( ) );
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
        public bool Edit ( LineProductMesEntityu . LEDNewsPaperHeaderEntity model ,DataTable tableView,DataTable tableViewOne,List<string> idxList,List<string> idxListOne )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            EditHeader ( SQLString ,model );

            LineProductMesEntityu . LEDNewsPaperBodyEntity modelBody = new LineProductMesEntityu . LEDNewsPaperBodyEntity ( );
            modelBody . LED001 = model . LEC001;
            foreach ( DataRow row in tableView . Rows )
            {
                modelBody . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                modelBody . LED002 = row [ "LED002" ] . ToString ( );
                modelBody . LED003 = row [ "LED003" ] . ToString ( );
                modelBody . LED004 = row [ "LED004" ] . ToString ( );
                if ( row [ "LED005" ] == null || row [ "LED005" ] . ToString ( ) == string . Empty )
                    modelBody . LED005 = null;
                else
                    modelBody . LED005 = Convert . ToDateTime ( row [ "LED005" ] . ToString ( ) );
                if ( row [ "LED006" ] == null || row [ "LED006" ] . ToString ( ) == string . Empty )
                    modelBody . LED006 = null;
                else
                    modelBody . LED006 = Convert . ToDateTime ( row [ "LED006" ] . ToString ( ) );
                modelBody . LED007 = string . IsNullOrEmpty ( row [ "LED007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED007" ] . ToString ( ) );
                if ( row [ "LED008" ] == null || row [ "LED008" ] . ToString ( ) == string . Empty )
                    modelBody . LED008 = null;
                else
                    modelBody . LED008 = Convert . ToDateTime ( row [ "LED008" ] . ToString ( ) );
                if ( row [ "LED009" ] == null || row [ "LED009" ] . ToString ( ) == string . Empty )
                    modelBody . LED009 = null;
                else
                    modelBody . LED009 = Convert . ToDateTime ( row [ "LED009" ] . ToString ( ) );
                modelBody . LED010 = string . IsNullOrEmpty ( row [ "LED010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED010" ] . ToString ( ) );
                modelBody . LED011 = row [ "LED011" ] . ToString ( );
                modelBody . LED012 = row [ "LED012" ] . ToString ( );
                modelBody . LED013 = row [ "LED013" ] . ToString ( );
                modelBody . LED014 = string . IsNullOrEmpty ( row [ "LED014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED014" ] . ToString ( ) );
                modelBody . LED015 = string . IsNullOrEmpty ( row [ "LED015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED015" ] . ToString ( ) );
                modelBody . LED016 = string . IsNullOrEmpty ( row [ "LED016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED016" ] . ToString ( ) );
                if ( modelBody . idx < 1 )
                    AddBody ( SQLString ,modelBody );
                else
                    EditBody ( SQLString ,modelBody );
            }

            foreach ( string s in idxList )
            {
                Delete ( SQLString ,s );
            }

            LineProductMesEntityu . LEENewsPaperBodyEntity body = new LineProductMesEntityu . LEENewsPaperBodyEntity ( );
            body . LEE001 = model . LEC001;
            foreach ( DataRow row in tableViewOne . Rows )
            {
                body . LEE002 = row [ "LEE002" ] . ToString ( );
                body . LEE003 = row [ "LEE003" ] . ToString ( );
                body . LEE004 = row [ "LEE004" ] . ToString ( );
                body . LEE005 = row [ "LEE005" ] . ToString ( );
                body . LEE006 = row [ "LEE006" ] . ToString ( );
                body . LEE007 = string . IsNullOrEmpty ( row [ "LEE007" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEE007" ] . ToString ( ) );
                body . LEE008 = string . IsNullOrEmpty ( row [ "LEE008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEE008" ] . ToString ( ) );
                body . LEE009 = string . IsNullOrEmpty ( row [ "LEE009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEE009" ] . ToString ( ) );
                body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( body . idx < 1 )
                    AddBodyOne ( SQLString ,body );
                else
                    EditBodyOne ( SQLString ,body );
            }

            foreach ( string s in idxListOne )
            {
                DeleteOne ( SQLString ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString,LineProductMesEntityu . LEDNewsPaperHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLEC(" );
            strSql . Append ( "LEC001,LEC002,LEC003,LEC004,LEC005,LEC006,LEC007,LEC008,LEC009,LEC010,LEC011,LEC012,LEC013,LEC014,LEC015,LEC016,LEC017,LEC018,LEC019,LEC020,LEC021,LEC022)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LEC001,@LEC002,@LEC003,@LEC004,@LEC005,@LEC006,@LEC007,@LEC008,@LEC009,@LEC010,@LEC011,@LEC012,@LEC013,@LEC014,@LEC015,@LEC016,@LEC017,@LEC018,@LEC019,@LEC020,@LEC021,@LEC022)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEC001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC004", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEC005", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC006", SqlDbType.NVarChar,5),
                    new SqlParameter("@LEC007", SqlDbType.Int,4),
                    new SqlParameter("@LEC008", SqlDbType.Decimal,9),
                    new SqlParameter("@LEC009", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC010", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC011", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC012", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC013", SqlDbType.Date,3),
                    new SqlParameter("@LEC014", SqlDbType.Int,4),
                    new SqlParameter("@LEC015", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEC016", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEC017", SqlDbType.Bit,1),
                    new SqlParameter("@LEC018", SqlDbType.Bit,1),
                    new SqlParameter("@LEC019", SqlDbType.Decimal),
                    new SqlParameter("@LEC020", SqlDbType.Decimal),
                    new SqlParameter("@LEC021", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC022", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . LEC001;
            parameters [ 1 ] . Value = model . LEC002;
            parameters [ 2 ] . Value = model . LEC003;
            parameters [ 3 ] . Value = model . LEC004;
            parameters [ 4 ] . Value = model . LEC005;
            parameters [ 5 ] . Value = model . LEC006;
            parameters [ 6 ] . Value = model . LEC007;
            parameters [ 7 ] . Value = model . LEC008;
            parameters [ 8 ] . Value = model . LEC009;
            parameters [ 9 ] . Value = model . LEC010;
            parameters [ 10 ] . Value = model . LEC011;
            parameters [ 11 ] . Value = model . LEC012;
            parameters [ 12 ] . Value = model . LEC013;
            parameters [ 13 ] . Value = model . LEC014;
            parameters [ 14 ] . Value = model . LEC015;
            parameters [ 15 ] . Value = model . LEC016;
            parameters [ 16 ] . Value = model . LEC017;
            parameters [ 17 ] . Value = model . LEC018;
            parameters [ 18 ] . Value = model . LEC019;
            parameters [ 19 ] . Value = model . LEC020;
            parameters [ 20 ] . Value = model . LEC021;
            parameters [ 21 ] . Value = model . LEC022;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEDNewsPaperBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLED(" );
            strSql . Append ( "LED001,LED002,LED003,LED004,LED005,LED006,LED007,LED008,LED009,LED010,LED011,LED012,LED013,LED014,LED015,LED016)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LED001,@LED002,@LED003,@LED004,@LED005,@LED006,@LED007,@LED008,@LED009,@LED010,@LED011,@LED012,@LED013,@LED014,@LED015,@LED016)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LED001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED005", SqlDbType.DateTime),
                    new SqlParameter("@LED006", SqlDbType.DateTime),
                    new SqlParameter("@LED007", SqlDbType.Decimal,9),
                    new SqlParameter("@LED008", SqlDbType.DateTime),
                    new SqlParameter("@LED009", SqlDbType.DateTime),
                    new SqlParameter("@LED010", SqlDbType.Decimal,9),
                    new SqlParameter("@LED011", SqlDbType.NVarChar,100),
                    new SqlParameter("@LED012", SqlDbType.NVarChar,5),
                    new SqlParameter("@LED013", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED014", SqlDbType.Decimal,9),
                    new SqlParameter("@LED015", SqlDbType.Decimal,9),
                    new SqlParameter("@LED016", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . LED001;
            parameters [ 1 ] . Value = model . LED002;
            parameters [ 2 ] . Value = model . LED003;
            parameters [ 3 ] . Value = model . LED004;
            parameters [ 4 ] . Value = model . LED005;
            parameters [ 5 ] . Value = model . LED006;
            parameters [ 6 ] . Value = model . LED007;
            parameters [ 7 ] . Value = model . LED008;
            parameters [ 8 ] . Value = model . LED009;
            parameters [ 9 ] . Value = model . LED010;
            parameters [ 10 ] . Value = model . LED011;
            parameters [ 11 ] . Value = model . LED012;
            parameters [ 12 ] . Value = model . LED013;
            parameters [ 13 ] . Value = model . LED014;
            parameters [ 14 ] . Value = model . LED015;
            parameters [ 15 ] . Value = model . LED016;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEENewsPaperBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKLEE(" );
            strSql . Append ( "LEE001,LEE002,LEE003,LEE004,LEE005,LEE006,LEE007,LEE008,LEE009)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@LEE001,@LEE002,@LEE003,@LEE004,@LEE005,@LEE006,@LEE007,@LEE008,@LEE009)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEE001", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEE002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEE003", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEE004", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEE005", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEE006", SqlDbType.NVarChar,10),
                    new SqlParameter("@LEE007", SqlDbType.Int,4),
                    new SqlParameter("@LEE008", SqlDbType.Decimal,9),
                    new SqlParameter("@LEE009", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . LEE001;
            parameters [ 1 ] . Value = model . LEE002;
            parameters [ 2 ] . Value = model . LEE003;
            parameters [ 3 ] . Value = model . LEE004;
            parameters [ 4 ] . Value = model . LEE005;
            parameters [ 5 ] . Value = model . LEE006;
            parameters [ 6 ] . Value = model . LEE007;
            parameters [ 7 ] . Value = model . LEE008;
            parameters [ 8 ] . Value = model . LEE009;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEDNewsPaperHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLEC set " );
            strSql . Append ( "LEC002=@LEC002," );
            strSql . Append ( "LEC003=@LEC003," );
            strSql . Append ( "LEC004=@LEC004," );
            strSql . Append ( "LEC005=@LEC005," );
            strSql . Append ( "LEC006=@LEC006," );
            strSql . Append ( "LEC007=@LEC007," );
            strSql . Append ( "LEC008=@LEC008," );
            strSql . Append ( "LEC009=@LEC009," );
            strSql . Append ( "LEC010=@LEC010," );
            strSql . Append ( "LEC011=@LEC011," );
            strSql . Append ( "LEC012=@LEC012," );
            strSql . Append ( "LEC013=@LEC013," );
            strSql . Append ( "LEC014=@LEC014," );
            strSql . Append ( "LEC015=@LEC015," );
            strSql . Append ( "LEC016=@LEC016," );
            strSql . Append ( "LEC017=@LEC017," );
            strSql . Append ( "LEC018=@LEC018," );
            strSql . Append ( "LEC019=@LEC019," );
            strSql . Append ( "LEC020=@LEC020," );
            strSql . Append ( "LEC021=@LEC021 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEC002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC004", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEC005", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC006", SqlDbType.NVarChar,5),
                    new SqlParameter("@LEC007", SqlDbType.Int,4),
                    new SqlParameter("@LEC008", SqlDbType.Decimal,9),
                    new SqlParameter("@LEC009", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC010", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC011", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC012", SqlDbType.NVarChar,20),
                    new SqlParameter("@LEC013", SqlDbType.Date,3),
                    new SqlParameter("@LEC014", SqlDbType.Int,4),
                    new SqlParameter("@LEC015", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEC016", SqlDbType.NVarChar,100),
                    new SqlParameter("@LEC017", SqlDbType.Bit,1),
                    new SqlParameter("@LEC018", SqlDbType.Bit,1),
                    new SqlParameter("@LEC019", SqlDbType.Decimal),
                    new SqlParameter("@LEC020", SqlDbType.Decimal),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@LEC021", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . LEC002;
            parameters [ 1 ] . Value = model . LEC003;
            parameters [ 2 ] . Value = model . LEC004;
            parameters [ 3 ] . Value = model . LEC005;
            parameters [ 4 ] . Value = model . LEC006;
            parameters [ 5 ] . Value = model . LEC007;
            parameters [ 6 ] . Value = model . LEC008;
            parameters [ 7 ] . Value = model . LEC009;
            parameters [ 8 ] . Value = model . LEC010;
            parameters [ 9 ] . Value = model . LEC011;
            parameters [ 10 ] . Value = model . LEC012;
            parameters [ 11 ] . Value = model . LEC013;
            parameters [ 12 ] . Value = model . LEC014;
            parameters [ 13 ] . Value = model . LEC015;
            parameters [ 14 ] . Value = model . LEC016;
            parameters [ 15 ] . Value = model . LEC017;
            parameters [ 16 ] . Value = model . LEC018;
            parameters [ 17 ] . Value = model . LEC019;
            parameters [ 18 ] . Value = model . LEC020;
            parameters [ 19 ] . Value = model . LEC021;
            parameters [ 20 ] . Value = model . idx;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEDNewsPaperBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLED set " );
            strSql . Append ( "LED002=@LED002," );
            strSql . Append ( "LED003=@LED003," );
            strSql . Append ( "LED004=@LED004," );
            strSql . Append ( "LED005=@LED005," );
            strSql . Append ( "LED006=@LED006," );
            strSql . Append ( "LED007=@LED007," );
            strSql . Append ( "LED008=@LED008," );
            strSql . Append ( "LED009=@LED009," );
            strSql . Append ( "LED010=@LED010," );
            strSql . Append ( "LED011=@LED011," );
            strSql . Append ( "LED012=@LED012," );
            strSql . Append ( "LED013=@LED013," );
            strSql . Append ( "LED014=@LED014," );
            strSql . Append ( "LED015=@LED015," );
            strSql . Append ( "LED016=@LED016 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LED003", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED004", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED005", SqlDbType.DateTime),
                    new SqlParameter("@LED006", SqlDbType.DateTime),
                    new SqlParameter("@LED007", SqlDbType.Decimal,9),
                    new SqlParameter("@LED008", SqlDbType.DateTime),
                    new SqlParameter("@LED009", SqlDbType.DateTime),
                    new SqlParameter("@LED010", SqlDbType.Decimal,9),
                    new SqlParameter("@LED011", SqlDbType.NVarChar,100),
                    new SqlParameter("@LED012", SqlDbType.NVarChar,5),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@LED002", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED013", SqlDbType.NVarChar,20),
                    new SqlParameter("@LED014", SqlDbType.Decimal),
                    new SqlParameter("@LED015", SqlDbType.Decimal),
                    new SqlParameter("@LED016", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . LED003;
            parameters [ 1 ] . Value = model . LED004;
            parameters [ 2 ] . Value = model . LED005;
            parameters [ 3 ] . Value = model . LED006;
            parameters [ 4 ] . Value = model . LED007;
            parameters [ 5 ] . Value = model . LED008;
            parameters [ 6 ] . Value = model . LED009;
            parameters [ 7 ] . Value = model . LED010;
            parameters [ 8 ] . Value = model . LED011;
            parameters [ 9 ] . Value = model . LED012;
            parameters [ 10 ] . Value = model . idx;
            parameters [ 11 ] . Value = model . LED002;
            parameters [ 12 ] . Value = model . LED013;
            parameters [ 13 ] . Value = model . LED014;
            parameters [ 14 ] . Value = model . LED015;
            parameters [ 15 ] . Value = model . LED016;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LEENewsPaperBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKLEE set " );
            strSql . Append ( "LEE002=@LEE002," );
            strSql . Append ( "LEE003=@LEE003," );
            strSql . Append ( "LEE004=@LEE004," );
            strSql . Append ( "LEE005=@LEE005," );
            strSql . Append ( "LEE006=@LEE006," );
            strSql . Append ( "LEE007=@LEE007," );
            strSql . Append ( "LEE008=@LEE008," );
            strSql . Append ( "LEE009=@LEE009 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@LEE003", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEE004", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEE005", SqlDbType.NVarChar,50),
                    new SqlParameter("@LEE006", SqlDbType.NVarChar,10),
                    new SqlParameter("@LEE007", SqlDbType.Int,4),
                    new SqlParameter("@LEE008", SqlDbType.Decimal,9),
                    new SqlParameter("@LEE009", SqlDbType.Int,4),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@LEE002", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . LEE003;
            parameters [ 1 ] . Value = model . LEE004;
            parameters [ 2 ] . Value = model . LEE005;
            parameters [ 3 ] . Value = model . LEE006;
            parameters [ 4 ] . Value = model . LEE007;
            parameters [ 5 ] . Value = model . LEE008;
            parameters [ 6 ] . Value = model . LEE009;
            parameters [ 7 ] . Value = model . idx;
            parameters [ 8 ] . Value = model . LEE002;
            SQLString . Add ( strSql ,parameters );
        }

        void Delete ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKLED WHERE idx={0}" ,s );

            SQLString . Add ( strSql ,null );
        }
        void DeleteOne ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "delete from MIKLEE " );
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
            strSql . AppendFormat ( "SELECT LEC001,LEE002,LEE003,LEE004,LEC017,LEC018,LEC013,LEE007,LEE009 FROM MIKLEC A INNER JOIN MIKLEE B ON A.LEC001=B.LEE001 WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . LEDNewsPaperHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,LEC001,LEC002,LEC003,LEC004,LEC005,LEC006,LEC007,LEC008,LEC009,LEC010,LEC011,LEC012,LEC013,LEC014,LEC015,LEC016,LEC017,LEC018,LEC019,LEC020,LEC021 FROM MIKLEC WHERE LEC001='{0}'" ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
                return getModel ( table . Rows [ 0 ] );
            else
                return null;
        }

        public LineProductMesEntityu . LEDNewsPaperHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . LEDNewsPaperHeaderEntity model = new LineProductMesEntityu . LEDNewsPaperHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "idx" ] != null && row [ "idx" ] . ToString ( ) != "" )
                {
                    model . idx = int . Parse ( row [ "idx" ] . ToString ( ) );
                }
                if ( row [ "LEC001" ] != null )
                {
                    model . LEC001 = row [ "LEC001" ] . ToString ( );
                }
                if ( row [ "LEC002" ] != null )
                {
                    model . LEC002 = row [ "LEC002" ] . ToString ( );
                }
                if ( row [ "LEC003" ] != null )
                {
                    model . LEC003 = row [ "LEC003" ] . ToString ( );
                }
                if ( row [ "LEC004" ] != null )
                {
                    model . LEC004 = row [ "LEC004" ] . ToString ( );
                }
                if ( row [ "LEC005" ] != null )
                {
                    model . LEC005 = row [ "LEC005" ] . ToString ( );
                }
                if ( row [ "LEC006" ] != null )
                {
                    model . LEC006 = row [ "LEC006" ] . ToString ( );
                }
                if ( row [ "LEC007" ] != null && row [ "LEC007" ] . ToString ( ) != "" )
                {
                    model . LEC007 = int . Parse ( row [ "LEC007" ] . ToString ( ) );
                }
                if ( row [ "LEC008" ] != null && row [ "LEC008" ] . ToString ( ) != "" )
                {
                    model . LEC008 = decimal . Parse ( row [ "LEC008" ] . ToString ( ) );
                }
                if ( row [ "LEC009" ] != null )
                {
                    model . LEC009 = row [ "LEC009" ] . ToString ( );
                }
                if ( row [ "LEC010" ] != null )
                {
                    model . LEC010 = row [ "LEC010" ] . ToString ( );
                }
                if ( row [ "LEC011" ] != null )
                {
                    model . LEC011 = row [ "LEC011" ] . ToString ( );
                }
                if ( row [ "LEC012" ] != null )
                {
                    model . LEC012 = row [ "LEC012" ] . ToString ( );
                }
                if ( row [ "LEC013" ] != null && row [ "LEC013" ] . ToString ( ) != "" )
                {
                    model . LEC013 = DateTime . Parse ( row [ "LEC013" ] . ToString ( ) );
                }
                if ( row [ "LEC014" ] != null && row [ "LEC014" ] . ToString ( ) != "" )
                {
                    model . LEC014 = int . Parse ( row [ "LEC014" ] . ToString ( ) );
                }
                if ( row [ "LEC015" ] != null )
                {
                    model . LEC015 = row [ "LEC015" ] . ToString ( );
                }
                if ( row [ "LEC016" ] != null )
                {
                    model . LEC016 = row [ "LEC016" ] . ToString ( );
                }
                if ( row [ "LEC017" ] != null && row [ "LEC017" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "LEC017" ] . ToString ( ) == "1" ) || ( row [ "LEC017" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . LEC017 = true;
                    }
                    else
                    {
                        model . LEC017 = false;
                    }
                }
                if ( row [ "LEC018" ] != null && row [ "LEC018" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "LEC018" ] . ToString ( ) == "1" ) || ( row [ "LEC018" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . LEC018 = true;
                    }
                    else
                    {
                        model . LEC018 = false;
                    }
                }
                if ( row [ "LEC019" ] != null && row [ "LEC019" ] . ToString ( ) != "" )
                {
                    model . LEC019 = decimal . Parse ( row [ "LEC019" ] . ToString ( ) );
                }
                if ( row [ "LEC020" ] != null && row [ "LEC020" ] . ToString ( ) != "" )
                {
                    model . LEC020 = decimal . Parse ( row [ "LEC020" ] . ToString ( ) );
                }
                if ( row [ "LEC021" ] != null && row [ "LEC021" ] . ToString ( ) != "" )
                {
                    model . LEC021 = row [ "LEC021" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 总完工数量是否大于工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists ( LineProductMesEntityu.LEDNewsPaperHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT SUM(LEC014) LEC014 FROM MIKLEC WHERE LEC001!='{0}' AND LEC002='{1}' AND LEC018=0" ,model . LEC001 ,model . LEC002 );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return true;
            else
            {
                decimal result = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "LEC014" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "LEC014" ] . ToString ( ) );
                if ( result + model . LEC014 > model . LEC007 )
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
            strSql . AppendFormat ( "SELECT LEC001 ANW001,LEC010 ANW011,LEC012 ANW013,LEC013 ANW022,GETDATE() dat,LEC022 ANW025 FROM MIKLEC WHERE LEC001='{0}'" ,oddNum );

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
            strSql . AppendFormat ( "SELECT DISTINCT LEE002 ANW002,LEE003 ANW003,LEE004 ANW004,LEE005 ANW005,LEE006 ANW007,LEE007 ANW006,LEE009 ANW009,DDA003 DEA008 FROM MIKLED A INNER JOIN MIKLEC B ON A.LED001=B.LEC001 INNER JOIN MIKLEE D ON A.LED001=D.LEE001 LEFT JOIN TPADEA C ON D.LEE003=C.DEA001 INNER JOIN TPADDA E ON C.DEA008=E.DDA001 WHERE LEC001='{0}'" ,oddNum );

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
            strSql . AppendFormat ( "SELECT LEE002,LEE003,LEE007-SUM(LEE009) LEE FROM MIKLEE WHERE LEE001!='{0}' AND LEE002='{1}' AND LEE003='{2}' GROUP BY LEE002,LEE003,LEE007" ,oddNum ,orderNum ,proNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表 报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LEC001,LEC010,LEC012,LEC013,LEC015,LEC019,LEC020,LEC021,JS,CASE LEC021 WHEN '计件' THEN JJ ELSE 0 END JJ,BT,CASE LEC021 WHEN '计件' THEN JJ+JS-BT ELSE JS-BT END ZGZ,ZGS FROM MIKLEC A INNER JOIN (SELECT LED001,CONVERT(FLOAT,SUM(LED010*LED015)) JS,CONVERT(FLOAT,SUM(LED007)) BT,CONVERT(FLOAT,SUM(LED014+LED015)) ZGS FROM MIKLED WHERE LED001='{0}' GROUP BY LED001) B ON A.LEC001=B.LED001 INNER JOIN (SELECT LEE001,CONVERT(FLOAT,SUM(LEE008*LEE009)) JJ FROM MIKLEE WHERE LEE001='{0}' GROUP BY LEE001) C ON A.LEC001=C.LEE001 WHERE LEC001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表 报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFor ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LEE001,LEE002,LEE003,LEE004,LEE005,LEE006,LEE007,CONVERT(FLOAT,LEE008) LEE008,LEE009,CONVERT(FLOAT,LEE008*LEE009) U6 FROM MIKLEE WHERE LEE001 ='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表 报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFiv ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LED001,LED002,LED003,DATENAME(HOUR,LED005)+':'+DATENAME(MINUTE,LED005) LED005,DATENAME(HOUR,LED006)+':'+DATENAME(MINUTE,LED006) LED006,CONVERT(FLOAT,LED007) LED007,DATENAME(HOUR,LED008)+':'+DATENAME(MINUTE,LED008) LED008,DATENAME(HOUR,LED009)+':'+DATENAME(MINUTE,LED009) LED009,CONVERT(FLOAT,LED010) LED010,LED011,LED012,LED013,CONVERT(FLOAT,LED014) LED014,CONVERT(FLOAT,LED015) LED015,CONVERT(FLOAT,LED016) LED016,CONVERT(FLOAT,LED015*LED010) U2,CONVERT(FLOAT,LED014+LED015) U3 FROM MIKLED WHERE LED001 ='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
