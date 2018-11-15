using System . Data;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using Utility;
using DevExpress . XtraEditors;

namespace LineProductMes
{
    public partial class FormBoarSet :FormChild
    {
        LineProductMesBll.Bll.BoarSetBll _bll=null;
        DataTable tableView;

        public FormBoarSet ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . BoarSetBll ( );
            tableView = new DataTable ( );

            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolCanecl ,toolExport ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit ,toolAdd ,toolQuery } );
            InitData ( );
        }
        private void FormBoarSet_Load ( object sender ,System . EventArgs e )
        {
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }
        void InitData ( )
        {
            tableView = _bll . getTableView ( );
            gridControl1 . DataSource = tableView;
        }

        protected override int Save ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            bool result = _bll . Save ( tableView );
            if ( result )
            {
                XtraMessageBox . Show ( "保存成功" );
                InitData ( );
            }
            else
                XtraMessageBox . Show ( "保存失败" );

            return base . Save ( );
        }
    }
}