using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using System . Collections . Generic;
using System . Reflection;
using Utility;

namespace LineProductMes
{
    public partial class FormPurProductPlan :FormChild
    {
        LineProductMesBll.Bll.SemiProductPlanBll _bll=null;
        LineProductMesEntityu.SemiProductPlanEntity model=null;
        
        public FormPurProductPlan ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . SemiProductPlanBll ( );
            model = new LineProductMesEntityu . SemiProductPlanEntity ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolCanecl ,toolSave  ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit ,toolAdd } );
            GrivColumnStyle . setColumnStyle ( treeList1 );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

        }

        protected override int Query ( )
        {
            treeList1 . KeyFieldName = "SEP007";
            treeList1 . ParentFieldName = "SEP008";
            List<LineProductMesEntityu . SemiProductPlanEntity> modelList = _bll . getListModelPur ( );
            treeList1 . DataSource = modelList;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            treeList1 . BestFitColumns ( );

            return base . Query ( );
        }
        protected override int Export ( )
        {
            ViewExport . ExportToExcel ( this . Text ,this . treeList1 );

            return base . Export ( );
        }

        private void treeList1_CustomDrawNodeIndicator ( object sender ,DevExpress . XtraTreeList . CustomDrawNodeIndicatorEventArgs e )
        {
            //DevExpress . XtraTreeList . TreeList temTree = sender as DevExpress . XtraTreeList . TreeList;
            //DevExpress . Utils . Drawing . IndicatorObjectInfoArgs args = e . ObjectArgs as DevExpress . Utils . Drawing . IndicatorObjectInfoArgs;
            //if ( args != null )
            //{
            //    int rownum = temTree . GetVisibleIndexByNode ( e . Node ) + 1;
            //    if ( rownum < 0 )
            //        return;
            //    this . treeList1 . IndicatorWidth = rownum . ToString ( ) . Length * 10 + 10;
            //    args . DisplayText = rownum . ToString ( );
            //}
            //e . ImageIndex = -1;
        }

        private void FormPurProductPlan_Load ( object sender ,System . EventArgs e )
        {
            treeList1 . OptionsBehavior . EnableFiltering = true;
            treeList1 . OptionsView . ShowAutoFilterRow = true;
            treeList1 . OptionsFilter . FilterMode = DevExpress . XtraTreeList . FilterMode . Smart;
        }
    }
}
