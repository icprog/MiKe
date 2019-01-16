using System . Text;
using StudentMgr;
using System . Data;
using System;
using System . Collections . Generic;
using System . Data . SqlClient;
using System . Collections;
using DevExpress . XtraEditors;

namespace LineProductMesBll . Dao
{
    public class ProductPlanPreviewDao
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( string proName ,DateTime dtStart,DateTime dtEnd )
        {
            SqlHelper . StoreProcedure ( "PLANMAIN" );
            SqlParameter [ ] parameter = {
                new SqlParameter("PRODUCTINFO",proName ),
                new SqlParameter("DTSTART",dtStart ),
                new SqlParameter("DTEND",dtEnd )
                };
            
            return SqlHelper . ExecuteNoStoreTable ( parameter );

            //POVIT 行转列  2005以后有此语法  2008 R2 32bit 无法用
            //StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "DECLARE @strCN NVARCHAR(4000) " );
            //strSql . AppendFormat ( "SELECT @strCN=ISNULL(@strCN+',','')+ QUOTENAME(PRF002) FROM MIKPRF WHERE {0} GROUP BY PRF002 ORDER BY PRF002 " ,strWhereS );
            //strSql . AppendFormat ( "DECLARE @SqlStr NVARCHAR(4000) SET @SqlStr='WITH CET AS (SELECT PRF001 主件品号,DEA002 主件品名,PRF002,PRF003 FROM MIKPRF A INNER JOIN TPADEA B ON A.PRF001=B.DEA001 WHERE {0}),CFT AS (SELECT PRF001,SUM(PRF003) PRF003 FROM MIKPRF WHERE {0} GROUP BY PRF001),CHT AS(SELECT A.*,B.PRF003 排产量 FROM CET A INNER JOIN CFT B ON A.主件品号=B.PRF001) ,CKT AS (SELECT PRF001,B.IBB006 FROM MIKPRF A INNER JOIN DCSIBB B ON A.PRF001=B.IBB003 WHERE IBB015=''N'' AND {0} GROUP BY PRF001,B.IBB006),CGT AS(SELECT PRF001,CONVERT(FLOAT,SUM(IBB006)) 订单量 FROM CKT GROUP BY PRF001),CJT AS (SELECT DISTINCT A.PRF001,CONVERT(FLOAT,RAA018-RAA019) RAA FROM MIKPRF A INNER JOIN SGMRAA B ON A.PRF001=B.RAA015 WHERE {0}),CLT AS(SELECT PRF001,SUM(RAA) 预计生产量 FROM CJT GROUP BY PRF001),A AS(SELECT DISTINCT PRF001,LOA003 FROM MIKPRF A INNER JOIN JSKLOA B ON A.PRF001=B.LOA001 WHERE {0}),B AS(SELECT PRF001,CONVERT(FLOAT,SUM(LOA003)) 库存量 FROM A GROUP BY PRF001),CMT AS(SELECT A.*,B.订单量,C.预计生产量,D.库存量,D.库存量+C.预计生产量-B.订单量 库存可用量,B.订单量-A.排产量 未排量 FROM CHT A INNER JOIN CGT B ON A.主件品号=B.PRF001 INNER JOIN CLT C ON A.主件品号=C.PRF001 INNER JOIN B D ON A.主件品号=D.PRF001) SELECT * FROM CMT PIVOT ( MAX(PRF003) FOR PRF002 IN ('+@strCN+') ) AS T ' " ,strWhere );
            //strSql . Append ( "EXEC(@sqlstr)" );


            //行转列  CASE WHEN 
            /*
            CREATE PROC PLANMAIN
@PRODUCTINFO NVARCHAR(100),
@DTSTART DATE,
@DTEND DATE
AS
DECLARE @strCN VARCHAR(8000); 
DECLARE @DTST NVARCHAR(100);
DECLARE @DTEN NVARCHAR(100);
IF object_id ( N'#C',N'U') is not null
DROP TABLE #C
ELSE
CREATE TABLE #C(
主件品号 NVARCHAR (100),
主件品名 NVARCHAR (100),
PRF002  DATE,
PRF003 INT,
排产量 INT,
订单量 INT,
预计生产量 INT,
库存量 INT,
库存可用量 INT,
未排量 INT
)
SET @DTST=CONVERT(NVARCHAR(100),@DTSTART)
SET @DTEN=CONVERT(NVARCHAR(100),@DTEND)
PRINT @DTST;
PRINT @DTEN;
IF(@PRODUCTINFO='')
SET @strCN='WITH CET AS (SELECT PRF001 主件品号,DEA002 主件品名,PRF002,PRF003 FROM MIKPRF A INNER JOIN TPADEA B ON A.PRF001=B.DEA001 WHERE PRF002>='''+@DTST+''' AND PRF002<='''+@DTEN+'''),'
ELSE
SET @strCN='WITH CET AS (SELECT PRF001 主件品号,DEA002 主件品名,PRF002,PRF003 FROM MIKPRF A INNER JOIN TPADEA B ON A.PRF001=B.DEA001 WHERE PRF002>='''+@DTST+''' AND PRF002<='''+@DTEN+''' AND PRF001='''+@PRODUCTINFO+'''),'
SET @strCN=@strCN+'
CFT AS (SELECT PRF001,SUM(PRF003) PRF003 FROM MIKPRF WHERE PRF002>='''+@DTST+''' AND PRF002<='''+@DTEN+''' GROUP BY PRF001),CHT AS(SELECT A.*,B.PRF003 排产量 FROM CET A INNER JOIN CFT B ON A.主件品号=B.PRF001) ,CKT AS (SELECT PRF001,B.IBB006 FROM MIKPRF A INNER JOIN DCSIBB B ON A.PRF001=B.IBB003 WHERE IBB015=''N'' AND 1=1 AND PRF002>='''+@DTST+''' AND PRF002<='''+@DTEN+''' GROUP BY PRF001,B.IBB006),CGT AS(SELECT PRF001,CONVERT(FLOAT,SUM(IBB006)) 订单量 FROM CKT GROUP BY PRF001),CJT AS (SELECT DISTINCT A.PRF001,CONVERT(FLOAT,RAA018-RAA019) RAA FROM MIKPRF A INNER JOIN SGMRAA B ON A.PRF001=B.RAA015 WHERE 1=1 AND PRF002>='''+@DTST+''' AND PRF002<='''+@DTEN+'''),CLT AS(SELECT PRF001,SUM(RAA) 预计生产量 FROM CJT GROUP BY PRF001),A AS(SELECT DISTINCT PRF001,LOA003 FROM MIKPRF A INNER JOIN JSKLOA B ON A.PRF001=B.LOA001 WHERE 1=1 AND PRF002>='''+@DTST+''' AND PRF002<='''+@DTEN+'''),B AS(SELECT PRF001,CONVERT(FLOAT,SUM(LOA003)) 库存量 FROM A GROUP BY PRF001),CMT AS(SELECT A.*,B.订单量,C.预计生产量,D.库存量,D.库存量+C.预计生产量-B.订单量 库存可用量,B.订单量-A.排产量 未排量 FROM CHT A INNER JOIN CGT B ON A.主件品号=B.PRF001 INNER JOIN CLT C ON A.主件品号=C.PRF001 INNER JOIN B D ON A.主件品号=D.PRF001) SELECT * FROM CMT'
INSERT INTO #C
EXEC (@strCN)
SET @strCN='SELECT 主件品号,主件品名,排产量,订单量,预计生产量,库存量,库存可用量,未排量,';
SELECT @strCN=@strCN+'SUM(CASE PRF002 WHEN PRF002 THEN PRF003 ELSE NULL END) AS '''+ QUOTENAME(PRF002)+''',' FROM (SELECT DISTINCT PRF002 FROM #C) AS  a
SELECT @strCN=LEFT(@strCN,LEN(@strCN)-1)+' FROM #C GROUP BY 主件品号,主件品名,排产量,订单量,预计生产量,库存量,库存可用量,未排量'
EXEC(@strCN)
GO
             */

            //DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            //return table;
        }

