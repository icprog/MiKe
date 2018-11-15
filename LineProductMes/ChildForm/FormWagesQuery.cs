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
using System . Reflection;
using Utility;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormWagesQuery :FormBaseChild
    {
        LineProductMesBll.Bll.WagesBll _bll=null;
        DataTable tableView;

        public FormWagesQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . WagesBll ( );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 } );
            InitData ( );
        }

        void InitData ( )
        {
            tableView = _bll . getTableQuery ( "1=1" );
            txtWAG001 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"WAG001" );
            txtWAG001 . Properties . DisplayMember = "WAG001";
            txtWAG001 . Properties . ValueMember = "WAG001";

            txtWAG002 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"WAG002" );
            txtWAG002 . Properties . DisplayMember = "WAG002";
            txtWAG002 . Properties . ValueMember = "WAG002";
        }

        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtWAG001 . Text ) )
                strWhere = strWhere + " AND WAG001='" + txtWAG001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtWAG002 . Text ) )
                strWhere = strWhere + " AND WAG002='" + txtWAG002 . Text + "'";

            tableView = _bll . getTableQuery ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtWAG001 . EditValue = txtWAG002 . EditValue = null;
        }

        string code;
        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            code = gridView1 . GetFocusedRowCellValue ( "WAG001" ) . ToString ( );
            if ( string . IsNullOrEmpty ( code ) )
                return;
            this . DialogResult = DialogResult . OK;
        }

        public string getCode
        {
            get
            {
                return code;
            }
        }

    }
}