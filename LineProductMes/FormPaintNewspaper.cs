using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Linq;
using DevExpress . XtraEditors;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using System . Threading;
using System . Windows . Forms;

namespace LineProductMes
{
    public partial class FormPaintNewspaper :FormChild
    {
        LineProductMesEntityu.PaintNewspaperHeaderEntity _header;
        LineProductMesEntityu.PaintNewspaperBodyOneEntity _bodyOne;
        LineProductMesEntityu.PaintNewspaperBodyTwoEntity _bodyTwo;
        LineProductMesBll.Bll.PaintNewspaperBll _bll;

        DataTable tableViewOne,tableViewTwo,tableProduct,tableWorker,tableUser,tableArt,tablePrintOne,tablePrintTwo,tableOtherSur,tablePrintTre,tablePrintFor,tablePrintFiv;
        DataRow row;

        string strWhere="1=1",state=string.Empty,focuseName=string.Empty;
        bool result=false;

        Thread thread;
        SynchronizationContext m_SyncContext = null;
        List<string> idxListOne=new List<string>();
        List<string> idxListTwo=new List<string>();

        DateTime dt,dtStart,dtEnd;

        public FormPaintNewspaper ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . PaintNewspaperBll ( );
            _header = new LineProductMesEntityu . PaintNewspaperHeaderEntity ( );
            _bodyOne = new LineProductMesEntityu . PaintNewspaperBodyOneEntity ( );
            _bodyTwo = new LineProductMesEntityu . PaintNewspaperBodyTwoEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,gridView1 ,View1 ,View2 ,View3 ,View4 ,View5 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,gridView1 ,View1 ,View2 ,View3 ,View4 ,View5 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            dt1 . VistaEditTime = dt2 . VistaEditTime = dt3 . VistaEditTime = dt4 . VistaEditTime = txtPAN015 . Properties . VistaEditTime = txtPAN016 . Properties . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            controlClear ( );
            controlUnEnable ( );

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext . Current;
            thread = new Thread ( new ThreadStart ( ThreadPost ) );
            thread . Start ( );

            dt = LineProductMesBll . UserInfoMation . sysTime;
            dtStart = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 08:00" ) );
            dtEnd = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:00" ) );
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormPaintNewspaperQuery from = new ChildForm . FormPaintNewspaperQuery ( );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                _header . PAN001 = from . getOdd;

                _header = _bll . getModel ( _header . PAN001 );
                setValue ( );

                strWhere = "1=1";
                strWhere += " AND PAO001='" + _header . PAN001 + "'";
                tableViewOne = _bll . tableViewOne ( strWhere );
                gridControl1 . DataSource = tableViewOne;
                strWhere = "1=1";
                strWhere += " AND PAP001='" + _header . PAN001 + "'";
                tableViewTwo = _bll . tableViewTwo ( strWhere );
                gridControl2 . DataSource = tableViewTwo;

                //calcuSalaryNumSum ( );
                calcuSalarySum ( );
                //calcuSalaryTimeSum ( );
                calcuTimeSum ( );
                //calcuSalaaryEveryWork ( );

                editOtherSur ( string . Empty ,string . Empty );

                controlUnEnable ( );

