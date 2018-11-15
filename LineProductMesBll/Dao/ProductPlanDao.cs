using System . Data;
using System . Text;
using StudentMgr;
using System . Collections;
using System;
using System . Collections . Generic;
using System . Data . SqlClient;
using System . Linq;

namespace LineProductMesBll . Dao
{
    public class ProductPlanDao
    {
        /// <summary>
        /// 获取排计划的订单
        /// </summary>
        /// <returns></returns>
        public DataTable getOrder ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT IBB001,IBB002,IBB003,IBB004,CONVERT(DATE,IBB013,111) IBB013,CONVERT(FLOAT,IBB006) IBB006,CONVERT(DATE,GETDATE(),111) IBB007,CONVERT(FLOAT,CASE WHEN DEA985>(IBB006-IBB985) THEN IBB006-IBB985 ELSE DEA985 END) IBB009,CONVERT(FLOAT,IBB006-IBB985) IBB010,CONVERT(FLOAT,IBB006-IBB985) IBB011,'' U0 FROM DCSIBA A INNER JOIN DCSIBB B ON A.IBA001=B.IBB001 INNER JOIN TPADEA C ON B.IBB003=C.DEA001 WHERE IBB015='N' AND IBB985<IBB006 AND DEA009='M' AND DEA076='0507'" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取已排的订单，序号，品号，日期信息
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable getArrayOrderAndProduct ( DataTable table )
        {
            DataTable tableResult = table . DefaultView . ToTable ( true ,new string [ ] { "IBB001" ,"IBB002" ,"IBB003" } );
            string strWhere = "1=1";
            foreach ( DataRow row in tableResult . Rows )
            {
                if ( strWhere . Equals ( "1=1" ) )
                    strWhere += " AND ((PRE002='" + row [ "IBB001" ] + "' AND PRE003='" + row [ "IBB002" ] + "' AND PRE004='" + row [ "IBB003" ] + "')";
                else
                    strWhere += " OR (PRE002='" + row [ "IBB001" ] + "' AND PRE003='" + row [ "IBB002" ] + "' AND PRE004='" + row [ "IBB003" ] + "')";
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRE002,PRE003,PRE004,PRE005 FROM MIKPRE A INNER JOIN MIKPRD B ON A.PRE001=B.PRD001 WHERE PRD003=0 AND {0} )" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,PRE001,PRE002,PRE003,PRE004,PRE005,PRE006,PRE007,PRE008,PRE009,PRE010,PRE011,PRE012 FROM MIKPRE WHERE PRE001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableViewFor ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT A.idx,PRE001,PRE002,PRE003,PRE004,PRE005,PRE006,PRE007,PRE008,PRE009,PRE010,PRE011 FROM MIKPRE A INNER JOIN MIKPRD B ON A.PRE001=B.PRD001 WHERE PRD003=0 AND PRE004='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 回写数量
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool EditOrder ( DataTable table )
        {
            Hashtable SQLString = new Hashtable ( );
            string orderNum = string . Empty, sNum = string . Empty;
            int num = 0;
            foreach ( DataRow row in table . Rows )
            {
                orderNum = row [ "IBB001" ] . ToString ( );
                sNum = row [ "IBB002" ] . ToString ( );
                num = string . IsNullOrEmpty ( row [ "IBB010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IBB010" ] . ToString ( ) );
                StringBuilder strSql = new StringBuilder ( );
                strSql . AppendFormat ( "UPDATE DCSIBB SET IBB985+={0} WHERE IBB001='{1}' AND IBB002='{2}'" ,num ,orderNum ,sNum );
                SQLString . Add ( strSql ,null );
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getOddNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(PRD001) PRD001 FROM MIKPRD" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 0 )
                return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "PRD001" ] . ToString ( );
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
        /// 删除数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Delete ( string oddNum ,DataTable table )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKPRD WHERE PRD001='{0}'" ,oddNum );
            SQLString . Add ( strSql . ToString ( ) ,null );

            var query = from p in table . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "PRE002" ) ,
                            p2 = p . Field<string> ( "PRE003" ) ,
                        } into m
                        let sum = m . Sum ( t => t . Field<int> ( "PRE008" ) )
                        orderby sum ascending
                        select new
                        {
                            pre002 = m . Key . p1 ,
                            pre003 = m . Key . p2 ,
                            sum = sum
                        };
            if ( query != null )
            {
                foreach ( var x in query )
                {
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "UPDATE DCSIBB SET IBB985-={0} WHERE IBB001='{1}' AND IBB002='{2}'" ,x . sum ,x . pre002 ,x . pre003 );
                    SQLString . Add ( strSql ,null );
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 取消  删除已排数量
        /// </summary>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool Cancel ( DataTable table )
        {
            if ( table == null || table . Rows . Count < 1 )
                return false;

            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );

            //DataTable table = tableView . Copy ( );
            //int i = 0;
            //foreach ( DataRow row in tableView . Rows )
            //{
            //    if ( row [ "idx" ] == null || row [ "idx" ] . ToString ( ) == string . Empty || Convert . ToInt32 ( row [ "idx" ] ) == 0 )
            //    {
            //        DataRow rows = row;
            //        table . Rows . RemoveAt ( i );
            //    }
            //    i++;
            //}

            var query = from p in table . AsEnumerable ( )
                        where p . Field<int?> ( "idx" )  == null
                        group p by new
                        {
                            p1 = p . Field<string> ( "PRE002" ) ,
                            p2 = p . Field<string> ( "PRE003" )
                        } into m
                        let sum = m . Sum ( t => t . Field<int> ( "PRE008" ) )
                        orderby sum ascending
                        select new
                        {
                            pre002 = m . Key . p1 ,
                            pre003 = m . Key . p2 ,
                            sum = sum
                        };
            if ( query != null )
            {
                foreach ( var x in query )
                {
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "UPDATE DCSIBB SET IBB985-={0} WHERE IBB001='{1}' AND IBB002='{2}'" ,x . sum ,x . pre002 ,x . pre003 );
                    SQLString . Add ( strSql ,null );
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 获取已经排产且启用的计划
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable getTableAllOrder ( DataTable table ,string oddNum)
        {
            DataTable tableResult = table . DefaultView . ToTable ( true ,new string [ ] { "PRE002" ,"PRE003" ,"PRE004" } );

            string strWhere = "1=1";
            foreach ( DataRow row in tableResult . Rows )
            {
                if ( strWhere . Equals ( "1=1" ) )
                    strWhere += " AND ((PRE002='" + row [ "PRE002" ] + "' AND PRE003='" + row [ "PRE003" ] + "' AND PRE004='" + row [ "PRE004" ] + "')";
                else
                    strWhere += " OR (PRE002='" + row [ "PRE002" ] + "' AND PRE003='" + row [ "PRE003" ] + "' AND PRE004='" + row [ "PRE004" ] + "')";
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRE002,PRE003,PRE004,SUM(PRE008) PRE008 FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001=B.PRE001 WHERE PRD003=0 AND {0}) AND PRE001!='{1}' GROUP BY PRE002,PRE003,PRE004" ,strWhere ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取已经排产日期
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable getTableAllTime ( DataTable table )
        {
            DataTable tableResult = table . DefaultView . ToTable ( true ,new string [ ] { "PRE002" ,"PRE003" ,"PRE004" } );

            string strWhere = "1=1";
            foreach ( DataRow row in tableResult . Rows )
            {
                if ( strWhere . Equals ( "1=1" ) )
                    strWhere += " AND ((PRE002='" + row [ "PRE002" ] + "' AND PRE003='" + row [ "PRE003" ] + "' AND PRE004='" + row [ "PRE004" ] + "')";
                else
                    strWhere += " OR (PRE002='" + row [ "PRE002" ] + "' AND PRE003='" + row [ "PRE003" ] + "' AND PRE004='" + row [ "PRE004" ] + "')";
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRE002,PRE003,PRE004,PRE005,COUNT(1) COUN FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001=B.PRE001 WHERE PRD003=0 AND {0})  GROUP BY PRE002,PRE003,PRE004,PRE005" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu.ProductPlanHeaderEntity model,DataTable table )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            model . PRD001 = getOddNum ( );
            UserInfoMation . oddNum = model . PRD001;
            AddHeader ( SQLString ,model );

            LineProductMesEntityu . ProductPlanBodyEntity modelBody = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            modelBody . PRE001 = model . PRD001;

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT PRF001,PRF002 FROM MIKPRF " );

            DataTable tableR = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            foreach ( DataRow row in table . Rows )
            {
                modelBody . PRE002 = row [ "PRE002" ] . ToString ( );
                modelBody . PRE003 = row [ "PRE003" ] . ToString ( );
                modelBody . PRE004 = row [ "PRE004" ] . ToString ( );
                modelBody . PRE005 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
                modelBody . PRE006 = Convert . ToDateTime ( row [ "PRE006" ] . ToString ( ) );
                modelBody . PRE007 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
                modelBody . PRE008 = Convert . ToInt32 ( row [ "PRE008" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "PRE009" ] . ToString ( ) ) )
                    modelBody . PRE009 = null;
                else
                    modelBody . PRE009 = Convert . ToInt32 ( row [ "PRE009" ] . ToString ( ) );
                modelBody . PRE010 = Convert . ToInt32 ( row [ "PRE010" ] . ToString ( ) );
                if ( string . IsNullOrEmpty ( row [ "PRE011" ] . ToString ( ) ) )
                    modelBody . PRE011 = 0;
                else
                    modelBody . PRE011 = Convert . ToInt32 ( row [ "PRE011" ] . ToString ( ) );
                modelBody . PRE008 = modelBody . PRE008 + modelBody . PRE011;
                AddBody ( SQLString ,modelBody );
                if ( tableR != null && tableR . Rows . Count > 0 )
                {
                    if(tableR.Select("PRF001='"+modelBody.PRE004+"' AND PRF002='"+modelBody.PRE005+"'").Length<1)
                        Addprf ( SQLString ,modelBody );
                }else
                    Addprf ( SQLString ,modelBody );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . ProductPlanHeaderEntity model ,DataTable table ,List<string> idxList )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );

            LineProductMesEntityu . ProductPlanBodyEntity modelBody = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            modelBody . PRE001 = model . PRD001;

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT PRF001,PRF002 FROM MIKPRF " );

            DataTable tableR = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            foreach ( DataRow row in table . Rows )
            {
                modelBody . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] );
                modelBody . PRE002 = row [ "PRE002" ] . ToString ( );
                modelBody . PRE003 = row [ "PRE003" ] . ToString ( );
                modelBody . PRE004 = row [ "PRE004" ] . ToString ( );
                modelBody . PRE005 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
                modelBody . PRE006 = Convert . ToDateTime ( row [ "PRE006" ] . ToString ( ) );
                modelBody . PRE007 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
                modelBody . PRE008 = string . IsNullOrEmpty ( row [ "PRE008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PRE008" ] . ToString ( ) );
                modelBody . PRE009 = string . IsNullOrEmpty ( row [ "PRE009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PRE009" ] . ToString ( ) );
                modelBody . PRE010 = Convert . ToInt32 ( row [ "PRE010" ] . ToString ( ) );
                modelBody . PRE012 = modelBody . PRE009 == 0 ? string . Empty : row [ "PRE012" ] . ToString ( );
                if ( string . IsNullOrEmpty ( row [ "PRE011" ] . ToString ( ) ) )
                    modelBody . PRE011 = 0;
                else
                    modelBody . PRE011 = Convert . ToInt32 ( row [ "PRE011" ] . ToString ( ) );
                modelBody . PRE008 = modelBody . PRE008 + modelBody . PRE011;
                if ( modelBody . idx < 1 )
                {
                    AddBody ( SQLString ,modelBody );
                    if ( tableR != null && tableR . Rows . Count > 0 )
                    {
                        if ( tableR . Select ( "PRF001='" + modelBody . PRE004 + "' AND PRF002='" + modelBody . PRE005 + "'" ) . Length < 1 )
                            Addprf ( SQLString ,modelBody );
                        else
                        {
                            if ( modelBody . PRE011 != 0 )
                            {
                                modelBody . PRE008 = modelBody . PRE011;
                                Editprf ( SQLString ,modelBody );
                            }
                        }
                    }
                    else
                        Addprf ( SQLString ,modelBody );
                }
                else
                {
                    EditBody ( SQLString ,modelBody );
                    if ( modelBody . PRE011 != 0 )
                    {
                        modelBody . PRE008 = modelBody . PRE011;
                        Editprf ( SQLString ,modelBody );
                    }
                }
            }

