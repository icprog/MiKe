using System;
using System . Collections . Generic;
using System . Data;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using System . Threading;
using DevExpress . XtraEditors;
using System . Linq;
using System . Windows . Forms;
using System . ComponentModel;
using LineProductMes . ChildForm;
using LineProductMesBll;

namespace LineProductMes
{
    public partial class FormLEGNews :FormChild
    {
        LineProductMesEntityu.LEDNewsHeaderEntity _header=null;
        LineProductMesEntityu.LEDNewsBodyEntity _body=null;
        LineProductMesEntityu.LEHNewsBodyEntity _bodyOne=null;
        LineProductMesBll.Bll.LEDNewsBll _bll=null;

        Thread thread; SynchronizationContext m_SyncContext = null;
    
        DataTable tableView,tableViewTwo,tableUser,tableWorker,tableLocal,tableProduct,tablePrintOne,tablePrintTwo,tableOtherSur,tablePrintTre,tablePrintFor,tablePrintFiv;

        string state=string.Empty,strWhere="1=1",focuseName=string.Empty;
        bool result=false;
        decimal outResult = 0M;
        DataRow row;
        List<string> idxList=new List<string>();
        List<string> idxListOne=new List<string>();

        DateTime dt,dtStart,dtEnd;

        public FormLEGNews ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll .LEDNewsBll ( );
            _header = new LineProductMesEntityu .LEDNewsHeaderEntity ( );
            _body = new LineProductMesEntityu .LEDNewsBodyEntity ( );
            _bodyOne = new LineProductMesEntityu . LEHNewsBodyEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { GridView1 ,View1 ,View2 ,gridView2 ,View4 ,View5 ,View6 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { GridView1 ,View1 ,View2 ,gridView2 ,View4 ,View5 ,View6 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            dt1 . VistaEditTime = dt2 . VistaEditTime = dt3 . VistaEditTime = dt4 . VistaEditTime = txtLEF023 . Properties . VistaEditTime = txtLEF024 . Properties . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            controlUnEnable ( );
            controlClear ( );

            wait . Hide ( );

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext . Current;
            thread = new Thread ( new ThreadStart (  ThreadPost ) );
            thread . Start ( );

            queryTime ( );
        }

        #region Main
        protected override int Query ( )
        {
            FormLEDNewsQuery from = new FormLEDNewsQuery ( );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                _header . LEF001 = from . getOdd;

                _header = _bll . getModel ( _header . LEF001 );

                setValue ( );

                strWhere = "1=1";
                strWhere += " AND LEG001='" + _header . LEF001 + "'";
                tableView = _bll . getTableView ( strWhere );
                gridControl1 . DataSource = tableView;
                strWhere = "1=1";
                strWhere += " AND LEH001='" + _header . LEF001 + "'";
                tableViewTwo = _bll . getTableViewOne ( strWhere );
                gridControl2 . DataSource = tableViewTwo;
                calcuSalaryByPrice ( );
                calcuSalaryTimeSum ( );
                calcuSalaryTieSum ( );
                calcuSalarySum ( );
                calcuTimeSum ( );
                //calcuSalaryUser ( );

                editOtherSur ( string . Empty ,string . Empty );

                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );

            state = "add";
            txtLEF001 . Text = _bll . getOddNum ( );
            tableView = _bll . getTableView ( "1=2" );
            gridControl1 . DataSource = tableView;
            tableViewTwo = _bll . getTableViewOne ( "1=2" );
            gridControl2 . DataSource = tableViewTwo;

            txtLEF010 . EditValue = "0506";
            txtLEF021 . Text = "计件";
            txtLEF013 . Text = dt. ToString ( "yyyy-MM-dd" );
            addTool ( );

            queryTime ( );

            txtLEF023 . Text = dtStart . ToString ( );
            txtLEF024 . Text = dtEnd . ToString ( );
            txtLEF022 . Text = LineProductMesBll . UserInfoMation . userName;

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            editTool ( );
            state = "edit";

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtLEF001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }

