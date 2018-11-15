using System . Data;
using System . Windows . Forms;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using System;
using DevExpress . XtraEditors;
using System . Linq;
using System . ComponentModel;

namespace LineProductMes
{
    public partial class FormLineForAssPlan :FormChild
    {
        LineProductMesBll.Bll.LineForAssPlanBll _bll=null;
        LineProductMesEntityu.LineForAssPlanEntity model=null;
        DataTable tableView,table;

        DateTime dtStart,dtEnd;
        string productName=string.Empty,focusedName=string.Empty;


        public FormLineForAssPlan ( string productName )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . LineForAssPlanBll ( );
            model = new LineProductMesEntityu . LineForAssPlanEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolExport ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit ,toolAdd  } );

            this . productName = productName;
        }
        
        private void FormLineForAssPlan_Load ( object sender ,EventArgs e )
        {
            dtStart = LineProductMesBll . UserInfoMation . sysTime;
            dtEnd = dtStart . AddDays ( 8 );
            dtSt . Text = dtStart . AddDays ( 1 ) . ToString ( "yyyy-MM-dd" );
            dtEn . Text = dtEnd . ToString ( "yyyy-MM-dd" );
            InitData ( );
            toolSave . Visibility = toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }

        protected override int Query ( )
        {
            if ( !string . IsNullOrEmpty ( dtSt . Text ) )
                dtStart = Convert . ToDateTime ( dtSt . Text );
            if ( !string . IsNullOrEmpty ( dtEn . Text ) )
                dtEnd = Convert . ToDateTime ( dtEn . Text );
            productName = string . Empty;

            InitData ( );

            return base . Query ( );
        }
        protected override int Save ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( checkTable ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            bool result = _bll . Save ( tableView );
            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                 ClassForMain.FormClosingState.formClost = true;
                this . DialogResult = DialogResult . OK;
            }
            else
            {
                 ClassForMain.FormClosingState.formClost = false;
                XtraMessageBox . Show ( "保存失败,请重试" );
            }

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            this . DialogResult = DialogResult . Cancel;
            return base . Cancel ( );
        }

        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        void InitData ( )
        {
            tableView = _bll . getTableView ( dtStart ,dtEnd ,productName );
            if ( tableView != null && tableView . Rows . Count > 0 )
            {
                foreach ( DataColumn column in tableView . Columns )
                {
                    if ( column . ColumnName . Contains ( "[" ) || column . ColumnName . Contains ( "]" ) )
                    {
                        column . ColumnName = column . ColumnName . Replace ( '[' ,' ' );
                        column . ColumnName = column . ColumnName . Replace ( ']' ,' ' );
                    }
                }
            }
            gridControl1 . DataSource = tableView;
            gridView1 . PopulateColumns ( );
            column ( );
        }
        void column ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            gridView1 . GroupCount = 1;
            gridView1 . OptionsBehavior . AutoExpandAllGroups = true;
            DateTime dt = Convert . ToDateTime ( LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" ) );
          
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                column . AppearanceCell . Options . UseTextOptions = true;
                column . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                column . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                column . BestFit ( );
                column . Summary . Clear ( );
                column . OptionsColumn . AllowEdit = false;

                if ( column . FieldName != "产线编号" && column . FieldName != "产线" && column . FieldName != "主件品号" && column . FieldName != "主件品名" && column . FieldName != "排产量" && column . FieldName != "总排量" )
                {
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Sum ,column . FieldName );
                    column . OptionsColumn . AllowEdit = true;
                    column . Width = 45;
                    if ( ( Convert . ToDateTime ( column . FieldName ) - dt ) . Days <= 0 )
                        column . OptionsColumn . AllowEdit = false;

                    gridView1 . GroupSummary . Add (
           new DevExpress . XtraGrid . GridGroupSummaryItem ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,column ,"{0:N0}" ) );
                }
                else if ( column . FieldName == "产线编号" )
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,"合计" );
                else if ( column . FieldName == "主件品号" )
                    column . GroupIndex = 0;
            }
        }

        private void gridView1_CustomDrawRowFooterCell ( object sender ,DevExpress . XtraGrid . Views . Grid . FooterCellCustomDrawEventArgs e )
        {
            table = _bll . getTableGroupSum ( );
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                if ( e . Column == column && ( column . FieldName != "产线编号" || column . FieldName != "产线" || column . FieldName != "主件品号" || column . FieldName != "主件品名" || column . FieldName != "排产量" || column . FieldName != "总排量" ) )
                {
                    model . PRG001 = gridView1 . GetDataRow ( e . RowHandle ) [ "主件品号" ] . ToString ( );
                    if ( table . Select ( "PRG001='" + model . PRG001 + "' AND PRG002='" + column . FieldName + "'" ) . Length < 1 )
                        return;
                    model . PRG001 = table . Select ( "PRG001='" + model . PRG001 + "' AND PRG002='" + column . FieldName + "'" ) [ 0 ] [ "PRG006" ] . ToString ( );
                    e . Info . DisplayText = model . PRG001;
                }
            }
        }

        bool checkTable ( )
        {
            bool result = true;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                if ( column . FieldName != "产线编号" && column . FieldName != "产线" && column . FieldName != "主件品号" && column . FieldName != "主件品名" && column . FieldName != "排产量" && column . FieldName != "总排量" )
                {
                    if ( ( Convert . ToDateTime ( column . FieldName ) - dt ) . Days >= 0 )
                    {
                        var query = from p in tableView . AsEnumerable ( )
                                    group p by new
                                    {
                                        p1 = p . Field<string> ( "主件品号" )
                                    } into m
                                    let sum = m . Sum ( t => string . IsNullOrEmpty ( t . Field<int?> ( column . FieldName ) . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( t . Field<int?> ( column . FieldName ) ) )
                                    select new
                                    {
                                        zj = m . Key . p1 ,
                                        sum = sum
                                    };
                        if ( query != null )
                        {
                            foreach ( var x in query )
                            {
                                if ( table != null && table . Rows . Count > 0 && table . Select ( "PRG001='" + x . zj + "' AND PRG002='" + column . FieldName + "'" ) . Length > 0 )
                                {
                                    model . PRG003 = Convert . ToInt32 ( table . Select ( "PRG001='" + x . zj + "' AND PRG002='" + column . FieldName + "'" ) [ 0 ] [ "PRG006" ] );
                                    if ( model . PRG003 < x . sum )
                                    {
                                        XtraMessageBox . Show ( "主件品号:" + x . zj + "\n\r排产日期:" + column . FieldName + "\n\r排产数量为:" + model . PRG003 + "\n\r您的排量为:" + x . sum + "\n\r请核实" );
                                        result = false;
                                        break;
                                    }

                                }
                            }
                            if ( result == false )
                                break;
                        }
                    }
                }
            }

            return result;
        }


        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focusedName = e . Column . FieldName;
        }

        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focusedName );
        }

        //protected override void OnClosing ( CancelEventArgs e )
        //{
        //    if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
        //    {
        //        if ( tableView == null || tableView . Rows . Count < 1 )
        //            return;
        //        if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
        //        {
        //            Save ( );
        //            if (  ClassForMain.FormClosingState.formClost == false )
        //                e . Cancel = true;
        //        }
        //    }

        //    base . OnClosing ( e );
        //}

    }
}