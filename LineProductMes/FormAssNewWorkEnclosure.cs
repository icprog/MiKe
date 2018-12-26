using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Linq;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class FormAssNewWorkEnclosure :FormChild
    {
        LineProductMesEntityu.AssNewWorkEnclosureHeaderEntity _header=null;
        LineProductMesEntityu.AssNewWorkEnclosureBodyOneEntity _bodyOne=null;
        LineProductMesEntityu.AssNewWorkEnclosureBodyTwoEntity _bodyTwo=null;
        LineProductMesBll.Bll.AssNewWorkEnclosureBll _bll=null;
        DataTable tableWork,tableViewOne,tableViewTwo,tablePInfo,tableDepart,tablePersonInfo,tableEInfo,tablePrintOne,tablePrintTwo,tableUser,tableOtherSur,tablePrintTre,tablePrintFor,tablePrintFiv;
        DataRow row;
        string state=string.Empty,strWhere=string.Empty,focuseName=string.Empty;
        bool result=false;
        int selectIdx;
        List<string> idxOne;
        List<string> idxTwo;
        DateTime dt,dtStart,dtEnd;

        public FormAssNewWorkEnclosure ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkEnclosureBll ( );
            _header = new LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity ( );
            _bodyOne = new LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity ( );
            _bodyTwo = new LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity ( );
            idxOne = new List<string> ( );
            idxTwo = new List<string> ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,bandedGridView1 ,View1 ,View2 ,View3 ,View4 ,View5 ,View6 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,bandedGridView1 ,View1 ,View2 ,View3 ,View4 ,View5 ,View6 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Edit5 . VistaEditTime = Edit6 . VistaEditTime = Edit9 . VistaEditTime = Edit10 . VistaEditTime = txtANT014 . Properties . VistaEditTime = txtANT015 . Properties . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;

            controlUnEnable ( );
            controlClear ( );
            wait . Hide ( );

            dt = LineProductMesBll . UserInfoMation . sysTime;

            dtStart = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 08:00" ) );
            dtEnd = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:00" ) );

            InitData ( );
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . AssNewWorkEnclosureQuery from = new ChildForm . AssNewWorkEnclosureQuery ( );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                _header . ANT001 = from . getOddNum;

                _header = _bll . getModel ( _header . ANT001 );
                if ( _header == null )
                    return 0;

                setValue ( );

                tableViewOne = _bll . getTableViewOne ( " ANU001='" + _header . ANT001 + "'" );
                gridControl1 . DataSource = tableViewOne;
                tableViewTwo = _bll . getTableViewTwo ( " ANV001='" + _header . ANT001 + "'" );
                gridControl2 . DataSource = tableViewTwo;

                calcuSumTime ( );
                //addTotalTime ( );
                //calcuSumPrice ( );
                //calcuSumNum ( );
                //calcuPrice ( );

                editOtherSur ( string . Empty ,string . Empty );

                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            txtANT001 . Text = _bll . getCode ( );
            addTool ( );
            state = "add";

            tableViewOne = _bll . getTableViewOne ( "1=2" );
            gridControl1 . DataSource = tableViewOne;
            tableViewTwo = _bll . getTableViewTwo ( "1=2" );
            gridControl2 . DataSource = tableViewTwo;

            txtANT003 . EditValue = "0507";
            txtANT008 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );
            txtANT011 . Text = "计时";

            txtANT014 . Text = dtStart . ToString ( );
            txtANT015 . Text = dtEnd . ToString ( );

            layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            state = "edit";
            editTool ( );

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtANT001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择需要删除的内容" );
                return 0;
            }
            result = _bll . Delete ( txtANT001 . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                deleteTool ( );
                controlClear ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );


            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getValeu ( ) == false )
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
            cancelTool ( state );
            if ( state . Equals ( "add" ) )
                controlClear ( );
            controlUnEnable ( );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( string . IsNullOrEmpty ( txtANT001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }

            _header . ANT001 = txtANT001 . Text;
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . ANT006 = true;
            else
                _header . ANT006 = false;
            result = _bll . Examine ( _header );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPic ( pictureEdit1 ,"审 核" );
                }
                else
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPic ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtANT001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            _header . ANT001 = txtANT001 . Text;
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . ANT007 = true;
            else
                _header . ANT007 = false;
            result = _bll . Cancella ( _header  );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPic ( pictureEdit1 ,"注 销" );
                }
                else
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
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
            Print ( new DataTable [ ] { tablePrintTre ,tablePrintFor,tablePrintFiv } ,"组装附件报工单.frx" );

            return base . PrintReport ( );
        }
        protected override int ExportReport ( )
        {
            printOrExportOne ( );
            Export ( new DataTable [ ] { tablePrintTre ,tablePrintFor ,tablePrintFiv } ,"组装附件报工单.frx" );

            return base . ExportReport ( );
        }
        #endregion

        #region Event
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void bandedGridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableViewOne ,tableViewTwo );
            else
                result = _bll . Edit ( _header ,tableViewOne ,tableViewTwo ,idxOne ,idxTwo );
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
                        _header . ANT001 = txtANT001 . Text = LineProductMesBll . UserInfoMation . oddNum;
                    else
                        _header . ANT001 = txtANT001 . Text;

                    setValue ( );

                    tableViewOne = _bll . getTableViewOne ( " ANU001='" + _header . ANT001 + "'" );
                    gridControl1 . DataSource = tableViewOne;
                    tableViewTwo = _bll . getTableViewTwo ( " ANV001='" + _header . ANT001 + "'" );
                    gridControl2 . DataSource = tableViewTwo;

                    //addTotalTime ( );
                    //calcuSumPrice ( );
                    //calcuPrice ( );
                    //calcuSumNum ( );
                    calcuSumTime ( );

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
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always && e . KeyChar == ( char ) Keys . Enter )
            {
                row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyOne . idx > 0 && !idxOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                    idxOne . Add ( _bodyOne . idx . ToString ( ) );
                tableViewOne . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
            }
        }
        private void gridControl2_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always && e . KeyChar == ( char ) Keys . Enter )
            {
                row = bandedGridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyTwo . idx > 0 && !idxTwo . Contains ( _bodyTwo . idx . ToString ( ) ) )
                    idxTwo . Add ( _bodyTwo . idx . ToString ( ) );
                tableViewTwo . Rows . Remove ( row );
                gridControl2 . RefreshDataSource ( );
            }
        }
        private void txtANT003_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANT003 . EditValue == null || txtANT003 . EditValue . ToString ( ) == string . Empty )
                return;
            tableUser = _bll . getDepart ( txtANT003 . EditValue . ToString ( ) );
            txtANT005 . Properties . DataSource = tableUser;
            txtANT005 . Properties . DisplayMember = "DAA002";
            txtANT005 . Properties . ValueMember = "DAA001";

            txtANT005 . EditValue = "050709";
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "ANU010" )
            {
                calcuSumTime ( );
                //calcuSumNum ( );
                //calcuSumPrice ( );
            }
            else if ( e . Column . FieldName == "ANU007" )
            {
                //calcuSumPrice ( );
                calcuSumTime ( );
            }
            //else if ( e . Column . FieldName == "ANU008" || e . Column . FieldName == "ANU009" )
            //{
            //    addPerson ( );
            //}
        }
        private void Edit7_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANU008":
                if ( edit . EditValue == null )
                    return;
                row = tableDepart . Select ( "DAA001='" + edit . EditValue . ToString ( ) + "'" ) [ 0 ];
                if ( row == null )
                    return;
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU009" ] ,row [ "DAA002" ] . ToString ( ) );
                break;
            }
        }
        private void Edit3_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "ANV002":
                if ( edit . EditValue == null )
                    return;
                row = tablePersonInfo . Select ( "EMP001='" + edit . EditValue . ToString ( ) + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyTwo . ANV003 = row [ "EMP002" ] . ToString ( );
                _bodyTwo . ANV004 = row [ "EMP007" ] . ToString ( );
                _bodyTwo . ANV008 = row [ "DAA002" ] . ToString ( );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANV003" ] ,_bodyTwo . ANV003 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANV004" ] ,_bodyTwo . ANV004 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANV007" ] ,"在职" );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "ANV008" ] ,_bodyTwo . ANV008 );
                break;
            }
        }
        private void Edit2_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANU002":
                if ( edit . EditValue == null )
                    return;
                if ( tableEInfo . Select ( "ANW001='" + edit . EditValue . ToString ( ) + "'" ) . Length < 1 )
                    return;
                row = tableEInfo . Select ( "ANW001='" + edit . EditValue . ToString ( ) + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyOne . ANU008 = row [ "ANW012" ] . ToString ( );
                _bodyOne . ANU009 = row [ "ANW013" ] . ToString ( );
                _bodyOne . ANU006 = row [ "ANW009" ] . ToString ( );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU008" ] ,_bodyOne . ANU008 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU009" ] ,_bodyOne . ANU009 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU010" ] ,_bodyOne . ANU006 );
                break;
            }
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            selectIdx = bandedGridView1 . FocusedRowHandle;
            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "ANV005" || e . Column . FieldName == "ANV006" )
            {
                if ( e . Column . FieldName == "ANV005" && workShopTime . startCenTime ( row ,e . Value ,"ANV014" ,"ANV006" ,"ANV013" ) == false )
                {
                    row [ "ANV005" ] = DBNull . Value;
                }
                if ( e . Column . FieldName == "ANV006" && workShopTime . endCenTime ( row ,e . Value ,"ANV005" ,"ANV013" ,"ANV014" ) == false )
                {
                    row [ "ANV006" ] = DBNull . Value;
                }
                addRow ( e . Column . FieldName ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "ANV013" || e . Column . FieldName == "ANV014" )
            {
                if ( e . Column . FieldName == "ANV013" && workShopTime . startTime ( row ,e . Value ,"ANV014" ,"ANV005" ,"ANV006" ) == false )
                {
                    row [ "ANV013" ] = DBNull . Value;
                }
                if ( e . Column . FieldName == "ANV014" && workShopTime . endTime ( row ,e . Value ,"ANV013" ,"ANV005" ,"ANV006" ) == false )
                {
                    row [ "ANV014" ] = DBNull . Value;
                }
                addRow ( e . Column . FieldName ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "ANV002" || e . Column . FieldName == "ANV003" || e . Column . FieldName == "ANV004" || e . Column . FieldName == "ANV008" )
            {
                if ( txtANT001 . Text . Equals ( "计时" ) )
                {
                    if ( row [ "ANV005" ] == null || row [ "ANV005" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANV005" ] = dtStart;
                    }
                    if ( row [ "ANV006" ] == null || row [ "ANV006" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANV006" ] = dtEnd;
                    }
                    row [ "ANV013" ] = DBNull . Value;
                    row [ "ANV014" ] = DBNull . Value;
                }
                else if ( txtANT001 . Text . Equals ( "计件" ) )
                {
                    if ( row [ "ANV013" ] == null || row [ "ANV013" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANV013" ] = dtStart;
                    }
                    if ( row [ "ANV014" ] == null || row [ "ANV014" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANV014" ] = dtEnd;
                    }
                    row [ "ANV005" ] = DBNull . Value;
                    row [ "ANV006" ] = DBNull . Value;
                }
                if ( row [ "ANV007" ] == null || row [ "ANV007" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANV007" ] = "在职";
                }
                calcuSumTime ( );
            }
            else if ( e . Column . FieldName == "ANV007" )
            {
                if ( row [ "ANV007" ] != null && row [ "ANV007" ] . ToString ( ) != string . Empty && ( row [ "ANV007" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANV007" ] . ToString ( ) . Equals ( "未上班" ) ) )
                {
                    row [ "ANV005" ] = DBNull . Value;
                    row [ "ANV006" ] = DBNull . Value;
                    row [ "ANV009" ] = DBNull . Value;
                    row [ "ANV013" ] = DBNull . Value;
                    row [ "ANV014" ] = DBNull . Value;
                    row [ "ANV015" ] = DBNull . Value;
                    row [ "ANV010" ] = DBNull . Value;
                }
                else if ( row [ "ANV007" ] . ToString ( ) . Equals ( "在职" ) )
                {
                    if ( txtANT001 . Text . Equals ( "计时" ) )
                    {
                        if ( row [ "ANV005" ] == null || row [ "ANV005" ] . ToString ( ) == string . Empty )
                        {
                            row [ "ANV005" ] = dtStart;
                        }
                        if ( row [ "ANV006" ] == null || row [ "ANV006" ] . ToString ( ) == string . Empty )
                        {
                            row [ "ANV006" ] = dtEnd;
                        }
                        row [ "ANV013" ] = DBNull . Value;
                        row [ "ANV014" ] = DBNull . Value;
                    }
                    else if ( txtANT001 . Text . Equals ( "计件" ) )
                    {
                        if ( row [ "ANV013" ] == null || row [ "ANV013" ] . ToString ( ) == string . Empty )
                        {
                            row [ "ANV013" ] = dtStart;
                        }
                        if ( row [ "ANV014" ] == null || row [ "ANV014" ] . ToString ( ) == string . Empty )
                        {
                            row [ "ANV014" ] = dtEnd;
                        }
                        row [ "ANV005" ] = DBNull . Value;
                        row [ "ANV006" ] = DBNull . Value;
                    }
                    calcuSumTime ( );
                }
            }
            else if ( e . Column . FieldName == "ANV016" )
            {
                int selectIndex = bandedGridView1 . FocusedRowHandle;
                if ( selectIndex < 0 )
                    return;

                decimal outRsult = 0;
                string anv016Result = bandedGridView1 . GetDataRow ( selectIndex ) [ "ANV016" ] . ToString ( );
                if ( string . IsNullOrEmpty ( anv016Result ) )
                    _bodyTwo . ANV016 = 0;
                else
                {
                    if ( !string . IsNullOrEmpty ( anv016Result ) && decimal . TryParse ( anv016Result ,out outRsult ) == false )
                        return;
                    else
                        _bodyTwo . ANV016 = outRsult;
                }
                for ( int i = selectIndex ; i < tableViewTwo . Rows . Count ; i++ )
                {
                    row = tableViewTwo . Rows [ i ];
                    if ( row [ "ANV009" ] != null && row [ "ANV009" ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ "ANV016" ] == null || row [ "ANV016" ] . ToString ( ) == string . Empty )
                        {
                            row . BeginEdit ( );
                            row [ "ANV016" ] = _bodyTwo . ANV016;
                            row . EndEdit ( );
                        }
                    }
                    if ( i == selectIndex && ( row [ "ANV009" ] == null || row [ "ANV009" ] . ToString ( ) == string . Empty ) )
                    {
                        row . BeginEdit ( );
                        row [ "ANV016" ] = DBNull . Value;
                        row . EndEdit ( );
                    }
                }
                gridControl2 . Refresh ( );
                calcuSumTime ( );
            }
        }
        private void Edit1_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANU003":
                if ( edit . EditValue == null )
                    return;
                row = tablePInfo . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( row [ "RAA008" ] == null || row [ "RAA008" ] . ToString ( ) == string . Empty )
                {
                    gridView1 . SetFocusedRowCellValue ( "ANU003" ,null );
                    return;
                }
                _bodyOne . ANU004 = row [ "RAA015" ] . ToString ( );
                _bodyOne . ANU005 = row [ "DEA002" ] . ToString ( );
                _bodyOne . ANU006 = row [ "ART003" ] . ToString ( );
                _bodyOne . ANU007 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( "ANU004" ,_bodyOne . ANU004 );
                gridView1 . SetFocusedRowCellValue ( "ANU005" ,_bodyOne . ANU005 );
                gridView1 . SetFocusedRowCellValue ( "ANU006" ,_bodyOne . ANU006 );
                gridView1 . SetFocusedRowCellValue ( "ANU007" ,_bodyOne . ANU007 );
                break;
            }
        }
        private void txtu0_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void txtu2_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void txtANT005_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANT003 . EditValue == null || txtANT003 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( txtANT005 . EditValue == null || txtANT005 . EditValue . ToString ( ) == string . Empty )
                return;

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo != null )
                tableViewTwo . Rows . Clear ( );

            DateTime dtNow = LineProductMesBll . UserInfoMation . sysTime;
            DataRow [ ] rows = tablePersonInfo . Select ( "EMP005='" + txtANT005 . EditValue + "'" );
            DataRow row;
            if ( tableViewTwo == null )
                return;
            if ( tableViewTwo . Rows . Count < 1 )
            {
                foreach ( DataRow ro in rows )
                {
                    if ( ro != null )
                    {
                        row = tableViewTwo . NewRow ( );
                        row [ "ANV002" ] = ro [ "EMP001" ];
                        row [ "ANV003" ] = ro [ "EMP002" ];
                        row [ "ANV004" ] = ro [ "EMP007" ];
                        row [ "ANV008" ] = ro [ "DAA002" ];
                        if ( "计时" . Equals ( txtANT011 . Text ) )
                        {
                            row [ "ANV005" ] = dtStart;
                            row [ "ANV006" ] = dtEnd;
                        }
                        else if ( "计件" . Equals ( txtANT011 . Text ) )
                        {
                            row [ "ANV013" ] = dtStart;
                            row [ "ANV014" ] = dtEnd;
                        }
                        row [ "ANV007" ] = "在职";
                        tableViewTwo . Rows . Add ( row );
                        gridControl2 . Refresh ( );
                    }
                }
            }
            else
            {
                foreach ( DataRow ro in rows )
                {
                    if ( tableViewTwo . Select ( "ANV002='" + ro [ "EMP001" ] . ToString ( ) + "'" ) . Length < 1 )
                    {
                        row = tableViewTwo . NewRow ( );
                        row [ "ANV002" ] = ro [ "EMP001" ] . ToString ( );
                        row [ "ANV003" ] = ro [ "EMP002" ] . ToString ( );
                        row [ "ANV004" ] = ro [ "EMP007" ] . ToString ( );
                        row [ "ANV008" ] = ro [ "DAA002" ];
                        if ( "计时" . Equals ( txtANT011 . Text ) )
                        {
                            row [ "ANV005" ] = dtStart;
                            row [ "ANV006" ] = dtEnd;
                        }
                        else if ( "计件" . Equals ( txtANT011 . Text ) )
                        {
                            row [ "ANV013" ] = dtStart;
                            row [ "ANV014" ] = dtEnd;
                        }
                        row [ "ANV007" ] = "在职";
                        tableViewTwo . Rows . Add ( row );
                        gridControl2 . Refresh ( );
                    }
                }
            }
            calcuSumTime ( );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtANT003 . Text == string . Empty || tableViewOne == null || tableViewOne . Rows . Count < 1 )
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
        private void txtANT009_TextChanged ( object sender ,EventArgs e )
        {
            calcuSumTime ( );
        }
        private void txtANT010_TextChanged ( object sender ,EventArgs e )
        {
            calcuSumTime ( );
        }
        private void btnEdit_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            tableEInfo = _bll . getTableOrder ( row [ "ANU003" ] . ToString ( ) ,row [ "ANU004" ] . ToString ( ) );
            ChildForm . FormAssNewList from = new ChildForm . FormAssNewList ( tableEInfo );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow ro = from . getDataRow;
                if ( ro == null )
                    return;
                row [ "ANU002" ] = ro [ "ANW001" ];
                row [ "ANU008" ] = ro [ "ANW012" ];
                row [ "ANU009" ] = ro [ "ANW013" ];
                row [ "ANU011" ] = ro [ "ANW009" ];

                editOtherSur ( row [ "ANU003" ] . ToString ( ) ,row [ "ANU004" ] . ToString ( ) );
            }
        }
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        private void contextMenuStrip2_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focuseName );
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void bandedGridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "ANV010" )
            {
                if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDecimal ( e . CellValue ) >= 200 )
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                }
            }
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void txtANT011_SelectedValueChanged ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtANT011 . Text ) )
                return;

            updateBatchTime ( );
        }
        private void txtANT014_EditValueChanged ( object sender ,EventArgs e )
        {
            //开工
            updateBatchTime ( );
        }
        private void txtANT015_EditValueChanged ( object sender ,EventArgs e )
        {
            //完工
            updateBatchTime ( );
        }
        void updateBatchTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;

            if ( !string . IsNullOrEmpty ( txtANT014 . Text ) )
                dtStart = Convert . ToDateTime ( txtANT014 . Text );
            if ( !string . IsNullOrEmpty ( txtANT015 . Text ) )
                dtEnd = Convert . ToDateTime ( txtANT015 . Text );

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                if ( "计时" . Equals ( txtANT011 . Text ) )
                {
                    row [ "ANV005" ] = dtStart;
                    row [ "ANV006" ] = dtEnd;
                    row [ "ANV013" ] = DBNull . Value;
                    row [ "ANV014" ] = DBNull . Value;
                    row [ "ANV015" ] = DBNull . Value;
                }
                else if ( "计件" . Equals ( txtANT011 . Text ) )
                {
                    row [ "ANV013" ] = dtStart;
                    row [ "ANV014" ] = dtEnd;
                    row [ "ANV005" ] = DBNull . Value;
                    row [ "ANV006" ] = DBNull . Value;
                    row [ "ANV009" ] = DBNull . Value;
                }
            }
            calcuSumTime ( );
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtANT003 . ReadOnly = txtANT005 . ReadOnly = txtANT009 . ReadOnly = txtANT010 . ReadOnly = txtANT011 . ReadOnly = txtANT012 . ReadOnly = txtANT014 . ReadOnly = txtANT015 . ReadOnly = true;
            gridView1 . OptionsBehavior . Editable = bandedGridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtANT003 . ReadOnly = txtANT005 . ReadOnly = txtANT009 . ReadOnly = txtANT010 . ReadOnly = txtANT011 . ReadOnly = txtANT012 . ReadOnly = txtANT014 . ReadOnly = txtANT015 . ReadOnly = false;
            gridView1 . OptionsBehavior . Editable = bandedGridView1 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtANT001 . Text = txtANT003 . Text = txtANT005 . Text = txtu0 . Text = txtu1 . Text = txtu2 . Text = txtANT009 . Text = txtANT010 . Text = txtANT011.Text=txtANT012.Text= txtANT014 . Text = txtANT015 . Text = string . Empty;
            gridControl1 . DataSource = null;
            gridControl2 . DataSource = null;
        }
        void InitData ( )
        {
            tableWork = _bll . getDepart ( );

            txtANT003 . Properties . DataSource = tableWork;
            txtANT003 . Properties . DisplayMember = "DAA002";
            txtANT003 . Properties . ValueMember = "DAA001";

            tablePInfo = _bll . getTablePInfo ( );
            Edit1 . DataSource = tablePInfo;
            Edit1 . DisplayMember = "RAA001";
            Edit1 . ValueMember = "RAA001";

            //tableEInfo = _bll . getTableOrder ( );
            //Edit2 . DataSource = tableEInfo;
            //Edit2 . DisplayMember = "ANW001";
            //Edit2 . ValueMember = "ANW001";

            tablePersonInfo= _bll . getPersonInfo ( );
            Edit3 . DataSource = tablePersonInfo;
            Edit3 . DisplayMember = "EMP001";
            Edit3 . ValueMember = "EMP001";

            tableDepart = _bll . getDepart ( "0507" );
            Edit7 . DataSource = tableDepart;
            Edit7 . DisplayMember = "DAA001";
            Edit7 . ValueMember = "DAA001";
        }
        bool getValeu ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtANT003 . Text ) )
            {
                XtraMessageBox . Show ( "请选择部门" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtANT005 . Text ) )
            {
                XtraMessageBox . Show ( "请选择班组" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtANT014 . Text ) )
            {
                XtraMessageBox . Show ( "请选择开工时间" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtANT015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择完工时间" );
                return false;
            }
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );
            if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                return false;

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return false;

            _header . ANT001 = txtANT001 . Text;
            _header . ANT002 = txtANT003 . EditValue . ToString ( );
            _header . ANT003 = txtANT003 . Text;
            _header . ANT004 = txtANT005 . EditValue . ToString ( );
            _header . ANT005 = txtANT005 . Text;
            _header . ANT008 = Convert . ToDateTime ( txtANT008 . Text );
            _header . ANT009 = string . IsNullOrEmpty ( txtANT009 . Text ) == true ? 0 : Convert . ToDecimal ( txtANT009 . Text );
            _header . ANT010 = string . IsNullOrEmpty ( txtANT010 . Text ) == true ? 0 : Convert . ToDecimal ( txtANT010 . Text );
            _header . ANT011 = txtANT011 . Text;
            _header . ANT012 = txtANT012 . Text;
            _header . ANT014 = Convert . ToDateTime ( txtANT014 . Text );
            _header . ANT015 = Convert . ToDateTime ( txtANT015 . Text );
            gridView1 . ClearColumnErrors ( );
            for ( int i = 0 ; i < gridView1 . RowCount ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                if ( row != null )
                {
                    row . ClearErrors ( );
                    _bodyOne . ANU002 = row [ "ANU002" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( _bodyOne . ANU002 ) )
                    {
                        row . SetColumnError ( "ANU002" ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyOne . ANU003 = row [ "ANU003" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( _bodyOne . ANU003 ) )
                    {
                        row . SetColumnError ( "ANU003" ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyOne . ANU010 = string . IsNullOrEmpty ( row [ "ANU010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU010" ] );
                    _bodyOne . ANU011 = string . IsNullOrEmpty ( row [ "U4" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "U4" ] );
                    if ( _bodyOne . ANU010 > _bodyOne . ANU011 )
                    {
                        row . SetColumnError ( "ANU010" ,"数量多于未完工数量" );
                        result = false;
                        break;
                    }
                }
            }
            if ( result == false )
                return result;

            bandedGridView1 . ClearColumnErrors ( );
            for ( int i = 0 ; i < bandedGridView1 . RowCount ; i++ )
            {
                row = bandedGridView1 . GetDataRow ( i );
                if ( row != null )
                {
                    row . ClearErrors ( );
                    _bodyTwo . ANV002 = row [ "ANV002" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( _bodyTwo . ANV002 ) )
                    {
                        row . SetColumnError ( "ANV002" ,"不可为空" );
                        result = false;
                        break;
                    }
                }
            }
            if ( result == false )
                return result;

            var query = from p in tableViewOne . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "ANU002" ) ,
                            p2 = p . Field<string> ( "ANU003" )
                        } into m
                        select new
                        {
                            anu002 = m . Key . p1 ,
                            anu003 = m . Key . p2 ,
                            count = m . Count ( )
                        };

            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "来源工单:" + x . anu003 + "\n\r来源组装报工单:" + x . anu002 + "\n\r重复,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return result;

            var quer = from t in tableViewTwo . AsEnumerable ( )
                       group t by new
                       {
                           t1 = t . Field<string> ( "ANV002" )
                       } into m
                       select new
                       {
                           anv002 = m . Key . t1 ,
                           count = m . Count ( )
                       };

            if ( quer != null )
            {
                foreach ( var x in quer )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . anv002 + "\n\r重复,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            _bodyOne . ANU001 = workShopTime . checkUserForOtherWork ( txtANT008 . Text ,tableViewTwo ,LineProductMesBll . ObtainInfo . codeTwo ,txtANT001 . Text );
            if ( !string . IsNullOrEmpty ( _bodyOne . ANU001 ) )
            {
                XtraMessageBox . Show ( _bodyOne . ANU001 ,"提示" );
                return false;
            }

            return result;
        }
        void setValue ( )
        {
            txtANT001 . Text = _header . ANT001;
            txtANT003 . EditValue = _header . ANT002;
            txtANT005 . EditValue = _header . ANT004;
            txtANT009 . Text = Convert . ToDecimal ( _header . ANT009 ) . ToString ( "0.#" );
            txtANT010 . Text = Convert . ToDecimal ( _header . ANT010 ) . ToString ( "0.#" );
            txtANT008 . Text = Convert . ToDateTime ( _header . ANT008 ) . ToString ( "yyyy-MM-dd" );
            txtANT011 . Text = _header . ANT011;
            txtANT012 . Text = _header . ANT012;
            txtANT014 . Text = _header . ANT014 . ToString ( );
            txtANT015 . Text = _header . ANT015 . ToString ( );
            Graph . grPic ( pictureEdit1 ,"反" );
            layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            if ( _header . ANT006 )
            {
                layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPic ( pictureEdit1 ,"审 核" );
                examineTool ( "审核" );
            }
            else
                examineTool ( "反审核" );
            if ( _header . ANT007 )
            {
                layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPic ( pictureEdit1 ,"注 销" );
                cancelltionTool ( "注销" );
            }
            else
                cancelltionTool ( "反注销" );
           
            //addPerson ( );
        }
        void calcuSumNum ( )
        {
            if ( tableViewOne . Compute ( "SUM(ANU010)" ,null ) == DBNull.Value )
                return;
            int sunNum = Convert . ToInt32 ( tableViewOne . Compute ( "SUM(ANU010)" ,null ) );
            txtu1 . Text = sunNum . ToString ( );
        }
        //总产值
        void calcuSumPrice ( )
        {
            //int numCount = 0;
            decimal /*priceCount = 0M,*/ priceSum = 0M;

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( "计件" . Equals ( txtANT011 . Text ) )
            {
                if ( U3 . SummaryItem . SummaryValue == null )
                    priceSum = 0;
                else
                    priceSum = string . IsNullOrEmpty ( U3 . SummaryItem . SummaryValue . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( U3 . SummaryItem . SummaryValue );
            }

            if ( U0 . SummaryItem . SummaryValue != null )
                priceSum += string . IsNullOrEmpty ( U0 . SummaryItem . SummaryValue . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( U0 . SummaryItem . SummaryValue );

            txtu2 . Text = priceSum . ToString ( "0.#" );
        }
        //总工时
        void addTotalTime ( decimal tTime )
        {
            //txtu0 . Text = Math . Round ( ANV009 . SummaryItem . SummaryValue == null ? 0 : Convert . ToDecimal ( ANV009 . SummaryItem . SummaryValue ) + ( ANV011 . SummaryItem . SummaryValue == null ? 0 : Convert . ToDecimal ( ANV011 . SummaryItem . SummaryValue ) ) ,1 ,MidpointRounding . AwayFromZero ) . ToString ( "0.#" );
            txtu0 . Text = tTime . ToString ( "0.#" );
        }
        //计算工资
        void calcuSumTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;
            DateTime dtOne, dtTwo;
            decimal sunNum = 0, tTime = 0;

            decimal ant009 = txtANT009 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANT009 . Text );
            decimal ant010 = txtANT010 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANT010 . Text );
            
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                //ANV007
                if ( string . IsNullOrEmpty ( row [ "ANV007" ] . ToString ( ) ) || row [ "ANV007" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANV007" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "ANV009" ] = 0;
                    row [ "ANV015" ] = 0;
                    row [ "ANV010" ] = 0;
                    continue;
                }
                sunNum = 0;
                if ( !string . IsNullOrEmpty ( row [ "ANV005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANV006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANV005" ] . ToString ( ) );
                    dtTwo = Convert . ToDateTime ( row [ "ANV006" ] . ToString ( ) );
                    sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );
                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( ant009 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( ant009 ) - Convert . ToDecimal ( ant010 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( ant010 );
                    row [ "ANV009" ] = Math . Round ( sunNum ,1 ,MidpointRounding . AwayFromZero );
                 
                }
                else
                    row [ "ANV009" ] = 0;

                tTime += sunNum;
                sunNum = 0;
                if ( !string . IsNullOrEmpty ( row [ "ANV013" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANV014" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANV013" ] . ToString ( ) );
                    dtTwo = Convert . ToDateTime ( row [ "ANV014" ] . ToString ( ) );
                    sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );
                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( ant009 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( ant009 ) - Convert . ToDecimal ( ant010 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        sunNum = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( ant010 );
                    row [ "ANV015" ] = Math . Round ( sunNum ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "ANV015" ] = 0;
                tTime += sunNum;
            }

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            calcuSumNum ( );
            calcuSumPrice ( );
            addTotalTime ( tTime );

            decimal totalTime = string . IsNullOrEmpty ( txtu0 . Text ) == true ? 0 : Convert . ToDecimal ( txtu0 . Text );
            decimal totalPrice = string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text );

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                //ANV007
                if ( string . IsNullOrEmpty ( row [ "ANV007" ] . ToString ( ) ) || row [ "ANV007" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANV007" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "ANV009" ] = 0;
                    row [ "ANV015" ] = 0;
                    row [ "ANV010" ] = 0;
                    continue;
                }

                sunNum = string . IsNullOrEmpty ( row [ "ANV009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV009" ] );
                sunNum += string . IsNullOrEmpty ( row [ "ANV015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV015" ] );

                row [ "ANV010" ] = totalTime == 0 ? 0 . ToString ( ) : ( totalPrice / totalTime * sunNum ) . ToString ( "0.##" );
            }
        }
        void calcuPrice ( )
        {
            if ( string . IsNullOrEmpty ( txtu0 . Text ) )
                return;
            if ( string . IsNullOrEmpty ( txtu2 . Text ) )
                return;
            decimal price = Convert . ToDecimal ( txtu0 . Text ) == 0 ? 0 : Convert . ToDecimal ( txtu2 . Text ) / Convert . ToDecimal ( txtu0 . Text );
            for ( int i = 0 ; i < bandedGridView1.RowCount ; i++ )
            {
                row = bandedGridView1 . GetDataRow ( i );
                if ( row != null )
                {
                    row [ "U1" ] = price . ToString ( "0.##" );
                    row [ "ANV010" ] = Math . Round ( Math . Round ( price ,2 ,MidpointRounding . AwayFromZero ) * ( string . IsNullOrEmpty ( row [ "ANV009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV009" ] ) ) ,1 ,MidpointRounding . AwayFromZero );
                }
            }
            gridControl2 . RefreshDataSource ( );
        }
        void addPerson ( )
        {
            DataRow [ ] rowes = tablePersonInfo . Select ( "EMP005='" + row [ "ANU008" ] + "'" );
            if ( rowes == null )
                return;
            DataRow rows;
            DateTime dtNow = LineProductMesBll . UserInfoMation . sysTime;
            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
            {
                foreach ( DataRow ro in rowes )
                {
                    rows = tableViewTwo . NewRow ( );
                    rows [ "ANV002" ] = ro [ "EMP001" ] . ToString ( );
                    rows [ "ANV003" ] = ro [ "EMP002" ] . ToString ( );
                    rows [ "ANV004" ] = ro [ "EMP007" ] . ToString ( );
                    rows [ "ANV008" ] = ro [ "DAA002" ] . ToString ( );
                    rows [ "ANV005" ] = dtStart;
                    rows [ "ANV006" ] = dtEnd;
                    rows [ "ANV007" ] = "在职";
                    tableViewTwo . Rows . Add ( rows );
                }
            }
            else
            {
                foreach ( DataRow ro in rowes )
                {
                    if ( tableViewTwo . Select ( "ANV002='" + ro [ "EMP001" ] + "'" ) . Length < 1 )
                    {
                        rows = tableViewTwo . NewRow ( );
                        rows [ "ANV002" ] = ro [ "EMP001" ] . ToString ( );
                        rows [ "ANV003" ] = ro [ "EMP002" ] . ToString ( );
                        rows [ "ANV004" ] = ro [ "EMP007" ] . ToString ( );
                        rows [ "ANV008" ] = ro [ "DAA002" ] . ToString ( );
                        rows [ "ANV005" ] = dtStart;
                        rows [ "ANV006" ] = dtEnd;
                        rows [ "ANV007" ] = "在职";
                        tableViewTwo . Rows . Add ( rows );
                    }
                }
            }
            gridControl2 . RefreshDataSource ( );
            calcuSumTime ( );
        }
        void printOrExport ( )
        { 
            tablePrintOne = _bll . getTablePrintOne ( txtANT001 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtANT001 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void printOrExportOne ( )
        {
            tablePrintTre = _bll . getTablePrintTre ( txtANT001 . Text );
            tablePrintTre . TableName = "TableOne";
            tablePrintFor = _bll . getTablePrintFor ( txtANT001 . Text );
            tablePrintFor . TableName = "TableTwo";
            tablePrintFiv = _bll . getTablePrintFiv ( txtANT001 . Text );
            tablePrintFiv . TableName = "TableTre";
        }
        void addRow ( string column ,int selectIdx ,object value )
        {
            if ( selectIdx < tableViewTwo . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableViewTwo . Rows . Count ; i++ )
                {
                    row = tableViewTwo . Rows [ i ];
                    if ( i + 1 < tableViewTwo . Rows . Count )
                    {
                        if ( column . Equals ( "ANV005" ) )
                        {
                            if ( workShopTime . startCenTime ( row ,row [ "ANV005" ] ,"ANV014" ,"ANV006" ,"ANV013" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "ANV005" ] == null || ro [ "ANV005" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "ANV005" ] = /*row [ "ANV005" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        if ( column . Equals ( "ANV006" ) )
                        {
                            if ( workShopTime . endCenTime ( row ,row [ "ANV006" ] ,"ANV005" ,"ANV013" ,"ANV014" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "ANV006" ] == null || ro [ "ANV006" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "ANV006" ] =/* row [ "ANV006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        if ( column . Equals ( "ANV013" ) )
                        {
                            if ( workShopTime . startTime ( row ,row [ "ANV013" ] ,"ANV014" ,"ANV005" ,"ANV006" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "ANV013" ] == null || ro [ "ANV013" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "ANV013" ] =/* row [ "ANV006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        if ( column . Equals ( "ANV014" ) )
                        {
                            if ( workShopTime . endTime ( row ,row [ "ANV014" ] ,"ANV013" ,"ANV005" ,"ANV006" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "ANV014" ] == null || ro [ "ANV014" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "ANV014" ] =/* row [ "ANV006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
            calcuSumTime ( );
        }
        void editOtherSur ( string orderNum ,string proNum )
        {
            _bodyOne . ANU001 = txtANT001 . Text;
            _bodyOne . ANU003 = orderNum;
            _bodyOne . ANU004 = proNum;

            if ( _bodyOne . ANU003 == string . Empty || _bodyOne . ANU004 == string . Empty )
            {
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    foreach (DataRow row in tableViewOne.Rows )
                    {
                        _bodyOne . ANU003 = row [ "ANU003" ] . ToString ( );
                        _bodyOne . ANU004 = row [ "ANU004" ] . ToString ( );
                        tableOtherSur = _bll . getTableOtherSur ( _bodyOne . ANU003 ,_bodyOne . ANU004 ,_bodyOne . ANU001 );
                        if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "ANU" ];
                        }
                        else
                        {
                            row [ "U4" ] = row [ "ANU011" ];
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne . ANU003 ,_bodyOne . ANU004 ,_bodyOne . ANU001 );
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewOne . Select ( "ANU003='" + _bodyOne . ANU003 + "' AND ANU004='" + _bodyOne . ANU004 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "ANU" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = row [ "ANU011" ];
                        }
                    }
                }
            }
        }
        #endregion

    }
}
