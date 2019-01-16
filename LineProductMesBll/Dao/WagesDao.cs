using System;
using System . Collections . Generic;
using System . Text;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class WagesDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable getTableView ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            string result = getPower ( );
            if ( result == string . Empty )
            {
                if ( UserInfoMation . userName . Equals ( "系统管理员" ) )
                    strSql . AppendFormat ( "SELECT A.idx,WAH001,WAH002,WAH003,WAH023,WAH004,CONVERT(FLOAT,ISNULL(WAH005,0)) WAH005,CONVERT(FLOAT,ISNULL(WAH006,0)) WAH006,CONVERT(FLOAT,ISNULL(WAH007,0)) WAH007,CONVERT(FLOAT,ISNULL(WAH008,0)) WAH008,CONVERT(FLOAT,ISNULL(WAH009,0)) WAH009,CONVERT(FLOAT,ISNULL(WAH010,0)) WAH010,CONVERT(FLOAT,ISNULL(WAH011,0)) WAH011,CONVERT(FLOAT,ISNULL(WAH012,0)) WAH012,CONVERT(FLOAT,ISNULL(CASE WHEN WAH022=0 THEN 0 ELSE WAH013/WAH022*WAH005 END,0)) WAH013,CONVERT(FLOAT,ISNULL(WAH014,0)) WAH014,CONVERT(FLOAT,ISNULL(WAH015,0)) WAH015,CONVERT(FLOAT,ISNULL(WAH016,0)) WAH016,CONVERT(FLOAT,ISNULL(WAH017,0)) WAH017,CONVERT(FLOAT,ISNULL(WAH018,0)) WAH018,CONVERT(FLOAT,ISNULL(WAH019,0)) WAH019,CONVERT(FLOAT,ISNULL(CASE WHEN WAH022=0 THEN 0 ELSE 200/WAH022*WAH005 END,0)) WAH020,CONVERT(FLOAT,ISNULL((SELECT SUM(CASE WHEN WAH022=0 THEN 0 ELSE 200/WAH022*WAH005 END) FROM MIKWAH A INNER JOIN MIKWAG B ON A.WAH001=B.WAG001 WHERE WAG002 LIKE '2018%' AND YEAR(B.WAG002)=YEAR(C.WAG002) AND MONTH(B.WAG002)<MONTH(C.WAG002)),0)) WAH021,CONVERT(FLOAT,ISNULL(WAH022,0)) WAH022,CONVERT(FLOAT,ISNULL(WAH024,0)) WAH024 FROM MIKWAH A INNER JOIN MIKWAG C ON A.WAH001=C.WAG001 WHERE WAH001='{0}'" ,code );
                else
                    return null;
            }
            else
                strSql . AppendFormat ( "SELECT A.idx,WAH001,WAH002,WAH003,WAH023,WAH004,CONVERT(FLOAT,ISNULL(WAH005,0)) WAH005,CONVERT(FLOAT,ISNULL(WAH006,0)) WAH006,CONVERT(FLOAT,ISNULL(WAH007,0)) WAH007,CONVERT(FLOAT,ISNULL(WAH008,0)) WAH008,CONVERT(FLOAT,ISNULL(WAH009,0)) WAH009,CONVERT(FLOAT,ISNULL(WAH010,0)) WAH010,CONVERT(FLOAT,ISNULL(WAH011,0)) WAH011,CONVERT(FLOAT,ISNULL(WAH012,0)) WAH012,CONVERT(FLOAT,ISNULL(CASE WHEN WAH022=0 THEN 0 ELSE WAH013/WAH022*WAH005 END,0)) WAH013,CONVERT(FLOAT,ISNULL(WAH014,0)) WAH014,CONVERT(FLOAT,ISNULL(WAH015,0)) WAH015,CONVERT(FLOAT,ISNULL(WAH016,0)) WAH016,CONVERT(FLOAT,ISNULL(WAH017,0)) WAH017,CONVERT(FLOAT,ISNULL(WAH018,0)) WAH018,CONVERT(FLOAT,ISNULL(WAH019,0)) WAH019,CONVERT(FLOAT,ISNULL(CASE WHEN WAH022=0 THEN 0 ELSE 200/WAH022*WAH005 END,0)) WAH020,CONVERT(FLOAT,ISNULL((SELECT SUM(CASE WHEN WAH022=0 THEN 0 ELSE 200/WAH022*WAH005 END) FROM MIKWAH A INNER JOIN MIKWAG B ON A.WAH001=B.WAG001 WHERE WAG002 LIKE '2018%' AND YEAR(B.WAG002)=YEAR(C.WAG002) AND MONTH(B.WAG002)<MONTH(C.WAG002)),0)) WAH021,CONVERT(FLOAT,ISNULL(WAH022,0)) WAH022,CONVERT(FLOAT,ISNULL(WAH024,0)) WAH024 FROM MIKWAH A INNER JOIN MIKWAG C ON A.WAH001=C.WAG001 WHERE WAH001='{0}' AND WAH023 IN ({1})" ,code ,result );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取读取数据权限
        /// </summary>
        /// <returns></returns>
        string getPower ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "select EMP038 from MIKEMP where EMP001='{0}'" ,UserInfoMation . userNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null && table . Rows . Count < 1 )
                return string . Empty;
            string result = string . Empty;
            DataRow row = table . Rows [ 0 ];
            if ( row [ "EMP038" ] != null && row [ "EMP038" ] . ToString ( ) != string . Empty )
            {
                string [ ] str = row [ "EMP038" ] . ToString ( ) . Split ( ',' );
                foreach ( string s in str )
                {
                    if ( string . IsNullOrEmpty ( result ) )
                        result = "'" + s . Trim ( ) + "'";
                    else
                        result = result + "," + "'" + s . Trim ( ) + "'";
                }
            }
            return result;
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getCode ( )
        {
            DateTime dt = UserInfoMation . sysTime;
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(WAG001) WAG001 FROM MIKWAG WHERE WAG001 LIKE '{0}%'" ,dt . ToString ( "yyyyMM" ) );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return dt . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "WAG001" ] . ToString ( );
                if ( code == string . Empty )
                    return dt . ToString ( "yyyyMMdd" ) + "001";
                else
                    return ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool getCode ( string date )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT WAG001 FROM MIKWAG WHERE SUBSTRING( CONVERT(VARCHAR(20),WAG002,112),0,7)='{0}'" ,date );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            if ( table == null || table . Rows . Count < 1 )
                return false;
            else if ( string . IsNullOrEmpty ( table . Rows [ 0 ] [ "WAG001" ] . ToString ( ) ) )
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Delete ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKWAG WHERE WAG001='{0}'" ,code );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 编辑保存数据
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Save ( DataTable tableView ,string code)
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );

            LineProductMesEntityu . WagesBodyEntity model = new LineProductMesEntityu . WagesBodyEntity ( );
            model . WAH001 = code;
            foreach ( DataRow row in tableView . Rows )
            {
                if ( row [ "WAH010" ] == null || row [ "WAH010" ] . ToString ( ) == string . Empty )
                    model . WAH010 = null;
                else
                    model . WAH010 = Convert . ToDecimal ( row [ "WAH010" ] );
                if ( row [ "WAH011" ] == null || row [ "WAH011" ] . ToString ( ) == string . Empty )
                    model . WAH011 = null;
                else
                    model . WAH011 = Convert . ToDecimal ( row [ "WAH011" ] );
                if ( row [ "WAH015" ] == null || row [ "WAH015" ] . ToString ( ) == string . Empty )
                    model . WAH015 = null;
                else
                    model . WAH015 = Convert . ToDecimal ( row [ "WAH015" ] );
                if ( row [ "WAH016" ] == null || row [ "WAH016" ] . ToString ( ) == string . Empty )
                    model . WAH016 = null;
                else
                    model . WAH016 = Convert . ToDecimal ( row [ "WAH016" ] );
                if ( row [ "WAH017" ] == null || row [ "WAH017" ] . ToString ( ) == string . Empty )
                    model . WAH017 = null;
                else
                    model . WAH017 = Convert . ToDecimal ( row [ "WAH017" ] );
                if ( row [ "WAH018" ] == null || row [ "WAH018" ] . ToString ( ) == string . Empty )
                    model . WAH018 = null;
                else
                    model . WAH018 = Convert . ToDecimal ( row [ "WAH018" ] );
                if ( row [ "WAH019" ] == null || row [ "WAH019" ] . ToString ( ) == string . Empty )
                    model . WAH019 = null;
                else
                    model . WAH019 = Convert . ToDecimal ( row [ "WAH019" ] );
                if ( row [ "WAH022" ] == null || row [ "WAH022" ] . ToString ( ) == string . Empty )
                    model . WAH022 = null;
                else
                    model . WAH022 = Convert . ToDecimal ( row [ "WAH022" ] );
                model . idx = Convert . ToInt32 ( row [ "idx" ] );
                EditBody ( SQLString ,model );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void EditBody ( Dictionary<object,object> SQLString,LineProductMesEntityu.WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKWAH SET " );
            strSql . Append ( "WAH010=@WAH010," );
            strSql . Append ( "WAH011=@WAH011," );
            strSql . Append ( "WAH015=@WAH015," );
            strSql . Append ( "WAH016=@WAH016," );
            strSql . Append ( "WAH017=@WAH017," );
            strSql . Append ( "WAH018=@WAH018," );
            strSql . Append ( "WAH019=@WAH019," );
            strSql . Append ( "WAH022=@WAH022 " );
            strSql . Append ( "WHERE idx=@idx" );
            SqlParameter [ ] parametre = {
                new SqlParameter("@WAH010",SqlDbType.Decimal,9),
                new SqlParameter("@WAH011",SqlDbType.Decimal,9),
                new SqlParameter("@WAH015",SqlDbType.Decimal,9),
                new SqlParameter("@WAH016",SqlDbType.Decimal,9),
                new SqlParameter("@WAH017",SqlDbType.Decimal,9),
                new SqlParameter("@WAH018",SqlDbType.Decimal,9),
                new SqlParameter("@WAH019",SqlDbType.Decimal,9),
                new SqlParameter("@WAH022",SqlDbType.Decimal,9),
                new SqlParameter("@idx",SqlDbType.Int,4)
            };
            parametre [ 0 ] . Value = model . WAH010;
            parametre [ 1 ] . Value = model . WAH011;
            parametre [ 2 ] . Value = model . WAH015;
            parametre [ 3 ] . Value = model . WAH016;
            parametre [ 4 ] . Value = model . WAH017;
            parametre [ 5 ] . Value = model . WAH018;
            parametre [ 6 ] . Value = model . WAH019;
            parametre [ 7 ] . Value = model . WAH022;
            parametre [ 8 ] . Value = model . idx;
            SQLString . Add ( strSql ,parametre );
        }

        /// <summary>
        /// 是否存在本年本月的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool ExistsYearMonth ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM MIKWAG WHERE YEAR(WAG002)={0} AND MONTH(WAG002)={1}" ,dt . Year ,dt . Month );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 读取工资
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Read ( DateTime dt ,string code )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            LineProductMesEntityu . WagesHeaderEntity header = new LineProductMesEntityu . WagesHeaderEntity ( );
            header . WAG001 = code;
            header . WAG002 = dt;
            if ( Exists ( code ) == false )
                AddHeader ( SQLString ,header );

            if ( SQLString . Count > 0 )
            {
                if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) == false )
                    return false;
                SQLString . Clear ( );
            }
            else
                SQLString . Clear ( );

            //报工工资
            DataTable table = getTableOne ( dt );
            LineProductMesEntityu . WagesBodyEntity model = new LineProductMesEntityu . WagesBodyEntity ( );
            model . WAH001 = code;

            EditBodyAll ( SQLString ,model );
            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH002 = row [ "ANX002" ] . ToString ( );
                        model . WAH003 = row [ "ANX003" ] . ToString ( );
                        //model . WAH004 = row [ "ANX004" ] . ToString ( );
                        model . WAH006 = string . IsNullOrEmpty ( row [ "AN" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "AN" ] . ToString ( ) );
                        model . WAH008 = string . IsNullOrEmpty ( row [ "ANW" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANW" ] . ToString ( ) );
                        if ( Exists ( model . WAH001 ,model . WAH002 ) )
                            EditOne ( SQLString ,model );
                        else
                            AddBodyOne ( SQLString ,model );
                    }
                }
            }

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                //获取本月员工所属车间或组别
                table = getTableFor ( dt );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH002 = row [ "EMP001" ] . ToString ( );
                        model . WAH023 = row [ "EMP006" ] . ToString ( );
                        model . WAH007 = string . IsNullOrEmpty ( row [ "EMP027" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "EMP027" ] . ToString ( ) );
                        model . WAH013 = string . IsNullOrEmpty ( row [ "EMP029" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "EMP029" ] . ToString ( ) );
                        EditFor ( SQLString ,model );
                    }
                }
            }

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                //本月各车间考勤天数
                table = getTableTwo ( dt );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH022 = string . IsNullOrEmpty ( row [ "CQ" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "CQ" ] . ToString ( ) );
                        model . WAH023 = row [ "CJ" ] . ToString ( );
                        EditTwo ( SQLString ,model );
                    }
                }
            }

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                //本月员工出勤天数
                table = getTableTre ( dt );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH002 = row [ "ANX002" ] . ToString ( );
                        model . WAH005 = string . IsNullOrEmpty ( row [ "CQ" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "CQ" ] . ToString ( ) );
                        EditTre ( SQLString ,model );
                    }
                }
            }

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                //获取入职补贴
                table = getTableSix ( dt );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH002 = row [ "ANX002" ] . ToString ( );
                        model . WAH014 = string . IsNullOrEmpty ( row [ "ANX" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX" ] . ToString ( ) );
                        EditSix ( SQLString ,model );
                    }
                }
            }

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                //获取提留工资
                table = getTableView ( dt );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH023 = row [ "ANW013" ] . ToString ( );
                        model . WAH024 = string . IsNullOrEmpty ( row [ "ANX" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX" ] . ToString ( ) );
                        EditSev ( SQLString ,model );
                    }
                }
            }

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                SQLString . Clear ( );
                //获取养老和技能
                table = getTableSev ( );
                if ( table != null && table . Rows . Count > 0 )
                {
                    foreach ( DataRow row in table . Rows )
                    {
                        model . WAH019 = string . IsNullOrEmpty ( row [ "EMP030" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "EMP030" ] . ToString ( ) );
                        //model . WAH013 = string . IsNullOrEmpty ( row [ "EMP029" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "EMP029" ] . ToString ( ) );
                        model . WAH004 = row [ "EMP007" ] . ToString ( );
                        model . WAH002 = row [ "EMP001" ] . ToString ( );
                        if ( Exists ( model . WAH001 ,model . WAH002 ) )
                            EditEgi ( SQLString ,model );
                    }
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 得到报工工资
        /// </summary>
        /// <param name="dt"></param>
        DataTable getTableOne ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            
            strSql . Append ( "WITH CET AS(" );
            //组装报工
            strSql . AppendFormat ( "SELECT ANX002,ANX003,ANX004,CONVERT(FLOAT,SUM(ANX015+ANX016)) AN,CONVERT(FLOAT,SUM(ANX017)) ANW FROM MIKANW A INNER JOIN MIKANX B ON A.ANW001=B.ANX001 WHERE ANW020=1 AND ANX011 IN ('在职','请假') AND YEAR(ANW015)={0} AND MONTH(ANW015)={1} GROUP BY ANX002,ANX003,ANX004" ,dt . Year ,dt . Month );
            //组装附件
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT ANV002,ANV003,ANV004,CONVERT(FLOAT,SUM(ANV009+ANV015)) ANV,CONVERT(FLOAT,SUM(ANV010)) ANT FROM MIKANT A INNER JOIN MIKANV B ON A.ANT001=B.ANV001 WHERE ANT006=1 AND ANV007 IN ('在职','请假') AND YEAR(ANT014)={0} AND MONTH(ANT014)={1} GROUP BY ANV002,ANV003,ANV004" ,dt . Year ,dt . Month );
            //五金
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT HAX002,HAX003,HAX004,CONVERT(FLOAT,SUM(HAX018+HAX019)) HAX,CONVERT(FLOAT,SUM(HAX020)) HAW FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 WHERE HAW018=1 AND HAX015 IN ('在职','请假') AND YEAR(HAW024)={0} AND MONTH(HAW024)={1} GROUP BY HAX002,HAX003,HAX004" ,dt . Year ,dt . Month );
            //注塑
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJB002,IJB012,IJB013,CONVERT(FLOAT,SUM(IJB023+IJB024)) IJB,CONVERT(FLOAT,SUM(IJB025)) IJA FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 WHERE IJA010=1 AND IJB003 IN ('在职','请假') AND YEAR(IJA015)={0} AND MONTH(IJA015)={1} GROUP BY IJB002,IJB012,IJB013" ,dt . Year ,dt . Month );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJD002,IJD003,IJD004,CONVERT(FLOAT,SUM(IJD012)) IJD,CONVERT(FLOAT,SUM(IJD013)) IJA FROM MIKIJA A INNER JOIN MIKIJD B ON A.IJA001=B.IJD001 WHERE  IJA010=1 AND IJD010 IN ('在职','请假') AND YEAR(IJA015)={0} AND MONTH(IJA015)={1} GROUP BY IJD002,IJD003,IJD004" ,dt . Year ,dt . Month );
            //LED
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LEG002,LEG003,LEG004,CONVERT(FLOAT,SUM(LEG014+LEG015)) LEG,CONVERT(FLOAT,SUM(LEG016)) LEF FROM MIKLEF A INNER JOIN MIKLEG B ON A.LEF001=B.LEG001 WHERE LEF017=1 AND LEG012 IN ('在职','请假') AND YEAR(LEF023)={0} AND MONTH(LEF023)={1} GROUP BY LEG002,LEG003,LEG004" ,dt . Year ,dt . Month );
            //面板
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LED002,LED003,LED004,CONVERT(FLOAT,SUM(LED014+LED015)) LED,CONVERT(FLOAT,SUM(LED016)) LEC FROM MIKLEC A INNER JOIN MIKLED B ON A.LEC001=B.LED001 WHERE LEC017=1 AND LED012 IN ('在职','请假') AND YEAR(LEC023)={0} AND MONTH(LEC023)={1}  GROUP BY LED002,LED003,LED004" ,dt . Year ,dt . Month );
            //喷漆
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT PPA002,PPA003,PPA004,CONVERT(FLOAT,SUM(PPA012+PPA013)) PPA,CONVERT(FLOAT,SUM(PPA014)) PPN FROM MIKPAN A INNER JOIN MIKPAP B ON A.PAN001=B.PAP001 WHERE PAN009=1 AND PPA010 IN ('在职','请假') AND YEAR(PAN015)={0} AND MONTH(PAN015)={1} GROUP BY PPA002,PPA003,PPA004" ,dt . Year ,dt . Month );
            //物流
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LGP002,LGP003,LGP004,CONVERT(FLOAT,SUM(ISNULL(LGP011,0)+ISNULL(LGP012,0))) LGP,CONVERT(FLOAT,SUM(ISNULL(LGP013,0))) LG FROM MIKLGP A INNER JOIN MIKLGN B ON A.LGP001=B.LGN001 WHERE LGN003=1 AND LGP005 IN ('在职','请假') AND YEAR(LGN009)={0} AND MONTH(LGN009)={1} GROUP BY LGP002,LGP003,LGP004" ,dt . Year ,dt . Month );
            strSql . Append ( ") " );
            strSql . Append ( "SELECT ANX002,ANX003,SUM(AN) AN,SUM(ANW) ANW FROM CET GROUP BY ANX002,ANX003" );

            return  SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 各车间考勤天数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        DataTable getTableTwo ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT CJ,SUM(CQ) CQ FROM (" );
            strSql . AppendFormat ( "SELECT ANW013 CJ,COUNT(1) CQ FROM (SELECT DISTINCT ANW013,CASE WHEN ANX005 IS NOT NULL AND ANX005!='' THEN CONVERT(NVARCHAR(20),ANX005,112) WHEN ANX006 IS NOT NULL AND ANX006!='' THEN CONVERT(NVARCHAR(20),ANX006,112) WHEN ANX007 IS NOT NULL AND ANX007!='' THEN CONVERT(NVARCHAR(20),ANX007,112) WHEN ANX008 IS NOT NULL AND ANX008!='' THEN CONVERT(NVARCHAR(20),ANX008,112) END ANX FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 WHERE  ANW020=1 AND ANX011 IN ('在职','请假') AND (CASE WHEN ANX005  IS NOT NULL AND ANX005!='' THEN CONVERT(NVARCHAR(20),ANX005,112) WHEN ANX006 IS NOT NULL AND ANX006!='' THEN CONVERT(NVARCHAR(20),ANX006,112) WHEN ANX007 IS NOT NULL AND ANX007!='' THEN CONVERT(NVARCHAR(20),ANX007,112) WHEN ANX008 IS NOT NULL AND ANX008!='' THEN CONVERT(NVARCHAR(20),ANX008,112) END) LIKE '{0}%') A GROUP BY ANW013" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT ANT005 CJ,COUNT(1) CQ FROM (SELECT DISTINCT ANT005,CASE WHEN ANV005 IS NOT NULL AND ANV005!='' THEN CONVERT(NVARCHAR(20),ANV005,112) WHEN ANV006 IS NOT NULL AND ANV006!='' THEN CONVERT(NVARCHAR(20),ANV006,112) WHEN ANV013 IS NOT NULL AND ANV013!='' THEN CONVERT(NVARCHAR(20),ANV013,112) WHEN ANV014 IS NOT NULL AND ANV014!='' THEN CONVERT(NVARCHAR(20),ANV014,112) END ANV FROM MIKANT A INNER JOIN MIKANV B ON A.ANT001=B.ANV001 WHERE ANT006=1 AND ANV007 IN ('在职','请假') AND (CASE WHEN ANV005 IS NOT NULL AND ANV005!='' THEN CONVERT(NVARCHAR(20),ANV005,112) WHEN ANV006 IS NOT NULL AND ANV006!='' THEN CONVERT(NVARCHAR(20),ANV006,112) END) LIKE '{0}%') A GROUP BY ANT005" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT HAW015,COUNT(1) CQ FROM (SELECT DISTINCT HAW015,CASE WHEN HAX009 IS NOT NULL AND HAX009!='' THEN CONVERT(NVARCHAR(20),HAX009,112) WHEN HAX010 IS NOT NULL AND HAX010!='' THEN CONVERT(NVARCHAR(20),HAX010,112) WHEN HAX011 IS NOT NULL AND HAX011!='' THEN CONVERT(NVARCHAR(20),HAX011,112) WHEN HAX012 IS NOT NULL AND HAX012!='' THEN CONVERT(NVARCHAR(20),HAX012,112) END HAX FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 WHERE HAW018=1 AND HAX015 IN ('在职','请假') AND (CASE WHEN HAX009 IS NOT NULL AND HAX009!='' THEN CONVERT(NVARCHAR(20),HAX009,112) WHEN HAX010 IS NOT NULL AND HAX010!='' THEN CONVERT(NVARCHAR(20),HAX010,112) WHEN HAX011 IS NOT NULL AND HAX011!='' THEN CONVERT(NVARCHAR(20),HAX011,112) WHEN HAX012 IS NOT NULL AND HAX012!='' THEN CONVERT(NVARCHAR(20),HAX012,112) END) LIKE '{0}%') A GROUP BY HAW015" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJA006,COUNT(1) CQ FROM (SELECT DISTINCT IJA006,CASE WHEN IJB016 IS NOT NULL AND IJB016!='' THEN CONVERT(NVARCHAR(20),IJB016,112) WHEN IJB017 IS NOT NULL AND IJB017!='' THEN CONVERT(NVARCHAR(20),IJB017,112) WHEN IJB018 IS NOT NULL AND IJB018!='' THEN CONVERT(NVARCHAR(20),IJB018,112) WHEN IJB019 IS NOT NULL AND IJB019!='' THEN CONVERT(NVARCHAR(20),IJB019,112) END IJB  FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 WHERE IJA010=1 AND IJB003 IN ('在职','请假') AND (CASE WHEN IJB016 IS NOT NULL AND IJB016!='' THEN CONVERT(NVARCHAR(20),IJB016,112) WHEN IJB017 IS NOT NULL AND IJB017!='' THEN CONVERT(NVARCHAR(20),IJB017,112) WHEN IJB018 IS NOT NULL AND IJB018!='' THEN CONVERT(NVARCHAR(20),IJB018,112) WHEN IJB019 IS NOT NULL AND IJB019!='' THEN CONVERT(NVARCHAR(20),IJB019,112) END) LIKE '{0}%' " ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION " );
            strSql . AppendFormat ( "SELECT DISTINCT IJA006,CASE WHEN IJD006 IS NOT NULL AND IJD006!='' THEN CONVERT(NVARCHAR(20),IJD006,112) WHEN IJD007 IS NOT NULL AND IJD007!='' THEN CONVERT(NVARCHAR(20),IJD007,112) END IJD FROM MIKIJA A INNER JOIN MIKIJD B ON A.IJA001=B.IJD001 WHERE IJA010=1 AND IJD010 IN ('在职','请假') AND (CASE WHEN IJD006 IS NOT NULL AND IJD006!='' THEN CONVERT(NVARCHAR(20),IJD006,112) WHEN IJD007 IS NOT NULL AND IJD007!='' THEN CONVERT(NVARCHAR(20),IJD007,112) END) LIKE '{0}%') A GROUP BY IJA006" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LEC012,COUNT(1) CQ FROM (SELECT DISTINCT LEC012,CASE WHEN LED005 IS NOT NULL AND LED005!='' THEN CONVERT(NVARCHAR(20),LED005,112)  WHEN LED006 IS NOT NULL AND LED006!='' THEN CONVERT(NVARCHAR(20),LED006,112) WHEN LED008 IS NOT NULL AND LED008!='' THEN CONVERT(NVARCHAR(20),LED008,112) WHEN LED009 IS NOT NULL AND LED009!='' THEN CONVERT(NVARCHAR(20),LED009,112) END LED FROM MIKLEC A INNER JOIN MIKLED B ON A.LEC001=B.LED001 WHERE LEC017=1 AND LED012 IN ('在职','请假') AND (CASE WHEN LED005 IS NOT NULL AND LED005!='' THEN CONVERT(NVARCHAR(20),LED005,112)  WHEN LED006 IS NOT NULL AND LED006!='' THEN CONVERT(NVARCHAR(20),LED006,112) WHEN LED008 IS NOT NULL AND LED008!='' THEN CONVERT(NVARCHAR(20),LED008,112) WHEN LED009 IS NOT NULL AND LED009!='' THEN CONVERT(NVARCHAR(20),LED009,112) END) LIKE '{0}%' ) A GROUP BY LEC012" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LEF012,COUNT(1) CQ FROM (SELECT DISTINCT LEF012,CASE WHEN LEG005 IS NOT NULL AND LEG005!='' THEN CONVERT(NVARCHAR(20),LEG005,112) WHEN LEG006 IS NOT NULL AND LEG006!='' THEN CONVERT(NVARCHAR(20),LEG006,112) WHEN LEG008 IS NOT NULL AND LEG008!='' THEN CONVERT(NVARCHAR(20),LEG008,112) WHEN LEG009 IS NOT NULL AND LEG009!='' THEN CONVERT(NVARCHAR(20),LEG009,112) END LEG FROM MIKLEF A INNER JOIN MIKLEG B ON A.LEF001=B.LEG001 WHERE LEF017=1 AND LEG012 IN ('在职','请假') AND (CASE WHEN LEG005 IS NOT NULL AND LEG005!='' THEN CONVERT(NVARCHAR(20),LEG005,112) WHEN LEG006 IS NOT NULL AND LEG006!='' THEN CONVERT(NVARCHAR(20),LEG006,112) WHEN LEG008 IS NOT NULL AND LEG008!='' THEN CONVERT(NVARCHAR(20),LEG008,112) WHEN LEG009 IS NOT NULL AND LEG009!='' THEN CONVERT(NVARCHAR(20),LEG009,112) END) LIKE '{0}%') A GROUP BY LEF012" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT PAN005,COUNT(1) CQ FROM (SELECT DISTINCT PAN005,CASE WHEN PPA005 IS NOT NULL AND PPA005!='' THEN CONVERT(NVARCHAR(20),PPA005,112) WHEN PPA006 IS NOT NULL AND PPA006!='' THEN CONVERT(NVARCHAR(20),PPA006,112) WHEN PPA007 IS NOT NULL AND PPA007!='' THEN CONVERT(NVARCHAR(20),PPA007,112) WHEN PPA008 IS NOT NULL AND PPA008!='' THEN CONVERT(NVARCHAR(20),PPA008,112) END PPA FROM MIKPAN A INNER JOIN MIKPAP B ON A.PAN001=B.PAP001 WHERE PAN009=1 AND PPA010 IN ('在职','请假') AND (CASE WHEN PPA005 IS NOT NULL AND PPA005!='' THEN CONVERT(NVARCHAR(20),PPA005,112) WHEN PPA006 IS NOT NULL AND PPA006!='' THEN CONVERT(NVARCHAR(20),PPA006,112) WHEN PPA007 IS NOT NULL AND PPA007!='' THEN CONVERT(NVARCHAR(20),PPA007,112) WHEN PPA008 IS NOT NULL AND PPA008!='' THEN CONVERT(NVARCHAR(20),PPA008,112) END) LIKE '{0}%') A GROUP BY PAN005" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LGP006,COUNT(1) CQ FROM (SELECT DISTINCT LGP006,CASE WHEN LGP009 IS NOT NULL AND LGP009!='' THEN CONVERT(NVARCHAR(20),LGP009,112) WHEN LGP010 IS NOT NULL AND LGP010!='' THEN CONVERT(NVARCHAR(20),LGP010,112) WHEN LGP007 IS NOT NULL AND LGP007!='' THEN CONVERT(NVARCHAR(20),LGP007,112) WHEN LGP008 IS NOT NULL AND LGP008!='' THEN CONVERT(NVARCHAR(20),LGP008,112) END PPA FROM MIKLGN A INNER JOIN MIKLGP B ON A.LGN001=B.LGP001 WHERE LGN003=1 AND LGP005 IN ('在职','请假') AND (CASE WHEN LGP009 IS NOT NULL AND LGP009!='' THEN CONVERT(NVARCHAR(20),LGP009,112) WHEN LGP010 IS NOT NULL AND LGP010!='' THEN CONVERT(NVARCHAR(20),LGP010,112) WHEN LGP007 IS NOT NULL AND LGP007!='' THEN CONVERT(NVARCHAR(20),LGP007,112) WHEN LGP008 IS NOT NULL AND LGP008!='' THEN CONVERT(NVARCHAR(20),LGP008,112) END) LIKE '{0}%') A GROUP BY LGP006" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( ") A GROUP BY CJ" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 员工考勤天数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        DataTable getTableTre ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT ANX002,SUM(CQ) CQ FROM (" );
            strSql . AppendFormat ( "SELECT ANX002,COUNT(1) CQ FROM (SELECT DISTINCT ANX002,ANX003,CASE WHEN ANX005 IS NOT NULL AND ANX005!='' THEN CONVERT(NVARCHAR(20),ANX005,112) WHEN ANX006 IS NOT NULL AND ANX006!='' THEN CONVERT(NVARCHAR(20),ANX006,112) WHEN ANX007 IS NOT NULL AND ANX007!='' THEN CONVERT(NVARCHAR(20),ANX007,112) WHEN ANX008 IS NOT NULL AND ANX008!='' THEN CONVERT(NVARCHAR(20),ANX008,112) END ANX FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 WHERE ANW020=1 AND ANX011 IN ('在职','请假') AND (CASE WHEN ANX005 IS NOT NULL AND ANX005!='' THEN CONVERT(NVARCHAR(20),ANX005,112) WHEN ANX006 IS NOT NULL AND ANX006!='' THEN CONVERT(NVARCHAR(20),ANX006,112) WHEN ANX007 IS NOT NULL AND ANX007!='' THEN CONVERT(NVARCHAR(20),ANX007,112) WHEN ANX008 IS NOT NULL AND ANX008!='' THEN CONVERT(NVARCHAR(20),ANX008,112) END) LIKE '{0}%') A GROUP BY ANX002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT ANV002,COUNT(1) CQ FROM (SELECT DISTINCT ANV002,ANV003,CASE WHEN ANV005 IS NOT NULL AND ANV005!='' THEN CONVERT(NVARCHAR(20),ANV005,112) WHEN ANV006 IS NOT NULL AND ANV006!='' THEN CONVERT(NVARCHAR(20),ANV006,112) WHEN ANV013 IS NOT NULL AND ANV013!='' THEN CONVERT(NVARCHAR(20),ANV013,112) WHEN ANV014 IS NOT NULL AND ANV014!='' THEN CONVERT(NVARCHAR(20),ANV014,112) END ANV FROM MIKANT A INNER JOIN MIKANV B ON A.ANT001=B.ANV001 WHERE ANT006=1 AND ANV007 IN ('在职','请假') AND (CASE WHEN ANV005 IS NOT NULL AND ANV005!='' THEN CONVERT(NVARCHAR(20),ANV005,112) WHEN ANV006 IS NOT NULL AND ANV006!='' THEN CONVERT(NVARCHAR(20),ANV006,112) WHEN ANV013 IS NOT NULL AND ANV013!='' THEN CONVERT(NVARCHAR(20),ANV013,112) WHEN ANV014 IS NOT NULL AND ANV014!='' THEN CONVERT(NVARCHAR(20),ANV014,112) END) LIKE '{0}%') A GROUP BY ANV002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT HAX002,COUNT(1) CQ FROM (SELECT DISTINCT HAX002,HAX003,CASE WHEN HAX009 IS NOT NULL AND HAX009!='' THEN CONVERT(NVARCHAR(20),HAX009,112) WHEN HAX010 IS NOT NULL AND HAX010!='' THEN CONVERT(NVARCHAR(20),HAX010,112) WHEN HAX011 IS NOT NULL AND HAX011!='' THEN CONVERT(NVARCHAR(20),HAX011,112) WHEN HAX012 IS NOT NULL AND HAX012!='' THEN CONVERT(NVARCHAR(20),HAX012,112) END HAX FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 WHERE HAW018=1 AND HAX015 IN ('在职','请假') AND (CASE WHEN HAX009 IS NOT NULL AND HAX009!='' THEN CONVERT(NVARCHAR(20),HAX009,112) WHEN HAX010 IS NOT NULL AND HAX010!='' THEN CONVERT(NVARCHAR(20),HAX010,112) WHEN HAX011 IS NOT NULL AND HAX011!='' THEN CONVERT(NVARCHAR(20),HAX011,112) WHEN HAX012 IS NOT NULL AND HAX012!='' THEN CONVERT(NVARCHAR(20),HAX012,112) END) LIKE '{0}%') A GROUP BY HAX002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJB002,COUNT(1) CQ FROM (SELECT DISTINCT IJB002,IJB012,CASE WHEN IJB016 IS NOT NULL AND IJB016!='' THEN CONVERT(NVARCHAR(20),IJB016,112) WHEN IJB017 IS NOT NULL AND IJB017!='' THEN CONVERT(NVARCHAR(20),IJB017,112) WHEN IJB018 IS NOT NULL AND IJB018!='' THEN CONVERT(NVARCHAR(20),IJB018,112)WHEN IJB019 IS NOT NULL AND IJB019!='' THEN CONVERT(NVARCHAR(20),IJB019,112) END IJB  FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 WHERE IJA010=1 AND IJB003 IN ('在职','请假') AND (CASE WHEN IJB016 IS NOT NULL AND IJB016!='' THEN CONVERT(NVARCHAR(20),IJB016,112) WHEN IJB017 IS NOT NULL AND IJB017!='' THEN CONVERT(NVARCHAR(20),IJB017,112) WHEN IJB018 IS NOT NULL AND IJB018!='' THEN CONVERT(NVARCHAR(20),IJB018,112)WHEN IJB019 IS NOT NULL AND IJB019!='' THEN CONVERT(NVARCHAR(20),IJB019,112) END) LIKE '{0}%') A GROUP BY IJB002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJD002,COUNT(1) CQ FROM (SELECT DISTINCT IJD002,IJD003,CASE WHEN IJD006 IS NOT NULL AND IJD006!='' THEN CONVERT(NVARCHAR(20),IJD006,112) WHEN IJD007 IS NOT NULL AND IJD007!='' THEN CONVERT(NVARCHAR(20),IJD007,112) END IJD FROM MIKIJA A INNER JOIN MIKIJD B ON A.IJA001=B.IJD001 WHERE IJA010=1 AND IJD010 IN ('在职','请假') AND (CASE WHEN IJD006 IS NOT NULL AND IJD006!='' THEN CONVERT(NVARCHAR(20),IJD006,112) WHEN IJD007 IS NOT NULL AND IJD007!='' THEN CONVERT(NVARCHAR(20),IJD007,112) END) LIKE '{0}%') A GROUP BY IJD002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LED002,COUNT(1) CQ FROM (SELECT DISTINCT LED002,LED003,CASE WHEN LED005 IS NOT NULL AND LED005!='' THEN CONVERT(NVARCHAR(20),LED005,112)  WHEN LED006 IS NOT NULL AND LED006!=''  THEN CONVERT(NVARCHAR(20),LED006,112) WHEN LED008 IS NOT NULL AND LED008!=''  THEN CONVERT(NVARCHAR(20),LED008,112) WHEN LED009 IS NOT NULL AND LED009!=''  THEN CONVERT(NVARCHAR(20),LED009,112) END LED FROM MIKLEC A INNER JOIN MIKLED B ON A.LEC001=B.LED001 WHERE LEC017=1 AND LED012 IN ('在职','请假') AND (CASE WHEN LED005 IS NOT NULL AND LED005!='' THEN CONVERT(NVARCHAR(20),LED005,112)  WHEN LED006 IS NOT NULL AND LED006!='' THEN CONVERT(NVARCHAR(20),LED006,112) WHEN LED008 IS NOT NULL AND LED008!=''  THEN CONVERT(NVARCHAR(20),LED008,112) WHEN LED009 IS NOT NULL AND LED009!=''  THEN CONVERT(NVARCHAR(20),LED009,112) END) LIKE '{0}%' ) A GROUP BY LED002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LEG002,COUNT(1) CQ FROM (SELECT DISTINCT LEG002,LEG003,CASE WHEN LEG005 IS NOT NULL AND LEG005!='' THEN CONVERT(NVARCHAR(20),LEG005,112) WHEN LEG006 IS NOT NULL AND LEG006!='' THEN CONVERT(NVARCHAR(20),LEG006,112) WHEN LEG008 IS NOT NULL AND LEG008!='' THEN CONVERT(NVARCHAR(20),LEG008,112) WHEN LEG009 IS NOT NULL AND LEG009!='' THEN CONVERT(NVARCHAR(20),LEG009,112) END LEG FROM MIKLEF A INNER JOIN MIKLEG B ON A.LEF001=B.LEG001 WHERE LEF017=1 AND LEG012 IN ('在职','请假') AND (CASE WHEN LEG005 IS NOT NULL AND LEG005!='' THEN CONVERT(NVARCHAR(20),LEG005,112) WHEN LEG006 IS NOT NULL AND LEG006!='' THEN CONVERT(NVARCHAR(20),LEG006,112) WHEN LEG008 IS NOT NULL AND LEG008!='' THEN CONVERT(NVARCHAR(20),LEG008,112) WHEN LEG009 IS NOT NULL AND LEG009!='' THEN CONVERT(NVARCHAR(20),LEG009,112) END) LIKE '{0}%') A GROUP BY LEG002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT PPA002,COUNT(1) CQ FROM (SELECT DISTINCT PPA002,PPA003,CASE WHEN PPA005 IS NOT NULL AND PPA005!='' THEN CONVERT(NVARCHAR(20),PPA005,112) WHEN PPA006 IS NOT NULL AND PPA006!='' THEN CONVERT(NVARCHAR(20),PPA006,112) WHEN PPA007 IS NOT NULL AND PPA007!='' THEN CONVERT(NVARCHAR(20),PPA007,112) WHEN PPA008 IS NOT NULL AND PPA008!='' THEN CONVERT(NVARCHAR(20),PPA008,112) END PPA FROM MIKPAN A INNER JOIN MIKPAP B ON A.PAN001=B.PAP001 WHERE PAN009=1 AND PPA010 IN ('在职','请假') AND (CASE WHEN  PPA005 IS NOT NULL AND PPA005!='' THEN CONVERT(NVARCHAR(20),PPA005,112) WHEN PPA006 IS NOT NULL AND PPA006!='' THEN CONVERT(NVARCHAR(20),PPA006,112) WHEN PPA007 IS NOT NULL AND PPA007!='' THEN CONVERT(NVARCHAR(20),PPA007,112) WHEN PPA008 IS NOT NULL AND PPA008!='' THEN CONVERT(NVARCHAR(20),PPA008,112) END) LIKE '{0}%') A GROUP BY PPA002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LGP002,COUNT(1) CQ FROM (SELECT DISTINCT LGP002,CASE WHEN LGP009 IS NOT NULL AND LGP009!='' THEN CONVERT(NVARCHAR(20),LGP009,112) WHEN LGP010 IS NOT NULL AND LGP010!='' THEN CONVERT(NVARCHAR(20),LGP010,112) WHEN LGP007 IS NOT NULL AND LGP007!='' THEN CONVERT(NVARCHAR(20),LGP007,112) WHEN LGP008 IS NOT NULL AND LGP008!='' THEN CONVERT(NVARCHAR(20),LGP008,112) END PPA FROM MIKLGN A INNER JOIN MIKLGP B ON A.LGN001=B.LGP001 WHERE LGN003=1 AND LGP005 IN ('在职','请假') AND (CASE WHEN LGP009 IS NOT NULL AND LGP009!='' THEN CONVERT(NVARCHAR(20),LGP009,112) WHEN LGP010 IS NOT NULL AND LGP010!='' THEN CONVERT(NVARCHAR(20),LGP010,112) WHEN LGP007 IS NOT NULL AND LGP007!='' THEN CONVERT(NVARCHAR(20),LGP007,112) WHEN LGP008 IS NOT NULL AND LGP008!='' THEN CONVERT(NVARCHAR(20),LGP008,112) END) LIKE '{0}%') A GROUP BY LGP002" ,dt . ToString ( "yyyyMM" ) );
            strSql . Append ( ") A GROUP BY ANX002" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取人员所属车间或组
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        DataTable getTableFor ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT EMP001,EMP006,EMP027,EMP029 FROM MIKEMP A INNER JOIN MIKWAH B ON A.EMP001=B.WAH002 INNER JOIN MIKWAG C ON B.WAH001=C.WAG001 WHERE CONVERT(NVARCHAR(20),WAG002,112) LIKE '{0}%'" ,dt . ToString ( "yyyyMM" ) );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取特殊岗位天数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        DataTable getTableFiv ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANX002,CONVERT(FLOAT,SUM(ANX017)) ANX FROM MIKANW A INNER JOIN MIKANX B ON A.ANW001=B.ANX001 WHERE A.ANW020=1 AND ANX011='在职' AND ANX013='检测' AND YEAR(ANW022)={0} AND MONTH(ANW022)={1} GROUP BY ANX002" ,dt . Year ,dt . Month );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取入职补贴   只有组装才有特殊岗位补贴
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        DataTable getTableSix ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT ANX002,SUM(ANX015+ANX016)*7 ANX FROM (" );
            strSql . AppendFormat ( "SELECT ANX002,ANX015,ANX016,ANW022,EMP023,DATEDIFF(DAY,EMP023,ANW022) AN FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 INNER JOIN MIKEMP C ON A.ANX002=C.EMP001 WHERE ANW020=1 AND ANX011 IN ('在职','请假') AND CONVERT(NVARCHAR(20),ANW022,112) LIKE '{0}%') A WHERE A.AN>=0 AND A.AN<=7 GROUP BY ANX002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( "SELECT ANX002,COUNT(1) ANX FROM (" );
            //strSql . AppendFormat ( "SELECT ANX002,COUNT(1) ANX FROM (SELECT DISTINCT ANX002,ANX003,CASE WHEN ANX005 IS NOT NULL AND ANX005!='' THEN DATEDIFF(DAY,EMP023,ANX005) WHEN ANX006 IS NOT NULL AND ANX006!='' THEN DATEDIFF(DAY,EMP023,ANX006) WHEN ANX007 IS NOT NULL AND ANX007!='' THEN DATEDIFF(DAY,EMP023,ANX007) WHEN ANX008 IS NOT NULL AND ANX008!='' THEN DATEDIFF(DAY,EMP023,ANX008) END ANX FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 INNER JOIN MIKEMP C ON A.ANX002=C.EMP001 WHERE ANW020=1 AND ANX011='在职' AND (CASE WHEN ANX005 IS NOT NULL AND ANX005!='' THEN CONVERT(NVARCHAR(20),ANX005,112) WHEN ANX006 IS NOT NULL AND ANX006!='' THEN CONVERT(NVARCHAR(20),ANX006,112) WHEN ANX007 IS NOT NULL AND ANX007!='' THEN CONVERT(NVARCHAR(20),ANX007,112) WHEN ANX008 IS NOT NULL AND ANX008!='' THEN CONVERT(NVARCHAR(20),ANX008,112) END) LIKE '{0}%') A WHERE ANX>=0 AND ANX<=7 GROUP BY ANX002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT ANV002,COUNT(1) CQ FROM (SELECT DISTINCT ANV002,ANV003,CASE WHEN ANV005 IS NOT NULL AND ANV005!='' THEN DATEDIFF(DAY,EMP023,ANV005) WHEN ANV006 IS NOT NULL AND ANV006!='' THEN DATEDIFF(DAY,EMP023,ANV006) END ANV FROM MIKANT A INNER JOIN MIKANV B ON A.ANT001=B.ANV001 INNER JOIN MIKEMP C ON B.ANV002=C.EMP001 WHERE ANT006=1 AND ANV007='在职' AND (CASE WHEN ANV005 IS NOT NULL AND ANV005!='' THEN CONVERT(NVARCHAR(20),ANV005,112) WHEN ANV006 IS NOT NULL AND ANV006!='' THEN CONVERT(NVARCHAR(20),ANV006,112) END) LIKE '{0}%') A WHERE ANV>=0 AND ANV<=7 GROUP BY ANV002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT HAX002,COUNT(1) CQ FROM (SELECT DISTINCT HAX002,HAX003,CASE WHEN HAX009 IS NOT NULL AND HAX009!='' THEN DATEDIFF(DAY,EMP023,HAX009) WHEN HAX010 IS NOT NULL AND HAX010!='' THEN DATEDIFF(DAY,EMP023,HAX010) WHEN HAX011 IS NOT NULL AND HAX011!='' THEN DATEDIFF(DAY,EMP023,HAX011) WHEN HAX012 IS NOT NULL AND HAX012!='' THEN DATEDIFF(DAY,EMP023,HAX012) END HAX FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 INNER JOIN MIKEMP C ON B.HAX002=C.EMP001 WHERE HAW018=1 AND HAX015='在职' AND (CASE WHEN HAX009 IS NOT NULL AND HAX009!='' THEN CONVERT(NVARCHAR(20),HAX009,112) WHEN HAX010 IS NOT NULL AND HAX010!='' THEN CONVERT(NVARCHAR(20),HAX010,112) WHEN HAX011 IS NOT NULL AND HAX011!='' THEN CONVERT(NVARCHAR(20),HAX011,112) WHEN HAX012 IS NOT NULL AND HAX012!='' THEN CONVERT(NVARCHAR(20),HAX012,112) END) LIKE '{0}%') A WHERE HAX>=0 AND HAX<=7 GROUP BY HAX002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT IJB002,COUNT(1) CQ FROM (SELECT DISTINCT IJB002,IJB012,CASE WHEN IJB016 IS NOT NULL AND IJB016!='' THEN DATEDIFF(DAY,EMP023,IJB016) WHEN IJB017 IS NOT NULL AND IJB017!='' THEN DATEDIFF(DAY,EMP023,IJB017) END IJB  FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 INNER JOIN MIKEMP C ON B.IJB002=C.EMP001 WHERE IJA010=1 AND IJB003='在职' AND (CASE WHEN IJB016 IS NOT NULL AND IJB016!='' THEN CONVERT(NVARCHAR(20),IJB016,112) WHEN IJB017 IS NOT NULL AND IJB017!='' THEN CONVERT(NVARCHAR(20),IJB017,112) END) LIKE '{0}%') A WHERE IJB>=0 AND IJB<=7 GROUP BY IJB002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT IJD002,COUNT(1) CQ FROM (SELECT DISTINCT IJD002,IJD003,CASE WHEN IJD006 IS NOT NULL AND IJD006!='' THEN DATEDIFF(DAY,EMP023,IJD006) WHEN IJD007 IS NOT NULL AND IJD007!='' THEN DATEDIFF(DAY,EMP023,IJD007) END IJD FROM MIKIJA A INNER JOIN MIKIJD B ON A.IJA001=B.IJD001 INNER JOIN MIKEMP C ON B.IJD002=C.EMP001 WHERE IJA010=1 AND IJD010='在职' AND (CASE WHEN IJD006 IS NOT NULL AND IJD006!='' THEN CONVERT(NVARCHAR(20),IJD006,112) WHEN IJD007 IS NOT NULL AND IJD007!='' THEN CONVERT(NVARCHAR(20),IJD007,112) END) LIKE '{0}%') A WHERE IJD>=0 AND IJD<=7 GROUP BY IJD002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT LED002,COUNT(1) CQ FROM (SELECT DISTINCT LED002,LED003,CASE WHEN LED005 IS NOT NULL AND LED005!='' THEN DATEDIFF(DAY,EMP023,LED005) WHEN LED006 IS NOT NULL AND LED006!='' THEN DATEDIFF(DAY,EMP023,LED006) END LED FROM MIKLEC A INNER JOIN MIKLED B ON A.LEC001=B.LED001 INNER JOIN MIKEMP C ON B.LED002=C.EMP001 WHERE LEC017=1 AND LED012='在职' AND (CASE WHEN LED005 IS NOT NULL AND LED005!='' THEN CONVERT(NVARCHAR(20),LED005,112)  WHEN LED006 IS NOT NULL AND LED006!='' THEN CONVERT(NVARCHAR(20),LED006,112) END) LIKE '{0}%') A WHERE LED>=0 AND LED<=7 GROUP BY LED002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT LEG002,COUNT(1) CQ FROM (SELECT DISTINCT LEG002,LEG003,CASE WHEN LEG005 IS NOT NULL AND LEG005!='' THEN DATEDIFF(DAY,EMP023,LEG005) WHEN LEG006 IS NOT NULL AND LEG006!='' THEN DATEDIFF(DAY,EMP023,LEG006) WHEN LEG008 IS NOT NULL AND LEG008!='' THEN DATEDIFF(DAY,EMP023,LEG008) WHEN LEG009 IS NOT NULL AND LEG009!='' THEN DATEDIFF(DAY,EMP023,LEG009) END LEG FROM MIKLEF A INNER JOIN MIKLEG B ON A.LEF001=B.LEG001 INNER JOIN MIKEMP C ON B.LEG002=C.EMP001 WHERE LEF017=1 AND LEG012='在职' AND (CASE WHEN LEG005 IS NOT NULL AND LEG005!='' THEN CONVERT(NVARCHAR(20),LEG005,112) WHEN LEG006 IS NOT NULL AND LEG006!='' THEN CONVERT(NVARCHAR(20),LEG006,112) WHEN LEG008 IS NOT NULL AND LEG008!='' THEN CONVERT(NVARCHAR(20),LEG008,112) WHEN LEG009 IS NOT NULL AND LEG009!='' THEN CONVERT(NVARCHAR(20),LEG009,112) END) LIKE '{0}%') A WHERE LEG>=0 AND LEG<=7 GROUP BY LEG002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT PPA002,COUNT(1) CQ FROM (SELECT DISTINCT PPA002,PPA003,CASE WHEN PPA005 IS NOT NULL AND PPA005!='' THEN DATEDIFF(DAY,EMP023,PPA005) WHEN PPA006 IS NOT NULL AND PPA006!='' THEN DATEDIFF(DAY,EMP023,PPA006) WHEN PPA007 IS NOT NULL AND PPA007!='' THEN DATEDIFF(DAY,EMP023,PPA007) WHEN PPA008 IS NOT NULL AND PPA008!='' THEN DATEDIFF(DAY,EMP023,PPA008) END PPA FROM MIKPAN A INNER JOIN MIKPAP B ON A.PAN001=B.PAP001 INNER JOIN MIKEMP C ON B.PPA002=C.EMP001 WHERE PAN009=1 AND PPA010='在职' AND (CASE WHEN PPA005 IS NOT NULL AND PPA005!='' THEN CONVERT(NVARCHAR(20),PPA005,112) WHEN PPA006 IS NOT NULL AND PPA006!='' THEN CONVERT(NVARCHAR(20),PPA006,112) WHEN PPA007 IS NOT NULL AND PPA007!='' THEN CONVERT(NVARCHAR(20),PPA007,112) WHEN PPA008 IS NOT NULL AND PPA008!='' THEN CONVERT(NVARCHAR(20),PPA008,112) END) LIKE '{0}%') A WHERE PPA>=0 AND PPA<=7 GROUP BY PPA002) A GROUP BY ANX002" ,dt . ToString ( "yyyyMM" ) );
            //strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT LGP002,COUNT(1) LG FROM (SELECT DISTINCT LGP002,LGP003,DATEDIFF(DAY,EMP023,LGN002) LG FROM MIKLGN A INNER JOIN MIKLGP B ON A.LGN001=B.LGP001 INNER JOIN MIKEMP C ON B.LGP002=C.EMP001 WHERE LGP005='在职' AND LGN003=1 AND CONVERT(NVARCHAR(20),LGN002,112) LIKE '{0}%') A WHERE LG>=0 AND LG<=7 GROUP BY LGP002" ,dt . ToString ( "yyyyMM" ) );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取养老和技能
        /// </summary>
        /// <returns></returns>
        DataTable getTableSev ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "select EMP001,EMP029,EMP030,EMP007 from MIKEMP " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Exists ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM MIKWAG WHERE WAG001='{0}'" ,code );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userNum"></param>
        /// <returns></returns>
        bool Exists ( string code ,string userNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM MIKWAH WHERE WAH001='{0}' AND WAH002='{1}'" ,code ,userNum );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu.WagesHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKWAG (" );
            strSql . Append ( "WAG001,WAG002) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@WAG001,@WAG002) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@WAG001",SqlDbType.NVarChar,20),
                new SqlParameter("@WAG002",SqlDbType.Date,3)
            };
            parameter [ 0 ] . Value = model . WAG001;
            parameter [ 1 ] . Value = model . WAG002;
            SQLString . Add ( strSql ,parameter );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKWAH (" );
            strSql . Append ( "WAH001,WAH002,WAH003,WAH004,WAH006,WAH008) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@WAH001,@WAH002,@WAH003,@WAH004,@WAH006,@WAH008) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@WAH001", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH002", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH003", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH004", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH006", SqlDbType.Decimal,9),
                new SqlParameter("@WAH008", SqlDbType.Decimal,9)
            };
            parameter [ 0 ] . Value = model . WAH001;
            parameter [ 1 ] . Value = model . WAH002;
            parameter [ 2 ] . Value = model . WAH003;
            parameter [ 3 ] . Value = model . WAH004;
            parameter [ 4 ] . Value = model . WAH006;
            parameter [ 5 ] . Value = model . WAH008;
            SQLString . Add ( strSql ,parameter );
        }

        void EditBodyAll ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH005=0,WAH006=0,WAH007=0,WAH008=0,WAH012=0,WAH013=0,WAH014=0 WHERE WAH001='{0}'" ,model . WAH001 );

            SQLString . Add ( strSql ,null );
        }

        void EditOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKWAH SET " );
            strSql . Append ( "WAH003=@WAH003," );
            //strSql . Append ( "WAH004=@WAH004," );
            strSql . Append ( "WAH006=@WAH006," );
            strSql . Append ( "WAH008=@WAH008 " );
            strSql . Append ( "WHERE WAH001=@WAH001 AND " );
            strSql . Append ( "WAH002=@WAH002 " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@WAH001", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH002", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH003", SqlDbType.NVarChar,20),
                //new SqlParameter("@WAH004", SqlDbType.NVarChar,20),
                new SqlParameter("@WAH006", SqlDbType.Decimal,9),
                new SqlParameter("@WAH008", SqlDbType.Decimal,9)
            };
            parameter [ 0 ] . Value = model . WAH001;
            parameter [ 1 ] . Value = model . WAH002;
            parameter [ 2 ] . Value = model . WAH003;
            //parameter [ 3 ] . Value = model . WAH004;
            parameter [ 3 ] . Value = model . WAH006;
            parameter [ 4 ] . Value = model . WAH008;
            SQLString . Add ( strSql ,parameter );
        }
        void EditTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH022={0} WHERE WAH023='{1}' AND WAH001='{2}' " ,model . WAH022 ,model . WAH023 ,model . WAH001 );

            SQLString . Add ( strSql ,null );
        }
        void EditTre ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH005={0} WHERE WAH001='{1}' AND WAH002='{2}' " ,model . WAH005 ,model . WAH001 ,model . WAH002 );

            SQLString . Add ( strSql ,null );
        }
        void EditFor ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH023='{0}',WAH007={3},WAH013={4} WHERE WAH001='{1}' AND WAH002='{2}' " ,model . WAH023 ,model . WAH001 ,model . WAH002 ,model . WAH007 ,model . WAH013 );

            SQLString . Add ( strSql ,null );
        }
        void EditFiv ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH012={0} WHERE WAH001='{1}' AND WAH002='{2}' " ,model . WAH012 ,model . WAH001 ,model . WAH002 );

            SQLString . Add ( strSql ,null );
        }
        void EditSix ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH014={0} WHERE WAH001='{1}' AND WAH002='{2}' " ,model . WAH014 ,model . WAH001 ,model . WAH002 );

            SQLString . Add ( strSql ,null );
        }
        void EditSev ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH024={0} WHERE WAH001='{1}' AND WAH023='{2}' " ,model . WAH024 ,model . WAH001 ,model . WAH023 );

            SQLString . Add ( strSql ,null );
        }
        void EditEgi ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . WagesBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAH SET WAH013={0},WAH019={1},WAH004='{4}' WHERE WAH001='{2}' AND WAH002='{3}' " ,model . WAH013 ,model . WAH019 ,model . WAH001 ,model . WAH002 ,model . WAH004 );

            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Examine ( string code ,bool result )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKWAG SET WAG003=@WAG003 WHERE WAG001=@WAG001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@WAG001",SqlDbType.NVarChar,20),
                new SqlParameter("@WAG003",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = code;
            parameter [ 1 ] . Value = result;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableQuery ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT WAG001,WAG002,WAG003 FROM MIKWAG WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LineProductMesEntityu . WagesHeaderEntity getModel ( string code )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT WAG001,WAG002,WAG003 FROM MIKWAG WHERE WAG001='{0}'" ,code );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
                return getModel ( table . Rows [ 0 ] );
            else
                return null;
        }

        public LineProductMesEntityu . WagesHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . WagesHeaderEntity model = new LineProductMesEntityu . WagesHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "WAG001" ] != null )
                    model . WAG001 = row [ "WAG001" ] . ToString ( );
                if ( row [ "WAG002" ] != null && row [ "WAG002" ] . ToString ( ) != string . Empty )
                    model . WAG002 = Convert . ToDateTime ( row [ "WAG002" ] . ToString ( ) );
                if ( row [ "WAG003" ] != null && row [ "WAG003" ] . ToString ( ) != string . Empty )
                    model . WAG003 = Convert . ToBoolean ( row [ "WAG003" ] . ToString ( ) );
            }
            return model;
        }
        
        /// <summary>
        /// 获取提留工资
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable getTableView ( DateTime dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . AppendFormat ( "SELECT ANW013,SUM(CASE ANW014 WHEN '计件' THEN (JJ+JS)*0.05 WHEN '计时' THEN 0 ELSE 0 END) ANX FROM MIKANW A INNER JOIN (SELECT ANN001,CONVERT(FLOAT,SUM(ANN008*ANN009)) JJ FROM MIKANN GROUP BY ANN001) B ON A.ANW001=B.ANN001 INNER JOIN (SELECT ANX001,CONVERT(FLOAT,SUM(ANX016*ANX009)) JS FROM MIKANX WHERE ANX011='在职' GROUP BY ANX001) C ON A.ANW001=C.ANX001 WHERE ANW020=1 AND YEAR(ANW015)={0} AND MONTH(ANW015)={1}  GROUP BY ANW013" ,dt . Year ,dt . Month );
            strSql . AppendFormat ( "WITH CET AS (SELECT ANW013,SUM(ISNULL(ANW026,0)+ISNULL(ANW027,0))+ISNULL(JSS,0)+ISNULL(JJ,0)+ISNULL(JS,0) ANX FROM MIKANW A LEFT JOIN (SELECT ANN001,CONVERT(FLOAT,SUM(ANN008*ANN009)) JJ FROM MIKANN A INNER JOIN MIKANW B ON A.ANN001=B.ANW001 WHERE ANW020=1 AND YEAR(ANW015)={0} AND MONTH(ANW015)={1} AND ANW014='计件' GROUP BY ANN001) B ON A.ANW001=B.ANN001 LEFT JOIN (SELECT ANX001,CONVERT(FLOAT,SUM(ANX016*ANX009)) JS FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 WHERE ANW020=1 AND YEAR(ANW015)={0} AND MONTH(ANW015)={1} AND ANW014='计件' AND ANX011='在职' GROUP BY ANX001) C ON A.ANW001=C.ANX001  LEFT JOIN (SELECT ANX001,CONVERT(FLOAT,SUM(ISNULL(ANX018,0))) JSS FROM MIKANX A INNER JOIN MIKANW B ON A.ANX001=B.ANW001 WHERE ANW020=1 AND YEAR(ANW015)={0} AND MONTH(ANW015)={1} AND ANW014='计件' AND ANX011='请假' GROUP BY ANX001) D ON A.ANW001=D.ANX001 WHERE ANW020=1 AND YEAR(ANW015)={0} AND MONTH(ANW015)={1}  GROUP BY ANW013,JSS,JJ,JS) SELECT ANW013,SUM(ANX)*0.05 ANX FROM CET GROUP BY ANW013" ,dt . Year ,dt . Month );
            strSql . Append ( " UNION ALL " );
            //strSql . AppendFormat ( "SELECT PAN005,CASE PAN013 WHEN '计件' THEN (PAO+PPA)*0.05 ELSE 0 END ANX  FROM MIKPAN A INNER JOIN (SELECT PAO001,SUM(PAO012*PAO006) PAO FROM MIKPAO GROUP BY PAO001) B ON A.PAN001=B.PAO001 INNER JOIN (SELECT PAP001,SUM(PPA009*(DATEDIFF(hh,PPA007,PPA008))) PPA FROM MIKPAP WHERE PPA010='在职' GROUP BY PAP001) C ON A.PAN001=C.PAP001 WHERE YEAR(PAN006)={0} AND MONTH(PAN006)={1} AND PAN009=1" ,dt . Year ,dt . Month );
            strSql . AppendFormat ( "SELECT PAN005,SUM(ISNULL(PAN,0)+ISNULL(PAO,0)+ISNULL(PPA,0)+ISNULL(PA,0))*0.05 ANX FROM (SELECT PAN005,SUM(ISNULL(PAN017,0)*ISNULL(PAN018,0)) PAN,PAO,PPA,PA FROM MIKPAN A LEFT JOIN (SELECT PAO001,SUM(PAO012*PAO006) PAO FROM MIKPAO A INNER JOIN MIKPAN B ON A.PAO001=B.PAN001 WHERE YEAR(PAN006)={0} AND MONTH(PAN006)={1} AND PAN009=1 AND PAN013='计件' GROUP BY PAO001) B ON A.PAN001=B.PAO001 LEFT JOIN (SELECT PAP001,SUM(PPA009*(DATEDIFF(hh,PPA007,PPA008))) PPA FROM MIKPAP A INNER JOIN MIKPAN B ON A.PAP001=B.PAN001 WHERE YEAR(PAN006)={0} AND MONTH(PAN006)={1} AND PAN009=1 AND PAN013='计件' AND PPA010='在职' GROUP BY PAP001) C ON A.PAN001=C.PAP001 LEFT JOIN (SELECT PAP001,SUM(ISNULL(PPA016,0)) PA FROM MIKPAP A INNER JOIN MIKPAN B ON A.PAP001=B.PAN001 WHERE YEAR(PAN006)={0} AND MONTH(PAN006)={1} AND PAN009=1 AND PAN013='计件' AND PPA010='请假' GROUP BY PAP001) D ON A.PAN001=D.PAP001 WHERE YEAR(PAN006)={0} AND MONTH(PAN006)={1} AND PAN009=1 GROUP BY PAN005,PAO,PPA,PA)  A GROUP BY PAN005" ,dt . Year ,dt . Month );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
