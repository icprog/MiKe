using System;
using System . ComponentModel;
using System . Data;
using System . Threading . Tasks;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using Utility;
using System . Reflection;
using DevExpress . Utils . Paint;
using System . Threading;
using System . Collections . Generic;
using System . Linq;
using System . Windows . Forms;

namespace LineProductMes
{
    public partial class FormInjectionMolding :FormChild
    {
        LineProductMesBll.Bll.InjectionMoldingBll _bll=null;
        LineProductMesEntityu.InjectionMoldingHeaderEntity _header=null;
        LineProductMesEntityu.InjectionMoldingBodyOneEntity _bodyOne=null;
        LineProductMesEntityu.InjectionMoldingBodyTwoEntity _bodyTwo=null;
        LineProductMesEntityu.InjectionMoldingBodyTreEntity _bodyTre=null;

        DataTable tableViewOne,tableViewTwo,tableViewTre,tableProduce,tableUser,tableProduct,tablePer,tablePrintOne,tablePrintTwo,tableOtherSur;
        List<string> idxOne=new List<string>();
        List<string> idxTwo=new List<string>();
        List<string> idxTre=new List<string>();

        DataRow row;

        Thread thread; SynchronizationContext m_SyncContext = null;

        string state=string.Empty,strWhere=string.Empty,focuseName=string.Empty;
        bool result=false;

        public FormInjectionMolding ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . InjectionMoldingBll ( );
            _header = new LineProductMesEntityu . InjectionMoldingHeaderEntity ( );
            _bodyOne = new LineProductMesEntityu . InjectionMoldingBodyOneEntity ( );
            _bodyTwo = new LineProductMesEntityu . InjectionMoldingBodyTwoEntity ( );
            _bodyTre = new LineProductMesEntityu . InjectionMoldingBodyTreEntity ( );

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext . Current;

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,gridView1 ,gridView2 ,gridView4 ,View1 ,View2 ,View3 ,View4 ,View5 ,View6 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,gridView1 ,gridView2 ,gridView4 ,View1 ,View2 ,View3 ,View4 ,View5 ,View6 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            dt1 . VistaEditTime = dt2 . VistaEditTime = dt3 . VistaEditTime = dt4 . VistaEditTime = dt5 . VistaEditTime = dt6 . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            wait . Hide ( );

            controlClear ( );
            controlUnEnable ( );

            Task task = new Task ( InitData );
            task . Start ( );

            this . thread = new Thread ( new ThreadStart ( this . ThreadProcSafePost ) );
            this . thread . Start ( );
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormInjectionMoldingQuery from = new ChildForm . FormInjectionMoldingQuery ( );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                _header . IJA001 = from . getOdd;
                _header = _bll . getModel ( _header . IJA001 );

                setValue ( );

                tableViewOne = _bll . getTableOne ( _header . IJA001 );
                gridControl1 . DataSource = tableViewOne;

                tableViewTwo = _bll . getTableTwo ( _header . IJA001 );
                gridControl2 . DataSource = tableViewTwo;
                tableViewTre = _bll . getTableTre ( _header . IJA001 );
                gridControl3 . DataSource = tableViewTre;

                calcuSumTime ( );


                editOther ( string . Empty ,string . Empty );


