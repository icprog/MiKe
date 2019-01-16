using AutoUpdate;
using DevExpress . XtraEditors;
using System;
using System . Windows . Forms;

namespace LineProductMes
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>

        [STAThread]

        static void Main ( )
        {
            DevExpress . Skins . SkinManager . EnableFormSkins ( );

            Application . SetUnhandledExceptionMode ( UnhandledExceptionMode . CatchException );
            //添加非UI上的异常.
            AppDomain . CurrentDomain . UnhandledException += new UnhandledExceptionEventHandler ( CurrentDomain_UnhandledException );

            FastReport . Utils . Res . LoadLocale ( Application . StartupPath + "\\Chinese (Simplified).frl" );

            Application . EnableVisualStyles ( );
            Application . SetCompatibleTextRenderingDefault ( false );

            startFromMain ( );
        }

        private static void CurrentDomain_UnhandledException ( object sender ,UnhandledExceptionEventArgs e )
        {
            try
            {
                Exception ex = ( Exception ) e . ExceptionObject;

                Utility . LogHelper . WriteLog ( ex . Message + "\n\nStack Trace:\n" + ex . StackTrace );
            }
            catch ( Exception exc )
            {
                try
                {
                    XtraMessageBox . Show ( " Error" ,
                        " Could not write the error to the log. Reason: "
                        + exc . Message ,MessageBoxButtons . OK ,MessageBoxIcon . Stop );
                }
                finally
                {
                    Application . Exit ( );
                }
            }
        }

        static void startFromMain ( )
        {
            //检查更新
            AppUpdate au = new AppUpdate ( );
            string msg = "";
            bool result = au . CheckAppVersion ( ref msg );

            if ( result == true )
                System . Diagnostics . Process . Start ( Application . StartupPath + @"\AutoUpdate.exe" );

            if ( !LineProductMesBll . Dao . EncryptQuery . getResult ( ) . Equals ( "D094" ,StringComparison . CurrentCultureIgnoreCase ) )
                return;
            //加载主窗体
            FormMain from = new FormMain ( );
            if ( from != null )
            {
                if ( !from . IsDisposed )
                    Application . Run ( from );
            }
        }

    }
}


//exec sp_dbcmptlevel MIKE,100  必须设置数据库的级别，才能正常行转列