        /// <summary>
        /// 读取排产数据
        /// </summary>
        /// <returns></returns>
        public bool ReadPlanData ( )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT PRE004,PRE005,SUM(PRE008) PRE008 FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001=B.PRE001 WHERE PRD003=0 GROUP BY PRE004,PRE005 ORDER BY PRE004,PRE005" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return true;

            strSql = new StringBuilder ( );
            strSql . Append ( "SELECT PRF001,PRF002,PRF003 FROM MIKPRF ORDER BY PRF001" );
            DataTable tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            bool result = true;
            if ( tableOne == null || tableOne . Rows . Count < 1 )
                result = false;

            LineProductMesEntityu . ProductPlanBodyEntity model = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            LineProductMesEntityu . ProductPlanPreviewEntity entity = new LineProductMesEntityu . ProductPlanPreviewEntity ( );
            foreach ( DataRow row in table . Rows )
            {
                model . PRE004 = row [ "PRE004" ] . ToString ( );
                model . PRE005 = Convert . ToDateTime ( row [ "PRE005" ] );
                model . PRE008 = Convert . ToInt32 ( row [ "PRE008" ] );
                if ( result == false )
                    Addprf ( SQLString ,model );
                else
                {
                    if ( tableOne . Select ( "PRF001='" + model . PRE004 + "' AND PRF002='" + model . PRE005 + "'" ) . Length < 1 )
                        Addprf ( SQLString ,model );
                    else
                        Editprf ( SQLString ,model );
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void Addprf ( Dictionary<object ,object> SQLString ,LineProductMesEntityu.ProductPlanBodyEntity model)
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPRF(" );
            strSql . Append ( "PRF001,PRF002,PRF003)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PRF001,@PRF002,@PRF003)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRF001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRF002", SqlDbType.Date,3),
                    new SqlParameter("@PRF003", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . PRE004;
            parameters [ 1 ] . Value = model . PRE005;
            parameters [ 2 ] . Value = model . PRE008;
            SQLString . Add ( strSql ,parameters );
        }
        void Editprf ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPRF set " );
            strSql . Append ( "PRF003=@PRF003 " );
            strSql . Append ( "WHERE PRF001=@PRF001 AND PRF002=@PRF002" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRF001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRF002", SqlDbType.Date,3),
                    new SqlParameter("@PRF003", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . PRE004;
            parameters [ 1 ] . Value = model . PRE005;
            parameters [ 2 ] . Value = model . PRE008;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( DataTable table )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT PRF001,PRF002,PRF003 FROM MIKPRF ORDER BY PRF001" );
            DataTable tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            LineProductMesEntityu . ProductPlanPreviewEntity model = new LineProductMesEntityu . ProductPlanPreviewEntity ( );
            foreach ( DataRow row in table . Rows )
            {
                foreach ( DataColumn column in table . Columns )
                {
                    if ( column . ColumnName == "主件品号" )
                        model . PRF001 = row [ column ] . ToString ( );
                    else if ( column . ColumnName != "主件品名" && column . ColumnName != "TOTAL" && column . ColumnName != "生产车间" && column . ColumnName != "仓库" && column . ColumnName != "单位" && column . ColumnName != "排产量" && column . ColumnName != "订单量" && column . ColumnName != "预计生产量" && column . ColumnName != "库存量" && column . ColumnName != "库存可用量" && column . ColumnName != "未排量" && column . ColumnName != "开单未入量" && column . ColumnName != "客户名称" )
                    {
                        model . PRF002 = Convert . ToDateTime ( column . ColumnName );
                        //if ( row [ column ] != null && row [ column ] . ToString ( ) != string . Empty )
                        //{
                            model . PRF003 =string.IsNullOrEmpty( row [ column ] . ToString ( ) ) ==true?0: Convert . ToInt32 ( row [ column ] );
                            if ( tableOne != null && tableOne . Rows . Count > 0 )
                            {
                                if ( tableOne . Select ( "PRF001='" + model . PRF001 + "' AND PRF002='" + model . PRF002 + "'" ) . Length < 1 )
                                    Addprf ( SQLString ,model );
                                else
                                    Editprf ( SQLString ,model );
                            }
                        //}
                    }
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void Addprf ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanPreviewEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPRF(" );
            strSql . Append ( "PRF001,PRF002,PRF003)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PRF001,@PRF002,@PRF003)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRF001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRF002", SqlDbType.Date,3),
                    new SqlParameter("@PRF003", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . PRF001;
            parameters [ 1 ] . Value = model . PRF002;
            parameters [ 2 ] . Value = model . PRF003;
            SQLString . Add ( strSql ,parameters );
        }
        void Editprf ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanPreviewEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPRF set " );
            strSql . Append ( "PRF003=@PRF003 " );
            strSql . Append ( "WHERE PRF001=@PRF001 AND PRF002=@PRF002" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRF001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRF002", SqlDbType.Date,3),
                    new SqlParameter("@PRF003", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . PRF001;
            parameters [ 1 ] . Value = model . PRF002;
            parameters [ 2 ] . Value = model . PRF003;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 获取品号等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getProPlanInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT PRE004,DEA002 FROM MIKPRE A INNER JOIN TPADEA B ON A.PRE004=B.DEA001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 批量增加排产天数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool SaveDay ( DataTable table )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );

            LineProductMesEntityu . ProductPlanPreviewEntity model = new LineProductMesEntityu . ProductPlanPreviewEntity ( );

            int days = 0;
            foreach ( DataRow row in table . Rows )
            {
                model . PRF001 = row [ "P1" ] . ToString ( );
                model . PRF003 = 0;
                days = string . IsNullOrEmpty ( row [ "P5" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "P5" ] );
                model . PRF002 = getTime ( model . PRF001 );
                if ( model . PRF002 == null )
                    continue;
                for ( int i = 0 ; i < days ; i++ )
                {
                    model . PRF002 = Convert . ToDateTime ( model . PRF002 ) . AddDays ( 1 );
                    Addprf ( SQLString ,model );
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 根据品号获取最大排产日期
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        DateTime? getTime ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(PRF002) PRF002 FROM MIKPRF WHERE PRF001='{0}'" ,piNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
            {
                string time = table . Rows [ 0 ] [ "PRF002" ] . ToString ( );
                if ( time == null )
                    return null;
                else
                    return Convert . ToDateTime ( time );
            }

        }


    }
}
