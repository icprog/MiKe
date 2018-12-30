using System . Data;
using System . Text;
using StudentMgr;
using System;
using System . Collections . Generic;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class LineForAssPlanDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( DateTime dtStart,DateTime dtEnd ,Dictionary<string ,string> strDic )
        {
            string productName = string . Empty;
            foreach ( string str in strDic . Keys )
            {
                if ( string . IsNullOrEmpty ( productName ) )
                    productName = "'" + str + "'";
                else
                    productName = productName + "," + "'" + str + "'";
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRF001,PRF002,PRF003 FROM MIKPRF WHERE PRF001 IN ({0}) " ,productName );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;

            strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '0507%' AND DAA002 LIKE '%线'" );
            DataTable work = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( work == null || work . Rows . Count < 1 )
                return null;
            
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRG001,PRG002,PRG004 FROM MIKPRG WHERE PRG001 IN ({0})" ,productName );
            DataTable tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );

            LineProductMesEntityu . LineForAssPlanEntity model = new LineProductMesEntityu . LineForAssPlanEntity ( );
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            foreach ( DataRow row in table . Rows )
            {
                model . PRG001 = row [ "PRF001" ] . ToString ( );
                model . PRG002 = Convert . ToDateTime ( row [ "PRF002" ] );
                model . PRG006 = Convert . ToInt32 ( row [ "PRF003" ] );

                foreach ( DataRow ro in work . Rows )
                {
                    model . PRG004 = ro [ "DAA001" ] . ToString ( );
                    model . PRG005 = ro [ "DAA002" ] . ToString ( );
                    if ( tableOne == null || tableOne . Rows . Count < 1 )
                        Add ( SQLString ,model );
                    else
                    {
                        if ( tableOne . Select ( "PRG002='" + model . PRG002 + "' AND PRG004='" + model . PRG004 + "' AND PRG001='" + model . PRG001 + "'" ) . Length < 1 )
                            Add ( SQLString ,model );
                        else
                            Edit ( SQLString ,model );
                    }
                }
            }

            

            if ( SqlHelper . ExecuteSqlTranDic ( SQLString ) )
            {
                //执行存储过程，取出数据
                SqlHelper . StoreProcedure ( "LINEAIN" );
                SqlParameter [ ] parameter = {
                    new SqlParameter("@DTSTART",dtStart),
                    new SqlParameter("@DTEND",dtEnd),
                    new SqlParameter("@PRONUM",productName)
                };
                return SqlHelper . ExecuteNoStoreTable ( parameter );
            }
            else
                return null;
        }

        void Add ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LineForAssPlanEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKPRG(" );
            strSql . Append ( "PRG001,PRG002,PRG004,PRG005,PRG006)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@PRG001,@PRG002,@PRG004,@PRG005,@PRG006)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRG001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRG002", SqlDbType.Date,3),
                    new SqlParameter("@PRG004", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRG005", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRG006", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . PRG001;
            parameters [ 1 ] . Value = model . PRG002;
            parameters [ 2 ] . Value = model . PRG004;
            parameters [ 3 ] . Value = model . PRG005;
            parameters [ 4 ] . Value = model . PRG006;
            SQLString . Add ( strSql ,parameters );
        }
        void Edit ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LineForAssPlanEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPRG set " );
            strSql . Append ( "PRG005=@PRG005," );
            strSql . Append ( "PRG006=@PRG006" );
            strSql . Append ( " where PRG001=@PRG001 AND PRG002=@PRG002 AND PRG004=@PRG004" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRG005", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRG006", SqlDbType.Int,4),
                    new SqlParameter("@PRG001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRG002", SqlDbType.Date,3),
                    new SqlParameter("@PRG004", SqlDbType.NVarChar,20)};
            parameters [ 0 ] . Value = model . PRG005;
            parameters [ 1 ] . Value = model . PRG006;
            parameters [ 2 ] . Value = model . PRG001;
            parameters [ 3 ] . Value = model . PRG002;
            parameters [ 4 ] . Value = model . PRG004;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( DataTable table )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            LineProductMesEntityu . LineForAssPlanEntity model = new LineProductMesEntityu . LineForAssPlanEntity ( );

            foreach ( DataRow row in table . Rows )
            {
                model . PRG001 = row [ "主件品号" ] . ToString ( );
                model . PRG004 = row [ "产线编号" ] . ToString ( );
                model . PRG005 = row [ "产线" ] . ToString ( );
                foreach ( DataColumn column in table . Columns )
                {
                    if ( column . ColumnName != "产线编号" && column . ColumnName != "产线" && column . ColumnName != "主件品号" && column . ColumnName != "主件品名" && column . ColumnName != "排产量" && column . ColumnName != "总排量" )
                    {
                        model . PRG002 = Convert . ToDateTime ( column . ColumnName );
                        if ( string . IsNullOrEmpty ( row [ column ] . ToString ( ) ) )
                            model . PRG003 = null;
                        else 
                            model . PRG003 = Convert . ToInt32 ( row [ column ] );
                        if ( model . PRG003 == 0 )
                            model . PRG003 = null;
                        Edits ( SQLString ,model );
                    }
                }
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void Edits ( Dictionary<object ,object> SQLString ,LineProductMesEntityu . LineForAssPlanEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "update MIKPRG set " );
            strSql . Append ( "PRG003=@PRG003 " );
            strSql . Append ( " WHERE PRG001=@PRG001 AND PRG002=@PRG002 AND PRG004=@PRG004" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRG003", SqlDbType.Int,4),
                    new SqlParameter("@PRG001", SqlDbType.NVarChar,20),
                    new SqlParameter("@PRG002", SqlDbType.Date,3),
                    new SqlParameter("@PRG004", SqlDbType.NVarChar,20)};
            parameters [ 0 ] . Value = model . PRG003;
            parameters [ 1 ] . Value = model . PRG001;
            parameters [ 2 ] . Value = model . PRG002;
            parameters [ 3 ] . Value = model . PRG004;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 获取同品号,同日期的排产总量
        /// </summary>
        /// <returns></returns>
        public DataTable getTableGroupSum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT PRG001,PRG002,PRG006 FROM MIKPRG" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
