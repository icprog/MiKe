using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using System . Linq;
using LineProductMes . ChildForm;

namespace LineProductMes
{
    public partial class FormAssNewWork :FormChild
    {
        LineProductMesBll.Bll.AssNewWorkBll _bll=null;
        LineProductMesEntityu.AssNewWorkBodyEntity _body=null;
        LineProductMesEntityu.AssNewWorkHeaderEntity _header=null;
        LineProductMesEntityu.AssNewWorkBodyAnnEntity _bodyOne=null;
        DataTable tableWork,tableView,tableViewTwo,tableOtherSur,tableUser,tablePInfo,talePrintOne,talePrintTwo,tablePrintTre,tablePrintFor,tablePrintFiv;
        DataRow row;
        List<string> idxDelete=new List<string>(); List<string> idxDeleteOne=new List<string>();
        int selectIndex;
        string parame=string.Empty,strWhere=string.Empty,state=string.Empty,focusName=string.Empty;
        bool result=false;
        DateTime dt,dtStart,dtEnd;
        
        public FormAssNewWork ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkBll ( );
            _body = new LineProductMesEntityu . AssNewWorkBodyEntity ( );
            _header = new LineProductMesEntityu . AssNewWorkHeaderEntity ( );
            _bodyOne = new LineProductMesEntityu . AssNewWorkBodyAnnEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,View ,View1 ,ViewInfo ,View3 ,gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,View ,View1 ,ViewInfo ,View3 ,gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Edit1 . VistaEditTime = Edit2 . VistaEditTime = Edit3 . VistaEditTime = Edit4 . VistaEditTime = txtANW015 . Properties . VistaEditTime = txtANW016 . Properties . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            wait . Hide ( );

            controlUnEnable ( );
            controlClear ( );

            //Task task = new Task ( InitData );
            //task . Start ( );
            InitData ( );
            getInitData ( );
            queryTime ( );
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormAssNewWorkQuery from = new ChildForm . FormAssNewWorkQuery ( );
            from . StartPosition = FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                _header . ANW001 = from . getOdd;
                strWhere = "1=1";
                strWhere += " AND ANX001='" + _header . ANW001 + "'";

                _header = _bll . getHeader ( _header . ANW001 );
                setValue ( );

                tableView = _bll . getTableView ( strWhere );
                gridControl1 . DataSource = tableView;

                strWhere = "1=1";
                strWhere += " AND ANN001='" + _header . ANW001 + "'";
                tableViewTwo = _bll . getTableViewOne ( strWhere );
                gridControl2 . DataSource = tableViewTwo;

                calcuPriceSum ( );
                //calcuSalaryForSub ( );
                calcuSalarySum ( );
                calcuSalaryForSub ( );
                calcuSalaryForTime ( );
                //calcuTSumByP ( );
                calcuTsumTime ( );
                //calcuTSumByT ( );
                setCalcuSalary ( );
                //calcuTsumTime ( );
                //addTotalTime ( );

                editOtherSur ( string . Empty ,string . Empty );

                QueryTool ( );
            }
            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            txtANW001 . Text = _bll . getOddNum ( );
            DateTime dtTime = LineProductMesBll . UserInfoMation . sysTime;

            txtANW022 . Text = dtTime . ToString ( "yyyy-MM-dd" );

            tableView = _bll . getTableView ( "1=2" );
            gridControl1 . DataSource = tableView;
            tableViewTwo = _bll . getTableViewOne ( "1=2" );
            gridControl2 . DataSource = tableViewTwo;

            addTool ( );
            state = "add";
            txtANW014 . Text = "计件";
            txtANW011 . EditValue = "0507";

            queryTime ( );

