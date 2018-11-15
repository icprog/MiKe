using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data . SqlClient;
using System . Collections;

namespace LineProductMesBll . Dao
{
    public class ProControlDao
    {
        /// <summary>
        /// 获取根节点信息
        /// </summary>
        /// <returns></returns>
        public DataTable getPInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT FOR001,FOR004 FROM MIKFOR ORDER BY FOR001" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            DataRow row = table . NewRow ( );
            row [ "FOR001" ] = "0";
            row [ "FOR004" ] = "根";
            table . Rows . InsertAt ( row ,0 );
            return table;
        }

        /// <summary>
        /// 是否存在节点和父节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Exists ( LineProductMesEntityu .MainEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM MIKFOR WHERE FOR001={0} AND FOR002={1}" ,model . FOR001 ,model . FOR002 );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . MainEntity model )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            if ( Exists ( model ) )
                Edit ( SQLString ,strSql ,model );
            else
                Add ( SQLString ,strSql ,model );

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void Add ( Hashtable SQLString, StringBuilder strSql ,LineProductMesEntityu.MainEntity model)
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKFOR(" );
            strSql . Append ( "FOR001,FOR002,FOR003,FOR004,FOR005,FOR006)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@FOR001,@FOR002,@FOR003,@FOR004,@FOR005,@FOR006)" );
            strSql . Append ( ";select @@IDENTITY" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@FOR001", SqlDbType.Int,4),
                    new SqlParameter("@FOR002", SqlDbType.Int,4),
                    new SqlParameter("@FOR003", SqlDbType.NVarChar,50),
                    new SqlParameter("@FOR004", SqlDbType.NVarChar,50),
                    new SqlParameter("@FOR005", SqlDbType.NVarChar,100),
                    new SqlParameter("@FOR006", SqlDbType.VarChar,10)
            };
            parameters [ 0 ] . Value = model . FOR001;
            parameters [ 1 ] . Value = model . FOR002;
            parameters [ 2 ] . Value = model . FOR003;
            parameters [ 3 ] . Value = model . FOR004;
            parameters [ 4 ] . Value = model . FOR005;
            parameters [ 5 ] . Value = model . FOR006;
            SQLString . Add ( strSql ,parameters );
        }

        void Edit ( Hashtable SQLString ,StringBuilder strSql ,LineProductMesEntityu . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKFOR set " );
            strSql . Append ( "FOR003=@FOR003," );
            strSql . Append ( "FOR004=@FOR004," );
            strSql . Append ( "FOR005=@FOR005," );
            strSql . Append ( "FOR006=@FOR006 " );
            strSql . Append ( " WHERE FOR001=@FOR001 AND " );
            strSql . Append ( "FOR002=@FOR002" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@FOR001", SqlDbType.Int,4),
                    new SqlParameter("@FOR002", SqlDbType.Int,4),
                    new SqlParameter("@FOR003", SqlDbType.NVarChar,50),
                    new SqlParameter("@FOR004", SqlDbType.NVarChar,50),
                    new SqlParameter("@FOR005", SqlDbType.NVarChar,100),
                    new SqlParameter("@FOR006", SqlDbType.VarChar,10)
            };
            parameters [ 0 ] . Value = model . FOR001;
            parameters [ 1 ] . Value = model . FOR002;
            parameters [ 2 ] . Value = model . FOR003;
            parameters [ 3 ] . Value = model . FOR004;
            parameters [ 4 ] . Value = model . FOR005;
            parameters [ 5 ] . Value = model . FOR006;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete ( LineProductMesEntityu . MainEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM MIKFOR " );
            strSql . Append ( "WHERE FOR001=@FOR001 AND FOR002=@FOR002" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@FOR001",SqlDbType.Int,4),
                new SqlParameter("@FOR002",SqlDbType.Int,4)
            };
            parameter [ 0 ] . Value = model . FOR001;
            parameter [ 1 ] . Value = model . FOR002;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT FOR001,FOR002,FOR003,FOR004,FOR005,FOR006 FROM MIKFOR ORDER BY FOR001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
