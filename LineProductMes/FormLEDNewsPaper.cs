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

namespace LineProductMes
{
    public partial class FormLEDNewsPaper :FormChild
    {
        LineProductMesEntityu.LEDNewsPaperHeaderEntity _header=null;
        LineProductMesEntityu.LEDNewsPaperBodyEntity _body=null;
        LineProductMesBll.Bll.LEDNewsPaperBll _bll=null;
        LineProductMesEntityu.LEENewsPaperBodyEntity _bodyOne=null;

        Thread thread; SynchronizationContext m_SyncContext = null;

        DataTable tableView,tableViewOne,tableUser,tableWorker,tableLocal,tableProduct,tablePrintOne,tablePrintTwo,tableOtherSur;

        string state=string.Empty,strWhere="1=1",focuseName=string.Empty;
        bool result=false;
        DataRow row;
        List<string> idxList=new List<string>();
        List<string> idxListOne=new List<string>();

        DateTime dt;

        public FormLEDNewsPaper ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . LEDNewsPaperBll ( );
            _header = new LineProductMesEntityu . LEDNewsPaperHeaderEntity ( );
            _body = new LineProductMesEntityu . LEDNewsPaperBodyEntity ( );
            _bodyOne = new LineProductMesEntityu . LEENewsPaperBodyEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { GridView1 ,View1 ,View2 ,gridView2 ,View4 ,View5 ,View6 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { GridView1 ,View1 ,View2 ,gridView2 ,View4 ,View5 ,View6 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            dt1 . VistaEditTime = dt2 . VistaEditTime = dt3 . VistaEditTime = dt4 . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            controlUnEnable ( );
            controlClear ( );

            wait . Hide ( );

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext . Current;
            thread = new Thread ( new ThreadStart ( ThreadPost ) );
            thread . Start ( );

            dt = LineProductMesBll . UserInfoMation . sysTime;
        }

        #region Main
        protected override int Query ( )
        {
            FormLEDNewsPaperQuery from = new FormLEDNewsPaperQuery ( );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                _header . LEC001 = from . getOdd;

                _header = _bll . getModel ( _header . LEC001 );

                strWhere = "1=1";
                strWhere += " AND LED001='" + _header . LEC001 + "'";
                setValue ( );

                tableView = _bll . getTableView ( strWhere );
                gridControl1 . DataSource = tableView;
                strWhere = "1=1";
                strWhere += " AND LEE001='" + _header . LEC001 + "'";
                tableViewOne = _bll . getTableViewOne ( strWhere );
                gridControl2 . DataSource = tableViewOne;
                calcuSalaryByPrice ( );
                calcuTimeSum ( );
                calcuSalaryTimeSum ( );
                calcuSalaryTieSum ( );
                calcuSalarySum ( );
                calcuSalaryUser ( );

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
            txtLEC001 . Text = _bll . getOddNum ( );
            tableView = _bll . getTableView ( "1=2" );
            gridControl1 . DataSource = tableView;
            tableViewOne = _bll . getTableViewOne ( "1=2" );
            gridControl2 . DataSource = tableViewOne;
            txtLEC010 . EditValue = "0504";
            txtLEC021 . Text = "计件";
            txtLEC013 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );
            addTool ( );

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
            if ( string . IsNullOrEmpty ( txtLEC001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }

            result = _bll . Delete ( txtLEC001 . Text );
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
            if ( string . IsNullOrEmpty ( txtLEC001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . LEC017 = true;
            else
                _header . LEC017 = false;
            result = _bll . Exanmie ( txtLEC001 . Text ,_header . LEC017 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    //Graph . grPic ( pictureEdit1 ,"审 核" );
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
            if ( string . IsNullOrEmpty ( txtLEC001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . LEC018 = true;
            else
                _header . LEC018 = false;
            result = _bll . CancelTion ( txtLEC001 . Text ,_header . LEC018 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    //Graph . grPic ( pictureEdit1 ,"注 销" );
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
        #endregion

        #region Event
        private void GridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void txtLEC010_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtLEC010 . EditValue == null || txtLEC010 . EditValue . ToString ( ) == string . Empty )
                return;
            txtLEC012 . Properties . DataSource = _bll . getDepart ( txtLEC010 . EditValue . ToString ( ) );
            txtLEC012 . Properties . DisplayMember = "DAA002";
            txtLEC012 . Properties . ValueMember = "DAA001";

            txtLEC012 . EditValue = "0504";
        }
        private void txtLEC012_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtLEC010 . EditValue == null || txtLEC010 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( txtLEC012 . EditValue == null || txtLEC012 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( tableView == null )
                return;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            DataRow [ ] rows = tableUser . Select ( "EMP005='" + txtLEC012 . EditValue + "'" );
            if ( rows == null )
                return;
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                foreach ( DataRow r in rows )
                {
                    row = tableView . NewRow ( );
                    row [ "LED002" ] = r [ "EMP001" ];
                    row [ "LED003" ] = r [ "EMP002" ];
                    row [ "LED004" ] = r [ "EMP007" ];
                    row [ "LED013" ] = r [ "DAA002" ];
                    row [ "LED005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    row [ "LED006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    row [ "LED012" ] = "在职";
                    tableView . Rows . Add ( row );
                }
            }
            else
            {
                foreach ( DataRow r in rows )
                {
                    if ( tableView . Select ( "LED002='" + r [ "EMP001" ] + "'" ) . Length < 1 )
                    {
                        row = tableView . NewRow ( );
                        row [ "LED002" ] = r [ "EMP001" ];
                        row [ "LED003" ] = r [ "EMP002" ];
                        row [ "LED004" ] = r [ "EMP007" ];
                        row [ "LED013" ] = r [ "DAA002" ];
                        row [ "LED005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                        row [ "LED006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        row [ "LED012" ] = "在职";
                        tableView . Rows . Add ( row );
                    }
                }
            }
            calcuTimeSum ( );
        }
        private void backgroundWorker1_DoWork ( object sender ,System . ComponentModel . DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableView ,tableViewOne );
            else
                result = _bll . Edit ( _header ,tableView ,tableViewOne ,idxList ,idxListOne );
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
                        _header . LEC001 = txtLEC001 . Text = LineProductMesBll . UserInfoMation . oddNum;

                    setValue ( );
                    strWhere = "1=1";
                    strWhere += " AND LED001='" + _header . LEC001 + "'";
                    tableView = _bll . getTableView ( strWhere );
                    gridControl1 . DataSource = tableView;

                    strWhere = "1=1";
                    strWhere += " AND LEE001='" + _header . LEC001 + "'";
                    tableViewOne = _bll . getTableViewOne ( strWhere );
                    gridControl2 . DataSource = tableViewOne;
                    calcuSalaryByPrice ( );
                    calcuTimeSum ( );
                    calcuSalaryTimeSum ( );
                    calcuSalaryTieSum ( );
                    calcuSalarySum ( );
                    calcuSalaryUser ( );

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
            if ( GridView1 . FocusedColumn . FieldName == "LED012" )
            {
                if ( e . Value . Equals ( "离职" ) || e . Value . Equals ( "未上班" ) )
                {
                    row [ "LED005" ] = DBNull . Value;
                    row [ "LED006" ] = DBNull . Value;
                    row [ "LED007" ] = DBNull . Value;
                    row [ "LED008" ] = DBNull . Value;
                    row [ "LED009" ] = DBNull . Value;
                    row [ "LED010" ] = DBNull . Value;
                    row [ "LED014" ] = DBNull . Value;
                    row [ "LED015" ] = DBNull . Value;

                }
                else if ( e . Value . Equals ( "在职" ) )
                {
                    if ( row [ "LED005" ] == null || row [ "LED005" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LED005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "LED006" ] == null || row [ "LED006" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LED006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                calcuTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED005" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"LED006" ,"LED008" ,"LED009" ) == false )
                {
                    row [ "LED005" ] = DBNull . Value;
                }
                addRow ( "LED005" ,e . RowHandle ,e . Value );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED006" )
            {
                if ( workShopTime . endTime ( row ,e . Value ,"LED005" ,"LED008" ,"LED009" ) == false )
                {
                    row [ "LED006" ] = DBNull . Value;
                }
                addRow ( "LED006" ,e . RowHandle ,e . Value );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED008" )
            {
                if ( workShopTime . startCenTime ( row ,e . Value ,"LED006" ,"LED009" ,"LED005" ) == false )
                {
                    row [ "LED008" ] = DBNull . Value;
                }
                addRow ( "LED008" ,e . RowHandle ,e . Value );
                calcuSalaryTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED009" )
            {
                if ( workShopTime . endCenTime ( row ,e . Value ,"LED008" ,"LED005" ,"LED006" ) == false )
                {
                    row [ "LED009" ] = DBNull . Value;
                }
                addRow ( "LED009" ,e . RowHandle ,e . Value );
                calcuSalaryTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED010" )
            {
                //led015
                int selectIndex = GridView1 . FocusedRowHandle;
                string led010Result = GridView1 . GetDataRow ( selectIndex ) [ "LED010" ] . ToString ( );

                if ( string . IsNullOrEmpty ( led010Result ) )
                    _body . LED010 = 0;
                else
                    _body . LED010 = Convert . ToDecimal ( led010Result );

                for ( int i = selectIndex ; i < tableView . Rows . Count ; i++ )
                {
                    row = tableView . Rows [ i ];
                    if ( row [ "LED015" ] != null && row [ "LED015" ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ "LED010" ] == null || row [ "LED010" ] . ToString ( ) == string . Empty )
                        {
                            row . BeginEdit ( );
                            row [ "LED010" ] = _body . LED010;
                            row . EndEdit ( );
                        }
                    }
                    if ( i == selectIndex && ( row [ "LED015" ] == null || row [ "LED015" ] . ToString ( ) == string . Empty ) )
                    {
                        row . BeginEdit ( );
                        row [ "LED010" ] = DBNull . Value;
                        row . EndEdit ( );
                    }
                }
                gridControl1 . Refresh ( );


                calcuSalaryTimeSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED007" )
            {
                calcuSalaryTieSum ( );
            }
            else if ( GridView1 . FocusedColumn . FieldName == "LED002" || GridView1 . FocusedColumn . FieldName == "LED003" || GridView1 . FocusedColumn . FieldName == "LED004" || GridView1 . FocusedColumn . FieldName == "LED013" )
            {

                if ( row [ "LED005" ] == null || row [ "LED005" ] . ToString ( ) == string . Empty )
                {
                    row [ "LED005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                }
                if ( row [ "LED006" ] == null || row [ "LED006" ] . ToString ( ) == string . Empty )
                {
                    row [ "LED006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                }
                if ( row [ "LED012" ] == null || row [ "LED012" ] . ToString ( ) == string . Empty )
                {
                    row [ "LED012" ] = "在职";
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
            calcuSalaryUser ( );
        }
        private void txtu1_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalaryUser ( );
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            row = GridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    _body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _body . idx > 0 && !idxList . Contains ( _body . idx . ToString ( ) ) )
                        idxList . Add ( _body . idx . ToString ( ) );
                    tableView . Rows . Remove ( row );
                    gridControl1 . RefreshDataSource ( );
                }
            }
        }
        private void EditUser_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = GridView1 . ActiveEditor;
            switch ( GridView1 . FocusedColumn . FieldName )
            {
                case "LED002":
                if ( edit . EditValue == null )
                    return;
                row = tableUser . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _body . LED003 = row [ "EMP002" ] . ToString ( );
                _body . LED004 = row [ "EMP007" ] . ToString ( );
                _body . LED013 = row [ "DAA002" ] . ToString ( );
                GridView1 . SetFocusedRowCellValue ( GridView1 . Columns [ "LED003" ] ,_body . LED003 );
                GridView1 . SetFocusedRowCellValue ( GridView1 . Columns [ "LED004" ] ,_body . LED004 );
                GridView1 . SetFocusedRowCellValue ( GridView1 . Columns [ "LED013" ] ,_body . LED013 );
                break;
            }
        }
        private void EditOrder_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView2 . ActiveEditor;
            switch ( gridView2 . FocusedColumn . FieldName )
            {
                case "LEE002":
                if ( edit . EditValue == null )
                    return;
                row = tableProduct . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyOne . LEE010 = row [ "RAA008" ] . ToString ( );
                if ( string . IsNullOrEmpty ( _bodyOne . LEE010 ) )
                    return;
                _bodyOne . LEE002 = edit . EditValue . ToString ( );
                _bodyOne . LEE003 = row [ "RAA015" ] . ToString ( );
                _bodyOne . LEE004 = row [ "DEA002" ] . ToString ( );
                _bodyOne . LEE005 = row [ "DEA057" ] . ToString ( );
                _bodyOne . LEE006 = row [ "DEA003" ] . ToString ( );
                _bodyOne . LEE007 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
                _bodyOne . LEE008 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEE003" ] ,_bodyOne . LEE003 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEE004" ] ,_bodyOne . LEE004 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEE005" ] ,_bodyOne . LEE005 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEE006" ] ,_bodyOne . LEE006 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEE007" ] ,_bodyOne . LEE007 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "LEE008" ] ,_bodyOne . LEE008 );

                editOtherSur ( _bodyOne . LEE002 ,_bodyOne . LEE003 );

                break;
            }
        }
        private void gridView2_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "LEE008" || e . Column . FieldName == "LEE009" )
            {
                calcuSalaryByPrice ( );
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtLEC010 . Text == string . Empty || tableView == null || tableView . Rows . Count < 1 )
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
        private void txtLEC019_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
        }
        private void txtLEC020_TextChanged ( object sender ,EventArgs e )
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
            if ( e . Column . FieldName == "U2" )
            {
                if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDecimal ( e . CellValue ) >= 200 )
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                }
            }
        }
        private void txtLEC021_SelectedValueChanged ( object sender ,EventArgs e )
        {
            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;

            foreach ( DataRow row in tableView . Rows )
            {
                if ( txtLEC021 . Text . Equals ( "计件" ) )
                {
                    row [ "LED005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    row [ "LED006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    //row [ "LEG014" ] = null;
                    row [ "LED008" ] = DBNull . Value;
                    row [ "LED009" ] = DBNull . Value;
                    row [ "LED015" ] = DBNull . Value;
                }
                else if ( txtLEC021 . Text . Equals ( "计时" ) )
                {
                    row [ "LED005" ] = DBNull . Value;
                    row [ "LED006" ] = DBNull . Value;
                    row [ "LED014" ] = DBNull . Value;
                    row [ "LED008" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    row [ "LED009" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    //row [ "LEG015" ] = null;
                }
            }
            calcuSalaryByPrice ( );
            calcuTimeSum ( );
            calcuSalaryUser ( );
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtLEC010 . ReadOnly = txtLEC012 . ReadOnly = txtLEC015 . ReadOnly = txtLEC019 . ReadOnly = txtLEC020 . ReadOnly = true;
            GridView1 . OptionsBehavior . Editable = false;
            gridView2 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtLEC010 . ReadOnly = txtLEC012 . ReadOnly = txtLEC015 . ReadOnly = txtLEC019 . ReadOnly = txtLEC020 . ReadOnly = false;
            GridView1 . OptionsBehavior . Editable = true;
            gridView2 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            gridControl1 . DataSource = null;
            gridControl2 . DataSource = null;
            txtLEC001 . Text = txtLEC010 . Text = txtLEC012 . Text = txtLEC013 . Text = txtLEC015 . Text = txtLEC019 . Text = txtLEC020 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtu5 . Text = string . Empty;
            txtLEC001 . Text = txtLEC010 . Text = txtLEC012 . Text = txtLEC013 . Text =  txtLEC015 . Text = txtLEC019 . Text = txtLEC020 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtu5 . Text = string . Empty;
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

            txtLEC010 . Properties . DataSource = tableWorker;
            txtLEC010 . Properties . DisplayMember = "DAA002";
            txtLEC010 . Properties . ValueMember = "DAA001";

            EditUser . DataSource = tableUser;
            EditUser . DisplayMember = "EMP001";
            EditUser . ValueMember = "EMP001";

            EditLocal . DataSource = tableLocal;
            EditLocal . DisplayMember = "EMP007";
            EditLocal . ValueMember = "EMP007";
        }
        void setValue ( )
        {
            txtLEC001 . Text = _header . LEC001;
            txtLEC010 . EditValue = _header . LEC009;
            txtLEC012 . EditValue = _header . LEC011;
            txtLEC013 . Text = Convert . ToDateTime ( _header . LEC013 ) . ToString ( "yyyy-MM-dd" );
            txtLEC019 . Text = Convert . ToDecimal ( _header . LEC019 ) . ToString ( "0.#" );
            txtLEC020 . Text = Convert . ToDecimal ( _header . LEC020 ) . ToString ( "0.#" );
            txtLEC021 . Text = _header . LEC021;
            layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPic ( pictureEdit1 ,"反" );
            if ( _header . LEC017 )
            {
                layoutControlItem21 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPic ( pictureEdit1 ,"审核" );
                examineTool ( "审核" );
            }
            else
              examineTool ( "反审核" );
            if ( _header . LEC018 )
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
            if ( string . IsNullOrEmpty ( txtLEC001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtLEC010 . Text ) )
            {
                XtraMessageBox . Show ( "生产部门不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtLEC012 . Text ) )
            {
                XtraMessageBox . Show ( "班组不可为空" );
                return false;
            }

            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return false;

            for ( int i=0 ;i<GridView1.RowCount ;i++ )
            {
                row = GridView1 . GetDataRow ( i );
                if ( row == null )
                    continue;
                row . ClearErrors ( );
                if ( row [ "LED012" ] == null || row [ "LED012" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LED012" ,"不可为空" );
                    result = false;
                    break;
                }
                if ( row [ "LED002" ] == null || row [ "LED002" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LED002" ,"不可为空" );
                    result = false;
                    break;
                }
                if ( row [ "LED004" ] == null || row [ "LED004" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LED004" ,"不可为空" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "LED002" )
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

            if ( txtLEC021 . Text . Equals ( "计件" ) )
            {
                if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                {
                    XtraMessageBox . Show ( "请选择工单等信息" );
                    return false;
                }
            }

            gridView2 . ClearColumnErrors ( );
            for ( int i = 0 ; i < gridView2 . RowCount ; i++ )
            {
                row = gridView2 . GetDataRow ( i );
                if ( row == null )
                    continue;
                row . ClearErrors ( );

                if ( row [ "LEE002" ] == null || row [ "LEE002" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LEE002" ,"请选择" );
                    result = false;
                    break;
                }
                if ( row [ "LEE003" ] == null || row [ "LEE003" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LEE003" ,"请选择" );
                    result = false;
                    break;
                }
                if ( row [ "LEE009" ] == null || row [ "LEE009" ] . ToString ( ) == string . Empty )
                {
                    row . SetColumnError ( "LEE009" ,"请录入" );
                    result = false;
                    break;
                }
                if ( row [ "LEE009" ] != null && row [ "LEE009" ] . ToString ( ) != string . Empty && row [ "U4" ] != null && row [ "U4" ] . ToString ( ) != string . Empty && Convert . ToInt32 ( row [ "LEE009" ] ) > Convert . ToInt32 ( row [ "U4" ] ) )
                {
                    row . SetColumnError ( "LEE009" ,"完工数量多于未完工数量" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
            {
                query = null;
                query = from p in tableViewOne . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "LEE002" )
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
            }

            if ( result == false )
                return false;
            

            _header . LEC001 = txtLEC001 . Text;
            _header . LEC009 = txtLEC010 . EditValue . ToString ( );
            _header . LEC010 = txtLEC010 . Text;
            _header . LEC011 = txtLEC012 . EditValue . ToString ( );
            _header . LEC012 = txtLEC012 . Text;
            _header . LEC013 = Convert . ToDateTime ( txtLEC013 . Text );
            _header . LEC015 = txtLEC015 . Text;
            _header . LEC016 = /*txtLEC016 . Text;*/string . Empty;
            _header . LEC019 = string . IsNullOrEmpty ( txtLEC019 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEC019 . Text );
            _header . LEC020 = string . IsNullOrEmpty ( txtLEC020 . Text ) == true ? 0 : Convert . ToDecimal ( txtLEC020 . Text );
            _header . LEC017 = false;
            _header . LEC018 = false;
            _header . LEC021 = txtLEC021 . Text;

            return result;
        }
        void calcuTimeSum ( )
        {
            GridView1 . CloseEditor ( );
            GridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                txtu1 . Text = 0 . ToString ( );
                return;
            }

            decimal lec019 = txtLEC019 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtLEC019 . Text ) * 60;
            decimal lec020 = txtLEC020 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtLEC020 . Text ) * 60;
            DateTime dtOne, dtTwo;
            decimal u0 = 0M, u1 = 0M;

            foreach ( DataRow row in tableView . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "LED012" ] . ToString ( ) ) || row [ "LED012" ] . ToString ( ) . Equals ( "离职" ) || row [ "LED012" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "LED014" ] = 0;
                    row [ "LED015" ] = 0;
                    row [ "LED016" ] = 0;
                    continue;
                }

                u1 = 0;
                if ( !string . IsNullOrEmpty ( row [ "LED005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "LED006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "LED005" ] );
                    dtTwo = Convert . ToDateTime ( row [ "LED006" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( lec019 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( "17:30" ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( lec019 ) - Convert . ToDecimal ( lec020 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( "17:30" ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( lec020 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "LED014" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    u1 += u0;
                }
                else
                    row [ "LED014" ] = 0;
                u0 = 0;
                if ( !string . IsNullOrEmpty ( row [ "LED008" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "LED009" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "LED008" ] );
                    dtTwo = Convert . ToDateTime ( row [ "LED009" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( lec019 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( "17:30" ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( lec019 ) - Convert . ToDecimal ( lec020 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( "17:30" ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( lec020 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "LED015" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    u1 += u0;
                }
                else
                    row [ "LED015" ] = 0;
                //row [ "U3" ] = Math . Round ( u1 ,1 ,MidpointRounding . AwayFromZero );
            }
            txtu1 . Text = ( LED014 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( LED014 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) + ( LED015 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( LED015 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) ) ) . ToString ( "0.#" );
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
                salaryTimeSum += ( string . IsNullOrEmpty ( row [ "LED015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED015" ] . ToString ( ) ) ) * ( string . IsNullOrEmpty ( row [ "LED010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED010" ] . ToString ( ) ) );
            }
            txtu2 . Text = Convert . ToDecimal ( salaryTimeSum ) . ToString ( "0.###" );
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
                if ( row [ "LED007" ] != null && row [ "LED007" ] . ToString ( ) != string . Empty )
                    _body . LED007 = Convert . ToDecimal ( row [ "LED007" ] . ToString ( ) );
                else
                    _body . LED007 = 0;
                salaryTimeSum += _body . LED007;
            }
            txtu4 . Text = Convert . ToDecimal ( salaryTimeSum ) . ToString ( "0.###" );
        }
        void calcuSalarySum ( )
        {
            txtu5 . Text = ( (string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text )) + ( string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text ) ) - ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) ) ) . ToString ( );
        }
        void calcuSalaryUser ( )
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
                if ( string . IsNullOrEmpty ( row [ "LED012" ] . ToString ( ) ) || row [ "LED012" ] . ToString ( ) . Equals ( "离职" ) || row [ "LED012" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "LED014" ] = 0;
                    row [ "LED015" ] = 0;
                    row [ "LED016" ] = 0;
                    continue;
                }

                timeSumUser = string . IsNullOrEmpty ( row [ "LED015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED015" ] . ToString ( ) ) + ( string . IsNullOrEmpty ( row [ "LED014" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LED014" ] . ToString ( ) ) );
                if ( row [ "LED007" ] != null && row [ "LED007" ] . ToString ( ) != string . Empty )
                    _body . LED007 = Convert . ToDecimal ( row [ "LED007" ] . ToString ( ) );
                else
                    _body . LED007 = 0;
                salarySumUser = Convert . ToDecimal ( _body . LED007 );
                row [ "LED016" ] = timeSum == 0 ? 0 . ToString ( ) : ( salarySum / timeSum * timeSumUser + salarySumUser ) . ToString ( "0.##" );
            }
        }
        void printOrExport ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtLEC001 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtLEC001 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void calcuSalaryByPrice ( )
        {
            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );

            if ( "计件" . Equals ( txtLEC021 . Text ) )
            {
                if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                    return;
                decimal result = 0M;
                foreach ( DataRow row in tableViewOne . Rows )
                {
                    _bodyOne . LEE008 = string . IsNullOrEmpty ( row [ "LEE008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LEE008" ] . ToString ( ) );
                    _bodyOne . LEE009 = string . IsNullOrEmpty ( row [ "LEE009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LEE009" ] . ToString ( ) );
                    result += Convert . ToDecimal ( _bodyOne . LEE008 * _bodyOne . LEE009 );
                }
                txtu3 . Text = result . ToString ( "0.##" );
            }
            else if ( "计时" . Equals ( txtLEC021 . Text ) )
                txtu3 . Text = 0 . ToString ( "0.##" );
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
                        if ( column . Equals ( "LED005" ) )
                        {
                            if ( workShopTime . startTime ( row ,row [ "LED005" ] ,"LED006" ,"LED008" ,"LED009" ) )
                            {
                                ro = tableView . Rows [ i + 1 ];
                                if ( ro [ "LED005" ] == null || ro [ "LED005" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LED005" ] = /*row [ "LED005" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "LED006" ) )
                        {
                            if ( workShopTime . endTime ( row ,row [ "LED006" ] ,"LED005" ,"LED008" ,"LED009" ) )
                            {
                                ro = tableView . Rows [ i + 1 ];
                                if ( ro [ "LED006" ] == null || ro [ "LED006" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LED006" ] = /*row [ "LED006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        if ( column . Equals ( "LED008" ) )
                        {
                            if ( workShopTime . startCenTime ( row ,row [ "LED008" ] ,"LED006" ,"LED009" ,"LED005" ) )
                            {
                                ro = tableView . Rows [ i + 1 ];
                                if ( ro [ "LED008" ] == null || ro [ "LED008" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LED008" ] = /*row [ "LED008" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "LED009" ) )
                        {
                            if ( workShopTime . endCenTime ( row ,row [ "LED009" ] ,"LED008" ,"LED005" ,"LED006" ) )
                            {
                                ro = tableView . Rows [ i + 1 ];
                                if ( ro [ "LED009" ] == null || ro [ "LED009" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "LED009" ] = /*row [ "LED009" ];*/value;
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
        void editOtherSur ( string orderNum ,string proNum )
        {
            _bodyOne . LEE001 = txtLEC001 . Text;
            _bodyOne . LEE002 = orderNum;
            _bodyOne . LEE003 = proNum;

            if ( _bodyOne . LEE002 == string . Empty || _bodyOne . LEE003 == string . Empty )
            {
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    foreach ( DataRow row in tableViewOne . Rows )
                    {
                        _bodyOne . LEE002 = row [ "LEE002" ] . ToString ( );
                        _bodyOne . LEE003 = row [ "LEE003" ] . ToString ( );
                        tableOtherSur = _bll . getTableOtherSur ( _bodyOne . LEE002 ,_bodyOne . LEE003 ,_bodyOne . LEE001 );
                        if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "LEE" ];
                        }
                        else
                        {
                            row [ "U4" ] = row [ "LEE007" ];
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne . LEE002 ,_bodyOne . LEE003 ,_bodyOne . LEE001 );
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewOne . Select ( "LEE002='" + _bodyOne . LEE002 + "' AND LEE003='" + _bodyOne . LEE003 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = tableOtherSur . Select ( "LEE002='" + row [ "LEE002" ] + "' AND LEE003='" + row [ "LEE003" ] + "'" ) [ 0 ] [ "LEE" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = row [ "LEE007" ];
                        }
                    }
                }
            }

        }
        #endregion

    }
}








