using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Collections . Generic;
using System . Data;
using System . Reflection;
using System . Windows . Forms;
using Utility;
using System . ComponentModel;

namespace LineProductMes
{
    public partial class FormEmployee :FormChild
    {
        LineProductMesBll . Bll . EmployeeBll _bll = null;
        LineProductMesEntityu . EmployeeEntity _model = null;
        List<LineProductMesEntityu . PUAEntity> model =null;
        string state=string.Empty;
        string statePrevious=string.Empty,focuseName=string.Empty;
        DataTable tableQuery;
        int count=0;
        List<int> intList=new List<int>();
        bool isOk=false;

        public FormEmployee ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . EmployeeBll ( );
            _model = new LineProductMesEntityu . EmployeeEntity ( );
            ToolBarContain . ToolbarC ( barTool ,toolExamin );

            UnEnable ( );

            GridViewMoHuSelect . SetFilter ( gridView1 );

            FieldInfo fi = typeof ( DevExpress . Utils . Paint . XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            wait . Hide ( );
            getValueForPUATask ( );
            getDepart ( );
            clear ( );
        }

        #region Main
        protected override int Add ( )
        {
            clear ( );
            Enable ( );
            addTool ( );         
            btnFirst . Enabled = false;
            btnLast . Enabled = false;
            btnNext . Enabled = false;
            btnPrevious . Enabled = false;
            //Graph . gra ( xtraTabControl1 ,"反" );
            tabPageOne . Refresh ( );
            state = "add";

            txtEMP001 . Text = _bll . GetNum ( );
            txtEMP031 . Text = "3.5";
            txtEMP032 . Text = "0.15";

            return 0;
        }
        protected override int Query ( )
        {
            toolQuery . Enabled = false;
            toolAdd . Enabled = false;

            wait . Show ( );
            backgroundWorker1 . RunWorkerAsync ( );

            return 0;
        }
        protected override int Edit ( )
        {
            Enable ( );

            editTool ( );

            btnFirst . Enabled = false;
            btnLast . Enabled = false;
            btnNext . Enabled = false;
            btnPrevious . Enabled = false;

            state = "edit";

            return 0;
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtEMP001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择需要删除的用户" );
                return 0;
            }

