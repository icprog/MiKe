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
    public partial class FormLEDNewsPaperQuery :FormBaseChild
    {
        LineProductMesBll.Bll.LEDNewsPaperBll _bll=null;
        
        Thread thread; SynchronizationContext m_SyncContext = null;
        DataTable tableView,tablColumn;
        string oddNum=string.Empty;

        public FormLEDNewsPaperQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . LEDNewsPaperBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 ,View3 ,View4  } );
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
            txtLEC001 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEC001" );
            txtLEC001 . Properties . DisplayMember = "LEC001";
            txtLEC001 . Properties . ValueMember = "LEC001";

            txtLEC002 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEE002" );
            txtLEC002 . Properties . DisplayMember = "LEE002";
            txtLEC002 . Properties . ValueMember = "LEE002";

            txtLEC003 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEE003" );
            txtLEC003 . Properties . DisplayMember = "LEE003";
            txtLEC003 . Properties . ValueMember = "LEE003";

            txtLEC004 . Properties . DataSource = tablColumn . DefaultView . ToTable ( true ,"LEE004" );
            txtLEC004 . Properties . DisplayMember = "LEE004";
            txtLEC004 . Properties . ValueMember = "LEE004";
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtLEC001 . EditValue = txtLEC002 . EditValue = txtLEC003 . EditValue = txtLEC004 . EditValue =dateEdit1.Text= null;
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
            string strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtLEC001 . Text ) )
                strWhere += " AND LEC001='" + txtLEC001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLEC002 . Text ) )
                strWhere += " AND LEE002='" + txtLEC002 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLEC003 . Text ) )
                strWhere += " AND LEE003='" + txtLEC003 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtLEC004 . Text ) )
                strWhere += " AND LEE004='" + txtLEC004 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere += " AND LEC013='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";

            tableView = _bll . getTableQuery ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            oddNum = gridView1 . GetFocusedRowCellValue ( "LEC001" ) . ToString ( );
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