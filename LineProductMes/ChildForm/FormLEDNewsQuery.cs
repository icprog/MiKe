using System;
using System . Data;
using System . Windows . Forms;
using System . Threading;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes
{
    public partial class FormLEDNewsQuery :FormBaseChild
    {
        LineProductMesBll.Bll.LEDNewsBll _bll=null;

        Thread thread; SynchronizationContext m_SyncContext = null;
        DataTable tableView,tablColumn;
        string oddNum=string.Empty;

        public FormLEDNewsQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . LEDNewsBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 ,View3 ,View4 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 ,View3 ,View4  } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext . Current;
            thread = new Thread ( new ThreadStart ( InitData ) );
            thread . Start ( );
        }
        
        void InitData ( )
        {
            tablColumn = _bll . getTableQuery ( "1=1" );
            m_SyncContext . Post ( setData ,tablColumn );
        }
        void setData ( object data )
        {
            txtLEF001 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEF001" );
            txtLEF001 . Properties . DisplayMember = "LEF001";
            txtLEF001 . Properties . ValueMember = "LEF001";

            txtLEF002 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEH002" );
            txtLEF002 . Properties . DisplayMember = "LEH002";
            txtLEF002 . Properties . ValueMember = "LEH002";

            txtLEF003 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEH003" );
            txtLEF003 . Properties . DisplayMember = "LEH003";
            txtLEF003 . Properties . ValueMember = "LEH003";

            txtLEF004 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEH004" );
            txtLEF004 . Properties . DisplayMember = "LEH004";
            txtLEF004 . Properties . ValueMember = "LEH004";
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtLEF001 . EditValue = txtLEF002 . EditValue = txtLEF003 . EditValue = txtLEF004 . EditValue =dateEdit1.Text= null;
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtLEF001 . Text ) )
                strWhere += " AND LEF001='" + txtLEF001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLEF002 . Text ) )
                strWhere += " AND LEH002='" + txtLEF002 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLEF003 . Text ) )
                strWhere += " AND LEH003='" + txtLEF003 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLEF004 . Text ) )
                strWhere += " AND LEH004='" + txtLEF004 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere += " AND LEF013='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
            
            tableView = _bll . getTableQuery ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            oddNum = gridView1 . GetFocusedRowCellValue ( "LEF001" ) . ToString ( );
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