                controlUnEnable ( );
                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            state = "add";
            txtIJA001 . Text = _bll . getOddNum ( );
            //txtIJA002 . Text = "计件";
            txtIJA004 . EditValue = "0502";
            txtIJA007 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );

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
            if ( string . IsNullOrEmpty ( txtIJA001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            result = _bll . Delete ( txtIJA001 . Text );
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
            if ( getValue ( ) == false )
            {
                ClassForMain . FormClosingState . formClost = false;
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
            if ( string . IsNullOrEmpty ( txtIJA001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . IJA010 = true;
            else
                _header . IJA010 = false;
            result = _bll . Exanmie ( txtIJA001 . Text ,_header . IJA010 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicS ( pictureEdit1 ,"审 核" );
                }
                else
                {
                    layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicS ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtIJA001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . IJA011 = true;
            else
                _header . IJA011 = false;
            result = _bll . CancelTion ( txtIJA001 . Text ,_header . IJA011 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicS ( pictureEdit1 ,"注 销" );
                }
                else
                {
                    layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicS ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        protected override int Print ( )
        {
            if ( "计件" . Equals ( txtIJA002 . Text ) && ( tableViewOne == null || tableViewOne . Rows . Count < 1 ) )
            {
                XtraMessageBox . Show ( "请选择需要打印的内容" );
                return 0;
            }
            else if ( "计时" . Equals ( txtIJA002 . Text ) && ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 ) )
            {
                XtraMessageBox . Show ( "请选择需要打印的内容" );
                return 0;
            }

            printOrExport ( );
            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . Print ( );
        }
        protected override int Export ( )
        {
            if ( "计件" . Equals ( txtIJA002 . Text ) && ( tableViewOne == null || tableViewOne . Rows . Count < 1 ) )
            {
                XtraMessageBox . Show ( "请选择需要导出的内容" );
                return 0;
            }
            else if ( "计时" . Equals ( txtIJA002 . Text ) && ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 ) )
            {
                XtraMessageBox . Show ( "请选择需要导出的内容" );
                return 0;
            }
            printOrExport ( );
            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

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
        private void gridView2_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void gridView4_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void txtIJA004_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtIJA004 . EditValue == null || txtIJA004 . EditValue . ToString ( ) == string . Empty )
                return;
            txtIJA006 . Properties . DataSource = _bll . getDepart ( txtIJA004 . EditValue . ToString ( ) );
            txtIJA006 . Properties . DisplayMember = "DAA002";
            txtIJA006 . Properties . ValueMember = "DAA001";

            txtIJA006 . EditValue = "0502";
        }
        private void txtIJA002_TextChanged ( object sender ,EventArgs e )
        {
            if ( txtIJA002 . Text . Equals ( "计件" ) )
            {
                bandedGridView1 . OptionsBehavior . Editable = true;
                gridView2 . OptionsBehavior . Editable = gridView4 . OptionsBehavior . Editable = false;

                tableViewOne = _bll . getTableOne ( "1" );
                gridControl1 . DataSource = tableViewOne;
                xtraTabControl1 . SelectedTabPage = PageOne;
            }
            else if ( txtIJA002 . Text . Equals ( "计时" ) )
            {
                bandedGridView1 . OptionsBehavior . Editable = false;
                gridView2 . OptionsBehavior . Editable = gridView4 . OptionsBehavior . Editable = true;
                tableViewTwo = _bll . getTableTwo ( "1" );
                gridControl2 . DataSource = tableViewTwo;
                tableViewTre = _bll . getTableTre ( "1" );
                gridControl3 . DataSource = tableViewTre;
                xtraTabControl1 . SelectedTabPage = PageTwo;
            }

            calcuSumTime ( );

            //if ( txtIJA006 . EditValue == null || txtIJA006 . EditValue . ToString ( ) == string . Empty )
            //    return;

            //DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            //if ( txtIJA002 . Text . Equals ( "计件" ) )
            //{
            //    if ( tableViewOne == null )
            //        return;
            //        DataRow [ ] rows = tableUser . Select ( "EMP005='" + txtIJA006 . EditValue + "'" );
            //    if ( rows == null )
            //        return;
            //    if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
            //    {
            //        foreach ( DataRow r in rows )
            //        {
            //            row = tableViewOne . NewRow ( );
            //            row [ "IJB003" ] = "在职";
            //            row [ "IJB002" ] = r [ "EMP001" ];
            //            row [ "IJB012" ] = r [ "EMP002" ];
            //            row [ "IJB013" ] = r [ "EMP007" ];
            //            row [ "IJB016" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
            //            row [ "IJB017" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
            //            tableViewOne . Rows . Add ( row );
            //        }
            //    }
            //    else
            //    {
            //        foreach ( DataRow r in rows )
            //        {
            //            if ( tableViewOne . Select ( "IJB002='" + r [ "EMP001" ] + "'" ) . Length < 1 )
            //            {
            //                row = tableViewOne . NewRow ( );
            //                row [ "IJB003" ] = "在职";
            //                row [ "IJB002" ] = r [ "EMP001" ];
            //                row [ "IJB012" ] = r [ "EMP002" ];
            //                row [ "IJB013" ] = r [ "EMP007" ];
            //                row [ "IJB016" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
            //                row [ "IJB017" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
            //                tableViewOne . Rows . Add ( row );
            //            }
            //        }
            //    }
            //}
            //else if ( txtIJA002 . Text . Equals ( "计时" ) )
            //{
            //    if ( tableViewTre == null )
            //        return;
            //    DataRow [ ] rows = tablePer . Select ( "EMP005='" + txtIJA006 . EditValue + "'" );
            //    if ( rows == null )
            //        return;
            //    if ( tableViewTre == null || tableViewTre . Rows . Count < 1 )
            //    {
            //        foreach ( DataRow r in rows )
            //        {
            //            row = tableViewTre . NewRow ( );
            //            row [ "IJD002" ] = r [ "EMP001" ];
            //            row [ "IJD003" ] = r [ "EMP002" ];
            //            row [ "IJD004" ] = r [ "EMP007" ];
            //            row [ "IJD006" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
            //            row [ "IJD007" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
            //            row [ "IJD010" ] = "在职";
            //            tableViewTre . Rows . Add ( row );
            //        }
            //    }
            //    else
            //    {
            //        foreach ( DataRow r in rows )
            //        {
            //            if ( tableViewTre . Select ( "IJD002='" + r [ "EMP001" ] + "'" ) . Length < 1 )
            //            {
            //                row = tableViewTre . NewRow ( );
            //                row [ "IJD002" ] = r [ "EMP001" ];
            //                row [ "IJD003" ] = r [ "EMP002" ];
            //                row [ "IJD004" ] = r [ "EMP007" ];
            //                row [ "IJD006" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
            //                row [ "IJD007" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
            //                row [ "IJD010" ] = "在职";
            //                tableViewTre . Rows . Add ( row );
            //            }
            //        }
            //    }
            //}
        }
        private void txtIJA006_EditValueChanged ( object sender ,EventArgs e )
        {

        }
        private void EditGD_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "IJB004":
                if ( edit . EditValue == null )
                    return;
                row = tableProduce . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( row [ "RAA008" ] == null || string . IsNullOrEmpty ( row [ "RAA008" ] . ToString ( ) ) )
                    return;
                _bodyOne . IJB004 = edit . EditValue . ToString ( );
                _bodyOne . IJB005 = row [ "RAA015" ] . ToString ( );
                _bodyOne . IJB006 = row [ "DEA002" ] . ToString ( );
                _bodyOne . IJB007 = row [ "DEA057" ] . ToString ( );
                _bodyOne . IJB008 = row [ "DEA003" ] . ToString ( );
                _bodyOne . IJB009 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );
                _bodyOne . IJB010 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
                _bodyOne . IJB011 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB005" ] ,_bodyOne . IJB005 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB006" ] ,_bodyOne . IJB006 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB007" ] ,_bodyOne . IJB007 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB008" ] ,_bodyOne . IJB008 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB009" ] ,_bodyOne . IJB009 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB010" ] ,_bodyOne . IJB010 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB011" ] ,_bodyOne . IJB011 );

                editOther ( _bodyOne . IJB004 ,_bodyOne . IJB005 );

