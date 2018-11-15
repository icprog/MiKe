using Utility;
using System . Reflection;
using DevExpress . Utils . Paint;
using System . Data;
using DevExpress . XtraEditors;
using System . Windows . Forms;
using System;
using LineProductMes . ClassForMain;

namespace LineProductMes
{
    public partial class FormSupplierForMould :FormChild
    {
        LineProductMesEntityu.SupplierForMouldEntity model=null;
        LineProductMesBll.Bll.SupplierForMouldBll _bll=null;
        DataTable tableView;
        DataRow row;
        int selectIdx;
        bool result=false;
        string focuseName=string.Empty;

        public FormSupplierForMould ( )
        {
            InitializeComponent ( );

            GridViewMoHuSelect . SetFilter ( gridView1 );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            model = new LineProductMesEntityu . SupplierForMouldEntity ( );
            _bll = new LineProductMesBll . Bll . SupplierForMouldBll ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolCanecl ,toolSave ,toolPrint ,toolExamin } );

            Query ( );
        }

        #region Main
        protected override int Query ( )
        {
            tableView = _bll . getTableView ( );
            gridControl1 . DataSource = tableView;
            QueryTool ( );

            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Query ( );
        }
        protected override int Add ( )
        {
            ChildForm . FormSupplierMould form = new ChildForm . FormSupplierMould ( null );
            form . StartPosition = FormStartPosition . CenterScreen;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = form . getModel;
                if ( model == null )
                    return 0;
                row = null;
                row = tableView . NewRow ( );
                row [ "idx" ] = model . idx;
                setValue ( row );
                tableView . Rows . Add ( row );
                gridControl1 . RefreshDataSource ( );
                row = null;
            }

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择需要编辑的内容" );
                return 0;
            }
            ChildForm . FormSupplierMould form = new ChildForm . FormSupplierMould ( model );
            form . StartPosition = FormStartPosition . CenterScreen;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = form . getModel;
                if ( model == null )
                    return 0;
                row = tableView . Rows [ selectIdx ];
                row . BeginEdit ( );
                setValue ( row );
                row . EndEdit ( );
                gridControl1 . RefreshDataSource ( );
                row = null;
            }

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择需要删除的内容" );
                return 0;
            }
            if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                return 0;
            result = _bll . Delete ( model . idx );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                tableView . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
                row = null;
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Cancellation ( )
        {
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择需要注销的内容" );
                return 0;
            }

            string captionCan = toolCancellation . Caption;
            result = _bll . CancellationBool ( model );
            if ( result )
            {
                XtraMessageBox . Show ( captionCan + "成功" );
                row = null;
                row = tableView . Rows [ selectIdx ];
                row . BeginEdit ( );
                row [ "SFM006" ] = model . SFM006;
                row . EndEdit ( );
                gridControl1 . RefreshDataSource ( );
                cancelltionTool ( captionCan );
            }
            else
                XtraMessageBox . Show ( captionCan + "失败" );

            return base . Cancellation ( );
        }
        protected override int Export ( )
        {
            ViewExport . ExportToExcel ( this . Text ,this . gridControl1 );

            return base . Export ( );
        }
        #endregion

        #region Method
        void setValue ( DataRow row )
        {
            row [ "SFM001" ] = model . SFM001;
            row [ "SFM002" ] = model . SFM002;
            row [ "SFM003" ] = model . SFM003;
            row [ "SFM004" ] = model . SFM004;
            row [ "SFM005" ] = model . SFM005;
            row [ "SFM006" ] = model . SFM006;
            row [ "SFM007" ] = model . SFM007;
            row [ "SFM008" ] = model . SFM008;
            row [ "SFM009" ] = model . SFM009;
            row [ "SFM010" ] = model . SFM010;
            row [ "U0" ] = model . SFM005 + " " + model . SFM007 + " " + model . SFM008 + " " + model . SFM009;
        }
        #endregion

        #region Event
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            selectIdx = gridView1 . FocusedRowHandle;
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
            model . SFM001 = row [ "SFM001" ] . ToString ( );
            model . SFM002 = row [ "SFM002" ] . ToString ( );
            model . SFM003 = row [ "SFM003" ] . ToString ( );
            model . SFM004 = row [ "SFM004" ] . ToString ( );
            model . SFM005 = row [ "SFM005" ] . ToString ( );
            model . SFM007 = row [ "SFM007" ] . ToString ( );
            model . SFM008 = row [ "SFM008" ] . ToString ( );
            model . SFM009 = row [ "SFM009" ] . ToString ( );
            model . SFM010 = row [ "SFM010" ] . ToString ( );
            model . SFM006 = string . IsNullOrEmpty ( row [ "SFM006" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "SFM006" ] );
            if ( model . SFM006 )
            {
                toolCancellation . Caption = "反注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                model . SFM006 = false;
            }
            else
            {
                toolCancellation . Caption = "注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                model . SFM006 = true;
            }
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
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


    }
}