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
using DevExpress . Utils . Paint;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormLogisticsQuery :FormBaseChild
    {
        LineProductMesBll.Bll.LogisticsNewBll _bll=null;
        DataTable tableView,table;
        
        public FormLogisticsQuery ( )
        {
            InitializeComponent ( );

            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 ,View3 ,View4 ,View5 } );

            _bll = new LineProductMesBll . Bll . LogisticsNewBll ( );

            Func<string> funStr = InitData;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( other ) ,null );
        }

        string InitData ( )
        {
            table = _bll . getTableColumn ( );
            return string . Empty;
        }

        delegate void AsynUpdateUI ( );//定义一个委托，使其可以更新UI控件的状态
        void other ( IAsyncResult isOver )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUI ( delegate ( )
                {
                    txtLGP001 . Properties . DataSource = table . DefaultView . ToTable ( true ,"LGN001" );
                    txtLGP001 . Properties . DisplayMember = "LGN001";
                    txtLGP001 . Properties . ValueMember = "LGN001";

                    txtLOG002 . Properties . DataSource = table . DefaultView . ToTable ( true ,"LOG002" );
                    txtLOG002 . Properties . DisplayMember = "LOG002";
                    txtLOG002 . Properties . ValueMember = "LOG002";

                    txtLOG004 . Properties . DataSource = table . DefaultView . ToTable ( true ,"LOG004" );
                    txtLOG004 . Properties . DisplayMember = "LOG004";
                    txtLOG004 . Properties . ValueMember = "LOG004";

                    txtLOG005 . Properties . DataSource = table . DefaultView . ToTable ( true ,"LOG005" );
                    txtLOG005 . Properties . DisplayMember = "LOG005";
                    txtLOG005 . Properties . ValueMember = "LOG005";
                } ) );
            }
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtLGP001 . Text ) )
                strWhere = strWhere + " AND LGN001='" + txtLGP001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLOG002 . Text ) )
                strWhere = strWhere + " AND LOG002='" + txtLOG002 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLOG004 . Text ) )
                strWhere = strWhere + " AND LOG004='" + txtLOG004 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLOG005 . Text ) )
                strWhere = strWhere + " AND LOG005='" + txtLOG005 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere = strWhere + " AND LGN002='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
            if ( !string . IsNullOrEmpty ( txtSa . Text ) )
                strWhere = strWhere + " AND LGN007='" + txtSa . Text + "'";

            tableView = _bll . getTableView ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtLGP001 . EditValue = txtLOG002 . EditValue = txtLOG004 . EditValue = txtLOG005 . EditValue =dateEdit1.Text= null;
        }

        string code=string.Empty;
        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            code = gridView1 . GetFocusedRowCellValue ( "LGN001" ) . ToString ( );
            if ( code == string . Empty )
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