            foreach ( string s in idxList )
            {
                DeleteBody ( SQLString ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPRD(" );
            strSql . Append ( "PRD001,PRD002,PRD003)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PRD001,@PRD002,@PRD003)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRD001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRD002", SqlDbType.Date,3),
                    new SqlParameter("@PRD003", SqlDbType.Bit,1)
            };
            parameters [ 0 ] . Value = model . PRD001;
            parameters [ 1 ] . Value = model . PRD002;
            parameters [ 2 ] . Value = model . PRD003;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPRE(" );
            strSql . Append ( "PRE001,PRE002,PRE003,PRE004,PRE005,PRE006,PRE007,PRE008,PRE009,PRE010)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PRE001,@PRE002,@PRE003,@PRE004,@PRE005,@PRE006,@PRE007,@PRE008,@PRE009,@PRE010)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRE001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE004", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE005", SqlDbType.Date,3),
                    new SqlParameter("@PRE006", SqlDbType.Date,3),
                    new SqlParameter("@PRE007", SqlDbType.Int,4),
                    new SqlParameter("@PRE008", SqlDbType.Int,4),
                    new SqlParameter("@PRE009", SqlDbType.Int,4),
                    new SqlParameter("@PRE010", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . PRE001;
            parameters [ 1 ] . Value = model . PRE002;
            parameters [ 2 ] . Value = model . PRE003;
            parameters [ 3 ] . Value = model . PRE004;
            parameters [ 4 ] . Value = model . PRE005;
            parameters [ 5 ] . Value = model . PRE006;
            parameters [ 6 ] . Value = model . PRE007;
            parameters [ 7 ] . Value = model . PRE008;
            parameters [ 8 ] . Value = model . PRE009;
            parameters [ 9 ] . Value = model . PRE010;
            SQLString . Add ( strSql ,parameters );
        }

        void EditBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPRE set " );
            strSql . Append ( "PRE002=@PRE002," );
            strSql . Append ( "PRE003=@PRE003," );
            strSql . Append ( "PRE004=@PRE004," );
            strSql . Append ( "PRE005=@PRE005," );
            strSql . Append ( "PRE006=@PRE006," );
            strSql . Append ( "PRE007=@PRE007," );
            strSql . Append ( "PRE008=@PRE008," );
            strSql . Append ( "PRE009=@PRE009," );
            strSql . Append ( "PRE010=@PRE010," );
            strSql . Append ( "PRE012=@PRE012 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRE002", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE003", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE004", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRE005", SqlDbType.Date,3),
                    new SqlParameter("@PRE006", SqlDbType.Date,3),
                    new SqlParameter("@PRE007", SqlDbType.Int,4),
                    new SqlParameter("@PRE008", SqlDbType.Int,4),
                    new SqlParameter("@PRE009", SqlDbType.Int,4),
                    new SqlParameter("@PRE010", SqlDbType.Int,4),
                    new SqlParameter("@PRE012", SqlDbType.NVarChar,20),
                    new SqlParameter("@idx", SqlDbType.Int,4),
            };
            parameters [ 0 ] . Value = model . PRE002;
            parameters [ 1 ] . Value = model . PRE003;
            parameters [ 2 ] . Value = model . PRE004;
            parameters [ 3 ] . Value = model . PRE005;
            parameters [ 4 ] . Value = model . PRE006;
            parameters [ 5 ] . Value = model . PRE007;
            parameters [ 6 ] . Value = model . PRE008;
            parameters [ 7 ] . Value = model . PRE009;
            parameters [ 8 ] . Value = model . PRE010;
            parameters [ 9 ] . Value = model . PRE012;
            parameters [ 10 ] . Value = model . idx;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteBody ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKPRE WHERE idx={0}" ,s );
            SQLString . Add ( strSql ,null );
        }

        void Addprf ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanBodyEntity model )
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
            strSql . Append ( "PRF003=ISNULL(PRF003,0)+@PRF003 " );
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
        /// 复制
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Copy ( DataTable table ,string oddNum)
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            LineProductMesEntityu . ProductPlanHeaderEntity model = new LineProductMesEntityu . ProductPlanHeaderEntity ( );
            model . PRD001 = getOddNum ( );
            model . PRD002 = UserInfoMation . sysTime;
            model . PRD003 = false;
            AddHeader ( SQLString ,model );
            UserInfoMation . oddNum = model . PRD001;

            LineProductMesEntityu . ProductPlanBodyEntity modelBody = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            modelBody . PRE001 = model . PRD001;
            foreach ( DataRow row in table . Rows )
            {
                modelBody . PRE002 = row [ "PRE002" ] . ToString ( );
                modelBody . PRE003 = row [ "PRE003" ] . ToString ( );
                modelBody . PRE004 = row [ "PRE004" ] . ToString ( );
                modelBody . PRE005 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
                modelBody . PRE006 = Convert . ToDateTime ( row [ "PRE006" ] . ToString ( ) );
                modelBody . PRE007 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
                modelBody . PRE008 = Convert . ToInt32 ( row [ "PRE008" ] . ToString ( ) );
                modelBody . PRE010 = Convert . ToInt32 ( row [ "PRE010" ] . ToString ( ) );
                AddBody ( SQLString ,modelBody );
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKPRD SET PRD003=1 WHERE PRD001='{0}'" ,oddNum );
            SQLString . Add ( strSql ,null );

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 获取需要复制的数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableCopyOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT A.PRE002,A.PRE003,A.PRE004,A.PRE005,A.PRE008 FROM MIKPRE A INNER JOIN MIKPRD B ON A.PRE001=B.PRD001 INNER JOIN (SELECT PRE002,PRE003,PRE004,PRE005,PRE008 FROM MIKPRE WHERE PRE001='{0}' ) C ON A.PRE002=C.PRE002 AND A.PRE003=C.PRE003 AND A.PRE004=C.PRE004 WHERE PRE001!='{0}' AND PRD003='0'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 启用或弃用
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Examine ( string oddNum ,int resuState )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKPRD SET PRD003={1} WHERE PRD001='{0}'" ,oddNum ,resuState );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取查询数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableColumn ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT PRD001,PRE004 FROM MIKPRD A INNER JOIN MIKPRE B ON A.PRD001=B.PRE001 WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . ProductPlanHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRD001,PRD002,PRD003 FROM MIKPRD WHERE PRD001='{0}'" ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
                return getModel ( table . Rows [ 0 ] );
        }

        public LineProductMesEntityu . ProductPlanHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . ProductPlanHeaderEntity model = new LineProductMesEntityu . ProductPlanHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "PRD001" ] != null )
                {
                    model . PRD001 = row [ "PRD001" ] . ToString ( );
                }
                if ( row [ "PRD002" ] != null && row [ "PRD002" ] . ToString ( ) != "" )
                {
                    model . PRD002 = DateTime . Parse ( row [ "PRD002" ] . ToString ( ) );
                }
                if ( row [ "PRD003" ] != null && row [ "PRD003" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "PRD003" ] . ToString ( ) == "1" ) || ( row [ "PRD003" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . PRD003 = true;
                    }
                    else
                    {
                        model . PRD003 = false;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 生成ERP计划
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool AddPlan ( string oddNum )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            LineProductMesEntityu . SGMSAAEntity saa = new LineProductMesEntityu . SGMSAAEntity ( );
            saa . SAA001 = getCodeForPlan ( );
            saa . SAA002 = string . Empty;
            saa . SAA003 = UserInfoMation . sysTime . ToString ( "yyyyMMdd" );
            saa . SAA004 = UserInfoMation . userNum;
            saa . SAA005 = "06";
            saa . SAA006 = string . Empty;
            saa . SAA013 = "F";
            saa . SAA014 = string . Empty;
            saa . SAA901 = UserInfoMation . userNum;
            saa . SAA902 = saa . SAA003;
            AddSAA ( SQLString ,saa );

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRE004,DEA002,DEA057,DEA003,PRE005,PRE008 FROM MIKPRE A INNER JOIN TPADEA B ON A.PRE004=B.DEA001 WHERE PRE001='{0}' AND PRE008-ISNULL(PRE009,0)>0" ,oddNum );
            DataTable tableView = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            
            int i = 0;
            List<LineProductMesEntityu . SGMSABEntity> modelList = new List<LineProductMesEntityu . SGMSABEntity> ( );
            foreach ( DataRow row in tableView . Rows )
            {
                LineProductMesEntityu . SGMSABEntity sab = new LineProductMesEntityu . SGMSABEntity ( );
                sab . SAB001 = saa . SAA001;
                i++;
                sab . SAB002 = i . ToString ( ) . PadLeft ( 3 ,'0' );
                sab . SAB003 = row [ "PRE004" ] . ToString ( );
                sab . SAB004 = row [ "DEA002" ] . ToString ( );
                sab . SAB005 = row [ "DEA057" ] . ToString ( );
                sab . SAB006 = row [ "DEA003" ] . ToString ( );
                sab . SAB007 = Convert . ToDecimal ( row [ "PRE008" ] . ToString ( ) );
                sab . SAB009 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) ) . ToString ( "yyyyMMdd" );
                sab . SAB010 = sab . SAB009;
                sab . SAB011 = "F";
                sab . SAB019 = "F";
                sab . SAB020 = string . Empty;
                sab . SAB901 = UserInfoMation . userNum;
                sab . SAB902 = UserInfoMation . sysTime . ToString ( "yyyyMMdd" );
                modelList . Add ( sab );
                ADDSAB ( SQLString ,sab );
            }

