using System . Text;
using StudentMgr;
using System . Data;
using System . Collections;
using System;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class BoarSetDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,BOS001,BOS002,BOS003,BOS004,BOS005 FROM MIKBOS" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool Save ( DataTable table )
        {
            Hashtable SQLString = new Hashtable ( );
            LineProductMesEntityu . BoarSetEntity model = new LineProductMesEntityu . BoarSetEntity ( );
            foreach ( DataRow row in table . Rows )
            {
                model . IDX = Convert . ToInt32 ( row [ "idx" ] );
                model . BOS001 = row [ "BOS001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( row [ "BOS002" ] . ToString ( ) ) )
                    model . BOS002 = null;
                else
                    model . BOS002 = Convert . ToInt32 ( row [ "BOS002" ] );
                if ( string . IsNullOrEmpty ( row [ "BOS003" ] . ToString ( ) ) )
                    model . BOS003 = null;
                else
                    model . BOS003 = Convert . ToInt32 ( row [ "BOS003" ] );
                if ( string . IsNullOrEmpty ( row [ "BOS004" ] . ToString ( ) ) )
                    model . BOS004 = null;
                else
                    model . BOS004 = Convert . ToInt32 ( row [ "BOS004" ] );
                if ( string . IsNullOrEmpty ( row [ "BOS005" ] . ToString ( ) ) )
                    model . BOS005 = null;
                else
                    model . BOS005 = Convert . ToDateTime ( row [ "BOS005" ] );
                StringBuilder strSql = new StringBuilder ( );
                strSql . Append ( "UPDATE MIKBOS SET BOS002=@BOS002,BOS003=@BOS003,BOS004=@BOS004,BOS005=@BOS005 WHERE idx=@idx" );
                SqlParameter [ ] parameter = {
                    new SqlParameter("@idx",SqlDbType.Int,4),
                    new SqlParameter("@BOS002",SqlDbType.Int,4),
                    new SqlParameter("@BOS003",SqlDbType.Int,4),
                    new SqlParameter("@BOS004",SqlDbType.Int,4),
                    new SqlParameter("@BOS005",SqlDbType.DateTime)
                };
                parameter [ 0 ] . Value = model . IDX;
                parameter [ 1 ] . Value = model . BOS002;
                parameter [ 2 ] . Value = model . BOS003;
                parameter [ 3 ] . Value = model . BOS004;
                parameter [ 4 ] . Value = model . BOS005;
                SQLString . Add ( strSql ,parameter );
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

    }
}
