using System . Data;
using System . Text;
using StudentMgr;
using System . Data . SqlClient;
using System . Collections . Generic;
using System;
using System . Collections;

namespace LineProductMesBll . Dao
{
    public class ArtDao
    {
        /// <summary>
        /// 获取机台信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAP001,MAP002,MAP005 FROM MIKMAP " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public DataTable getTableWork ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '05%' AND DAA001!='05'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 使用模具
        /// </summary>
        /// <returns></returns>
        public DataTable getTableMould ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MOI001,MOI002,MOI003,MOI005,MOI006,MOI007,MOI009,MOI010,MOI011,MOI012,MOI014,MOI015 FROM MIKMOI WHERE MOI006='在用'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . ArsEntity model ,DataTable table ,Hashtable haTable )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            AddHeader ( SQLString ,model );

            if ( table != null && table . Rows . Count > 0 )
            {
                LineProductMesEntityu . ArtEntity body = new LineProductMesEntityu . ArtEntity ( );
                body . ART001 = model . ARS001;
                foreach ( DataRow row in table . Rows )
                {
                    body . ART011 = row [ "ART011" ] . ToString ( );
                    body . ART002 = row [ "ART002" ] . ToString ( );
                    body . ART003 = row [ "ART003" ] . ToString ( );
                    body . ART004 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                    body . ART005 = string . IsNullOrEmpty ( row [ "ART005" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART005" ] . ToString ( ) );
                    body . ART006 = row [ "ART006" ] . ToString ( );
                    body . ART007 = row [ "ART007" ] . ToString ( );
                    //body . ART008 = row [ "ART008" ] . ToString ( );
                    body . ART009 = row [ "ART009" ] . ToString ( );
                    body . ART010 = string . IsNullOrEmpty ( row [ "ART010" ] . ToString ( ) ) == true ? "否" : row [ "ART010" ] . ToString ( );
                    body . ART012 = string . IsNullOrEmpty ( row [ "ART012" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ART012" ] . ToString ( ) );
                    body . ART013 = row [ "ART013" ] . ToString ( );
                    AddBody ( SQLString ,body );
                    if ( haTable != null && haTable . Count > 0 )
                    {
                        DataTable tableView = ( DataTable ) haTable [ body . ART001 + body . ART011 ];
                        if ( tableView != null && tableView . Rows . Count > 0 )
                        {
                            LineProductMesEntityu . AruEntity bodyOne = new LineProductMesEntityu . AruEntity ( );
                            bodyOne . ARU001 = body . ART001;
                            bodyOne . ARU002 = body . ART011;
                            foreach ( DataRow rows in tableView . Rows )
                            {
                                bodyOne . ARU003 = rows [ "ARU003" ] . ToString ( );
                                bodyOne . ARU004 = rows [ "ARU004" ] . ToString ( );
                                AddBodyOne ( SQLString ,bodyOne );
                            }
                        }
                    }
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 是否存在品号
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
         public  bool Exists ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM MIKARS WHERE ARS001='{0}'" ,piNum );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 是否存在品号和工艺
        /// </summary>
        /// <param name="piNum"></param>
        /// <param name="artNum"></param>
        /// <returns></returns>
        bool Exists ( string piNum ,string artNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM MIKART WHERE ART001='{0}' AND ART002='{1}'" ,piNum ,artNum );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . ArsEntity model ,DataTable table ,List<string> idxList ,Hashtable haTable ,List<string> idxListOne )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            EditHeader ( SQLString ,model );

            if ( table != null && table . Rows . Count > 0 )
            {
                LineProductMesEntityu . ArtEntity body = new LineProductMesEntityu . ArtEntity ( );
                body . ART001 = model . ARS001;
                foreach ( DataRow row in table . Rows )
                {
                    body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    body . ART002 = row [ "ART002" ] . ToString ( );
                    body . ART003 = row [ "ART003" ] . ToString ( );
                    body . ART004 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                    body . ART005 = string . IsNullOrEmpty ( row [ "ART005" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART005" ] . ToString ( ) );
                    body . ART006 = row [ "ART006" ] . ToString ( );
                    body . ART007 = row [ "ART007" ] . ToString ( );
                    //body . ART008 = row [ "ART008" ] . ToString ( );
                    body . ART009 = row [ "ART009" ] . ToString ( );
                    body . ART010 = string . IsNullOrEmpty ( row [ "ART010" ] . ToString ( ) ) == true ? "否" : row [ "ART010" ] . ToString ( );
                    body . ART011 = row [ "ART011" ] . ToString ( );
                    body . ART012 = string . IsNullOrEmpty ( row [ "ART012" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ART012" ] . ToString ( ) );
                    body . ART013 = row [ "ART013" ] . ToString ( );
                    if ( body . idx < 1 )
                        AddBody ( SQLString ,body );
                    else
                        EditBody ( SQLString ,body );
                    if ( haTable != null && haTable . Count > 0 )
                    {
                        DataTable tableView = ( DataTable ) haTable [ body . ART001 + body . ART011 ];
                        if ( tableView != null && tableView . Rows . Count > 0 )
                        {
                            LineProductMesEntityu . AruEntity bodyOne = new LineProductMesEntityu . AruEntity ( );
                            bodyOne . ARU001 = body . ART001;
                            bodyOne . ARU002 = body . ART011;
                            foreach ( DataRow rows in tableView . Rows )
                            {
                                bodyOne . ARU003 = rows [ "ARU003" ] . ToString ( );
                                bodyOne . ARU004 = rows [ "ARU004" ] . ToString ( );
                                bodyOne . idx = string . IsNullOrEmpty ( rows [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( rows [ "idx" ] . ToString ( ) );
                                if ( bodyOne . idx < 1 )
                                    AddBodyOne ( SQLString ,bodyOne );
                                else
                                    EditBodyOne ( SQLString ,bodyOne );
                            }
                        }
                    }
                }
            }

            foreach ( string s in idxList )
            {
                DeleteBody ( SQLString ,s );
            }
            foreach ( string s in idxListOne )
            {
                DeleteBodyOne ( SQLString ,s );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ArsEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO MIKARS (" );
            strSql . Append ( "ARS001,ARS002,ARS003,ARS004,ARS005,ARS006,ARS007,ARS008,ARS009,ARS011) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@ARS001,@ARS002,@ARS003,@ARS004,@ARS005,@ARS006,@ARS007,@ARS008,@ARS009,@ARS011) " );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ARS001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS002", SqlDbType.NVarChar,50),
                    new SqlParameter("@ARS003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS005", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS006", SqlDbType.NVarChar,5),
                    new SqlParameter("@ARS007", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS008", SqlDbType.Decimal,9),
                    new SqlParameter("@ARS009", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS011", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . ARS001;
            parameters [ 1 ] . Value = model . ARS002;
            parameters [ 2 ] . Value = model . ARS003;
            parameters [ 3 ] . Value = model . ARS004;
            parameters [ 4 ] . Value = model . ARS005;
            parameters [ 5 ] . Value = model . ARS006;
            parameters [ 6 ] . Value = model . ARS007;
            parameters [ 7 ] . Value = model . ARS008;
            parameters [ 8 ] . Value = model . ARS009;
            parameters [ 9 ] . Value = model . ARS011;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ArtEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKART(" );
            strSql . Append ( "ART001,ART002,ART003,ART004,ART005,ART006,ART007,ART009,ART010,ART011,ART012,ART013)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ART001,@ART002,@ART003,@ART004,@ART005,@ART006,@ART007,@ART009,@ART010,@ART011,@ART012,@ART013)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ART001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART004", SqlDbType.Decimal,9),
                    new SqlParameter("@ART005", SqlDbType.Decimal,9),
                    new SqlParameter("@ART006", SqlDbType.NVarChar,50),
                    new SqlParameter("@ART007", SqlDbType.NVarChar,50),
                    new SqlParameter("@ART009", SqlDbType.NVarChar,255),
                    new SqlParameter("@ART010", SqlDbType.NVarChar,5),
                    new SqlParameter("@ART011", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART012", SqlDbType.Int,4),
                    new SqlParameter("@ART013", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . ART001;
            parameters [ 1 ] . Value = model . ART002;
            parameters [ 2 ] . Value = model . ART003;
            parameters [ 3 ] . Value = model . ART004;
            parameters [ 4 ] . Value = model . ART005;
            parameters [ 5 ] . Value = model . ART006;
            parameters [ 6 ] . Value = model . ART007;
            parameters [ 7 ] . Value = model . ART009;
            parameters [ 8 ] . Value = model . ART010;
            parameters [ 9 ] . Value = model . ART011;
            parameters [ 10 ] . Value = model . ART012;
            parameters [ 11 ] . Value = model . ART013;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . AruEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKARU(" );
            strSql . Append ( "ARU001,ARU002,ARU003,ARU004,ARU005)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ARU001,@ARU002,@ARU003,@ARU004,@ARU005)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ARU001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARU002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARU003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARU004", SqlDbType.NVarChar,50),
                    new SqlParameter("@ARU005", SqlDbType.NChar,10)
            };
            parameters [ 0 ] . Value = model . ARU001;
            parameters [ 1 ] . Value = model . ARU002;
            parameters [ 2 ] . Value = model . ARU003;
            parameters [ 3 ] . Value = model . ARU004;
            parameters [ 4 ] . Value = model . ARU005;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ArsEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKARS SET " );
            strSql . Append ( "ARS008=@ARS008," );
            strSql . Append ( "ARS011=@ARS011 " );
            strSql . Append ( "WHERE ARS001=@ARS001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ARS001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ARS008", SqlDbType.Decimal,9),
                    new SqlParameter("@ARS011", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . ARS001;
            parameters [ 1 ] . Value = model . ARS008;
            parameters [ 2 ] . Value = model . ARS011;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBody ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ArtEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKART set " );
            strSql . Append ( "ART002=@ART002," );
            strSql . Append ( "ART003=@ART003," );
            strSql . Append ( "ART004=@ART004," );
            strSql . Append ( "ART005=@ART005," );
            strSql . Append ( "ART006=@ART006," );
            strSql . Append ( "ART007=@ART007," );
            //strSql . Append ( "ART008=@ART008," );
            strSql . Append ( "ART009=@ART009," );
            strSql . Append ( "ART010=@ART010," );
            strSql . Append ( "ART011=@ART011," );
            strSql . Append ( "ART012=@ART012," );
            strSql . Append ( "ART013=@ART013 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ART003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART004", SqlDbType.Decimal,9),
                    new SqlParameter("@ART005", SqlDbType.Decimal,9),
                    new SqlParameter("@ART006", SqlDbType.NVarChar,50),
                    new SqlParameter("@ART007", SqlDbType.NVarChar,50),
                    //new SqlParameter("@ART008", SqlDbType.NVarChar,255),
                    new SqlParameter("@ART009", SqlDbType.NVarChar,255),
                    new SqlParameter("@ART010", SqlDbType.NVarChar,5),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@ART002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART011", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART012", SqlDbType.Int,4),
                    new SqlParameter("@ART013", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . ART003;
            parameters [ 1 ] . Value = model . ART004;
            parameters [ 2 ] . Value = model . ART005;
            parameters [ 3 ] . Value = model . ART006;
            parameters [ 4 ] . Value = model . ART007;
            //parameters [ 5 ] . Value = model . ART008;
            parameters [ 5 ] . Value = model . ART009;
            parameters [ 6 ] . Value = model . ART010;
            parameters [ 7 ] . Value = model . idx;
            parameters [ 8 ] . Value = model . ART002;
            parameters [ 9 ] . Value = model . ART011;
            parameters [ 10 ] . Value = model . ART012;
            parameters [ 11 ] . Value = model . ART013;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyS ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . ArtEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKART set " );
            strSql . Append ( "ART003=@ART003," );
            strSql . Append ( "ART004=@ART004," );
            strSql . Append ( "ART005=@ART005," );
            strSql . Append ( "ART006=@ART006," );
            strSql . Append ( "ART007=@ART007," );
            strSql . Append ( "ART008=@ART008," );
            strSql . Append ( "ART009=@ART009," );
            strSql . Append ( "ART010=@ART010 " );
            strSql . Append ( " where ART001=@ART001 AND ART002=@ART002" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ART003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART004", SqlDbType.Decimal,9),
                    new SqlParameter("@ART005", SqlDbType.Decimal,9),
                    new SqlParameter("@ART006", SqlDbType.NVarChar,50),
                    new SqlParameter("@ART007", SqlDbType.NVarChar,50),
                    new SqlParameter("@ART008", SqlDbType.NVarChar,255),
                    new SqlParameter("@ART009", SqlDbType.NVarChar,255),
                    new SqlParameter("@ART010", SqlDbType.NVarChar,5),
                    new SqlParameter("@ART001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ART002", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . ART003;
            parameters [ 1 ] . Value = model . ART004;
            parameters [ 2 ] . Value = model . ART005;
            parameters [ 3 ] . Value = model . ART006;
            parameters [ 4 ] . Value = model . ART007;
            parameters [ 5 ] . Value = model . ART008;
            parameters [ 6 ] . Value = model . ART009;
            parameters [ 7 ] . Value = model . ART010;
            parameters [ 8 ] . Value = model . ART001;
            parameters [ 9 ] . Value = model . ART002;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyOne ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . AruEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKARU set " );
            strSql . Append ( "ARU003=@ARU003," );
            strSql . Append ( "ARU004=@ARU004 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ARU004", SqlDbType.NVarChar,50),
                    new SqlParameter("@ARU003", SqlDbType.NChar,10),
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . ARU004;
            parameters [ 1 ] . Value = model . ARU003;
            parameters [ 2 ] . Value = model . idx;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteBody ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKARS WHERE idx={0}" ,s );

            SQLString . Add ( strSql ,null );
        }
        void DeleteBodyOne ( Dictionary<object ,object> SQLString ,string s )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKARU WHERE idx={0}" ,s );

            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewAll ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,ARS001,ARS002,ARS003,DDA003,ARS004,ARS005,ARS006,ARS007,'厂内生产件' DEA010,ARS008,ARS009,DAA002 FROM MIKARS A INNER JOIN TPADAA B ON A.ARS009=B.DAA001 INNER JOIN TPADDA C ON A.ARS003=C.DDA001 INNER JOIN TPADEA D ON A.ARS001=D.DEA001 " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewMain ( string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,ART001,ART002,ART003,CONVERT(FLOAT,ART004) ART004,CONVERT(FLOAT,ART005) ART005,ART006,ART007,ART009,ART010,ART011,ART012,ART013 FROM MIKART WHERE ART001='{0}'" ,num );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取模具供应商
        /// </summary>
        /// <param name="piNum"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataTable getTableViewBody ( string piNum ,string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT A.idx,ARU001,ARU002,ARU003,ARU004,ART003 FROM MIKARU A INNER JOIN MIKART B ON A.ARU002=B.ART011 WHERE ARU001='{0}' AND ARU002='{1}' AND ART001='{0}' " ,piNum ,num );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKARS WHERE ARS001='{0}'" ,num );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu . ArtEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKART SET ART018=@ART018 WHERE idx=@idx" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@ART018",SqlDbType.Bit),
                new SqlParameter("@idx",SqlDbType.Int)
            };
            parameter [ 0 ] . Value = model . ART018;
            parameter [ 1 ] . Value = model . idx;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableProInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DEA001,DEA002,DEA057,DEA003,DEA008,DDA003,DEA009,'厂内生产件' DEA010,DEA076,DAA002,DEA962 FROM TPADEA A INNER JOIN TPADAA B ON A.DEA076=B.DAA001 INNER JOIN TPADDA C ON A.DEA008=C.DDA001 WHERE DEA009 IN ('M') AND DEA013='F'  ORDER BY DEA001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        public DataTable getTableProInfo ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DDA003,'厂内生产件' DEA010,DAA002 FROM TPADEA A INNER JOIN TPADAA B ON A.DEA076=B.DAA001 INNER JOIN TPADDA C ON A.DEA008=C.DDA001 WHERE DEA001='{0}'" ,piNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . ArsEntity getModel ( string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,ARS001,ARS002,ARS003,ARS004,ARS005,ARS006,ARS007,ARS008,ARS009,ARS011 FROM MIKARS " );
            strSql . Append ( " WHERE ARS001=@ARS001 " );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ARS001", SqlDbType.NVarChar,20)          };
            parameters [ 0 ] . Value = piNum;

            DataTable ds = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) ,parameters );
            if ( ds != null && ds . Rows . Count > 0 )
                return getModel ( ds . Rows [ 0 ] );
            else
                return null;
        }

        public LineProductMesEntityu . ArsEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . ArsEntity model = new LineProductMesEntityu . ArsEntity ( );
            if ( row != null )
            {
                if ( row [ "ARS001" ] != null )
                {
                    model . ARS001 = row [ "ARS001" ] . ToString ( );
                }
                if ( row [ "ARS002" ] != null )
                {
                    model . ARS002 = row [ "ARS002" ] . ToString ( );
                }
                if ( row [ "ARS003" ] != null )
                {
                    model . ARS003 = row [ "ARS003" ] . ToString ( );
                }
                if ( row [ "ARS004" ] != null )
                {
                    model . ARS004 = row [ "ARS004" ] . ToString ( );
                }
                if ( row [ "ARS005" ] != null )
                {
                    model . ARS005 = row [ "ARS005" ] . ToString ( );
                }
                if ( row [ "ARS006" ] != null )
                {
                    model . ARS006 = row [ "ARS006" ] . ToString ( );
                }
                if ( row [ "ARS007" ] != null )
                {
                    model . ARS007 = row [ "ARS007" ] . ToString ( );
                }
                if ( row [ "ARS008" ] != null && row [ "ARS008" ] . ToString ( ) != "" )
                {
                    model . ARS008 = decimal . Parse ( row [ "ARS008" ] . ToString ( ) );
                }
                if ( row [ "ARS009" ] != null )
                {
                    model . ARS009 = row [ "ARS009" ] . ToString ( );
                }
                if ( row [ "ARS011" ] != null && row [ "ARS011" ] . ToString ( ) != "" )
                {
                    model . ARS011 = decimal . Parse ( row [ "ARS011" ] . ToString ( ) );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取工艺信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableArt ( string workId )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT QBA001,QBA002,QBA006,QBA007 FROM SGMQBA WHERE QBA006='{0}'" ,workId );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取人员岗位
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePost ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT EMP007 FROM MIKEMP WHERE EMP037=1 AND EMP007 IS NOT NULL" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取导出数据
        /// </summary>
        /// <param name="piList"></param>
        /// <returns></returns>
        public DataTable getTableExport ( List<string> piList  )
        {
            string str = string . Empty;
            foreach ( string s in piList )
            {
                if ( str == string . Empty )
                    str = "'" + s + "'";
                else
                    str = str + "," + "'" + s + "'";
            }
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ARS001 品号,ARS002 品名,ARS003 仓库,DDA003 仓库名称,ARS004 规格,ARS005 材质,ARS006 单位,ARS007 主要来源,'厂内生产件' 厂内生产件,ARS008 单头单位克重,ARS009 部门编号,DAA002 部门名称,ART011 序号,ART002 工艺编号,ART003 工艺名称,CONVERT(FLOAT,ART004) 工艺单价,CONVERT(FLOAT,ART005) 单身单位克重,ART006 机台编号,ART007 使用机台,ART009 备注,ART010 是否末级,ART012 工时,ART013 岗位 FROM MIKARS A INNER JOIN MIKART B ON A.ARS001=B.ART001 INNER JOIN TPADDA C ON A.ARS003=C.DDA001 INNER JOIN TPADAA D ON A.ARS009=D.DAA001 WHERE ARS001 IN ({0}) ORDER BY ARS001,ART011 " ,str );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
