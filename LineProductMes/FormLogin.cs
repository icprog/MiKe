using DevExpress . XtraEditors;
using System;
using System . Configuration;
using System . Windows . Forms;
using LineProductMes . ClassForMain;
using LineProductMesBll;

namespace LineProductMes
{
    public partial class FormLogin :FormBaseChild
    {
        //获取Configuration对象
        Configuration config = ConfigurationManager . OpenExeConfiguration ( /*ConfigurationUserLevel.None*/System . Windows . Forms . Application . ExecutablePath );
        LineProductMesBll . Bll . EmployeeBll _bll =null;
        
        public FormLogin ( )
        {
            InitializeComponent ( );

            this . StartPosition = FormStartPosition . CenterScreen;

            _bll = new LineProductMesBll . Bll . EmployeeBll ( );

            labUserName . Visible = labPassW . Visible = labTip . Visible = false;

            SetImage . setImage ( pictureBox1 ,"SetConn.png" );

            UserInfoMation . userName = config . AppSettings . Settings [ "UserName" ] . Value;
            UserInfoMation . userNum = config . AppSettings . Settings [ "UserNum" ] . Value;
            UserInfoMation . sign = config . AppSettings . Settings [ "Sign" ] . Value;

            if ( UserInfoMation . sign . Equals ( "1" ) )
            {
                txtUserName . Text = UserInfoMation . userName;
                txtUserName . Tag = UserInfoMation . userNum;
            }
        }
        
        private void FormLogin_Load ( object sender ,EventArgs e )
        {
            pictureBox1 . Location = new System . Drawing . Point ( 164 ,147 );
            this . pictureBox1 . Size = new System . Drawing . Size ( 24 ,21 );

            if ( UserInfoMation . userName . Equals ( "组装看板" ) || UserInfoMation . userName . Equals ( "五金看板" )|| UserInfoMation . userName . Equals ( "注塑看板" ) || UserInfoMation . userName . Equals ( "LED看板" ) || UserInfoMation . userName . Equals ( "面板看板" ) || UserInfoMation . userName . Equals ( "喷漆看板" ) || UserInfoMation . userName . Equals ( "采购看板" ) )
                this . DialogResult = DialogResult . OK;
        }

        private void button2_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void btnOk_Click ( object sender ,EventArgs e )
        {
            labUserName . Visible = labPassW . Visible = labTip.Visible= false;
            bool isOk = true;
            if ( string . IsNullOrEmpty ( txtUserName . Text ) )
            {
                labUserName . Text = "请输入用户名";
                labUserName . Visible = true;
                isOk = false;
                txtUserName . Focus ( );
            }
            if ( string . IsNullOrEmpty ( txtPassW . Text ) )
            {
                labPassW . Text = "请输入密码";
                labPassW . Visible = true;
                isOk = false;
                txtPassW . Focus ( );
            }
            if ( isOk == false )
            {
                this . DialogResult = System . Windows . Forms . DialogResult . None;
                return;
            }

            UserInfoMation . userName = txtUserName . Text;
            try
            {
                readNum ( );
            }
            catch ( Exception ex )
            {
                XtraMessageBox . Show ( "请检查连接参数" );
                Utility . LogHelper . WriteLog ( ex . StackTrace );
                return;
            }
            UserInfoMation . sign = chxUserName . Checked == true ? "1" : "0";
            string password = txtPassW . Text;
            string pwdMD5 = Utility . DesEncryptUtil .EncryptMD5 ( password );

            try
            {
                bool result = _bll . yesOrNoLogin ( UserInfoMation . userName ,pwdMD5 );
                if ( result == false )
                {
                    this . DialogResult = System . Windows . Forms . DialogResult . None;
                    labTip . Text = "登录失败，用户名或密码错误";
                    labTip . Visible = true;
                    return;
                }

                SaveUser ( );
                DialogResult = System . Windows . Forms . DialogResult . OK;
                this . Close ( );
            }
            catch ( Exception ex )
            {
                XtraMessageBox . Show ( "请检查网络是否正常" );
                Utility . LogHelper . WriteLog ( ex . StackTrace );
                this . DialogResult = System . Windows . Forms . DialogResult . Cancel;
            }
        }

        protected void readNum ( )
        {
            UserInfoMation . userNum = _bll . GetUserNum ( UserInfoMation . userName );
            UserInfoMation . sysTime = ObtainInfo . getTime ( );
        }

        protected void SaveUser ( )
        {
            //删除<add>元素
            config . AppSettings . Settings . Remove ( "UserName" );
            config . AppSettings . Settings . Remove ( "UserNum" );
            config . AppSettings . Settings . Remove ( "Sign" );
            //写入<add>元素的value
            //config.AppSettings.Settings["UserName"].Value = userName;
            //config.AppSettings.Settings["PassWord"].Value = passWord;
            //增加<add> 元素
            config . AppSettings . Settings . Add ( "UserName" ,UserInfoMation . userName );
            config . AppSettings . Settings . Add ( "UserNum" ,UserInfoMation . userNum );
            config . AppSettings . Settings . Add ( "Sign" ,UserInfoMation . sign );
            //一定要记得保存，写不带参数的config.Save()也可以
            config . Save ( ConfigurationSaveMode . Modified );
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System . Configuration . ConfigurationManager . RefreshSection ( "appSettings" );
        }

        private void pictureBox1_Click ( object sender ,EventArgs e )
        {
            Connection . Form1 form = new Connection . Form1 ( );
            form . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            form . ShowDialog ( );
        }

    }
}
