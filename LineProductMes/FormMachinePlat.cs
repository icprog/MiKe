using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class FormMachinePlat :FormChild
    {
        LineProductMesEntityu.MachinePlatEntity model=null;
        LineProductMesBll.Bll.MachinePlatBll _bll=null;
        DataTable tableView;
        DataRow row=null;
        bool result=false;
        int selectIdx=0;
        string focuseName=string.Empty;

        public FormMachinePlat ( )
        {
            InitializeComponent ( );

            model = new LineProductMesEntityu . MachinePlatEntity ( );
            _bll = new LineProductMesBll . Bll . MachinePlatBll ( );

            GridViewMoHuSelect . SetFilter ( gridView1 );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolCanecl ,toolSave  ,toolPrint ,toolExamin } );

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
            ChildForm . FormMaPlate from = new ChildForm . FormMaPlate ( null );
            from . StartPosition = FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) != DialogResult . OK )
                return 0;
            model = from . getModel;
            if ( model == null )
                return 0;
            row = null;
            row = tableView . NewRow ( );
            row [ "idx" ] = model . idx;
            setColumn ( row );
            tableView . Rows . Add ( row );
            gridControl1 . RefreshDataSource ( );
            row = null;

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择需要编辑的列" );
                return 0;
            }
            ChildForm . FormMaPlate from = new ChildForm . FormMaPlate ( model );
            from . StartPosition = FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) != DialogResult . OK )
                return 0;
            model = from . getModel;
            if ( model == null )
                return 0;
            row = null;
            row = tableView . Rows [ selectIdx ];
            row . BeginEdit ( );
            setColumn ( row );
            row . EndEdit ( );
            gridControl1 . RefreshDataSource ( );
            row = null;

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择需要删除的内容" );
                return 0;
            }
            if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . YesNo ) != DialogResult . Yes )
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
                row [ "MAP007" ] = model . MAP007;
                row . EndEdit ( );
                gridControl1 . RefreshDataSource ( );
                cancelltionTool ( captionCan );
            }
            else
                XtraMessageBox . Show ( captionCan + "失败" );

            return base . Cancellation ( );
        }
        protected override int ExportBase ( )
        {
            ViewExport . ExportToExcel ( this . Text ,this . gridControl1 );

            return base . ExportBase ( );
        }
        #endregion

        #region Method
        void setColumn ( DataRow row )
        {
            row [ "MAP001" ] = model . MAP001;
            row [ "MAP002" ] = model . MAP002;
            row [ "MAP003" ] = model . MAP003;
            row [ "MAP004" ] = model . MAP004;
            row [ "MAP005" ] = model . MAP005;
            row [ "MAP006" ] = model . MAP006;
            row [ "MAP007" ] = false;
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
            model . MAP001 = row [ "MAP001" ] . ToString ( );
            model . MAP002 = row [ "MAP002" ] . ToString ( );
            model . MAP003 = row [ "MAP003" ] . ToString ( );
            model . MAP004 = row [ "MAP004" ] . ToString ( );
            model . MAP005 = row [ "MAP005" ] . ToString ( );
            model . MAP006 = row [ "MAP006" ] . ToString ( );
            model . MAP007 = string . IsNullOrEmpty ( row [ "MAP007" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "MAP007" ] );
            if ( model . MAP007 )
            {
                toolCancellation . Caption = "反注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                model . MAP007 = false;
            }
            else
            {
                toolCancellation . Caption = "注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                model . MAP007 = true;
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