            if ( XtraMessageBox . Show ( "确定要删除整条信息?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
            {
                bool result = _bll . Delete ( Convert . ToInt32 ( txtEMP001 . Tag ) );
                if ( result )
                {
                    if ( gridView1 . DataRowCount < 1 )
                    {
                        clear ( );
                        btnFirst . Enabled = false;
                        btnLast . Enabled = false;
                        btnNext . Enabled = false;
                        btnPrevious . Enabled = false;
                    }
                    else
                    {
                        btnFirst . Enabled = true;
                        btnLast . Enabled = true;
                        btnNext . Enabled = true;
                        btnPrevious . Enabled = true;
                    }
                    statePrevious = string . Empty;
                    //Graph . gra ( xtraTabControl1 ,"反" );
                    tabPageOne . Refresh ( );
                    Query ( );
                }
                else
                    XtraMessageBox . Show ( "删除失败,请重试" );
            }

            return 0;
        }
        protected override int Save ( )
        {
            if ( getValue ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            serial ( _model );
            if ( state . Equals ( "add" ) )
            {
                _model . EMP033 = Utility . DesEncryptUtil . EncryptMD5 ( "123456" );
                _model . idx = _bll . Add ( _model );
                if ( _model . idx > 0 )
                {
                    txtEMP001 . Tag = _model . idx;
                    isOk = true;
                }
                else
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败,请重试" );
                }
            }
            else if ( state . Equals ( "edit" ) )
            {
                _model . idx = Convert . ToInt32 ( txtEMP001 . Tag );
                isOk = _bll . Edit ( _model );
                if ( isOk == false )
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败,请重试" );
                }
            }

            if ( isOk == true )
            {
                XtraMessageBox . Show ( "保存数据成功" );
                 ClassForMain.FormClosingState.formClost = true;
                UnEnable ( );

                btnFirst . Enabled = true;
                btnLast . Enabled = true;
                btnNext . Enabled = true;
                btnPrevious . Enabled = true;
                Query ( );
            }

            return 0;
        }
        protected override int Cancel ( )
        {
            cancelTool ( state );

            if ( state . Equals ( "add" ) )
            {
                clear ( );
                UnEnable ( );
                if ( gridView1 . DataRowCount > 0 )
                {
                    if ( count >= 0 && count <= gridView1 . DataRowCount - 1 )
                    {
                        DataRow row = gridView1 . GetDataRow ( count );
                        _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                        assignMent ( row ,_model );
                    }
                    toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                    toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                    toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                    toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                    toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                    toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                    toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                    btnFirst . Enabled = true;
                    btnLast . Enabled = true;
                    btnNext . Enabled = true;
                    btnPrevious . Enabled = true;
                }
                else
                {
                    btnFirst . Enabled = false;
                    btnLast . Enabled = false;
                    btnNext . Enabled = false;
                    btnPrevious . Enabled = false;
                }
            }
            else if ( state . Equals ( "edit" ) )
            {
                UnEnable ( );
                btnFirst . Enabled = true;
                btnLast . Enabled = true;
                btnNext . Enabled = true;
                btnPrevious . Enabled = true;
                //toolPrint . Visibility = toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }

            return 0;
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtEMP001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择需要注销的用户" );
                return 0;
            }

            if ( toolCancellation . Caption . Equals ( "注销" ) )
            {
                isOk = _bll . Cancel (  txtEMP001 . Text  ,true );
                if ( isOk == true )
                {
                    layoutControlItem37 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPic ( pictureEdit1 ,"注销" );
                    tabPageOne . Refresh ( );
                    Query ( );
                    cancelltionTool ( "反注销" );
                }
                else
                    XtraMessageBox . Show ( "注销失败" );
            }
            else if ( toolCancellation . Caption . Equals ( "反注销" ) )
            {
                isOk = _bll . Cancel ( txtEMP001 . Text ,false );
                if ( isOk == true )
                {
                    layoutControlItem37 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPic ( pictureEdit1 ,"反" );
                    tabPageOne . Refresh ( );
                    Query ( );
                    cancelltionTool ( "注销" );
                }
                else
                    XtraMessageBox . Show ( "反注销失败" );
            }

            return 0;
        }
        protected override int ExportBase ( )
        {
            ClassForMain . ViewExport . ExportToExcel ( this . Text ,gridControl1 );

            return base . ExportBase ( );
        }
        #endregion

