using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Drawing;
using System . Text;
using System . Linq;
using System . Threading . Tasks;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormInjectionMoldingQuery :FormBaseChild
    {
        LineProductMesBll.Bll.InjectionMoldingBll _bll=null;
        string oddNum=string.Empty;
        DataTable tableColumn;
        
        public FormInjectionMoldingQuery ( )
        {
            InitializeComponent ( );

           
        }

        private void FormInjectionMoldingQuery_Load ( object sender ,EventArgs e )
        {
            _bll = new LineProductMesBll . Bll . InjectionMoldingBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View2 ,View3 ,View4 ,View } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View2 ,View3 ,View4 ,View } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Func<string> funStr = InitData;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( UI ) ,null );
        }

        string InitData ( )
        {
            tableColumn = _bll . getTableHeader ( "1=1" ,"1=1" ,"1=1" );
            return string . Empty;
        }
        
        delegate void AsynUpdateUI ( );

        void UI ( IAsyncResult result )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUI ( delegate ( )
                {
                    txtIJA001 . Properties . DataSource = tableColumn . DefaultView . ToTable ( true ,"IJA001" );
                    txtIJA001 . Properties . DisplayMember = "IJA001";
                    txtIJA001 . Properties . ValueMember = "IJA001";

                    txtIJB004 . Properties . DataSource = tableColumn . DefaultView . ToTable ( true ,"IJB004" );
                    txtIJB004 . Properties . DisplayMember = "IJB004";
                    txtIJB004 . Properties . ValueMember = "IJB004";

                    txtIJB005 . Properties . DataSource = tableColumn . DefaultView . ToTable ( true ,"IJB005" );
                    txtIJB005 . Properties . DisplayMember = "IJB005";
                    txtIJB005 . Properties . ValueMember = "IJB005";

                    txtIJB006 . Properties . DataSource = tableColumn . DefaultView . ToTable ( true ,"IJB006" );
                    txtIJB006 . Properties . DisplayMember = "IJB006";
                    txtIJB006 . Properties . ValueMember = "IJB006";
                } ) );
            }
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtIJA001 . EditValue = txtIJB004 . EditValue = txtIJB005 . EditValue = txtIJB006 . EditValue =dateEdit1.Text=txtSa.Text= null;
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1", strWhere1 = "1=1", strWhere2 = "1=1";
            if ( !string . IsNullOrEmpty ( txtIJA001 . Text ) )
            {
                strWhere += " AND IJA001='" + txtIJA001 . Text + "'";
                strWhere1 += " AND IJA001='" + txtIJA001 . Text + "'";
                strWhere2 += " AND IJA001='" + txtIJA001 . Text + "'";
            }
            if ( !string . IsNullOrEmpty ( txtIJB004 . Text ) )
            {
                strWhere += " AND IJB004='" + txtIJB004 . Text + "'";
                strWhere1 += " AND IJC002='" + txtIJB004 . Text + "'";
            }
            if ( !string . IsNullOrEmpty ( txtIJB005 . Text ) )
            {
                strWhere += " AND IJB005='" + txtIJB005 . Text + "'";
                strWhere1 += " AND IJC003='" + txtIJB005 . Text + "'";
            }
            if ( !string . IsNullOrEmpty ( txtIJB006 . Text ) )
            {
                strWhere += " AND IJB006='" + txtIJB006 . Text + "'";
                strWhere1 += " AND IJC004='" + txtIJB006 . Text + "'";
            }
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
            {
                strWhere += " AND IJA007='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
                strWhere1 += " AND IJA007='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
                strWhere2 += " AND IJA007='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
            }
            if ( !string . IsNullOrEmpty ( txtSa . Text ) )
            {
                strWhere += " AND IJA002='" + txtSa.Text + "'";
                strWhere1 += " AND IJA002='" + txtSa . Text + "'";
                strWhere2 += " AND IJA002='" + txtSa . Text + "'";
            }

            DataTable tableView = _bll . getTableHeader ( strWhere ,strWhere1 ,strWhere2 );
            gridControl1 . DataSource = tableView;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            oddNum = gridView1 . GetFocusedRowCellValue ( "IJA001" ) . ToString ( );
            if ( oddNum != string . Empty )
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