using StudentMgr;
using System . Data;
using System . Text;

namespace LineProductMesBll . Dao
{
    public static class EncryptQuery
    {
        public static string encryPt ( string plainText ,string keyValue )
        {
            int len = keyValue . Length - 1;
            int _moveIndex = 0;
            string _result = string . Empty;

            for ( int i = 0 ; i < plainText . Length ; i++ )
            {
                _moveIndex = keyValue . LastIndexOf ( plainText [ i ] ) + i + 1 > len ?
                            ( ( keyValue . LastIndexOf ( plainText [ i ] ) + i + 1 ) % len ) - 1 :
                            ( keyValue . LastIndexOf ( plainText [ i ] ) + i + 1 ) % len;
                _result += keyValue . Substring ( _moveIndex ,1 );
            }
            return _result;
        }

        public static string getResult ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "select net_address from master..sysprocesses where net_address in ('D094665845D3')" );

            DataTable da = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( da != null && da . Rows . Count > 0 )
                return encryPt ( "MIKE" ,da . Rows [ 0 ] [ "net_address" ] . ToString ( ) );
            else
                return string . Empty;
        }
    }
}
