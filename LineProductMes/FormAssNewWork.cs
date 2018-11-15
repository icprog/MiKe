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

namespace LineProductMes
{
    public partial class FormAssNewWork :FormChild
    {
        LineProductMesBll.Bll.AssNewWorkBll _bll=null;
        LineProductMesEntityu.AssNewWorkBodyEntity _body=null;
        LineProductMesEntityu.AssNewWorkHeaderEntity _header=null;
        DataTable tableWork,tableView,tableUser,tablePInfo,talePrintOne,talePrintTwo;
        DataRow row;
        List<string> idxDelete=new List<string>();
        int selectIndex;
        string parame=string.Empty,strWhere=string.Empty,state=string.Empty,focusName=string.Empty;
        bool result=false;
        DateTime dt;

        public FormAssNewWork ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkBll ( );
            _body = new LineProductMesEntityu . AssNewWorkBodyEntity ( );
            _header = new LineProductMesEntityu . AssNewWorkHeaderEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,View ,View1 ,View2 ,View3 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,View ,View1 ,View2 ,View3 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            txtANW015 . Properties . VistaEditTime = txtANW016 . Properties . VistaEditTime = Edit1 . VistaEditTime = Edit2 . VistaEditTime = Edit3 . VistaEditTime = Edit4 . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;
            wait . Hide ( );

            controlUnEnable ( );
            controlClear ( );

            //Task task = new Task ( InitData );
            //task . Start ( );
            InitData ( );
            getInitData ( );
            dt = LineProductMesBll . UserInfoMation . sysTime;
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

                calcuPriceSum ( );
                //calcuSalaryForSub ( );
                calcuSalarySum ( );
                calcuSalaryForSub ( );
                calcuSalaryForTime ( );
                //calcuTSumByP ( );
                //calcuTSumByT ( );
                //setCalcuSalary ( );
                //calcuTsumTime ( );
                addTotalTime ( );

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

            addTool ( );
            state = "add";
            txtANW014 . Text = "计件";
            txtANW011 . EditValue = "0507";
           
            txtANW015 . Text = dtTime . ToString ( "yyyy-MM-dd 08:00" );
            txtANW016 . Text = dtTime . ToString ( "yyyy-MM-dd 17:00" );
           

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

