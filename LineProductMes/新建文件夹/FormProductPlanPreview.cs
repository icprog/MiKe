using System . Data;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using System;
using System . ComponentModel;
using System . Windows . Forms;
using LineProductMes . ChildForm;
using System . Collections . Generic;
using LineProductMesBll;

namespace LineProductMes
{
    //MIKPRF
    public partial class FormProductPlanPreview :FormChild
    {
        LineProductMesEntityu.ProductPlanPreviewEntity model;
        LineProductMesBll.Bll.ProductPlanPreviewBll _bll;

        DataTable tableView,tableViewCopy,table;
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

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolPrint ,toolExamin ,toolDelete } );

            dt = LineProductMesBll . UserInfoMation . sysTime;
            dtStart . Text =dt . ToString ( "yyyy-MM-dd" );
            dtEnd . Text = Convert . ToDateTime ( dt ) . AddMonths ( 3 ) . ToString ( "yyyy-MM-dd" );

            txtPRE . Properties . DataSource = _bll . getProPlanInfo ( );
            txtPRE . Properties . DisplayMember = "DEA002";
            txtPRE . Properties . ValueMember = "PRE004";

            toolCancellation . Caption = "产线排产";

            table = new DataTable ( );
            table . Columns . Add ( "P1" ,typeof ( System . String ) );
            table . Columns . Add ( "P2" ,typeof ( System . String ) );
            table . Columns . Add ( "P3" ,typeof ( System . DateTime ) );
            table . Columns . Add ( "P4" ,typeof ( System . DateTime ) );
            table . Columns . Add ( "P5" ,typeof ( System . Int32 ) );
            table . Columns . Add ( "P6" ,typeof ( System . Int32 ) );

            controlUnEnable ( );
        }

        #region Main
        protected override int Query ( )
        {
            DateTime dtStartT = dt, dtEndT = dt . AddMonths ( 3 );

            if ( !string . IsNullOrEmpty ( dtStart . Text ) )
                dtStartT = Convert . ToDateTime ( dtStart . Text );

            if ( !string . IsNullOrEmpty ( dtEnd . Text ) )
                dtEndT = Convert . ToDateTime ( dtEnd . Text );

            tableView = _bll . getTableView ( txtPRE . EditValue == null ? string . Empty : txtPRE . EditValue . ToString ( ) ,dtStartT ,dtEndT );
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
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            controlUnEnable ( );

            check = false;

            return base . Query ( );
        }
        protected override int ExportBase ( )
        {
            ViewExport . ExportToExcel ( this . Text ,gridControl1 );

            return base . ExportBase ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            editTool ( );

            check = false;

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
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolCancellation . Caption = "产线排产";
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
            toolCancellation . Caption = "产线排产";

            gridControl1 . DataSource = tableViewCopy;
            gridControl1 . RefreshDataSource ( );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            txtAllow . Text = string . Empty;

            return base . Cancel ( );
        }
        protected override int Add ( )
        {
            btnAdd_Click ( null ,null );

            return base . Add ( );
        }
        protected override int Cancellation ( )
        {
            btnLine_Click ( null ,null );

            return base . Cancellation ( );
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
            UserInfoMation . programName = "FormProductPlan";
            FormProductPlan from = new FormProductPlan ( );
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

            //FormProductPlan from = new FormProductPlan ( row );
            //from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            //if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            //{
            //    Query ( );
            //}
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            columnName = gridView1 . FocusedColumn . FieldName;
            int selectColumnValue = 0, allOfValue = 0;
            if ( columnName == "DX$CheckboxSelectorColumn" )
                return;
            model . PRF003 = 0;
            if ( columnName != "主件品号" && columnName != "主件品名" && columnName != "订单量" && columnName != "预计生产量" && columnName != "排产量" && columnName != "库存量" && columnName != "库存可用量" && columnName != "未排量" && columnName != "生产车间" && columnName != "仓库" && columnName != "单位" && columnName != "开单未入量" && columnName != "DX$CheckboxSelectorColumn" && columnName != "客户名称" )
                selectColumnValue = string . IsNullOrEmpty ( row [ columnName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ columnName ] . ToString ( ) );

            allOfValue = string . IsNullOrEmpty ( row [ "排产量" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "排产量" ] . ToString ( ) );

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            foreach ( DataColumn column in tableView . Columns )
            {
                if ( column . ColumnName != "主件品号" && column . ColumnName != "主件品名" && column . ColumnName != "订单量" && column . ColumnName != "预计生产量" && column . ColumnName != "排产量" && column . ColumnName != "库存量" && column . ColumnName != "库存可用量" && column . ColumnName != "未排量" && column . ColumnName != "生产车间" && column . ColumnName != "仓库" && column . ColumnName != "单位" && column . ColumnName != "DX$CheckboxSelectorColumn" && column . ColumnName != "开单未入量" && column . ColumnName != columnName && column . ColumnName != "客户名称" )
                {
                    model . PRF003 = model . PRF003 + ( string . IsNullOrEmpty ( row [ column . ColumnName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ column . ColumnName ] . ToString ( ) ) );
                }
            }

            txtAllow . Text = ( allOfValue - model . PRF003 - selectColumnValue ) . ToString ( );
            if ( allOfValue - model . PRF003 - selectColumnValue < 0 )
            {
                XtraMessageBox . Show ( "品号:" + row [ "主件品号" ] + "\n\r剩余量小于0,请核实" ,"提示" );
            }
        }
        private void gridView1_CellValueChanging ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            //check = true;
            //selectIdx = e . RowHandle;
            //selectColumn = e . Column . VisibleIndex;
        }
        int selectPrevious=0;
        private void gridView1_MouseEnter ( object sender ,EventArgs e )
        {

            //int selectNow = gridView1 . FocusedColumn . VisibleIndex;

            //if ( selectPrevious != selectNow )
            //{
            //    row = gridView1 . GetFocusedDataRow ( );
            //    if ( row == null )
            //        return;

            //    int selectColumnValue = 0, allOfValue = 0;

            //    columnName = gridView1 . FocusedColumn . FieldName;
            //    if ( columnName == "DX$CheckboxSelectorColumn" )
            //        return;
            //    model . PRF003 = 0;
            //    if ( columnName != "主件品号" && columnName != "主件品名" && columnName != "订单量" && columnName != "预计生产量" && columnName != "排产量" && columnName != "库存量" && columnName != "库存可用量" && columnName != "未排量" && columnName != "生产车间" && columnName != "仓库" && columnName != "单位" && columnName != "未生产量" && columnName != "DX$CheckboxSelectorColumn" )
            //        selectColumnValue = string . IsNullOrEmpty ( row [ columnName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ columnName ] . ToString ( ) );

            //    allOfValue = string . IsNullOrEmpty ( row [ "排产量" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "排产量" ] . ToString ( ) );

            //    gridView1 . CloseEditor ( );
            //    gridView1 . UpdateCurrentRow ( );


            //    foreach ( DataColumn column in tableView . Columns )
            //    {
            //        if ( column . ColumnName != "主件品号" && column . ColumnName != "主件品名" && column . ColumnName != "订单量" && column . ColumnName != "预计生产量" && column . ColumnName != "排产量" && column . ColumnName != "库存量" && column . ColumnName != "库存可用量" && column . ColumnName != "未排量" && column . ColumnName != "生产车间" && column . ColumnName != "仓库" && column . ColumnName != "单位" && column . ColumnName != "DX$CheckboxSelectorColumn" && column . ColumnName != "未生产量" && column . ColumnName != columnName )
            //        {
            //            model . PRF003 = model . PRF003 + ( string . IsNullOrEmpty ( row [ column . ColumnName ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ column . ColumnName ] . ToString ( ) ) );
            //        }
            //    }

            //    if ( columnName == "主件品号" || columnName == "主件品名" || columnName == "订单量" || columnName == "预计生产量" || columnName == "排产量" || columnName == "库存量" || columnName == "库存可用量" || columnName == "未排量" || columnName == "生产车间" || columnName == "仓库" || columnName == "单位" || columnName == "DX$CheckboxSelectorColumn" || columnName == "未生产量" )
            //        return;

            //    gridView1 . SetRowCellValue ( gridView1 . FocusedRowHandle ,columnName ,allOfValue - model . PRF003  );

            //    selectPrevious = selectNow;
            //}

        }
        private void btnLine_Click ( object sender ,EventArgs e )
        {
            int [ ] selectRows = gridView1 . GetSelectedRows ( );
            if ( selectRows . Length < 1 )
            {
                XtraMessageBox . Show ( "请选择需要排产的产品" );
                return;
            }
            Dictionary<string ,string> strDic = new Dictionary<string ,string> ( );
            //string productName = string . Empty, productNum = string . Empty;
            foreach ( int i in selectRows )
            {
                DataRow row = gridView1 . GetDataRow ( i );
                if ( row == null )
                    continue;

                strDic . Add ( row [ "主件品号" ] . ToString ( ) ,row [ "主件品名" ] . ToString ( ) );
            }

            UserInfoMation . programName = "FormLineForAssPlan";
            FormLineForAssPlan from = new FormLineForAssPlan ( strDic );
            from . ShowDialog ( );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if (  tableView == null || tableView . Rows . Count < 1 )
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
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        //增加天数
        private void btnAddDays_Click ( object sender ,EventArgs e )
        {
            if ( choiseProduct ( ) == false )
                return;
            FormBatOpera from = new FormBatOpera ( "增加排产天数" ,table );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                Query ( );
            }
        }
        //清空
        private void btnClear_Click ( object sender ,EventArgs e )
        {
            if ( choiseProduct ( ) == false )
                return;
            FormBatOpera from = new FormBatOpera ( "批量清空" ,table );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                table = from . getTable;
                DateTime dtOne, dtTwo;
                int days;
                foreach ( DataRow row in table . Rows )
                {
                    DataRow ro = tableView . Select ( "主件品号='" + row [ "P1" ] + "'" ) [ 0 ];
                    dtOne = Convert . ToDateTime ( row [ "P3" ] );
                    dtTwo = Convert . ToDateTime ( row [ "P4" ] );
                    days = ( dtTwo - dtOne ) . Days;
                    if ( tableView . Columns . Contains ( dtOne . ToString ( " yyyy-MM-dd " ) ) )
                    {
                        ro [ dtOne . ToString ( " yyyy-MM-dd " ) ] = 0;
                    }
                    for ( int i = 0 ; i < days ; i++ )
                    {
                        if ( tableView . Columns . Contains ( dtOne . ToString ( " yyyy-MM-dd " ) ) )
                        {
                            dtOne = dtOne . AddDays ( 1 );
                            ro [ dtOne . ToString ( " yyyy-MM-dd " ) ] = 0;
                        }
                    }
                }
                gridControl1 . Refresh ( );
            }
        }
        //排产
        private void btnPai_Click ( object sender ,EventArgs e )
        {
            if ( choiseProduct ( ) == false )
                return;
            FormBatOpera from = new FormBatOpera ( "批量分摊" ,table );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                table = from . getTable;
                DateTime dtOne, dtTwo;
                int days, proNum, totalNum = 0, sumNum = 0;
                foreach ( DataRow row in table . Rows )
                {
                    DataRow ro = tableView . Select ( "主件品号='" + row [ "P1" ] + "'" ) [ 0 ];
                    foreach ( DataColumn column in tableView . Columns )
                    {
                        if ( column . ColumnName == "排产量" )
                            sumNum = Convert . ToInt32 ( ro [ column . ColumnName ] );
                        if ( column . ColumnName != "主件品号" && column . ColumnName != "主件品名" && column . ColumnName != "订单量" && column . ColumnName != "预计生产量" && column . ColumnName != "排产量" && column . ColumnName != "库存量" && column . ColumnName != "库存可用量" && column . ColumnName != "未排量" && column . ColumnName != "生产车间" && column . ColumnName != "仓库" && column . ColumnName != "单位" && column . ColumnName != "开单未入量" && column . ColumnName != "客户名称" )
                        {
                            if ( ro [ column . ColumnName ] != null && !string . IsNullOrEmpty ( ro [ column . ColumnName ] . ToString ( ) ) )
                                totalNum += Convert . ToInt32 ( ro [ column . ColumnName ] );
                        }
                    }
                    dtOne = Convert . ToDateTime ( row [ "P3" ] );
                    dtTwo = Convert . ToDateTime ( row [ "P4" ] );
                    proNum = Convert . ToInt32 ( row [ "P6" ] );
                    days = ( dtTwo - dtOne ) . Days;

                    if ( tableView . Columns . Contains ( dtOne . ToString ( " yyyy-MM-dd " ) ) )
                    {
                        if ( proNum + totalNum > sumNum )
                            proNum = sumNum - totalNum;
                        ro [ dtOne . ToString ( " yyyy-MM-dd " ) ] = proNum;
                        totalNum += proNum;
                    }
                    for ( int i = 0 ; i < days ; i++ )
                    {
                        if ( tableView . Columns . Contains ( dtOne . ToString ( " yyyy-MM-dd " ) ) )
                        {
                            if ( proNum + totalNum > sumNum )
                                proNum = sumNum - totalNum;
                            dtOne = dtOne . AddDays ( 1 );
                            ro [ dtOne . ToString ( " yyyy-MM-dd " ) ] = proNum;
                            totalNum += proNum;
                        }
                    }
                }
                gridControl1 . Refresh ( );
            }
        }
        bool choiseProduct ( )
        {
            int [ ] selectRows = gridView1 . GetSelectedRows ( );
            if ( selectRows . Length < 1 )
            {
                XtraMessageBox . Show ( "请选择产品" );
                return false;
            }
            table . Rows . Clear ( );
            foreach ( int i in selectRows )
            {
                DataRow row = gridView1 . GetDataRow ( i );
                if ( row == null )
                    continue;
                DataRow r = table . NewRow ( );
                r [ "P1" ] = row [ "主件品号" ] . ToString ( );
                r [ "P2" ] = row [ "主件品名" ] . ToString ( );
                table . Rows . Add ( r );
            }
            return true;
        }
        #endregion

        #region Method
        void addCount ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;

            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            string week = string . Empty;
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                if ( column . Name . Contains ( "col" ) )
                    column . Name = column . Name . Replace ( "col" ,"" );
                column . AppearanceCell . Options . UseTextOptions = true;
                column . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                column . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                column . BestFit ( );
                column . Summary . Clear ( );
                column . OptionsColumn . AllowEdit = false;
                if ( column . FieldName != "主件品号" && column . FieldName != "主件品名" && column . FieldName != "订单量" && column . FieldName != "预计生产量" && column . FieldName != "排产量" && column . FieldName != "库存量" && column . FieldName != "库存可用量" && column . FieldName != "未排量" && column . FieldName != "生产车间" && column . FieldName != "仓库" && column . FieldName != "单位" && column . FieldName != "开单未入量" && column . FieldName != "客户名称" )
                {
                    object obj = tableView . Compute ( "COUNT([" + column . FieldName + "])" ,"[" + column . FieldName + "]>0" );
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,obj . ToString ( ) );
                    column . OptionsColumn . AllowEdit = true;
                    column . Width = 45;
                    //if ( ( Convert . ToDateTime ( column . FieldName ) - dt ) . Days < 0 )
                    //    column . OptionsColumn.AllowEdit = false;
                    week = System . Globalization . CultureInfo . CurrentCulture . DateTimeFormat . GetDayName ( Convert . ToDateTime ( column . Name ) . DayOfWeek );
                    column . Caption = column . Name + week;
                    if ( week . Equals ( "星期日" ) )
                    {
                        column . AppearanceHeader . BackColor = System . Drawing . Color . Lavender;
                        column . AppearanceCell . BackColor = System . Drawing . Color . Lavender;
                    }
                }
                else if ( column . FieldName == "主件品号" )
                {
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,"合计" );
                    column . Fixed = DevExpress . XtraGrid . Columns . FixedStyle . Left;
                }
                else if ( column . FieldName == "订单量" )
                    column . ToolTip = "同品号所有未结束订单总量     ";
                else if ( column . FieldName == "预计生产量" )
                    column . ToolTip = "工单单头未完成量=计划生产量-已完工量          ";
                else if ( column . FieldName == "库存量" )
                    column . ToolTip = "现有库存量     ";
                else if ( column . FieldName == "库存可用量" )
                    column . ToolTip = "库存量+预计生产量-订单量     ";
                else if ( column . FieldName == "未排量" )
                    column . ToolTip = "订单量-排产量     ";
                else if ( column . FieldName == "开单未入量" )
                    column . ToolTip = "(已领料量-入库耗用量)/(预计用量/产品数量)     ";
                else if ( column . FieldName == "客户名称" )
                    column . Fixed = DevExpress . XtraGrid . Columns . FixedStyle . Left;
            }
        }
        void controlUnEnable ( )
        {
            btnAddDays . Enabled = btnClear . Enabled = btnPai . Enabled = false;
            gridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            btnAddDays . Enabled = btnClear . Enabled = btnPai . Enabled = true;
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
                    if ( row [ column ] != null && row [ column ] . ToString ( ) != string . Empty && column . ColumnName != "主件品号" && column . ColumnName != "主件品名" && column . ColumnName != "排产量" && column . ColumnName != "订单量" && column . ColumnName != "预计生产量" && column . ColumnName != "库存量" && column . ColumnName != "库存可用量" && column . ColumnName != "未排量" && column . ColumnName != "生产车间" && column . ColumnName != "仓库" && column . ColumnName != "单位" && column . ColumnName != "开单未入量" && column . ColumnName != "客户名称" )
                    {
                        if ( Convert . ToInt32 ( row [ column ] ) < 0 )
                        {
                            XtraMessageBox . Show ( "主件品号:"+row["主件品号"]+"的排产量为负数,请核实" );
                            result = false;
                            break;
                        }
                        total += Convert . ToInt32 ( row [ column ] );
                    }
                }
                if ( result == false )
                    break;
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