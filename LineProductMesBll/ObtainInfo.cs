using System;
using System . Data;
using System . Text;
using StudentMgr;

namespace LineProductMesBll
{
    public static class ObtainInfo
    {
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public static DateTime getTime ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT GETDATE() t" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
                return string . IsNullOrEmpty ( table . Rows [ 0 ] [ "t" ] . ToString ( ) ) == true ? DateTime . Now : Convert . ToDateTime ( table . Rows [ 0 ] [ "t" ] . ToString ( ) );
            else
                return DateTime . Now;
        }

        /// <summary>
        /// 获取设置的车间读取日期
        /// </summary>
        /// <param name="workName"></param>
        /// <returns></returns>
        public static string getDate ( string workName )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT BOS005 FROM MIKBOS WHERE BOS001='{0}'" ,workName );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return DateTime . Now . ToString ( "yyyy-MM-dd" );
            else
            {
                string dateTime = table . Rows [ 0 ] [ "BOS005" ] . ToString ( );
                if ( string . IsNullOrEmpty ( dateTime ) )
                    return DateTime . Now . ToString ( "yyyy-MM-dd" );
                else
                    return ( Convert . ToDateTime ( dateTime ) ) . ToString ( "yyyy-MM-dd" );
            }
        }

        /// <summary>
        /// 获取设置的显示数据切换时间
        /// </summary>
        /// <param name="workName"></param>
        /// <returns></returns>
        public static int getTimeChange ( string workName )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT BOS003 FROM MIKBOS WHERE BOS001='{0}'" ,workName );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return 5;
            else
            {
                string code = table . Rows [ 0 ] [ "BOS003" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return 5;
                else
                    return Convert . ToInt32 ( code );
            }
        }

        /// <summary>
        /// 获取设置的显示数据切换行数
        /// </summary>
        /// <param name="workName"></param>
        /// <returns></returns>
        public static int getNumData ( string workName )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT BOS004 FROM MIKBOS WHERE BOS001='{0}'" ,workName );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return 20;
            else
            {
                string code = table . Rows [ 0 ] [ "BOS004" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return 20;
                else
                    return Convert . ToInt32 ( code );
            }
        }

        /// <summary>
        /// 获取库名称
        /// </summary>
        /// <returns></returns>
        public static string getDataBase ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT BAA002 FROM TPABAA" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            return table . Rows [ 0 ] [ "BAA002" ] . ToString ( );
        }

        /// <summary>
        /// 组装
        /// </summary>
        public static string codeOne="组装";

        /// <summary>
        /// 组装附件
        /// </summary>
        public static string codeTwo="组装附件";

        /// <summary>
        /// 五金车间
        /// </summary>
        public static string codeTre="五金";

        /// <summary>
        /// 注塑车间  计件
        /// </summary>
        public static string codeFor="注塑计件";

        /// <summary>
        /// 注塑车间  计时
        /// </summary>
        public static string codeFiv="注塑计时";

        /// <summary>
        /// LED
        /// </summary>
        public static string codeSix="LED";

        /// <summary>
        /// 面板
        /// </summary>
        public static string codeSev="面板";

        /// <summary>
        /// 物流组
        /// </summary>
        public static string codeEgi="物流组";

        /// <summary>
        /// 喷漆
        /// </summary>
        public static string codeNin="喷漆";

    }
}