                break;
            }
        }
        private void EditGD2_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView2 . ActiveEditor;
            switch ( gridView2 . FocusedColumn . FieldName )
            {
                case "IJC002":
                if ( edit . EditValue == null )
                    return;
                row = tableProduct . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( row [ "RAA008" ] == null || string . IsNullOrEmpty ( row [ "RAA008" ] . ToString ( ) ) )
                    return;
                _bodyTwo . IJC002 = edit . EditValue . ToString ( );
                _bodyTwo . IJC003 = row [ "RAA015" ] . ToString ( );
                _bodyTwo . IJC004 = row [ "DEA002" ] . ToString ( );
                _bodyTwo . IJC005 = row [ "DEA057" ] . ToString ( );
                _bodyTwo . IJC006 = row [ "DEA003" ] . ToString ( );
                _bodyTwo . IJC007 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );
                _bodyTwo . IJC008 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
                _bodyTwo . IJC009 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC003" ] ,_bodyTwo . IJC003 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC004" ] ,_bodyTwo . IJC004 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC005" ] ,_bodyTwo . IJC005 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC006" ] ,_bodyTwo . IJC006 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC007" ] ,_bodyTwo . IJC007 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC008" ] ,_bodyTwo . IJC008 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "IJC009" ] ,_bodyTwo . IJC009 );

                editOther ( _bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                break;
            }
        }
        private void EditUser_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "IJB002":
                if ( edit . EditValue == null )
                    return;
                row = tableUser . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyOne . IJB012 = row [ "EMP002" ] . ToString ( );
                _bodyOne . IJB013 = row [ "EMP007" ] . ToString ( );
                _bodyOne . IJB022 = row [ "DAA002" ] . ToString ( );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB012" ] ,_bodyOne . IJB012 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB013" ] ,_bodyOne . IJB013 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "IJB022" ] ,_bodyOne . IJB022 );
                break;
            }
        }
        private void EditPer_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView4 . ActiveEditor;
            switch ( gridView4 . FocusedColumn . FieldName )
            {
                case "IJD002":
                if ( edit . EditValue == null )
                    return;
                row = tablePer . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyTre . IJD003 = row [ "EMP002" ] . ToString ( );
                _bodyTre . IJD004 = row [ "EMP007" ] . ToString ( );
                _bodyTre . IJD011 = row [ "DAA002" ] . ToString ( );
                gridView4 . SetFocusedRowCellValue ( gridView4 . Columns [ "IJD003" ] ,_bodyTre . IJD003 );
                gridView4 . SetFocusedRowCellValue ( gridView4 . Columns [ "IJD004" ] ,_bodyTre . IJD004 );
                gridView4 . SetFocusedRowCellValue ( gridView4 . Columns [ "IJD011" ] ,_bodyTre . IJD011 );
                break;
            }
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            //工作状态
            if ( e . Column . FieldName == "IJB003" )
            {
                if ( e . Value == null || e . Value . ToString ( ) == string . Empty )
                    return;
                if ( e . Value . Equals ( "离职" ) || e . Value . Equals ( "未上班" ) )
                {
                    row [ "IJB015" ] = DBNull . Value;
                    row [ "IJB016" ] = DBNull . Value;
                    row [ "IJB017" ] = DBNull . Value;
                    row [ "IJB018" ] = DBNull . Value;
                    row [ "IJB019" ] = DBNull . Value;
                    row [ "IJB020" ] = DBNull . Value;
                }
            }
            else if ( e . Column . FieldName == "IJB002" || e . Column . FieldName == "IJB004" )
            {
                DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
                if ( row [ "IJB016" ] == null || row [ "IJB016" ] . ToString ( ) == string . Empty )
                {
                    row [ "IJB016" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                }
                if ( row [ "IJB017" ] == null || row [ "IJB017" ] . ToString ( ) == string . Empty )
                {
                    row [ "IJB017" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                }
                if ( row [ "IJB003" ] == null || row [ "IJB003" ] . ToString ( ) == string . Empty )
                {
                    row [ "IJB003" ] = "在职";
                }
                calcuSumTime ( );
            }
            else if ( e . Column . FieldName == "IJB015" || e . Column . FieldName == "IJB020" )
            {
                calcuSumTime ( );
            }
            else if ( e . Column . FieldName == "IJB016" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"IJB017" ,"IJB018" ,"IJB019" ) == false )
                {
                    row [ "IJB016" ] = DBNull . Value;
                }

                addRow ( "IJB016" ,e . RowHandle );
            }
            else if ( e . Column . FieldName == "IJB017" )
            {
                if ( workShopTime . endTime ( row ,e . Value ,"IJB016" ,"IJB018" ,"IJB019" ) == false )
                {
                    row [ "IJB017" ] = DBNull . Value;
                }

                addRow ( "IJB017" ,e . RowHandle );
            }
            else if ( e . Column . FieldName == "IJB018" )
            {
                if ( workShopTime . startCenTime ( row ,e . Value ,"IJB017" ,"IJB019" ,"IJB016" ) == false )
                {
                    row [ "IJB018" ] = DBNull . Value;
                }
                addRow ( "IJB018" ,e . RowHandle );
            }
            else if ( e . Column . FieldName == "IJB019" )
            {
                if ( workShopTime . endCenTime ( row ,e . Value ,"IJB018" ,"IJB016" ,"IJB017" ) == false )
                {
                    row [ "IJB019" ] = DBNull . Value;
                }
                addRow ( "IJB019" ,e . RowHandle );
            }
            //else if ( e . Column . FieldName == "IJB012" /*|| e . Column . FieldName == "IJB013"*/ )
            //{
            //    editOther ( );
            //}
        }
        private void gridView4_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = gridView4 . GetFocusedDataRow ( );
            if ( e . Column . FieldName == "IJD010" )
            {
                if ( e . Value . Equals ( "离职" ) || e . Value . Equals ( "未上班" ) )
                {
                    row [ "IJD002" ] = DBNull . Value;
                    row [ "IJD003" ] = DBNull . Value;
                    row [ "IJD004" ] = DBNull . Value;
                    row [ "IJD006" ] = DBNull . Value;
                    row [ "IJD007" ] = DBNull . Value;
                    row [ "IJD008" ] = DBNull . Value;
                }
            }
            else if ( e . Column . FieldName == "IJD002" )
            {
                DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
                if ( row [ "IJD006" ] == null || row [ "IJD006" ] . ToString ( ) == string . Empty )
                {
                    row [ "IJD006" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                }
                if ( row [ "IJD007" ] == null || row [ "IJD007" ] . ToString ( ) == string . Empty )
                {
                    row [ "IJD007" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                }
                if ( row [ "IJD010" ] == null || row [ "IJD010" ] . ToString ( ) == string . Empty )
                {
                    row [ "IJD010" ] = "在职";
                }
                calcuSumTime ( );
            }
            else if ( e . Column . FieldName == "IJD006" )
            {
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( row [ "IJD007" ] != null && row [ "IJD007" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) > Convert . ToDateTime ( row [ "IJD007" ] ) )
                        {
                            row [ "IJD006" ] = DBNull . Value;
                        }
                    }
                }
                addRowTime ( "IJD006" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "IJD007" )
            {
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( row [ "IJD006" ] != null && row [ "IJD006" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) < Convert . ToDateTime ( row [ "IJD006" ] ) )
                        {
                            row [ "IJD007" ] = DBNull . Value;
                        }
                    }
                }
                addRowTime ( "IJD007" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "IJD008" )
            {
                calcuSumTime ( );
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableViewOne ,tableViewTwo ,tableViewTre );
            else
                result = _bll . Edit ( _header ,tableViewOne ,tableViewTwo ,tableViewTre ,idxOne ,idxTwo ,idxTre );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功保存" );
                    ClassForMain . FormClosingState . formClost = true;
                    saveTool ( );
                    controlUnEnable ( );
                    if ( state . Equals ( "add" ) )
                        _header . IJA001 = txtIJA001 . Text = LineProductMesBll . UserInfoMation . oddNum;

                    setValue ( );
                    if ( _header . IJA002 . Equals ( "计件" ) )
                    {
                        tableViewOne = _bll . getTableOne ( _header . IJA001 );
                        gridControl1 . DataSource = tableViewOne;
                    }
                    else
                    {
                        tableViewTwo = _bll . getTableTwo ( _header . IJA001 );
                        gridControl2 . DataSource = tableViewTwo;
                        tableViewTre = _bll . getTableTre ( _header . IJA001 );
                        gridControl3 . DataSource = tableViewTre;
                    }
                    editOther ( string . Empty ,string . Empty );
                }
                else
                {
                    XtraMessageBox . Show ( "保存失败" );
                    ClassForMain . FormClosingState . formClost = false;
                }
            }
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _bodyOne . idx > 0 && !idxOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                        idxOne . Add ( _bodyOne . idx . ToString ( ) );
                    tableViewOne . Rows . Remove ( row );
                    gridControl1 . Refresh ( );
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
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    _bodyTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _bodyTwo . idx > 0 && !idxTwo . Contains ( _bodyTwo . idx . ToString ( ) ) )
                        idxTwo . Add ( _bodyTwo . idx . ToString ( ) );
                    tableViewTwo . Rows . Remove ( row );
                    gridControl2 . Refresh ( );
                }
            }
        }
        private void gridControl3_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            row = gridView4 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    _bodyTre . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( _bodyTre . idx > 0 && !idxTre . Contains ( _bodyTre . idx . ToString ( ) ) )
                        idxTre . Add ( _bodyTre . idx . ToString ( ) );
                    tableViewTre . Rows . Remove ( row );
                    gridControl3 . Refresh ( );
                }
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtIJA002 . Text == string . Empty || tableViewOne == null || tableViewOne . Rows . Count < 1 )
                    return;
                if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    Save ( );
                    if ( ClassForMain . FormClosingState . formClost == false )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        private void txtIJA012_TextChanged ( object sender ,EventArgs e )
        {
            calcuSumTime ( );
        }
        private void txtIJA013_TextChanged ( object sender ,EventArgs e )
        {
            calcuSumTime ( );
        }
        private void bandedGridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
            if ( e . Column . FieldName == "U3" )
            {
                if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDecimal ( e . CellValue ) >= 200 )
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                }
            }
        }
        private void gridView2_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focuseName );
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtIJA002 . ReadOnly = txtIJA004 . ReadOnly = txtIJA006 . ReadOnly = txtIJA008 . ReadOnly = txtIJA012 . ReadOnly = txtIJA013 . ReadOnly = true;
            bandedGridView1 . OptionsBehavior . Editable = gridView2 . OptionsBehavior . Editable = gridView4 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtIJA002 . ReadOnly = txtIJA004 . ReadOnly = txtIJA006 . ReadOnly = txtIJA008 . ReadOnly = txtIJA012 . ReadOnly = txtIJA013 . ReadOnly = false;
            bandedGridView1 . OptionsBehavior . Editable = gridView2 . OptionsBehavior . Editable = gridView4 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtIJA001 . Text = txtIJA002 . Text = txtIJA004 . Text = txtIJA006 . Text = txtIJA007 . Text = txtIJA008 . Text = txtIJA012 . Text = txtIJA013 . Text = string . Empty;
            gridControl1 . DataSource = gridControl2 . DataSource = gridControl3 . DataSource = null;
            layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }
        void InitData ( )
        {
            DataTable tableWork = _bll . getDepart ( );
            if ( txtIJA004 . InvokeRequired )
            {
                Action<string> ij004 = ( x ) =>
                    {
                        txtIJA004 . Properties . DataSource = tableWork;
                        txtIJA004 . Properties . DisplayMember = "DAA002";
                        txtIJA004 . Properties . ValueMember = "DAA001";
                    };
                this . txtIJA004 . Invoke ( ij004 ,string . Empty );
            }
        }
        void setValue ( )
        {
            txtIJA001 . Text = _header . IJA001;
            txtIJA002 . Text = _header . IJA002;
            txtIJA004 . EditValue = _header . IJA003;
            txtIJA006 . EditValue = _header . IJA005;
            txtIJA007 . Text = Convert . ToDateTime ( _header . IJA007 ) . ToString ( "yyyy-MM-dd" );
            txtIJA008 . Text = _header . IJA008;
            //txtIJA009 . Text = _header . IJA009;
            txtIJA012 . Text = Convert . ToDecimal ( _header . IJA012 ) . ToString ( "0.#" );
            txtIJA013 . Text = Convert . ToDecimal ( _header . IJA013 ) . ToString ( "0.#" );
            layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPicS ( pictureEdit1 ,"反" );
            if ( _header . IJA010 )
            {
                layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicS ( pictureEdit1 ,"审 核" );
                examineTool ( "审核" );
            }
            else
                examineTool ( "反审核" );
            if ( _header . IJA011 )
            {
                layoutControlItem8 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicS ( pictureEdit1 ,"注 销" );
                cancelltionTool ( "注销" );
            }
            else
                cancelltionTool ( "反注销" );
        }
        bool getValue ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtIJA001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtIJA002 . Text ) )
            {
                XtraMessageBox . Show ( "请选择工资类型" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtIJA004 . Text ) )
            {
                XtraMessageBox . Show ( "请选择生产部门" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtIJA006 . Text ) )
            {
                XtraMessageBox . Show ( "请选择班组" );
                return false;
            }


            if ( txtIJA002 . Text . Equals ( "计件" ) )
            {
                bandedGridView1 . CloseEditor ( );
                bandedGridView1 . UpdateCurrentRow ( );
                if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                    return false;

                var qu = from p in tableViewOne . AsEnumerable ( )
                         group p by new
                         {
                             p1 = p . Field<string> ( "IJB004" ) ,
                             p2 = p . Field<string> ( "IJB005" )
                         } into m
                         select new
                         {
                             p1 = m . Key . p1 ,
                             p2 = m . Key . p2 ,
                             count = m . Count ( )
                         };
                if ( qu != null )
                {
                    if ( qu . ToArray ( ) . Length > 1 )
                    {
                        XtraMessageBox . Show ( "每次只能报工一个来源工单的品号" ,"提示" );
                        return false;
                    }
                }


                bandedGridView1 . ClearColumnErrors ( );
                DataRow row;
                for ( int i = 0 ; i < bandedGridView1 . RowCount ; i++ )
                {
                    row = bandedGridView1 . GetDataRow ( i );
                    if ( row == null )
                        continue;
                    _bodyOne . IJB002 = row [ "IJB002" ] . ToString ( );
                    if ( _bodyOne . IJB002 == string . Empty )
                    {
                        row . SetColumnError ( "IJB002" ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyOne . IJB003 = row [ "IJB003" ] . ToString ( );
                    if ( _bodyOne . IJB003 == string . Empty )
                    {
                        row . SetColumnError ( "IJB003" ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyOne . IJB004 = row [ "IJB004" ] . ToString ( );
                    if ( _bodyOne . IJB004 == string . Empty )
                    {
                        row . SetColumnError ( "IJB004" ,"不可为空" );
                        result = false;
                        break;
                    }
                    if ( row [ "IJB015" ] == null || row [ "IJB015" ] . ToString ( ) == string . Empty )
                    {
                        row . SetColumnError ( "IJB015" ,"不可为空" );
                        result = false;
                        break;
                    }
                }

                if ( result == false )
                    return false;

                var query = from p in tableViewOne . AsEnumerable ( )
                            group p by new
                            {
                                p1 = p . Field<string> ( "IJB002" )
                            } into m
                            select new
                            {
                                ijb002 = m . Key . p1 ,
                                count = m . Count ( )
                            };
                if ( query != null )
                {
                    foreach ( var x in query )
                    {
                        if ( x . count > 1 )
                        {
                            XtraMessageBox . Show ( "工号:" + x . ijb002 + "重复,请核实" );
                            result = false;
                            break;
                        }
                    }
                }
                if ( result == false )
                    return false;

                var que = from p in tableViewOne . AsEnumerable ( )
                          group p by new
                          {
                              p1 = p . Field<int?> ( "U4" ) == null ? 0 : p . Field<int> ( "U4" )
                          } into m
                          let sum = m . Sum ( t => t . Field<int?> ( "IJB015" ) == null ? 0 : t . Field<int> ( "IJB015" ) )
                          select new
                          {
                              u4 = m . Key . p1 ,
                              sum = sum
                          };
                if ( que != null )
                {
                    if ( que . ToArray ( ) [ 0 ] . sum > que . ToArray ( ) [ 0 ] . u4 )
                    {
                        XtraMessageBox . Show ( "完工数量之和多于工单未完工数量" ,"提示" );
                        return false;
                    }
                }
            }
            else
            {
                gridView2 . CloseEditor ( );
                gridView2 . UpdateCurrentRow ( );

                if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
                {
                    gridView2 . ClearColumnErrors ( );
                    DataRow row;
                    for ( int i=0;i< gridView2 . RowCount;i++ )
                    {
                        row = gridView2 . GetDataRow ( i );
                        if ( row == null )
                            continue;
                        _bodyTwo . IJC002 = row [ "IJC002" ] . ToString ( );
                        if ( _bodyTwo . IJC002 == string . Empty )
                        {
                            row . SetColumnError ( "IJC002" ,"不可为空" );
                            result = false;
                            break;
                        }
                        if ( row [ "IJC008" ] == null || row [ "IJC008" ] . ToString ( ) == string . Empty )
                        {
                            row . SetColumnError ( "IJC008" ,"不可为空" );
                            result = false;
                            break;
                        }
                        _bodyTwo . IJC010 = string . IsNullOrEmpty ( row [ "IJC010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IJC010" ] . ToString ( ) );
                        if ( _bodyTwo . IJC010 == 0 )
                        {
                            row . SetColumnError ( "IJC010" ,"不可为空" );
                            result = false;
                            break;
                        }
                    }
                    if ( result == false )
                        return false;

                    var query = from p in tableViewTwo . AsEnumerable ( )
                                group p by new
                                {
                                    p1 = p . Field<string> ( "IJC002" )
                                } into m
                                select new
                                {
                                    ijc002 = m . Key . p1 ,
                                    count = m . Count ( )
                                };
                    if ( query != null )
                    {
                        foreach ( var x in query )
                        {
                            if ( x . count > 1 )
                            {
                                XtraMessageBox . Show ( "来源工单:" + x . ijc002 + "重复,请核实" );
                                result = false;
                                break;
                            }
                        }
                    }

                    if ( result == false )
                        return false;

                    var oNum = from p in tableViewTwo . AsEnumerable ( )
                               group p by new
                               {
                                   p1 = p . Field<string> ( "IJC002" ) ,
                                   p2 = p . Field<string> ( "IJC003" ) ,
                                   p3 = p . Field<int?> ( "U4" ) == null ? 0 : p . Field<int> ( "U4" )
                               } into m
                               let sum = m . Sum ( t => t . Field<int?> ( "IJC010" ) == null ? 0 : t . Field<int> ( "IJC010" ) )
                               select new
                               {
                                   ijc002 = m . Key . p1 ,
                                   ijc003 = m . Key . p2 ,
                                   u4 = m . Key . p3 ,
                                   sum = sum
                               };
                    if ( oNum != null )
                    {
                        foreach ( var y in oNum )
                        {
                            if ( y . sum > y . u4 )
                            {
                                XtraMessageBox . Show ( "来源工单:" + y . ijc002 + "品号:" + y . ijc003 + "完工数量多于剩余数量" ,"提示" );
                                result = false;
                                break;
                            }
                        }
                    }

                    if ( result == false )
                        return false;

                }

                gridView4 . CloseEditor ( );
                gridView4 . UpdateCurrentRow ( );
                if ( tableViewTre == null || tableViewTre . Rows . Count < 1 )
                    return false;
                gridView4 . ClearColumnErrors ( );
                foreach ( DataRow row in tableViewTre . Rows )
                {
                    _bodyTre . IJD002 = row [ "IJD002" ] . ToString ( );
                    if ( _bodyTre . IJD002 == String . Empty )
                    {
                        gridView4 . SetColumnError ( IJD002 ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyTre . IJD010 = row [ "IJD010" ] . ToString ( );
                    if ( _bodyTre . IJD010 == String . Empty )
                    {
                        gridView4 . SetColumnError ( IJD010 ,"不可为空" );
                        result = false;
                        break;
                    }
                }
                if ( result == false )
                    return false;

                var que = from p in tableViewTre . AsEnumerable ( )
                          group p by new
                          {
                              p1 = p . Field<string> ( "IJD002" )
                          } into m
                          select new
                          {
                              ijd002 = m . Key . p1 ,
                              count = m . Count ( )
                          };
                if ( que != null )
                {
                    foreach ( var x in que )
                    {
                        if ( x . count > 1 )
                        {
                            XtraMessageBox . Show ( "工号:" + x . ijd002 + "重复,请核实" );
                            result = false;
                            break;
                        }
                    }
                    if ( result == false )
                        return false;
                }
            }

            _header . IJA001 = txtIJA001 . Text;
            _header . IJA002 = txtIJA002 . Text;
            _header . IJA003 = txtIJA004 . EditValue . ToString ( );
            _header . IJA004 = txtIJA004 . Text;
            _header . IJA005 = txtIJA006 . EditValue . ToString ( );
            _header . IJA006 = txtIJA006 . Text;
            _header . IJA007 = Convert . ToDateTime ( txtIJA007 . Text );
            _header . IJA008 = txtIJA008 . Text;
            _header . IJA009 = /*txtIJA009 . Text;*/string . Empty;
            _header . IJA010 = _header . IJA011 = false;
            _header . IJA012 = string . IsNullOrEmpty ( txtIJA012 . Text ) == true ? 0 : Convert . ToDecimal ( txtIJA012 . Text );
            _header . IJA013 = string . IsNullOrEmpty ( txtIJA013 . Text ) == true ? 0 : Convert . ToDecimal ( txtIJA013 . Text );

            return result;
        }
        void printOrExport ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtIJA001 . Text );
            tablePrintOne . TableName = "TableOne";
            if ( txtIJA002 . Text . Equals ( "计件" ) )
                tablePrintTwo = _bll . getTablePrintTwo ( txtIJA001 . Text );
            else
                tablePrintTwo = _bll . getTablePrintTre ( txtIJA001 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void calcuSumTime ( )
        {
            if ( txtIJA002 . Text == string . Empty )
                return;

            DateTime dtOne, dtTwo;
            decimal u0 = 0M;

            decimal ija012 = txtIJA012 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtIJA012 . Text ) * 60;
            decimal ija013 = txtIJA013 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtIJA013 . Text ) * 60;

            if ( txtIJA002 . Text . Equals ( "计件" ) )
            {
                bandedGridView1 . CloseEditor ( );
                bandedGridView1 . UpdateCurrentRow ( );

                if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                    return;

                foreach ( DataRow row in tableViewOne . Rows )
                {
                    if ( string . IsNullOrEmpty ( row [ "IJB003" ] . ToString ( ) ) || row [ "IJB003" ] . ToString ( ) . Equals ( "离职" ) || row [ "IJB003" ] . ToString ( ) . Equals ( "未上班" ) )
                    {
                        row [ "IJB025" ] = 0;
                        row [ "IJB023" ] = 0;
                        row [ "IJB024" ] = 0;
                        continue;
                    }

                    u0 = 0;
                    if ( !string . IsNullOrEmpty ( row [ "IJB016" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "IJB017" ] . ToString ( ) ) )
                    {
                        dtOne = Convert . ToDateTime ( row [ "IJB016" ] );
                        dtTwo = Convert . ToDateTime ( row [ "IJB017" ] );
                        //判断开始上班时间和中午休息时间、下午下班时间
                        u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                        if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                        {
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija012 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                            if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                                u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija012 ) - Convert . ToDecimal ( ija013 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        }
                        else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija013 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                        row [ "IJB024" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    }
                    else
                        row [ "IJB024" ] = 0;
                    u0 = 0;
                    if ( !string . IsNullOrEmpty ( row [ "IJB018" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "IJB019" ] . ToString ( ) ) )
                    {
                        dtOne = Convert . ToDateTime ( row [ "IJB018" ] );
                        dtTwo = Convert . ToDateTime ( row [ "IJB019" ] );
                        //判断开始上班时间和中午休息时间、下午下班时间
                        u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                        if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                        {
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija012 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                            if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                                u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija012 ) - Convert . ToDecimal ( ija013 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        }
                        else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija013 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                        row [ "IJB023" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    }
                    else
                        row [ "IJB023" ] = 0;

                    row [ "IJB025" ] = Math . Round ( string . IsNullOrEmpty ( row [ "IJB011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB011" ] . ToString ( ) ) * ( string . IsNullOrEmpty ( row [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB015" ] . ToString ( ) ) ) ,1 ,MidpointRounding . AwayFromZero ) + Math . Round ( string . IsNullOrEmpty ( row [ "IJB023" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB023" ] . ToString ( ) ) * ( string . IsNullOrEmpty ( row [ "IJB020" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJB020" ] . ToString ( ) ) ) ,1 ,MidpointRounding . AwayFromZero );
                }
            }
            else if ( txtIJA002 . Text . Equals ( "计时" ) )
            {
                gridView4 . CloseEditor ( );
                gridView4 . UpdateCurrentRow ( );

                if ( tableViewTre == null || tableViewTre . Rows . Count < 1 )
                    return;

                foreach ( DataRow row in tableViewTre . Rows )
                {

                    if ( string . IsNullOrEmpty ( row [ "IJD010" ] . ToString ( ) ) || row [ "IJD010" ] . ToString ( ) . Equals ( "离职" ) || row [ "IJD010" ] . ToString ( ) . Equals ( "未上班" ) )
                    {
                        row [ "IJD012" ] = 0;
                        row [ "IJD013" ] = 0;
                        continue;
                    }

                    u0 = 0;
                    if ( !string . IsNullOrEmpty ( row [ "IJD006" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "IJD007" ] . ToString ( ) ) )
                    {
                        dtOne = Convert . ToDateTime ( row [ "IJD006" ] );
                        dtTwo = Convert . ToDateTime ( row [ "IJD007" ] );
                        //判断开始上班时间和中午休息时间、下午下班时间
                        u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                        if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                        {
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija012 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                            if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                                u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija012 ) - Convert . ToDecimal ( ija013 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        }
                        else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ija013 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                        row [ "IJD012" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                    }
                    else
                        row [ "IJD012" ] = 0;

                    row [ "IJD013" ] = Math . Round ( string . IsNullOrEmpty ( row [ "IJD012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD012" ] . ToString ( ) ) * ( string . IsNullOrEmpty ( row [ "IJD008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "IJD008" ] . ToString ( ) ) ) ,1 ,MidpointRounding . AwayFromZero );
                }
            }
        }
        void addRow ( string column ,int selectIdx )
        {
            if ( selectIdx < 0 )
                return;
            //tableViewOne
            if ( selectIdx < tableViewOne . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableViewOne . Rows . Count ; i++ )
                {
                    row = tableViewOne . Rows [ i ];
                    if ( i + 1 != tableViewOne . Rows . Count )
                    {
                        if ( txtIJA002 . Text . Equals ( "计件" ) )
                        {
                            if ( column . Equals ( "IJB016" ) )
                            {
                                if ( workShopTime . startTime ( row ,row [ "IJB016" ] ,"IJB017" ,"IJB018" ,"IJB019" ) )
                                {
                                    ro = tableViewOne . Rows [ i + 1 ];
                                    if ( ro [ "IJB016" ] == null || ro [ "IJB016" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "IJB016" ] = row [ "IJB016" ];
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "IJB017" ) )
                            {
                                if ( workShopTime . endTime ( row ,row [ "IJB017" ] ,"IJB016" ,"IJB018" ,"IJB019" ) )
                                {
                                    ro = tableViewOne . Rows [ i + 1 ];
                                    if ( ro [ "IJB017" ] == null || ro [ "IJB017" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "IJB017" ] = row [ "IJB017" ];
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ( column . Equals ( "IJB018" ) )
                            {
                                if ( workShopTime . startCenTime ( row ,row [ "IJB018" ] ,"IJB017" ,"IJB019" ,"IJB016" ) )
                                {
                                    ro = tableViewOne . Rows [ i + 1 ];
                                    if ( ro [ "IJB018" ] == null || ro [ "IJB018" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "IJB018" ] = row [ "IJB018" ];
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "IJB019" ) )
                            {
                                if ( workShopTime . endCenTime ( row ,row [ "IJB019" ] ,"IJB018" ,"IJB016" ,"IJB017" ) )
                                {
                                    ro = tableViewOne . Rows [ i + 1 ];
                                    if ( ro [ "IJB019" ] == null || ro [ "IJB019" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "IJB019" ] = row [ "IJB019" ];
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
            calcuSumTime ( );
        }
        void addRowTime ( string column ,int selectIdx ,object value )
        {
            if ( selectIdx < 0 )
                return;
            if ( selectIdx < tableViewTre . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableViewTre . Rows . Count ; i++ )
                {
                    row = tableViewTre . Rows [ i ];
                    if ( i + 1 != tableViewTre . Rows . Count )
                    {
                        if ( column . Equals ( "IJD006" ) )
                        {
                            ro = tableViewTre . Rows [ i + 1 ];
                            if ( row [ "IJD006" ] != null && row [ "IJD006" ] . ToString ( ) != string . Empty && ( ( row [ "IJD007" ] == null || row [ "IJD007" ] . ToString ( ) == string . Empty ) || Convert . ToDateTime ( row [ "IJD006" ] ) < Convert . ToDateTime ( row [ "IJD007" ] ) ) )
                            {
                                if ( ro [ "IJD006" ] == null || ro [ "IJD006" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "IJD006" ] = /*row [ "IJD006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "IJD007" ) )
                        {
                            ro = tableViewTre . Rows [ i + 1 ];
                            if ( row [ "IJD007" ] != null && row [ "IJD007" ] . ToString ( ) != string . Empty && ( ( row [ "IJD006" ] == null || row [ "IJD006" ] . ToString ( ) == string . Empty ) || Convert . ToDateTime ( row [ "IJD006" ] ) < Convert . ToDateTime ( row [ "IJD007" ] ) ) )
                            {
                                if ( ro [ "IJD007" ] == null || ro [ "IJD007" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "IJD007" ] = /*row [ "IJD007" ];*/ value;
                                    ro . EndEdit ( );
                                }

                            }
                        }
                    }
                }
            }
            gridControl3 . RefreshDataSource ( );
            calcuSumTime ( );
        }
        void editOther ( string orderNum,string proNum )
        {
            if ( "计件" . Equals ( txtIJA002 . Text ) )
            {
                bandedGridView1 . CloseEditor ( );
                bandedGridView1 . UpdateCurrentRow ( );

                _bodyOne . IJB003 = txtIJA001 . Text;
                _bodyOne . IJB004 = orderNum;
                _bodyOne . IJB005 = proNum;

                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    _bodyOne . IJB004 = tableViewOne . Rows [ 0 ] [ "IJB004" ] . ToString ( );
                    _bodyOne . IJB005 = tableViewOne . Rows [ 0 ] [ "IJB005" ] . ToString ( );
                }

                tableOtherSur = _bll . getTableSur ( _bodyOne . IJB004 ,_bodyOne . IJB005 ,_bodyOne . IJB003 );
                editOtherSurPre ( );
            }
            else if ( "计时" . Equals ( txtIJA002 . Text ) )
            {
                gridView2 . CloseEditor ( );
                gridView2 . UpdateCurrentRow ( );

                _bodyTwo . IJC005 = txtIJA001 . Text;
                _bodyTwo . IJC002 = orderNum;
                _bodyTwo . IJC003 = proNum;

                if ( _bodyTwo . IJC002 == string . Empty || _bodyTwo . IJC003 == string . Empty )
                {
                    if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in tableViewTwo . Rows )
                        {
                            _bodyTwo . IJC002 = row [ "IJC002" ] . ToString ( );
                            _bodyTwo . IJC003 = row [ "IJC003" ] . ToString ( );
                            tableOtherSur = _bll . getTableSurTime ( _bodyTwo . IJC005 ,_bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                            editOtherSurPreTime ( _bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                        }
                    }
                    else
                    {
                        tableOtherSur = _bll . getTableSurTime ( _bodyTwo . IJC005 ,_bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                        editOtherSurPreTime ( _bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                    }
                }
                else
                {
                    tableOtherSur = _bll . getTableSurTime ( _bodyTwo . IJC005 ,_bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                    editOtherSurPreTime ( _bodyTwo . IJC002 ,_bodyTwo . IJC003 );
                }


            }
        }
        void editOtherSurPre ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                return;
            if ( tableOtherSur == null || tableOtherSur . Rows . Count < 1 )
            {
                foreach ( DataRow row in tableViewOne . Rows )
                {
                    row [ "U4" ] = row [ "IJB010" ];
                }
            }
            else
            {
                foreach ( DataRow row in tableViewOne . Rows )
                {
                    row [ "U4" ] = string . IsNullOrEmpty ( tableOtherSur . Rows [ 0 ] [ "IJB015" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( tableOtherSur . Rows [ 0 ] [ "IJB015" ] . ToString ( ) );                }
            }
        }
        void editOtherSurPreTime ( string orderNum ,string proNum )
        {
            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;
            if ( tableOtherSur == null || tableOtherSur . Rows . Count < 1 )
            {
                DataRow row = tableViewTwo . Select ( "IJC002='" + orderNum + "' AND IJC003='" + proNum + "'" ) [ 0 ];
                if ( row == null )
                    return;
                row [ "U4" ] = row [ "IJC008" ];
            }
            else
            {
                DataRow row = tableViewTwo . Select ( "IJC002='" + orderNum + "' AND IJC003='" + proNum + "'" ) [ 0 ];
                if ( row == null )
                    return;

                row [ "U4" ] = string . IsNullOrEmpty ( tableOtherSur . Rows [ 0 ] [ "IJC010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( tableOtherSur . Rows [ 0 ] [ "IJC010" ] . ToString ( ) );
            }
        }
        #endregion

        #region ThreadMethod
        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void ThreadProcSafePost ( )
        {
            tableProduce = _bll . getTablePInfo ( );
            //在线程中更新UI（通过UI线程同步上下文m_SyncContext）
            m_SyncContext . Post ( SetTextSafePost ,tableProduce );
            tableUser = _bll . getUserInfo ( );
            m_SyncContext . Post ( SetUser ,tableUser );
        }
        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetTextSafePost ( object table )
        {
            EditGD . DataSource = table;
            EditGD . DisplayMember = "RAA001";
            EditGD . ValueMember = "RAA001";

            tableProduct = ( DataTable ) table;
            EditGD2 . DataSource = tableProduct;
            EditGD2 . DisplayMember = "RAA001";
            EditGD2 . ValueMember = "RAA001";
        }
        private void SetUser ( object table )
        {
            EditUser . DataSource = table;
            EditUser . DisplayMember = "EMP001";
            EditUser . ValueMember = "EMP001";

            tablePer = ( DataTable ) table;
            EditPer . DataSource = tablePer;
            EditPer . DisplayMember = "EMP001";
            EditPer . ValueMember = "EMP001";
        }
        #endregion

    }
}