            if ( !string . IsNullOrEmpty ( txtANW002 . Text ) )
            {
                int nums = _bll . ExistsNum ( _header );
                if ( nums > 0 )
                {
                    XtraMessageBox . Show ( "已完工量为:" + nums . ToString ( ) + ",剩余完工量应为:" + ( _header . ANW006 - nums ) . ToString ( ) + "" );
                    ClassForMain . FormClosingState . formClost = true;
                    return 0;
                }
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
                    Graph . grPicZ ( pictureEdit1 ,"审 核" );
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
                    Graph . grPicZ ( pictureEdit1 ,"注 销" );
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
        protected override int Print ( )
        {
            if ( string . IsNullOrEmpty ( txtANW002 . Text ) )
                return 0;

            printOrExport ( );
            Print ( new DataTable [ ] { talePrintOne ,talePrintTwo } ,"入库单.frx" );

            return base . Print ( );
        }
        protected override int Export ( )
        {
            if ( string . IsNullOrEmpty ( txtANW002 . Text ) )
                return 0;

            printOrExport ( );
            Export ( new DataTable [ ] { talePrintOne ,talePrintTwo } ,"入库单.frx" );

            return base . Export ( );
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
                        rows [ "ANX002" ] = row [ "ANX002" ] . ToString ( );
                        rows [ "ANX003" ] = row [ "ANX003" ] . ToString ( );
                        rows [ "ANX004" ] = row [ "ANX004" ] . ToString ( );
                        rows [ "ANX014" ] = row [ "DAA002" ] . ToString ( );
                        rows [ "ANX011" ] = "在职";
                        if ( txtANW014 . Text . Equals ( "计件" ) )
                        {
                            rows [ "ANX005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                            rows [ "ANX006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        }
                        else
                        {
                            rows [ "ANX007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                            rows [ "ANX008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
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
                        rows [ "ANX002" ] = row [ "ANX002" ] . ToString ( );
                        rows [ "ANX003" ] = row [ "ANX003" ] . ToString ( );
                        rows [ "ANX004" ] = row [ "ANX004" ] . ToString ( );
                        rows [ "ANX014" ] = row [ "DAA002" ] . ToString ( );
                        rows [ "ANX011" ] = "在职";
                        if ( txtANW014 . Text . Equals ( "计件" ) )
                        {
                            rows [ "ANX005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                            rows [ "ANX006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        }
                        else
                        {
                            rows [ "ANX007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                            rows [ "ANX008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        }
                        tableView . Rows . Add ( rows );
                        gridControl1 . Refresh ( );
                    }
                }
            }
            calcuTsumTime ( );
        }
        private void txtANW015_EditValueChanged ( object sender ,EventArgs e )
        {
            startT ( );
            //calcuTSumByP ( );
            //calcuTSumByT ( );
        }
        private void txtANW016_EditValueChanged ( object sender ,EventArgs e )
        {
            endT ( );
            //calcuTSumByP ( );
            //calcuTSumByT ( );
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
                if ( XtraMessageBox . Show ( "确认删除?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
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
            startT ( );
            endT ( );
            calcuSalaryForTime ( );
            calcuTsumTime ( );
        }
        private void txtANW006_TextChanged ( object sender ,EventArgs e )
        {
            if ( txtANW006 . Text == string . Empty )
                return;
            calcuPriceSum ( );
        }
        private void txtANW008_TextChanged ( object sender ,EventArgs e )
        {
            if ( txtANW008 . Text == string . Empty )
                return;
            calcuPriceSum ( );
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            string anx009Result = string . Empty;
            decimal outRsult = 0M;
          
            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
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

                //calcuBTSay ( );
                calcuSalaryForTime ( );
                calcuSalaryForSub ( );
                calcuSalarySum ( );
                //calcuTSumByP ( );
                //calcuTSumByT ( );
                setCalcuSalary ( );
            }
            if ( e . Column . FieldName == "ANX005" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"ANX006" ,"ANX007" ,"ANX008" ) == false )
                {
                    row [ "ANX005" ] = DBNull . Value;
                    row [ "ANX017" ] = DBNull . Value;
                    row [ "ANX015" ] = DBNull . Value;
                    row [ "ANX016" ] = DBNull . Value;
                }
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( e . Value ) . ToString ( "yyyyMMdd" ) != Convert . ToDateTime ( txtANW015 . Text ) . ToString ( "yyyyMMdd" ) )
                    {
                        row [ "ANX005" ] = DBNull . Value;
                        row [ "ANX017" ] = DBNull . Value;
                        row [ "ANX015" ] = DBNull . Value;
                        row [ "ANX016" ] = DBNull . Value;
                    }
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
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( e . Value ) . ToString ( "yyyyMMdd" ) != Convert . ToDateTime ( txtANW015 . Text ) . ToString ( "yyyyMMdd" ) )
                    {
                        row [ "ANX006" ] = DBNull . Value;
                        row [ "ANX017" ] = DBNull . Value;
                        row [ "ANX015" ] = DBNull . Value;
                        row [ "ANX016" ] = DBNull . Value;
                    }
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
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( e . Value ) . ToString ( "yyyyMMdd" ) != Convert . ToDateTime ( txtANW015 . Text ) . ToString ( "yyyyMMdd" ) )
                    {
                        row [ "ANX007" ] = DBNull . Value;
                        row [ "ANX017" ] = DBNull . Value;
                        row [ "ANX015" ] = DBNull . Value;
                        row [ "ANX016" ] = DBNull . Value;
                    }
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
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( e . Value ) . ToString ( "yyyyMMdd" ) != Convert . ToDateTime ( txtANW015 . Text ) . ToString ( "yyyyMMdd" ) )
                    {
                        row [ "ANX008" ] = DBNull . Value;
                        row [ "ANX017" ] = DBNull . Value;
                        row [ "ANX015" ] = DBNull . Value;
                        row [ "ANX016" ] = DBNull . Value;
                    }
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
            }
            else if ( e . Column . FieldName == "ANX002" || e . Column . FieldName == "ANX003" || e . Column . FieldName == "ANX004" || e . Column . FieldName == "ANX009" || e . Column . FieldName == "ANX013" )
            {
                if ( txtANW014 . Text . Equals ( "计件" ) )
                {
                    if ( row [ "ANX005" ] == null || row [ "ANX005" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANX005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "ANX006" ] == null || row [ "ANX006" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANX006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                else if ( txtANW014 . Text . Equals ( "计时" ) )
                {
                    if ( row [ "ANX007" ] == null || row [ "ANX007" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANX007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "ANX008" ] == null || row [ "ANX008" ] . ToString ( ) == string . Empty )
                    {
                        row [ "ANX008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                if ( row [ "ANX011" ] == null || row [ "ANX011" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANX011" ] = "在职";
                }
                calcuTsumTime ( );
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
        private void txtANW002_EditValueChanged ( object sender ,EventArgs e )
        {
            row = View2 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( row [ "RAA008" ] . ToString ( ) == string . Empty )
            {
                txtANW003 . Text = string . Empty;
                txtANW004 . Text = string . Empty;
                txtANW005 . Text = string . Empty;
                txtANW006 . Text = string . Empty;
                txtANW007 . Text = string . Empty;
                txtANW008 . Text = string . Empty;
                return;
            }
            txtANW003 . Text = row [ "RAA015" ] . ToString ( );
            txtANW004 . Text = row [ "DEA002" ] . ToString ( );
            txtANW005 . Text = row [ "DEA057" ] . ToString ( );
            txtANW006 . Text = row [ "RAA018" ] . ToString ( );
            txtANW007 . Text = row [ "DEA003" ] . ToString ( );
            txtANW008 . Text = row [ "DEA050" ] . ToString ( );
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableView );
            else
                result = _bll . Edit ( _header ,tableView ,idxDelete );
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

                    _header . ANW020 = _header . ANW021 = false;
                    //_header = _bll . getHeader ( _header . ANW001 );
                    setValue ( );

                    setCalcuSalary ( );

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
                if ( txtANW002 . Text == string . Empty || tableView == null || tableView . Rows . Count < 1 )
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
        private void txtANW023_TextChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtANW024_TextChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtANW022_TextChanged ( object sender ,EventArgs e )
        {
            //calcuBTSay ( );
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focusName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focusName );
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
                    txtANW002 . Properties . DataSource = tablePInfo;
                    txtANW002 . Properties . DisplayMember = "RAA001";
                    txtANW002 . Properties . ValueMember = "RAA001";
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
            txtANW002 . ReadOnly = txtANW009 . ReadOnly = txtANW014 . ReadOnly = txtANW011 . ReadOnly = txtANW013 . ReadOnly = txtANW015 . ReadOnly = txtANW016 . ReadOnly = txtANW017 . ReadOnly = txtANW023 . ReadOnly = txtANW024 . ReadOnly = true;
            bandedGridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtANW002 . ReadOnly = txtANW009 . ReadOnly = txtANW014 . ReadOnly = txtANW011 . ReadOnly = txtANW013 . ReadOnly = txtANW015 . ReadOnly = txtANW016 . ReadOnly = txtANW017 . ReadOnly = txtANW023 . ReadOnly = txtANW024 . ReadOnly = false;
            bandedGridView1 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtANW001 . Text = txtANW002 . Text = txtANW003 . Text = txtANW004 . Text = txtANW005 . Text = txtANW006 . Text = txtANW007 . Text = txtANW008 . Text = txtANW009 . Text = txtANW011 . Text = txtANW013 . Text = txtANW014 . Text = txtANW015 . Text = txtANW016 . Text = txtANW017 . Text = txtu0 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtANW019 . Text = txtu0 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text = txtu4 . Text = txtu5 . Text = txtANW022 . Text = txtANW023 . Text = txtANW024 . Text = string . Empty;
            gridControl1 . DataSource = null;
            layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }
        void startT ( )
        {
            result = true;
            if ( txtANW015 . EditValue == null || txtANW015 . EditValue . ToString ( ) == string . Empty )
                return;
            string startTime = txtANW015 . Text;
            if ( string . IsNullOrEmpty ( startTime ) )
                return;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = tableView . Rows [ i ];
                if ( row != null )
                {
                    if ( txtANW014 . Text . Equals ( "计件" ) )
                    {
                        row [ "ANX007" ] = DBNull . Value;
                        row [ "ANX008" ] = DBNull . Value;
                        row [ "ANX017" ] = 0;
                        if ( workShopTime . startTime ( row ,row [ "ANX005" ] ,"ANX006" ,"ANX007" ,"ANX008" ) == false )
                        {
                            row [ "ANX005" ] = DBNull . Value;
                            row [ "ANX017" ] = 0;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . startTime ( row ,startTime ,"ANX006" ,"ANX007" ,"ANX008" ) == false )
                            {
                                row [ "ANX005" ] = DBNull . Value;
                                row [ "ANX017" ] = 0;
                                result = false;
                            }
                            else
                            {
                                row . BeginEdit ( );
                                row [ "ANX005" ] = startTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                    else if ( txtANW014 . Text . Equals ( "计时" ) )
                    {
                        row [ "ANX005" ] = DBNull . Value;
                        row [ "ANX006" ] = DBNull . Value;
                        row [ "ANX017" ] = 0;
                        if ( workShopTime . startCenTime ( row ,row [ "ANX007" ] ,"ANX006" ,"ANX008","ANX005" )==false )
                        {
                            row [ "ANX007" ] = DBNull . Value;
                            row [ "ANX017" ] = 0;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . startCenTime ( row ,startTime ,"ANX006" ,"ANX008","ANX005" )==false )
                            {
                                row [ "ANX007" ] = DBNull . Value;
                                row [ "ANX017" ] = 0;
                                result = false;
                            }
                            else
                            {
                                row . BeginEdit ( );
                                row [ "ANX007" ] = startTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                    //calcuBTSay ( );
                    calcuSalaryForSub ( );
                    calcuSalarySum ( );
                }
            }
            calcuTsumTime ( );
        }
        void endT ( )
        {
            result = true;
            if ( txtANW016 . EditValue == null || txtANW016 . EditValue . ToString ( ) == string . Empty )
                return;
            string endTime = txtANW016 . Text;
            if ( string . IsNullOrEmpty ( endTime ) )
                return;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = tableView . Rows [ i ];
                if ( row != null )
                {
                    if ( txtANW014 . Text . Equals ( "计件" ) )
                    {
                        row [ "ANX007" ] = DBNull . Value;
                        row [ "ANX008" ] = DBNull . Value;
                        row [ "ANX017" ] = 0;
                        if ( workShopTime . endTime ( row ,row [ "ANX006" ] ,"ANX005" ,"ANX007","ANX008" )==false )
                        {
                            row [ "ANX006" ] = DBNull . Value;
                            row [ "ANX017" ] = 0;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . endTime ( row ,endTime ,"ANX005" ,"ANX007" ,"ANX008" ) ==false )
                            {
                                row [ "ANX006" ] = DBNull . Value;
                                row [ "ANX017" ] = 0;
                                result = false;
                            }
                            if ( result )
                            {
                                row . BeginEdit ( );
                                row [ "ANX006" ] = endTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                    else if ( txtANW014 . Text . Equals ( "计时" ) )
                    {
                        row [ "ANX005" ] = DBNull . Value;
                        row [ "ANX006" ] = DBNull . Value;
                        row [ "ANX017" ] = 0;
                        if ( workShopTime . endCenTime ( row ,row [ "ANX008" ] ,"ANX007" ,"ANX005" ,"ANX006" ) == false )
                        {
                            row [ "ANX008" ] = DBNull . Value;
                            row [ "ANX017" ] = 0;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . endCenTime ( row ,endTime ,"ANX007" ,"ANX005" ,"ANX006" ) == false )
                            {
                                row [ "ANX008" ] = DBNull . Value;
                                row [ "ANX017" ] = 0;
                                result = false;
                            }
                            if ( result )
                            {
                                row . BeginEdit ( );
                                row [ "ANX008" ] = endTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                    //calcuBTSay ( );
                    calcuSalaryForSub ( );
                    calcuSalarySum ( );
                }
            }
            calcuTsumTime ( );
        }
        void calcuPriceSum ( )
        {
            txtu1 . Text = ( string . IsNullOrEmpty ( txtANW009 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW009 . Text ) * ( string . IsNullOrEmpty ( txtANW008 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW008 . Text ) ) ) . ToString ( "0.######" );
        }
        void calcuSalaryForTime ( )
        {
            //if ( txtANW014 . Text . Equals ( "计时" ) )
            //{
            //    txtu0 . Text = 0 . ToString ( );
            //    return;
            //}
            //if ( tableView == null || tableView . Rows . Count < 1 )
            //    return;
            //parame = tableView . Compute ( "SUM(ANX009)" ,null ) . ToString ( );
            txtu0 . Text = U4 . SummaryItem . SummaryValue == null ? 0 . ToString ( ) : Convert . ToDecimal ( U4 . SummaryItem . SummaryValue ) . ToString ( "0.##" );
        }
        void calcuSalaryForSub ( )
        {
            parame = tableView . Compute ( "SUM(ANX010)" ,null ) . ToString ( );
            txtu2 . Text = string . IsNullOrEmpty ( parame ) == true ? 0 . ToString ( ) : Convert . ToDecimal ( parame ) . ToString ( "0.######" );
        }
        void calcuSalarySum ( )
        {
            txtu4 . Text = ( ( string . IsNullOrEmpty ( txtu0 . Text ) == true ? 0 : Convert . ToDecimal ( txtu0 . Text ) ) + ( string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text ) ) ) . ToString ( "0.######" );
            if ( string . IsNullOrEmpty ( txtANW014 . Text ) )
                return;
            if ( txtANW014 . Text . Equals ( "计时" ) )
                return;
            txtu4 . Text = ( ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) ) + ( string . IsNullOrEmpty ( txtu1 . Text ) == true ? 0 : Convert . ToDecimal ( txtu1 . Text ) ) ) . ToString ( "0.######" );
        }
        void calcuTSumByP ( )
        {
            if ( string . IsNullOrEmpty ( txtANW014 . Text ) )
                return;
            if ( txtANW014 . Text . Equals ( "计时" ) )
                return;
            int calcuT = 0;
            string dtStart, dtEnd;
            if ( tableView == null )
                return;
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                dtStart = tableView . Rows [ i ] [ "ANX005" ] . ToString ( );
                dtEnd = tableView . Rows [ i ] [ "ANX006" ] . ToString ( );
                if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
                {
                    calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
                }
                dtStart = tableView . Rows [ i ] [ "ANX007" ] . ToString ( );
                dtEnd = tableView . Rows [ i ] [ "ANX008" ] . ToString ( );
                if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
                {
                    calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
                }
            }
            txtu5 . Text = calcuT . ToString ( );
        }
        void calcuTSumByT ( )
        {
            if ( string . IsNullOrEmpty ( txtANW014 . Text ) )
                return;
            //if ( txtANW014 . Text . Equals ( "计件" ) )
            //    return;
            int calcuT = 0;
            string dtStart, dtEnd;
            if ( tableView == null )
                return;
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                dtStart = tableView . Rows [ i ] [ "ANX005" ] . ToString ( );
                dtEnd = tableView . Rows [ i ] [ "ANX006" ] . ToString ( );
                if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
                {
                    calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
                }
                dtStart = tableView . Rows [ i ] [ "ANX007" ] . ToString ( );
                dtEnd = tableView . Rows [ i ] [ "ANX008" ] . ToString ( );
                if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
                {
                    calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
                }
            }
            txtu5 . Text = calcuT . ToString ( );
        }
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
            //    if ( !string . IsNullOrEmpty ( row [ "ANX005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANX006" ] . ToString ( ) ) )
            //{
            //    dtStartUpper = Convert . ToDateTime ( row [ "ANX005" ] . ToString ( ) );
            //    days = dtStartUpper . Subtract ( dtTime ) . Days;
            //    if ( days <= 7 && days >= 0 )
            //        row [ "ANX010" ] = days * 7;
            //}
            //else if ( !string . IsNullOrEmpty ( row [ "ANX007" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANX008" ] . ToString ( ) ) )
            //{
            //    dtStartUpper = Convert . ToDateTime ( row [ "ANX007" ] . ToString ( ) );
            //    days = dtStartUpper . Subtract ( dtTime ) . Days;
            //    if ( days <= 7 && days >= 0 )
            //        row [ "ANX010" ] = days * 7;
            //}
            calcuSalaryForSub ( );
            calcuSalarySum ( );
        }
        void setCalcuSalary ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            decimal calcuT = 0, dtStart = 0, dtEnd = 0, resultSalary = 0;
            if ( "计件" . Equals ( txtANW014 . Text ) )
            {
                if ( string . IsNullOrEmpty ( txtu5 . Text ) )
                    return;
                if ( Convert . ToDecimal ( txtu5 . Text ) == 0 )
                    return;
                resultSalary = ( string . IsNullOrEmpty ( txtu4 . Text ) == true ? 0 : Convert . ToDecimal ( txtu4 . Text ) - ( string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text ) ) ) / Convert . ToDecimal ( txtu5 . Text );

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
                            row [ "ANX015" ] = 0;
                            row [ "ANX017" ] = 0;
                            continue;
                        }
                        calcuT = 0;
                        row = tableView . Rows [ i ];
                        dtStart = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] . ToString ( ) );
                        dtEnd = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] . ToString ( ) );
                        calcuT += dtEnd + dtStart;
                        row [ "ANX017" ] = ( calcuT * resultSalary ) . ToString ( "0.#" );
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
                        row [ "ANX015" ] = 0;
                        row [ "ANX017" ] = 0;
                        continue;
                    }
                    calcuT = 0;
                    row = tableView . Rows [ i ];
                    dtStart = string . IsNullOrEmpty ( row [ "ANX015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX015" ] . ToString ( ) );
                    dtEnd = string . IsNullOrEmpty ( row [ "ANX016" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX016" ] . ToString ( ) );
                    calcuT += dtEnd + dtStart;
                    resultSalary = string . IsNullOrEmpty ( row [ "ANX009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANX009" ] . ToString ( ) );
                    row [ "ANX017" ] = ( calcuT * resultSalary ) . ToString ( "0.#" );
                }
            }
            //if ( !string . IsNullOrEmpty ( txtANW014 . Text ) && txtANW014 . Text . Equals ( "计时" ) )
            //{
            //    for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            //    {
            //        calcuT = 0;
            //        row = tableView . Rows [ i ];
            //        dtStart = row [ "ANX005" ] . ToString ( );
            //        dtEnd = row [ "ANX006" ] . ToString ( );
            //        if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
            //        {
            //            calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
            //        }
            //        dtStart = row [ "ANX007" ] . ToString ( );
            //        dtEnd = row [ "ANX008" ] . ToString ( );
            //        if ( !string . IsNullOrEmpty ( dtStart ) && !string . IsNullOrEmpty ( dtEnd ) )
            //        {
            //            calcuT += ( Convert . ToDateTime ( dtEnd ) - Convert . ToDateTime ( dtStart ) ) . Hours;
            //        }
            //        row [ "ANX017" ] = ( calcuT * resultSalary ) . ToString ( "0.#" );
            //    }
            //}
        }
        void setValue ( )
        {
            txtANW001 . Text = _header . ANW001;
            txtANW014 . EditValue = _header . ANW014;
            txtANW011 . EditValue = _header . ANW010;
            txtANW013 . EditValue = _header . ANW012;
            txtANW015 . EditValue = _header . ANW015;
            txtANW016 . EditValue = _header . ANW016;
            txtANW002 . EditValue = _header . ANW002;
            txtANW003 . Text = _header . ANW003;
            txtANW004 . Text = _header . ANW004;
            txtANW005 . Text = _header . ANW005;
            txtANW006 . Text = _header . ANW006 . ToString ( );
            txtANW007 . Text = _header . ANW007;
            txtANW008 . Text = _header . ANW008 . ToString ( );
            txtANW009 . Text = _header . ANW009 . ToString ( );
            txtANW017 . Text = _header . ANW017;
            //txtANW018 . Text = _header . ANW018;
            txtANW019 . Text = _header . ANW019;
            txtANW022 . Text = Convert . ToDateTime ( _header . ANW022 ) . ToString ( "yyyy-MM-dd" );
            txtANW023 . Text = _header . ANW023 . ToString ( );
            txtANW024 . Text = _header . ANW024 . ToString ( );

            layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPic ( pictureEdit1 ,"反" );
            if ( _header . ANW020 )
            {
                layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicZ ( pictureEdit1 ,"审 核" );
                examineTool ( "审核" );
            }
            else
            {
                examineTool ( "反审核" );
            }
            if ( _header . ANW021 )
            {
                layoutControlItem24 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicZ ( pictureEdit1 ,"注 销" );
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
            if ( txtANW014 . Text . Equals ( "计件" ) && string . IsNullOrEmpty ( txtANW002 . Text ) )
            {
                XtraMessageBox . Show ( "请选择来源单号" );
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
            if ( ( txtANW014 . Text . Equals ( "计件" ) || ( txtANW014 . Text . Equals ( "计时" ) && !string . IsNullOrEmpty ( txtANW002 . Text ) ) ) && string . IsNullOrEmpty ( txtANW009 . Text ) )
            {
                XtraMessageBox . Show ( "请录入完工数量" );
                return false;
            }
            int outResult = 0;
            if ( !string . IsNullOrEmpty ( txtANW009 . Text ) && int . TryParse ( txtANW009 . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "完工数量请填写数字" );
                return false;
            }
            _header . ANW009 = outResult;
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

            _header . ANW002 = txtANW002 . Text;
            _header . ANW003 = txtANW003 . Text;
            _header . ANW004 = txtANW004 . Text;
            _header . ANW005 = txtANW005 . Text;
            _header . ANW006 = string . IsNullOrEmpty ( txtANW006 . Text ) == true ? 0 : Convert . ToInt32 ( txtANW006 . Text );
            if ( txtANW014 . Text . Equals ( "计件" ) && _header . ANW009 > _header . ANW006 )
            {
                XtraMessageBox . Show ( "完工数量多余工单数量,请核实" );
                return false;
            }

            _header . ANW007 = txtANW007 . Text;
            _header . ANW008 = string . IsNullOrEmpty ( txtANW008 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW008 . Text );
            _header . ANW010 = txtANW011 . EditValue . ToString ( );
            _header . ANW011 = txtANW011 . Text;
            _header . ANW012 = txtANW013 . EditValue . ToString ( );
            _header . ANW013 = txtANW013 . Text;
            _header . ANW014 = txtANW014 . Text;
            if ( string . IsNullOrEmpty ( txtANW015 . Text ) )
                _header . ANW015 = null;
            else
                _header . ANW015 = Convert . ToDateTime ( txtANW015 . Text );
            if ( string . IsNullOrEmpty ( txtANW016 . Text ) )
                _header . ANW016 = null;
            else
                _header . ANW016 = Convert . ToDateTime ( txtANW016 . Text );
            _header . ANW017 = txtANW017 . Text;
            //_header . ANW018 = txtANW018 . Text;
            _header . ANW018 = string . Empty;
            _header . ANW019 = txtANW019 . Text;
            _header . ANW022 = Convert . ToDateTime ( txtANW022 . Text );
            _header . ANW023 = string . IsNullOrEmpty ( txtANW023 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW023 . Text );
            _header . ANW024 = string . IsNullOrEmpty ( txtANW024 . Text ) == true ? 0 : Convert . ToDecimal ( txtANW024 . Text );

            return true;
        }
        void printOrExport ( )
        {
            talePrintOne = _bll . getTablePrintOne ( txtANW001 . Text );
            talePrintOne . TableName = "TableOne";
            talePrintTwo = _bll . getTablePrintTwo ( txtANW001 . Text );
            talePrintTwo . TableName = "TableTwo";
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
                                if ( workShopTime . startTime ( row ,row [ "ANX005" ] ,"ANX006" ,"ANX007" ,"ANX008" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
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
                                if ( workShopTime . endTime ( row ,row [ "ANX006" ] ,"ANX005" ,"ANX007" ,"ANX008" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
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
                                if ( workShopTime . startCenTime ( row ,row [ "ANX007" ] ,"ANX006" ,"ANX008" ,"ANX005" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
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
                                if ( workShopTime . endCenTime ( row ,row [ "ANX008" ] ,"ANX007" ,"ANX005" ,"ANX006" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
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
        void calcuTsumTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            decimal anw023 = txtANW023 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANW023 . Text ) * 60;
            decimal anw024 = txtANW024 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANW024 . Text ) * 60;

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

                u1 = 0M;
                if ( !string . IsNullOrEmpty ( row [ "ANX005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANX006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANX005" ] );
                    dtTwo = Convert . ToDateTime ( row [ "ANX006" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( anw023 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( anw023 ) - Convert . ToDecimal ( anw024 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( anw024 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "ANX015" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    u1 += u0;
                }else
                    row [ "ANX015" ] = 0;
                u0 = 0M;
                if ( !string . IsNullOrEmpty ( row [ "ANX007" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANX008" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANX007" ] );
                    dtTwo = Convert . ToDateTime ( row [ "ANX008" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;
                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( anw023 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( anw023 ) - Convert . ToDecimal ( anw024 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( anw024 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "ANX016" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "ANX016" ] = 0;
            }
            addTotalTime ( );
        }
        void addTotalTime ( )
        {
            txtu5 . Text = ( ANX015 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( ANX015 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) + ( ANX016 . SummaryItem . SummaryValue == null ? 0 : Math . Round ( Convert . ToDecimal ( ANX016 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) ) ) . ToString ( "0.#" );
        }
        #endregion

    }
}










