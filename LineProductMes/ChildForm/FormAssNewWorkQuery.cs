using System . Threading . Tasks;
using System . Data;
using System;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormAssNewWorkQuery :FormBaseChild
    {
        LineProductMesBll.Bll.AssNewWorkBll _bll=null;
        DataTable table,tableView;
        string oddNum=string.Empty;

        public FormAssNewWorkQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2  ,View4 ,View5,View6 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2  ,View4 ,View5 ,View6 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Task task = new Task ( InitData );
            task . Start ( );
        }

        void InitData ( )
        {
            table = _bll . getTableColumn ( );
            if ( txtANW001 . InvokeRequired )
            {
                Action<string> actionANW001 = ( x ) =>
                {
                    txtANW001 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANW001" );
                    txtANW001 . Properties . DisplayMember = "ANW001";
                    txtANW001 . Properties . ValueMember = "ANW001";
                };
                txtANW001 . Invoke ( actionANW001 ,string . Empty );
            }
            if ( txtANW002 . InvokeRequired )
            {
                Action<string> actionANW002 = ( x ) =>
                {
                    txtANW002 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANN002" );
                    txtANW002 . Properties . DisplayMember = "ANN002";
                    txtANW002 . Properties . ValueMember = "ANN002";
                };
                txtANW002 . Invoke ( actionANW002 ,string . Empty );
            }
            if ( txtANW003 . InvokeRequired )
            {
                Action<string> actionANW003 = ( x ) =>
                {
                    txtANW003 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANN003" );
                    txtANW003 . Properties . DisplayMember = "ANN003";
                    txtANW003 . Properties . ValueMember = "ANN003";
                };
                txtANW003 . Invoke ( actionANW003 ,string . Empty );
            }
            if ( txtANW004 . InvokeRequired )
            {
                Action<string> actionANW004 = ( x ) =>
                {
                    txtANW004 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANN004" );
                    txtANW004 . Properties . DisplayMember = "ANN004";
                    txtANW004 . Properties . ValueMember = "ANN004";
                };
                txtANW004 . Invoke ( actionANW004 ,string . Empty );
            }
            if ( txtANW013 . InvokeRequired )
            {
                Action<string> actionANW019 = ( x ) =>
                {
                    txtANW013 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANW013" );
                    txtANW013 . Properties . DisplayMember = "ANW013";
                    txtANW013 . Properties . ValueMember = "ANW013";
                };
                txtANW013 . Invoke ( actionANW019 ,string . Empty );
            }
        }
        
        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtANW001 . Text ) )
                strWhere += " AND ANW001='" + txtANW001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANW002 . Text ) )
                strWhere += " AND ANN002='" + txtANW002 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANW003 . Text ) )
                strWhere += " AND ANN003='" + txtANW003 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANW004 . Text ) )
                strWhere += " AND ANN004='" + txtANW004 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANW013 . Text ) )
                strWhere += " AND ANW013='" + txtANW013 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere += " AND ANW022='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";

            tableView = _bll . getTableViewQuery ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = System . Windows . Forms . DialogResult . Cancel;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            if ( gridView1 . FocusedRowHandle < 0 )
                return;
            oddNum = gridView1 . GetFocusedRowCellValue ( "ANW001" ) . ToString ( );
            if ( string . IsNullOrEmpty ( oddNum ) )
                return;
            this . DialogResult = System . Windows . Forms . DialogResult . OK;
        }
        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtANW001 . EditValue = txtANW002 . EditValue = txtANW003 . EditValue = txtANW004 . EditValue = txtANW013 . EditValue = null;
            dateEdit1 . Text = null;
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