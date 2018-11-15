using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using Utility;
using LineProductMes . ChildForm;
using DevExpress . XtraEditors;

namespace LineProductMes
{
    public partial class FormProControl :FormChild
    {
        LineProductMesBll.Bll.ProControlBll _bll=null;
        LineProductMesEntityu.MainEntity model;
        DataTable tableView;int selectIndex;

        public FormProControl ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . ProControlBll ( );
            GridViewMoHuSelect . SetFilter ( gridView1 );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { this . gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolCanecl ,toolSave ,toolExport ,toolPrint ,toolCancellation ,toolExamin } );

            Query ( );
        }

        #region Main
        protected override int Query ( )
        {
            getTableView ( );
            return base . Query ( );
        }
        protected override int Add ( )
        {
            ProControlChild form = new ProControlChild ( "新增" ,model );
            form . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = form . getModel;
                if ( model == null )
                    return 0;
                DataRow row = tableView . NewRow ( );
                setValue ( row );
                tableView . Rows . Add ( row );
                gridControl1 . RefreshDataSource ( );
            }

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            if ( model == null )
            {
                XtraMessageBox . Show ( "请选择需要编辑的内容" );
                return 0;
            }
            ProControlChild form = new ProControlChild ( "编辑" ,model );
            form . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = form . getModel;
                if ( model == null )
                    return 0;
                DataRow row = tableView . Rows [ selectIndex ];
                row . BeginEdit ( );
                setValue ( row );
                row . EndEdit ( );
                gridControl1 . RefreshDataSource ( );
            }

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( model == null )
            {
                XtraMessageBox . Show ( "请选择需要删除的内容" );
                return 0;
            }

            bool result = _bll . Delete ( model );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                tableView . Rows . RemoveAt ( selectIndex );
                gridControl1 . RefreshDataSource ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        #endregion

        #region Event
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            selectIndex = gridView1 . FocusedRowHandle;
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            model = new LineProductMesEntityu . MainEntity ( );
            model . FOR001 = Convert . ToInt32 ( row [ "FOR001" ] );
            model . FOR002 = Convert . ToInt32 ( row [ "FOR002" ] );
            model . FOR003 = row [ "FOR003" ] . ToString ( );
            model . FOR004 = row [ "FOR004" ] . ToString ( );
            model . FOR005 = row [ "FOR005" ] . ToString ( );
            model . FOR006 = row [ "FOR006" ] . ToString ( );
        }
        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            if ( model == null )
            {
                XtraMessageBox . Show ( "请选择需要编辑的内容" );
                return ;
            }
            ProControlChild form = new ProControlChild ( "编辑" ,model );
            form . StartPosition = System . Windows . Forms . FormStartPosition . CenterScreen;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                model = form . getModel;
                if ( model == null )
                    return ;
                DataRow row = tableView . Rows [ selectIndex ];
                row . BeginEdit ( );
                setValue ( row );
                row . EndEdit ( );
                gridControl1 . RefreshDataSource ( );
            }
        }
        #endregion

        #region Method
        void setValue ( DataRow row )
        {
            row [ "FOR001" ] = model . FOR001;
            row [ "FOR002" ] = model . FOR002;
            row [ "FOR003" ] = model . FOR003;
            row [ "FOR004" ] = model . FOR004;
            row [ "FOR005" ] = model . FOR005;
            row [ "FOR006" ] = model . FOR006;
        }
        void getTableView ( )
        {
            tableView = _bll . getTableView ( );
            gridControl1 . DataSource = tableView;
            QueryTool ( );
        }
        #endregion

    }
}
