using LineProductMesEntityu;
using System;
using System . Collections . Generic;
using System . Data;
using System . Data . SqlClient;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;

namespace LineProductMesBll
{
    public class GenerateSGMRCACB
    {
        /// <summary>
        /// 类别
        /// </summary>
        private static string cateGory="84";

        /// <summary>
        /// 来源
        /// </summary>
        private static string source="81";

        /// <summary>
        /// 批号
        /// </summary>
        private static string batchNum="********************";

        /// <summary>
        /// 生成生产入库单
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="table"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Dictionary<object ,object> GenerateSGM ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,string code ,string department)
        {
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            if ( table == null || table . Rows . Count < 1 )
                return null;

            SGMRCA header = new SGMRCA ( );
            header . RCA001 = cateGory;
            header . RCA002 = code;
            header . RCA004 = UserInfoMation . sysTime . ToString ( "yyyyMMdd" );
            header . RCA005 = "DS";
            header . RCA006 = department;
            header . RCA016 = "F";
            header . RCA019 = "F";
            add ( SQLString ,header );

            SGMRCC bodyOne = new SGMRCC ( );
            bodyOne . RCC001 = cateGory;
            bodyOne . RCC002 = code;
            bodyOne . RCC003 = source;
            bodyOne . RCC007 = 0;
            bodyOne . RCC008 = batchNum;
            bodyOne . RCC011 = "000";

            DataTable tableOne;

            SGMRCB bodyTwo = new SGMRCB ( );
            bodyTwo . RCB001 = cateGory;
            bodyTwo . RCB002 = code;
            bodyTwo . RCB019 = batchNum;
            bodyTwo . RCB021 = source;
            bodyTwo . RCB023 = "000";

            int i = 0;

            foreach ( DataRow row in table . Rows )
            {
                i++;
                bodyOne . RCC004 = row [ "ANN002" ] . ToString ( );
                bodyOne . RCC005 = row [ "DDA001" ] . ToString ( );
                bodyOne . RCC006 = string . IsNullOrEmpty ( row [ "ANN009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANN009" ] );
                bodyOne . RCC010 = row [ "ANN003" ] . ToString ( );
                bodyOne . RCC022 = i . ToString ( ) . PadLeft ( 3 ,'0' );
                bodyOne . RCC028 = row [ "ANN005" ] . ToString ( );
                addBodyOne ( SQLString ,bodyOne );

                bodyTwo . RCB022 = bodyOne . RCC004;
                bodyTwo . RCB024 = bodyOne . RCC010;
                //bodyTwo . RCB008 = bodyOne . RCC006;
                bodyTwo . RCB029 = bodyOne . RCC022;

                strSql = new StringBuilder ( );
                //001
                //strSql . AppendFormat ( "SELECT QAB003,DEA002,DEA003,DEA057,DDA001,CONVERT(FLOAT,QAB005/QAB006) QAB FROM SGMQAB INNER JOIN TPADEA ON QAB003=DEA001 INNER JOIN TPADDA ON DEA008=DDA001 WHERE QAB001='{0}'" ,bodyTwo . RCB024 );
                //002
                //strSql . Append ( "WITH CET AS (" );
                //strSql . AppendFormat ( "SELECT QAB003,DEA002,DEA003,DEA057,DDA001 FROM SGMQAB INNER JOIN TPADEA ON QAB003=DEA001 INNER JOIN TPADDA ON DEA008=DDA001 WHERE QAB001='{0}'),CFT AS ( " ,bodyTwo . RCB024 );
                //strSql . AppendFormat ( "SELECT B.RAB003,CASE WHEN RAB=0 THEN 0 ELSE (RAB008-RAB009)/RAB END RAB FROM SGMRAA A INNER JOIN (SELECT RAA001,RAB003,MAX(CASE WHEN RAB007=0 THEN 0 WHEN RAA018=0 THEN 0 ELSE CONVERT(FLOAT,(RAB008-RAB009)/(RAB007/RAA018)) END) RAB FROM SGMRAA A INNER JOIN SGMRAB B ON A.RAA001=B.RAB001 WHERE RAA001='{0}' GROUP BY RAA001,RAB003) B ON A.RAA001=B.RAA001 INNER JOIN SGMRAB C ON B.RAA001=C.RAB001 AND B.RAB003=C.RAB003 WHERE A.RAA001='{0}') SELECT  QAB003,DEA002,DEA003,DEA057,DDA001,RAB QAB FROM CET A INNER JOIN CFT B ON A.QAB003=B.RAB003" ,bodyTwo . RCB022 );
                //003
                strSql . AppendFormat ( "SELECT B.RAB003 QAB003,DEA002,DEA003,DEA057,DDA001,CASE WHEN RAB=0 THEN 0 ELSE (RAB008-RAB009)/RAB END QAB FROM SGMRAA A INNER JOIN (SELECT RAA001,RAB003,MAX(CASE WHEN RAB007=0 THEN 0 WHEN RAA018=0 THEN 0 ELSE CONVERT(FLOAT,(RAB008-RAB009)/(RAB007/RAA018)) END) RAB FROM SGMRAA A INNER JOIN SGMRAB B ON A.RAA001=B.RAB001 WHERE RAA001='{0}' AND RAA015='{1}' GROUP BY RAA001,RAB003) B ON A.RAA001=B.RAA001 INNER JOIN SGMRAB C ON B.RAA001=C.RAB001 AND B.RAB003=C.RAB003 INNER JOIN TPADEA D ON B.RAB003=D.DEA001 INNER JOIN TPADDA F ON DEA008=DDA001 WHERE A.RAA001='{0}'  AND RAA015='{1}'" ,bodyTwo . RCB022 ,bodyTwo . RCB024 );

                tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

                if ( tableOne != null && tableOne . Rows . Count > 0 )
                {
                   int j = 0;
                    foreach ( DataRow r in tableOne . Rows )
                    {
                        j++;
                        bodyTwo . RCB003 = j . ToString ( ) . PadLeft ( 3 ,'0' );
                        bodyTwo . RCB004 = r [ "QAB003" ] . ToString ( );
                        bodyTwo . RCB005 = r [ "DEA002" ] . ToString ( );
                        bodyTwo . RCB006 = r [ "DEA003" ] . ToString ( );
                        bodyTwo . RCB007 = r [ "DDA001" ] . ToString ( );
                        bodyTwo . RCB008 = bodyOne . RCC006 * ( string . IsNullOrEmpty ( r [ "QAB" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "QAB" ] ) );
                        bodyTwo . RCB020 = r [ "DEA057" ] . ToString ( );
                        if ( bodyTwo . RCB008 > 0 )
                            addBodyTwo ( SQLString ,bodyTwo );
                    }
                }
            }

            return SQLString;
        }

