using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Threading . Tasks;
using System . Windows . Forms;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class AssNewWorkEnclosureQuery :FormBaseChild
    {
        LineProductMesBll.Bll.AssNewWorkEnclosureBll _bll=null;
        string oddNum=string.Empty;
        
        public AssNewWorkEnclosureQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkEnclosureBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1  ,View3 ,View4 ,View5   } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1  ,View3 ,View4 ,View5  } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Task task = new Task ( InitData );
            task . Start ( );
        }

        void InitData ( )
        {
            DataTable table = _bll . getTableColumn ( "1=1" );
            if ( txtANT001 . InvokeRequired )
            {
                Action<string> actionANT001 = ( x ) =>
                {
                    txtANT001 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANT001" );
                    txtANT001 . Properties . DisplayMember = "ANT001";
                    txtANT001 . Properties . ValueMember = "ANT001";
                };
                this . txtANT001 . Invoke ( actionANT001 ,string . Empty );
            }
            if ( txtANU003 . InvokeRequired )
            {
                Action<string> actionANU003 = ( x ) =>
                {
                    txtANU003 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANU003" );
                    txtANU003 . Properties . DisplayMember = "ANU003";
                    txtANU003 . Properties . ValueMember = "ANU003";
                };
                this . txtANU003 . Invoke ( actionANU003 ,string . Empty );
            }
            if ( txtANU004 . InvokeRequired )
            {
                Action<string> actionANU004 = ( x ) =>
                {
                    txtANU004 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANU004" );
                    txtANU004 . Properties . DisplayMember = "ANU004";
                    txtANU004 . Properties . ValueMember = "ANU004";
                };
                this . txtANU004 . Invoke ( actionANU004 ,string . Empty );
            }
            if ( txtANU005 . InvokeRequired )
            {
                Action<string> actionANU005 = ( x ) =>
                {
                    txtANU005 . Properties . DataSource = table . DefaultView . ToTable ( true ,"ANU005" );
                    txtANU005 . Properties . DisplayMember = "ANU005";
                    txtANU005 . Properties . ValueMember = "ANU005";
                };
                this . txtANU005 . Invoke ( actionANU005 ,string . Empty );
            }
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtANT001 . EditValue =  txtANU003 . EditValue = txtANU004 . EditValue = txtANU005 . EditValue = null;
            dateEdit1 . Text = null;
        }
        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtANT001 . Text ) )
                strWhere += " AND ANT001='" + txtANT001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANU003 . Text ) )
                strWhere += " AND ANU003='" + txtANU003 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANU004 . Text ) )
                strWhere += " AND ANU004='" + txtANU004 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtANU005 . Text ) )
                strWhere += " AND ANU005='" + txtANU005 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere += " AND ANT008='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
            if ( !string . IsNullOrEmpty ( txtSa . Text ) )
                strWhere += " AND ANT011='" + txtSa . Text + "'";

            DataTable tableView = _bll . getTableColumn ( strWhere );
            gridControl1 . DataSource = tableView;
        }
        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            oddNum = row [ "ANT001" ] . ToString ( );
            if ( oddNum == string . Empty )
                return;
            this . DialogResult = DialogResult . OK;
        }

        public string getOddNum
        {
            get
            {
                return oddNum;
            }
        }

    }
}