                QueryTool ( );
            }
            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            txtPAN001 . Text = _bll . getOddNum ( );
            state = "add";
            tableViewOne = _bll . tableViewOne ( "1=2" );
            gridControl1 . DataSource = tableViewOne;
            tableViewTwo = _bll . tableViewTwo ( "1=2" );
            gridControl2 . DataSource = tableViewTwo;
            txtPAN003 . EditValue = "0503";
            txtPAN013 . Text = "计件";
            txtPAN006 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );
            addTool ( );
            txtPAN015 . Text = dtStart . ToString ( );
            txtPAN016 . Text = dtEnd . ToString ( );

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
            if ( string . IsNullOrEmpty ( txtPAN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }

            result = _bll . Delete ( txtPAN001 . Text );
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
            if ( string . IsNullOrEmpty ( txtPAN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . PAN009 = true;
            else
                _header . PAN009 = false;
            result = _bll . Exanmie ( txtPAN001 . Text ,_header . PAN009 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    //Graph . grPicS ( pictureEdit1 ,"审 核" );
                    Graph . grPic ( pictureEdit1 ,"审核" );
                }
                else
                {
                    layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicS ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtPAN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . PAN010 = true;
            else
                _header . PAN010 = false;
            result = _bll . CancelTion ( txtPAN001 . Text ,_header . PAN010 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    //Graph . grPicS ( pictureEdit1 ,"注 销" );
                    Graph . grPic ( pictureEdit1 ,"注销" );
                }
                else
                {
                    layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicS ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        protected override int PrintWork ( )
        {
            getPrintOrExport ( );
            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . PrintWork ( );
        }
        protected override int ExportWork ( )
        {
            getPrintOrExport ( );
            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . ExportWork ( );
        }
        protected override int PrintReport ( )
        {
            printOrExport ( );

            Print ( new DataTable [ ] { tablePrintTre ,tablePrintFor ,tablePrintFiv } ,"喷漆报工单.frx" );

            return base . PrintReport ( );
        }
        protected override int ExportReport ( )
        {
            printOrExport ( );

            Export ( new DataTable [ ] { tablePrintTre ,tablePrintFor ,tablePrintFiv } ,"喷漆报工单.frx" );

            return base . ExportReport ( );
        }
        #endregion

        #region Event
        private void txtPAN003_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtPAN003 . EditValue == null || txtPAN003 . EditValue . ToString ( ) == String . Empty )
                return;

            txtPAN005 . Properties . DataSource = _bll . getDepart ( txtPAN003 . EditValue . ToString ( ) );
            txtPAN005 . Properties . DisplayMember = "DAA002";
            txtPAN005 . Properties . ValueMember = "DAA001";

            txtPAN005 . EditValue = "0503";
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableViewOne ,tableViewTwo );
            else
                result = _bll . Edit ( _header ,tableViewOne ,tableViewTwo ,idxListOne ,idxListTwo );
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
                    saveTool ( );
                    controlUnEnable ( );
                    if ( state . Equals ( "add" ) )
                        _header . PAN001 = txtPAN001 . Text = LineProductMesBll . UserInfoMation . oddNum;
                    else
                        _header . PAN001 = txtPAN001 . Text;

                    setValue ( );
                    strWhere = "1=1";
                    strWhere += " AND PAO001='" + _header . PAN001 + "'";
                    tableViewOne = _bll . tableViewOne ( strWhere );
                    gridControl1 . DataSource = tableViewOne;
                    strWhere = "1=1";
                    strWhere += " AND PAP001='" + _header . PAN001 + "'";
                    tableViewTwo = _bll . tableViewTwo ( strWhere );
                    gridControl2 . DataSource = tableViewTwo;

                    //calcuSalaryNumSum ( );
                    calcuSalarySum ( );
                    //calcuSalaryTimeSum ( );
                    calcuTimeSum ( );
                    //calcuSalaaryEveryWork ( );

                    editOtherSur ( string . Empty ,string . Empty );
                }
                else
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败" );
                }
            }
        }
        private void Edit1_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "PAO002":
                if ( edit . EditValue == null )
                    return;
                if ( tableProduct . Select ( "RAA001='" + edit . EditValue + "'" ) . Length < 1 )
                    return;
                row = tableProduct . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row [ "RAA008" ] == null || row [ "RAA008" ] . ToString ( ) == string . Empty )
                    return;
                _bodyOne . PAO002 = edit . EditValue . ToString ( );
                _bodyOne . PAO003 = row [ "RAA015" ] . ToString ( );
                _bodyOne . PAO004 = row [ "DEA002" ] . ToString ( );
                _bodyOne . PAO005 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
                _bodyOne . PAO006 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );
                _bodyOne . PAO007 = row [ "DEA003" ] . ToString ( );
                _bodyOne . PAO008 = row [ "DEA057" ] . ToString ( );
                if ( tableViewOne . Select ( "PAO002='" + _bodyOne . PAO002 + "' AND PAO003='" + _bodyOne . PAO003 + "'" ) . Length > 0 )
                    return;
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO003" ] ,_bodyOne . PAO003 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO004" ] ,_bodyOne . PAO004 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO005" ] ,_bodyOne . PAO005 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO006" ] ,_bodyOne . PAO006 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO007" ] ,_bodyOne . PAO007 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO008" ] ,_bodyOne . PAO008 );
                //tableArt = _bll . tableArt ( _bodyOne . PAO003 );
                //Edit2 . DataSource = tableArt;
                //Edit2 . DisplayMember = "ART002";
                //Edit2 . ValueMember = "ART002";
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO002" ] ,_bodyOne . PAO002 );


                DataTable tableArt = _bll . getTableArtForAll ( _bodyOne . PAO003 );
                if ( tableArt == null || tableArt . Rows . Count < 1 )
                    return;

                gridView1 . CloseEditor ( );
                gridView1 . UpdateCurrentRow ( );

                //DataRow rw;

                if ( tableViewOne . Select ( "PAO002='" + _bodyOne . PAO002 + "' AND PAO003='" + _bodyOne . PAO003 + "'" ) . Length > 0 )
                {
                    if ( tableArt != null && tableArt . Rows . Count > 0 )
                    {
                        _bodyOne . PAO009 = string . Empty;
                        foreach ( DataRow row in tableArt . Rows )
                        {
                            if ( _bodyOne . PAO009 == string . Empty )
                                _bodyOne . PAO009 = row [ "ART" ] . ToString ( );
                            else
                                _bodyOne . PAO009 = _bodyOne . PAO009 + " | " + row [ "ART" ] . ToString ( );
                        }
                    }

                    DataRow rows = tableViewOne . Select ( "PAO002='" + _bodyOne . PAO002 + "' AND PAO003='" + _bodyOne . PAO003 + "'" )[0];
                    rows [ "PAO010" ] = _bodyOne . PAO009;
                    //int i = 0;
                    //foreach ( DataRow row in rows )
                    //{
                    //    if ( row [ "PAO010" ] == null || row [ "PAO010" ] . ToString ( ) == string . Empty )
                    //    {
                    //        if ( tableArt != null && tableArt . Rows . Count > 0 && i < tableArt . Rows . Count )
                    //        {
                    //            rw = tableArt . Rows [ i ];
                    //            row [ "PAO010" ] = rw [ "ART" ];
                    //        }
                    //    }
                    //    i++;
                    //}
                    //if ( i <= tableArt . Rows . Count - 1 )
                    //{
                    //    DataRow row = tableViewOne . Select ( "PAO002='" + _bodyOne . PAO002 + "' AND PAO003='" + _bodyOne . PAO003 + "'" ) [ 0 ];
                    //    DataRow r;
                    //    for ( int j = i ; j < tableArt . Rows . Count ; j++ )
                    //    {
                    //        rw = tableArt . Rows [ j ];
                    //        r = tableViewOne . NewRow ( );
                    //        r [ "PAO002" ] = row [ "PAO002" ];
                    //        r [ "PAO003" ] = row [ "PAO003" ];
                    //        r [ "PAO004" ] = row [ "PAO004" ];
                    //        r [ "PAO005" ] = row [ "PAO005" ];
                    //        r [ "PAO006" ] = row [ "PAO006" ];
                    //        r [ "PAO007" ] = row [ "PAO007" ];
                    //        r [ "PAO008" ] = row [ "PAO008" ];
                    //        r [ "PAO009" ] = rw [ "ART002" ];
                    //        r [ "PAO010" ] = rw [ "ART003" ];
                    //        r [ "PAO011" ] = rw [ "ART004" ];
                    //        r [ "PAO013" ] = rw [ "ART011" ];
                    //        tableViewOne . Rows . Add ( r );
                    //    }
                    //}
                }

                editOtherSur ( _bodyOne . PAO002 ,_bodyOne . PAO003 );

                gridControl1 . RefreshDataSource ( );

                break;
            }
        }
        private void Edit2_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "PAO009":
                if ( edit . EditValue == null )
                    return;
                if ( tableArt . Select ( "ART002='" + edit . EditValue + "'" ) . Length < 1 )
                    return;
                row = tableArt . Select ( "ART002='" + edit . EditValue + "'" ) [ 0 ];
                _bodyOne . PAO010 = row [ "ART003" ] . ToString ( );
                _bodyOne . PAO011 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO010" ] ,_bodyOne . PAO010 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "PAO011" ] ,_bodyOne . PAO011 );
                break;
            }
        }
        private void Edit3_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "PPA002":
                if ( edit . EditValue == null )
                    return;
                if ( tableUser . Select ( "EMP001='" + edit . EditValue + "'" ) . Length < 1 )
                    return;
                row = tableUser . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                _bodyTwo . PPA003 = row [ "EMP002" ] . ToString ( );
                _bodyTwo . PPA004 = row [ "EMP007" ] . ToString ( );
                _bodyTwo . PPA011 = row [ "DAA002" ] . ToString ( );
                if ( tableViewTwo . Select ( "PPA002='" + edit . EditValue + "'" ) . Length > 0 )
                    return;
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "PPA003" ] ,_bodyTwo . PPA003 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "PPA004" ] ,_bodyTwo . PPA004 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "PPA010" ] ,"在职" );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "PPA011" ] ,_bodyTwo . PPA011 );
                break;
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "PAO011" || e . Column . FieldName == "PAO012" )
            {
                calcuTimeSum ( );
                //calcuSalaaryEveryWork ( );
            }
        }
        private void txtu0_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void txtu1_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalarySum ( );
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            DataRow rows = bandedGridView1 . GetFocusedDataRow ( );
            if ( rows == null )
                return;
            if ( e . Column . FieldName == "PPA002" || e . Column . FieldName == "PPA003" || e . Column . FieldName == "PPA004" || e . Column . FieldName == "PPA011" )
            {
                if ( rows [ "PPA007" ] == null || rows [ "PPA007" ] . ToString ( ) == string . Empty )
                {
                    rows [ "PPA007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                }
                if ( rows [ "PPA008" ] == null || rows [ "PPA008" ] . ToString ( ) == string . Empty )
                {
                    rows [ "PPA008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                }
                if ( rows [ "PPA010" ] == null || rows [ "PPA010" ] . ToString ( ) == string . Empty )
                {
                    rows [ "PPA010" ] = "在职";
                }
            }
            if ( e . Column . FieldName == "PPA010" )
            {
                _bodyTwo . PPA010 = rows [ "PPA010" ] . ToString ( );
                if ( _bodyTwo . PPA010 == string . Empty || _bodyTwo . PPA010 . Equals ( "离职" ) || _bodyTwo . PPA010 . Equals ( "未上班" ) )
                {
                    rows [ "PPA005" ] = DBNull . Value;
                    rows [ "PPA006" ] = DBNull . Value;
                    rows [ "PPA007" ] = DBNull . Value;
                    rows [ "PPA008" ] = DBNull . Value;
                    rows [ "PPA012" ] = DBNull . Value;
                    rows [ "PPA013" ] = DBNull . Value;
                }
                else if ( _bodyTwo . PPA010 . Equals ( "在职" ) )
                {
                    if ( rows [ "PPA007" ] == null || rows [ "PPA007" ] . ToString ( ) == string . Empty )
                    {
                        rows [ "PPA007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( rows [ "PPA008" ] == null || rows [ "PPA008" ] . ToString ( ) == string . Empty )
                    {
                        rows [ "PPA008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                calcuTimeSum ( );
            }
            if ( e . Column . FieldName == "PPA009" )
            {
                int selectIndex = bandedGridView1 . FocusedRowHandle;
                string ppa009Result = bandedGridView1 . GetDataRow ( selectIndex ) [ "PPA009" ] . ToString ( );

                if ( string . IsNullOrEmpty ( ppa009Result ) )
                    _bodyTwo . PPA009 = 0;
                else
                    _bodyTwo . PPA009 = Convert . ToDecimal ( ppa009Result );

                for ( int i = selectIndex ; i < tableViewTwo . Rows . Count ; i++ )
                {
                    row = tableViewTwo . Rows [ i ];
                    if ( row [ "PPA013" ] != null && row [ "PPA013" ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ "PPA009" ] == null || row [ "PPA009" ] . ToString ( ) == string . Empty )
                        {
                            row . BeginEdit ( );
                            row [ "PPA009" ] = _bodyTwo . PPA009;
                            row . EndEdit ( );
                        }
                    }
                    if ( i == selectIndex && ( row [ "PPA013" ] == null || row [ "PPA013" ] . ToString ( ) == string . Empty ) )
                    {
                        row . BeginEdit ( );
                        row [ "PPA009" ] = DBNull . Value;
                        row . EndEdit ( );
                    }
                }
                gridControl2 . Refresh ( );

                calcuTimeSum ( );
            }
            else if ( e . Column . FieldName == "PPA005" )
            {
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( rows [ "PPA006" ] != null && rows [ "PPA006" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) > Convert . ToDateTime ( rows [ "PPA006" ] ) )
                        {
                            rows [ "PPA005" ] = DBNull . Value;
                        }
                    }
                    if ( rows [ "PPA007" ] != null && rows [ "PPA007" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) > Convert . ToDateTime ( rows [ "PPA007" ] ) )
                        {
                            rows [ "PPA005" ] = DBNull . Value;
                        }
                    }
                }
                //calcuTimeSum ( );
                addRow ( "PPA005" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "PPA006" )
            {
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( rows [ "PPA005" ] != null && rows [ "PPA005" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) < Convert . ToDateTime ( rows [ "PPA005" ] ) )
                        {
                            rows [ "PPA006" ] = DBNull . Value;
                        }
                    }
                    if ( rows [ "PPA007" ] != null && rows [ "PPA007" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) > Convert . ToDateTime ( rows [ "PPA007" ] ) )
                        {
                            rows [ "PPA006" ] = DBNull . Value;
                        }
                    }
                }
                //calcuTimeSum ( );
                addRow ( "PPA006" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "PPA007" )
            {
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( rows [ "PPA008" ] != null && rows [ "PPA008" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) > Convert . ToDateTime ( rows [ "PPA008" ] ) )
                        {
                            rows [ "PPA007" ] = DBNull . Value;
                        }
                    }
                    if ( rows [ "PPA006" ] != null && rows [ "PPA006" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) < Convert . ToDateTime ( rows [ "PPA006" ] ) )
                        {
                            rows [ "PPA007" ] = DBNull . Value;
                        }
                    }
                }
                //calcuTimeSum ( );
                addRow ( "PPA007" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "PPA008" )
            {
                if ( e . Value != null && e . Value . ToString ( ) != string . Empty )
                {
                    if ( rows [ "PPA007" ] != null && rows [ "PPA007" ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( e . Value ) < Convert . ToDateTime ( rows [ "PPA007" ] ) )
                        {
                            rows [ "PPA008" ] = DBNull . Value;
                        }
                    }
                }
                //calcuTimeSum ( );
                addRow ( "PPA008" ,e . RowHandle ,e . Value );
            }
        }
        private void txtu2_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalaaryEveryWork ( );
        }
        private void txtu3_TextChanged ( object sender ,EventArgs e )
        {
            calcuSalaaryEveryWork ( );
        }
        private void txtPAN005_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtPAN005 . EditValue == null || txtPAN005 . EditValue . ToString ( ) == string . Empty )
                return;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            DataRow [ ] rows = tableUser . Select ( "EMP005='" + txtPAN005 . EditValue + "'" );
            if ( rows == null )
                return;
            if ( tableViewTwo == null )
                return;
            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
            {
                foreach ( DataRow r in rows )
                {
                    row = tableViewTwo . NewRow ( );
                    row [ "PPA002" ] = r [ "EMP001" ];
                    row [ "PPA003" ] = r [ "EMP002" ];
                    row [ "PPA004" ] = r [ "EMP007" ];
                    row [ "PPA011" ] = r [ "DAA002" ];
                    row [ "PPA007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    row [ "PPA008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    row [ "PPA010" ] = "在职";
                    tableViewTwo . Rows . Add ( row );
                }
            }
            else
            {
                foreach ( DataRow r in rows )
                {
                    if ( tableViewTwo . Select ( "PPA002='" + r [ "EMP001" ] + "'" ) . Length < 1 )
                    {
                        row = tableViewTwo . NewRow ( );
                        row [ "PPA002" ] = r [ "EMP001" ];
                        row [ "PPA003" ] = r [ "EMP002" ];
                        row [ "PPA004" ] = r [ "EMP007" ];
                        row [ "PPA011" ] = r [ "DAA002" ];
                        row [ "PPA007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                        row [ "PPA008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        row [ "PPA010" ] = "在职";
                        tableViewTwo . Rows . Add ( row );
                    }
                }
            }
            calcuTimeSum ( );
        }
        private void btnEdit_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( row [ "PAO003" ] == null || row [ "PAO003" ] . ToString ( ) == string . Empty )
                return;
            DataTable tableArt = _bll . getTableArt ( row [ "PAO003" ] . ToString ( ) );
            if ( tableArt == null || tableArt . Rows . Count < 1 )
                return;
            ChildForm . PaintNewsArt from = new ChildForm . PaintNewsArt ( row [ "PAO003" ] . ToString ( ) ,tableArt );
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                DataRow rows = from . getRow;
                if ( rows == null )
                    return;
                row [ "PAO009" ] = rows [ "ART002" ];
                row [ "PAO010" ] = rows [ "ART003" ];
                row [ "PAO011" ] = rows [ "ART004" ];
                row [ "PAO013" ] = rows [ "ART011" ];
                gridControl1 . RefreshDataSource ( );
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtPAN003 . Text == string . Empty || tableViewOne == null || tableViewOne . Rows . Count < 1 )
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
        private void textEdit2_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
            //calcuSalaryTimeSum ( );
        }
        private void textEdit1_TextChanged ( object sender ,EventArgs e )
        {
            calcuTimeSum ( );
            //calcuSalaryTimeSum ( );
        }
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U5" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        private void contextMenuStrip2_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focuseName );
        }
        private void bandedGridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "PPA014" )
            {
                if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDecimal ( e . CellValue ) >= 200 )
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                }
            }
        }
        private void txtPAN013_SelectedValueChanged ( object sender ,EventArgs e )
        {
            updateBatchTime ( );
        }
        private void gridControl1_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( e . KeyChar == ( char ) Keys . Enter )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                DataRow row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyOne . idx > 0 && !idxListOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                    idxListOne . Add ( _bodyOne . idx . ToString ( ) );
                tableViewOne . Rows . Remove ( row );
                gridControl1 . Refresh ( );
            }
        }
        private void gridControl2_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( e . KeyChar == ( char ) Keys . Enter )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                DataRow row = bandedGridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyOne . idx > 0 && !idxListTwo . Contains ( _bodyTwo . idx . ToString ( ) ) )
                    idxListTwo . Add ( _bodyTwo . idx . ToString ( ) );
                tableViewTwo . Rows . Remove ( row );
                gridControl2 . Refresh ( );
            }
        }
        private void txtPAN016_EditValueChanged ( object sender ,EventArgs e )
        {
            updateBatchTime ( );
        }
        void updateBatchTime ( )
        {
            if ( txtPAN013 . Text == string . Empty )
                return;

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;

            if ( !string . IsNullOrEmpty ( txtPAN015 . Text ) )
                dtStart = Convert . ToDateTime ( txtPAN015 . Text );
            if ( !string . IsNullOrEmpty ( txtPAN016 . Text ) )
                dtEnd = Convert . ToDateTime ( txtPAN016 . Text );

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                 if ( txtPAN013 . Text . Equals ( "计件" ) )
                {
                    row [ "PPA005" ] = dtStart;
                    row [ "PPA006" ] = dtEnd;
                    row [ "PPA007" ] = DBNull . Value;
                    row [ "PPA008" ] = DBNull . Value;
                    row [ "PPA013" ] = DBNull . Value;
                }
                else if ( txtPAN013 . Text . Equals ( "计时" ) )
                {
                    row [ "PPA005" ] = DBNull . Value;
                    row [ "PPA006" ] = DBNull . Value;
                    row [ "PPA012" ] = DBNull . Value;
                    row [ "PPA007" ] = dtStart;
                    row [ "PPA008" ] = dtEnd;
                    //row [ "PPA013" ] = DBNull . Value;
                }
                calcuTimeSum ( );
            }
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
             txtPAN003 . ReadOnly = txtPAN005 . ReadOnly = txtPAN007 . ReadOnly =txtPAN011.ReadOnly=txtPAN012.ReadOnly=txtPAN013.ReadOnly= txtPAN015 . ReadOnly = txtPAN016 . ReadOnly = true;
            bandedGridView1 . OptionsBehavior . Editable = gridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtPAN003 . ReadOnly = txtPAN005 . ReadOnly = txtPAN007 . ReadOnly = txtPAN011 . ReadOnly = txtPAN012 . ReadOnly = txtPAN013 . ReadOnly = txtPAN015 . ReadOnly = txtPAN016 . ReadOnly = false;
            bandedGridView1 . OptionsBehavior . Editable = gridView1 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtPAN001 . Text = txtPAN003 . Text = txtPAN005 . Text = txtPAN006 . Text = txtPAN007 . Text = txtPAN011 . Text = txtPAN012 . Text = txtu0 . Text = txtu1 . Text = txtu2 . Text = txtu3 . Text =txtPAN013.Text= txtPAN015 . Text = txtPAN016 . Text = string . Empty;
            gridControl1 . DataSource = gridControl2 . DataSource = null;
            layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            wait . Hide ( );
        }
        void ThreadPost ( )
        {
            tableWorker = _bll . getDepart ( "05" );
            m_SyncContext . Post ( InitData ,tableWorker );
            tableProduct = _bll . getTablePInfo ( );
            m_SyncContext . Post ( InitDataProduct ,tableProduct );
            tableUser = _bll . getUserInfo ( );
            m_SyncContext . Post ( InitDataUser ,tableUser );
        }
        void InitData ( object table )
        {
            txtPAN003 . Properties . DataSource = table;
            txtPAN003 . Properties . DisplayMember = "DAA002";
            txtPAN003 . Properties . ValueMember = "DAA001";
        }
        void InitDataProduct ( object table )
        {
            Edit1 . DataSource = table;
            Edit1 . DisplayMember = "RAA001";
            Edit1 . ValueMember = "RAA001";
        }
        void InitDataUser ( object table )
        {
            Edit3 . DataSource = table;
            Edit3 . DisplayMember = "EMP001";
            Edit3 . ValueMember = "EMP001";
        }
        void setValue ( )
        {
            txtPAN001 . Text = _header . PAN001;
            txtPAN003 . EditValue = _header . PAN002;
            txtPAN005 . EditValue = _header . PAN004;
            txtPAN006 . Text = Convert . ToDateTime ( _header . PAN006 ) . ToString ( "yyyy-MM-dd" );
            txtPAN007 . Text = _header . PAN007;
            //txtPAN008 . Text = _header . PAN008;
            txtPAN011 . Text = Convert . ToDecimal ( _header . PAN011 ) . ToString ( "0.#" );
            txtPAN012 . Text = Convert . ToDecimal ( _header . PAN012 ) . ToString ( "0.#" );
            txtPAN013 . Text = _header . PAN013;
            txtPAN015 . Text = _header . PAN015 . ToString ( );
            txtPAN016 . Text = _header . PAN016 . ToString ( );

            layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPic ( pictureEdit1 ,"反" );
            if ( _header . PAN009 )
            {
                layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                //Graph . grPicS ( pictureEdit1 ,"审 核" );
                Graph . grPic ( pictureEdit1 ,"审核" );
                examineTool ( "审核" );
            }
            else
                examineTool ( "反审核" );
            if ( _header . PAN010 )
            {
                layoutControlItem11 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                //Graph . grPicS ( pictureEdit1 ,"注 销" );
                Graph . grPic ( pictureEdit1 ,"注销" );
                cancelltionTool ( "注销" );
            }
            else
                cancelltionTool ( "反注销" );
        }
        bool getValue ( )
        {
            result = true;

            if ( string . IsNullOrEmpty ( txtPAN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtPAN003 . Text ) )
            {
                XtraMessageBox . Show ( "生产车间不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtPAN005 . Text ) )
            {
                XtraMessageBox . Show ( "班组不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtPAN013 . Text ) )
            {
                XtraMessageBox . Show ( "工资类型不可为空" );
                return false;
            }

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );
            if ( txtPAN013 . Text . Equals ( "计件" ) )
            {
                if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                {
                    XtraMessageBox . Show ( "请选择来源工单等信息" );
                    return false;
                }
            }

            gridView1 . ClearColumnErrors ( );

            for ( int i = 0 ; i < gridView1 . RowCount ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                if ( row == null )
                    continue;
                if ( txtPAN013 . Text . Equals ( "计件" ) )
                {
                    if ( row [ "PAO002" ] == null || row [ "PAO002" ] . ToString ( ) == string . Empty )
                    {
                        row . SetColumnError ( "PAO002" ,"不可为空" );
                        result = false;
                        break;
                    }
                    //if ( row [ "PAO013" ] == null || row [ "PAO013" ] . ToString ( ) == string . Empty )
                    //{
                    //    row . SetColumnError ( "PAO013" ,"不可为空" );
                    //    result = false;
                    //    break;
                    //}
                    if ( row [ "PAO012" ] == null || row [ "PAO012" ] . ToString ( ) == string . Empty )
                    {
                        row . SetColumnError ( "PAO012" ,"不可为空" );
                        result = false;
                        break;
                    }
                }

                _bodyOne . PAO005 = string . IsNullOrEmpty ( row [ "U5" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "U5" ] . ToString ( ) );
                _bodyOne . PAO012 = Convert . ToInt32 ( row [ "PAO012" ] . ToString ( ) );
                if ( _bodyOne . PAO012 > _bodyOne . PAO005 )
                {
                    XtraMessageBox . Show ( "来源工单:" + row [ "PAO002" ] + "\n\r序号:" + row [ "PAO013" ] + "\n\r完工数量多于未完工数量,请核实" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return false;

            bandedGridView1 . ClearColumnErrors ( );
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                if ( row [ "PPA002" ] == null || row [ "PPA002" ] . ToString ( ) == string . Empty )
                {
                    bandedGridView1 . SetColumnError ( PPA002 ,"不可为空" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            var quer = from p in tableViewTwo . AsEnumerable ( )
                       group p by new
                       {
                           p1 = p . Field<string> ( "PPA002" )
                       }
                     into m
                       select new
                       {
                           ppa002 = m . Key . p1 ,
                           count = m . Count ( )
                       };

            if ( quer != null )
            {
                foreach ( var x in quer )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . ppa002 + "重复,请核实" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            _bodyOne . PAO001 = workShopTime . checkUserForOtherWork ( txtPAN006 . Text ,tableViewTwo ,LineProductMesBll . ObtainInfo . codeNin ,txtPAN001 . Text );
            if ( !string . IsNullOrEmpty ( _bodyOne . PAO001 ) )
            {
                XtraMessageBox . Show ( _bodyOne . PAO001 ,"提示" );
                return false;
            }

            if ( string . IsNullOrEmpty ( txtPAN015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择开工日期" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtPAN016 . Text ) )
            {
                XtraMessageBox . Show ( "请选择完工日期" );
                return false;
            }
            _header . PAN015 = Convert . ToDateTime ( txtPAN015 . Text );
            _header . PAN016 = Convert . ToDateTime ( txtPAN016 . Text );
            _header . PAN001 = txtPAN001 . Text;
            _header . PAN002 = txtPAN003 . EditValue . ToString ( );
            _header . PAN003 = txtPAN003 . Text;
            _header . PAN004 = txtPAN005 . EditValue . ToString ( );
            _header . PAN005 = txtPAN005 . Text;
            _header . PAN006 = Convert . ToDateTime ( txtPAN006 . Text );
            _header . PAN007 = txtPAN007 . Text;
            _header . PAN008 = /*txtPAN008 . Text;*/string . Empty;
            _header . PAN009 = _header . PAN010 = false;
            _header . PAN011 = string . IsNullOrEmpty ( txtPAN011 . Text ) == true ? 0 : Convert . ToDecimal ( txtPAN011 . Text );
            _header . PAN012 = string . IsNullOrEmpty ( txtPAN012 . Text ) == true ? 0 : Convert . ToDecimal ( txtPAN012 . Text );
            _header . PAN013 = txtPAN013 . Text;

            return result;
        }
        void calcuSalaryNumSum ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( "计件" . Equals ( txtPAN013 . Text ) )
                txtu0 . Text = U0 . SummaryItem . SummaryValue == null ? 0 . ToString ( ) : Convert . ToDecimal ( U0 . SummaryItem . SummaryValue ) . ToString ( "0.#" );
            else
                txtu0 . Text = 0 . ToString ( );
        }
        void calcuSalarySum ( )
        {
            txtu2 . Text = ( ( string . IsNullOrEmpty ( txtu0 . Text ) == true ? 0 : Convert . ToDecimal ( txtu0 . Text ) ) + ( string . IsNullOrEmpty ( txtu1 . Text ) == true ? 0 : Convert . ToDecimal ( txtu1 . Text ) ) ) . ToString ( "0.##" );
        }
        void calcuSalaryTimeSum ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            txtu1 . Text = U3 . SummaryItem . SummaryValue == null ? 0 . ToString ( ) :Convert.ToDecimal( U3 . SummaryItem . SummaryValue) . ToString ( "0.#" );
            calcuSalaaryEveryWork ( );
        }
        void calcuTimeSum ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;

            decimal pan011 = txtPAN011 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtPAN011 . Text );
            decimal pan012 = txtPAN012 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtPAN012 . Text );

            DateTime dtOne, dtTwo;
            decimal u0 = 0M;

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "PPA010" ] . ToString ( ) ) || row [ "PPA010" ] . ToString ( ) . Equals ( "离职" ) || row [ "PPA010" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "PPA012" ] = 0;
                    row [ "PPA013" ] = 0;
                    row [ "PPA014" ] = 0;
                    continue;
                }

                if ( !string . IsNullOrEmpty ( row [ "PPA005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "PPA006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "PPA005" ] );
                    dtTwo = Convert . ToDateTime ( row [ "PPA006" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );

                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( pan011 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( pan011 ) - Convert . ToDecimal ( pan012 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( pan012 );

                    row [ "PPA012" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "PPA012" ] = 0;
                if ( !string . IsNullOrEmpty ( row [ "PPA007" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "PPA008" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "PPA007" ] );
                    dtTwo = Convert . ToDateTime ( row [ "PPA008" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours );

                    if ( dtOne . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 11:00" ) ) ) <= 0 && dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 12:00" ) ) ) >= 0 )
                    {
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( pan011 );
                        if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                            u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( pan011 ) - Convert . ToDecimal ( pan012 );
                    }
                    else if ( dtTwo . CompareTo ( Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:30" ) ) ) > 0 /*dtTwo . Hour >= 17 && dtTwo . Minute >= 30*/ )
                        u0 = Convert . ToDecimal ( ( dtTwo - dtOne ) . TotalHours ) - Convert . ToDecimal ( pan012 );

                    row [ "PPA013" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "PPA013" ] = 0;
            }

            txtu3 . Text = U4 . SummaryItem . SummaryValue == null ? 0 . ToString ( ) : Convert . ToDecimal ( U4 . SummaryItem . SummaryValue ) . ToString ( "0.#" );
            calcuSalaaryEveryWork ( );
            calcuSalaryTimeSum ( );
            calcuSalaryNumSum ( );
        }
        void calcuSalaaryEveryWork ( )
        {
            txtu4 . Text = ( ( string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text ) ) * Convert . ToDecimal ( 0.05 ) ) . ToString ( "0.######" );

            decimal timeSum = 0, timeAll = 0, salarySum = 0M;

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            string piNum = string . Empty;

            Dictionary<string ,decimal> gs = new Dictionary<string ,decimal> ( );

            if ( "计件" . Equals ( txtPAN013 . Text ) )
            {
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    foreach ( DataRow row in tableViewOne . Rows )
                    {
                        if ( piNum == string . Empty )
                            piNum = "'" + row [ "PAO003" ] + "'";
                        else
                            piNum = piNum + "," + "'" + row [ "PAO003" ] + "'";
                    }
                    DataTable tableA = _bll . getTableA ( piNum );
                    if ( tableA != null || tableA . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in tableA . Rows )
                        {
                            _bodyOne . PAO009 = row [ "ART001" ] . ToString ( );
                            _bodyOne . PAO013 = row [ "ART013" ] . ToString ( );
                            _bodyOne . PAO011 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] );
                            DataRow rowSe = tableViewOne . Select ( "PAO003='" + _bodyOne . PAO009 + "'" ) [ 0 ];
                            _bodyOne . PAO012 = Convert . ToInt32 ( rowSe [ "PAO012" ] );
                            if ( gs . ContainsKey ( _bodyOne . PAO013 ) )
                                gs [ _bodyOne . PAO013 ] = Convert . ToDecimal ( _bodyOne . PAO011 * _bodyOne . PAO012 ) * Convert . ToDecimal ( 0.95 ) + gs [ _bodyOne . PAO013 ];
                            else
                                gs . Add ( _bodyOne . PAO013 ,Convert . ToDecimal ( _bodyOne . PAO011 * _bodyOne . PAO012 * Convert . ToDecimal ( 0.95 ) ) );
                        }
                    }
                }
            }

            if ( tableViewTwo != null && tableViewTwo . Rows . Count > 0 )
            {
                Dictionary<string ,decimal> ut = new Dictionary<string ,decimal> ( );
                foreach ( DataRow row in tableViewTwo . Rows )
                {
                    _bodyTwo . PPA010 = row [ "PPA010" ] . ToString ( );
                    if ( _bodyTwo . PPA010 != string . Empty && _bodyTwo . PPA010.Trim() . Equals ( "在职" ) )
                    {
                        _bodyTwo . PPA004 = row [ "PPA004" ] . ToString ( );
                        _bodyTwo . PPA014 = string . IsNullOrEmpty ( row [ "PPA009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA009" ] ) * ( string . IsNullOrEmpty ( row [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA013" ] ) ) * Convert . ToDecimal ( 0.95 );
                        if ( gs . ContainsKey ( _bodyTwo . PPA004 ) )
                            gs [ _bodyTwo . PPA004 ] = Convert . ToDecimal ( _bodyTwo . PPA014 ) + gs [ _bodyTwo . PPA004 ];
                        else
                            gs . Add ( _bodyTwo . PPA004 ,Convert . ToDecimal ( _bodyTwo . PPA014 ) );

                        _bodyTwo . PPA014 = ( string . IsNullOrEmpty ( row [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA012" ] ) + ( string . IsNullOrEmpty ( row [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA013" ] ) ) );
                        if ( ut . ContainsKey ( _bodyTwo . PPA004 ) )
                            ut [ _bodyTwo . PPA004 ] = Convert . ToDecimal ( _bodyTwo . PPA014 ) + ut [ _bodyTwo . PPA004 ];
                        else
                            ut . Add ( _bodyTwo . PPA004 ,Convert . ToDecimal ( _bodyTwo . PPA014 ) );
                    }
                }

                if ( gs . Count < 1 )
                    return;

                foreach ( DataRow row in tableViewTwo . Rows )
                {
                    _bodyTwo . PPA010 = row [ "PPA010" ] . ToString ( );
                    _bodyTwo . PPA002 = row [ "PPA002" ] . ToString ( );
                    _bodyTwo . PPA004 = row [ "PPA004" ] . ToString ( );
                    timeSum = ( string . IsNullOrEmpty ( row [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA012" ] ) + ( string . IsNullOrEmpty ( row [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "PPA013" ] ) ) );
                    if ( _bodyTwo . PPA010 != string . Empty && _bodyTwo . PPA010.Trim() . Equals ( "在职" ) )
                    {
                        salarySum = gs [ _bodyTwo . PPA004 ];
                        timeAll = 0;
                        if ( ut . Count > 0 )
                            timeAll = ut [ _bodyTwo . PPA004 ];
                        row [ "PPA014" ] = timeAll == 0 ? 0 . ToString ( ) : ( salarySum / timeAll * timeSum ) . ToString ( "0.##" );
                    }
                }

            }

        }
        void calcuSalaryEveryone ( )
        {
            txtu4 . Text = ( ( string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text ) ) * Convert . ToDecimal ( 0.05 ) ) . ToString ( "0.######" );

            decimal timeSum = 0;
            decimal timeAll = 0; /*string . IsNullOrEmpty ( txtu3 . Text ) == true ? 0 : Convert . ToDecimal ( txtu3 . Text );*/
            decimal salarySum = 0M; /*string . IsNullOrEmpty ( txtu2 . Text ) == true ? 0 : Convert . ToDecimal ( txtu2 . Text );*/

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                return;

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;

            if ( "计件" . Equals ( txtPAN013 . Text ) )
            {
                var query = from p in tableViewOne . AsEnumerable ( )
                            group p by new
                            {
                                p1 = p . Field<string> ( "PAO003" )
                            } into m
                            select new
                            {
                                pao003 = m . Key . p1
                            };
                if ( query == null )
                    return;
                string piNum = string . Empty;
                foreach ( var x in query )
                {
                    if ( string . IsNullOrEmpty ( piNum ) )
                        piNum = "'" + x . pao003 + "'";
                    else
                        piNum = piNum + "," + "'" + x . pao003 + "'";
                }

                DataTable tableA = _bll . getTableA ( piNum );
                if ( tableA == null || tableA . Rows . Count < 1 )
                    return;

                var que = from p in tableViewTwo . AsEnumerable ( )
                          group p by new
                          {
                              p1 = p . Field<string> ( "PPA004" )
                          } into m
                          select new
                          {
                              ppa004 = m . Key . p1
                          };

                if ( que == null )
                    return;

                foreach ( var re in que )
                {
                    salarySum = 0;
                    DataRow [ ] rows = tableA . Select ( "ART013='" + re . ppa004 + "'" );
                    if ( rows == null )
                        continue;
                    foreach ( DataRow row in rows )
                    {
                        _bodyOne . PAO003 = row [ "ART001" ] . ToString ( );
                        _bodyOne . PAO009 = row [ "ART011" ] . ToString ( );
                        DataRow [ ] ros = tableViewOne . Select ( "PAO003='" + _bodyOne . PAO003 + "' AND PAO013='" + _bodyOne . PAO009 + "'" );
                        if ( ros == null )
                            continue;
                        foreach ( DataRow ro in ros )
                        {
                            salarySum += string . IsNullOrEmpty ( ro [ "PAO011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( ro [ "PAO011" ] ) * ( string . IsNullOrEmpty ( ro [ "PAO012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( ro [ "PAO012" ] ) );
                        }
                    }
                    DataRow [ ] rowes = tableViewTwo . Select ( "PPA004='" + re . ppa004 + "'" );
                    if ( rows == null )
                        continue;
                    timeAll = 0;
                    foreach ( DataRow r in rowes )
                    {
                        timeAll += ( string . IsNullOrEmpty ( r [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA012" ] ) + ( string . IsNullOrEmpty ( r [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA013" ] ) ) );
                        salarySum += ( string . IsNullOrEmpty ( r [ "PPA009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA009" ] ) * ( string . IsNullOrEmpty ( r [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA013" ] ) ) );
                    }
                    foreach ( DataRow r in rowes )
                    {
                        if ( string . IsNullOrEmpty ( r [ "PPA010" ] . ToString ( ) ) || r [ "PPA010" ] . ToString ( ) . Equals ( "离职" ) || r [ "PPA010" ] . ToString ( ) . Equals ( "未上班" ) )
                        {
                            r [ "PPA012" ] = 0;
                            r [ "PPA013" ] = 0;
                            r [ "PPA014" ] = 0;
                            continue;
                        }

                        timeSum = ( string . IsNullOrEmpty ( r [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA012" ] ) + ( string . IsNullOrEmpty ( r [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA013" ] ) ) );

                        r [ "PPA014" ] = timeAll == 0 ? 0 . ToString ( ) : ( salarySum / timeAll * timeSum ) . ToString ( );
                    }
                }
            }
            else if ( "计时" . Equals ( txtPAN013 . Text ) )
            {
                timeAll = salarySum = 0;
                foreach ( DataRow r in tableViewTwo.Rows )
                {
                    timeAll += ( string . IsNullOrEmpty ( r [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA012" ] ) + ( string . IsNullOrEmpty ( r [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA013" ] ) ) );
                    salarySum += ( string . IsNullOrEmpty ( r [ "PPA009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA009" ] ) * ( string . IsNullOrEmpty ( r [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA013" ] ) ) );
                }
                foreach ( DataRow r in tableViewTwo . Rows )
                {
                    if ( string . IsNullOrEmpty ( r [ "PPA010" ] . ToString ( ) ) || r [ "PPA010" ] . ToString ( ) . Equals ( "离职" ) || r [ "PPA010" ] . ToString ( ) . Equals ( "未上班" ) )
                    {
                        r [ "PPA012" ] = 0;
                        r [ "PPA013" ] = 0;
                        r [ "PPA014" ] = 0;
                        continue;
                    }

                    timeSum = ( string . IsNullOrEmpty ( r [ "PPA012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA012" ] ) + ( string . IsNullOrEmpty ( r [ "PPA013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( r [ "PPA013" ] ) ) );

                    r [ "PPA014" ] = timeAll == 0 ? 0 . ToString ( ) : ( salarySum / timeAll * timeSum ) . ToString ( );
                }
            }
        }
        void getPrintOrExport ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtPAN001 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtPAN001 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void printOrExport ( )
        {
            tablePrintTre = _bll . getPrintTre ( txtPAN001 . Text );
            tablePrintTre . TableName = "TableOne";
            tablePrintFor = _bll . getPrintFor ( txtPAN001 . Text );
            tablePrintFor . TableName = "TableTwo";
            tablePrintFiv = _bll . getPrintFiv ( txtPAN001 . Text );
            tablePrintFiv . TableName = "TableTre";
        }
        void addRow ( string column ,int selectIdx,object value )
        {
            if ( selectIdx < 0 )
                return;
            if ( selectIdx < tableViewTwo . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableViewTwo . Rows . Count ; i++ )
                {
                    row = tableViewTwo . Rows [ i ];
                    if ( i + 1 != tableViewTwo . Rows . Count )
                    {
                        if ( column . Equals ( "PPA005" ) )
                        {
                            if ( workShopTime . startTime ( row ,row [ "PPA005" ] ,"PPA006" ,"PPA007" ,"PPA008" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "PPA005" ] == null || ro [ "PPA005" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "PPA005" ] = /*row [ "PPA005" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "PPA006" ) )
                        {
                            if ( workShopTime . endTime ( row ,row [ "PPA006" ] ,"PPA005" ,"PPA007" ,"PPA008" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "PPA006" ] == null || ro [ "PPA006" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "PPA006" ] =/* row [ "PPA006" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        if ( column . Equals ( "PPA007" ) )
                        {
                            if ( workShopTime . startCenTime ( row ,row [ "PPA007" ] ,"PPA006" ,"PPA008" ,"PPA005" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "PPA007" ] == null || ro [ "PPA007" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "PPA007" ] = /*row [ "PPA007" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                        else if ( column . Equals ( "PPA008" ) )
                        {
                            if ( workShopTime . endCenTime ( row ,row [ "PPA008" ] ,"PPA007" ,"PPA005" ,"PPA006" ) )
                            {
                                ro = tableViewTwo . Rows [ i + 1 ];
                                if ( ro [ "PPA008" ] == null || ro [ "PPA008" ] . ToString ( ) == string . Empty )
                                {
                                    ro . BeginEdit ( );
                                    ro [ "PPA008" ] =/* row [ "PPA008" ];*/value;
                                    ro . EndEdit ( );
                                }
                            }
                        }
                    }
                }
            }
            gridControl2 . RefreshDataSource ( );
            calcuTimeSum ( );
        }
        void editOtherSur ( string orderNum,string proNum )
        {
            _bodyOne . PAO001 = txtPAN001 . Text;
            _bodyOne . PAO002 = orderNum;
            _bodyOne . PAO003 = proNum;

            if ( _bodyOne . PAO002 == string . Empty || _bodyOne . PAO003 == string . Empty )
            {
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    var query = from p in tableViewOne . AsEnumerable ( )
                                group p by new
                                {
                                    p1 = p . Field<string> ( "PAO002" ) ,
                                    p2 = p . Field<string> ( "PAO003" )
                                } into m
                                select new
                                {
                                    pao002 = m . Key . p1 ,
                                    pao003 = m . Key . p2
                                };
                    if ( query != null )
                    {
                        foreach ( var x in query )
                        {
                            _bodyOne . PAO002 = x . pao002;
                            _bodyOne . PAO003 = x . pao003;
                            tableOtherSur = _bll . getTableOtherSur ( _bodyOne );
                            DataRow [ ] rows = tableViewOne . Select ( "PAO002='" + _bodyOne . PAO002 + "' AND PAO003='" + _bodyOne . PAO003 + "'" );
                            if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                            {
                                foreach ( DataRow row in rows )
                                {
                                    row [ "U5" ] = tableOtherSur . Rows[ 0 ] [ "PAO" ];
                                }
                            }
                            else
                            {
                                foreach ( DataRow row in rows )
                                {
                                    row [ "U5" ] = row [ "PAO005" ];
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne );
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewOne . Select ( "PAO002='" + _bodyOne . PAO002 + "' AND PAO003='" + _bodyOne . PAO003 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U5" ] = tableOtherSur . Rows [ 0 ] [ "PAO" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U5" ] = row [ "PAO005" ];
                        }
                    }
                }
            }
        }
        #endregion

    }
}