        static void add ( Dictionary<object ,object> SQLString ,SGMRCA header )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO SGMRCA (" );
            strSql . Append ( "RCA001,RCA002,RCA004,RCA005,RCA006,RCA016,RCA019) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@RCA001,@RCA002,@RCA004,@RCA005,@RCA006,@RCA016,@RCA019) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@RCA001",SqlDbType.VarChar),
                new SqlParameter("@RCA002",SqlDbType.VarChar),
                new SqlParameter("@RCA004",SqlDbType.VarChar),
                new SqlParameter("@RCA005",SqlDbType.VarChar),
                new SqlParameter("@RCA006",SqlDbType.VarChar),
                new SqlParameter("@RCA016",SqlDbType.VarChar),
                new SqlParameter("@RCA019",SqlDbType.VarChar)
            };
            parameter [ 0 ] . Value = header . RCA001;
            parameter [ 1 ] . Value = header . RCA002;
            parameter [ 2 ] . Value = header . RCA004;
            parameter [ 3 ] . Value = header . RCA005;
            parameter [ 4 ] . Value = header . RCA006;
            parameter [ 5 ] . Value = header . RCA016;
            parameter [ 6 ] . Value = header . RCA019;
            SQLString . Add ( strSql . ToString ( ) ,parameter );
        }

        static void addBodyOne ( Dictionary<object ,object> SQLString ,SGMRCC model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO SGMRCC (" );
            strSql . Append ( "RCC001,RCC002,RCC003,RCC004,RCC005,RCC006,RCC007,RCC008,RCC010,RCC011,RCC022,RCC028) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@RCC001,@RCC002,@RCC003,@RCC004,@RCC005,@RCC006,@RCC007,@RCC008,@RCC010,@RCC011,@RCC022,@RCC028) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@RCC001",SqlDbType.VarChar),
                new SqlParameter("@RCC002",SqlDbType.VarChar),
                new SqlParameter("@RCC003",SqlDbType.VarChar),
                new SqlParameter("@RCC004",SqlDbType.VarChar),
                new SqlParameter("@RCC005",SqlDbType.VarChar),
                new SqlParameter("@RCC006",SqlDbType.Decimal),
                new SqlParameter("@RCC007",SqlDbType.Decimal),
                new SqlParameter("@RCC008",SqlDbType.VarChar),
                new SqlParameter("@RCC010",SqlDbType.VarChar),
                new SqlParameter("@RCC011",SqlDbType.VarChar),
                new SqlParameter("@RCC022",SqlDbType.VarChar),
                new SqlParameter("@RCC028",SqlDbType.VarChar)
            };
            parameter [ 0 ] . Value = model . RCC001;
            parameter [ 1 ] . Value = model . RCC002;
            parameter [ 2 ] . Value = model . RCC003;
            parameter [ 3 ] . Value = model . RCC004;
            parameter [ 4 ] . Value = model . RCC005;
            parameter [ 5 ] . Value = model . RCC006;
            parameter [ 6 ] . Value = model . RCC007;
            parameter [ 7 ] . Value = model . RCC008;
            parameter [ 8 ] . Value = model . RCC010;
            parameter [ 9 ] . Value = model . RCC011;
            parameter [ 10 ] . Value = model . RCC022;
            parameter [ 11 ] . Value = model . RCC028;

            SQLString . Add ( strSql ,parameter );
        }

        static void addBodyTwo ( Dictionary<object ,object> SQLString ,SGMRCB model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO SGMRCB (" );
            strSql . Append ( "RCB001,RCB002,RCB003,RCB004,RCB005,RCB006,RCB007,RCB008,RCB019,RCB020,RCB021,RCB022,RCB023,RCB024,RCB029) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@RCB001,@RCB002,@RCB003,@RCB004,@RCB005,@RCB006,@RCB007,@RCB008,@RCB019,@RCB020,@RCB021,@RCB022,@RCB023,@RCB024,@RCB029) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@RCB001",SqlDbType.VarChar),
                new SqlParameter("@RCB002",SqlDbType.VarChar),
                new SqlParameter("@RCB003",SqlDbType.VarChar),
                new SqlParameter("@RCB004",SqlDbType.VarChar),
                new SqlParameter("@RCB005",SqlDbType.VarChar),
                new SqlParameter("@RCB006",SqlDbType.VarChar),
                new SqlParameter("@RCB007",SqlDbType.VarChar),
                new SqlParameter("@RCB008",SqlDbType.Decimal),
                new SqlParameter("@RCB019",SqlDbType.VarChar),
                new SqlParameter("@RCB020",SqlDbType.VarChar),
                new SqlParameter("@RCB021",SqlDbType.VarChar),
                new SqlParameter("@RCB022",SqlDbType.VarChar),
                new SqlParameter("@RCB023",SqlDbType.VarChar),
                new SqlParameter("@RCB024",SqlDbType.VarChar),
                new SqlParameter("@RCB029",SqlDbType.VarChar)
            };
            parameter [ 0 ] . Value = model . RCB001;
            parameter [ 1 ] . Value = model . RCB002;
            parameter [ 2 ] . Value = model . RCB003;
            parameter [ 3 ] . Value = model . RCB004;
            parameter [ 4 ] . Value = model . RCB005;
            parameter [ 5 ] . Value = model . RCB006;
            parameter [ 6 ] . Value = model . RCB007;
            parameter [ 7 ] . Value = model . RCB008;
            parameter [ 8 ] . Value = model . RCB019;
            parameter [ 9 ] . Value = model . RCB020;
            parameter [ 10 ] . Value = model . RCB021;
            parameter [ 11 ] . Value = model . RCB022;
            parameter [ 12 ] . Value = model . RCB023;
            parameter [ 13 ] . Value = model . RCB024;
            parameter [ 14 ] . Value = model . RCB029;

            SQLString . Add ( strSql ,parameter );
        }

        /// <summary>
        /// 是否已经存在入库单，且已审核
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool Exists ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM SGMRCA WHERE RCA002='{0}'" ,code );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

    }
}
