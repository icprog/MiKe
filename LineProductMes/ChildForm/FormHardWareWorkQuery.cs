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
    public partial class FormHardWareWorkQuery :FormBaseChild
    {
        LineProductMesBll.Bll.HardWareWorkBll _bll=null;
        string oddNum=string.Empty;

        public FormHardWareWorkQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . HardWareWorkBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView4 ,View1 ,View2 ,View3 ,View4 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView4 ,View1 ,View2 ,View3 ,View4 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Task task = new Task ( InitData );
            task . Start ( );
        }

        private void InitData ( )
        {
            DataTable tableView = _bll . getTableColumn ( "1=1" );
            if ( txtHAW001 . InvokeRequired )
            {
                Action<string> haw001Ac = ( x ) =>
                {
                    txtHAW001 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"HAW001" );
                    txtHAW001 . Properties . DisplayMember = "HAW001";
                    txtHAW001 . Properties . ValueMember = "HAW001";
                };
                this . txtHAW001 . Invoke ( haw001Ac ,string . Empty );
                Action<string> haw002Ac = ( x ) =>
                {
                    txtHAW002 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"HAW002" );
                    txtHAW002 . Properties . DisplayMember = "HAW002";
                    txtHAW002 . Properties . ValueMember = "HAW002";
                };
                this . txtHAW002 . Invoke ( haw002Ac ,string . Empty );
                Action<string> haw003Ac = ( x ) =>
                {
                    txtHAW003 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"HAW003" );
                    txtHAW003 . Properties . DisplayMember = "HAW003";
                    txtHAW003 . Properties . ValueMember = "HAW003";
                };
                this . txtHAW003 . Invoke ( haw003Ac ,string . Empty );
                Action<string> haw004Ac = ( x ) =>
                {
                    txtHAW004 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"HAW004" );
                    txtHAW004 . Properties . DisplayMember = "HAW004";
                    txtHAW004 . Properties . ValueMember = "HAW004";
                };
                this . txtHAW004 . Invoke ( haw004Ac ,string . Empty );
            }
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtHAW001 . EditValue = txtHAW002 . EditValue = txtHAW003 . EditValue = txtHAW004 . EditValue =dateEdit1.Text= null;
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtHAW001 . Text ) )
                strWhere += " AND HAW001='" + txtHAW001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtHAW002 . Text ) )
                strWhere += " AND HAW002='" + txtHAW002 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtHAW003 . Text ) )
                strWhere += " AND HAW003='" + txtHAW003 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtHAW004 . Text ) )
                strWhere += " AND HAW004='" + txtHAW004 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere += " AND HAW010='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";

            DataTable tableView = _bll . getTableColumn ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void gridView4_DoubleClick ( object sender ,EventArgs e )
        {
            oddNum = gridView4 . GetFocusedRowCellValue ( "HAW001" ) . ToString ( );
            if ( !string . IsNullOrEmpty ( oddNum ) )
                this . DialogResult = DialogResult . OK;
        }

        public string getStr
        {
            get
            {
                return oddNum;
            }
        }

    }
}