            if ( modelList . Count > 0 )
            {
                foreach ( LineProductMesEntityu . SGMSABEntity list in modelList )
                {
                    strSql = new StringBuilder ( );
                    strSql . AppendFormat ( "SELECT QAB003,DEA002,DEA057,DEA003,DEA008,CONVERT(FLOAT,QAB005) QAB005 FROM SGMQAB A INNER JOIN TPADEA B ON A.QAB003=B.DEA001 WHERE QAB001='{0}'" ,list . SAB003 );
                    DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                    if ( table == null || table . Rows . Count < 1 )
                        continue;
                    i = 0;
                    LineProductMesEntityu . SGMSACEntity entity = new LineProductMesEntityu . SGMSACEntity ( );
                    entity . SAC001 = saa . SAA001;
                    entity . SAC002 = list . SAB002;
                    foreach ( DataRow row in table . Rows )
                    {
                        i++;
                        entity . SAC003 = i . ToString ( ) . PadLeft ( 3 ,'0' );
                        entity . SAC004 = row [ "QAB003" ] . ToString ( );
                        entity . SAC005 = row [ "DEA002" ] . ToString ( );
                        entity . SAC006 = row [ "DEA057" ] . ToString ( );
                        entity . SAC007 = row [ "DEA003" ] . ToString ( );
                        entity . SAC008 = row [ "DEA008" ] . ToString ( );
                        entity . SAC009 = string . IsNullOrEmpty ( row [ "QAB005" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "QAB005" ] ) * list . SAB007;
                        ADDSAC ( SQLString ,entity );
                    }
                }
            }

            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,PRE008-ISNULL(PRE009,0) PRE008 FROM MIKPRE WHERE PRE001='{0}' AND PRE008-ISNULL(PRE009,0)>0" ,oddNum );
            tableView = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            LineProductMesEntityu . ProductPlanBodyEntity model = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            model . PRE012 = saa . SAA001;
            foreach ( DataRow row in tableView . Rows )
            {
                model . idx = Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                model . PRE009 = Convert . ToInt32 ( row [ "PRE008" ] . ToString ( ) );
                AddPlan ( SQLString ,model );
            }
            
            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 获取ERP计划单号
        /// </summary>
        /// <returns></returns>
        string getCodeForPlan ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(SAA001) SAA001 FROM SGMSAA " );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            DateTime dt = UserInfoMation . sysTime;
            if ( table != null && table . Rows . Count > 0 )
            {
                string code = table . Rows [ 0 ] [ "SAA001" ] . ToString ( );
                if ( code == string . Empty )
                    return "PL" + dt . ToString ( "yyyyMMdd" ) + "0001";
                else
                {
                    if ( code . Substring ( 2 ,8 ) . Equals ( dt . ToString ( "yyyyMMdd" ) ) )
                        return "PL" + ( Convert . ToInt64 ( code . Substring ( 2 ,12 ) ) + 1 ) . ToString ( );
                    else
                        return "PL" + dt . ToString ( "yyyyMMdd" ) + "0001";
                }
            }
            else
                return "PL" + dt . ToString ( "yyyyMMdd" ) + "0001";
        }

