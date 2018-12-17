using System . Data;
using Utility;
using System . Reflection;
using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using System;
using LineProductMes . ClassForMain;

namespace LineProductMes
{
    public partial class FormMouldInformation :FormChild
    {
        LineProductMesEntityu.MouldInformationEntity model=null;
        LineProductMesBll.Bll.MouldInformationBll _bll=null;
        DataTable tableView;DataRow row;int selectIdx;bool result=false;
        string focuseName=string.Empty;

        public FormMouldInformation ( )
        {
            InitializeComponent ( );

            model = new LineProductMesEntityu . MouldInformationEntity ( );
            _bll = new LineProductMesBll . Bll . MouldInformationBll ( );

            GridViewMoHuSelect . SetFilter ( gridView1 );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
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
            ChildForm . FormMouldInfo from = new ChildForm . FormMouldInfo ( null );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = from . getModel;
                if ( model == null )
                    return 0;
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
            ChildForm . FormMouldInfo from = new ChildForm . FormMouldInfo ( model );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = from . getModel;
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
                XtraMessageBox . Show ( "请选择需要编辑的内容" );
                return 0;
            }
            result = _bll . Delete ( model . idx );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                tableView . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
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
                row [ "MOI016" ] = model . MOI016;
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
        void setValue ( DataRow row )
        {
            row [ "MOI001" ] = model . MOI001;
            row [ "MOI002" ] = model . MOI002;
            row [ "MOI003" ] = model . MOI003;
            row [ "MOI004" ] = model . MOI004;
            row [ "MOI005" ] = model . MOI005;
            row [ "MOI006" ] = model . MOI006;
            row [ "MOI007" ] = model . MOI007;
            row [ "MOI008" ] = model . MOI008;
            row [ "MOI009" ] = model . MOI009;
            row [ "MOI010" ] = model . MOI010;
            row [ "MOI011" ] = model . MOI011;
            row [ "MOI012" ] = model . MOI012;
            row [ "MOI013" ] = model . MOI013;
            row [ "MOI014" ] = model . MOI014;
            row [ "MOI015" ] = model . MOI015;
            row [ "MOI016" ] = model . MOI016;
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
            model . MOI001 = row [ "MOI001" ] . ToString ( );
            model . MOI002 = row [ "MOI002" ] . ToString ( );
            model . MOI003 = row [ "MOI003" ] . ToString ( );
            model . MOI004 = row [ "MOI004" ] . ToString ( );
            model . MOI005 = row [ "MOI005" ] . ToString ( );
            model . MOI006 = row [ "MOI006" ] . ToString ( );
            model . MOI007 = row [ "MOI007" ] . ToString ( );
            model . MOI008 = row [ "MOI008" ] . ToString ( );
            model . MOI009 = row [ "MOI009" ] . ToString ( );
            model . MOI010 = row [ "MOI010" ] . ToString ( );
            model . MOI011 = Convert . ToDateTime ( row [ "MOI011" ] . ToString ( ) );
            model . MOI012 = row [ "MOI012" ] . ToString ( );
            model . MOI013 = row [ "MOI013" ] . ToString ( );
            model . MOI014 = row [ "MOI014" ] . ToString ( );
            model . MOI015 = row [ "MOI015" ] . ToString ( );
            model . MOI016 = string . IsNullOrEmpty ( row [ "MOI016" ] . ToString ( ) ) == true ? false : Convert . ToBoolean ( row [ "MOI016" ] );
            if ( model . MOI016 )
            {
                toolCancellation . Caption = "反注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                model . MOI016 = false;
            }
            else
            {
                toolCancellation . Caption = "注销";
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                model . MOI016 = true;
            }
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
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
        private void contextMenuStrip1_ItemClicked ( object sender ,System . Windows . Forms . ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        #endregion

    }
}