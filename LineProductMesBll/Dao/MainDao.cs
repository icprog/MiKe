using System . Collections . Generic;
using System . Text;
using StudentMgr;
using System . Data;
using System . Threading . Tasks;

namespace LineProductMesBll . Dao
{
    public class MainDao
    {
        /// <summary>
        /// 获取系统目录
        /// </summary>
        /// <returns></returns>
        public List<LineProductMesEntityu . MainEntity> GetModel ( string userNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            if ( userNum . Equals ( "DS" ) )
                strSql . Append ( "SELECT FOR001,FOR002,FOR003,FOR004,FOR005 FROM MIKFOR" );
            else
            {
                strSql . Append ( "WITH CET AS (" );
                strSql . AppendFormat ( "SELECT FOR001,FOR002,FOR003,FOR004,FOR005 FROM MIKFOR A INNER JOIN MIKPOW B ON A.FOR003=B.POW003 INNER JOIN MIKEMP C ON B.POW002=C.EMP001  WHERE  POW002='{0}' and POW013=1 " ,userNum );
                strSql . Append ( ") SELECT FOR001,FOR002,FOR003,FOR004,FOR005 FROM MIKFOR WHERE FOR001 IN (SELECT FOR002 FROM CET) UNION  SELECT * FROM CET UNION SELECT FOR001,FOR002,FOR003,FOR004,FOR005 FROM MIKFOR WHERE FOR006 = 6 " );
            }
            
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
            {
                List<LineProductMesEntityu . MainEntity> modelList = new List<LineProductMesEntityu . MainEntity> ( );
                foreach ( DataRow row in table . Rows )
                {
                    modelList . Add ( getModel ( row ) );
                }
                return modelList;
            }
        }

        public LineProductMesEntityu . MainEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . MainEntity model = new LineProductMesEntityu . MainEntity ( );
            if ( row != null )
            {
                if ( row [ "FOR001" ] != null && row [ "FOR001" ] . ToString ( ) != "" )
                {
                    model . FOR001 = int . Parse ( row [ "FOR001" ] . ToString ( ) );
                }
                if ( row [ "FOR002" ] != null && row [ "FOR002" ] . ToString ( ) != "" )
                {
                    model . FOR002 = int . Parse ( row [ "FOR002" ] . ToString ( ) );
                }
                if ( row [ "FOR003" ] != null )
                {
                    model . FOR003 = row [ "FOR003" ] . ToString ( );
                }
                if ( row [ "FOR004" ] != null )
                {
                    model . FOR004 = row [ "FOR004" ] . ToString ( );
                }
                if ( row [ "FOR005" ] != null )
                {
                    model . FOR005 = row [ "FOR005" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取人员权限信息
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="programNum"></param>
        /// <returns></returns>
        public List<LineProductMesEntityu.PowerEntity> getPowerList ( )
        {
            Task task = new Task ( editBOSTime );
            task . Start ( );

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT POW002,POW003,POW004,POW005,POW006,POW007,POW009,POW010,POW011,POW012,POW013,POW016,POW017 FROM MIKPOW " );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return null;
            else
            {
                List<LineProductMesEntityu . PowerEntity> powList = new List<LineProductMesEntityu . PowerEntity> ( );
                foreach ( DataRow row in table . Rows )
                {
                    powList . Add ( getModelPow ( row ) );
                }
                return powList;
            }
        }

        /// <summary>
        /// 编辑看板车间显示日期
        /// </summary>
        void editBOSTime ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKBOS SET BOS005=DATEADD(DAY,-1,GETDATE())" );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

        public LineProductMesEntityu . PowerEntity getModelPow ( DataRow row )
        {
            LineProductMesEntityu . PowerEntity model = new LineProductMesEntityu . PowerEntity ( );
            if ( row != null )
            {
                if ( row [ "POW002" ] != null )
                {
                    model . POW002 = row [ "POW002" ] . ToString ( );
                }
                if ( row [ "POW003" ] != null )
                {
                    model . POW003 = row [ "POW003" ] . ToString ( );
                }
                if ( row [ "POW004" ] != null && row [ "POW004" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW004" ] . ToString ( ) == "1" ) || ( row [ "POW004" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW004 = true;
                    }
                    else
                    {
                        model . POW004 = false;
                    }
                }
                if ( row [ "POW005" ] != null && row [ "POW005" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW005" ] . ToString ( ) == "1" ) || ( row [ "POW005" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW005 = true;
                    }
                    else
                    {
                        model . POW005 = false;
                    }
                }
                if ( row [ "POW006" ] != null && row [ "POW006" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW006" ] . ToString ( ) == "1" ) || ( row [ "POW006" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW006 = true;
                    }
                    else
                    {
                        model . POW006 = false;
                    }
                }
                if ( row [ "POW007" ] != null && row [ "POW007" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW007" ] . ToString ( ) == "1" ) || ( row [ "POW007" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW007 = true;
                    }
                    else
                    {
                        model . POW007 = false;
                    }
                }
                if ( row [ "POW009" ] != null && row [ "POW009" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW009" ] . ToString ( ) == "1" ) || ( row [ "POW009" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW009 = true;
                    }
                    else
                    {
                        model . POW009 = false;
                    }
                }
                if ( row [ "POW010" ] != null && row [ "POW010" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW010" ] . ToString ( ) == "1" ) || ( row [ "POW010" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW010 = true;
                    }
                    else
                    {
                        model . POW010 = false;
                    }
                }
                if ( row [ "POW011" ] != null && row [ "POW011" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW011" ] . ToString ( ) == "1" ) || ( row [ "POW011" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW011 = true;
                    }
                    else
                    {
                        model . POW011 = false;
                    }
                }
                if ( row [ "POW012" ] != null && row [ "POW012" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW012" ] . ToString ( ) == "1" ) || ( row [ "POW012" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW012 = true;
                    }
                    else
                    {
                        model . POW012 = false;
                    }
                }
                if ( row [ "POW013" ] != null && row [ "POW013" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW013" ] . ToString ( ) == "1" ) || ( row [ "POW013" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW013 = true;
                    }
                    else
                    {
                        model . POW013 = false;
                    }
                }
                if ( row [ "POW016" ] != null && row [ "POW016" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW016" ] . ToString ( ) == "1" ) || ( row [ "POW016" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW016 = true;
                    }
                    else
                    {
                        model . POW016 = false;
                    }
                }
                if ( row [ "POW017" ] != null && row [ "POW017" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "POW017" ] . ToString ( ) == "1" ) || ( row [ "POW017" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . POW017 = true;
                    }
                    else
                    {
                        model . POW017 = false;
                    }
                }
            }
            return model;
        }

    }
}
