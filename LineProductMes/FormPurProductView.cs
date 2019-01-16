using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Threading;
using System . Threading . Tasks;
using Utility;

namespace LineProductMes
{
    public partial class FormPurProductView :FormChild
    {
        LineProductMesBll.Bll.SemiProductPlanBll _bll=null;
        DataTable tableView;
        DateTime dt;
        string focuseName=string.Empty;

        public FormPurProductView ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . SemiProductPlanBll ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolCanecl ,toolSave  ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 } );

            toolAdd . Caption = "采购计划";

            txtPRE . Properties . DataSource = _bll . getProTableP ( );
            txtPRE . Properties . DisplayMember = "DEA002";
            txtPRE . Properties . ValueMember = "QAB001";

            dt = LineProductMesBll . UserInfoMation . sysTime;
            dtStart . Text = dt . ToString ( "yyyy-MM-dd" );
            dtEnd . Text = dt . AddMonths ( 3 ) . ToString ( "yyyy-MM-dd" );
        }

        protected override int Query ( )
        {
            DateTime dtStartT = dt;
            DateTime dtEndT = dt . AddMonths ( 3 );
            if ( !string . IsNullOrEmpty ( dtStart . Text ) )
                dtStartT = Convert . ToDateTime ( dtStart . Text );
            if ( !string . IsNullOrEmpty ( dtEnd . Text ) )
                dtEndT = Convert . ToDateTime ( dtEnd . Text );

            tableView = _bll . getListPurAll ( txtPRE . EditValue . ToString ( ) ,dtStartT ,dtEndT );
            if ( tableView != null && tableView . Rows . Count > 0 )
            {
                foreach ( DataColumn column in tableView . Columns )
                {
                    if ( column . ColumnName . Contains ( "[" ) || column . ColumnName . Contains ( "]" ) )
                    {
                        column . ColumnName = column . ColumnName . Replace ( '[' ,' ' ) . TrimStart ( );
                        column . ColumnName = column . ColumnName . Replace ( ']' ,' ' ) . TrimEnd ( );
                    }
                }
            }
            gridControl1 . DataSource = tableView;
            gridView1 . PopulateColumns ( );

            column ( );

            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            return base . Query ( );
        }

        protected override int ExportBase ( )
        {
            ViewExport . ExportToExcel ( this . Text ,this . gridControl1 );

            return base . ExportBase ( );
        }

        protected override int Add ( )
        {
            FormPurProductPlan from = new FormPurProductPlan ( );
            from . Show ( );

            return base . Add ( );
        }

        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        void column ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                column . AppearanceCell . Options . UseTextOptions = true;
                column . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                column . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                column . BestFit ( );
                column . Summary . Clear ( );
                if ( column . FieldName != "品号" && column . FieldName != "品名" && column . FieldName != "规格" && column . FieldName != "总量" && column . FieldName != "主要供应商" && column . FieldName != "仓库" && column . FieldName != "单位" && column . FieldName != "可用库存量" )
                {
                    object obj = tableView . Compute ( "COUNT([" + column . FieldName + "])" ,"[" + column . FieldName + "]>0" );
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,obj . ToString ( ) );
                    column . Width = 45;
                }
            }
        }

        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }

        private void contextMenuStrip1_ItemClicked ( object sender ,System . Windows . Forms . ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }

    }
}