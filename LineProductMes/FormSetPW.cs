using DevExpress . XtraEditors;

namespace LineProductMes
{
    public partial class FormSetPW :FormBaseChild
    {
        public FormSetPW ( )
        {
            InitializeComponent ( );

            this . texNumPerson . Text = LineProductMesBll . UserInfoMation . userNum;
            this . texNamePerson . Text = LineProductMesBll . UserInfoMation . userName;
        }
        
        private void btnOk_Click ( object sender ,System . EventArgs e )
        {
            dxErrorProvider1 . ClearErrors ( );
            bool isOk = true;
            if ( string . IsNullOrEmpty ( texNumPerson . Text ) )
            {
                dxErrorProvider1 . SetError ( texNumPerson ,"用户编号不可为空" );
                isOk = false;
            }
            if ( string . IsNullOrEmpty ( texNamePerson . Text ) )
            {
                dxErrorProvider1 . SetError ( texNamePerson ,"用户姓名不可为空" );
                isOk = false;
            }
            if ( string . IsNullOrEmpty ( texPW . Text ) )
            {
                dxErrorProvider1 . SetError ( texPW ,"口令不可为空" );
                isOk = false;
            }
            if ( string . IsNullOrEmpty ( texPWS . Text ) )
            {
                dxErrorProvider1 . SetError ( texNumPerson ,"请确认口令" );
                isOk = false;
            }
            if ( isOk == false )
                return;

            if ( texPW . Text . Trim ( ) != texPWS . Text . Trim ( ) )
            {
                XtraMessageBox . Show ( "两次输入口令不一致,请重新输入" );
                return;
            }

            string pwdMD5 = Utility . DesEncryptUtil . EncryptMD5 ( texPW . Text );
            LineProductMesBll . Bll . EmployeeBll _bll = new LineProductMesBll . Bll . EmployeeBll ( );
            isOk = _bll . EditPw ( texNumPerson . Text ,pwdMD5 );
            if ( isOk == true )
                XtraMessageBox . Show ( "成功设置口令" );
            else
                XtraMessageBox . Show ( "设置口令失败" );
        }

        private void btnCancel_Click ( object sender ,System . EventArgs e )
        {
            this . Close ( );
        }

    }
}