        void AddSAA ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . SGMSAAEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into SGMSAA(" );
            strSql . Append ( "SAA001,SAA002,SAA003,SAA004,SAA005,SAA006,SAA013,SAA014,SAA901,SAA902)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@SAA001,@SAA002,@SAA003,@SAA004,@SAA005,@SAA006,@SAA013,@SAA014,@SAA901,@SAA902)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@SAA001", SqlDbType.VarChar,14),
                    new SqlParameter("@SAA002", SqlDbType.VarChar,4),
                    new SqlParameter("@SAA003", SqlDbType.VarChar,8),
                    new SqlParameter("@SAA004", SqlDbType.VarChar,8),
                    new SqlParameter("@SAA005", SqlDbType.VarChar,6),
                    new SqlParameter("@SAA006", SqlDbType.VarChar,255),
                    new SqlParameter("@SAA013", SqlDbType.VarChar,1),
                    new SqlParameter("@SAA014", SqlDbType.VarChar,8),
                    new SqlParameter("@SAA901", SqlDbType.VarChar,8),
                    new SqlParameter("@SAA902", SqlDbType.VarChar,24)
            };
            parameters [ 0 ] . Value = model . SAA001;
            parameters [ 1 ] . Value = model . SAA002;
            parameters [ 2 ] . Value = model . SAA003;
            parameters [ 3 ] . Value = model . SAA004;
            parameters [ 4 ] . Value = model . SAA005;
            parameters [ 5 ] . Value = model . SAA006;
            parameters [ 6 ] . Value = model . SAA013;
            parameters [ 7 ] . Value = model . SAA014;
            parameters [ 8 ] . Value = model . SAA901;
            parameters [ 9 ] . Value = model . SAA902;
            SQLString . Add ( strSql ,parameters );
        }
        void ADDSAB ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . SGMSABEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into SGMSAB(" );
            strSql . Append ( "SAB001,SAB002,SAB003,SAB004,SAB005,SAB006,SAB007,SAB009,SAB010,SAB011,SAB019,SAB020,SAB901,SAB902)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@SAB001,@SAB002,@SAB003,@SAB004,@SAB005,@SAB006,@SAB007,@SAB009,@SAB010,@SAB011,@SAB019,@SAB020,@SAB901,@SAB902)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@SAB001", SqlDbType.VarChar,14),
                    new SqlParameter("@SAB002", SqlDbType.VarChar,3),
                    new SqlParameter("@SAB003", SqlDbType.VarChar,20),
                    new SqlParameter("@SAB004", SqlDbType.VarChar,60),
                    new SqlParameter("@SAB005", SqlDbType.VarChar,60),
                    new SqlParameter("@SAB006", SqlDbType.VarChar,4),
                    new SqlParameter("@SAB007", SqlDbType.Decimal,9),
                    new SqlParameter("@SAB009", SqlDbType.VarChar,8),
                    new SqlParameter("@SAB010", SqlDbType.VarChar,8),
                    new SqlParameter("@SAB011", SqlDbType.VarChar,1),
                    new SqlParameter("@SAB019", SqlDbType.VarChar,1),
                    new SqlParameter("@SAB020", SqlDbType.VarChar,2),
                    new SqlParameter("@SAB901", SqlDbType.VarChar,8),
                    new SqlParameter("@SAB902", SqlDbType.VarChar,24)
            };
            parameters [ 0 ] . Value = model . SAB001;
            parameters [ 1 ] . Value = model . SAB002;
            parameters [ 2 ] . Value = model . SAB003;
            parameters [ 3 ] . Value = model . SAB004;
            parameters [ 4 ] . Value = model . SAB005;
            parameters [ 5 ] . Value = model . SAB006;
            parameters [ 6 ] . Value = model . SAB007;
            parameters [ 7 ] . Value = model . SAB009;
            parameters [ 8 ] . Value = model . SAB010;
            parameters [ 9 ] . Value = model . SAB011;
            parameters [ 10 ] . Value = model . SAB019;
            parameters [ 11 ] . Value = model . SAB020;
            parameters [ 12 ] . Value = model . SAB901;
            parameters [ 13 ] . Value = model . SAB902;
            SQLString . Add ( strSql ,parameters );
        }
        void ADDSAC ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . SGMSACEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into SGMSAC(" );
            strSql . Append ( "SAC001,SAC002,SAC003,SAC004,SAC005,SAC006,SAC007,SAC008,SAC009)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@SAC001,@SAC002,@SAC003,@SAC004,@SAC005,@SAC006,@SAC007,@SAC008,@SAC009)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@SAC001", SqlDbType.VarChar,14),
                    new SqlParameter("@SAC002", SqlDbType.VarChar,3),
                    new SqlParameter("@SAC003", SqlDbType.VarChar,3),
                    new SqlParameter("@SAC004", SqlDbType.VarChar,20),
                    new SqlParameter("@SAC005", SqlDbType.VarChar,60),
                    new SqlParameter("@SAC006", SqlDbType.VarChar,60),
                    new SqlParameter("@SAC007", SqlDbType.VarChar,4),
                    new SqlParameter("@SAC008", SqlDbType.VarChar,6),
                    new SqlParameter("@SAC009", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . SAC001;
            parameters [ 1 ] . Value = model . SAC002;
            parameters [ 2 ] . Value = model . SAC003;
            parameters [ 3 ] . Value = model . SAC004;
            parameters [ 4 ] . Value = model . SAC005;
            parameters [ 5 ] . Value = model . SAC006;
            parameters [ 6 ] . Value = model . SAC007;
            parameters [ 7 ] . Value = model . SAC008;
            parameters [ 8 ] . Value = model . SAC009;
            SQLString . Add ( strSql ,parameters );
        }
        void AddPlan ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ProductPlanBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE MIKPRE SET PRE009=ISNULL(PRE009,0)+{0},PRE012='{2}' WHERE idx={1}" ,model . PRE009 ,model . idx ,model . PRE012 );

            SQLString . Add ( strSql ,null );
        }

    }
}
