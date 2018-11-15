using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace StudentMgr
{
    public static class SqlHelper
    {
        //public static readonly string connstr =
            //ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        private static string connstr = string.Empty;
        //Sql Server数据库连接字符串
        public static string Connstr
        {
            get
            {
                if ( string.IsNullOrEmpty( connstr ) )
                {
                    string host = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"host" );
                    string dbname = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"dbname" );
                    string user = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"user" );
                    string password = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"password" );
                    connstr = string.Format( "Data Source={0};Initial Catalog={1};User Id={2};Password={3}" ,host ,dbname ,user ,password );   
                }

                return connstr;
            }
        }

        /// <summary>
        /// 连接数据库是否成功
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public static bool connectionTest ( string connstr )
        {
            SqlConnection conn = new SqlConnection( connstr );
            return QuickOpen( conn ,500 );
        }

        public static bool QuickOpen ( this SqlConnection conn ,int timeout )
        {
            // We'll use a Stopwatch here for simplicity. A comparison to a stored DateTime.Now value could also be used
            Stopwatch sw = new Stopwatch( );
            bool connectSuccess = false;

            // Try to open the connection, if anything goes wrong, make sure we set connectSuccess = false
            Thread t = new Thread( delegate ( )
            {
                try
                {
                    sw.Start( );
                    conn.Open( );
                    connectSuccess = true;
                }
                catch { }
            } );

            // Make sure it's marked as a background thread so it'll get cleaned up automatically
            t.IsBackground = true;
            t.Start( );

            // Keep trying to join the thread until we either succeed or the timeout value has been exceeded
            while ( timeout > sw.ElapsedMilliseconds )
                if ( t.Join( 1 ) )
                    break;

            // If we didn't connect successfully, throw an exception
            //if ( !connectSuccess )
            //    //throw new Exception( "Timed out while trying to connect." );
            //    return connectSuccess;
            //else
            //    return false;

            return connectSuccess;
        }

        /// <summary>
        /// 连接并打开数据库
        /// </summary>
        /// <returns></returns>
        public static SqlConnection OpenConnection( )
        {
            SqlConnection conn = new SqlConnection( connstr );
            conn.Open( );
            return conn;
        }
        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery( string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlConnection conn = new SqlConnection( Connstr ))
            {
                conn.Open( );
                return ExecuteNonQuery( conn, cmdText, parameters );
            }
        }

        /// <summary>
        /// 执行没有参数的SQL语句  返回受影响的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery ( string cmdText )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteNonQuery( conn ,cmdText );
            }
        }

        public static object ExecuteScalar( string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlConnection conn = new SqlConnection( Connstr ))
            {
                conn.Open( );
                return ExecuteScalar( conn, cmdText, parameters );
            }
        }
        /// <summary>
        /// 返回一个DataTable实例
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable( string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteDataTable( conn, cmdText, parameters );
            }
        }
        public static DataTable ExecuteDataTable ( string connstr,string cmdText ,
            params SqlParameter[] parameters )
        {
            using ( SqlConnection conn = new SqlConnection( connstr ) )
            {
                conn.Open( );
                return ExecuteDataTable( conn ,cmdText ,parameters );
            }
        }
        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DataSet Query ( string SQLString ,params object[] parameter )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteDataSet( conn ,SQLString ,parameter );
            }
        }

        public static int ExecuteNonQuery( SqlConnection conn, string cmdText,
           params SqlParameter[] parameters )
        {
            using (SqlCommand cmd = conn.CreateCommand( ))
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange( parameters );
                return cmd.ExecuteNonQuery( );
            }
        }

        public static int ExecuteNonQuery ( SqlConnection conn ,string cmdText )
        {
            using ( SqlCommand cmd = conn.CreateCommand() )
            {
                cmd.CommandText = cmdText;
                return cmd.ExecuteNonQuery( );
            }
        }

        public static object ExecuteScalar( SqlConnection conn, string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlCommand cmd = conn.CreateCommand( ))
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange( parameters );
                return cmd.ExecuteScalar( );
            }
        }

        public static DataTable ExecuteDataTable( SqlConnection conn, string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlCommand cmd = conn.CreateCommand( ))
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange( parameters );
                using (SqlDataAdapter adapter = new SqlDataAdapter( cmd ))
                {
                    DataTable dt = new DataTable( );
                    adapter.Fill( dt );
                    return dt;
                }
            }
        }

        public static DataSet ExecuteDataSet ( SqlConnection conn ,string SQLString ,params object[] parameter )
        {
            using ( SqlCommand cmd = conn.CreateCommand( ) )
            {
                PrepareCommand( cmd ,conn ,null ,SQLString ,parameter );
                using ( SqlDataAdapter da = new SqlDataAdapter( cmd ) )
                {
                    DataSet ds = new DataSet( );
                    try
                    {
                        da.Fill( ds ,"ds" );
                        cmd.Parameters.Clear( );
                    }
                    catch (SqlException ex) { throw new Exception( ex.Message ); }
                    finally {
                        cmd.Dispose( );
                        conn.Close( );
                    }

                    return ds;
                }
            }
        }

        /// <summary>
        /// 不用Sql语句更新数据到数据库
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool UpdateTable (DataTable table,string sql,params object[] parameter)
        {
            bool result = false;
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                DataSet ds = new DataSet( );

                try
                {
                    table.TableName = "R_PQZB";
                    ds.Tables.Add( table.Copy( ) );
                    SqlDataAdapter sda = new SqlDataAdapter( );
                    sda.SelectCommand = new SqlCommand( sql ,conn );
                    SqlCommandBuilder cd = new SqlCommandBuilder( sda );
                    conn.Open( );
                    sda.Fill( ds );
                    table = ds.Tables[0];
                    int row = sda.Update( table );
                    table.AcceptChanges( );
                    result = true;
                }
                catch ( SqlException ex )
                {
                    
                }
                finally
                {
                    conn.Close( );
                }
            }
            return result;
        }


        #region 执行存储过程

        private static string _proceName; //存储过程名

        private static void StoreProcedure ( string sprocName )
        {
            _proceName = sprocName;
        }

        /// <summary>
        /// 执行存储过程  不返回值
        /// </summary>
        /// <param name="sprocName">存储过程名</param>
        /// <param name="parameters">参数</param>
        public static void ExecuteNoStore (  params SqlParameter[] parameters )
        {
            using (SqlConnection conn=new SqlConnection(Connstr) )
            {
                SqlCommand cmd = new SqlCommand( _proceName ,conn );
                cmd.CommandType = CommandType.StoredProcedure;
                AddInParaValues( cmd ,parameters );
                conn.Open( );
                cmd.ExecuteNonQuery( );
                conn.Close( );
            }
        }

        /// <summary>
        /// 执行存储过程  返回一个表
        /// </summary>
        /// <param name="sprocName">存储过程名称</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableToStore ( params SqlParameter[] parameters )
        {
            SqlCommand cmd = new SqlCommand( _proceName ,new SqlConnection( Connstr ) );
            cmd.CommandType = CommandType.StoredProcedure;
            AddInParaValues( cmd ,parameters );
            SqlDataAdapter ada = new SqlDataAdapter( cmd );
            DataTable dt = new DataTable( );
            ada.Fill( dt );
            return dt;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象   在SqlDataReader对象关闭的同时，数据库连接自动关闭
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReaderStore ( params SqlParameter[] parameters )
        {
            SqlConnection conn = new SqlConnection( Connstr );
            SqlCommand cmd = new SqlCommand( _proceName ,conn );
            cmd.CommandType = CommandType.StoredProcedure;
            AddInParaValues( cmd ,parameters );
            conn.Open( );
            return cmd.ExecuteReader( CommandBehavior.CloseConnection );
        }

        /// <summary>
        /// 获取存储过程的参数列表
        /// </summary>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        private static ArrayList GetParameters ( )
        {
            SqlCommand cmd = new SqlCommand( "dbo.sp_sproc_columns_90" ,new SqlConnection( Connstr ) );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue( "@procedure_name" ,_proceName );
            SqlDataAdapter sda = new SqlDataAdapter( cmd );
            DataTable dt = new DataTable( );
            sda.Fill( dt );
            ArrayList al = new ArrayList( );
            for ( int i = 0 ; i < dt.Rows.Count ; i++ )
            {
                al.Add( dt.Rows[i][3].ToString( ) );
            }
            return al;
        }

        /// <summary>
        /// 为 SqlCommand 添加参数及赋值
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        private static void AddInParaValues ( SqlCommand cmd ,params SqlParameter[] parameters )
        {
            cmd.Parameters.Add( new SqlParameter( "@RESURN_VALUE" ,SqlDbType.Int ) );
            cmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
            if ( parameters != null )
            {
                ArrayList al = GetParameters( );
                for ( int i = 0 ; i < parameters.Length ; i++ )
                {
                    cmd.Parameters.AddWithValue( al[i + 1].ToString( ) ,parameters[i] );
                }
            }
        }

        #endregion


        public static void PrepareCommand ( SqlCommand cmd ,SqlConnection conn ,SqlTransaction trans ,string cmdText ,object[] parameter )
        {
            if ( conn.State != ConnectionState.Open )
                conn.Open( );
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if ( trans != null )
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            if ( parameter != null )
            {
                foreach ( SqlParameter para in parameter )
                {
                    if ( ( para.Direction == ParameterDirection.InputOutput  || para.Direction == ParameterDirection.Input ) && ( para.Value == null ) )
                    {
                        para.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add( para );
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLSting">计算查询结果语句</param>
        /// <param name="parameter"></param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle ( string SQLSting ,params object[] parameter )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                using ( SqlCommand cmd = new SqlCommand( ) )
                {
                    try
                    {
                        PrepareCommand( cmd ,conn ,null ,SQLSting ,parameter );
                        object obj = cmd.ExecuteScalar( );
                        if ( ( object.Equals( obj ,null ) ) || ( object.Equals( obj ,System.DBNull.Value ) ) )
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch( SqlException e )
                    {
                        throw new Exception( e.Message );
                    }
                    finally {
                        cmd.Dispose( );
                        conn.Close( );
                    }
                }    
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool Exists ( string sql ,params object[] parameter )
        {
            object result = GetSingle( sql ,parameter );
            if ( result == null )
                return false;
            long count = 0;
            long.TryParse( result.ToString( ) ,out count );
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 执行SQL语句，返回自增列值
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int ExecuteSqlReturnId ( string SQLString ,params object[] parameter )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                using ( SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand( cmd ,conn ,null ,SQLString ,parameter );
                        object obj = cmd.ExecuteScalar( );

                        cmd.Parameters.Clear( );
                        if ( obj == null )
                            return 0;
                        else
                            return Convert.ToInt32( obj );
                    }
                    catch (Exception E)
                    {
                        throw new Exception( E.Message );
                    }
                    finally {
                        cmd.Dispose( );
                        conn.Close( );
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList"></param>
        public static bool ExecuteSqlTran ( ArrayList SQLStringList )
        {
            using (SqlConnection conn=new SqlConnection(Connstr))
            {
                conn.Open( );
                SqlCommand cmd = new SqlCommand( );
                cmd.Connection = conn;
                SqlTransaction tran = conn.BeginTransaction( );
                cmd.Transaction = tran;
                try
                {
                    for ( int i = 0 ; i < SQLStringList.Count ; i++ )
                    {
                        string strsql = SQLStringList[i].ToString( );
                        if ( strsql.Trim( ).Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery( );
                        }
                    }
                    tran.Commit( );
                    return true;
                }
                catch
                {
                    tran.Rollback( );
                    return false;
                }
                finally
                {
                    cmd.Dispose( );
                    conn.Close( );
                }
            }
        }

        public static object ToDBValue(this object value)
        {
            return value == null ? DBNull.Value : value;
        }

        public static object FromDBValue(this object dbValue)
        {
            return dbValue == DBNull.Value ? null : dbValue;
        }
    }
}