            result = _bll . Delete ( txtLEF001 . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                controlClear ( );
                deleteTool ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getValue ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            wait . Show ( );
            backgroundWorker1 . RunWorkerAsync ( );

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            controlUnEnable ( );
            if ( state . Equals ( "add" ) )
                controlClear ( );
            cancelTool ( state );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( string . IsNullOrEmpty ( txtLEF001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }

            _header . LEF001 = txtLEF001 . Text;
            _header . LEF011 = txtLEF012 . EditValue . ToString ( );

            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . LEF017 = true;
            else
                _header . LEF017 = false;

            if ( _header . LEF017 == false )
            {
                if ( GenerateSGMRCACB . Exists ( _header . LEF001 ) )
                {
                    XtraMessageBox . Show ( "已入库,不允许反审核" );
                    return 0;
                }
            }

            result = _bll . Exanmie ( _header );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPic ( pictureEdit1 ,"审核" );
                }
                else
                {
                    layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPic ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtLEF001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . LEF018 = true;
            else
                _header . LEF018 = false;
            result = _bll . CancelTion ( txtLEF001 . Text ,_header . LEF018 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPic ( pictureEdit1 ,"注销" );
                }
                else
                {
                    layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPic ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        protected override int PrintWork ( )
        {
            printOrExport ( );
            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . PrintWork ( );
        }
        protected override int ExportWork ( )
        {
            printOrExport ( );
            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . ExportWork ( );
        }
        protected override int PrintReport ( )
        {
            printOrExportOne ( );
            Print ( new DataTable [ ] { tablePrintTre ,tablePrintFor ,tablePrintFiv } ,"LED报工单.frx" );

            return base . PrintReport ( );
        }
        protected override int ExportReport ( )
        {
            printOrExportOne ( );
            Export ( new DataTable [ ] { tablePrintTre ,tablePrintFor ,tablePrintFiv } ,"LED报工单.frx" );

            return base . ExportReport ( );
        }
        #endregion

        #region Event
        private void GridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void gridView2_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void txtLEF010_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtLEF010 . EditValue == null || txtLEF010 . EditValue . ToString ( ) == string . Empty )
                return;
            txtLEF012 . Properties . DataSource = _bll . getDepart ( txtLEF010 . EditValue . ToString ( ) );
            txtLEF012 . Properties . DisplayMember = "DAA002";
            txtLEF012 . Properties . ValueMember = "DAA001";

            txtLEF012 . EditValue = "0506";
        }
        private void txtLEF012_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtLEF010 . EditValue == null || txtLEF010 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( txtLEF012 . EditValue == null || txtLEF012 . EditValue . ToString ( ) == string . Empty )
                return;

            if ( tableView == null )
                return;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            DataRow [ ] rows = tableUser . Select ( "EMP005='" + txtLEF012 . EditValue + "'" );
            if ( rows == null )
                return;
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                foreach ( DataRow r in rows )
                {
                    row = tableView . NewRow ( );
                    row [ "LEG002" ] = r [ "EMP001" ];
                    row [ "LEG003" ] = r [ "EMP002" ];
                    row [ "LEG004" ] = r [ "EMP007" ];
                    row [ "LEG013" ] = r [ "DAA002" ];
                    row [ "LEG005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    row [ "LEG006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    row [ "LEG012" ] = "在职";
                    tableView . Rows . Add ( row );
                }
            }
            else
            {
                foreach ( DataRow r in rows )
                {
                    if ( tableView . Select ( "LEG002='" + r [ "EMP001" ] + "'" ) . Length < 1 )
                    {
                        row = tableView . NewRow ( );
                        row [ "LEG002" ] = r [ "EMP001" ];
                        row [ "LEG003" ] = r [ "EMP002" ];
                        row [ "LEG004" ] = r [ "EMP007" ];
                        row [ "LEG013" ] = r [ "DAA002" ];
                        row [ "LEG005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                        row [ "LEG006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        row [ "LEG012" ] = "在职";
                        tableView . Rows . Add ( row );
                    }
                }
            }
            calcuTimeSum ( );
        }
        private void backgroundWorker1_DoWork ( object sender ,System . ComponentModel . DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableView ,tableViewTwo );
            else
                result = _bll . Edit ( _header ,tableView ,tableViewTwo ,idxList ,idxListOne );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,System . ComponentModel . RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功保存" );
                     ClassForMain.FormClosingState.formClost = true;
                    saveTool ( );
                    controlUnEnable ( );
                    if ( state . Equals ( "add" ) )
                        _header . LEF001 = txtLEF001 . Text = LineProductMesBll . UserInfoMation . oddNum;

                    setValue ( );
                    strWhere = "1=1";
                    strWhere += " AND LEG001='" + _header . LEF001 + "'";
                    tableView = _bll . getTableView ( strWhere );
                    gridControl1 . DataSource = tableView;

                    strWhere = "1=1";
                    strWhere += " AND LEH001='" + _header . LEF001 + "'";
                    tableViewTwo = _bll . getTableViewOne ( strWhere );
                    gridControl2 . DataSource = tableViewTwo;

                    calcuSalaryByPrice ( );
                    calcuSalaryTimeSum ( );
                    calcuSalaryTieSum ( );
                    calcuSalarySum ( );
                    calcuTimeSum ( );

                    editOtherSur ( string . Empty ,string . Empty );
                }
                else
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败" );
                }
            }
        }
        private void GridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = GridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( GridView1 . FocusedColumn . FieldName == "LEG012" )
            {
                if ( e . Value . Equals ( "离职" ) || e . Value . Equals ( "未上班" ) )
                {
                    row [ "LEG005" ] = DBNull . Value;
                    row [ "LEG006" ] = DBNull . Value;
                    row [ "LEG007" ] = DBNull . Value;
                    row [ "LEG008" ] = DBNull . Value;
                    row [ "LEG009" ] = DBNull . Value;
                    row [ "LEG010" ] = DBNull . Value;
                    row [ "LEG014" ] = DBNull . Value;
                    row [ "LEG015" ] = DBNull . Value;
                }
                else if ( e . Value . Equals ( "在职" ) || e . Value . Equals ( "请假" ) )
                {
                    if ( row [ "LEG005" ] == null || row [ "LEG005" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LEG005" ] = dtStart;
                    }
                    if ( row [ "LEG006" ] == null || row [ "LEG006" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LEG006" ] = dtEnd;
                    }
                }
                calcuTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG017" )
            {
                calcuTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG005" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"LEG006" ,"LEG008" ,"LEG009" ) == false )
                {
                    row [ "LEG005" ] = DBNull . Value;
                }
                addRow ( "LEG005" ,e . RowHandle ,e . Value );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG006" )
            {
                if ( workShopTime . endTime ( row ,e . Value ,"LEG005" ,"LEG008" ,"LEG009" ) == false )
                {
                    row [ "LEG006" ] = DBNull . Value;
                }
                addRow ( "LEG006" ,e . RowHandle ,e . Value );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG008" )
            {
                if ( workShopTime . startCenTime ( row ,e . Value ,"LEG006" ,"LEG009" ,"LEG005" ) == false )
                {
                    row [ "LEG008" ] = DBNull . Value;
                }
                addRow ( "LEG008" ,e . RowHandle ,e . Value );
                calcuSalaryTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG009" )
            {
                if ( workShopTime . endCenTime ( row ,e . Value ,"LEG008" ,"LEG005" ,"LEG006" ) == false )
                {
                    row [ "LEG009" ] = DBNull . Value;
                }
                addRow ( "LEG009" ,e . RowHandle ,e . Value );
                calcuSalaryTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG010" )
            {
                //leg015

                int selectIndex = GridView1 . FocusedRowHandle;
                string leg010Result = GridView1 . GetDataRow ( selectIndex ) [ "LEG010" ] . ToString ( );
                if ( string . IsNullOrEmpty ( leg010Result ) )
                    _body . LEG010 = 0;
                else
                    _body . LEG010 = Convert . ToDecimal ( leg010Result );

                for ( int i = selectIndex ; i < tableView . Rows . Count ; i++ )
                {
                    row = tableView . Rows [ i ];
                    if ( row [ "LEG015" ] != null && row [ "LEG015" ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ "LEG010" ] == null || row [ "LEG010" ] . ToString ( ) == string . Empty )
                        {
                            row . BeginEdit ( );
                            row [ "LEG010" ] = _body . LEG010;
                            row . EndEdit ( );
                        }
                    }
                    if ( i == selectIndex && ( row [ "LEG015" ] == null || row [ "LEG015" ] . ToString ( ) == string . Empty ) )
                    {
                        row . BeginEdit ( );
                        row [ "LEG010" ] = DBNull . Value;
                        row . EndEdit ( );
                    }
                }
                gridControl1 . Refresh ( );

                calcuSalaryTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG007" )
            {
                calcuSalaryTieSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LEG002" || GridView1 . FocusedColumn . FieldName == "LEG003" || GridView1 . FocusedColumn . FieldName == "LEG004" || GridView1 . FocusedColumn . FieldName == "LEG013" )
            {
                if ( row [ "LEG005" ] == null || row [ "LEG005" ] . ToString ( ) == string . Empty )
                {
                    row [ "LEG005" ] = dtStart;
                }
                if ( row [ "LEG006" ] == null || row [ "LEG006" ] . ToString ( ) == string . Empty )
                {
                    row [ "LEG006" ] = dtEnd;
                }
                if ( row [ "LEG012" ] == null || row [ "LEG012" ] . ToString ( ) == string . Empty )
                {
                    row [ "LEG012" ] = "在职";
                }
                calcuTimeSum ( );
            }
        }
        private void txtu2_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu3_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu4_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu5_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
        }
        private void txtu1_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            row = GridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . YesNo ) == DialogResult . Yes )
                {
                    _body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _body . idx > 0 && !idxList . Contains ( _body . idx . ToString ( ) ) )
                        idxList . Add ( _body . idx . ToString ( ) );
                    tableView . Rows . Remove ( row );
                    gridControl1 . RefreshDataSource ( );
                }
            }
        }
        private void gridControl2_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            row = gridView2 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . YesNo ) == DialogResult . Yes )
                {
                    _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _bodyOne . idx > 0 && !idxListOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                        idxListOne . Add ( _bodyOne . idx . ToString ( ) );
                    tableViewTwo . Rows . Remove ( row );
                    gridControl2 . RefreshDataSource ( );
                }
            }
        }
        private void EditUser_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = GridView1 . ActiveEditor;
            switch ( GridView1 . FocusedColumn . FieldName )
            {
                case "LEG002":
                if ( edit . EditValue == null )
                    return;
                row = tableUser . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _body . LEG003 = row [ "EMP002" ] . ToString ( );
                _body . LEG004 = row [ "EMP007" ] . ToString ( );
                _body . LEG013 = row [ "DAA002" ] . ToString ( );
                GridView1 . SetFocusedRowCellValue ( GridView1 . Columns [ "LEG003" ] ,_body . LEG003 );
                GridView1 . SetFocusedRowCellValue ( GridView1 . Columns [ "LEG004" ] ,_body . LEG004 );
                GridView1 . SetFocusedRowCellValue ( GridView1 . Columns [ "LEG013" ] ,_body . LEG013 );
                break;
            }
        }
        private void EditOrder_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView2 . ActiveEditor;
            switch ( gridView2 . FocusedColumn . FieldName )
            {
                case "LEH002":
                if ( edit . EditValue == null )
                    return;
                row = tableProduct . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyOne . LEH010 = row [ "RAA008" ] . ToString ( );
                if ( string . IsNullOrEmpty ( _bodyOne . LEH010 ) )
                    return;
                _bodyOne . LEH002 = edit . EditValue . ToString ( );
                _bodyOne . LEH003 = row [ "RAA015" ] . ToString ( );
                _bodyOne . LEH004 = row [ "DEA002" ] . ToString ( );
                _bodyOne . LEH005 = row [ "DEA057" ] . ToString ( );
                _bodyOne . LEH006 = row [ "DEA003" ] . ToString ( );
                _bodyOne . LEH007 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
                _bodyOne . LEH008 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEH003" ] ,_bodyOne . LEH003 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEH004" ] ,_bodyOne . LEH004 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEH005" ] ,_bodyOne . LEH005 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEH006" ] ,_bodyOne . LEH006 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEH007" ] ,_bodyOne . LEH007 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEH008" ] ,_bodyOne . LEH008 );

                editOtherSur ( _bodyOne . LEH002 ,_bodyOne . LEH003 );
                break;
            }
        }
        private void gridView2_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "LEH008" || e . Column . FieldName == "LEH009" )
            {
                calcuSalaryByPrice ( );
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtLEF010 . Text == string . Empty || tableView == null || tableView . Rows . Count < 1 )
                    return;
                if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . YesNo ) == DialogResult . Yes )
                {
                    Save ( );
                    if (  ClassForMain.FormClosingState.formClost == false )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        private void txtLEF019_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
        }
        private void txtLEF020_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
        }
        private void gridView2_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void gridView2_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void GridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView2 ,focuseName );
        }
        private void contextMenuStrip2_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( GridView1 ,focuseName );
        }
        private void GridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "LEG016" )
            {
                if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDecimal ( e . CellValue ) >= 200 )
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                }
            }
        }
        private void txtLEF021_SelectedValueChanged ( object sender ,EventArgs e )
        {
            updateBatchTime ( );
        }
        private void txtLEF023_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( !string . IsNullOrEmpty ( txtLEF023 . Text ) )
            {
                dtStart = Convert . ToDateTime ( txtLEF023 . Text );
                dt = dtStart;
            }
            updateBatchTime ( );
        }
        void updateBatchTime ( )
        {
            if ( string . IsNullOrEmpty ( txtLEF021 . Text ) )
                return;

            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;

            if ( !string . IsNullOrEmpty ( txtLEF023 . Text ) )
            {
                dtStart = Convert . ToDateTime ( txtLEF023 . Text );
                dt = dtStart;
            }
            if ( !string . IsNullOrEmpty ( txtLEF024 . Text ) )
                dtEnd = Convert . ToDateTime ( txtLEF024 . Text );

            foreach ( DataRow row in tableView . Rows )
            {
                if ( txtLEF021 . Text . Equals ( "计件" ) )
                {
                    row [ "LEG005" ] = dtStart;
                    row [ "LEG006" ] = dtEnd;
                    //row [ "LEG014" ] = null;
                    row [ "LEG008" ] = DBNull . Value;
                    row [ "LEG009" ] = DBNull . Value;
                    row [ "LEG015" ] = DBNull . Value;
                }
                else if ( txtLEF021 . Text . Equals ( "计时" ) )
                {
                    row [ "LEG005" ] = DBNull . Value;
                    row [ "LEG006" ] = DBNull . Value;
                    row [ "LEG014" ] = DBNull . Value;
                    row [ "LEG008" ] = dtStart;
                    row [ "LEG009" ] = dtEnd;
                    //row [ "LEG015" ] = null;
                }
            }

            calcuTimeSum ( );
            calcuSalaryByPrice ( );
            calcuSalaryTimeSum ( );
        }
        private void txtLEF025_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
            calcuTimeSum ( );
        }
        private void btnEdit_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = gridView2 . GetFocusedDataRow ( );
            FormLEDOrder form = new FormLEDOrder ( tableProduct );
            if ( form . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow ro = form . getRow;
                _bodyOne . LEH002 = ro [ "RAA001" ] . ToString ( );
                _bodyOne . LEH003 = ro [ "RAA015" ] . ToString ( );
                _bodyOne . LEH004 = ro [ "DEA002" ] . ToString ( );
                _bodyOne . LEH005 = ro [ "DEA057" ] . ToString ( );
                _bodyOne . LEH006 = ro [ "DEA003" ] . ToString ( );
                _bodyOne . LEH007 = string . IsNullOrEmpty ( ro [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( ro [ "RAA018" ] . ToString ( ) );
                _bodyOne . LEH008 = string . IsNullOrEmpty ( ro [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( ro [ "DEA050" ] . ToString ( ) );

                if ( row == null )
                {
                    row = tableViewTwo . NewRow ( );
                    rowEdit ( row );
                    tableViewTwo . Rows . Add ( row );
                }
                else
                    rowEdit ( row );

                editOtherSur ( _bodyOne . LEH002 ,_bodyOne . LEH003 );
            }
        }
        void rowEdit ( DataRow row )
        {
            row [ "LEH002" ] = _bodyOne . LEH002;
            row [ "LEH003" ] = _bodyOne . LEH003;
            row [ "LEH004" ] = _bodyOne . LEH004;
            row [ "LEH005" ] = _bodyOne . LEH005;
            row [ "LEH006" ] = _bodyOne . LEH006;
            row [ "LEH007" ] = _bodyOne . LEH007;
            row [ "LEH008" ] = _bodyOne . LEH008;
        }
        private void btnUser_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = GridView1 . GetFocusedDataRow ( );
            FormUserChoise form = new FormUserChoise ( tableUser );
            if ( form . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow ro = form . getRow;
                _body . LEG002 = ro [ "EMP001" ] . ToString ( );
                _body . LEG003 = ro [ "EMP002" ] . ToString ( );
                _body . LEG004 = ro [ "EMP007" ] . ToString ( );
                _body . LEG012 = "在职";
                _body . LEG013 = ro [ "DAA002" ] . ToString ( );

                if ( row == null )
                {
                    row = tableView . NewRow ( );
                    rowUser ( row );
                    tableView . Rows . Add ( row );
                }
                else
                    rowUser ( row );
            }
        }
        void rowUser ( DataRow row )
        {
            row [ "LEG002" ] = _body . LEG002;
            row [ "LEG003" ] = _body . LEG003;
            row [ "LEG004" ] = _body . LEG004;
            row [ "LEG012" ] = _body . LEG012;
            row [ "LEG013" ] = _body . LEG013;
        }
        private void txtLEF026_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtLEF027_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtLEF010 . ReadOnly = txtLEF012 . ReadOnly = txtLEF015 . ReadOnly =  txtLEF019 . ReadOnly = txtLEF020 . ReadOnly =txtLEF021.ReadOnly= txtLEF023 . ReadOnly = txtLEF024 . ReadOnly =txtLEF025.ReadOnly=txtLEF026.ReadOnly=txtLEF027.ReadOnly= true;
            GridView1 . OptionsBehavior . Editable = false;
            gridView2 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtLEF010 . ReadOnly = txtLEF012 . ReadOnly =  txtLEF015 . ReadOnly = txtLEF019 . ReadOnly = txtLEF020 . ReadOnly = txtLEF021 . ReadOnly = txtLEF023 . ReadOnly = txtLEF024 . ReadOnly = txtLEF025 . ReadOnly = txtLEF026 . ReadOnly = txtLEF027 . ReadOnly = false;
            GridView1 . OptionsBehavior . Editable = true;
            gridView2 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            gridControl1 . DataSource = null;
            gridControl2 . DataSource = null;
            txtLEF001 . Text = txtLEF010 . Text = txtLEF012 . Text = txtLEF013 . Text = txtLEF015 . Text =   txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtu5 . Text =txtLEF019.Text=txtLEF020.Text=txtLEF021.Text = txtLEF023 . Text = txtLEF024 . Text = txtLEF025 . Text = txtLEF026 . Text = txtLEF027 . Text = txtLEF022.Text= string . Empty;
            txtLEF001 . Text = txtLEF010 . Text = txtLEF012 . Text = txtLEF013 . Text = txtLEF015 . Text =  txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtu5 . Text = txtLEF019 . Text = txtLEF020 . Text = txtLEF021 . Text = txtLEF023 . Text = txtLEF024 . Text = txtLEF025 . Text = txtLEF026 . Text = txtLEF027 . Text = txtLEF022.Text= string . Empty;
            layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }
        void ThreadPost ( )
        {
            tableProduct = _bll . getTablePInfo ( );
            m_SyncContext . Post ( InitData ,tableProduct );
            tableWorker = _bll . getDepart ( "05" );
            m_SyncContext . Post ( InitData ,tableWorker );
            tableUser = _bll . getUserInfo ( );
            m_SyncContext . Post ( InitData ,tableUser );
            tableLocal = _bll . getUserLocal ( );
            m_SyncContext . Post ( InitData ,tableLocal );
        }
        void InitData ( object table )
        {
            EditOrder  . DataSource = tableProduct;
            EditOrder  . DisplayMember = "RAA001";
            EditOrder  . ValueMember = "RAA001";

            txtLEF010 . Properties . DataSource = tableWorker;
            txtLEF010 . Properties . DisplayMember = "DAA002";
            txtLEF010 . Properties . ValueMember = "DAA001";

            EditUser . DataSource = tableUser;
            EditUser . DisplayMember = "EMP001";
            EditUser . ValueMember = "EMP001";

            EditLocal . DataSource = tableLocal;
            EditLocal . DisplayMember = "EMP007";
            EditLocal . ValueMember = "EMP007";
        }
        void setValue ( )
        {
            txtLEF001 . Text = _header . LEF001;
            txtLEF010 . EditValue = _header . LEF009;
            txtLEF012 . EditValue = _header . LEF011;
            txtLEF013 . Text = Convert . ToDateTime ( _header . LEF013 ) . ToString ( "yyyy-MM-dd" );
            txtLEF021 . Text = _header . LEF021;
            txtLEF023 . Text = _header . LEF023 . ToString ( );
            txtLEF024 . Text = _header . LEF024 . ToString ( );

            dtStart = Convert . ToDateTime ( _header . LEF023 );
            dtEnd = Convert . ToDateTime ( _header . LEF024 );

            txtLEF019 . Text = Convert . ToDecimal ( _header . LEF019 ) . ToString ( "0.#" );
            txtLEF020 . Text = Convert . ToDecimal ( _header . LEF020 ) . ToString ( "0.#" );
            txtLEF025 . Text = Convert . ToDecimal ( _header . LEF025 ) . ToString ( "0.######" );
            txtLEF022 . Text = _header . LEF022;
            txtLEF026 . Text = _header . LEF026 . ToString ( );
            txtLEF027 . Text = _header . LEF027 . ToString ( );


            layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPic ( pictureEdit1 ,"反" );
            if ( _header . LEF017 )
            {
                layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPic ( pictureEdit1 ,"审核" );
                examineTool ( "审核" );
            }
            else
              examineTool ( "反审核" );
            if ( _header . LEF018 )
            {
                layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPic ( pictureEdit1 ,"注销" );
                cancelltionTool ( "注销" );
            }
            else
                cancelltionTool ( "反注销" );
            
        }
        bool getValue ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtLEF001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtLEF010 . Text ) )
            {
                XtraMessageBox . Show ( "生产部门不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtLEF012 . Text ) )
            {
                XtraMessageBox . Show ( "班组不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtLEF021 . Text ) )
            {
                XtraMessageBox . Show ( "工资类型不可为空" );
                return false;
            }

            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return false;

            for ( int i = 0 ; i < GridView1 . RowCount ; i++ )
            {
                row = GridView1 . GetDataRow ( i );
                if ( row == null )
                    continue;
                row . ClearErrors ( );

                if ( row [ "LEG012" ] == null || row [ "LEG012" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LEG012" ,"不可为空" );
                    result = false;
                    break;
                }
                if ( row [ "LEG002" ] == null || row [ "LEG002" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LEG002" ,"不可为空" );
                    result = false;
                    break;
                }
                if ( row [ "LEG004" ] == null || row [ "LEG004" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LEG004" ,"不可为空" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "LEG002" )
                        } into m
                        select new
                        {
                            led002 = m . Key . p1 ,
                            count = m . Count ( )
                        };

            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . led002 + "重复,请核实" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );

            if ( txtLEF021 . Text . Equals ( "计件" ) )
            {
                if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                {
                    XtraMessageBox . Show ( "请选择来源工单等信息" );
                    return false;
                }
            }
            else if ( "计时" . Equals ( txtLEF021 . Text ) && ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 ) )
            {
                if ( XtraMessageBox . Show ( "是否选择来源工单?" ,"提示" ,MessageBoxButtons . YesNo ) == DialogResult . Yes )
                    return false;
            }

            gridView2 . ClearColumnErrors ( );
            for ( int i = 0 ; i < gridView2 . RowCount ; i++ )
            {
                row = gridView2 . GetDataRow ( i );
                if ( row == null )
                    continue;
                row . ClearErrors ( );

                    if ( row [ "LEH002" ] == null || row [ "LEH002" ] . ToString ( ) == string . Empty )
                    {
                        row . SetColumnError ( "LEH002" ,"请选择" );
                        result = false;
                        break;
                    }
                    if ( row [ "LEH003" ] == null || row [ "LEH003" ] . ToString ( ) == string . Empty )
                    {
                        row . SetColumnError ( "LEH003" ,"请选择" );
                        result = false;
                        break;
                    }
                    if ( row [ "LEH009" ] == null || row [ "LEH009" ] . ToString ( ) == string . Empty )
                    {
                        row . SetColumnError ( "LEH009" ,"请录入" );
                        result = false;
                        break;
                    }
                
                if ( row [ "LEH009" ] != null && row [ "LEH009" ] . ToString ( ) != string . Empty && row [ "U4" ] != null && row [ "U4" ] . ToString ( ) != string . Empty && Convert . ToInt32 ( row [ "LEH009" ] ) > Convert . ToInt32 ( row [ "U4" ] ) )
                {
                    row . SetColumnError ( "LEH009" ,"完工数量多于未完工数量" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            if ( tableViewTwo!=null && tableViewTwo.Rows.Count>0 )
            {
                query = null;
                query = from p in tableViewTwo . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "LEH002" )
                        } into m
                        select new
                        {
                            led002 = m . Key . p1 ,
                            count = m . Count ( )
                        };

                if ( query != null )
                {
                    foreach ( var x in query )
                    {
                        if ( x . count > 1 )
                        {
                            XtraMessageBox . Show ( "来源单号:" + x . led002 + "重复,请核实" );
                            result = false;
                            break;
                        }
                    }
                }

                if ( result == false )
                    return false;
            }

            _body . LEG001 = workShopTime . checkUserForOtherWork ( txtLEF013 . Text ,tableView ,LineProductMesBll . ObtainInfo . codeSix ,txtLEF001 . Text );
            if ( !string . IsNullOrEmpty ( _body . LEG001 ) )
            {
                XtraMessageBox . Show ( _body . LEG001 ,"提示" );
                return false;
            }

            if ( string . IsNullOrEmpty ( txtLEF023 . Text ) )
            {
                XtraMessageBox . Show ( "请选择开工时间" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtLEF024 . Text ) )
            {
                XtraMessageBox . Show ( "请选择完工时间" );
                return false;
            }

            _header . LEF023 = Convert . ToDateTime ( txtLEF023 . Text );
            _header . LEF024 = Convert . ToDateTime ( txtLEF024 . Text );

             outResult = 0M;
            if ( !string . IsNullOrEmpty ( txtLEF026 . Text ) && decimal . TryParse ( txtLEF026 . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "补贴工时是数字" );
                return false;
            }
            _header . LEF026 = outResult;
            outResult = 0M;
            if ( !string . IsNullOrEmpty ( txtLEF027 . Text ) && decimal . TryParse ( txtLEF027 . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "补贴单价是数字" );
                return false;
            }
            _header . LEF027 = outResult;

            _header . LEF001 = txtLEF001 . Text;
            _header . LEF009 = txtLEF010 . EditValue . ToString ( );
            _header . LEF010 = txtLEF010 . Text;
            _header . LEF011 = txtLEF012 . EditValue . ToString ( );
            _header . LEF012 = txtLEF012 . Text;
            _header . LEF013 = Convert . ToDateTime ( txtLEF013 . Text );
            _header . LEF015 = txtLEF015 . Text;
            _header . LEF016 = /*txtLEF016 . Text;*/string . Empty;
            _header . LEF017 = false;
            _header . LEF018 = false;
            _header . LEF019 = string . IsNullOrEmpty ( txtLEF019 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF019 . Text );
            _header . LEF020 = string . IsNullOrEmpty ( txtLEF020 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF020 . Text );
            _header . LEF021 = txtLEF021 . Text;
            _header . LEF025 = string . IsNullOrEmpty ( txtLEF025 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF025 . Text );
            return result;
        }
        /// <summary>
        /// 个人工时
        /// </summary>
        void calcuTimeSum ( )
        {
           
            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                txtu1 . Text = 0 . ToString ( );
                return;
            }
            decimal u0 = 0, u1 = 0, totalFP = 0;
            decimal lef019 = txtLEF019 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtLEF019 . Text ) ;
            decimal lef020 = txtLEF020 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtLEF020 . Text ) ;
            DateTime dtOne, dtTwo;


            foreach ( DataRow row in tableView . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "LEG012" ] . ToString ( ) ) || row [ "LEG012" ] . ToString ( ) . Equals ( "离职" ) || row [ "LEG012" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "LEG014" ] = 0;
                    row [ "LEG015" ] = 0;
                    row [ "LEG016" ] = 0;
                    continue;
                }

                u0 = 0;
                if ( !string . IsNullOrEmpty ( row [ "LEG005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "LEG006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "LEG005" ] );
                    dtTwo = Convert . ToDateTime ( row [ "LEG006" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );

                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( lef019 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( lef019 ) - Convert . ToDecimal ( lef020 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( lef020 );

                    row [ "LEG014" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "LEG014" ] = 0;

                if ( !"请假" . Equals ( row [ "LEG012" ] . ToString ( ).Trim() ) )
                    u1 += u0;
                else
                    totalFP = string . IsNullOrEmpty ( row [ "LEG017" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG017" ] );

                u0 = 0;

                if ( !string . IsNullOrEmpty ( row [ "LEG008" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "LEG009" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "LEG008" ] );
                    dtTwo = Convert . ToDateTime ( row [ "LEG009" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );

                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( lef019 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( lef019 ) - Convert . ToDecimal ( lef020 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( lef020 );

                    row [ "LEG015" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "LEG015" ] = 0;


                if ( !"请假" . Equals ( row [ "LEG012" ] . ToString ( ) . Trim ( ) ) )
                    u1 += u0;


            }

            txtu1 . Text = u1 . ToString ( "0.#" ); /*( LEG014 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( LEG014 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) + ( LEG015 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( LEG015 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) ) ) . ToString ( "0.#" );*/
            calcuSalaryUser ( totalFP );
        }
        void calcuSalaryTimeSum ( )
        {
            decimal? salaryTimeSum = 0;
            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                txtu2 . Text = 0 . ToString ( );
                return;
            }

            foreach ( DataRow row in tableView . Rows )
            {
                if ( row [ "LEG010" ] != null && row [ "LEG010" ] . ToString ( ) != string . Empty )
                    _body . LEG010 = Convert . ToDecimal ( row [ "LEG010" ] . ToString ( ) );
                else
                    _body . LEG010 = 0;

                salaryTimeSum += ( string . IsNullOrEmpty ( row [ "LEG015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG015" ] ) ) * _body . LEG010;

            }
            txtu2 . Text = Convert . ToDecimal ( salaryTimeSum ) . ToString ( "0.#" );
        }
        void calcuSalaryTieSum ( )
        {
            decimal? salaryTimeSum = 0;
            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                txtu4 . Text = 0 . ToString ( );
                return;
            }

            foreach ( DataRow row in tableView . Rows )
            {
                if ( row [ "LEG007" ] != null && row [ "LEG007" ] . ToString ( ) != string . Empty )
                    _body . LEG007 = Convert . ToDecimal ( row [ "LEG007" ] . ToString ( ) );
                else
                    _body . LEG007 = 0;
                salaryTimeSum += _body . LEG007;
            }
            txtu4 . Text = Convert . ToDecimal ( salaryTimeSum ) . ToString ( "0.#" );
        }
        /// <summary>
        /// 总工资
        /// </summary>
        void calcuSalarySum ( )
        {
            if ( "计件" . Equals ( txtLEF021 . Text ) )
            {
                txtu5 . Text = ( ( string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text ) ) + ( string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text ) ) - ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) ) + ( string . IsNullOrEmpty ( txtLEF025 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF025 . Text ) ) ) . ToString ( );
                if ( !string . IsNullOrEmpty ( txtLEF026 . Text ) && decimal . TryParse ( txtLEF026 . Text ,out outResult ) == false )
                {
                    XtraMessageBox . Show ( "补贴工时是数字" );
                    return;
                }
                outResult = 0M;
                if ( !string . IsNullOrEmpty ( txtLEF027 . Text ) && decimal . TryParse ( txtLEF027 . Text ,out outResult ) == false )
                {
                    XtraMessageBox . Show ( "补贴单价是数字" );
                    return;
                }
                txtu5 . Text = ( ( string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text ) ) + ( string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text ) ) - ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) ) + ( string . IsNullOrEmpty ( txtLEF025 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF025 . Text ) ) + ( string . IsNullOrEmpty ( txtLEF026 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF026 . Text ) * ( ( string . IsNullOrEmpty ( txtLEF027 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEF027 . Text ) ) ) ) ) . ToString ( );
            }
            else
                txtu5 . Text = ( ( string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text ) ) + ( string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text ) ) ) . ToString ( );
        }
        /// <summary>
        /// 个人工资
        /// </summary>
        void calcuSalaryUser ( decimal totalFP )
        {
            decimal salarySum = string . IsNullOrEmpty ( txtu5 . Text ) == true ? 0 : Convert . ToDecimal ( txtu5 . Text );
            decimal timeSum = string . IsNullOrEmpty ( txtu1 . Text ) == true ? 0 : Convert . ToDecimal ( txtu1 . Text );
            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );
            decimal timeSumUser = 0;
            decimal salarySumUser = 0M;

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;

            foreach ( DataRow row in tableView . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "LEG012" ] . ToString ( ) ) || row [ "LEG012" ] . ToString ( ) . Equals ( "离职" ) || row [ "LEG012" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "LEG014" ] = 0;
                    row [ "LEG015" ] = 0;
                    row [ "LEG016" ] = 0;
                    continue;
                }

                timeSumUser = string . IsNullOrEmpty ( row [ "LEG015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG015" ] . ToString ( ) ) + ( string . IsNullOrEmpty ( row [ "LEG014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEG014" ] . ToString ( ) ) );

                if ( row [ "LEG007" ] != null && row [ "LEG007" ] . ToString ( ) != string . Empty )
                    _body . LEG007 = Convert . ToDecimal ( row [ "LEG007" ] . ToString ( ) );
                else
                    _body . LEG007 = 0;
                salarySumUser = Convert . ToDecimal ( _body . LEG007 );

                if ( !"请假" . Equals ( row [ "LEG012" ] . ToString ( ) ) )
                    row [ "LEG016" ] = timeSum == 0 ? 0 . ToString ( ) : ( ( salarySum - totalFP ) / timeSum * timeSumUser + salarySumUser ) . ToString ( "0.##" );
                else
                    row [ "LEG016" ] = row [ "LEG017" ];
            }
        }
        void printOrExport ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtLEF001 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtLEF001 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void printOrExportOne ( )
        {
            tablePrintTre = _bll . getPrintTre ( txtLEF001 . Text );
            tablePrintTre . TableName = "TableOne";
            tablePrintFor = _bll . getPrintFor ( txtLEF001 . Text );
            tablePrintFor . TableName = "TableTwo";
            tablePrintFiv = _bll . getPrintFiv ( txtLEF001 . Text );
            tablePrintFiv . TableName = "TableTre";
        }
        void calcuSalaryByPrice ( )
        {
            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );

            if ( "计件" . Equals ( txtLEF021 . Text ) )
            {
                if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                    return;
                decimal result = 0M;
                foreach ( DataRow row in tableViewTwo . Rows )
                {
                    _bodyOne . LEH008 = string . IsNullOrEmpty ( row [ "LEH008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEH008" ] . ToString ( ) );
                    _bodyOne . LEH009 = string . IsNullOrEmpty ( row [ "LEH009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEH009" ] . ToString ( ) );
                    result += Convert . ToDecimal ( _bodyOne . LEH008 * _bodyOne . LEH009 );
                }
                txtu3 . Text = result . ToString ( "0.##" );
            }
            else if ( "计时" . Equals ( txtLEF021 . Text ) )
                txtu3 . Text = 0 . ToString ( );
        }
        void addRow ( string column ,int selectIdx ,object value )
        {
            if ( selectIdx < 0 )
                return;
            if ( selectIdx < tableView . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableView . Rows . Count ; i++ )
                {
                    row = tableView . Rows [ i ];
                    if ( i + 1 != tableView . Rows . Count )
                    {
                        if ( column . Equals ( "LEG005" ) )
                        {
                            if ( workShopTime . startTime ( row ,/*row [ "LEG005" ]*/ value ,"LEG006" ,"LEG008" ,"LEG009" ) )
                            {
                                ro = tableView . Rows [ i /*+ 1 */];
                                if ( ro [ "LEG005" ] == null || ro [ "LEG005" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LEG005" ] = /*row [ "LEG005" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "LEG006" ) )
                        {
                            if ( workShopTime . endTime ( row ,/*row [ "LEG006" ]*/value ,"LEG005" ,"LEG008" ,"LEG009" ) )
                            {
                                ro = tableView . Rows [ i/* + 1*/ ];
                                if ( ro [ "LEG006" ] == null || ro [ "LEG006" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LEG006" ] = /*row [ "LEG006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        if ( column . Equals ( "LEG008" ) )
                        {
                            if ( workShopTime . startCenTime ( row ,/*row [ "LEG008" ]*/value ,"LEG006" ,"LEG009" ,"LEG005" ) )
                            {
                                ro = tableView . Rows [ i /*+ 1*/ ];
                                if ( ro [ "LEG008" ] == null || ro [ "LEG008" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LEG008" ] = /*row [ "LEG008" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "LEG009" ) )
                        {
                            if ( workShopTime . endCenTime ( row ,/*row [ "LEG009" ]*/value ,"LEG008" ,"LEG005" ,"LEG006" ) )
                            {
                                ro = tableView . Rows [ i/* + 1*/ ];
                                if ( ro [ "LEG009" ] == null || ro [ "LEG009" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LEG009" ] = /*row [ "LEG009" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
            calcuTimeSum ( );
        }
        void editOtherSur ( string orderNum,string proNum )
        {
            _bodyOne . LEH001 = txtLEF001 . Text;
            _bodyOne . LEH002 = orderNum;
            _bodyOne . LEH003 = proNum;

            if ( _bodyOne . LEH002 == string . Empty || _bodyOne . LEH003 == string . Empty )
            {
                if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
                {
                    foreach ( DataRow row in tableViewTwo . Rows )
                    {
                        _bodyOne . LEH002 = row [ "LEH002" ] . ToString ( );
                        _bodyOne . LEH003 = row [ "LEH003" ] . ToString ( );
                        tableOtherSur = _bll . getTableOtherSur ( _bodyOne . LEH002 ,_bodyOne . LEH003 ,_bodyOne . LEH001 );
                        if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "LEH" ];
                        }
                        else
                        {
                            row [ "U4" ] = row [ "LEH007" ];
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne . LEH002 ,_bodyOne . LEH003 ,_bodyOne . LEH001 );
                if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewTwo . Select ( "LEH002='" + _bodyOne . LEH002 + "' AND LEH003='" + _bodyOne . LEH003 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = tableOtherSur . Select ( "LEH002='" + row [ "LEH002" ] + "' AND LEH003='" + row [ "LEH003" ] + "'" ) [ 0 ] [ "LEH" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = row [ "LEH007" ];
                        }
                    }
                }
            }

        }
        void queryTime ( )
        {
            dt = LineProductMesBll . UserInfoMation . sysTime;
            dtStart = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 08:00" ) );
            dtEnd = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:00" ) );
        }
        #endregion

    }
}