        #region Method
        void numOfPerson ( )
        {
            _model . EMP001 = _bll . GetNum ( );
            if ( string . IsNullOrEmpty ( _model . EMP001 ) )
                _model . EMP001 = "00001";
            else
            {
                _model . EMP001 = ( Convert . ToInt32 ( _model . EMP001 ) + 1 ) . ToString ( );
                _model . EMP001 = _model . EMP001 . PadLeft ( 5 ,'0' );
            }
            //texNumPerson . Text = _model . EMP001;          
        }
        void serial ( LineProductMesEntityu . EmployeeEntity _model )
        {
            statePrevious = _model . EMP001;
        }
        void assignMent ( DataRow row ,LineProductMesEntityu . EmployeeEntity _model)
        {
            if ( _model . idx < 0 )
                return;

            _model . EMP034 = ( bool ) row [ "EMP034" ];
            if ( Convert . ToBoolean ( _model . EMP034 ) )
            {
                layoutControlItem37 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPic ( pictureEdit1 ,"注销" );
                toolCancellation . Caption = "反注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
            else
            {
                layoutControlItem37 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                Graph . grPic ( pictureEdit1 ,"反" );
                toolCancellation . Caption = "注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
            txtEMP001 . Tag = row [ "idx" ] . ToString ( );
            txtEMP001 . Text = row [ "EMP001" ] . ToString ( );
            txtEMP002 . Text = row [ "EMP002" ] . ToString ( );
            txtEMP004 . EditValue = row [ "EMP003" ];
            txtEMP006 . EditValue = row [ "EMP005" ];
            txtEMP007 . Text = row [ "EMP007" ] . ToString ( );
            txtEMP008 . Text = row [ "EMP008" ] . ToString ( );
            txtEMP009 . Text = string . IsNullOrEmpty ( row [ "EMP009" ] . ToString ( ) ) == true ? string . Empty : Convert . ToDateTime ( row [ "EMP009" ] . ToString ( ) ) . ToString ( "yyyy-MM-dd" );
            txtEMP010 . Text = row [ "EMP010" ] . ToString ( );
            txtEMP011 . Text = row [ "EMP011" ] . ToString ( );
            txtEMP012 . Text = row [ "EMP012" ] . ToString ( );
            txtEMP013 . Text = row [ "EMP013" ] . ToString ( );
            txtEMP014 . Text = row [ "EMP014" ] . ToString ( );
            txtEMP015 . Text = row [ "EMP015" ] . ToString ( );
            txtEMP016 . Text = row [ "EMP016" ] . ToString ( );
            txtEMP017 . Text = row [ "EMP017" ] . ToString ( );
            txtEMP018 . Text = row [ "EMP018" ] . ToString ( );
            txtEMP019 . Text = row [ "EMP019" ] . ToString ( );
            txtEMP020 . Text = row [ "EMP020" ] . ToString ( );
            txtEMP021 . Text = row [ "EMP021" ] . ToString ( );
            txtEMP022 . Text = row [ "EMP022" ] . ToString ( );
            txtEMP023 . Text = string . IsNullOrEmpty ( row [ "EMP023" ] . ToString ( ) ) == true ? string . Empty : Convert . ToDateTime ( row [ "EMP023" ] . ToString ( ) ) . ToString ( "yyyy-MM-dd" );
            txtEMP024 . Text = row [ "EMP024" ] . ToString ( );
            txtEMP025 . Text = row [ "EMP025" ] . ToString ( );
            txtEMP026 . Text = row [ "EMP026" ] . ToString ( );
            txtEMP027 . Text = row [ "EMP027" ] . ToString ( );
            txtEMP028 . Text = row [ "EMP028" ] . ToString ( );
            txtEMP029 . Text = row [ "EMP029" ] . ToString ( );
            txtEMP030 . Text = row [ "EMP030" ] . ToString ( );
            txtEMP031 . Text = row [ "EMP031" ] . ToString ( );
            txtEMP032 . Text = row [ "EMP032" ] . ToString ( );
            txtEMP035 . Text = row [ "EMP035" ] . ToString ( );
            txtEMP036 . Text = row [ "EMP036" ] . ToString ( );
            txtEMP037 . Checked = string . IsNullOrEmpty ( row [ "EMP037" ] . ToString ( ) ) == true ? true : Convert . ToBoolean ( row [ "EMP037" ] );
            string sx = row [ "EMP038" ] . ToString ( );
            txtEMP038 . Text = row [ "EMP038" ] . ToString ( );

            tabPageOne . Refresh ( );
        }
        void printOrExport ( )
        {
            //print = _bll . printOne ( intList );
            //print . TableName = "TableOne";
        }
        void UnEnable ( )
        {
            txtEMP002 . Enabled = txtEMP004 . Enabled = txtEMP006 . Enabled = txtEMP007 . Enabled = txtEMP008 . Enabled = txtEMP009 . Enabled = txtEMP010 . Enabled = txtEMP011 . Enabled = txtEMP012 . Enabled = txtEMP013 . Enabled = txtEMP014 . Enabled = txtEMP015 . Enabled = txtEMP016 . Enabled = txtEMP017 . Enabled = txtEMP018 . Enabled = txtEMP019 . Enabled = txtEMP020 . Enabled = txtEMP021 . Enabled = txtEMP022 . Enabled = txtEMP023 . Enabled = txtEMP024 . Enabled = txtEMP025 . Enabled = txtEMP026 . Enabled = txtEMP027 . Enabled = txtEMP028 . Enabled = txtEMP029 . Enabled = txtEMP030 . Enabled = txtEMP031 . Enabled = txtEMP032 . Enabled = txtEMP035 . Enabled = txtEMP036 . Enabled = txtEMP037 . Enabled = txtEMP038 . Enabled = false;
        }
        void Enable ( )
        {
            txtEMP002 . Enabled = txtEMP004 . Enabled = txtEMP006 . Enabled = txtEMP007 . Enabled = txtEMP008 . Enabled = txtEMP009 . Enabled = txtEMP010 . Enabled = txtEMP011 . Enabled = txtEMP012 . Enabled = txtEMP013 . Enabled = txtEMP014 . Enabled = txtEMP015 . Enabled = txtEMP016 . Enabled = txtEMP017 . Enabled = txtEMP018 . Enabled = txtEMP019 . Enabled = txtEMP020 . Enabled = txtEMP021 . Enabled = txtEMP022 . Enabled = txtEMP023 . Enabled = txtEMP024 . Enabled = txtEMP025 . Enabled = txtEMP026 . Enabled = txtEMP027 . Enabled = txtEMP028 . Enabled = txtEMP029 . Enabled = txtEMP030 . Enabled = txtEMP031 . Enabled = txtEMP032 . Enabled = txtEMP035 . Enabled = txtEMP036 . Enabled = txtEMP037 . Enabled = txtEMP038 . Enabled = true;
        }
        void clear ( )
        {
            txtEMP002 . Text = txtEMP004 . Text = txtEMP006 . Text = txtEMP007 . Text = txtEMP008 . Text = txtEMP009 . Text = txtEMP010 . Text = txtEMP011 . Text = txtEMP012 . Text = txtEMP013 . Text = txtEMP014 . Text = txtEMP015 . Text = txtEMP016 . Text = txtEMP017 . Text = txtEMP018 . Text = txtEMP019 . Text = txtEMP020 . Text = txtEMP021 . Text = txtEMP022 . Text = txtEMP023 . Text = txtEMP024 . Text = txtEMP025 . Text = txtEMP026 . Text = txtEMP027 . Text = txtEMP028 . Text = txtEMP029 . Text = txtEMP030 . Text = txtEMP031 . Text = txtEMP032 . Text = txtEMP035 . Text = txtEMP036 . Text = txtEMP038 . Text = string . Empty;
            layoutControlItem37 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }
        bool getValue ( )
        {
            errorProvider1 . Clear ( );
            if ( string . IsNullOrEmpty ( txtEMP001 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP001 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP002 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP002 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP004 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP004 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP006 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP006 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP007 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP007 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP008 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP008 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP009 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP009 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP016 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP016 ,"不可为空" );
                return false;
            }
            if ( !string . IsNullOrEmpty ( txtEMP016 . Text ) && RegexHelper . checkID ( txtEMP016 . Text ) == false )
            {
                errorProvider1 . SetError ( txtEMP016 ,"身份证格式错误" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP025 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP025 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtEMP023 . Text ) )
            {
                errorProvider1 . SetError ( txtEMP023 ,"不可为空" );
                return false;
            }
            //if ( string . IsNullOrEmpty ( txtEMP010 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP010 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP011 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP011 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP012 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP012 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP013 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP013 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP015 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP015 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP017 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP017 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP018 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP018 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP019 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP019 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP020 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP020 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP021 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP021 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP022 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP022 ,"不可为空" );
            //    return false;
            //}

            //if ( string . IsNullOrEmpty ( txtEMP024 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP024 ,"不可为空" );
            //    return false;
            //}

            //if ( string . IsNullOrEmpty ( txtEMP035 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP035 ,"不可为空" );
            //    return false;
            //}
            //if ( string . IsNullOrEmpty ( txtEMP036 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP036 ,"不可为空" );
            //    return false;
            //}

            //if ( string . IsNullOrEmpty ( txtEMP025 . Text ) )
            //{
            //    errorProvider1 . SetError ( txtEMP025 ,"在职状态不可为空" );
            //    return false;
            //}
            decimal outRsult = 0M;
            if ( !string . IsNullOrEmpty ( txtEMP027 . Text ) && decimal . TryParse ( txtEMP027 . Text ,out outRsult ) == false )
            {
                errorProvider1 . SetError ( txtEMP027 ,"请输入数字" );
                return false;
            }
            _model . EMP027 = outRsult;
            outRsult = 0M;
            if ( !string . IsNullOrEmpty ( txtEMP028 . Text ) && decimal . TryParse ( txtEMP028 . Text ,out outRsult ) == false )
            {
                errorProvider1 . SetError ( txtEMP028 ,"请输入数字" );
                return false;
            }
            _model . EMP028 = outRsult;
            outRsult = 0M;
            if ( !string . IsNullOrEmpty ( txtEMP029 . Text ) && decimal . TryParse ( txtEMP029 . Text ,out outRsult ) == false )
            {
                errorProvider1 . SetError ( txtEMP029 ,"请输入数字" );
                return false;
            }
            _model . EMP029 = outRsult;
            outRsult = 0M;
            if ( !string . IsNullOrEmpty ( txtEMP030 . Text ) && decimal . TryParse ( txtEMP030 . Text ,out outRsult ) == false )
            {
                errorProvider1 . SetError ( txtEMP030 ,"请输入数字" );
                return false;
            }
            _model . EMP030 = outRsult;
            outRsult = 0M;
            if ( !string . IsNullOrEmpty ( txtEMP031 . Text ) && decimal . TryParse ( txtEMP031 . Text ,out outRsult ) == false )
            {
                errorProvider1 . SetError ( txtEMP031 ,"请输入数字" );
                return false;
            }
            _model . EMP031 = outRsult;
            outRsult = 0M;
            if ( !string . IsNullOrEmpty ( txtEMP032 . Text ) && decimal . TryParse ( txtEMP032 . Text ,out outRsult ) == false )
            {
                errorProvider1 . SetError ( txtEMP032 ,"请输入数字" );
                return false;
            }
            _model . EMP032 = outRsult;
            _model . EMP001 = txtEMP001 . Text;
            _model . EMP002 = txtEMP002 . Text;
            _model . EMP003 = txtEMP004 . EditValue . ToString ( );
            _model . EMP004 = txtEMP004 . Text;
            _model . EMP005 = txtEMP006 . EditValue . ToString ( );
            _model . EMP006 = txtEMP006 . Text;
            _model . EMP007 = txtEMP007 . Text;
            _model . EMP008 = txtEMP008 . Text == "男" ? false : true;
            if ( string . IsNullOrEmpty ( txtEMP009 . Text ) )
                _model . EMP009 = null;
            else
                _model . EMP009 = Convert . ToDateTime ( txtEMP009 . Text );
            _model . EMP010 = txtEMP010 . Text;
            _model . EMP011 = txtEMP011 . Text;
            _model . EMP012 = txtEMP012 . Text;
            _model . EMP013 = txtEMP013 . Text;
            _model . EMP014 = txtEMP014 . Text;
            _model . EMP015 = txtEMP015 . Text;
            _model . EMP016 = txtEMP016 . Text;
            _model . EMP017 = txtEMP017 . Text;
            _model . EMP018 = txtEMP018 . Text;
            _model . EMP019 = txtEMP019 . Text;
            _model . EMP020 = txtEMP020 . Text;
            _model . EMP021 = txtEMP021 . Text;
            _model . EMP022 = txtEMP022 . Text;
            if ( string . IsNullOrEmpty ( txtEMP023 . Text ) )
                _model . EMP023 = null;
            else
                _model . EMP023 = Convert . ToDateTime ( txtEMP023 . Text );
            _model . EMP024 = txtEMP024 . Text;
            _model . EMP025 = txtEMP025 . Text == "在职" ? true : false;
            _model . EMP026 = txtEMP026 . Text;
            _model . EMP034 = false;
            _model . EMP035 = txtEMP035 . Text;
            _model . EMP036 = txtEMP036 . Text;
            _model . EMP037 = txtEMP037 . Checked;
            _model . EMP038 = txtEMP038 . Text;

            errorProvider1 . Clear ( );

            return true;
        }
        void getValueForPUATask ( )
        {
            Func<List<LineProductMesEntityu . PUAEntity>> funStr = getValueForPUA;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( setValueForPUA ) ,null );
        }
        List<LineProductMesEntityu . PUAEntity> getValueForPUA ( )
        {
            model = JsonUtils . JsonStringToVar<LineProductMesEntityu . PUAEntity> ( JsonUtils . ReadJson ( ) );
            return model;
        }
        delegate void AsynUpdateUIBase ( );
        void setValueForPUA ( IAsyncResult result )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUIBase ( delegate ( )
                {
                    if ( model == null )
                        return;
                    txtEMP010 . Properties . Items . Clear ( );
                    txtEMP012 . Properties . Items . Clear ( );
                    foreach ( LineProductMesEntityu . PUAEntity entity in model )
                    {
                        txtEMP010 . Properties . Items . Add ( entity . name );
                        txtEMP012 . Properties . Items . Add ( entity . name );
                    }
                } ) );
            }
            
        }
        void getDepart ( )
        {
            txtEMP004 . Properties . DataSource = _bll . getDepart ( );
            txtEMP004 . Properties . DisplayMember = "DAA002";
            txtEMP004 . Properties . ValueMember = "DAA001";

            txtEMP038 . Properties . DataSource = _bll . getDepartPower ( );
            txtEMP038 . Properties . DisplayMember = "DAA002";
            txtEMP038 . Properties . ValueMember = "DAA002";
        }
        #endregion
        
        #region Event
        private void backgroundWorker1_DoWork ( object sender ,System . ComponentModel . DoWorkEventArgs e )
        {
            tableQuery = _bll . GetDataTableAll ( );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,System . ComponentModel . RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                toolQuery . Enabled = true;
                toolAdd . Enabled = true;

                gridControl1 . DataSource = tableQuery;
                if ( gridView1 . DataRowCount > 0 )
                {
                    btnFirst . Enabled = true;
                    btnLast . Enabled = true;
                    btnNext . Enabled = true;
                    btnPrevious . Enabled = true;

                    DataRow row;
                    if ( statePrevious != "" )
                        row = tableQuery . Select ( "EMP001='" + statePrevious + "'" ) [ 0 ];
                    else
                        row = gridView1 . GetDataRow ( 0 );
                    
                    _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    assignMent ( row ,_model );

                    QueryTool ( );
                    toolPrint . Visibility = toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                }             
            }
        }
        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
            assignMent ( row ,_model );
            tabPageOne . Show ( );
            xtraTabControl1 . SelectedTabPageIndex = 0;
        }
        private void btnFirst_Click ( object sender ,EventArgs e )
        {
            count = 0;
            if ( count >= 0 && count <= gridView1 . DataRowCount - 1 )
            {
                DataRow row = gridView1 . GetDataRow ( count );
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                assignMent ( row ,_model );
            }
        }
        private void btnPrevious_Click ( object sender ,EventArgs e )
        {
            if ( count == 0 )
                return;
            if ( count > 0 )
                count = count - 1;
            if ( count >= 0 && count <= gridView1 . DataRowCount - 1 )
            {
                DataRow row = gridView1 . GetDataRow ( count );
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                assignMent ( row ,_model );
            }
        }
        private void btnNext_Click ( object sender ,EventArgs e )
        {
            if ( count < gridView1 . DataRowCount - 1 )
                count = count + 1;
            if ( count >= 0 && count <= gridView1 . DataRowCount - 1 )
            {
                DataRow row = gridView1 . GetDataRow ( count );
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                assignMent ( row ,_model );
            }
        }
        private void btnLast_Click ( object sender ,EventArgs e )
        {
            count = gridView1 . DataRowCount - 1;
            if ( count >= 0 && count <= gridView1 . DataRowCount - 1 )
            {
                DataRow row = gridView1 . GetDataRow ( count );
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                assignMent ( row ,_model );
            }
        }
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            count = gridView1 . FocusedRowHandle;
            if ( count >= 0 && count <= gridView1 . DataRowCount - 1 )
            {
                DataRow row = gridView1 . GetDataRow ( count );
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                //if ( row [ "check" ] . ToString ( ) == "True" )
                //{
                //    row [ "check" ] = false;
                //    if ( intList . Contains ( _model . idx ) )
                //        intList . Remove ( _model . idx );
                //}
                //else
                //{
                //    row [ "check" ] = true;
                //    intList . Add ( _model . idx );
                //}
            }
        }
        private void txtEMP010_SelectedValueChanged ( object sender ,EventArgs e )
        {
            txtEMP011 . Properties . Items . Clear ( );
            txtEMP011 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtEMP010 . Text ) )
                return;
            if ( model == null )
                return;
            List<LineProductMesEntityu . PUAEntity> modelCity = model . FindAll ( t =>
                {
                    return t . name == txtEMP010 . Text;
                } );
            if ( modelCity == null )
                return;
            foreach ( LineProductMesEntityu . City entity in modelCity[0].city )
            {
                txtEMP011 . Properties . Items . Add ( entity . name );
            }
        }
        private void txtEMP011_SelectedValueChanged ( object sender ,EventArgs e )
        {
            txtEMP035 . Properties . Items . Clear ( );
            txtEMP035 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtEMP011 . Text ) )
                return;
            if ( model == null )
                return;
            List<LineProductMesEntityu . PUAEntity> modelCity = model . FindAll ( t =>
            {
                return t . name == txtEMP010 . Text;
            } );
            if ( modelCity == null )
                return;
            List<LineProductMesEntityu . City> entityCity = modelCity [ 0 ] . city;
            if ( entityCity == null )
                return;
            List<LineProductMesEntityu . City> citys = entityCity . FindAll ( t =>
                  {
                      return t . name == txtEMP011 . Text;
                  } );
            if ( citys == null )
                return;
            foreach ( string item in citys [ 0 ] . area )
            {
                txtEMP035 . Properties . Items . Add ( item );
            }
        }
        private void txtEMP012_SelectedValueChanged ( object sender ,EventArgs e )
        {
            txtEMP013 . Properties . Items . Clear ( );
            txtEMP013 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtEMP012 . Text ) )
                return;
            if ( model == null )
                return;
            List<LineProductMesEntityu . PUAEntity> modelCity = model . FindAll ( t =>
            {
                return t . name == txtEMP012 . Text;
            } );
            if ( modelCity == null )
                return;
            foreach ( LineProductMesEntityu . City entity in modelCity [ 0 ] . city )
            {
                txtEMP013 . Properties . Items . Add ( entity . name );
            }
        }
        private void txtEMP013_SelectedValueChanged ( object sender ,EventArgs e )
        {
            txtEMP036 . Properties . Items . Clear ( );
            txtEMP036 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtEMP013 . Text ) )
                return;
            if ( model == null )
                return;
            List<LineProductMesEntityu . PUAEntity> modelCity = model . FindAll ( t =>
            {
                return t . name == txtEMP012 . Text;
            } );
            if ( modelCity == null )
                return;
            List<LineProductMesEntityu . City> entityCity = modelCity [ 0 ] . city;
            if ( entityCity == null )
                return;
            List<LineProductMesEntityu . City> citys = entityCity . FindAll ( t =>
            {
                return t . name == txtEMP013 . Text;
            } );
            if ( citys == null )
                return;
            foreach ( string item in citys [ 0 ] . area )
            {
                txtEMP036 . Properties . Items . Add ( item );
            }
        }
        private void txtEMP004_EditValueChanged ( object sender ,EventArgs e )
        {
            txtEMP006 . Properties . DataSource = null;
            txtEMP006 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtEMP004 . EditValue . ToString ( ) ) )
                return;
            txtEMP006 . Properties . DataSource = _bll . getDepart ( txtEMP004 . EditValue . ToString ( ) );
            txtEMP006 . Properties . DisplayMember = "DAA002";
            txtEMP006 . Properties . ValueMember = "DAA001";
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtEMP002 . Text == string . Empty )
                    return;
                if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    Save ( );
                    if (  ClassForMain.FormClosingState.formClost == false )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        #endregion

    }
}
