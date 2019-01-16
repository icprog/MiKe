using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class InjectionMoldingDao
    {
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
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT DISTINCT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART04,0) DEA050,RAA008,CONVERT(FLOAT,ISNULL(ART004,0)) ART004 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN MIKART C ON A.RAA015=C.ART001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART04 FROM MIKART GROUP BY ART001) D ON A.RAA015=D.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0502' AND RAA020='N' AND RAA024='T'" );
            strSql . Append ( "SELECT DISTINCT RAA001,RAA015,DEA002,DEA003,DEA057,CONVERT(FLOAT,RAA018) RAA018,ISNULL(ART04,0) DEA050,RAA008,CONVERT(FLOAT,ISNULL(ART004,0)) ART004,DDA003 FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 LEFT JOIN MIKART C ON A.RAA015=C.ART001 LEFT JOIN (SELECT ART001,CONVERT(FLOAT,SUM(ART004)) ART04 FROM MIKART GROUP BY ART001) D ON A.RAA015=D.ART001 LEFT JOIN TPADDA E ON B.DEA008=E.DDA001 WHERE DEA009 IN ('M','S') AND DEA076='0502' AND RAA020='N' AND RAA024='T'" );

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
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP023,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1" );

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
            strSql . AppendFormat ( "DELETE FROM MIKIJA WHERE IJA001='{0}'" ,oddNum );

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
            strSql . Append ( "UPDATE MIKIJA SET IJA010=@IJA010 WHERE IJA001=@IJA001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@IJA001",SqlDbType.NVarChar,20),
                new SqlParameter("@IJA010",SqlDbType.Bit)
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
            strSql . Append ( "UPDATE MIKIJA SET IJA011=@IJA011 WHERE IJA001=@IJA001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@IJA001",SqlDbType.NVarChar,20),
                new SqlParameter("@IJA011",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = oddNum;
            parameter [ 1 ] . Value = result;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            return GetCodeUtils . getOddNum ( "MIKIJA" ,"IJA001" );
            //StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "SELECT MAX(IJA001) IJA001 FROM MIKIJA" );

            //DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            //if ( table == null || table . Rows . Count < 0 )
            //    return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            //else
            //{
            //    string code = table . Rows [ 0 ] [ "IJA001" ] . ToString ( );
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
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . InjectionMoldingHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJA001,IJA002,IJA003,IJA004,IJA005,IJA006,IJA007,IJA008,IJA009,IJA010,IJA011,IJA012,IJA013,IJA014,IJA015,IJA016,IJA017 FROM MIKIJA WHERE IJA001='{0}'" ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
                return getModel ( table . Rows [ 0 ] );
        }

        public LineProductMesEntityu . InjectionMoldingHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . InjectionMoldingHeaderEntity model = new LineProductMesEntityu . InjectionMoldingHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "IJA001" ] != null )
                {
                    model . IJA001 = row [ "IJA001" ] . ToString ( );
                }
                if ( row [ "IJA002" ] != null )
                {
                    model . IJA002 = row [ "IJA002" ] . ToString ( );
                }
                if ( row [ "IJA003" ] != null )
                {
                    model . IJA003 = row [ "IJA003" ] . ToString ( );
                }
                if ( row [ "IJA004" ] != null )
                {
                    model . IJA004 = row [ "IJA004" ] . ToString ( );
                }
                if ( row [ "IJA005" ] != null )
                {
                    model . IJA005 = row [ "IJA005" ] . ToString ( );
                }
                if ( row [ "IJA006" ] != null )
                {
                    model . IJA006 = row [ "IJA006" ] . ToString ( );
                }
                if ( row [ "IJA007" ] != null && row [ "IJA007" ] . ToString ( ) != "" )
                {
                    model . IJA007 = DateTime . Parse ( row [ "IJA007" ] . ToString ( ) );
                }
                if ( row [ "IJA008" ] != null )
                {
                    model . IJA008 = row [ "IJA008" ] . ToString ( );
                }
                if ( row [ "IJA009" ] != null )
                {
                    model . IJA009 = row [ "IJA009" ] . ToString ( );
                }
                if ( row [ "IJA010" ] != null && row [ "IJA010" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "IJA010" ] . ToString ( ) == "1" ) || ( row [ "IJA010" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . IJA010 = true;
                    }
                    else
                    {
                        model . IJA010 = false;
                    }
                }
                if ( row [ "IJA011" ] != null && row [ "IJA011" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "IJA011" ] . ToString ( ) == "1" ) || ( row [ "IJA011" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . IJA011 = true;
                    }
                    else
                    {
                        model . IJA011 = false;
                    }
                }
                if ( row [ "IJA012" ] != null && row [ "IJA012" ] . ToString ( ) != "" )
                {
                    model . IJA012 = decimal . Parse ( row [ "IJA012" ] . ToString ( ) );
                }
                if ( row [ "IJA013" ] != null && row [ "IJA013" ] . ToString ( ) != "" )
                {
                    model . IJA013 = decimal . Parse ( row [ "IJA013" ] . ToString ( ) );
                }
                if ( row [ "IJA014" ] != null && row [ "IJA014" ] . ToString ( ) != "" )
                {
                    model . IJA014 = row [ "IJA014" ] . ToString ( );
                }
                if ( row [ "IJA015" ] != null && row [ "IJA015" ] . ToString ( ) != "" )
                {
                    model . IJA015 = DateTime . Parse ( row [ "IJA015" ] . ToString ( ) );
                }
                if ( row [ "IJA016" ] != null && row [ "IJA016" ] . ToString ( ) != "" )
                {
                    model . IJA016 = DateTime . Parse ( row [ "IJA016" ] . ToString ( ) );
                }
                if ( row [ "IJA017" ] != null )
                {
                    model . IJA017 = row [ "IJA017" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableHeader ( string strWhere,string strWhere1 ,string strWhere2 )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJA001,IJA007,IJA010,IJA011,IJB004,IJB005,IJB006,IJB010,IJB015,IJA002 FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 WHERE {0} UNION SELECT IJC001,IJA007,IJA010,IJA011,IJC002,IJC003,IJC004,IJC008,IJC010,IJA002 FROM MIKIJC A INNER JOIN MIKIJA B ON A.IJC001=B.IJA001 WHERE {1} UNION SELECT IJD001,IJA007,IJA010,IJA011,'' IJC002,''IJC003,'' IJC004,''IJC008,'' IJC010,IJA002 FROM MIKIJD A INNER JOIN MIKIJA B ON A.IJD001=B.IJA001 WHERE {2}" ,strWhere ,strWhere1 ,strWhere2 );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,IJB001,IJB002,IJB003,IJB004,IJB005,IJB006,IJB007,IJB008,IJB009,IJB010,IJB011,IJB012,IJB013,IJB014,IJB015,IJB016,IJB017,IJB018,IJB019,IJB020,IJB021,IJB022,IJB023,IJB024,IJB025,IJB026,IJB027,IJB028,0 U4 FROM MIKIJB WHERE IJB001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableTwo ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,IJC001,IJC002,IJC003,IJC004,IJC005,IJC006,IJC007,IJC008,IJC009,IJC010,IJC011,IJC012,0 U4 FROM MIKIJC WHERE IJC001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,IJD001,IJD002,IJD003,IJD004,IJD005,IJD006,IJD007,IJD008,IJD009,IJD010,IJD011,IJD012,IJD013,IJD014 FROM  MIKIJD WHERE IJD001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="tableViewTre"></param>
        /// <returns></returns>
        public bool Save (LineProductMesEntityu.InjectionMoldingHeaderEntity model,DataTable tableViewOne,DataTable tableViewTwo,DataTable tableViewTre )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            model . IJA001 = getOddNum ( );
            model . IJA014 = UserInfoMation . userName;
            AddHeader ( SQLString ,model );
            UserInfoMation . oddNum = model . IJA001;

            if ( model . IJA002 . Equals ( "计件" ) )
            {
                LineProductMesEntityu . InjectionMoldingBodyOneEntity modelOne = new LineProductMesEntityu . InjectionMoldingBodyOneEntity ( );
                foreach ( DataRow row in tableViewOne . Rows )
                {
                    modelOne . IJB001 = model . IJA001;
                    modelOne . IJB002 = row [ "IJB002" ] . ToString ( );
                    modelOne . IJB003 = row [ "IJB003" ] . ToString ( );
                    modelOne . IJB004 = row [ "IJB004" ] . ToString ( );
                    modelOne . IJB005 = row [ "IJB005" ] . ToString ( );
                    modelOne . IJB006 = row [ "IJB006" ] . ToString ( );
                    modelOne . IJB007 = row [ "IJB007" ] . ToString ( );
                    modelOne . IJB008 = row [ "IJB008" ] . ToString ( );
                    modelOne . IJB009 = string . IsNullOrEmpty ( row [ "IJB009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB009" ] . ToString ( ) );
                    modelOne . IJB010 = string . IsNullOrEmpty ( row [ "IJB010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJB010" ] . ToString ( ) );
                    modelOne . IJB011 = string . IsNullOrEmpty ( row [ "IJB011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB011" ] . ToString ( ) );
                    modelOne . IJB012 = row [ "IJB012" ] . ToString ( );
                    modelOne . IJB013 = row [ "IJB013" ] . ToString ( );
                    modelOne . IJB014 = row [ "IJB014" ] . ToString ( );
                    modelOne . IJB015 = string . IsNullOrEmpty ( row [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJB015" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB016" ] . ToString ( ) ) )
                        modelOne . IJB016 = null;
                    else
                        modelOne . IJB016 = Convert . ToDateTime ( row [ "IJB016" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB017" ] . ToString ( ) ) )
                        modelOne . IJB017 = null;
                    else
                        modelOne . IJB017 = Convert . ToDateTime ( row [ "IJB017" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB018" ] . ToString ( ) ) )
                        modelOne . IJB018 = null;
                    else
                        modelOne . IJB018 = Convert . ToDateTime ( row [ "IJB018" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB019" ] . ToString ( ) ) )
                        modelOne . IJB019 = null;
                    else
                        modelOne . IJB019 = Convert . ToDateTime ( row [ "IJB019" ] . ToString ( ) );
                    modelOne . IJB020 = string . IsNullOrEmpty ( row [ "IJB020" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB020" ] . ToString ( ) );
                    modelOne . IJB021 = row [ "IJB021" ] . ToString ( );
                    modelOne . IJB022 = row [ "IJB022" ] . ToString ( );
                    modelOne . IJB023 = string . IsNullOrEmpty ( row [ "IJB023" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB023" ] . ToString ( ) );
                    modelOne . IJB024 = string . IsNullOrEmpty ( row [ "IJB024" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB024" ] . ToString ( ) );
                    modelOne . IJB025 = string . IsNullOrEmpty ( row [ "IJB025" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB025" ] . ToString ( ) );
                    modelOne . IJB026 = row [ "IJB026" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( row [ "IJB027" ] . ToString ( ) ) )
                        modelOne . IJB027 = null;
                    else
                        modelOne . IJB027 = Convert . ToDecimal ( row [ "IJB027" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB028" ] . ToString ( ) ) )
                        modelOne . IJB028 = null;
                    else
                        modelOne . IJB028 = Convert . ToDecimal ( row [ "IJB028" ] . ToString ( ) );
                    AddBodyOne ( SQLString ,modelOne );
                }
            }
            else
            {
                LineProductMesEntityu . InjectionMoldingBodyTwoEntity modelTwo = new LineProductMesEntityu . InjectionMoldingBodyTwoEntity ( );
                foreach ( DataRow row in tableViewTwo . Rows )
                {
                    modelTwo . IJC001 = model . IJA001;
                    modelTwo . IJC002 = row [ "IJC002" ] . ToString ( );
                    modelTwo . IJC003 = row [ "IJC003" ] . ToString ( );
                    modelTwo . IJC004 = row [ "IJC004" ] . ToString ( );
                    modelTwo . IJC005 = row [ "IJC005" ] . ToString ( );
                    modelTwo . IJC006 = row [ "IJC006" ] . ToString ( );
                    modelTwo . IJC007 = string . IsNullOrEmpty ( row [ "IJC007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJC007" ] . ToString ( ) );
                    modelTwo . IJC008 = string . IsNullOrEmpty ( row [ "IJC008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJC008" ] . ToString ( ) );
                    modelTwo . IJC009 = string . IsNullOrEmpty ( row [ "IJC009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJC009" ] . ToString ( ) );
                    modelTwo . IJC010 = string . IsNullOrEmpty ( row [ "IJC010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJC010" ] . ToString ( ) );
                    modelTwo . IJC011 = row [ "IJC011" ] . ToString ( );
                    modelTwo . IJC012 = row [ "IJC012" ] . ToString ( );
                    AddBodyTwo ( SQLString ,modelTwo );
                }

                LineProductMesEntityu . InjectionMoldingBodyTreEntity modelTre = new LineProductMesEntityu . InjectionMoldingBodyTreEntity ( );
                foreach ( DataRow row in tableViewTre . Rows )
                {
                    modelTre . IJD001 = model . IJA001;
                    modelTre . IJD002 = row [ "IJD002" ] . ToString ( );
                    modelTre . IJD003 = row [ "IJD003" ] . ToString ( );
                    modelTre . IJD004 = row [ "IJD004" ] . ToString ( );
                    modelTre . IJD005 = row [ "IJD005" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( row [ "IJD006" ] . ToString ( ) ) )
                        modelTre . IJD006 = null;
                    else
                        modelTre . IJD006 = Convert . ToDateTime ( row [ "IJD006" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJD007" ] . ToString ( ) ) )
                        modelTre . IJD007 = null;
                    else
                        modelTre . IJD007 = Convert . ToDateTime ( row [ "IJD007" ] . ToString ( ) );
                    modelTre . IJD008 = string . IsNullOrEmpty ( row [ "IJD008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD008" ] . ToString ( ) );
                    modelTre . IJD009 = row [ "IJD009" ] . ToString ( );
                    modelTre . IJD010 = row [ "IJD010" ] . ToString ( );
                    modelTre . IJD011 = row [ "IJD011" ] . ToString ( );
                    modelTre . IJD012 = string . IsNullOrEmpty ( row [ "IJD012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD012" ] . ToString ( ) );
                    modelTre . IJD013 = string . IsNullOrEmpty ( row [ "IJD013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD013" ] . ToString ( ) );
                    AddBodyTre ( SQLString ,modelTre );
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="tableViewTre"></param>
        /// <param name="idxOne"></param>
        /// <param name="idxTwo"></param>
        /// <param name="idxTre"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . InjectionMoldingHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo ,DataTable tableViewTre,List<string> idxOne,List<string> idxTwo,List<string> idxTre )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            EditHeader ( SQLString ,model );

            if ( model . IJA002 . Equals ( "计件" ) )
            {
                LineProductMesEntityu . InjectionMoldingBodyOneEntity modelOne = new LineProductMesEntityu . InjectionMoldingBodyOneEntity ( );
                foreach ( DataRow row in tableViewOne . Rows )
                {
                    modelOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    modelOne . IJB001 = model . IJA001;
                    modelOne . IJB002 = row [ "IJB002" ] . ToString ( );
                    modelOne . IJB003 = row [ "IJB003" ] . ToString ( );
                    modelOne . IJB004 = row [ "IJB004" ] . ToString ( );
                    modelOne . IJB005 = row [ "IJB005" ] . ToString ( );
                    modelOne . IJB006 = row [ "IJB006" ] . ToString ( );
                    modelOne . IJB007 = row [ "IJB007" ] . ToString ( );
                    modelOne . IJB008 = row [ "IJB008" ] . ToString ( );
                    modelOne . IJB009 = string . IsNullOrEmpty ( row [ "IJB009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB009" ] . ToString ( ) );
                    modelOne . IJB010 = string . IsNullOrEmpty ( row [ "IJB010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJB010" ] . ToString ( ) );
                    modelOne . IJB011 = string . IsNullOrEmpty ( row [ "IJB011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB011" ] . ToString ( ) );
                    modelOne . IJB012 = row [ "IJB012" ] . ToString ( );
                    modelOne . IJB013 = row [ "IJB013" ] . ToString ( );
                    modelOne . IJB014 = row [ "IJB014" ] . ToString ( );
                    modelOne . IJB015 = string . IsNullOrEmpty ( row [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJB015" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB016" ] . ToString ( ) ) )
                        modelOne . IJB016 = null;
                    else
                        modelOne . IJB016 = Convert . ToDateTime ( row [ "IJB016" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB017" ] . ToString ( ) ) )
                        modelOne . IJB017 = null;
                    else
                        modelOne . IJB017 = Convert . ToDateTime ( row [ "IJB017" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB018" ] . ToString ( ) ) )
                        modelOne . IJB018 = null;
                    else
                        modelOne . IJB018 = Convert . ToDateTime ( row [ "IJB018" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB019" ] . ToString ( ) ) )
                        modelOne . IJB019 = null;
                    else
                        modelOne . IJB019 = Convert . ToDateTime ( row [ "IJB019" ] . ToString ( ) );
                    modelOne . IJB020 = string . IsNullOrEmpty ( row [ "IJB020" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB020" ] . ToString ( ) );
                    modelOne . IJB021 = row [ "IJB021" ] . ToString ( );
                    modelOne . IJB022 = row [ "IJB022" ] . ToString ( );
                    modelOne . IJB023 = string . IsNullOrEmpty ( row [ "IJB023" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB023" ] . ToString ( ) );
                    modelOne . IJB024 = string . IsNullOrEmpty ( row [ "IJB024" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB024" ] . ToString ( ) );
                    modelOne . IJB025 = string . IsNullOrEmpty ( row [ "IJB025" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB025" ] . ToString ( ) );
                    modelOne . IJB026 = row [ "IJB026" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( row [ "IJB027" ] . ToString ( ) ) )
                        modelOne . IJB027 = null;
                    else
                        modelOne . IJB027 = Convert . ToDecimal ( row [ "IJB027" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJB028" ] . ToString ( ) ) )
                        modelOne . IJB028 = null;
                    else
                        modelOne . IJB028 = Convert . ToDecimal ( row [ "IJB028" ] . ToString ( ) );
                    if ( modelOne . idx < 1 )
                        AddBodyOne ( SQLString ,modelOne );
                    else
                        EditBodyOne ( SQLString ,modelOne );
                }

                foreach ( string s in idxOne )
                {
                    DeleteOne ( SQLString ,s );
                }

            }
            else
            {
                LineProductMesEntityu . InjectionMoldingBodyTwoEntity modelTwo = new LineProductMesEntityu . InjectionMoldingBodyTwoEntity ( );
                foreach ( DataRow row in tableViewTwo . Rows )
                {
                    modelTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    modelTwo . IJC001 = model . IJA001;
                    modelTwo . IJC002 = row [ "IJC002" ] . ToString ( );
                    modelTwo . IJC003 = row [ "IJC003" ] . ToString ( );
                    modelTwo . IJC004 = row [ "IJC004" ] . ToString ( );
                    modelTwo . IJC005 = row [ "IJC005" ] . ToString ( );
                    modelTwo . IJC006 = row [ "IJC006" ] . ToString ( );
                    modelTwo . IJC007 = string . IsNullOrEmpty ( row [ "IJC007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJC007" ] . ToString ( ) );
                    modelTwo . IJC008 = string . IsNullOrEmpty ( row [ "IJC008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJC008" ] . ToString ( ) );
                    modelTwo . IJC009 = string . IsNullOrEmpty ( row [ "IJC009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJC009" ] . ToString ( ) );
                    modelTwo . IJC010 = string . IsNullOrEmpty ( row [ "IJC010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJC010" ] . ToString ( ) );
                    modelTwo . IJC011 = row [ "IJC011" ] . ToString ( );
                    if ( modelTwo . idx < 1 )
                        AddBodyTwo ( SQLString ,modelTwo );
                    else
                        EditBodyTwo ( SQLString ,modelTwo );
                }

                LineProductMesEntityu . InjectionMoldingBodyTreEntity modelTre = new LineProductMesEntityu . InjectionMoldingBodyTreEntity ( );
                foreach ( DataRow row in tableViewTre . Rows )
                {
                    modelTre . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    modelTre . IJD001 = model . IJA001;
                    modelTre . IJD002 = row [ "IJD002" ] . ToString ( );
                    modelTre . IJD003 = row [ "IJD003" ] . ToString ( );
                    modelTre . IJD004 = row [ "IJD004" ] . ToString ( );
                    modelTre . IJD005 = row [ "IJD005" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( row [ "IJD006" ] . ToString ( ) ) )
                        modelTre . IJD006 = null;
                    else
                        modelTre . IJD006 = Convert . ToDateTime ( row [ "IJD006" ] . ToString ( ) );
                    if ( string . IsNullOrEmpty ( row [ "IJD007" ] . ToString ( ) ) )
                        modelTre . IJD007 = null;
                    else
                        modelTre . IJD007 = Convert . ToDateTime ( row [ "IJD007" ] . ToString ( ) );
                    modelTre . IJD008 = string . IsNullOrEmpty ( row [ "IJD008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD008" ] . ToString ( ) );
                    modelTre . IJD009 = row [ "IJD009" ] . ToString ( );
                    modelTre . IJD010 = row [ "IJD010" ] . ToString ( );
                    modelTre . IJD011 = row [ "IJD011" ] . ToString ( );
                    modelTre . IJD012 = string . IsNullOrEmpty ( row [ "IJD012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD012" ] . ToString ( ) );
                    modelTre . IJD013 = string . IsNullOrEmpty ( row [ "IJD013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD013" ] . ToString ( ) );
                    if ( modelTre . idx < 1 )
                        AddBodyTre ( SQLString ,modelTre );
                    else
                        EditBodyTre ( SQLString ,modelTre );
                }

                foreach ( string s in idxTwo )
                {
                    DeleteTwo ( SQLString ,s );
                }
                foreach ( string s in idxTre )
                {
                    DeleteTre ( SQLString ,s );
                }

            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );

        }

        void AddHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu.InjectionMoldingHeaderEntity model)
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKIJA(" );
            strSql . Append ( "IJA001,IJA002,IJA003,IJA004,IJA005,IJA006,IJA007,IJA008,IJA009,IJA010,IJA011,IJA012,IJA013,IJA014,IJA015,IJA016,IJA017)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@IJA001,@IJA002,@IJA003,@IJA004,@IJA005,@IJA006,@IJA007,@IJA008,@IJA009,@IJA010,@IJA011,@IJA012,@IJA013,@IJA014,@IJA015,@IJA016,@IJA017)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJA001", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA002", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJA003", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA004", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA005", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA006", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA007", SqlDbType.Date,3),
                    new SqlParameter("@IJA008", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJA009", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJA010", SqlDbType.Bit,1),
                    new SqlParameter("@IJA011", SqlDbType.Bit,1),
                    new SqlParameter("@IJA012", SqlDbType.Decimal),
                    new SqlParameter("@IJA013", SqlDbType.Decimal),
                    new SqlParameter("@IJA014", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA015", SqlDbType.DateTime),
                    new SqlParameter("@IJA016", SqlDbType.DateTime),
                    new SqlParameter("@IJA017", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . IJA001;
            parameters [ 1 ] . Value = model . IJA002;
            parameters [ 2 ] . Value = model . IJA003;
            parameters [ 3 ] . Value = model . IJA004;
            parameters [ 4 ] . Value = model . IJA005;
            parameters [ 5 ] . Value = model . IJA006;
            parameters [ 6 ] . Value = model . IJA007;
            parameters [ 7 ] . Value = model . IJA008;
            parameters [ 8 ] . Value = model . IJA009;
            parameters [ 9 ] . Value = model . IJA010;
            parameters [ 10 ] . Value = model . IJA011;
            parameters [ 11 ] . Value = model . IJA012;
            parameters [ 12 ] . Value = model . IJA013;
            parameters [ 13 ] . Value = model . IJA014;
            parameters [ 14 ] . Value = model . IJA015;
            parameters [ 15 ] . Value = model . IJA016;
            parameters [ 16 ] . Value = model . IJA017;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKIJB(" );
            strSql . Append ( "IJB001,IJB002,IJB003,IJB004,IJB005,IJB006,IJB007,IJB008,IJB009,IJB010,IJB011,IJB012,IJB013,IJB014,IJB015,IJB016,IJB017,IJB018,IJB019,IJB020,IJB021,IJB022,IJB023,IJB024,IJB025,IJB026,IJB027,IJB028)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@IJB001,@IJB002,@IJB003,@IJB004,@IJB005,@IJB006,@IJB007,@IJB008,@IJB009,@IJB010,@IJB011,@IJB012,@IJB013,@IJB014,@IJB015,@IJB016,@IJB017,@IJB018,@IJB019,@IJB020,@IJB021,@IJB022,@IJB023,@IJB024,@IJB025,@IJB026,@IJB027,@IJB028)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJB001", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB002", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB003", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJB004", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB005", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB006", SqlDbType.NVarChar,50),
                    new SqlParameter("@IJB007", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB008", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJB009", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB010", SqlDbType.Int,4),
                    new SqlParameter("@IJB011", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB012", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB013", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB014", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJB015", SqlDbType.Int,4),
                    new SqlParameter("@IJB016", SqlDbType.DateTime),
                    new SqlParameter("@IJB017", SqlDbType.DateTime),
                    new SqlParameter("@IJB018", SqlDbType.DateTime),
                    new SqlParameter("@IJB019", SqlDbType.DateTime),
                    new SqlParameter("@IJB020", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB021", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJB022", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB023", SqlDbType.Decimal),
                    new SqlParameter("@IJB024", SqlDbType.Decimal),
                    new SqlParameter("@IJB025", SqlDbType.Decimal),
                    new SqlParameter("@IJB026", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB027", SqlDbType.Decimal),
                    new SqlParameter("@IJB028", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . IJB001;
            parameters [ 1 ] . Value = model . IJB002;
            parameters [ 2 ] . Value = model . IJB003;
            parameters [ 3 ] . Value = model . IJB004;
            parameters [ 4 ] . Value = model . IJB005;
            parameters [ 5 ] . Value = model . IJB006;
            parameters [ 6 ] . Value = model . IJB007;
            parameters [ 7 ] . Value = model . IJB008;
            parameters [ 8 ] . Value = model . IJB009;
            parameters [ 9 ] . Value = model . IJB010;
            parameters [ 10 ] . Value = model . IJB011;
            parameters [ 11 ] . Value = model . IJB012;
            parameters [ 12 ] . Value = model . IJB013;
            parameters [ 13 ] . Value = model . IJB014;
            parameters [ 14 ] . Value = model . IJB015;
            parameters [ 15 ] . Value = model . IJB016;
            parameters [ 16 ] . Value = model . IJB017;
            parameters [ 17 ] . Value = model . IJB018;
            parameters [ 18 ] . Value = model . IJB019;
            parameters [ 19 ] . Value = model . IJB020;
            parameters [ 20 ] . Value = model . IJB021;
            parameters [ 21 ] . Value = model . IJB022;
            parameters [ 22 ] . Value = model . IJB023;
            parameters [ 23 ] . Value = model . IJB024;
            parameters [ 24 ] . Value = model . IJB025;
            parameters [ 25 ] . Value = model . IJB026;
            parameters [ 26 ] . Value = model . IJB027;
            parameters [ 27 ] . Value = model . IJB028;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingBodyTwoEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKIJC(" );
            strSql . Append ( "IJC001,IJC002,IJC003,IJC004,IJC005,IJC006,IJC007,IJC008,IJC009,IJC010,IJC011,IJC012)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@IJC001,@IJC002,@IJC003,@IJC004,@IJC005,@IJC006,@IJC007,@IJC008,@IJC009,@IJC010,@IJC011,@IJC012)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJC001", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC002", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC003", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC004", SqlDbType.NVarChar,50),
                    new SqlParameter("@IJC005", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC006", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJC007", SqlDbType.Decimal,9),
                    new SqlParameter("@IJC008", SqlDbType.Int,4),
                    new SqlParameter("@IJC009", SqlDbType.Decimal,9),
                    new SqlParameter("@IJC010", SqlDbType.Int,4),
                    new SqlParameter("@IJC011", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJC012", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . IJC001;
            parameters [ 1 ] . Value = model . IJC002;
            parameters [ 2 ] . Value = model . IJC003;
            parameters [ 3 ] . Value = model . IJC004;
            parameters [ 4 ] . Value = model . IJC005;
            parameters [ 5 ] . Value = model . IJC006;
            parameters [ 6 ] . Value = model . IJC007;
            parameters [ 7 ] . Value = model . IJC008;
            parameters [ 8 ] . Value = model . IJC009;
            parameters [ 9 ] . Value = model . IJC010;
            parameters [ 10 ] . Value = model . IJC011;
            parameters [ 11 ] . Value = model . IJC012;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyTre ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingBodyTreEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKIJD(" );
            strSql . Append ( "IJD001,IJD002,IJD003,IJD004,IJD005,IJD006,IJD007,IJD008,IJD009,IJD010,IJD011,IJD012,IJD013)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@IJD001,@IJD002,@IJD003,@IJD004,@IJD005,@IJD006,@IJD007,@IJD008,@IJD009,@IJD010,@IJD011,@IJD012,@IJD013)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJD001", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD002", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD003", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD004", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD005", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJD006", SqlDbType.DateTime),
                    new SqlParameter("@IJD007", SqlDbType.DateTime),
                    new SqlParameter("@IJD008", SqlDbType.Decimal,9),
                    new SqlParameter("@IJD009", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJD010", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJD011", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD012", SqlDbType.Decimal),
                    new SqlParameter("@IJD013", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . IJD001;
            parameters [ 1 ] . Value = model . IJD002;
            parameters [ 2 ] . Value = model . IJD003;
            parameters [ 3 ] . Value = model . IJD004;
            parameters [ 4 ] . Value = model . IJD005;
            parameters [ 5 ] . Value = model . IJD006;
            parameters [ 6 ] . Value = model . IJD007;
            parameters [ 7 ] . Value = model . IJD008;
            parameters [ 8 ] . Value = model . IJD009;
            parameters [ 9 ] . Value = model . IJD010;
            parameters [ 10 ] . Value = model . IJD011;
            parameters [ 11 ] . Value = model . IJD012;
            parameters [ 12 ] . Value = model . IJD013;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKIJA set " );
            strSql . Append ( "IJA002=@IJA002," );
            strSql . Append ( "IJA003=@IJA003," );
            strSql . Append ( "IJA004=@IJA004," );
            strSql . Append ( "IJA005=@IJA005," );
            strSql . Append ( "IJA006=@IJA006," );
            strSql . Append ( "IJA007=@IJA007," );
            strSql . Append ( "IJA008=@IJA008," );
            strSql . Append ( "IJA009=@IJA009," );
            strSql . Append ( "IJA010=@IJA010," );
            strSql . Append ( "IJA011=@IJA011," );
            strSql . Append ( "IJA012=@IJA012," );
            strSql . Append ( "IJA013=@IJA013," );
            strSql . Append ( "IJA015=@IJA015," );
            strSql . Append ( "IJA016=@IJA016," );
            strSql . Append ( "IJA017=@IJA017" );
            strSql . Append ( " where IJA001=@IJA001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJA002", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJA003", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA004", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA005", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA006", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA007", SqlDbType.Date,3),
                    new SqlParameter("@IJA008", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJA009", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJA010", SqlDbType.Bit,1),
                    new SqlParameter("@IJA011", SqlDbType.Bit,1),
                    new SqlParameter("@IJA001", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJA012", SqlDbType.Decimal),
                    new SqlParameter("@IJA013", SqlDbType.Decimal),
                    new SqlParameter("@IJA015", SqlDbType.DateTime),
                    new SqlParameter("@IJA016", SqlDbType.DateTime),
                    new SqlParameter("@IJA017", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . IJA002;
            parameters [ 1 ] . Value = model . IJA003;
            parameters [ 2 ] . Value = model . IJA004;
            parameters [ 3 ] . Value = model . IJA005;
            parameters [ 4 ] . Value = model . IJA006;
            parameters [ 5 ] . Value = model . IJA007;
            parameters [ 6 ] . Value = model . IJA008;
            parameters [ 7 ] . Value = model . IJA009;
            parameters [ 8 ] . Value = model . IJA010;
            parameters [ 9 ] . Value = model . IJA011;
            parameters [ 10 ] . Value = model . IJA001;
            parameters [ 11 ] . Value = model . IJA012;
            parameters [ 12 ] . Value = model . IJA013;
            parameters [ 13 ] . Value = model . IJA015;
            parameters [ 14 ] . Value = model . IJA016;
            parameters [ 15 ] . Value = model . IJA017;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingBodyOneEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKIJB set " );
            strSql . Append ( "IJB002=@IJB002," );
            strSql . Append ( "IJB003=@IJB003," );
            strSql . Append ( "IJB004=@IJB004," );
            strSql . Append ( "IJB005=@IJB005," );
            strSql . Append ( "IJB006=@IJB006," );
            strSql . Append ( "IJB007=@IJB007," );
            strSql . Append ( "IJB008=@IJB008," );
            strSql . Append ( "IJB009=@IJB009," );
            strSql . Append ( "IJB010=@IJB010," );
            strSql . Append ( "IJB011=@IJB011," );
            strSql . Append ( "IJB012=@IJB012," );
            strSql . Append ( "IJB013=@IJB013," );
            strSql . Append ( "IJB014=@IJB014," );
            strSql . Append ( "IJB015=@IJB015," );
            strSql . Append ( "IJB016=@IJB016," );
            strSql . Append ( "IJB017=@IJB017," );
            strSql . Append ( "IJB018=@IJB018," );
            strSql . Append ( "IJB019=@IJB019," );
            strSql . Append ( "IJB020=@IJB020," );
            strSql . Append ( "IJB021=@IJB021," );
            strSql . Append ( "IJB022=@IJB022," );
            strSql . Append ( "IJB023=@IJB023," );
            strSql . Append ( "IJB024=@IJB024," );
            strSql . Append ( "IJB025=@IJB025," );
            strSql . Append ( "IJB026=@IJB026," );
            strSql . Append ( "IJB027=@IJB027," );
            strSql . Append ( "IJB028=@IJB028 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJB003", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJB004", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB005", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB006", SqlDbType.NVarChar,50),
                    new SqlParameter("@IJB007", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB008", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJB009", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB010", SqlDbType.Int,4),
                    new SqlParameter("@IJB011", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB012", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB013", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB014", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJB015", SqlDbType.Int,4),
                    new SqlParameter("@IJB016", SqlDbType.DateTime),
                    new SqlParameter("@IJB017", SqlDbType.DateTime),
                    new SqlParameter("@IJB018", SqlDbType.DateTime),
                    new SqlParameter("@IJB019", SqlDbType.DateTime),
                    new SqlParameter("@IJB020", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB021", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJB002", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB022", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB023", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB024", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB025", SqlDbType.Decimal,9),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@IJB026", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJB027", SqlDbType.Decimal,9),
                    new SqlParameter("@IJB028", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . IJB003;
            parameters [ 1 ] . Value = model . IJB004;
            parameters [ 2 ] . Value = model . IJB005;
            parameters [ 3 ] . Value = model . IJB006;
            parameters [ 4 ] . Value = model . IJB007;
            parameters [ 5 ] . Value = model . IJB008;
            parameters [ 6 ] . Value = model . IJB009;
            parameters [ 7 ] . Value = model . IJB010;
            parameters [ 8 ] . Value = model . IJB011;
            parameters [ 9 ] . Value = model . IJB012;
            parameters [ 10 ] . Value = model . IJB013;
            parameters [ 11 ] . Value = model . IJB014;
            parameters [ 12 ] . Value = model . IJB015;
            parameters [ 13 ] . Value = model . IJB016;
            parameters [ 14 ] . Value = model . IJB017;
            parameters [ 15 ] . Value = model . IJB018;
            parameters [ 16 ] . Value = model . IJB019;
            parameters [ 17 ] . Value = model . IJB020;
            parameters [ 18 ] . Value = model . IJB021;
            parameters [ 19 ] . Value = model . IJB002;
            parameters [ 20 ] . Value = model . IJB022;
            parameters [ 21 ] . Value = model . IJB023;
            parameters [ 22 ] . Value = model . IJB024;
            parameters [ 23 ] . Value = model . IJB025;
            parameters [ 24 ] . Value = model . idx;
            parameters [ 25 ] . Value = model . IJB026;
            parameters [ 26 ] . Value = model . IJB027;
            parameters [ 27 ] . Value = model . IJB028;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyTwo ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingBodyTwoEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKIJC set " );
            strSql . Append ( "IJC002=@IJC002," );
            strSql . Append ( "IJC003=@IJC003," );
            strSql . Append ( "IJC004=@IJC004," );
            strSql . Append ( "IJC005=@IJC005," );
            strSql . Append ( "IJC006=@IJC006," );
            strSql . Append ( "IJC007=@IJC007," );
            strSql . Append ( "IJC008=@IJC008," );
            strSql . Append ( "IJC009=@IJC009," );
            strSql . Append ( "IJC010=@IJC010," );
            strSql . Append ( "IJC011=@IJC011," );
            strSql . Append ( "IJC012=@IJC012 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@IJC003", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC004", SqlDbType.NVarChar,50),
                    new SqlParameter("@IJC005", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC006", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJC007", SqlDbType.Decimal,9),
                    new SqlParameter("@IJC008", SqlDbType.Int,4),
                    new SqlParameter("@IJC009", SqlDbType.Decimal,9),
                    new SqlParameter("@IJC010", SqlDbType.Int,4),
                    new SqlParameter("@IJC011", SqlDbType.NVarChar,100),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@IJC002", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJC012", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . IJC003;
            parameters [ 1 ] . Value = model . IJC004;
            parameters [ 2 ] . Value = model . IJC005;
            parameters [ 3 ] . Value = model . IJC006;
            parameters [ 4 ] . Value = model . IJC007;
            parameters [ 5 ] . Value = model . IJC008;
            parameters [ 6 ] . Value = model . IJC009;
            parameters [ 7 ] . Value = model . IJC010;
            parameters [ 8 ] . Value = model . IJC011;
            parameters [ 9 ] . Value = model . idx;
            parameters [ 10 ] . Value = model . IJC002;
            parameters [ 11 ] . Value = model . IJC012;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyTre ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . InjectionMoldingBodyTreEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKIJD set " );
            strSql . Append ( "IJD002=@IJD002," );
            strSql . Append ( "IJD003=@IJD003," );
            strSql . Append ( "IJD004=@IJD004," );
            strSql . Append ( "IJD005=@IJD005," );
            strSql . Append ( "IJD006=@IJD006," );
            strSql . Append ( "IJD007=@IJD007," );
            strSql . Append ( "IJD008=@IJD008," );
            strSql . Append ( "IJD009=@IJD009," );
            strSql . Append ( "IJD010=@IJD010," );
            strSql . Append ( "IJD011=@IJD011," );
            strSql . Append ( "IJD012=@IJD012," );
            strSql . Append ( "IJD013=@IJD013" );
            strSql . Append ( " where idx=@idx " );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@IJD003", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD004", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD005", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJD006", SqlDbType.DateTime),
                    new SqlParameter("@IJD007", SqlDbType.DateTime),
                    new SqlParameter("@IJD008", SqlDbType.Decimal,9),
                    new SqlParameter("@IJD009", SqlDbType.NVarChar,100),
                    new SqlParameter("@IJD010", SqlDbType.NVarChar,5),
                    new SqlParameter("@IJD011", SqlDbType.NVarChar,20),
                    new SqlParameter("@IJD012", SqlDbType.Decimal),
                    new SqlParameter("@IJD013", SqlDbType.Decimal),
                    new SqlParameter("@IJD002", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . idx;
            parameters [ 1 ] . Value = model . IJD003;
            parameters [ 2 ] . Value = model . IJD004;
            parameters [ 3 ] . Value = model . IJD005;
            parameters [ 4 ] . Value = model . IJD006;
            parameters [ 5 ] . Value = model . IJD007;
            parameters [ 6 ] . Value = model . IJD008;
            parameters [ 7 ] . Value = model . IJD009;
            parameters [ 8 ] . Value = model . IJD010;
            parameters [ 9 ] . Value = model . IJD011;
            parameters [ 10 ] . Value = model . IJD012;
            parameters [ 11 ] . Value = model . IJD013;
            parameters [ 12 ] . Value = model . IJD002;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteOne ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKIJB WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }
        void DeleteTwo ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKIJC WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }
        void DeleteTre ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKIJD WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJA001 ANW001,IJA004 ANW011,IJA006 ANW013,IJA007 ANW022,GETDATE() dat,IJA014 ANW025 FROM MIKIJA WHERE IJA001='{0}'" ,oddNum );

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
            strSql . AppendFormat ( "SELECT IJB004 ANW002,IJB005 ANW003,IJB006 ANW004,IJB007 ANW005,IJB008 ANW007,SUM(IJB015) ANW006,IJB010-(SELECT SUM(IJB015) FROM MIKIJB E WHERE A.IJB004=E.IJB004 AND A.IJB005=E.IJB005)-ISNULL((SELECT SUM(IJC010) FROM MIKIJC F WHERE A.IJB004=F.IJC002 AND A.IJB005=F.IJC003),0) ANW009,DDA003 DEA008 FROM MIKIJB A LEFT JOIN TPADEA B ON A.IJB005=B.DEA001 INNER JOIN TPADDA C ON B.DEA008=C.DDA001 WHERE IJB001='{0}' GROUP BY IJB004,IJB005,IJB006,IJB007,IJB008,IJB010,DDA003" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT IJC002 ANW002,IJC003 ANW003,IJC004 ANW004,IJC005 ANW005,IJC006 ANW007,SUM(IJC010) ANW006,IJC008-(SELECT SUM(IJC010) FROM MIKIJC E WHERE A.IJC002=E.IJC002 AND A.IJC003=E.IJC003)-ISNULL((SELECT SUM(IJB015) FROM MIKIJB F WHERE A.IJC002=F.IJB004 AND A.IJC003=F.IJB005),0) ANW009,DDA003 DEA008 FROM MIKIJC A LEFT JOIN TPADEA B ON A.IJC003=B.DEA001 INNER JOIN TPADDA C ON B.DEA008=C.DDA001 WHERE IJC001='{0}' GROUP BY IJC002,IJC003,IJC004,IJC005,IJC006,DDA003,IJC008" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int getTableSur ( string orderNum ,string proNum ,string oddNum ,int? nums )
        {
            int num = 0;
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJB004,IJB005,SUM(IJB015) IJB015 FROM MIKIJB WHERE IJB001!='{0}' AND IJB004='{1}' AND IJB005='{2}' GROUP BY IJB004,IJB005,IJB010" ,oddNum ,orderNum ,proNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
            {
                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT IJC002 IJB004,IJC003 IJB005,SUM(IJC010) IJB015 FROM MIKIJC WHERE IJC001!='{0}' AND IJC002='{1}' AND IJC003='{2}' GROUP BY IJC002,IJC003,IJC008" ,oddNum ,orderNum ,proNum );

                table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                if ( table == null || table . Rows . Count < 1 )
                    return Convert . ToInt32 ( nums );
                else
                    return Convert.ToInt32( nums )- ( string . IsNullOrEmpty ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) );
            }
            else
            {
                num = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) );

                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT IJC002 IJB004,IJC003 IJB005,SUM(IJC010) IJB015 FROM MIKIJC WHERE IJC001!='{0}' AND IJC002='{1}' AND IJC003='{2}' GROUP BY IJC002,IJC003,IJC008" ,oddNum ,orderNum ,proNum );

                table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                if ( table == null || table . Rows . Count < 1 )
                    return Convert . ToInt32 ( nums ) - num;
                else
                {
                    num += string . IsNullOrEmpty ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) );
                    return Convert . ToInt32 ( nums ) - num;
                }
            }
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int getTableSurTime ( string oddNum ,string orderNum ,string proNum ,int? nums )
        {
            int num = 0, num1 = 0;
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJC002,IJC003,SUM(IJC010) IJB015 FROM MIKIJC WHERE IJC001!='{0}' AND IJC002='{1}' AND IJC003='{2}' GROUP BY IJC002,IJC003,IJC008" ,oddNum ,orderNum ,proNum  );
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
            {
                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT IJB004,IJB005,SUM(IJB015) IJB015 FROM MIKIJB WHERE IJB001!='{0}' AND IJB004='{1}' AND IJB005='{2}' GROUP BY IJB004,IJB005,IJB010" ,oddNum ,orderNum ,proNum );

                table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                if ( table == null || table . Rows . Count < 1 )
                    return Convert . ToInt32 ( nums );
                else
                {
                    num = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) );
                    return Convert . ToInt32 ( nums ) - num;
                }
            }
            else
            {
                num = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) );

                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT IJB004,IJB005,SUM(IJB015) IJB015 FROM MIKIJB WHERE IJB001!='{0}' AND IJB004='{1}' AND IJB005='{2}' GROUP BY IJB004,IJB005,IJB010" ,oddNum ,orderNum ,proNum );

                table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                if ( table == null || table . Rows . Count < 1 )
                    return Convert . ToInt32 ( nums )-num;
                else
                {
                    num1 = string . IsNullOrEmpty ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ 0 ] [ "IJB015" ] . ToString ( ) );
                    num += num1;
                    return Convert . ToInt32 ( nums ) - num;
                }
            }
        }

        /// <summary>
        /// 获取打印列表  报工单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintTre ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJA001,IJA002,IJA004,IJA006,IJA007,IJA008,IJA012,IJA013,IJA015,IJA016 FROM MIKIJA WHERE IJA001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单  计件
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFor ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJB002,IJB003,IJB004,IJB005,IJB006,IJB007,IJB008,CONVERT(FLOAT,IJB009) IJB009,IJB010,CONVERT(FLOAT,IJB011) IJB011,IJB012,IJB013,IJB014,IJB015,DATENAME(HOUR,IJB016)+':'+DATENAME(MINUTE,IJB016) IJB016,DATENAME(HOUR,IJB017)+':'+DATENAME(MINUTE,IJB017) IJB017,DATENAME(HOUR,IJB018)+':'+DATENAME(MINUTE,IJB018) IJB018,DATENAME(HOUR,IJB019)+':'+DATENAME(MINUTE,IJB019) IJB019,CONVERT(FLOAT,IJB020) IJB020,IJB021,IJB022,CONVERT(FLOAT,IJB023) IJB023,CONVERT(FLOAT,IJB024) IJB024,CONVERT(FLOAT,IJB025) IJB025,CONVERT(FLOAT,IJB011*IJB015) U1,CONVERT(FLOAT,IJB023*IJB020) U3 FROM MIKIJB WHERE IJB001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单  计时
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintFiv ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJC001,IJC002,IJC003,IJC004,IJC005,IJC006,CONVERT(FLOAT,IJC007) IJC007,CONVERT(FLOAT,IJC008) IJC008,CONVERT(FLOAT,IJC009) IJC009,IJC010,IJC011 FROM MIKIJC WHERE IJC001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表  报工单  计时
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getPrintSix ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IJD002,IJD003,IJD005,DATENAME(HOUR,IJD006)+':'+DATENAME(MINUTE,IJD006) IJD006,DATENAME(HOUR,IJD007)+':'+DATENAME(MINUTE,IJD007) IJD007,CONVERT(FLOAT,IJD008) IJD008,IJD009,IJD010,IJD011,CONVERT(FLOAT,IJD012) IJD012,CONVERT(FLOAT,IJD013) IJD013 FROM  MIKIJD WHERE IJD001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