            txtANW015 . Text = dtStart . ToString ( );
            txtANW016 . Text = dtEnd . ToString ( );
            txtANW025 . Text = LineProductMesBll . UserInfoMation . userName;
           
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
            if ( string . IsNullOrEmpty ( txtANW001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            result = _bll . Delete ( txtANW001 . Text );
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


            int nums = _bll . ExistsNum ( _header );
            if ( nums > 0 )
            {
                XtraMessageBox . Show ( "已完工量为:" + nums . ToString ( ) + ",剩余完工量应为:" + ( _header . ANW006 - nums ) . ToString ( ) + "" );
                ClassForMain . FormClosingState . formClost = true;
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
            if ( string . IsNullOrEmpty ( txtANW001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . ANW020 = true;
            else
                _header . ANW020 = false;
            result = _bll . Exanmie ( txtANW001 . Text ,_header . ANW020 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicZ ( pictureEdit1 ,"审核" );
                }
                else
                {
                    layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicZ ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtANW001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . ANW021 = true;
            else
                _header . ANW021 = false;
            result = _bll . CancelTion ( txtANW001 . Text ,_header . ANW021 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicZ ( pictureEdit1 ,"注销" );
                }
                else
                {
                    layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicZ ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        protected override int PrintWork ( )
        {
            printOrExport ( );
            Print ( new DataTable [ ] { talePrintOne ,talePrintTwo } ,"入库单.frx" );

            return base . PrintWork ( );
        }
        protected override int ExportWork ( )
        {
            printOrExport ( );
            Export ( new DataTable [ ] { talePrintOne ,talePrintTwo } ,"入库单.frx" );

            return base . ExportWork ( );
        }
        protected override int PrintReport ( )
        {
            printOrExportOne ( );
            Print ( new DataTable [ ] { tablePrintTre ,tablePrintFor,tablePrintFiv } ,"组装报工单.frx" );

            return base . PrintReport ( );
        }
        protected override int ExportReport ( )
        {
            printOrExportOne ( );
            Export ( new DataTable [ ] { tablePrintTre ,tablePrintFor ,tablePrintFiv } ,"组装报工单.frx" );

            return base . ExportReport ( );
        }
        #endregion

        #region Event
        private void bandedGridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void txtANW011_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANW011 . EditValue == null || txtANW011 . EditValue . ToString ( ) == string . Empty )
                return;
            txtANW013 . Properties . DataSource = _bll . getDepart ( txtANW011 . EditValue . ToString ( ) );
            txtANW013 . Properties . DisplayMember = "DAA002";
            txtANW013 . Properties . ValueMember = "DAA001";

            txtANW013 . EditValue = "050701";
        }
        private void txtANW013_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANW011 . EditValue == null || txtANW011 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( txtANW013 . EditValue == null || txtANW013 . EditValue . ToString ( ) == string . Empty )
                return;

            if ( tableView == null )
                return;

            if ( tableView != null )
                tableView . Rows . Clear ( );

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            DataRow [ ] rowes = tableUser . Select ( "EMP005='" + txtANW013 . EditValue . ToString ( ) + "'" );
            DataRow rows;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            if ( tableView . Rows . Count < 1 )
            {
                foreach ( DataRow row in rowes )
                {
                    if ( row != null )
                    {
                        rows = tableView . NewRow ( );
                        rows [ "ANX002" ] = row [ "EMP001" ] . ToString ( );
                        rows [ "ANX003" ] = row [ "EMP002" ] . ToString ( );
                        rows [ "ANX004" ] = row [ "EMP007" ] . ToString ( );
                        rows [ "ANX014" ] = row [ "DAA002" ] . ToString ( );
                        rows [ "ANX011" ] = "在职";
                        if ( "检测" . Equals ( row [ "EMP007" ] . ToString ( ) ) )
                            rows [ "ANX013" ] = "检测";
                        if ( txtANW014 . Text . Equals ( "计件" ) )
                        {
                            rows [ "ANX005" ] = dtStart;
                            rows [ "ANX006" ] = dtEnd;
                        }
                        else
                        {
                            rows [ "ANX007" ] = dtStart;
                            rows [ "ANX008" ] = dtEnd;
                        }
                        tableView . Rows . Add ( rows );
                        gridControl1 . Refresh ( );
                    }
                }
            }
            else
            {
                foreach ( DataRow row in rowes )
                {
                    if ( tableView . Select ( "ANX002='" + row [ "ANX002" ] . ToString ( ) + "'" ) . Length < 1 )
                    {
                        rows = tableView . NewRow ( );
                        rows [ "ANX002" ] = row [ "EMP001" ] . ToString ( );
                        rows [ "ANX003" ] = row [ "EMP002" ] . ToString ( );
                        rows [ "ANX004" ] = row [ "EMP007" ] . ToString ( );
                        rows [ "ANX014" ] = row [ "DAA002" ] . ToString ( );
                        rows [ "ANX011" ] = "在职";
                        if ( "检测" . Equals ( row [ "EMP007" ] . ToString ( ) ) )
                            rows [ "ANX013" ] = "检测";
                        if ( txtANW014 . Text . Equals ( "计件" ) )
                        {
                            rows [ "ANX005" ] = dtStart;
                            rows [ "ANX006" ] = dtEnd;
                        }
                        else
                        {
                            rows [ "ANX007" ] = dtStart;
                            rows [ "ANX008" ] = dtEnd;
                        }
                        tableView . Rows . Add ( rows );
                        gridControl1 . Refresh ( );
                    }
                }
            }
            calcuTsumTime ( );
        }
        private void Edit6_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "ANX002":
                if ( edit . EditValue == null )
                    return;
                row = tableUser . Select ( "ANX002='" + edit . EditValue + "'" ) [ 0 ];
                if ( tableView != null && tableView . Rows . Count > 0 )
                {
                    if ( tableView . Select ( "ANX002='" + edit . EditValue + "'" ) . Length > 0 )
                    {
                        edit . EditValue = null;
                        return;
                    }
                }
                _body . ANX003 = row [ "ANX003" ] . ToString ( );
                _body . ANX004 = row [ "ANX004" ] . ToString ( );
                _body . ANX014 = row [ "DAA002" ] . ToString ( );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANX003" ] ,_body . ANX003 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANX004" ] ,_body . ANX004 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANX014" ] ,_body . ANX014 );
                if ( "检测" . Equals ( _body . ANX004 ) )
                    bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANX013" ] ,"检测" );
                break;
            }
        }
        private void gridControl1_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if (toolSave.Visibility== DevExpress.XtraBars.BarItemVisibility.Always && e . KeyChar == ( char ) Keys . Enter )
            {
                row = bandedGridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                if ( XtraMessageBox . Show ( "确认删除?" ,"提示" ,MessageBoxButtons . YesNo ) == DialogResult . Yes )
                {
                    _body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _body . idx > 0 && !idxDelete . Contains ( _body . idx . ToString ( ) ) )
                        idxDelete . Add ( _body . idx . ToString ( ) );
                    tableView . Rows . Remove ( row );
                    gridControl1 . Refresh ( );
                }
            }
        }
        private void txtANW014_SelectedValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANW014 . Text == string . Empty )
                return;

            updateBatchTime ( );
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            string anx009Result = string . Empty;
            decimal outRsult = 0M;

            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "ANX011" )
            {
                if ( row [ "ANX011" ] != null && row [ "ANX011" ] . ToString ( ) != string . Empty && ( row [ "ANX011" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANX011" ] . ToString ( ) . Equals ( "未上班" ) ) )
                {
                    row [ "ANX005" ] = DBNull . Value;
                    row [ "ANX006" ] = DBNull . Value;
                    row [ "ANX007" ] = DBNull . Value;
                    row [ "ANX008" ] = DBNull . Value;
                    row [ "ANX017" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                    row [ "ANX009" ] = DBNull . Value;
                    row [ "ANX010" ] = DBNull . Value;
                }
                else if ( row [ "ANX011" ] . ToString ( ) . Equals ( "在职" ) || row [ "ANX011" ] . ToString ( ) . Equals ( "请假" ) )
                    editTime ( );

                //calcuBTSay ( );
                calcuSalaryForTime ( );
                calcuSalaryForSub ( );
                calcuTsumTime ( );
                calcuSalarySum ( );
                //calcuTSumByP ( );
            }
            else if ( e . Column . FieldName == "ANX018" )
            {
                calcuSalarySum ( );
            }
            else if ( e . Column . FieldName == "ANX005" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"ANX006" ,"ANX007" ,"ANX008" ) == false )
                {
                    row [ "ANX005" ] = DBNull . Value;
                    row [ "ANX017" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                }
                //calcuTSumByP ( );
                //calcuBTSay ( );
                calcuSalaryForSub ( );
                calcuSalarySum ( );
                addRows ( "ANX005" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "ANX006" )
            {
                if ( workShopTime . endTime ( row ,e . Value ,"ANX005" ,"ANX007" ,"ANX008" ) == false )
                {
                    row [ "ANX006" ] = DBNull . Value;
                    row [ "ANX017" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                }
                //calcuTSumByP ( );
                //calcuBTSay ( );
                calcuSalaryForSub ( );
                calcuSalarySum ( );
                addRows ( "ANX006" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "ANX007" )
            {
                if ( workShopTime . startCenTime ( row ,e . Value ,"ANX006" ,"ANX008" ,"ANX005" ) == false )
                {
                    row [ "ANX007" ] = DBNull . Value;
                    row [ "ANX017" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                }
                //calcuTSumByT ( );
                //calcuBTSay ( );
                calcuSalaryForSub ( );
                calcuSalarySum ( );
                addRows ( "ANX007" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "ANX008" )
            {
                if ( workShopTime . endCenTime ( row ,e . Value ,"ANX007" ,"ANX005" ,"ANX006" ) == false )
                {
                    row [ "ANX008" ] = DBNull . Value;
                    row [ "ANX017" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                }
                //calcuTSumByT ( );
                //calcuBTSay ( );
                calcuSalaryForSub ( );
                calcuSalarySum ( );
                addRows ( "ANX008" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "ANX009" )
            {
                selectIndex = bandedGridView1 . FocusedRowHandle;
                if ( selectIndex < 0 )
                    return;

                anx009Result = bandedGridView1 . GetDataRow ( selectIndex ) [ "ANX009" ] . ToString ( );
                if ( string . IsNullOrEmpty ( anx009Result ) )
                    _body . ANX009 = 0;
                else
                {
                    if ( !string . IsNullOrEmpty ( anx009Result ) && decimal . TryParse ( anx009Result ,out outRsult ) == false )
                        return;
                    else
                        _body . ANX009 = outRsult;
                }
                for ( int i = selectIndex ; i < tableView . Rows . Count ; i++ )
                {
                    row = tableView . Rows [ i ];
                    if ( row [ "ANX007" ] != null && row [ "ANX007" ] . ToString ( ) != string . Empty )
                    {
                        row . BeginEdit ( );
                        row [ "ANX009" ] = _body . ANX009;
                        row . EndEdit ( );
                    }
                    if ( i == selectIndex && ( row [ "ANX007" ] == null || row [ "ANX007" ] . ToString ( ) == string . Empty ) )
                    {
                        row . BeginEdit ( );
                        row [ "ANX009" ] = DBNull . Value;
                        row . EndEdit ( );
                    }
                }
                gridControl1 . Refresh ( );
                if ( string . IsNullOrEmpty ( txtANW014 . Text ) )
                    return;
                calcuSalaryForTime ( );
                setCalcuSalary ( );
            }
            else if ( e . Column . FieldName == "ANX010" )
            {
                calcuSalaryForSub ( );
                setCalcuSalary ( );
            }
            else if ( e . Column . FieldName == "ANX002" || e . Column . FieldName == "ANX003" || e . Column . FieldName == "ANX004" || e . Column . FieldName == "ANX009" || e . Column . FieldName == "ANX013" )
            {
                editTime ( );
                if ( row [ "ANX011" ] == null || row [ "ANX011" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANX011" ] = "在职";
                }
                calcuTsumTime ( );
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "ANN008" || e . Column . FieldName == "ANN009" )
            {
                calcuPriceSum ( );
            }
        }
        void editTime ( )
        {
            if ( txtANW014 . Text . Equals ( "计件" ) )
            {
                if ( row [ "ANX005" ] == null || row [ "ANX005" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANX005" ] = dtStart;
                }
                if ( row [ "ANX006" ] == null || row [ "ANX006" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANX006" ] = dtEnd;
                }
            }
            else if ( txtANW014 . Text . Equals ( "计时" ) )
            {
                if ( row [ "ANX007" ] == null || row [ "ANX007" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANX007" ] = dtStart;
                }
                if ( row [ "ANX008" ] == null || row [ "ANX008" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANX008" ] = dtEnd;
                }
            }
        }
        private void txtu1_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu0_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu2_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu4_TextChanged ( object sender ,EventArgs e )
        {
            if ( "计件" . Equals ( txtANW014 . Text ) )
            {
                if ( txtu4 . Text == string . Empty )
                    txtu3 . Text = 0 . ToString ( );
                else
                    txtu3 . Text = ( Convert . ToDecimal ( txtu4 . Text ) * Convert . ToDecimal ( 0.05 ) ) . ToString ( "0.######" );
            }
            else if ( "计时" . Equals ( txtANW014 . Text ) )
                txtu3 . Text = 0 . ToString ( );

            setCalcuSalary ( );
        }
        private void txtu5_TextChanged ( object sender ,EventArgs e )
        {
            setCalcuSalary ( );
        }
        private void btnEdit_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );

            FormAssOrder form = new FormAssOrder ( tablePInfo );
            if ( form . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow r = form . getRow;
                _bodyOne . ANN002 = r [ "RAA001" ] . ToString ( );
                _bodyOne . ANN003 = r [ "RAA015" ] . ToString ( );
                _bodyOne . ANN004 = r [ "DEA002" ] . ToString ( );
                _bodyOne . ANN005 = r [ "DEA057" ] . ToString ( );
                _bodyOne . ANN006 = r [ "DEA003" ] . ToString ( );
                _bodyOne . ANN007 = string . IsNullOrEmpty ( r [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( r [ "RAA018" ] . ToString ( ) );
                _bodyOne . ANN008 = string . IsNullOrEmpty ( r [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "DEA050" ] . ToString ( ) );

                if ( row == null )
                {
                    row = tableViewTwo . NewRow ( );
                    rowEdit ( row );
                    tableViewTwo . Rows . Add ( row );
                }
                else
                    rowEdit ( row );

                editOtherSur ( _bodyOne . ANN002 ,_bodyOne . ANN003 );
            }
        }
        void rowEdit ( DataRow row )
        {
            row [ "ANN002" ] = _bodyOne . ANN002;
            row [ "ANN003" ] = _bodyOne . ANN003;
            row [ "ANN004" ] = _bodyOne . ANN004;
            row [ "ANN005" ] = _bodyOne . ANN005;
            row [ "ANN006" ] = _bodyOne . ANN006;
            row [ "ANN007" ] = _bodyOne . ANN007;
            row [ "ANN008" ] = _bodyOne . ANN008;
        }
        private void EditView_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANN002":
                if ( edit . EditValue == null )
                    return;
                row = tablePInfo . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyOne . ANN010 = row [ "RAA008" ] . ToString ( );
                if ( string . IsNullOrEmpty ( _bodyOne . ANN010 ) )
                    return;
                _bodyOne . ANN002 = edit . EditValue . ToString ( );
                _bodyOne . ANN003 = row [ "RAA015" ] . ToString ( );
                _bodyOne . ANN004 = row [ "DEA002" ] . ToString ( );
                _bodyOne . ANN005 = row [ "DEA057" ] . ToString ( );
                _bodyOne . ANN006 = row [ "DEA003" ] . ToString ( );
                _bodyOne . ANN007 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
                _bodyOne . ANN008 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN002" ] ,_bodyOne . ANN002 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN003" ] ,_bodyOne . ANN003 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN004" ] ,_bodyOne . ANN004 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN005" ] ,_bodyOne . ANN005 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN006" ] ,_bodyOne . ANN006 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN007" ] ,_bodyOne . ANN007 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANN008" ] ,_bodyOne . ANN008 );
                
                editOtherSur ( _bodyOne . ANN002 ,_bodyOne . ANN003 );
                break;
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableView ,tableViewTwo );
            else
                result = _bll . Edit ( _header ,tableView ,idxDelete ,tableViewTwo ,idxDeleteOne );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功保存" );
                     ClassForMain.FormClosingState.formClost = true;
                    controlUnEnable ( );
                    if ( state . Equals ( "add" ) )
                        _header . ANW001 = txtANW001 . Text = LineProductMesBll . UserInfoMation . oddNum;
                    else
                        _header . ANW001 = txtANW001 . Text;

                    strWhere = "1=1";
                    strWhere += " AND ANX001='" + _header . ANW001 + "'";
                    tableView = _bll . getTableView ( strWhere );
                    gridControl1 . DataSource = tableView;

                    strWhere = "1=1";
                    strWhere += " AND ANN001='" + _header . ANW001 + "'";
                    tableViewTwo = _bll . getTableViewOne ( strWhere );
                    gridControl2 . DataSource = tableViewTwo;

                    _header . ANW020 = _header . ANW021 = false;
                    //_header = _bll . getHeader ( _header . ANW001 );
                    setValue ( );

                    setCalcuSalary ( );

                    editOtherSur ( string . Empty ,string . Empty );

                    saveTool ( );
                }
                else
                {
                    XtraMessageBox . Show ( "保存失败" );
                     ClassForMain.FormClosingState.formClost = false;
                }
            }
        }
        private void bandedGridView1_InitNewRow ( object sender ,DevExpress . XtraGrid . Views . Grid . InitNewRowEventArgs e )
        {
            DevExpress . XtraGrid . Views . Grid . GridView view = sender as DevExpress . XtraGrid . Views . Grid . GridView;
            view . SetRowCellValue ( e . RowHandle ,view . Columns [ "ANX011" ] ,"在职" );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if (  tableView == null || tableView . Rows . Count < 1  )
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
        private void txtANW023_TextChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtANW024_TextChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focusName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focusName );
        }
        private void bandedGridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "ANX017" )
            {
                if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDecimal ( e . CellValue ) >= 200 )
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                }
            }
        }
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U1" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void txtANW015_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( !string . IsNullOrEmpty ( txtANW015 . Text ) )
            {
                dtStart = Convert . ToDateTime ( txtANW015 . Text );
                dt = dtStart;
            }
            //开始
            updateBatchTime ( );
        }
        private void txtANW016_EditValueChanged ( object sender ,EventArgs e )
        {
            //结束
            updateBatchTime ( );
        }
        void updateBatchTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            if ( !string . IsNullOrEmpty ( txtANW015 . Text ) )
                dtStart = Convert . ToDateTime ( txtANW015 . Text );
            if ( !string . IsNullOrEmpty ( txtANW016 . Text ) )
                dtEnd = Convert . ToDateTime ( txtANW016 . Text );

            foreach ( DataRow row in tableView . Rows )
            {
                if ( txtANW014 . Text . Equals ( "计件" ) )
                {
                    row [ "ANX005" ] = dtStart;
                    row [ "ANX006" ] = dtEnd;
                    row [ "ANX007" ] = DBNull . Value;
                    row [ "ANX008" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                }
                else if ( txtANW014 . Text . Equals ( "计时" ) )
                {
                    row [ "ANX005" ] = DBNull . Value;
                    row [ "ANX006" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX007" ] = dtStart;
                    row [ "ANX008" ] = dtEnd;
                }
            }

            calcuSalaryForTime ( );
            calcuTsumTime ( );
        }
        private void btnUser_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = bandedGridView1 . GetFocusedDataRow ( );
            FormUserChoise form = new FormUserChoise ( tableUser );
            if ( form . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow ro = form . getRow;
                _body . ANX002 = ro [ "EMP001" ] . ToString ( );
                _body . ANX003 = ro [ "EMP002" ] . ToString ( );
                _body . ANX004 = ro [ "EMP007" ] . ToString ( );
                _body . ANX011 = "在职";
                _body . ANX014 = ro [ "DAA002" ] . ToString ( );

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
            row [ "ANX002" ] = _body . ANX002;
            row [ "ANX003" ] = _body . ANX003;
            row [ "ANX004" ] = _body . ANX004;
            row [ "ANX011" ] = _body . ANX011;
            row [ "ANX014" ] = _body . ANX014;
            if ( "检测" . Equals ( _body . ANX004 ) )
                row [ "ANX013" ] = _body . ANX004;
        }
        private void txtANW026_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtANW027_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        #endregion

        #region Method
        void InitData ( )
        {
            tablePInfo = _bll . getTablePInfo ( );
            //if ( txtANW002 . InvokeRequired )
            //{
            //    Action<string> actionPInfo = ( x ) =>
            //    {
            EditView . DataSource = tablePInfo;
            EditView . DisplayMember = "RAA001";
            EditView . ValueMember = "RAA001";
            //    };
            //    this . txtANW002 . Invoke ( actionPInfo ,string . Empty );
            //}
        }
        void getInitData ( )
        {
            tableWork = _bll . getDepart ( );
            txtANW011 . Properties . DataSource = tableWork;
            txtANW011 . Properties . DisplayMember = "DAA002";
            txtANW011 . Properties . ValueMember = "DAA001";

            tableUser = _bll . getUserInfo ( );
            Edit6 . DataSource = tableUser;
            Edit6 . DisplayMember = "ANX002";
            Edit6 . ValueMember = "ANX002";
        }
        void controlUnEnable ( )
        {
            txtANW014 . ReadOnly = txtANW011 . ReadOnly = txtANW013 . ReadOnly = txtANW017 . ReadOnly = txtANW023 . ReadOnly = txtANW024 . ReadOnly = txtANW015 . ReadOnly = txtANW016 . ReadOnly =txtANW026.ReadOnly=txtANW027.ReadOnly= true;
            bandedGridView1 . OptionsBehavior . Editable = false;
            gridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtANW014 . ReadOnly = txtANW011 . ReadOnly = txtANW013 . ReadOnly = txtANW017 . ReadOnly = txtANW023 . ReadOnly = txtANW024 . ReadOnly = txtANW015 . ReadOnly = txtANW016 . ReadOnly = txtANW026 . ReadOnly = txtANW027 . ReadOnly = false;
            bandedGridView1 . OptionsBehavior . Editable = true;
            gridView1 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtANW001 . Text = txtANW011 . Text = txtANW013 . Text = txtANW014 . Text = txtANW017 . Text = txtu0 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text =/* txtANW019 . Text =*/ txtu0 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtu5 . Text = txtANW022 . Text = txtANW023 . Text = txtANW024 . Text = txtANW015 . Text = txtANW016 . Text = txtANW026 . Text = txtANW027 . Text = txtANW025.Text= string . Empty;
            gridControl1 . DataSource = null;
            gridControl2 . DataSource = null;
            layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }
        /// <summary>
        /// 计件工资
        /// </summary>
        void calcuPriceSum ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( "计件" . Equals ( txtANW014 . Text ) )
            {
                if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                    return;
                decimal result = 0M;
                foreach ( DataRow row in tableViewTwo . Rows )
                {
                    _bodyOne . ANN008 = string . IsNullOrEmpty ( row [ "ANN008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANN008" ] . ToString ( ) );
                    _bodyOne . ANN009 = string . IsNullOrEmpty ( row [ "ANN009" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANN009" ] . ToString ( ) );
                    result += Convert . ToDecimal ( _bodyOne . ANN008 * _bodyOne . ANN009 );
                }
                txtu1 . Text = result . ToString ( "0.##" );
            }
            else if ( "计时" . Equals ( txtANW014 . Text ) )
                txtu1 . Text = 0 . ToString ( );
        }
        /// <summary>
        /// 计时工资
        /// </summary>
        void calcuSalaryForTime ( )
        {
            //ANX016*ANX009

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            decimal? totalSalary = 0M;
            foreach ( DataRow row in tableView . Rows )
            {
                _body . ANX016 = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] );
                _body . ANX009 = string . IsNullOrEmpty ( row [ "ANX009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX009" ] );
                totalSalary += _body . ANX009 * _body . ANX016;
            }
            txtu0 . Text = Convert . ToDecimal ( totalSalary ) . ToString ( "0.##" );
        }
        /// <summary>
        /// 补贴工资
        /// </summary>
        void calcuSalaryForSub ( )
        {
            parame = tableView . Compute ( "SUM(ANX010)" ,null ) . ToString ( );
            txtu2 . Text = string . IsNullOrEmpty ( parame ) == true ? 0 . ToString ( ) : Convert . ToDecimal ( parame ) . ToString ( "0.######" );
        }
        /// <summary>
        /// 总工资
        /// </summary>
        void calcuSalarySum ( )
        {
            txtu4 . Text = ( string . IsNullOrEmpty ( txtu0 . Text ) == true ? 0 : Convert . ToDecimal ( txtu0 . Text ) ) . ToString ( "0.######" );

            if ( "计件" . Equals ( txtANW014 . Text ) )
            {
                txtu4 . Text = ( ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) ) + ( string . IsNullOrEmpty ( txtu1 . Text ) == true ? 0 : Convert . ToDecimal ( txtu1 . Text ) ) + ( string . IsNullOrEmpty ( txtANW026 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW026 . Text ) ) * ( string . IsNullOrEmpty ( txtANW027 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW027 . Text ) ) ) . ToString ( "0.######" );
            }
        }
        /// <summary>
        /// 总工时
        /// </summary>
        void calcuTSumByT ( )
        {
            if ( string . IsNullOrEmpty ( txtANW014 . Text ) )
                return;
            //int calcuT = 0;
            //string dtStart, dtEnd;
            if ( tableView == null )
                return;
            //for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            //{
            //    if ( tableView . Rows [ i ] [ "ANX011" ] . ToString ( ) . Equals ( "请假" ) )
            //        continue;
            //    dtStart = tableView . Rows [ i ] [ "ANX005" ] . ToString ( );
            //    dtEnd = tableView . Rows [ i ] [ "ANX006" ] . ToString ( );
            //    if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
            //    {
            //        calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
            //    }
            //    dtStart = tableView . Rows [ i ] [ "ANX007" ] . ToString ( );
            //    dtEnd = tableView . Rows [ i ] [ "ANX008" ] . ToString ( );
            //    if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
            //    {
            //        calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
            //    }
            //}
            //txtu5 . Text = calcuT . ToString ( );
            txtu5 . Text = string . IsNullOrEmpty ( U3 . SummaryItem . SummaryValue . ToString ( ) ) == true ? 0 . ToString ( ) : Convert . ToDecimal ( U3 . SummaryItem . SummaryValue ) . ToString ( "0.######" );
        }
        /// <summary>
        /// 补贴
        /// </summary>
        void calcuBTSay ( )
        {
            //如果计件或计时的开工时间和完工时间都没有了，那么补贴工资就没有   需要人员信息带出入职时间
            if ( tableUser == null || tableUser . Rows . Count < 1 )
                return;
            if ( row == null )
                return;
            if ( tableUser . Select ( "ANX002='" + row [ "ANX002" ] . ToString ( ) + "'" ) . Length < 1 )
                return;
            if ( string . IsNullOrEmpty ( tableUser . Select ( "ANX002='" + row [ "ANX002" ] . ToString ( ) + "'" ) [ 0 ] [ "EMP023" ] . ToString ( ) ) )
                return;
            if ( ( row [ "ANX005" ] == null || row [ "ANX006" ] == null ) && ( row [ "ANX007" ] == null || row [ "ANX008" ] == null ) )
            {
                row [ "ANX010" ] = DBNull . Value;
                return;
            }
            if ( ( string . IsNullOrEmpty ( row [ "ANX005" ] . ToString ( ) ) || string . IsNullOrEmpty ( row [ "ANX006" ] . ToString ( ) ) ) && ( string . IsNullOrEmpty ( row [ "ANX007" ] . ToString ( ) ) || string . IsNullOrEmpty ( row [ "ANX008" ] . ToString ( ) ) ) )
            {
                row [ "ANX010" ] = DBNull . Value;
                return;
            }
            DateTime dtStartUpper, dtTime;
            dtTime = Convert . ToDateTime ( tableUser . Select ( "ANX002='" + row [ "ANX002" ] . ToString ( ) + "'" ) [ 0 ] [ "EMP023" ] );

            int days;
            decimal totalTime;
            if ( string . IsNullOrEmpty ( txtANW022 . Text ) )
                return;
            dtStartUpper = Convert . ToDateTime ( txtANW022 . Text );
            days = dtStartUpper . Subtract ( dtTime ) . Days;
            if ( days <= 7 && days >= 0 )
            {
                totalTime = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] ) + ( string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] ) );
                row [ "ANX010" ] = totalTime * 7;
            }

            calcuSalaryForSub ( );
            calcuSalarySum ( );
        }
        /// <summary>
        /// 个人工资
        /// </summary>
        void setCalcuSalary ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            decimal calcuT = 0, dtStart = 0, dtEnd = 0, resultSalary = 0, subWage = 0;
            if ( "计件" . Equals ( txtANW014 . Text ) )
            {
                if ( string . IsNullOrEmpty ( txtu5 . Text ) )
                    return;
                if ( Convert . ToDecimal ( txtu5 . Text ) == 0 )
                    return;

                resultSalary = ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) - ( string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text ) ) );


                object qj = tableView . Compute ( "sum(ANX018)" ,"ANX011='请假'" );
                if ( qj != DBNull . Value )
                    resultSalary -= Convert . ToDecimal ( qj );

                resultSalary = resultSalary / Convert . ToDecimal ( txtu5 . Text );

                if ( !string . IsNullOrEmpty ( txtANW014 . Text ) /*&& txtANW014 . Text . Equals ( "计件" )*/ )
                {
                    for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
                    {
                        DataRow row = tableView . Rows [ i ];
                        if ( row == null )
                            continue;
                        if ( string . IsNullOrEmpty ( row [ "ANX011" ] . ToString ( ) ) || row [ "ANX011" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANX011" ] . ToString ( ) . Equals ( "未上班" ) )
                        {
                            row [ "ANX015" ] = 0;
                            row [ "ANX016" ] = 0;
                            row [ "ANX017" ] = 0;
                            continue;
                        }
                        else if ( !row [ "ANX011" ] . ToString ( ) . Equals ( "请假" ) )
                        {
                            calcuT = 0;
                            row = tableView . Rows [ i ];
                            dtStart = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] . ToString ( ) );
                            dtEnd = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] . ToString ( ) );
                            calcuT += dtEnd + dtStart;
                            subWage = string . IsNullOrEmpty ( row [ "ANX010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX010" ] . ToString ( ) );
                            row [ "ANX017" ] = ( calcuT * resultSalary + subWage ) . ToString ( "0.##" );
                        }
                        else if ( row [ "ANX011" ] . ToString ( ) . Equals ( "请假" ) )
                        {
                            row [ "ANX017" ] = ( string . IsNullOrEmpty ( row [ "ANX018" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX018" ] . ToString ( ) ) ) . ToString ( "0.##" );
                        }
                    }
                }
            }
            else if ( "计时" . Equals ( txtANW014 . Text ) )
            {
                for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
                {
                    DataRow row = tableView . Rows [ i ];
                    if ( row == null )
                        continue;
                    if ( string . IsNullOrEmpty ( row [ "ANX011" ] . ToString ( ) ) || row [ "ANX011" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANX011" ] . ToString ( ) . Equals ( "未上班" ) )
                    {
                        row [ "ANX015" ] = 0;
                        row [ "ANX016" ] = 0;
                        row [ "ANX017" ] = 0;
                        continue;
                    }
                    else if ( !row [ "ANX011" ] . ToString ( ) . Equals ( "请假" ) )
                    {
                        calcuT = 0;
                        row = tableView . Rows [ i ];
                        dtStart = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] . ToString ( ) );
                        dtEnd = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] . ToString ( ) );
                        calcuT += dtEnd + dtStart;
                        resultSalary = string . IsNullOrEmpty ( row [ "ANX009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX009" ] . ToString ( ) );
                        subWage = string . IsNullOrEmpty ( row [ "ANX010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX010" ] . ToString ( ) );
                        row [ "ANX017" ] = ( calcuT * resultSalary + subWage ) . ToString ( "0.##" );
                    }
                    else if ( row [ "ANX011" ] . ToString ( ) . Equals ( "请假" ) )
                    {
                        row [ "ANX017" ] = ( string . IsNullOrEmpty ( row [ "ANX018" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX018" ] . ToString ( ) ) ) . ToString ( "0.##" );
                    }
                }
            }
        }
        void setValue ( )
        {
            txtANW001 . Text = _header . ANW001;
            txtANW014 . EditValue = _header . ANW014;
            txtANW011 . EditValue = _header . ANW010;
            txtANW013 . EditValue = _header . ANW012;
            //txtANW015 . EditValue = _header . ANW015;
            //txtANW016 . EditValue = _header . ANW016;
            //txtANW002 . EditValue = _header . ANW002;
            //txtANW003 . Text = _header . ANW003;
            //txtANW004 . Text = _header . ANW004;
            //txtANW005 . Text = _header . ANW005;
            //txtANW006 . Text = _header . ANW006 . ToString ( );
            //txtANW007 . Text = _header . ANW007;
            //txtANW008 . Text = _header . ANW008 . ToString ( );
            //txtANW009 . Text = _header . ANW009 . ToString ( );
            txtANW017 . Text = _header . ANW017;
            //txtANW018 . Text = _header . ANW018;
            //txtANW019 . Text = _header . ANW019;
            txtANW022 . Text = Convert . ToDateTime ( _header . ANW022 ) . ToString ( "yyyy-MM-dd" );
            txtANW023 . Text = _header . ANW023 . ToString ( );
            txtANW024 . Text = _header . ANW024 . ToString ( );
            txtANW015 . Text = _header . ANW015 . ToString ( );
            txtANW016 . Text = _header . ANW016 . ToString ( );

            dtStart = Convert . ToDateTime ( _header . ANW015 );
            dtEnd = Convert . ToDateTime ( _header . ANW016 );

            txtANW025 . Text = _header . ANW025;
            txtANW026 . Text = _header . ANW026 . ToString ( );
            txtANW027 . Text = _header . ANW027 . ToString ( );

            layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPic ( pictureEdit1 ,"反" );
            if ( _header . ANW020 )
            {
                layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicZ ( pictureEdit1 ,"审核" );
                examineTool ( "审核" );
            }
            else
            {
                examineTool ( "反审核" );
            }
            if ( _header . ANW021 )
            {
                layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicZ ( pictureEdit1 ,"注销" );
                cancelltionTool ( "注销" );
            }
            else
            {
                cancelltionTool ( "反注销" );
            }
        }
        bool getValue ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtANW014 . Text ) )
            {
                XtraMessageBox . Show ( "请选择工资类型" );
                return false;
            }
           
            if ( string . IsNullOrEmpty ( txtANW011 . Text ) )
            {
                XtraMessageBox . Show ( "请选择生产车间" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtANW013 . Text ) )
            {
                XtraMessageBox . Show ( "请选择班组" );
                return false;
            }
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请填写考勤人员信息" );
                return false;
            }

            bandedGridView1 . ClearColumnErrors ( );
            foreach ( DataRow row in tableView . Rows )
            {
                if ( row [ "ANX011" ] == null || row [ "ANX011" ] . ToString ( ) == string . Empty )
                {
                    bandedGridView1 . SetColumnError ( ANX011 ,"不可为空" );
                    result = false;
                    break;
                }
                if ( row [ "ANX002" ] == null || row [ "ANX002" ] . ToString ( ) == string . Empty )
                {
                    bandedGridView1 . SetColumnError ( ANX002 ,"不可为空" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "ANX002" )
                        } into m
                        select new
                        {
                            anx002 = m . Key . p1 ,
                            count = m . Count ( )
                        };
            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . anx002 + "重复,请核实" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            _header . ANW002 = workShopTime . checkUserForOtherWork ( txtANW022 . Text ,tableView ,LineProductMesBll . ObtainInfo . codeOne ,txtANW001 . Text );
            if ( !string . IsNullOrEmpty ( _header . ANW002 ) )
            {
                XtraMessageBox . Show ( _header . ANW002 ,"提示" );
                return false;
            }

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( "计件" . Equals ( txtANW014 . Text ) )
            {
                if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                {
                    XtraMessageBox . Show ( "请选择来源工单信息" );
                    return false;
                }
            }
            else if ( "计时" . Equals ( txtANW014 . Text ) && ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 ) )
            {
                if ( XtraMessageBox . Show ( "是否选择来源工单?" ,"提示" ,MessageBoxButtons . YesNo ) == DialogResult . Yes )
                    return false;
            }

            if ( string . IsNullOrEmpty ( txtANW015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择开始时间" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtANW016 . Text ) )
            {
                XtraMessageBox . Show ( "请选择结束时间" );
                return false;
            }

            decimal outResult = 0M;
            if ( !string . IsNullOrEmpty ( txtANW026 . Text ) && decimal . TryParse ( txtANW026 . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "补贴工时为数字" );
                return false;
            }
            _header . ANW026 = outResult;
            outResult = 0M;
            if ( !string . IsNullOrEmpty ( txtANW027 . Text ) && decimal . TryParse ( txtANW027 . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "补贴单价为数字" );
                return false;
            }
            if ( outResult < 0 || outResult > 20 )
            {
                XtraMessageBox . Show ( "补贴单价在1-20之内" );
                return false;
            }
            _header . ANW027 = outResult;

            _header . ANW010 = txtANW011 . EditValue . ToString ( );
            _header . ANW011 = txtANW011 . Text;
            _header . ANW012 = txtANW013 . EditValue . ToString ( );
            _header . ANW013 = txtANW013 . Text;
            _header . ANW014 = txtANW014 . Text;
            _header . ANW017 = txtANW017 . Text;
            //_header . ANW018 = txtANW018 . Text;
            _header . ANW018 = string . Empty;
            _header . ANW019 = /*txtANW019 . Text;*/string . Empty;
            _header . ANW022 = Convert . ToDateTime ( txtANW022 . Text );
            _header . ANW023 = string . IsNullOrEmpty ( txtANW023 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW023 . Text );
            _header . ANW024 = string . IsNullOrEmpty ( txtANW024 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW024 . Text );
            _header . ANW015 = Convert . ToDateTime ( txtANW015 . Text );
            _header . ANW016 = Convert . ToDateTime ( txtANW016 . Text );

            return true;
        }
        void printOrExport ( )
        {
            talePrintOne = _bll . getTablePrintOne ( txtANW001 . Text );
            talePrintOne . TableName = "TableOne";
            talePrintTwo = _bll . getTablePrintTwo ( txtANW001 . Text );
            talePrintTwo . TableName = "TableTwo";
        }
        void printOrExportOne ( )
        {
            tablePrintTre = _bll . getTablePrintTre ( txtANW001 . Text );
            tablePrintTre . TableName = "TableOne";
            tablePrintFor = _bll . getTablePrintFor ( txtANW001 . Text );
            tablePrintFor . TableName = "TableTwo";
            tablePrintFiv = _bll . getTablePrintFiv ( txtANW001 . Text );
            tablePrintFiv . TableName = "TableTre";
        }
        void addRows ( string column ,int selectIdx ,object value )
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
                        if ( txtANW014 . Text . Equals ( "计件" ) )
                        {
                            if ( column . Equals ( "ANX005" ) )
                            {
                                if ( workShopTime . startTime ( row ,/*row [ "ANX005" ]*/value ,"ANX006" ,"ANX007" ,"ANX008" ) )
                                {
                                    ro = tableView . Rows [ i /*+ 1*/ ];
                                    if ( ro [ "ANX005" ] == null || ro [ "ANX005" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "ANX005" ] = /*row [ "ANX005" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "ANX006" ) )
                            {
                                if ( workShopTime . endTime ( row ,/*row [ "ANX006" ]*/value  ,"ANX005" ,"ANX007" ,"ANX008" ) )
                                {
                                    ro = tableView . Rows [ i /*+ 1*/ ];
                                    if ( ro [ "ANX006" ] == null || ro [ "ANX006" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "ANX006" ] =/* row [ "ANX006" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ( column . Equals ( "ANX007" ) )
                            {
                                if ( workShopTime . startCenTime ( row ,/*row [ "ANX007" ]*/value ,"ANX006" ,"ANX008" ,"ANX005" ) )
                                {
                                    ro = tableView . Rows [ i /*+ 1*/ ];
                                    if ( ro [ "ANX007" ] == null || ro [ "ANX007" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "ANX007" ] = /*row [ "ANX007" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "ANX008" ) )
                            {
                                if ( workShopTime . endCenTime ( row ,/*row [ "ANX008" ]*/value ,"ANX007" ,"ANX005" ,"ANX006" ) )
                                {
                                    ro = tableView . Rows [ i /*+ 1*/ ];
                                    if ( ro [ "ANX008" ] == null || ro [ "ANX008" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "ANX008" ] = /*row [ "ANX008" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
            calcuTsumTime ( );
        }
        /// <summary>
        /// 工时个人
        /// </summary>
        void calcuTsumTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            decimal anw023 = txtANW023 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANW023 . Text ) ;
            decimal anw024 = txtANW024 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANW024 . Text ) ;

            DateTime dtOne, dtTwo;
            decimal u0 = 0M, u1 = 0M;
            foreach ( DataRow row in tableView . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "ANX011" ] . ToString ( ) ) || row [ "ANX011" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANX011" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "ANX015" ] = 0;
                    row [ "ANX016" ] = 0;
                    row [ "ANX017" ] = 0;
                    continue;
                }

                if ( !string . IsNullOrEmpty ( row [ "ANX005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANX006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANX005" ] );
                    dtTwo = Convert . ToDateTime ( row [ "ANX006" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );

                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( anw023 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( anw023 ) - Convert . ToDecimal ( anw024 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( anw024 );

                    row [ "ANX015" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    if ( !"请假" . Equals ( row [ "ANX011" ] . ToString ( ) ) )
                        u1 += u0;
                }else
                    row [ "ANX015" ] = 0;
                u0 = 0M;
                if ( !string . IsNullOrEmpty ( row [ "ANX007" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANX008" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANX007" ] );
                    dtTwo = Convert . ToDateTime ( row [ "ANX008" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );
                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( anw023 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30 */)
                            u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( anw023 ) - Convert . ToDecimal ( anw024 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( anw024 );

                    row [ "ANX016" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );

                    if ( !"请假" . Equals ( row [ "ANX011" ] . ToString ( ) ) )
                        u1 += u0;
                }
                else
                    row [ "ANX016" ] = 0;
            }

            //calcuTSumByT ( );
            txtu5 . Text = u1 . ToString ( "0.######" );
            //addTotalTime ( );
        }
        void addTotalTime ( )
        {
            txtu5 . Text = ( ANX015 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( ANX015 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) + ( ANX016 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( ANX016 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) ) ) . ToString ( "0.#" );
        }
        void editOtherSur ( string orderNum ,string proNum )
        {
            _bodyOne . ANN001 = txtANW001 . Text;
            _bodyOne . ANN002 = orderNum;
            _bodyOne . ANN003 = proNum;

            if ( _bodyOne . ANN002 == string . Empty || _bodyOne . ANN003 == string . Empty )
            {
                if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
                {
                    foreach ( DataRow row in tableViewTwo . Rows )
                    {
                        _bodyOne . ANN002 = row [ "ANN002" ] . ToString ( );
                        _bodyOne . ANN003 = row [ "ANN003" ] . ToString ( );
                        tableOtherSur = _bll . getTableOtherSur ( _bodyOne . ANN002 ,_bodyOne . ANN003 ,_bodyOne . ANN001 );
                        if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                        {
                            row [ "U1" ] = tableOtherSur . Rows [ 0 ] [ "LEH" ];
                        }
                        else
                        {
                            row [ "U1" ] = row [ "ANN007" ];
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne . ANN002 ,_bodyOne . ANN003 ,_bodyOne . ANN001 );
                if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewTwo . Select ( "ANN002='" + _bodyOne . ANN002 + "' AND ANN003='" + _bodyOne . ANN003 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U1" ] = tableOtherSur . Select ( "ANN002='" + row [ "ANN002" ] + "' AND ANN003='" + row [ "ANN003" ] + "'" ) [ 0 ] [ "LEH" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U1" ] = row [ "ANN007" ];
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










