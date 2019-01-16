using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data;

namespace LineProductMesBll
{
    public static class GetCodeUtils
    {
        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public static string getOddNum ( string tableName ,string column )
        {
            string code = string . Empty;

            DateTime dt = UserInfoMation . sysTime;

            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX({1}) {1} FROM {0} WHERE {1} LIKE '%{2}%'" ,tableName ,column ,dt . ToString ( "yyyyMMdd" ) );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 0 )
                code = dt . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                code = table . Rows [ 0 ] [ column ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    code = dt . ToString ( "yyyyMMdd" ) + "001";
                else
                {
                    code = code . Substring ( 1 ,11 );
                    if ( code . Substring ( 0 ,8 ) . Equals ( dt . ToString ( "yyyyMMdd" ) ) )
                        code = ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
                    else
                        code = dt . ToString ( "yyyyMMdd" ) + "001";
                }
            }

            //组装 Z
            if ( tableName . Equals ( "MIKANW" ) )
                code = "Z" + code;
            //组装附件 F
            else if ( tableName . Equals ( "MIKANT" ) )
                code = "F" + code;
            //五金  W
            else if ( tableName . Equals ( "MIKHAW" ) )
                code = "W" + code;
            //注塑  S
            else if ( tableName . Equals ( "MIKIJA" ) )
                code = "S" + code;
            //LED   L
            else if ( tableName . Equals ( "MIKLEF" ) )
                code = "L" + code;
            //面板  M
            else if ( tableName . Equals ( "MIKLEC" ) )
                code = "M" + code;
            //喷漆  P
            else if ( tableName . Equals ( "MIKPAN" ) )
                code = "P" + code;
            //物流组 T
            else if ( tableName . Equals ( "MIKLGN" ) )
                code = "T" + code;

            return code;
        }
    }
}
