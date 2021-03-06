﻿using DevExpress . Utils . Paint;
using DevExpress . XtraCharts;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Linq;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class FormLEGBoard :FormBaseChild
    {
        LineProductMesBll.Bll.LEGBoardBll _bll=null;
        DataTable tableView,tableOne,table;
        bool result=false,isOk=false;
        int timeChange=0,numData=0;

        public delegate void ReadDataTableHandler ( bool resu );
        public event ReadDataTableHandler tableHandler;

        public delegate void DrawChartUiHandler ( bool resu );
        public event DrawChartUiHandler chartHandler;

        public FormLEGBoard ( )
        {
            InitializeComponent ( );

            Control . CheckForIllegalCrossThreadCalls = false;

            _bll = new LineProductMesBll . Bll . LEGBoardBll ( );
            tableView = new DataTable ( );

            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            this . Text = this . Text + ( "(日期:" + LineProductMesBll . ObtainInfo . getDate ( "LED车间" ) + ")" );
        }

        private void FormLEGBoard_Load ( object sender ,EventArgs e )
        {
            this . tableHandler += new ReadDataTableHandler ( getData );
            this . chartHandler += new DrawChartUiHandler ( drawUi );

            Func<string> funStr = getDataView;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( other ) ,null );

            timeChange = 1;

            System . Timers . Timer t = new System . Timers . Timer ( 1000 * 60 * timeChange );
            t . Elapsed += new System . Timers . ElapsedEventHandler ( refreData );
            t . AutoReset = true;
            t . Enabled = true;
        }

        void getData ( bool result )
        {
            if ( result )
            {
                tableView = _bll . getTableView ( );
                timeChange = LineProductMesBll . ObtainInfo . getTimeChange ( "LED车间" );
                numData = LineProductMesBll . ObtainInfo . getNumData ( "LED车间" );
                result = false;
                isOk = true;
                chartHandler ( isOk );
            }
        }

        string getDataView ( )
        {
            tableView = _bll . getTableView ( );
            timeChange = LineProductMesBll . ObtainInfo . getTimeChange ( "LED车间" );
            numData = LineProductMesBll . ObtainInfo . getNumData ( "LED车间" );
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
                    gridView1 . PopulateColumns ( );
                    columnSet ( );
                    gridView1 . BestFitColumns ( );
                    //chartOne ( );
                    chartTwo ( );
                } ) );
            }
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
                    gridView1 . PopulateColumns ( );
                    columnSet ( );
                    gridView1 . BestFitColumns ( );
                    gridControl1 . Refresh ( );
                } ) );
            }
        }

        void drawUi ( bool result )
        {
            //chartOne ( );
            chartTwo ( );
            isOk = false;
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


        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        void columnSet ( )
        {
            if ( tableView == null )
                return;
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                column . Name = column . Name . Replace ( "col" ,"" );
                if ( column . FieldName == "PRG005" )
                    column . Caption = "产线";
                else if ( column . FieldName == "PRG001" )
                    column . Caption = "品号";
                else if ( column . FieldName == "DEA002" )
                    column . Caption = "品名";
                else if ( column . FieldName == "DEA057" )
                    column . Caption = "规格";
                else if ( column . FieldName == "PRF003" )
                {
                    column . Caption = "总计划量";
                    column . ToolTip = "今日止以前的计划量之和";
                }
                else if ( column . FieldName == "PRG003" )
                    column . Caption = "当日计划量";
                else if ( column . FieldName == "ANW9" )
                    column . Caption = "当日完成量";
                else if ( column . FieldName == "PRF" )
                {
                    column . Caption = "总欠产量";
                    column . ToolTip = "总计划量-总完成量";
                }
                else if ( column . FieldName == "PRF3" )
                {
                    column . Caption = "计划余量";
                    column . ToolTip = "计划总量-已完成量";
                }
                else if ( column . FieldName == "LEH" )
                    column . Caption = "总完成量";
                else if ( column . FieldName == "ANW009" )
                {
                    column . Caption = "当日完成率";
                    column . ColumnEdit = this . repositoryItemProgressBar1;
                }
                else if ( column . FieldName == "ANW09" )
                {
                    column . Caption = "总完成率";
                    column . ColumnEdit = this . repositoryItemProgressBar2;
                }
                else
                    column . Caption = column . Name + "计划量";
            }
        }

        //void chartOne ( )
        //{
        //    var query = from p in tableView . AsEnumerable ( )
        //                group p by new
        //                {
        //                    p1 = p . Field<string> ( "PRG001" ) ,
        //                    p2 = p . Field<int> ( "PRF003" )
        //                } into m
        //                let sum1 = m . Sum ( t => t . Field<int> ( "PRF003" ) )
        //                let sum2 = m . Sum ( t => t . Field<int> ( "PRF" ) )
        //                select new
        //                {
        //                    prg001 = m . Key . p1 ,
        //                    sum1 = sum1 ,
        //                    sum2 = sum2
        //                };
        //    DataTable table = new DataTable ( );
        //    table . Columns . Add ( "X" ,typeof ( string ) );
        //    table . Columns . Add ( "Y" ,typeof ( decimal ) );
        //    foreach ( var x in query )
        //    {
        //        DataRow row = table . NewRow ( );
        //        row [ "X" ] = x . prg001;
        //        row [ "Y" ] = x . sum1 == 0 ? 0 . ToString ( ) : Math . Round ( ( x . sum1 - x . sum2 ) * 1.0 / x . sum1 * 100 ,2 ) . ToString ( );
        //        table . Rows . Add ( row );
        //    }
        //    Series seOne = chartControl2 . GetSeriesByName ( "SeriesOne" );
        //    ( ( PiePointOptions ) seOne . PointOptions ) . PercentOptions . ValueAsPercent = true;
        //    ( ( PiePointOptions ) seOne . PointOptions ) . ValueNumericOptions . Format = NumericFormat . Percent;
        //    seOne . ArgumentScaleType = ScaleType . Qualitative;
        //    seOne . ValueScaleType = ScaleType . Numerical;
        //    seOne . PointOptions . PointView = PointView . ArgumentAndValues;
        //    seOne . DataSource = table;
        //    seOne . ArgumentDataMember = "X";
        //    seOne . ValueDataMembers [ 0 ] = "Y";
        //    chartControl2 . Series . Add ( seOne );
        //}

        void chartTwo ( )
        {
            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "DEA002" )
                        } into m
                        let sum = m . Sum ( t => t . Field<int> ( "PRF" ) )
                        select new
                        {
                            dea002 = m . Key . p1 ,
                            sum = sum
                        };
            DataTable table = new DataTable ( );
            table . Columns . Add ( "X" ,typeof ( string ) );
            table . Columns . Add ( "Y" ,typeof ( int ) );
            foreach ( var x in query )
            {
                DataRow row = table . NewRow ( );
                row [ "X" ] = x . dea002;
                row [ "Y" ] = x . sum;
                table . Rows . Add ( row );
            }
            Series seOne = chartControl1 . GetSeriesByName ( "SeriesOne" );
            seOne . DataSource = table;
            seOne . ArgumentDataMember = "X";
            seOne . ValueDataMembers [ 0 ] = "Y";
            chartControl1 . Series . Add ( seOne );
        }

        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            //if ( e . Column . FieldName == "PRF" )
            //{
            //    if ( e . CellValue != null && e . CellValue . ToString ( ) != string . Empty )
            //    {
            //        if ( Convert . ToInt32 ( e . CellValue ) != 0 )
            //            e  . Appearance . BackColor = System . Drawing . Color . Red;
            //    }
            //}
        }
        private void gridView1_RowStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowStyleEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( row [ "PRF" ] != null && row [ "PRF" ] . ToString ( ) != string . Empty )
            {
                if ( Convert . ToInt32 ( row [ "PRF" ] ) != 0 )
                    e . Appearance . BackColor = System . Drawing . Color . MistyRose;
            }
        }

    }
}