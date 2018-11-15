using System;
using System . Data;
using System . Windows . Forms;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using Utility;

namespace LineProductMes
{
    public partial class FormPurchaseBoard :FormBaseChild
    {
        LineProductMesEntityu.PurchaseBoardEntity entity=null;
        LineProductMesBll.Bll.PurchaseBoardBll _bll;

        DateTime dt;
        DataTable tableView,tableOne,table,tableViewOne,tableTwo,tableTre;
        bool result=false;
        int timeChange=0,numData=0,timeChangeOne=0,numDataTwo=0;

        public delegate void ReadDataTableHandler ( bool resu );
        public event ReadDataTableHandler tableHandler;

        public FormPurchaseBoard ( )
        {
            InitializeComponent ( );

            entity = new LineProductMesEntityu . PurchaseBoardEntity ( );
            _bll = new LineProductMesBll . Bll . PurchaseBoardBll ( );

            Control . CheckForIllegalCrossThreadCalls = false;
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,bandedGridView2 } );


            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            dt = LineProductMesBll . UserInfoMation . sysTime;

            this . Text = this . Text + ( "(日期:" + dt . AddDays ( -1 ) + ")" );

            setColumnBand ( );
        }

        private void FormPurchaseBoard_Load ( object sender ,EventArgs e )
        {

            this . tableHandler += new ReadDataTableHandler ( getData );

            Func<string> funStr = getDataView;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( other ) ,null );

            timeChange = timeChangeOne = 1;

            System . Timers . Timer t = new System . Timers . Timer ( 1000 * 60 * timeChange );
            t . Elapsed += new System . Timers . ElapsedEventHandler ( refreData );
            t . AutoReset = true;
            t . Enabled = true;

            System . Timers . Timer tOne = new System . Timers . Timer ( 1000 * 60 * timeChangeOne );
            tOne . Elapsed += new System . Timers . ElapsedEventHandler ( refreDataOne );
            tOne . AutoReset = true;
            tOne . Enabled = true;
        }

        void getData ( bool result )
        {
            if ( result )
            {
                tableView = _bll . getTableView ( );
                timeChange = LineProductMesBll . ObtainInfo . getTimeChange ( "采购看板" );
                numData = LineProductMesBll . ObtainInfo . getNumData ( "采购看板" );
                tableViewOne = _bll . getTableViewOne ( );
                timeChangeOne = LineProductMesBll . ObtainInfo . getTimeChange ( "采购超期" );
                numDataTwo = LineProductMesBll . ObtainInfo . getNumData ( "采购超期" );
                result = false;

            }
        }
        string getDataView ( )
        {
            tableView = _bll . getTableView ( );
            timeChange = LineProductMesBll . ObtainInfo . getTimeChange ( "采购看板" );
            numData = LineProductMesBll . ObtainInfo . getNumData ( "采购看板" );
            tableViewOne = _bll . getTableViewOne ( );
            timeChangeOne = LineProductMesBll . ObtainInfo . getTimeChange ( "采购超期" );
            numDataTwo = LineProductMesBll . ObtainInfo . getNumData ( "采购超期" );
            return string . Empty;
        }

        delegate void AsynUpdateUI ( );//定义一个委托，使其可以更新UI控件的状态
        void other ( IAsyncResult isOver )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUI ( delegate ( )
                {
                    table = getTable ( numData );
                    this . gridControl1 . DataSource = table;
                    //bandedGridView1 . BestFitColumns ( );
                    tableTre = getTableOne ( numDataTwo );
                    gridControl2 . DataSource = tableTre;
                    bandedGridView2 . BestFitColumns ( );
                } ) );
            }
        }

        DataTable getTable ( int nums )
        {
            if ( tableOne == null )
                tableOne = tableView . Copy ( );
            DataTable tableTwo = tableOne . Clone ( );
            if ( tableOne . Rows . Count <= nums )
            {
                nums = tableOne . Rows . Count;
                result = true;
                tableHandler ( result );
            }
            DataRow [ ] rows = tableOne . Select ( "1=1" );
            for ( int i = 0 ; i < nums ; i++ )
            {
                tableTwo . ImportRow ( ( DataRow ) rows [ i ] );
                tableOne . Rows . Remove ( rows [ i ] );
            }
            if ( tableOne . Rows . Count == 0 )
            {
                tableOne = tableView . Copy ( );
            }

            return tableTwo;
        }

        DataTable getTableOne ( int nums )
        {
            if ( tableTwo == null )
                tableTwo = tableViewOne . Copy ( );
            DataTable tableFor = tableTwo . Clone ( );
            if ( tableTwo . Rows . Count <= nums )
            {
                nums = tableTwo . Rows . Count;
                result = true;
                tableHandler ( result );
            }
            DataRow [ ] rows = tableTwo . Select ( "1=1" );
            for ( int i = 0 ; i < nums ; i++ )
            {
                tableFor . ImportRow ( ( DataRow ) rows [ i ] );
                tableTwo . Rows . Remove ( rows [ i ] );
            }
            if ( tableTwo . Rows . Count == 0 )
            {
                tableTwo = tableViewOne . Copy ( );
            }

            return tableFor;
        }

        void refreData ( object source ,System . Timers . ElapsedEventArgs e )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUI ( delegate ( )
                {
                    result = false;

                    table = getTable ( numData );
                    gridControl1 . DataSource = table;
                    //bandedGridView1 . BestFitColumns ( );
                    gridControl1 . Refresh ( );

                } ) );
            }
        }
        void refreDataOne ( object source ,System . Timers . ElapsedEventArgs e )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUI ( delegate ( )
                {
                    result = false;

                    tableTre = getTableOne ( numDataTwo );
                    gridControl2 . DataSource = tableTre;
                    bandedGridView2 . BestFitColumns ( );
                    gridControl2 . Refresh ( );

                } ) );
            }
        }

        void setColumnBand ( )
        {
            g1 . Caption = dt . AddDays ( -1 ) . ToString ( "yyyy-MM-dd" );
            g2 . Caption = dt . ToString ( "yyyy-MM-dd" );
            g3 . Caption = dt . AddDays ( 1 ) . ToString ( "yyyy-MM-dd" );
            g4 . Caption = dt . AddDays ( 2 ) . ToString ( "yyyy-MM-dd" );
            g5 . Caption = dt . AddDays ( 3 ) . ToString ( "yyyy-MM-dd" );
            g6 . Caption = dt . AddDays ( 4 ) . ToString ( "yyyy-MM-dd" );
            g7 . Caption = dt . AddDays ( 5 ) . ToString ( "yyyy-MM-dd" );
        }

        private void bandedGridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        private void bandedGridView2_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        private void bandedGridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            DataRow row = bandedGridView1 . GetDataRow ( e . RowHandle );
            if ( row == null )
                return;
            if ( e . CellValue == null || e . CellValue . ToString ( ) == string . Empty )
                return;
            entity . PUB002 = string . IsNullOrEmpty ( row [ "PUB002" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB002" ] );
            entity . PUB003 = string . IsNullOrEmpty ( row [ "PUB003" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB003" ] );
            entity . PUB004 = string . IsNullOrEmpty ( row [ "PUB004" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB004" ] );
            if ( e . Column . FieldName == "PUB005" )
            {
                entity . PUB006 = string . IsNullOrEmpty ( row [ "PUB006" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB006" ] );
                entity . PUB005 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB005 > entity . PUB003 + entity . PUB004 + entity . PUB006 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }else if ( e . Column . FieldName == "PUB007" )
            {
                entity . PUB008 = string . IsNullOrEmpty ( row [ "PUB008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB008" ] );
                entity . PUB007 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB007 > entity . PUB003 + entity . PUB004 + entity . PUB008 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }
            else if ( e . Column . FieldName == "PUB009" )
            {
                entity . PUB010 = string . IsNullOrEmpty ( row [ "PUB010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB010" ] );
                entity . PUB009 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB009 > entity . PUB003 + entity . PUB004 + entity . PUB010 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }
            else if ( e . Column . FieldName == "PUB011" )
            {
                entity . PUB012 = string . IsNullOrEmpty ( row [ "PUB012" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB012" ] );
                entity . PUB011 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB011 > entity . PUB003 + entity . PUB004 + entity . PUB012 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }
            else if ( e . Column . FieldName == "PUB013" )
            {
                entity . PUB014 = string . IsNullOrEmpty ( row [ "PUB014" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB014" ] );
                entity . PUB013 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB013 > entity . PUB003 + entity . PUB004 + entity . PUB014 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }
            else if ( e . Column . FieldName == "PUB015" )
            {
                entity . PUB016 = string . IsNullOrEmpty ( row [ "PUB016" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB016" ] );
                entity . PUB015 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB015 > entity . PUB003 + entity . PUB004 + entity . PUB016 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }
            else if ( e . Column . FieldName == "PUB017" )
            {
                entity . PUB018 = string . IsNullOrEmpty ( row [ "PUB018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "PUB018" ] );
                entity . PUB017 = Convert . ToInt32 ( e . CellValue . ToString ( ) );
                if ( entity . PUB002 + entity . PUB017 > entity . PUB003 + entity . PUB004 + entity . PUB018 )
                {
                    e . Appearance . BackColor = System . Drawing . Color . Red;
                    e . Appearance . ForeColor = System . Drawing . Color . White;
                }
            }
        }

        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            if ( e . ClickedItem . Name == "Export" )
            {

            }
        }
        private void btnExport_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( dtStart . Text ) )
            {
                MessageBox . Show ( "请选择开始日期" );
                return;
            }
            if ( string . IsNullOrEmpty ( dtEnd . Text ) )
            {
                MessageBox . Show ( "请选择结束日期" );
                return;
            }
            int ts = ( Convert . ToDateTime ( dtEnd . Text ) - Convert . ToDateTime ( dtStart . Text ) ) . Days;
            if ( ts <= 0 )
            {
                MessageBox . Show ( "结束日期必须大于开始日期" );
                return;
            }

            DataTable tableExport = _bll . getTableViewExport ( dtStart . Text ,dtEnd . Text );

            saveFileDialog1 . Filter = "Execl(*.xls)|*.xls|Execl(*.xlsx)|*.xlsx";
            if ( saveFileDialog1 . ShowDialog ( ) == DialogResult . Cancel )
                return;

            ExportExcel . TableToExcel ( tableExport ,saveFileDialog1 . FileName );
        }

    }
}