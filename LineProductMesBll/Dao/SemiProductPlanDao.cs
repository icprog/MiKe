using System . Data;
using System . Text;
using StudentMgr;
using System . Collections . Generic;
using System;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class SemiProductPlanDao
    {
        /// <summary>
        /// 获取数据列表  厂内生产件和委外件
        /// </summary>
        /// <returns></returns>
        public List<LineProductMesEntityu .SemiProductPlanEntity> getListModel ( )
        {
            string piNum = string . Empty;
            List<LineProductMesEntityu . SemiProductPlanEntity> modelList = new List<LineProductMesEntityu . SemiProductPlanEntity> ( );
            StringBuilder strSql = new StringBuilder ( );
            
            strSql . Append ( "SELECT DISTINCT PRE004,DEA002,DEA057,PRF002 PRE005,PRE007,PRF003 PRE008,DAA002,DDA003,DEA003 FROM (SELECT MAX(PRF002) PRF002,SUM(PRF003) PRF003,PRF001 FROM  MIKPRF GROUP BY PRF001) A INNER JOIN MIKPRE B ON A.PRF001=B.PRE004 INNER JOIN TPADEA C ON B.PRE004=C.DEA001 INNER JOIN SGMQAA D ON B.PRE004=D.QAA001 INNER JOIN TPADAA E ON C.DEA076=E.DAA001 INNER JOIN TPADDA F ON C.DEA008=F.DDA001 WHERE DEA076='0507' AND DEA009 IN ('M','S') AND QAA001 NOT IN (SELECT QAB003 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB003=B.DEA001)" );

            
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                int i = 0;
                foreach ( DataRow row in table . Rows )
                {
                    LineProductMesEntityu . SemiProductPlanEntity model = new LineProductMesEntityu . SemiProductPlanEntity ( );
                    i++;
                    model . SEP001 = row [ "PRE004" ] . ToString ( );
                    model . SEP002 = row [ "DEA002" ] . ToString ( );
                    model . SEP003 = row [ "DEA057" ] . ToString ( );
                    model . SEP004 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
                    model . SEP005 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
                    model . SEP006 = Convert . ToDecimal ( row [ "PRE008" ] . ToString ( ) );
                    model . SEP007 = i . ToString ( );
                    model . SEP008 = 0 . ToString ( );
                    model . SEP009 = 1;
                    model . SEP010 = row [ "DAA002" ] . ToString ( );
                    model . SEP011 = row [ "DDA003" ] . ToString ( );
                    model . SEP012 = row [ "DEA003" ] . ToString ( );
                    modelList . Add ( model );
                    if ( piNum == string . Empty )
                        piNum = "'" + model . SEP001 + "'";
                    else
                        piNum = piNum + "," + "'" + model . SEP001 + "'";
                }
            }

            if ( piNum != string . Empty )
            {
                bool state = true;
                while ( state )
                {
                    //展开第一阶
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "SELECT DISTINCT QAB001,QAB003,DEA002,DEA057,QAB005,DAA002,DDA003,DEA003 FROM SGMQAB C INNER JOIN TPADEA D ON C.QAB003=D.DEA001 LEFT JOIN TPADAA E ON D.DEA076=E.DAA001 LEFT JOIN TPADDA F ON D.DEA008=F.DDA001 WHERE DEA009 IN ('M','S') AND QAB001 IN ({0}) " ,piNum );

                    DataTable tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                    if ( tableOne != null && tableOne . Rows . Count > 0 )
                    {
                        piNum = string . Empty;
                        int i = 0;
                        foreach ( DataRow row in tableOne . Rows )
                        {
                            LineProductMesEntityu . SemiProductPlanEntity model = new LineProductMesEntityu . SemiProductPlanEntity ( );
                            i++;
                            model . SEP001 = row [ "QAB003" ] . ToString ( );
                            LineProductMesEntityu . SemiProductPlanEntity body = modelList . Find ( ( t ) =>
                            {
                                return t . SEP001 . Equals ( row [ "QAB001" ] . ToString ( ) );
                            } );
                            model . SEP002 = row [ "DEA002" ] . ToString ( );
                            model . SEP003 = row [ "DEA057" ] . ToString ( );
                            model . SEP004 = Convert . ToDateTime ( body . SEP004 ) . AddDays ( -1 );
                            model . SEP005 = body . SEP005;
                            model . SEP006 = body . SEP006 * body . SEP009;
                            if ( body != null )
                                model . SEP008 = body . SEP007;
                            else
                                model . SEP008 = 0 . ToString ( );
                            model . SEP007 = model . SEP008 + "_" + i . ToString ( );
                            model . SEP009 = Convert . ToDecimal ( row [ "QAB005" ] );
                            model . SEP010 = row [ "DAA002" ] . ToString ( );
                            model . SEP011 = row [ "DDA003" ] . ToString ( );
                            model . SEP012 = row [ "DEA003" ] . ToString ( );
                            modelList . Add ( model );
                            if ( piNum == string . Empty )
                                piNum = "'" + model . SEP001 + "'";
                            else
                                piNum = piNum + "," + "'" + model . SEP001 + "'";
                        }
                    }
                    else
                        state = false;
                }
            }

            return modelList;
        }

        /// <summary>
        /// 获取厂内生产件和委外件
        /// </summary>
        /// <returns></returns>
        public DataTable  getListAll ( string proName ,DateTime dtStart ,DateTime dtEnd )
        {
            SqlHelper . StoreProcedure ( "CNSCJ" );
            SqlParameter [ ] parameter = {
                new SqlParameter("PRONAME",proName ),
                new SqlParameter("DTSTART",dtStart ),
                new SqlParameter("DTEND",dtEnd )
                };

            return SqlHelper . ExecuteNoStoreTable ( parameter );
        }
        

        /*
CREATE PROC [dbo].[CNSCJ]
        AS
DECLARE @i int
DECLARE @j int
DECLARE @k int
DECLARE @count int
DECLARE @strCN NVARCHAR (4000)
DECLARE @SqlStr NVARCHAR (4000)
BEGIN
SET @j=0;
SET @k=0;
        SET @count=1;
        IF object_id ( N'#A',N'U') is not null
DROP TABLE #A
ELSE
CREATE TABLE #A(
ROW INT,
QAB003 NVARCHAR (20),
DEA002 NVARCHAR (50),
DEA057 NVARCHAR (50),
PRE005 DATE,
PRE007 INT,
QAB005 DECIMAL (18,6),
QAB006 DECIMAL (18,6)
)
INSERT INTO #A 
SELECT @j,QAB003,D.DEA002,D . DEA057,DATEADD ( day,-1,E . PRE005) PRE005,PRE007,CONVERT ( FLOAT,PRE008* QAB005 ) QAB005,0 QAB006 FROM SGMQAB C INNER JOIN TPADEA D ON C.QAB003=D . DEA001 INNER JOIN (SELECT PRE004 ,DEA002,DEA057,PRE005,PRE007,PRE008 FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001= B . PRE001 INNER JOIN TPADEA C ON B.PRE004= C . DEA001 INNER JOIN SGMQAA D ON B.PRE004= D . QAA001 WHERE PRD003 = 0 AND DEA076 = '0507' AND DEA009 IN ('M','S') AND QAA001 NOT IN ( SELECT QAB003 FROM SGMQAB A INNER JOIN TPADEA B ON A . QAB003= B . DEA001 )) E ON C . QAB001=E.PRE004 WHERE DEA009 IN ('M','S')

SET @i=1;
        WHILE @i=1
BEGIN
SET @j=@j+1;
PRINT @j;
        INSERT INTO #A 
SELECT @j,C.QAB003,D . DEA002,D . DEA057,DATEADD ( day,-1,E . PRE005) PRE005,PRE007,CONVERT ( FLOAT,C . QAB005* E.QAB005) QAB005,0 QAB006 FROM SGMQAB C INNER JOIN TPADEA D ON C.QAB003=D . DEA001 INNER JOIN (SELECT QAB003 ,DEA002,DEA057,PRE005,PRE007,QAB005 FROM #A WHERE ROW=@k) E ON C.QAB001=E.QAB003  collate Chinese_PRC_CI_AI_WS WHERE DEA009 IN ('M','S') 
SET @k = @k + 1;
        SELECT @count=COUNT(1) FROM SGMQAB C INNER JOIN TPADEA D ON C . QAB003=D.DEA001 INNER JOIN ( SELECT QAB003 ,DEA002,DEA057,PRE005,PRE007,QAB005 FROM #A WHERE ROW=@k) E ON C.QAB001=E.QAB003 collate Chinese_PRC_CI_AI_WS WHERE DEA009 IN ('M','S') 
IF(@count= 0 )
SET @i=0;
        END
        UPDATE #A SET QAB006=B.QAB005 FROM (SELECT SUM(QAB005) QAB005,QAB003 FROM #A GROUP BY QAB003) B WHERE #A.QAB003=B.QAB003
SELECT @strCN=ISNULL(@strCN+',','')+ QUOTENAME(PRE005) FROM  #A GROUP BY PRE005	
SET @SqlStr='
WITH CET AS (
SELECT QAB003 品号,DEA002 品名 ,DEA057 规格 ,PRE005,CONVERT(FLOAT,QAB005) 单量,CONVERT ( FLOAT,QAB006) 总量 FROM #A)
SELECT * FROM CET PIVOT ( SUM(单量) FOR PRE005 IN ('+@strCN+')) AS T'
EXEC ( @SqlStr);
        END
GO
                     */

        
        /// <summary>
        /// 获取采购件
        /// </summary>
        /// <returns></returns>
        public List<LineProductMesEntityu . SemiProductPlanEntity> getListModelPur ( )
        { 
            string piNum = string . Empty;
            List<LineProductMesEntityu . SemiProductPlanEntity> modelList = new List<LineProductMesEntityu . SemiProductPlanEntity> ( );
            StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT PRE004,DEA002,DEA057,MAX(PRE005) PRE005,PRE007,SUM(PRE008) PRE008 FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001=B.PRE001 INNER JOIN TPADEA C ON B.PRE004=C.DEA001 WHERE PRD003=0 GROUP BY PRE004,DEA002,DEA057,PRE007" );

            strSql . Append ( "SELECT DISTINCT PRE004,DEA002,DEA057,PRF002 PRE005,PRE007,PRF003 PRE008,DDA003,DEA003,DGA003 FROM (SELECT MAX(PRF002) PRF002,SUM(PRF003) PRF003,PRF001 FROM MIKPRF GROUP BY PRF001) A INNER JOIN MIKPRE B ON A.PRF001=B.PRE004 INNER JOIN TPADEA C ON B.PRE004=C.DEA001 INNER JOIN SGMQAB D ON B.PRE004=D.QAB001 LEFT JOIN TPADDA E ON C.DEA008=E.DDA001 LEFT JOIN TPADGA F ON C.DEA010=F.DGA001 WHERE QAB001 NOT IN (SELECT QAB003 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB003=B.DEA001) " );


            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                int i = 0;
                foreach ( DataRow row in table . Rows )
                {
                    LineProductMesEntityu . SemiProductPlanEntity model = new LineProductMesEntityu . SemiProductPlanEntity ( );
                    i++;
                    model . SEP001 = row [ "PRE004" ] . ToString ( );
                    model . SEP002 = row [ "DEA002" ] . ToString ( );
                    model . SEP003 = row [ "DEA057" ] . ToString ( );
                    model . SEP004 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
                    model . SEP005 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
                    model . SEP006 = Convert . ToDecimal ( row [ "PRE008" ] . ToString ( ) );
                    model . SEP007 = i . ToString ( );
                    model . SEP008 = 0 . ToString ( );
                    model . SEP009 = 1;
                    model . SEP010 = row [ "DDA003" ] . ToString ( );
                    model . SEP011 = row [ "DEA003" ] . ToString ( );
                    model . SEP012 = row [ "DGA003" ] . ToString ( );
                    modelList . Add ( model );
                    if ( piNum == string . Empty )
                        piNum = "'" + model . SEP001 + "'";
                    else
                        piNum = piNum + "," + "'" + model . SEP001 + "'";
                }
            }

            if ( piNum != string . Empty )
            {
                bool state = true;
                while ( state )
                {
                    //展开第一阶
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "SELECT DISTINCT QAB001,QAB003,DEA002,DEA057,QAB005,DDA003,DEA003,DGA003 FROM SGMQAB C INNER JOIN TPADEA D ON C.QAB003=D.DEA001 INNER JOIN TPADDA E ON D.DEA008=E.DDA001 INNER JOIN TPADGA F ON D.DEA010=F.DGA001 WHERE DEA009 IN ('P') AND QAB001 IN ({0}) " ,piNum );

                    DataTable tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                    if ( tableOne != null && tableOne . Rows . Count > 0 )
                    {
                        piNum = string . Empty;
                        int i = 0;
                        foreach ( DataRow row in tableOne . Rows )
                        {
                            LineProductMesEntityu . SemiProductPlanEntity model = new LineProductMesEntityu . SemiProductPlanEntity ( );
                            i++;
                            model . SEP001 = row [ "QAB003" ] . ToString ( );
                            LineProductMesEntityu . SemiProductPlanEntity body = modelList . Find ( ( t ) =>
                            {
                                return t . SEP001 . Equals ( row [ "QAB001" ] . ToString ( ) );
                            } );
                            model . SEP002 = row [ "DEA002" ] . ToString ( );
                            model . SEP003 = row [ "DEA057" ] . ToString ( );
                            model . SEP004 = Convert . ToDateTime ( body . SEP004 ) . AddDays ( -1 );
                            model . SEP005 = body . SEP005;
                            model . SEP006 = body . SEP006 * body . SEP009;
                            if ( body != null )
                                model . SEP008 = body . SEP007;
                            else
                                model . SEP008 = 0 . ToString ( );
                            model . SEP007 = model . SEP008 + "_" + i . ToString ( );
                            model . SEP009 = Convert . ToDecimal ( row [ "QAB005" ] );
                            model . SEP010 = row [ "DDA003" ] . ToString ( );
                            model . SEP011 = row [ "DEA003" ] . ToString ( );
                            model . SEP012 = row [ "DGA003" ] . ToString ( );
                            modelList . Add ( model );
                            if ( piNum == string . Empty )
                                piNum = "'" + model . SEP001 + "'";
                            else
                                piNum = piNum + "," + "'" + model . SEP001 + "'";
                        }
                    }
                    else
                        state = false;
                }
            }

            //订单采购内容，不要
            //strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT IBB003,IBB004,DEA057,DATEADD(DAY,-2,CONVERT(DATE,IBB013,112)) PRE005,CONVERT(FLOAT,IBB006) PRE007,CONVERT(FLOAT,IBB006) QAB005,DDA003,DEA003,DGA003 FROM DCSIBB A INNER JOIN TPADEA B ON A.IBB003=B.DEA001 INNER JOIN TPADDA E ON B.DEA008=E.DDA001 INNER JOIN TPADGA F ON B.DEA010=F.DGA001 WHERE IBB015 IN('Y','F') AND DEA009='P' AND YEAR(IBB013)>=2018 AND MONTH(IBB013)>=6" );
            //table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            //if ( table != null && table . Rows . Count > 0 )
            //{
            //    int i = 0;
            //    foreach ( DataRow row in table . Rows )
            //    {
            //        LineProductMesEntityu . SemiProductPlanEntity model = new LineProductMesEntityu . SemiProductPlanEntity ( );
            //        i++;
            //        model . SEP001 = row [ "IBB003" ] . ToString ( );
            //        model . SEP002 = row [ "IBB004" ] . ToString ( );
            //        model . SEP003 = row [ "DEA057" ] . ToString ( );
            //        model . SEP004 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
            //        model . SEP005 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
            //        model . SEP006 = Convert . ToDecimal ( row [ "QAB005" ] . ToString ( ) );
            //        model . SEP007 = i . ToString ( ) + "+";
            //        model . SEP008 = model . SEP007;
            //        model . SEP009 = 1;
            //        model . SEP010 = row [ "DGA003" ] . ToString ( );
            //        model . SEP011 = row [ "DDA003" ] . ToString ( );
            //        model . SEP012 = row [ "DEA003" ] . ToString ( );
            //        modelList . Add ( model );
            //        if ( piNum == string . Empty )
            //            piNum = "'" + model . SEP001 + "'";
            //        else
            //            piNum = piNum + "," + "'" + model . SEP001 + "'";
            //    }
            //}

            return modelList;
        }

        /// <summary>
        /// 获取采购件
        /// </summary>
        /// <returns></returns>
        public DataTable getListPurAll ( string proName,DateTime dtStart,DateTime dtEnd )
        {
            SqlHelper . StoreProcedure ( "CNSCG" );
            SqlParameter [ ] parameter = {
                new SqlParameter("PRONAME",proName ),
                new SqlParameter("DTSTART",dtStart ),
                new SqlParameter("DTEND",dtEnd )
                };
            
            return SqlHelper . ExecuteNoStoreTable ( parameter );
        }

        /*
         CREATE PROC [dbo].[CNSCG]
        AS
DECLARE @i int
DECLARE @j int
DECLARE @k int
DECLARE @count int
DECLARE @strCN NVARCHAR (4000)
DECLARE @SqlStr NVARCHAR (4000)
BEGIN
SET @j=0;
SET @k=0;
        SET @count=1;
        IF object_id ( N'#B',N'U') is not null
DROP TABLE #B
ELSE
CREATE TABLE #B(
ROW INT,
QAB003 NVARCHAR (20),
DEA002 NVARCHAR (50),
DEA057 NVARCHAR (50),
PRE005 DATE,
PRE007 INT,
QAB005 DECIMAL (18,6),
QAB006 DECIMAL (18,6)
)
INSERT INTO #B 
SELECT @j,QAB003,D.DEA002,D . DEA057,DATEADD ( day,-1,E . PRE005) PRE005,PRE007,CONVERT ( FLOAT,PRE008* QAB005 ) QAB005,0 QAB006 FROM SGMQAB C INNER JOIN TPADEA D ON C.QAB003=D . DEA001 INNER JOIN (SELECT PRE004 ,DEA002,DEA057,PRE005,PRE007,PRE008 FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001= B . PRE001 INNER JOIN TPADEA C ON B.PRE004= C . DEA001 INNER JOIN SGMQAA D ON B.PRE004= D . QAA001 WHERE PRD003 = 0 AND DEA076 = '0507' AND DEA009 IN ('M','S') AND QAA001 NOT IN ( SELECT QAB003 FROM SGMQAB A INNER JOIN TPADEA B ON A . QAB003= B . DEA001 )) E ON C . QAB001=E.PRE004 WHERE DEA009 IN ('P')

SET @i=1;
        WHILE @i=1
BEGIN
SET @j=@j+1;
PRINT @j;
        INSERT INTO #B 
SELECT @j,C.QAB003,D . DEA002,D . DEA057,DATEADD ( day,-1,E . PRE005) PRE005,PRE007,CONVERT ( FLOAT,C . QAB005* E.QAB005) QAB005,0 QAB006 FROM SGMQAB C INNER JOIN TPADEA D ON C.QAB003=D . DEA001 INNER JOIN (SELECT QAB003 ,DEA002,DEA057,PRE005,PRE007,QAB005 FROM #B WHERE ROW=@k) E ON C.QAB001=E.QAB003  collate Chinese_PRC_CI_AI_WS WHERE DEA009 IN ('P') 
SET @k = @k + 1;
        SELECT @count=COUNT(1) FROM SGMQAB C INNER JOIN TPADEA D ON C . QAB003=D.DEA001 INNER JOIN ( SELECT QAB003 ,DEA002,DEA057,PRE005,PRE007,QAB005 FROM #B WHERE ROW=@k) E ON C.QAB001=E.QAB003 collate Chinese_PRC_CI_AI_WS WHERE DEA009 IN ('P') 
IF(@count= 0 )
SET @i=0;
        END
        UPDATE #B SET QAB006=B.QAB005 FROM (SELECT SUM(QAB005) QAB005,QAB003 FROM #B GROUP BY QAB003) B WHERE #B.QAB003=B.QAB003
SELECT @strCN=ISNULL(@strCN+',','')+ QUOTENAME(PRE005) FROM  #B GROUP BY PRE005	
SET @SqlStr='
WITH CET AS (
SELECT QAB003 品号,DEA002 品名 ,DEA057 规格 ,PRE005,CONVERT(FLOAT,QAB005) 单量,CONVERT ( FLOAT,QAB006) 总量 FROM #B)
SELECT * FROM CET PIVOT ( SUM(单量) FOR PRE005 IN ('+@strCN+')) AS T'
EXEC ( @SqlStr);
        END
GO
         */


       /// <summary>
       /// 获取所有采购件
       /// </summary>
       /// <returns></returns>
        public DataTable getProTableP ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT QAB001,DEA002 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB001=B.DEA001 WHERE DEA009='P' UNION SELECT QAB003,DEA002 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB003=B.DEA001 WHERE DEA009='P'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取所有采购件
        /// </summary>
        /// <returns></returns>
        public DataTable getProTableMS ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT QAB001,DEA002 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB001=B.DEA001 WHERE DEA009 IN ('M','S') UNION SELECT QAB003,DEA002 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB003=B.DEA001 WHERE DEA009 IN ('M','S')" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
