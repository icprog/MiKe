using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using LineProductMesBll;
using System;
using System . Data;
using System . Linq;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class FormSemiPlanView :FormChild
    {
        LineProductMesBll.Bll.SemiProductPlanBll _bll=null;
        DataTable tableView,tableCalcu;
        DateTime dt;
        string focuseName=string.Empty;


        public FormSemiPlanView ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . SemiProductPlanBll ( );

           

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolCanecl ,toolSave ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 } );

            toolAdd . Caption = "半成品计划";

            dt = LineProductMesBll . UserInfoMation . sysTime;
            dtStart . Text = dt . ToString ( "yyyy-MM-dd" );
            dtEnd . Text = dt . AddMonths ( 3 ) . ToString ( "yyyy-MM-dd" );

            txtPRE . Properties . DataSource = _bll . getProTableMS ( );
            txtPRE . Properties . DisplayMember = "DEA002";
            txtPRE . Properties . ValueMember = "QAB001";
        }

        protected override int Query ( )
        {
            DateTime dtStartT = dt;
            DateTime dtEndT = dt . AddMonths ( 3 );
            if ( !string . IsNullOrEmpty ( dtStart . Text ) )
                dtStartT = Convert . ToDateTime ( dtStart . Text );
            if ( !string . IsNullOrEmpty ( dtEnd . Text ) )
                dtEndT = Convert . ToDateTime ( dtEnd . Text );

            tableView = _bll . getListAll ( txtPRE . EditValue . ToString ( ) ,dtStartT ,dtEndT );
            if ( tableView != null || tableView . Rows . Count > 0 )
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
            tableCalcu = tableView;
            gridView1 . PopulateColumns ( );
            column ( );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Query ( );
        }
        protected override int Add ( )
        {
            FormSemiProductPlan from = new FormSemiProductPlan ( );
            from . Owner = this;
            from . Show ( );

            return base . Add ( );
        }
        protected override int ExportBase ( )
        {
            ViewExport . ExportToExcel ( this . Text ,this . gridControl1 );

            return base . ExportBase ( );
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
                if ( column . FieldName != "品号" && column . FieldName != "品名" && column . FieldName != "规格" && column . FieldName != "总量" && column . FieldName != "生产部门" && column . FieldName != "仓库" && column . FieldName != "单位" && column . FieldName != "可用库存量" )
                {
                    object obj = tableView . Compute ( "COUNT([" + column . FieldName + "])" ,"[" + column . FieldName + "]>0" );
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,obj . ToString ( ) );
                    column . Width = 45;
                }
            }
        }
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }

        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        int kcl = 0, other = 0;
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            other = 0;
            DataRow row = gridView1 . GetDataRow ( e . RowHandle );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "可用库存量" )
                kcl = string . IsNullOrEmpty ( row [ e . Column . FieldName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ e . Column . FieldName ] );
            if ( e . Column . FieldName != "品号" && e . Column . FieldName != "品名" && e . Column . FieldName != "规格" && e . Column . FieldName != "总量" && e . Column . FieldName != "生产部门" && e . Column . FieldName != "仓库" && e . Column . FieldName != "单位" && e . Column . FieldName != "可用库存量" && e . Column . FieldName != "DX$CheckboxSelectorColumn" )
            {
                if ( Convert . ToDateTime ( e . Column . FieldName ) >= dt && !string . IsNullOrEmpty ( row [ e . Column . FieldName ] . ToString ( ) ) )
                {
                    foreach ( DataColumn colu in tableView . Columns )
                    {
                        if ( colu . ColumnName != "品号" && colu . ColumnName != "品名" && colu . ColumnName != "规格" && colu . ColumnName != "总量" && colu . ColumnName != "生产部门" && colu . ColumnName != "仓库" && colu . ColumnName != "单位" && colu . ColumnName != "可用库存量" && colu . ColumnName != "DX$CheckboxSelectorColumn" )
                        {
                            if ( Convert . ToDateTime ( colu . ColumnName ) <= Convert . ToDateTime ( e . Column . FieldName ) )
                                other += string . IsNullOrEmpty ( row [ colu . ColumnName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ colu . ColumnName ] );
                        }
                    }
                    if ( other > kcl )
                    {
                        e . Appearance . BackColor = System . Drawing . Color . Red;
                        other = 0;
                    }
                }
            }
        }

    }
}