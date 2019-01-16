using System;
using System . Collections . Generic;
using System . Data;
using System . Text;
using StudentMgr;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class AssNewWorkDao
    {
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="workId">车间编号</param>
        /// <param name="gruopId">班组编号</param>
        /// <returns></returns>
        public DataTable getUserInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP023,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1 " );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            //StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT MAX(ANW001) ANW001 FROM MIKANW" );

            //DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            //if ( table == null || table . Rows . Count < 0 )
            //    return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            //else
            //{
            //    string code = table . Rows [ 0 ] [ "ANW001" ] . ToString ( );
            //    if ( string . IsNullOrEmpty ( code ) )
            //        return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            //    else
            //    {
            //        if ( code . Substring ( 0 ,8 ) . Equals ( UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) ) )
            //            return ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
            //        else
            //            return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            //    }
            //}
            return GetCodeUtils . getOddNum ( "MIKANW" ,"ANW001" );
        }
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,ANX001,ANX002,ANX003,ANX004,ANX005,ANX006,ANX007,ANX008,CASE WHEN CONVERT(FLOAT,ANX009)=0 THEN NULL ELSE CONVERT(FLOAT,ANX009) END ANX009,CASE WHEN CONVERT(FLOAT,ANX010)=0 THEN NULL ELSE CONVERT(FLOAT,ANX010) END ANX010,ANX011,ANX012,ANX015,ANX016,ANX017,ANX013,ANX014,ANX018 FROM MIKANX " );
            strSql . AppendFormat ( "WHERE {0} " ,strWhere );

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
            strSql . AppendFormat ( "SELECT idx,ANN001,ANN002,ANN003,ANN004,ANN005,ANN006,ANN007,ANN008,ANN009,ANN010,0 U1 FROM MIKANN " );
            strSql . AppendFormat ( " WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单头信息
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . AssNewWorkHeaderEntity getHeader ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANW001,ANW010,ANW011,ANW012,ANW013,ANW014,ANW015,ANW016,ANW017,ANW018,ANW019,ANW020,ANW021,ANW022,CONVERT(FLOAT,ANW023) ANW023,CONVERT(FLOAT,ANW024) ANW024,ANW025,CONVERT(FLOAT,ANW026) ANW026,CONVERT(FLOAT,ANW027) ANW027 FROM MIKANW WHERE ANW001='{0}' " ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
                return getHeader ( table . Rows [ 0 ] );

        }
        
        public LineProductMesEntityu . AssNewWorkHeaderEntity getHeader ( DataRow row )
        {
            LineProductMesEntityu . AssNewWorkHeaderEntity model = new LineProductMesEntityu . AssNewWorkHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "ANW001" ] != null )
                {
                    model . ANW001 = row [ "ANW001" ] . ToString ( );
                }
                
                if ( row [ "ANW010" ] != null )
                {
                    model . ANW010 = row [ "ANW010" ] . ToString ( );
                }
                if ( row [ "ANW011" ] != null )
                {
                    model . ANW011 = row [ "ANW011" ] . ToString ( );
                }
                if ( row [ "ANW012" ] != null )
                {
                    model . ANW012 = row [ "ANW012" ] . ToString ( );
                }
                if ( row [ "ANW013" ] != null )
                {
                    model . ANW013 = row [ "ANW013" ] . ToString ( );
                }
                if ( row [ "ANW014" ] != null )
                {
                    model . ANW014 = row [ "ANW014" ] . ToString ( );
                }
                if ( row [ "ANW015" ] != null && row [ "ANW015" ] . ToString ( ) != "" )
                {
                    model . ANW015 = Convert . ToDateTime ( row [ "ANW015" ] . ToString ( ) );
                }
                if ( row [ "ANW016" ] != null && row [ "ANW016" ] . ToString ( ) != "" )
                {
                    model . ANW016 = Convert . ToDateTime ( row [ "ANW016" ] . ToString ( ) );
                }

                if ( row [ "ANW017" ] != null )
                {
                    model . ANW017 = row [ "ANW017" ] . ToString ( );
                }
                if ( row [ "ANW018" ] != null )
                {
                    model . ANW018 = row [ "ANW018" ] . ToString ( );
                }
                if ( row [ "ANW019" ] != null )
                {
                    model . ANW019 = row [ "ANW019" ] . ToString ( );
                }
                if ( row [ "ANW020" ] != null && row [ "ANW020" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "ANW020" ] . ToString ( ) == "1" ) || ( row [ "ANW020" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . ANW020 = true;
                    }
                    else
                    {
                        model . ANW020 = false;
                    }
                }
                if ( row [ "ANW021" ] != null && row [ "ANW021" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "ANW021" ] . ToString ( ) == "1" ) || ( row [ "ANW021" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . ANW021 = true;
                    }
                    else
                    {
                        model . ANW021 = false;
                    }
                }
                if ( row [ "ANW022" ] != null && row [ "ANW022" ] . ToString ( ) != "" )
                {
                    model . ANW022 = DateTime . Parse ( row [ "ANW022" ] . ToString ( ) );
                }
                if ( row [ "ANW023" ] != null && row [ "ANW023" ] . ToString ( ) != "" )
                {
                    model . ANW023 = decimal . Parse ( row [ "ANW023" ] . ToString ( ) );
                }
                if ( row [ "ANW024" ] != null && row [ "ANW024" ] . ToString ( ) != "" )
                {
                    model . ANW024 = decimal . Parse ( row [ "ANW024" ] . ToString ( ) );
                }
                if ( row [ "ANW026" ] != null && row [ "ANW026" ] . ToString ( ) != "" )
                {
                    model . ANW026 = decimal . Parse ( row [ "ANW026" ] . ToString ( ) );
                }
                if ( row [ "ANW027" ] != null && row [ "ANW027" ] . ToString ( ) != "" )
                {
                    model . ANW027 = decimal . Parse ( row [ "ANW027" ] . ToString ( ) );
                }
                if ( row [ "ANW025" ] != null && row [ "ANW025" ] . ToString ( ) != "" )
                {
                    model . ANW025 = row [ "ANW025" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,CONVERT(FLOAT,DEA050) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 WHERE DEA009 IN ('M','S') AND DEA076='0507' AND RAA020='N'" );
            strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART WHERE ART003='装配' GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0507' AND RAA020='N' AND RAA024='T'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <returns></returns>
        public DataTable getTableColumn ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT ANW001,ANN002,ANN003,ANN004,ANW013 FROM MIKANW A LEFT JOIN MIKANN B ON A.ANW001=B.ANN001 " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewQuery ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT ANW001,ANN002,ANN003,ANN004,ANN007,ANN009,ANW013,ANW022,ANW020,ANW021,ANW014 FROM MIKANW A LEFT JOIN MIKANN B ON A.ANW001=B.ANN001 " );
            strSql . AppendFormat ( "WHERE {0} " ,strWhere );

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
            strSql . AppendFormat ( "DELETE FROM MIKANW WHERE ANW001='{0}'" ,oddNum );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="_header"></param>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . AssNewWorkHeaderEntity _header ,DataTable tableView ,DataTable tableViewTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            //Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            _header . ANW001 = getOddNum ( );
            _header . ANW025 = UserInfoMation . userName;
            AddHeader ( SQLString ,strSql ,_header );
            UserInfoMation . oddNum = _header . ANW001;

            LineProductMesEntityu . AssNewWorkBodyEntity model = new LineProductMesEntityu . AssNewWorkBodyEntity ( );
            foreach ( DataRow row in tableView . Rows )
            {
                model . ANX001 = _header . ANW001;
                model . ANX002 = row [ "ANX002" ] . ToString ( );
                model . ANX003 = row [ "ANX003" ] . ToString ( );
                model . ANX004 = row [ "ANX004" ] . ToString ( );
                if ( string . IsNullOrEmpty ( row [ "ANX005" ] . ToString ( ) ) )
                    model . ANX005 = null;
                else
                    model . ANX005 = Convert . ToDateTime ( row [ "ANX005" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX006" ] . ToString ( ) ) )
                    model . ANX006 = null;
                else
                    model . ANX006 = Convert . ToDateTime ( row [ "ANX006" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX007" ] . ToString ( ) ) )
                    model . ANX007 = null;
                else
                    model . ANX007 = Convert . ToDateTime ( row [ "ANX007" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX008" ] . ToString ( ) ) )
                    model . ANX008 = null;
                else
                    model . ANX008 = Convert . ToDateTime ( row [ "ANX008" ] . ToString ( ) );
                model . ANX009 = string . IsNullOrEmpty ( row [ "ANX009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX009" ] . ToString ( ) );
                model . ANX010 = string . IsNullOrEmpty ( row [ "ANX010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX010" ] . ToString ( ) );
                model . ANX011 = row [ "ANX011" ] . ToString ( );
                model . ANX012 = row [ "ANX012" ] . ToString ( );
                model . ANX013 = row [ "ANX013" ] . ToString ( );
                model . ANX014 = row [ "ANX014" ] . ToString ( );
                model . ANX015 = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] . ToString ( ) );
                model . ANX016 = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] . ToString ( ) );
                model . ANX017 = string . IsNullOrEmpty ( row [ "ANX017" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX017" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX018" ] . ToString ( ) ) )
                    model . ANX018 = null;
                else
                    model . ANX018 = Convert . ToDecimal ( row [ "ANX018" ] . ToString ( ) );
                if ( !string . IsNullOrEmpty ( model . ANX002 ) )
                    AddBody ( SQLString ,strSql ,model );
            }

            LineProductMesEntityu . AssNewWorkBodyAnnEntity bodyOne = new LineProductMesEntityu . AssNewWorkBodyAnnEntity ( );
            bodyOne . ANN001 = _header . ANW001;
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                bodyOne . ANN002 = row [ "ANN002" ] . ToString ( );
                bodyOne . ANN003 = row [ "ANN003" ] . ToString ( );
                bodyOne . ANN004 = row [ "ANN004" ] . ToString ( );
                bodyOne . ANN005 = row [ "ANN005" ] . ToString ( );
                bodyOne . ANN006 = row [ "ANN006" ] . ToString ( );
                bodyOne . ANN007 = string . IsNullOrEmpty ( row [ "ANN007" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANN007" ] );
                bodyOne . ANN008 = string . IsNullOrEmpty ( row [ "ANN008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANN008" ] );
                bodyOne . ANN009 = string . IsNullOrEmpty ( row [ "ANN009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANN009" ] );
                bodyOne . ANN010 = row [ "ANN010" ] . ToString ( );
                if ( !string . IsNullOrEmpty ( bodyOne . ANN002 ) )
                    AddBodyOne ( SQLString ,strSql ,bodyOne );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }
        
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="_header"></param>
        /// <param name="tableView"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . AssNewWorkHeaderEntity _header ,DataTable tableView ,List<string> idxList ,DataTable tableViewTwo ,List<string> idxListOne )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            //Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            EditHeader ( SQLString ,strSql ,_header );

            LineProductMesEntityu . AssNewWorkBodyEntity model = new LineProductMesEntityu . AssNewWorkBodyEntity ( );
            foreach ( DataRow row in tableView . Rows )
            {
                model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                model . ANX001 = _header . ANW001;
                model . ANX002 = row [ "ANX002" ] . ToString ( );
                model . ANX003 = row [ "ANX003" ] . ToString ( );
                model . ANX004 = row [ "ANX004" ] . ToString ( );
                if ( string . IsNullOrEmpty ( row [ "ANX005" ] . ToString ( ) ) )
                    model . ANX005 = null;
                else
                    model . ANX005 = Convert . ToDateTime ( row [ "ANX005" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX006" ] . ToString ( ) ) )
                    model . ANX006 = null;
                else
                    model . ANX006 = Convert . ToDateTime ( row [ "ANX006" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX007" ] . ToString ( ) ) )
                    model . ANX007 = null;
                else
                    model . ANX007 = Convert . ToDateTime ( row [ "ANX007" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "ANX008" ] . ToString ( ) ) )
                    model . ANX008 = null;
                else
                    model . ANX008 = Convert . ToDateTime ( row [ "ANX008" ] . ToString ( ) );
                model . ANX009 = string . IsNullOrEmpty ( row [ "ANX009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX009" ] . ToString ( ) );
                model . ANX010 = string . IsNullOrEmpty ( row [ "ANX010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX010" ] . ToString ( ) );
                model . ANX011 = row [ "ANX011" ] . ToString ( );
                model . ANX012 = row [ "ANX012" ] . ToString ( );
                model . ANX013 = row [ "ANX013" ] . ToString ( );
                model . ANX014 = row [ "ANX014" ] . ToString ( );
                model . ANX015 = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] . ToString ( ) );
                model . ANX016 = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] . ToString ( ) );
                model . ANX017 = string . IsNullOrEmpty ( row [ "ANX017" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX017" ] . ToString ( ) );

                if ( string . IsNullOrEmpty ( row [ "ANX018" ] . ToString ( ) ) )
                    model . ANX018 = null;
                else
                    model . ANX018 = Convert . ToDecimal ( row [ "ANX018" ] . ToString ( ) );

                if ( !string . IsNullOrEmpty ( model . ANX002 ) )
                {
                    if ( model . idx > 0 )
                        EditBody ( SQLString ,strSql ,model );
                    else
                        AddBody ( SQLString ,strSql ,model );
                }
            }

            LineProductMesEntityu . AssNewWorkBodyAnnEntity bodyOne = new LineProductMesEntityu . AssNewWorkBodyAnnEntity ( );
            bodyOne . ANN001 = _header . ANW001;
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] );
                bodyOne . ANN002 = row [ "ANN002" ] . ToString ( );
                bodyOne . ANN003 = row [ "ANN003" ] . ToString ( );
                bodyOne . ANN004 = row [ "ANN004" ] . ToString ( );
                bodyOne . ANN005 = row [ "ANN005" ] . ToString ( );
                bodyOne . ANN006 = row [ "ANN006" ] . ToString ( );
                bodyOne . ANN007 = string . IsNullOrEmpty ( row [ "ANN007" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANN007" ] );
                bodyOne . ANN008 = string . IsNullOrEmpty ( row [ "ANN008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANN008" ] );
                bodyOne . ANN009 = string . IsNullOrEmpty ( row [ "ANN009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANN009" ] );
                bodyOne . ANN010 = row [ "ANN010" ] . ToString ( );
                if ( !string . IsNullOrEmpty ( bodyOne . ANN002 ) )
                {
                    if ( bodyOne . idx > 0 )
                        EditBodyOne ( SQLString ,strSql ,bodyOne );
                    else
                        AddBodyOne ( SQLString ,strSql ,bodyOne );
                }
            }

            foreach ( string s in idxList )
            {
                if ( Convert . ToInt32 ( s ) > 0 )
                    DeleteBody ( SQLString ,strSql ,s );
            }


            foreach ( string s in idxListOne )
            {
                if ( Convert . ToInt32 ( s ) > 0 )
                    DeleteBodyOne ( SQLString ,strSql ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkHeaderEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKANW(" );//ANW002,ANW003,ANW004,ANW005,ANW006,ANW007,ANW008,ANW009,ANW015,ANW016,
            strSql . Append ( "ANW001,ANW010,ANW011,ANW012,ANW013,ANW014,ANW015,ANW016,ANW017,ANW018,ANW019,ANW020,ANW021,ANW022,ANW023,ANW024,ANW025,ANW026,ANW027)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ANW001,@ANW010,@ANW011,@ANW012,@ANW013,@ANW014,@ANW015,@ANW016,@ANW017,@ANW018,@ANW019,@ANW020,@ANW021,@ANW022,@ANW023,@ANW024,@ANW025,@ANW026,@ANW027)" );
            //@ANW002,@ANW003,@ANW004,@ANW005,@ANW006,@ANW007,@ANW008,@ANW009,@ANW015,@ANW016,
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANW001", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW002", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW003", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW004", SqlDbType.NVarChar,50),
                    //new SqlParameter("@ANW005", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW006", SqlDbType.Int,4),
                    //new SqlParameter("@ANW007", SqlDbType.NVarChar,10),
                    //new SqlParameter("@ANW008", SqlDbType.Decimal,9),
                    //new SqlParameter("@ANW009", SqlDbType.Int,4),
                    new SqlParameter("@ANW010", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW011", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW012", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW013", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW014", SqlDbType.NVarChar,10),
                    new SqlParameter("@ANW015", SqlDbType.DateTime),
                    new SqlParameter("@ANW016", SqlDbType.DateTime),
                    new SqlParameter("@ANW017", SqlDbType.NVarChar,100),
                    new SqlParameter("@ANW018", SqlDbType.NVarChar,100),
                    new SqlParameter("@ANW019", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW020", SqlDbType.Bit,1),
                    new SqlParameter("@ANW021", SqlDbType.Bit,1),
                    new SqlParameter("@ANW022", SqlDbType.DateTime),
                    new SqlParameter("@ANW023", SqlDbType.Decimal,9),
                    new SqlParameter("@ANW024", SqlDbType.Decimal,9),
                    new SqlParameter("@ANW025", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW026", SqlDbType.Decimal,9),
                    new SqlParameter("@ANW027", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . ANW001;
            //parameters [ 1 ] . Value = model . ANW002;
            //parameters [ 2 ] . Value = model . ANW003;
            //parameters [ 3 ] . Value = model . ANW004;
            //parameters [ 4 ] . Value = model . ANW005;
            //parameters [ 5 ] . Value = model . ANW006;
            //parameters [ 6 ] . Value = model . ANW007;
            //parameters [ 7 ] . Value = model . ANW008;
            //parameters [ 8 ] . Value = model . ANW009;
            parameters [ 1 ] . Value = model . ANW010;
            parameters [ 2 ] . Value = model . ANW011;
            parameters [ 3 ] . Value = model . ANW012;
            parameters [ 4 ] . Value = model . ANW013;
            parameters [ 5 ] . Value = model . ANW014;
            parameters [ 6 ] . Value = model . ANW015;
            parameters [ 7 ] . Value = model . ANW016;
            parameters [ 8 ] . Value = model . ANW017;
            parameters [ 9 ] . Value = model . ANW018;
            parameters [ 10 ] . Value = model . ANW019;
            parameters [ 11 ] . Value = model . ANW020;
            parameters [ 12 ] . Value = model . ANW021;
            parameters [ 13 ] . Value = model . ANW022;
            parameters [ 14 ] . Value = model . ANW023;
            parameters [ 15 ] . Value = model . ANW024;
            parameters [ 16 ] . Value = model . ANW025;
            parameters [ 17 ] . Value = model . ANW026;
            parameters [ 18 ] . Value = model . ANW027;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBody ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkBodyEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKANX(" );
            strSql . Append ( "ANX001,ANX002,ANX003,ANX004,ANX005,ANX006,ANX007,ANX008,ANX009,ANX010,ANX011,ANX012,ANX013,ANX014,ANX015,ANX016,ANX017,ANX018)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ANX001,@ANX002,@ANX003,@ANX004,@ANX005,@ANX006,@ANX007,@ANX008,@ANX009,@ANX010,@ANX011,@ANX012,@ANX013,@ANX014,@ANX015,@ANX016,@ANX017,@ANX018)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANX001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX005", SqlDbType.DateTime),
                    new SqlParameter("@ANX006", SqlDbType.DateTime),
                    new SqlParameter("@ANX007", SqlDbType.DateTime),
                    new SqlParameter("@ANX008", SqlDbType.DateTime),
                    new SqlParameter("@ANX009", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX010", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX011", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX012", SqlDbType.NVarChar,100),
                    new SqlParameter("@ANX013", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX014", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX015", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX016", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX017", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX018", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . ANX001;
            parameters [ 1 ] . Value = model . ANX002;
            parameters [ 2 ] . Value = model . ANX003;
            parameters [ 3 ] . Value = model . ANX004;
            parameters [ 4 ] . Value = model . ANX005;
            parameters [ 5 ] . Value = model . ANX006;
            parameters [ 6 ] . Value = model . ANX007;
            parameters [ 7 ] . Value = model . ANX008;
            parameters [ 8 ] . Value = model . ANX009;
            parameters [ 9 ] . Value = model . ANX010;
            parameters [ 10 ] . Value = model . ANX011;
            parameters [ 11 ] . Value = model . ANX012;
            parameters [ 12 ] . Value = model . ANX013;
            parameters [ 13 ] . Value = model . ANX014;
            parameters [ 14 ] . Value = model . ANX015;
            parameters [ 15 ] . Value = model . ANX016;
            parameters [ 16 ] . Value = model . ANX017;
            parameters [ 17 ] . Value = model . ANX018;

            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkBodyAnnEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKANN(" );
            strSql . Append ( "ANN001,ANN002,ANN003,ANN004,ANN005,ANN006,ANN007,ANN008,ANN009,ANN010)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ANN001,@ANN002,@ANN003,@ANN004,@ANN005,@ANN006,@ANN007,@ANN008,@ANN009,@ANN010)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANN001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANN002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANN003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANN004", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANN005", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANN006", SqlDbType.NVarChar,5),
                    new SqlParameter("@ANN007", SqlDbType.Int),
                    new SqlParameter("@ANN008", SqlDbType.Decimal,12),
                    new SqlParameter("@ANN009", SqlDbType.Int),
                    new SqlParameter("@ANN010", SqlDbType.NVarChar,100)
            };
            parameters [ 0 ] . Value = model . ANN001;
            parameters [ 1 ] . Value = model . ANN002;
            parameters [ 2 ] . Value = model . ANN003;
            parameters [ 3 ] . Value = model . ANN004;
            parameters [ 4 ] . Value = model . ANN005;
            parameters [ 5 ] . Value = model . ANN006;
            parameters [ 6 ] . Value = model . ANN007;
            parameters [ 7 ] . Value = model . ANN008;
            parameters [ 8 ] . Value = model . ANN009;
            parameters [ 9 ] . Value = model . ANN010;

            SQLString . Add ( strSql ,parameters );
        }
        void EditHeader ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkHeaderEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKANW set " );
            //strSql . Append ( "ANW002=@ANW002," );
            //strSql . Append ( "ANW003=@ANW003," );
            //strSql . Append ( "ANW004=@ANW004," );
            //strSql . Append ( "ANW005=@ANW005," );
            //strSql . Append ( "ANW006=@ANW006," );
            //strSql . Append ( "ANW007=@ANW007," );
            //strSql . Append ( "ANW008=@ANW008," );
            //strSql . Append ( "ANW009=@ANW009," );
            strSql . Append ( "ANW010=@ANW010," );
            strSql . Append ( "ANW011=@ANW011," );
            strSql . Append ( "ANW012=@ANW012," );
            strSql . Append ( "ANW013=@ANW013," );
            strSql . Append ( "ANW014=@ANW014," );
            strSql . Append ( "ANW015=@ANW015," );
            strSql . Append ( "ANW016=@ANW016," );
            strSql . Append ( "ANW017=@ANW017," );
            strSql . Append ( "ANW018=@ANW018," );
            strSql . Append ( "ANW019=@ANW019," );
            strSql . Append ( "ANW020=@ANW020," );
            strSql . Append ( "ANW021=@ANW021," );
            strSql . Append ( "ANW023=@ANW023," );
            strSql . Append ( "ANW024=@ANW024," );
            strSql . Append ( "ANW026=@ANW026," );
            strSql . Append ( "ANW027=@ANW027 " );
            strSql . Append ( " WHERE ANW001=@ANW001" );
            SqlParameter [ ] parameters = {
                    //new SqlParameter("@ANW002", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW003", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW004", SqlDbType.NVarChar,50),
                    //new SqlParameter("@ANW005", SqlDbType.NVarChar,20),
                    //new SqlParameter("@ANW006", SqlDbType.Int,4),
                    //new SqlParameter("@ANW007", SqlDbType.NVarChar,10),
                    //new SqlParameter("@ANW008", SqlDbType.Decimal,9),
                    //new SqlParameter("@ANW009", SqlDbType.Int,4),
                    new SqlParameter("@ANW010", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW011", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW012", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW013", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW014", SqlDbType.NVarChar,10),
                    new SqlParameter("@ANW015", SqlDbType.DateTime),
                    new SqlParameter("@ANW016", SqlDbType.DateTime),
                    new SqlParameter("@ANW017", SqlDbType.NVarChar,100),
                    new SqlParameter("@ANW018", SqlDbType.NVarChar,100),
                    new SqlParameter("@ANW019", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW020", SqlDbType.Bit,1),
                    new SqlParameter("@ANW021", SqlDbType.Bit,1),
                    new SqlParameter("@ANW001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANW023", SqlDbType.Decimal,9),
                    new SqlParameter("@ANW024", SqlDbType.Decimal,9),
                    new SqlParameter("@ANW026", SqlDbType.Decimal,9),
                    new SqlParameter("@ANW027", SqlDbType.Decimal,9)
            };
            //parameters [ 0 ] . Value = model . ANW002;
            //parameters [ 1 ] . Value = model . ANW003;
            //parameters [ 2 ] . Value = model . ANW004;
            //parameters [ 3 ] . Value = model . ANW005;
            //parameters [ 4 ] . Value = model . ANW006;
            //parameters [ 5 ] . Value = model . ANW007;
            //parameters [ 6 ] . Value = model . ANW008;
            //parameters [ 7 ] . Value = model . ANW009;
            parameters [ 0 ] . Value = model . ANW010;
            parameters [ 1 ] . Value = model . ANW011;
            parameters [ 2 ] . Value = model . ANW012;
            parameters [ 3 ] . Value = model . ANW013;
            parameters [ 4 ] . Value = model . ANW014;
            parameters [ 5 ] . Value = model . ANW015;
            parameters [ 6 ] . Value = model . ANW016;
            parameters [ 7 ] . Value = model . ANW017;
            parameters [ 8 ] . Value = model . ANW018;
            parameters [ 9 ] . Value = model . ANW019;
            parameters [ 10 ] . Value = model . ANW020;
            parameters [ 11 ] . Value = model . ANW021;
            parameters [ 12 ] . Value = model . ANW001;
            parameters [ 13 ] . Value = model . ANW023;
            parameters [ 14 ] . Value = model . ANW024;
            parameters [ 15 ] . Value = model . ANW026;
            parameters [ 16 ] . Value = model . ANW027;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBody ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkBodyEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKANX set " );
            strSql . Append ( "ANX002=@ANX002," );
            strSql . Append ( "ANX003=@ANX003," );
            strSql . Append ( "ANX004=@ANX004," );
            strSql . Append ( "ANX005=@ANX005," );
            strSql . Append ( "ANX006=@ANX006," );
            strSql . Append ( "ANX007=@ANX007," );
            strSql . Append ( "ANX008=@ANX008," );
            strSql . Append ( "ANX009=@ANX009," );
            strSql . Append ( "ANX010=@ANX010," );
            strSql . Append ( "ANX011=@ANX011," );
            strSql . Append ( "ANX012=@ANX012," );
            strSql . Append ( "ANX013=@ANX013," );
            strSql . Append ( "ANX014=@ANX014," );
            strSql . Append ( "ANX015=@ANX015," );
            strSql . Append ( "ANX016=@ANX016," );
            strSql . Append ( "ANX017=@ANX017," );
            strSql . Append ( "ANX018=@ANX018 " );
            strSql . Append ( " WHERE idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANX003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX005", SqlDbType.DateTime),
                    new SqlParameter("@ANX006", SqlDbType.DateTime),
                    new SqlParameter("@ANX007", SqlDbType.DateTime),
                    new SqlParameter("@ANX008", SqlDbType.DateTime),
                    new SqlParameter("@ANX009", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX010", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX011", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX012", SqlDbType.NVarChar,100),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@ANX002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX013", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX014", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANX015", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX016", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX017", SqlDbType.Decimal,9),
                    new SqlParameter("@ANX018", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . ANX003;
            parameters [ 1 ] . Value = model . ANX004;
            parameters [ 2 ] . Value = model . ANX005;
            parameters [ 3 ] . Value = model . ANX006;
            parameters [ 4 ] . Value = model . ANX007;
            parameters [ 5 ] . Value = model . ANX008;
            parameters [ 6 ] . Value = model . ANX009;
            parameters [ 7 ] . Value = model . ANX010;
            parameters [ 8 ] . Value = model . ANX011;
            parameters [ 9 ] . Value = model . ANX012;
            parameters [ 10 ] . Value = model . idx;
            parameters [ 11 ] . Value = model . ANX002;
            parameters [ 12 ] . Value = model . ANX013;
            parameters [ 13 ] . Value = model . ANX014;
            parameters [ 14 ] . Value = model . ANX015;
            parameters [ 15 ] . Value = model . ANX016;
            parameters [ 16 ] . Value = model . ANX017;
            parameters [ 17 ] . Value = model . ANX018;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyOne ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkBodyAnnEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKANN set " );
            strSql . Append ( "ANN002=@ANN002," );
            strSql . Append ( "ANN003=@ANN003," );
            strSql . Append ( "ANN004=@ANN004," );
            strSql . Append ( "ANN005=@ANN005," );
            strSql . Append ( "ANN006=@ANN006," );
            strSql . Append ( "ANN007=@ANN007," );
            strSql . Append ( "ANN008=@ANN008," );
            strSql . Append ( "ANN009=@ANN009," );
            strSql . Append ( "ANN010=@ANN010 " );
            strSql . Append ( " WHERE idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANN003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANN004", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANN005", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANN006", SqlDbType.NVarChar,5),
                    new SqlParameter("@ANN007", SqlDbType.Int),
                    new SqlParameter("@ANN008", SqlDbType.Decimal,12),
                    new SqlParameter("@ANN009", SqlDbType.Int),
                    new SqlParameter("@ANN010", SqlDbType.NVarChar,100),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@ANN002", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . ANN003;
            parameters [ 1 ] . Value = model . ANN004;
            parameters [ 2 ] . Value = model . ANN005;
            parameters [ 3 ] . Value = model . ANN006;
            parameters [ 4 ] . Value = model . ANN007;
            parameters [ 5 ] . Value = model . ANN008;
            parameters [ 6 ] . Value = model . ANN009;
            parameters [ 7 ] . Value = model . ANN010;
            parameters [ 8 ] . Value = model . idx;
            parameters [ 9 ] . Value = model . ANN002;
            SQLString . Add ( strSql ,parameters );
        }
        void DeleteBody ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,string idx )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "delete from MIKANX " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = idx;
            SQLString . Add ( strSql ,parameters );
        }
        void DeleteBodyOne ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,string idx )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "delete from MIKANN " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = idx;
            SQLString . Add ( strSql ,parameters );
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
            strSql . Append ( "UPDATE MIKANW SET ANW020=@ANW020 WHERE ANW001=@ANW001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@ANW001",SqlDbType.NVarChar,20),
                new SqlParameter("@ANW020",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;
            SQLString . Add ( strSql ,parameter );
            //if ( result )
            //{
            //    strSql = new StringBuilder ( );
            //    //
            //}
            //回写生产入库单

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
            strSql . Append ( "UPDATE MIKANW SET ANW021=@ANW021 WHERE ANW001=@ANW001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@ANW001",SqlDbType.NVarChar,20),
                new SqlParameter("@ANW021",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 完工数量总和是否少于工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ExistsNum ( LineProductMesEntityu . AssNewWorkHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT SUM(ANW009) ANW009 FROM MIKANW WHERE ANW001!='{0}' AND ANW002='{1}' AND ANW021=0 GROUP BY ANW002" ,model . ANW001 ,model . ANW002 );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return 0;
            else
            {
                int x = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "ANW009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "ANW009" ] . ToString ( ) );
                if ( x + model . ANW009 <= model . ANW006 )
                    return 0;
                else
                    return x;
            }

        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001='0507' ORDER BY DAA001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANW001,ANW011,ANW013,ANW022,GETDATE() dat,ANW025 FROM MIKANW WHERE ANW001='{0}' " ,oddNum );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . AppendFormat ( "SELECT DISTINCT ANW002,ANW003,ANW004,ANW005,ANW007,ANW006,ANW009,DDA003 DEA008 FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 LEFT JOIN TPADEA C ON B.ANW003=C.DEA001 INNER JOIN TPADDA D ON C.DEA008=D.DDA001 WHERE ANW001='{0}'" ,oddNum );
            strSql . AppendFormat ( "SELECT ANN002 ANW002,ANN003 ANW003,ANN004 ANW004,ANN005 ANW005,ANN006 ANW007,SUM(ANN009) ANW006,ANN007-(SELECT SUM(ANN009) FROM MIKANN E WHERE A.ANN002=E.ANN002 AND A.ANN003=E.ANN003) ANW009,DDA003 DEA008 FROM MIKANN A INNER JOIN MIKANW B ON A.ANN001=B.ANW001 LEFT JOIN TPADEA C ON A.ANN003=C.DEA001 INNER JOIN TPADDA D ON C.DEA008=D.DDA001 WHERE ANW001='{0}' GROUP BY ANN002,ANN003,ANN004,ANN005,ANN006,ANN007,DDA003 " ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANW001,ANW015,ANW016,ANW011,ANW013,ANW014,ANW022,ANW023,ANW024,ANW017,CASE ANW014 WHEN '计件' THEN JJ+JS WHEN '计时' THEN JS ELSE 0 END ZGZ,ZGS,CASE ANW014 WHEN '计件' THEN JJ WHEN '计时' THEN 0 ELSE 0 END JJ,JS,BT,CASE ANW014 WHEN '计件' THEN (JJ+JS)*0.05 WHEN '计时' THEN JS*0.05 ELSE 0 END TC FROM MIKANW A INNER JOIN (SELECT ANN001,CONVERT(FLOAT,SUM(ANN008*ANN009)) JJ FROM MIKANN WHERE ANN001='{0}' GROUP BY ANN001) B ON A.ANW001=B.ANN001 INNER JOIN (SELECT ANX001,CONVERT(FLOAT,SUM(ANX016*ANX009)) JS,CONVERT(FLOAT,SUM(ANX010)) BT,CONVERT(FLOAT,SUM(ANX015+ANX016)) ZGS FROM MIKANX WHERE ANX001='{0}' GROUP BY ANX001) C ON A.ANW001=C.ANX001 WHERE ANW001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintFor ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANX002,ANX003,ANX004,ISNULL(SUBSTRING(CONVERT(varchar(100),ANX005, 22),10,5),'') ANX005,ISNULL(SUBSTRING(CONVERT(varchar(100),ANX006, 22),10,5),'') ANX006,ISNULL(SUBSTRING(CONVERT(varchar(100),ANX007, 22),10,5),'') ANX007,ISNULL(SUBSTRING(CONVERT(varchar(100),ANX008, 22),10,5),'') ANX008,CONVERT(FLOAT,ANX009) ANX009,CONVERT(FLOAT,ANX010) ANX010,ANX011,ANX012,ANX013,ANX014,CONVERT(FLOAT,ANX015) ANX015,CONVERT(FLOAT,ANX016) ANX016,CONVERT(FLOAT,ANX017) ANX017,CONVERT(FLOAT,ANX015+ANX016) U3,CONVERT(FLOAT,ANX016*ANX009) U4 FROM MIKANX WHERE ANX001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintFiv ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANN001,ANN002,ANN003,ANN004,ANN005,ANN006,ANN007,CONVERT(FLOAT,ANN008) ANN008,ANN009,ANN010,CONVERT(FLOAT,ANN008*ANN009) ANN FROM MIKANN where ANN001='{0}' " ,oddNum );

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
            strSql . AppendFormat ( "SELECT ANN002,ANN003,ANN007-SUM(ANN009) LEH FROM MIKANN WHERE ANN001!='{0}' AND ANN002='{1}' AND ANN003='{2}' GROUP BY ANN002,ANN003,ANN007" ,oddNum ,orderNum ,proNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
