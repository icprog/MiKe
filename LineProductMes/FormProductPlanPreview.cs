using System . Data;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using System;
using System . ComponentModel;
using System . Windows . Forms;

namespace LineProductMes
{
    //MIKPRF
    public partial class FormProductPlanPreview :FormChild
    {
        LineProductMesEntityu.ProductPlanPreviewEntity model;
        LineProductMesBll.Bll.ProductPlanPreviewBll _bll;

        DataTable tableView,tableViewCopy;
        DataRow row;
        DateTime dt;

        bool result=false,check=false;

        string columnName=string.Empty,focuseName=string.Empty;
        int selectIdx=0,selectColumn=0;

        public FormProductPlanPreview ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . ProductPlanPreviewBll ( );
            model = new LineProductMesEntityu . ProductPlanPreviewEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1} );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolAdd } );

            dt = LineProductMesBll . UserInfoMation . sysTime;
            dtStart . Text =dt . ToString ( "yyyy-MM-dd" );
            dtEnd . Text = Convert . ToDateTime ( dt ) . AddMonths ( 3 ) . ToString ( "yyyy-MM-dd" );

            txtPRE . Properties . DataSource = _bll . getProPlanInfo ( );
            txtPRE . Properties . DisplayMember = "DEA002";
            txtPRE . Properties . ValueMember = "PRE004";
        }

        #region Main
        protected override int Query ( )
        {
            DateTime dtStartT = dt, dtEndT = dt . AddMonths ( 3 );

            if ( !string . IsNullOrEmpty ( dtStart . Text ) )
                dtStartT = Convert . ToDateTime ( dtStart . Text );

            if ( !string . IsNullOrEmpty ( dtEnd . Text ) )
                dtEndT = Convert . ToDateTime ( dtEnd . Text );

            tableView = _bll . getTableView ( txtPRE.Text ,dtStartT ,dtEndT );
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
            addCount ( );
            tableViewCopy = tableView . Copy ( );
            QueryTool ( );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            controlUnEnable ( );

            return base . Query ( );
        }
        protected override int Export ( )
        {
            ViewExport . ExportToExcel ( this . Text ,gridControl1 );

            return base . Export ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            editTool ( );

            tableView = null;
            tableView = tableViewCopy . Copy ( );
            gridControl1 . DataSource = tableView;
            gridControl1 . RefreshDataSource ( );

            return base . Edit ( );
        }
        protected override int Save ( )
        {
            if ( getValue ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            result = _bll . Save ( tableView );
            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                 ClassForMain.FormClosingState.formClost = true;
                controlUnEnable ( );
                saveTool ( );
            }
            else
            {
                 ClassForMain.FormClosingState.formClost = false;
                XtraMessageBox . Show ( "保存失败" );
            }

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            controlUnEnable ( );
            cancelltionTool ( "edit" );

            gridControl1 . DataSource = tableViewCopy;
            gridControl1 . RefreshDataSource ( );

            return base . Cancel ( );
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
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
        }
        private void btnRead_Click ( object sender ,System . EventArgs e )
        {
            result = _bll . ReadPlanData ( );
            if ( result )
            {
                XtraMessageBox . Show ( "读取成功" );
                Query ( );
            }
            else
                XtraMessageBox . Show ( "读取失败" );
        }
        private void btnAdd_Click ( object sender ,EventArgs e )
        {
            FormProductPlan from = new FormProductPlan ( null );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                Query ( );
            }
        }
        private void btnEdit_Click ( object sender ,EventArgs e )
        {
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择需要编辑的内容" );
                return ;
            }

            FormProductPlan from = new FormProductPlan ( row );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                Query ( );
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( row == null )
                return;
            if ( model . PRF003 == 0 )
                return;
            DataRow r = gridView1 . GetDataRow ( selectIdx );
            if ( row == null )
                return;
            int changedResult = string . IsNullOrEmpty ( e . Value . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( e . Value );
            if ( model . PRF003 < changedResult )
                return;
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            int nextColumn = selectColumn + 1;
            if ( nextColumn > gridView1 . Columns . Count - 1 )
                return;
            string coluName = gridView1 . Columns [ nextColumn ] . FieldName;
            string nextValue = row [ coluName ] . ToString ( );
            r [ coluName ] = model . PRF003 - changedResult + ( string . IsNullOrEmpty ( nextValue ) == true ? 0 : Convert . ToInt32 ( nextValue ) );
            check = false;
        }
        private void gridView1_CellValueChanging ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            check = true;
            selectIdx = e . RowHandle;
            selectColumn = e . Column . VisibleIndex;
        }
        private void gridView1_MouseEnter ( object sender ,EventArgs e )
        {
            if ( check == false )
            {
                row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                
                columnName = gridView1 . FocusedColumn . FieldName;
                if ( columnName != "主件品号" && columnName != "主件品名" && columnName != "订单量" && columnName != "预计生产量" && columnName != "排产量" && columnName != "库存量" && columnName != "库存可用量" && columnName != "未排量" && columnName != "生产车间" && columnName != "仓库" && columnName != "单位" && columnName!= "DX$CheckboxSelectorColumn" )
                    model . PRF003 = string . IsNullOrEmpty ( row [ columnName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ columnName ] . ToString ( ) );
            }
        }
        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
        }
        private void btnLine_Click ( object sender ,EventArgs e )
        {
            int [ ] selectRows = gridView1 . GetSelectedRows ( );
            if ( selectRows . Length < 1 )
            {
                XtraMessageBox . Show ( "请选择需要排产的产品" );
                return;
            }
            string productName = string . Empty;
            foreach ( int i in selectRows )
            {
                DataRow row = gridView1 . GetDataRow ( i );
                if ( row == null )
                    continue;
                if ( productName == string . Empty )
                    productName = "'" + row [ "主件品号" ] . ToString ( ) + "'";
                else
                    productName = productName + "," + "'" + row [ "主件品号" ] . ToString ( ) + "'";
            }
            FormLineForAssPlan from = new FormLineForAssPlan ( productName );
            from . ShowDialog ( );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if (  tableView == null || tableView . Rows . Count < 1 )
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
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        #endregion

        #region Method
        void addCount ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;

            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;

            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                column . AppearanceCell . Options . UseTextOptions = true;
                column . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                column . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                column . BestFit ( );
                column . Summary . Clear ( );
                column . OptionsColumn . AllowEdit = false;
                if ( column . FieldName != "主件品号" && column . FieldName != "主件品名" && column . FieldName != "订单量" && column . FieldName != "预计生产量" && column . FieldName != "排产量" && column . FieldName != "库存量" && column . FieldName != "库存可用量" && column . FieldName != "未排量" && column . FieldName != "生产车间" && column . FieldName != "仓库" && column . FieldName != "单位" )
                {
                    object obj = tableView . Compute ( "COUNT([" + column . FieldName + "])" ,"[" + column . FieldName + "]>0" );
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,obj . ToString ( ) );
                    column . OptionsColumn . AllowEdit = true;
                    column . Width = 45;
                    if ( ( Convert . ToDateTime ( column . FieldName ) - dt ) . Days < 0 )
                        column . OptionsColumn.AllowEdit = false;
                }
                else if ( column . FieldName == "主件品号" )
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,"合计" );
                else if ( column . FieldName == "订单量" )
                    column . ToolTip = "同品号所有未结束订单总量";
                else if ( column . FieldName == "预计生产量" )
                    column . ToolTip = "工单单头未完成量=计划生产量-已完工量";
                else if ( column . FieldName == "库存量" )
                    column . ToolTip = "现有库存量";
                else if ( column . FieldName == "库存可用量" )
                    column . ToolTip = "库存量+预计生产量-订单量";
                else if ( column . FieldName == "未排量" )
                    column . ToolTip = "订单量-排产量";
            }
        }
        void controlUnEnable ( )
        {
            gridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            gridView1 . OptionsBehavior . Editable = true;
        }
        bool getValue ( )
        {
            result = true;

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            int total = 0, totalValue = 0;
            foreach ( DataRow row in tableView . Rows )
            {
                total = 0;
                foreach ( DataColumn column in tableView . Columns )
                {
                    if ( row [ column ] != null && row [ column ] . ToString ( ) != string . Empty && column . ColumnName != "主件品号" && column . ColumnName != "主件品名" && column . ColumnName != "排产量" && column . ColumnName != "订单量" && column . ColumnName != "预计生产量" && column . ColumnName != "库存量" && column . ColumnName != "库存可用量" && column . ColumnName != "未排量" && column . ColumnName != "生产车间" && column . ColumnName != "仓库" && column . ColumnName != "单位" )
                        total += Convert . ToInt32 ( row [ column ] );
                }
                totalValue = Convert . ToInt32 ( row [ "排产量" ] );
                if ( totalValue != total )
                {
                    XtraMessageBox . Show ( "主件品号:" + row [ "主件品号" ] + "\n\r总排产量:" + totalValue + "\n\r您的排量:" + total + "\n\r请核实" ,"提示" );
                    result = false;
                    break;
                }
            }

            return result;
        }
        #endregion

    }
}