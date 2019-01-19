using System;
using System . Text;
using StudentMgr;
using System . Data;
using System . Collections . Generic;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class PaintNewspaperDao
    {
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
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            
            //strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0503' AND RAA020='N' AND RAA024='T'" );
            strSql . Append ( "SELECT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART004,0) DEA050,RAA008 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART004 FROM MIKART GROUP BY ART001) C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0503' AND RAA020='N' AND RAA024='T'" );

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
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            return GetCodeUtils . getOddNum ( "MIKPAN" ,"PAN001" );
            //StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT MAX(PAN001) PAN001 FROM MIKPAN" );

            //DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            //if ( table == null || table . Rows . Count < 0 )
            //    return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            //else
            //{
            //    string code = table . Rows [ 0 ] [ "PAN001" ] . ToString ( );
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
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable tableViewOne ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,PAO002,PAO003,PAO004,PAO005,PAO006,PAO007,PAO008,PAO009,PAO010,PAO011,PAO012,PAO013,0 U5,PAO014 FROM MIKPAO WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable tableViewTwo ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,PPA002,PPA003,PPA004,PPA005,PPA006,PPA007,PPA008,PPA009,PPA010,PPA011,PPA012,PPA013,PPA014,PPA015,PPA016 FROM MIKPAP WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取工艺
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable tableArt ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ART002,ART003,ART004 FROM MIKART WHERE ART001='{0}'" ,piNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Delete ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKPAN WHERE PAN001='{0}'" ,oddNum );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Exanmie ( LineProductMesEntityu . PaintNewspaperHeaderEntity  model )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKPAN SET PAN009=@PAN009 WHERE PAN001=@PAN001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@PAN001",SqlDbType.NVarChar,20),
                new SqlParameter("@PAN009",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = model . PAN001;
            parameter [ 1 ] . Value = model . PAN009;
            SQLString . Add ( strSql ,parameter );

            if ( model . PAN009 )
            {
                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT PAO002 ANN002,PAO003 ANN003,PAO008 ANN005,PAO012 ANN009,DDA001 FROM MIKPAO A LEFT JOIN TPADEA B ON A.PAO003=B.DEA001 INNER JOIN TPADDA C ON B.DEA008=C.DDA001 WHERE PAO001='{0}'" ,model . PAN001 );

                GenerateSGMRCACB . GenerateSGM ( SQLString ,strSql ,model . PAN001 ,model . PAN004 );
            }

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
            strSql . Append ( "UPDATE MIKPAN SET PAN010=@PAN010 WHERE PAN001=@PAN001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@PAN001",SqlDbType.NVarChar,20),
                new SqlParameter("@PAN010",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . PaintNewspaperHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            model . PAN001 = getOddNum ( );
            UserInfoMation . oddNum = model . PAN001;
            model . PAN014 = UserInfoMation . userName;
            AddHeader ( SQLString ,model );

            LineProductMesEntityu . PaintNewspaperBodyOneEntity modelOne = new LineProductMesEntityu . PaintNewspaperBodyOneEntity ( );
            modelOne . PAO001 = model . PAN001;
            foreach ( DataRow row in tableViewOne . Rows )
            {
                modelOne . PAO002 = row [ "PAO002" ] . ToString ( );
                modelOne . PAO003 = row [ "PAO003" ] . ToString ( );
                modelOne . PAO004 = row [ "PAO004" ] . ToString ( );
                modelOne . PAO005 = string . IsNullOrEmpty ( row [ "PAO005" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PAO005" ] . ToString ( ) );
                modelOne . PAO006 = string . IsNullOrEmpty ( row [ "PAO006" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PAO006" ] . ToString ( ) );
                modelOne . PAO007 = row [ "PAO007" ] . ToString ( );
                modelOne . PAO008 = row [ "PAO008" ] . ToString ( );
                modelOne . PAO009 = row [ "PAO009" ] . ToString ( );
                modelOne . PAO010 = row [ "PAO010" ] . ToString ( );
                modelOne . PAO011 = string . IsNullOrEmpty ( row [ "PAO011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PAO011" ] . ToString ( ) );
                modelOne . PAO012 = string . IsNullOrEmpty ( row [ "PAO012" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PAO012" ] . ToString ( ) );
                modelOne . PAO013 = row [ "PAO013" ] . ToString ( );
                modelOne . PAO014 = row [ "PAO014" ] . ToString ( );
                AddBodyOne ( SQLString ,modelOne );
            }

            LineProductMesEntityu . PaintNewspaperBodyTwoEntity modelTwo = new LineProductMesEntityu . PaintNewspaperBodyTwoEntity ( );
            modelTwo . PAP001 = model . PAN001;
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                modelTwo . PPA002 = row [ "PPA002" ] . ToString ( );
                modelTwo . PPA003 = row [ "PPA003" ] . ToString ( );
                modelTwo . PPA004 = row [ "PPA004" ] . ToString ( );
                if ( row [ "PPA005" ] == null || row [ "PPA005" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA005 = null;
                else
                    modelTwo . PPA005 = Convert . ToDateTime ( row [ "PPA005" ] . ToString ( ) );
                if ( row [ "PPA006" ] == null || row [ "PPA006" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA006 = null;
                else
                    modelTwo . PPA006 = Convert . ToDateTime ( row [ "PPA006" ] . ToString ( ) );
                if ( row [ "PPA007" ] == null || row [ "PPA007" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA007 = null;
                else
                    modelTwo . PPA007 = Convert . ToDateTime ( row [ "PPA007" ] . ToString ( ) );
                if ( row [ "PPA008" ] == null || row [ "PPA008" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA008 = null;
                else
                    modelTwo . PPA008 = Convert . ToDateTime ( row [ "PPA008" ] . ToString ( ) );
                modelTwo . PPA009 = string . IsNullOrEmpty ( row [ "PPA009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA009" ] . ToString ( ) );
                modelTwo . PPA010 = row [ "PPA010" ] . ToString ( );
                modelTwo . PPA011 = row [ "PPA011" ] . ToString ( );
                modelTwo . PPA012 = string . IsNullOrEmpty ( row [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA012" ] . ToString ( ) );
                modelTwo . PPA013 = string . IsNullOrEmpty ( row [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA013" ] . ToString ( ) );
                modelTwo . PPA014 = string . IsNullOrEmpty ( row [ "PPA014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA014" ] . ToString ( ) );
                modelTwo . PPA015 = row [ "PPA015" ] . ToString ( );
                if ( row [ "PPA016" ] == null || row [ "PPA016" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA016 = null;
                else
                    modelTwo . PPA016 = Convert . ToDecimal ( row [ "PPA016" ] . ToString ( ) );
                AddBodyTwo ( SQLString ,modelTwo );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="idxListOne"></param>
        /// <param name="idxListTwo"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . PaintNewspaperHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo,List<string> idxListOne,List<string> idxListTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            //model . PAN001 = getOddNum ( );
            //UserInfoMation . oddNum = model . PAN001;
            EditHeader ( SQLString ,model );

            LineProductMesEntityu . PaintNewspaperBodyOneEntity modelOne = new LineProductMesEntityu . PaintNewspaperBodyOneEntity ( );
            modelOne . PAO001 = model . PAN001;
            foreach ( DataRow row in tableViewOne . Rows )
            {
                modelOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                modelOne . PAO002 = row [ "PAO002" ] . ToString ( );
                modelOne . PAO003 = row [ "PAO003" ] . ToString ( );
                modelOne . PAO004 = row [ "PAO004" ] . ToString ( );
                modelOne . PAO005 = string . IsNullOrEmpty ( row [ "PAO005" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PAO005" ] . ToString ( ) );
                modelOne . PAO006 = string . IsNullOrEmpty ( row [ "PAO006" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PAO006" ] . ToString ( ) );
                modelOne . PAO007 = row [ "PAO007" ] . ToString ( );
                modelOne . PAO008 = row [ "PAO008" ] . ToString ( );
                modelOne . PAO009 = row [ "PAO009" ] . ToString ( );
                modelOne . PAO010 = row [ "PAO010" ] . ToString ( );
                modelOne . PAO011 = string . IsNullOrEmpty ( row [ "PAO011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PAO011" ] . ToString ( ) );
                modelOne . PAO012 = string . IsNullOrEmpty ( row [ "PAO012" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PAO012" ] . ToString ( ) );
                modelOne . PAO013 = row [ "PAO013" ] . ToString ( );
                modelOne . PAO014 = row [ "PAO014" ] . ToString ( );
                if ( modelOne . idx < 1 )
                    AddBodyOne ( SQLString ,modelOne );
                else
                    EditBodyOne ( SQLString ,modelOne );
            }

            foreach ( string s in idxListOne )
            {
                DeleteOne ( SQLString ,s );
            }

            LineProductMesEntityu . PaintNewspaperBodyTwoEntity modelTwo = new LineProductMesEntityu . PaintNewspaperBodyTwoEntity ( );
            modelTwo . PAP001 = model . PAN001;
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                modelTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                modelTwo . PPA002 = row [ "PPA002" ] . ToString ( );
                modelTwo . PPA003 = row [ "PPA003" ] . ToString ( );
                modelTwo . PPA004 = row [ "PPA004" ] . ToString ( );
                if ( row [ "PPA005" ] == null || row [ "PPA005" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA005 = null;
                else
                    modelTwo . PPA005 = Convert . ToDateTime ( row [ "PPA005" ] . ToString ( ) );
                if ( row [ "PPA006" ] == null || row [ "PPA006" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA006 = null;
                else
                    modelTwo . PPA006 = Convert . ToDateTime ( row [ "PPA006" ] . ToString ( ) );
                if ( row [ "PPA007" ] == null || row [ "PPA007" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA007 = null;
                else
                    modelTwo . PPA007 = Convert . ToDateTime ( row [ "PPA007" ] . ToString ( ) );
                if ( row [ "PPA008" ] == null || row [ "PPA008" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA008 = null;
                else
                    modelTwo . PPA008 = Convert . ToDateTime ( row [ "PPA008" ] . ToString ( ) );
                modelTwo . PPA009 = string . IsNullOrEmpty ( row [ "PPA009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA009" ] . ToString ( ) );
                modelTwo . PPA010 = row [ "PPA010" ] . ToString ( );
                modelTwo . PPA011 = row [ "PPA011" ] . ToString ( );
                modelTwo . PPA012 = string . IsNullOrEmpty ( row [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA012" ] . ToString ( ) );
                modelTwo . PPA013 = string . IsNullOrEmpty ( row [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA013" ] . ToString ( ) );
                modelTwo . PPA014 = string . IsNullOrEmpty ( row [ "PPA014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA014" ] . ToString ( ) );
                modelTwo . PPA015 = row [ "PPA015" ] . ToString ( );
                if ( row [ "PPA016" ] == null || row [ "PPA016" ] . ToString ( ) == string . Empty )
                    modelTwo . PPA016 = null;
                else
                    modelTwo . PPA016 = Convert . ToDecimal ( row [ "PPA016" ] . ToString ( ) );
                if ( modelTwo . idx < 1 )
                    AddBodyTwo ( SQLString ,modelTwo );
                else
                    EditBodyTwo ( SQLString ,modelTwo );
            }

            foreach ( string s in idxListTwo )
            {
                DeleteTwo ( SQLString ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . PaintNewspaperHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPAN(" );
            strSql . Append ( "PAN001,PAN002,PAN003,PAN004,PAN005,PAN006,PAN007,PAN008,PAN009,PAN010,PAN011,PAN012,PAN013,PAN014,PAN015,PAN016,PAN017,PAN018)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PAN001,@PAN002,@PAN003,@PAN004,@PAN005,@PAN006,@PAN007,@PAN008,@PAN009,@PAN010,@PAN011,@PAN012,@PAN013,@PAN014,@PAN015,@PAN016,@PAN017,@PAN018)" );
            SqlParameter [ ] parameters = {
                        new SqlParameter("@PAN001", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN002", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN003", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN004", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN005", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN006", SqlDbType.Date,3),
                        new SqlParameter("@PAN007", SqlDbType.NVarChar,100),
                        new SqlParameter("@PAN008", SqlDbType.NVarChar,100),
                        new SqlParameter("@PAN009", SqlDbType.Bit,1),
                        new SqlParameter("@PAN010", SqlDbType.Bit,1),
                        new SqlParameter("@PAN011", SqlDbType.Decimal),
                        new SqlParameter("@PAN012", SqlDbType.Decimal),
                        new SqlParameter("@PAN013", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN014", SqlDbType.NVarChar,20),
                        new SqlParameter("@PAN015", SqlDbType.DateTime),
                        new SqlParameter("@PAN016", SqlDbType.DateTime),
                        new SqlParameter("@PAN017", SqlDbType.Decimal),
                        new SqlParameter("@PAN018", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . PAN001;
            parameters [ 1 ] . Value = model . PAN002;
            parameters [ 2 ] . Value = model . PAN003;
            parameters [ 3 ] . Value = model . PAN004;
            parameters [ 4 ] . Value = model . PAN005;
            parameters [ 5 ] . Value = model . PAN006;
            parameters [ 6 ] . Value = model . PAN007;
            parameters [ 7 ] . Value = model . PAN008;
            parameters [ 8 ] . Value = model . PAN009;
            parameters [ 9 ] . Value = model . PAN010;
            parameters [ 10 ] . Value = model . PAN011;
            parameters [ 11 ] . Value = model . PAN012;
            parameters [ 12 ] . Value = model . PAN013;
            parameters [ 13 ] . Value = model . PAN014;
            parameters [ 14 ] . Value = model . PAN015;
            parameters [ 15 ] . Value = model . PAN016;
            parameters [ 16 ] . Value = model . PAN017;
            parameters [ 17 ] . Value = model . PAN018;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . PaintNewspaperBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPAO(" );
            strSql . Append ( "PAO001,PAO002,PAO003,PAO004,PAO005,PAO006,PAO007,PAO008,PAO009,PAO010,PAO011,PAO012,PAO013,PAO014)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PAO001,@PAO002,@PAO003,@PAO004,@PAO005,@PAO006,@PAO007,@PAO008,@PAO009,@PAO010,@PAO011,@PAO012,@PAO013,@PAO014)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PAO001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO004", SqlDbType.NVarChar,50),
                    new SqlParameter("@PAO005", SqlDbType.Int,4),
                    new SqlParameter("@PAO006", SqlDbType.Decimal,9),
                    new SqlParameter("@PAO007", SqlDbType.NVarChar,5),
                    new SqlParameter("@PAO008", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO009", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO010", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO011", SqlDbType.Decimal,9),
                    new SqlParameter("@PAO012", SqlDbType.Int,4),
                    new SqlParameter("@PAO013", SqlDbType.NVarChar,10),
                    new SqlParameter("@PAO014", SqlDbType.NVarChar,100)
            };
            parameters [ 0 ] . Value = model . PAO001;
            parameters [ 1 ] . Value = model . PAO002;
            parameters [ 2 ] . Value = model . PAO003;
            parameters [ 3 ] . Value = model . PAO004;
            parameters [ 4 ] . Value = model . PAO005;
            parameters [ 5 ] . Value = model . PAO006;
            parameters [ 6 ] . Value = model . PAO007;
            parameters [ 7 ] . Value = model . PAO008;
            parameters [ 8 ] . Value = model . PAO009;
            parameters [ 9 ] . Value = model . PAO010;
            parameters [ 10 ] . Value = model . PAO011;
            parameters [ 11 ] . Value = model . PAO012;
            parameters [ 12 ] . Value = model . PAO013;
            parameters [ 13 ] . Value = model . PAO014;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . PaintNewspaperBodyTwoEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPAP(" );
            strSql . Append ( "PAP001,PPA002,PPA003,PPA004,PPA005,PPA006,PPA007,PPA008,PPA009,PPA010,PPA011,PPA012,PPA013,PPA014,PPA015,PPA016)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PAP001,@PPA002,@PPA003,@PPA004,@PPA005,@PPA006,@PPA007,@PPA008,@PPA009,@PPA010,@PPA011,@PPA012,@PPA013,@PPA014,@PPA015,@PPA016)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PAP001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA004", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA005", SqlDbType.DateTime),
                    new SqlParameter("@PPA006", SqlDbType.DateTime),
                    new SqlParameter("@PPA007", SqlDbType.DateTime),
                    new SqlParameter("@PPA008", SqlDbType.DateTime),
                    new SqlParameter("@PPA009", SqlDbType.Decimal,9),
                    new SqlParameter("@PPA010", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA011", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA012", SqlDbType.Decimal),
                    new SqlParameter("@PPA013", SqlDbType.Decimal),
                    new SqlParameter("@PPA014", SqlDbType.Decimal),
                    new SqlParameter("@PPA015", SqlDbType.NVarChar,100),
                    new SqlParameter("@PPA016", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . PAP001;
            parameters [ 1 ] . Value = model . PPA002;
            parameters [ 2 ] . Value = model . PPA003;
            parameters [ 3 ] . Value = model . PPA004;
            parameters [ 4 ] . Value = model . PPA005;
            parameters [ 5 ] . Value = model . PPA006;
            parameters [ 6 ] . Value = model . PPA007;
            parameters [ 7 ] . Value = model . PPA008;
            parameters [ 8 ] . Value = model . PPA009;
            parameters [ 9 ] . Value = model . PPA010;
            parameters [ 10 ] . Value = model . PPA011;
            parameters [ 11 ] . Value = model . PPA012;
            parameters [ 12 ] . Value = model . PPA013;
            parameters [ 13 ] . Value = model . PPA014;
            parameters [ 14 ] . Value = model . PPA015;
            parameters [ 15 ] . Value = model . PPA016;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . PaintNewspaperHeaderEntity model  )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPAN set " );
            strSql . Append ( "PAN002=@PAN002," );
            strSql . Append ( "PAN003=@PAN003," );
            strSql . Append ( "PAN004=@PAN004," );
            strSql . Append ( "PAN005=@PAN005," );
            strSql . Append ( "PAN006=@PAN006," );
            strSql . Append ( "PAN007=@PAN007," );
            strSql . Append ( "PAN008=@PAN008," );
            strSql . Append ( "PAN009=@PAN009," );
            strSql . Append ( "PAN010=@PAN010," );
            strSql . Append ( "PAN011=@PAN011," );
            strSql . Append ( "PAN012=@PAN012," );
            strSql . Append ( "PAN013=@PAN013," );
            strSql . Append ( "PAN015=@PAN015," );
            strSql . Append ( "PAN016=@PAN016," );
            strSql . Append ( "PAN017=@PAN017," );
            strSql . Append ( "PAN018=@PAN018 " );
            strSql . Append ( " where PAN001=@PAN001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PAN002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAN003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAN004", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAN005", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAN006", SqlDbType.Date,3),
                    new SqlParameter("@PAN007", SqlDbType.NVarChar,100),
                    new SqlParameter("@PAN008", SqlDbType.NVarChar,100),
                    new SqlParameter("@PAN009", SqlDbType.Bit,1),
                    new SqlParameter("@PAN010", SqlDbType.Bit,1),
                    new SqlParameter("@PAN001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAN011", SqlDbType.Decimal),
                    new SqlParameter("@PAN012", SqlDbType.Decimal),
                    new SqlParameter("@PAN013", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAN015", SqlDbType.DateTime),
                    new SqlParameter("@PAN016", SqlDbType.DateTime),
                    new SqlParameter("@PAN017", SqlDbType.Decimal),
                    new SqlParameter("@PAN018", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . PAN002;
            parameters [ 1 ] . Value = model . PAN003;
            parameters [ 2 ] . Value = model . PAN004;
            parameters [ 3 ] . Value = model . PAN005;
            parameters [ 4 ] . Value = model . PAN006;
            parameters [ 5 ] . Value = model . PAN007;
            parameters [ 6 ] . Value = model . PAN008;
            parameters [ 7 ] . Value = model . PAN009;
            parameters [ 8 ] . Value = model . PAN010;
            parameters [ 9 ] . Value = model . PAN001;
            parameters [ 10 ] . Value = model . PAN011;
            parameters [ 11 ] . Value = model . PAN012;
            parameters [ 12 ] . Value = model . PAN013;
            parameters [ 13 ] . Value = model . PAN015;
            parameters [ 14 ] . Value = model . PAN016;
            parameters [ 15 ] . Value = model . PAN017;
            parameters [ 16 ] . Value = model . PAN018;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . PaintNewspaperBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPAO set " );
            strSql . Append ( "PAO002=@PAO002," );
            strSql . Append ( "PAO003=@PAO003," );
            strSql . Append ( "PAO004=@PAO004," );
            strSql . Append ( "PAO005=@PAO005," );
            strSql . Append ( "PAO006=@PAO006," );
            strSql . Append ( "PAO007=@PAO007," );
            strSql . Append ( "PAO008=@PAO008," );
            strSql . Append ( "PAO009=@PAO009," );
            strSql . Append ( "PAO010=@PAO010," );
            strSql . Append ( "PAO011=@PAO011," );
            strSql . Append ( "PAO012=@PAO012," );
            strSql . Append ( "PAO013=@PAO013," );
            strSql . Append ( "PAO014=@PAO014 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PAO003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO004", SqlDbType.NVarChar,50),
                    new SqlParameter("@PAO005", SqlDbType.Int,4),
                    new SqlParameter("@PAO006", SqlDbType.Decimal,9),
                    new SqlParameter("@PAO007", SqlDbType.NVarChar,5),
                    new SqlParameter("@PAO008", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO009", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO010", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO011", SqlDbType.Decimal,9),
                    new SqlParameter("@PAO012", SqlDbType.Int,4),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@PAO002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PAO013", SqlDbType.NVarChar,10),
                    new SqlParameter("@PAO014", SqlDbType.NVarChar,100)
            };
            parameters [ 0 ] . Value = model . PAO003;
            parameters [ 1 ] . Value = model . PAO004;
            parameters [ 2 ] . Value = model . PAO005;
            parameters [ 3 ] . Value = model . PAO006;
            parameters [ 4 ] . Value = model . PAO007;
            parameters [ 5 ] . Value = model . PAO008;
            parameters [ 6 ] . Value = model . PAO009;
            parameters [ 7 ] . Value = model . PAO010;
            parameters [ 8 ] . Value = model . PAO011;
            parameters [ 9 ] . Value = model . PAO012;
            parameters [ 10 ] . Value = model . idx;
            parameters [ 11 ] . Value = model . PAO002;
            parameters [ 12 ] . Value = model . PAO013;
            parameters [ 13 ] . Value = model . PAO014;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . PaintNewspaperBodyTwoEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPAP set " );
            strSql . Append ( "PPA002=@PPA002," );
            strSql . Append ( "PPA003=@PPA003," );
            strSql . Append ( "PPA004=@PPA004," );
            strSql . Append ( "PPA005=@PPA005," );
            strSql . Append ( "PPA006=@PPA006," );
            strSql . Append ( "PPA007=@PPA007," );
            strSql . Append ( "PPA008=@PPA008," );
            strSql . Append ( "PPA009=@PPA009," );
            strSql . Append ( "PPA010=@PPA010," );
            strSql . Append ( "PPA011=@PPA011," );
            strSql . Append ( "PPA012=@PPA012," );
            strSql . Append ( "PPA013=@PPA013," );
            strSql . Append ( "PPA014=@PPA014," );
            strSql . Append ( "PPA015=@PPA015," );
            strSql . Append ( "PPA016=@PPA016" );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PPA003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA004", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA005", SqlDbType.DateTime),
                    new SqlParameter("@PPA006", SqlDbType.DateTime),
                    new SqlParameter("@PPA007", SqlDbType.DateTime),
                    new SqlParameter("@PPA008", SqlDbType.DateTime),
                    new SqlParameter("@PPA009", SqlDbType.Decimal,9),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@PPA002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA010", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA011", SqlDbType.NVarChar,20),
                    new SqlParameter("@PPA012", SqlDbType.Decimal),
                    new SqlParameter("@PPA013", SqlDbType.Decimal),
                    new SqlParameter("@PPA014", SqlDbType.Decimal),
                    new SqlParameter("@PPA015", SqlDbType.NVarChar,100),
                    new SqlParameter("@PPA016", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . PPA003;
            parameters [ 1 ] . Value = model . PPA004;
            parameters [ 2 ] . Value = model . PPA005;
            parameters [ 3 ] . Value = model . PPA006;
            parameters [ 4 ] . Value = model . PPA007;
            parameters [ 5 ] . Value = model . PPA008;
            parameters [ 6 ] . Value = model . PPA009;
            parameters [ 7 ] . Value = model . idx;
            parameters [ 8 ] . Value = model . PPA002;
            parameters [ 9 ] . Value = model . PPA010;
            parameters [ 10 ] . Value = model . PPA011;
            parameters [ 11 ] . Value = model . PPA012;
            parameters [ 12 ] . Value = model . PPA013;
            parameters [ 13 ] . Value = model . PPA014;
            parameters [ 14 ] . Value = model . PPA015;
            parameters [ 15 ] . Value = model . PPA016;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteOne ( Dictionary<object ,object> SQLString ,string s)
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKPAO WHERE idx={0}" ,s );

            SQLString . Add ( strSql ,null );
        }
        void DeleteTwo ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKPAP WHERE idx={0}" ,s );

            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取查询数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable tableViewQuery ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PAN001,PAN010,PAN006,PAN009,PAN010,PAO002,PAO003,PAO004,PAO005,PAO012,PAN013 FROM MIKPAN A LEFT JOIN MIKPAO B ON A.PAN001=B.PAO001 WHERE {0}" ,strWhere );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . PaintNewspaperHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "select idx,PAN001,PAN002,PAN003,PAN004,PAN005,PAN006,PAN007,PAN008,PAN009,PAN010,PAN011,PAN012,PAN013,PAN014,PAN015,PAN016,CONVERT(FLOAT,PAN017) PAN017,CONVERT(FLOAT,PAN018) PAN018 from MIKPAN " );
            strSql . Append ( " where PAN001=@PAN001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PAN001", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = oddNum;

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) ,parameters );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
                return getModel ( table . Rows [ 0 ] );
        }

        public LineProductMesEntityu . PaintNewspaperHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . PaintNewspaperHeaderEntity model = new LineProductMesEntityu . PaintNewspaperHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "idx" ] != null && row [ "idx" ] . ToString ( ) != "" )
                {
                    model . idx = int . Parse ( row [ "idx" ] . ToString ( ) );
                }
                if ( row [ "PAN001" ] != null )
                {
                    model . PAN001 = row [ "PAN001" ] . ToString ( );
                }
                if ( row [ "PAN002" ] != null )
                {
                    model . PAN002 = row [ "PAN002" ] . ToString ( );
                }
                if ( row [ "PAN003" ] != null )
                {
                    model . PAN003 = row [ "PAN003" ] . ToString ( );
                }
                if ( row [ "PAN004" ] != null )
                {
                    model . PAN004 = row [ "PAN004" ] . ToString ( );
                }
                if ( row [ "PAN005" ] != null )
                {
                    model . PAN005 = row [ "PAN005" ] . ToString ( );
                }
                if ( row [ "PAN006" ] != null && row [ "PAN006" ] . ToString ( ) != "" )
                {
                    model . PAN006 = DateTime . Parse ( row [ "PAN006" ] . ToString ( ) );
                }
                if ( row [ "PAN007" ] != null )
                {
                    model . PAN007 = row [ "PAN007" ] . ToString ( );
                }
                if ( row [ "PAN008" ] != null )
                {
                    model . PAN008 = row [ "PAN008" ] . ToString ( );
                }
                if ( row [ "PAN009" ] != null && row [ "PAN009" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "PAN009" ] . ToString ( ) == "1" ) || ( row [ "PAN009" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . PAN009 = true;
                    }
                    else
                    {
                        model . PAN009 = false;
                    }
                }
                if ( row [ "PAN010" ] != null && row [ "PAN010" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "PAN010" ] . ToString ( ) == "1" ) || ( row [ "PAN010" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . PAN010 = true;
                    }
                    else
                    {
                        model . PAN010 = false;
                    }
                }
                if ( row [ "PAN011" ] != null && row [ "PAN011" ] . ToString ( ) != "" )
                {
                    model . PAN011 = decimal . Parse ( row [ "PAN011" ] . ToString ( ) );
                }
                if ( row [ "PAN012" ] != null && row [ "PAN012" ] . ToString ( ) != "" )
                {
                    model . PAN012 = decimal . Parse ( row [ "PAN012" ] . ToString ( ) );
                }
                if ( row [ "PAN013" ] != null && row [ "PAN013" ] . ToString ( ) != "" )
                {
                    model . PAN013 = row [ "PAN013" ] . ToString ( );
                }
                if ( row [ "PAN014" ] != null && row [ "PAN014" ] . ToString ( ) != "" )
                {
                    model . PAN014 = row [ "PAN014" ] . ToString ( );
                }
                if ( row [ "PAN015" ] != null && row [ "PAN015" ] . ToString ( ) != "" )
                {
                    model . PAN015 = DateTime . Parse ( row [ "PAN015" ] . ToString ( ) );
                }
                if ( row [ "PAN016" ] != null && row [ "PAN016" ] . ToString ( ) != "" )
                {
                    model . PAN016 = DateTime . Parse ( row [ "PAN016" ] . ToString ( ) );
                }
                if ( row [ "PAN017" ] != null && row [ "PAN017" ] . ToString ( ) != "" )
                {
                    model . PAN017 = decimal . Parse ( row [ "PAN017" ] . ToString ( ) );
                }
                if ( row [ "PAN018" ] != null && row [ "PAN018" ] . ToString ( ) != "" )
                {
                    model . PAN018 = decimal . Parse ( row [ "PAN018" ] . ToString ( ) );
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
            strSql . AppendFormat ( "SELECT PAN001 ANW001,PAN003 ANW011,PAN005 ANW013,PAN006 ANW022,GETDATE() dat,PAN014 ANW025 FROM MIKPAN WHERE PAN001='{0}'" ,oddNum );

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
            strSql . AppendFormat ( "SELECT PAO002 ANW002,PAO003 ANW003,PAO004 ANW004,PAO008 ANW005,PAO007 ANW007,SUM(PAO012) ANW006,PAO005-(SELECT SUM(PAO012) FROM MIKPAO E WHERE A.PAO002=E.PAO002 AND A.PAO003=E.PAO003) ANW009,DDA003 DEA008 FROM MIKPAO A LEFT JOIN TPADEA B ON A.PAO003=B.DEA001 INNER JOIN TPADDA D ON B.DEA008=D.DDA001 WHERE  PAO012>0 AND PAO001='{0}' GROUP BY PAO002,PAO003,PAO004,PAO008,PAO007,PAO005,DDA003" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableArt ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ART011,ART002,ART003,ART004 FROM MIKARS A INNER JOIN MIKART B ON A.ARS001=B.ART001 WHERE ART001='{0}'" ,piNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        public DataTable getTableArtForAll ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ART003+':'+CONVERT(NVARCHAR,CONVERT(FLOAT,ART004)) ART FROM MIKART WHERE ART001='{0}' ORDER BY ART011" ,piNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取工艺信息数据
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableA ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ART001,CASE ART002 WHEN '3001' THEN '喷漆工' WHEN '3002' THEN '装配工' ELSE '' END ART013,ART004 FROM MIKART WHERE ART001 IN ({0})" ,piNum );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( LineProductMesEntityu.PaintNewspaperBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . AppendFormat ( "SELECT PAO002,PAO003,PAO013,PAO005-SUM(PAO012) PAO FROM MIKPAO WHERE PAO001!='{0}' AND PAO002='{1}' AND PAO003='{2}' GROUP BY PAO002,PAO003,PAO013,PAO005" ,model . PAO001 ,model . PAO002 ,model . PAO003 );

            strSql . AppendFormat ( "SELECT PAO002,PAO003,PAO005-SUM(PAO012) PAO FROM MIKPAO WHERE PAO001!='{0}' AND PAO002='{1}' AND PAO003='{2}' GROUP BY PAO002,PAO003,PAO005" ,model . PAO001 ,model . PAO002 ,model . PAO003 );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "select PAN001,PAN003,PAN005,PAN006,PAN007,PAN008,PAN011,PAN012,PAN013,PAN015,PAN016,JS,CASE WHEN PAN013='计件' THEN JJ ELSE 0 END JJ,CASE WHEN PAN013='计件' THEN JJ+JS WHEN PAN013='计时' THEN JS ELSE 0 END ZGZ,CASE WHEN PAN013='计件' THEN (JJ+JS)*0.05 WHEN PAN013='计时' THEN JS*0.05 ELSE 0 END TL,ZGS from MIKPAN A INNER JOIN (SELECT PAP001,CONVERT(FLOAT,SUM(PPA009*PPA013)) JS,CONVERT(FLOAT,SUM(PPA012+PPA013)) ZGS FROM MIKPAP WHERE PAP001='{0}' GROUP BY PAP001 ) B ON A.PAN001=B.PAP001 INNER JOIN (SELECT PAO001,CONVERT(FLOAT,SUM(PAO006*PAO012)) JJ FROM MIKPAO WHERE PAO001='{0}' GROUP BY PAO001) C ON A.PAN001=C.PAO001 WHERE PAN001='{0}'" ,oddNum );


            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFor ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PAO001,PAO002,PAO003,PAO004,PAO005,CONVERT(FLOAT,PAO006) PAO006,PAO007,PAO008,PAO010,PAO012,PAO014,CONVERT(FLOAT,PAO006*PAO012) U0 FROM MIKPAO WHERE PAO001 ='{0}'" ,oddNum );


            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFiv ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PPA002,PPA003,DATENAME(HOUR,PPA005)+':'+DATENAME(MINUTE,PPA005) PPA005,DATENAME(HOUR,PPA006)+':'+DATENAME(MINUTE,PPA006) PPA006,DATENAME(HOUR,PPA007)+':'+DATENAME(MINUTE,PPA007) PPA007,DATENAME(HOUR,PPA008)+':'+DATENAME(MINUTE,PPA008) PPA008,CONVERT(FLOAT,PPA009) PPA009,PPA010,PPA011,CONVERT(FLOAT,PPA012) PPA012,CONVERT(FLOAT,PPA013) PPA013,CONVERT(FLOAT,PPA014) PPA014,PPA015,CONVERT(FLOAT,PPA009*PPA013) U3,CONVERT(FLOAT,PPA012+PPA013) U4 FROM MIKPAP WHERE PAP001 ='{0}'" ,oddNum );


            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
