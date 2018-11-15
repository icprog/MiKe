using System;
using System . Data;
using System . Windows . Forms;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormProductPlanQuery :FormBaseChild
    {
        LineProductMesBll.Bll.ProductPlanBll _bll=null;
        DataTable tableView;
        string oddNum=string.Empty;

        public FormProductPlanQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . ProductPlanBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            tableView = _bll . getTableColumn ( "1=1" );
            txtPRD001 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"PRD001" );
            txtPRD001 . Properties . DisplayMember = "PRD001";
            txtPRD001 . Properties . ValueMember = "PRD001";

            txtPRE004 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"PRE004" );
            txtPRE004 . Properties . DisplayMember = "PRE004";
            txtPRE004 . Properties . ValueMember = "PRE004";
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtPRD001 . EditValue =txtPRE004.EditValue= null;
        }
        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtPRD001 . Text ) )
                strWhere += " AND PRD001='" + txtPRD001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtPRE004 . Text ) )
                strWhere += " AND PRE004='" + txtPRE004 . Text + "'";

            tableView = _bll . getTableColumn ( strWhere );
            gridControl1 . DataSource = tableView;
        }
        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            oddNum = gridView1 . GetFocusedRowCellValue ( "PRD001" ) . ToString ( );
            if ( oddNum == string . Empty )
                return;
            this . DialogResult = DialogResult . OK;
        }

        public string getOdd
        {
            get
            {
                return oddNum;
            }
        }

    }
}