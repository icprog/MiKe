using DevExpress . XtraCharts;
using DevExpress . XtraPrinting;
using DevExpress . XtraPrintingLinks;
using System;
using System . IO;
using System . Windows . Forms;

namespace LineProductMes . ClassForMain
{
    public static class ViewExport
    {
        /// <summary>
        /// DevExpress通用导出Excel,支持多个控件同时导出在同一个Sheet表
        /// eg:ExportToXlsx("",gridControl1,gridControl2);
        /// 将gridControl1和gridControl2的数据一同导出到同一张工作表
        /// </summary>
        /// <param name="title">文件名</param>
        /// <param name="panels">控件集</param>
        public static void ExportToExcel ( string title ,params IPrintable [ ] panels )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog ( );
            saveFileDialog . FileName = title;
            saveFileDialog . Title = "导出Excel";
            saveFileDialog . Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog . ShowDialog ( );
            if ( dialogResult == DialogResult . Cancel )
                return;
            string FileName = saveFileDialog . FileName;
            PrintingSystem ps = new PrintingSystem ( );
            CompositeLink link = new CompositeLink ( ps );
            ps . Links . Add ( link );
            foreach ( IPrintable panel in panels )
            {
                link . Links . Add ( CreatePrintableLink ( panel ) );
            }
            link . Landscape = true;//横向           
            //判断是否有标题，有则设置         
            //link.CreateDocument(); //建立文档
            try
            {
                int count = 1;
                //在重复名称后加（序号）
                while ( File . Exists ( FileName ) )
                {
                    if ( FileName . Contains ( ")." ) )
                    {
                        int start = FileName . LastIndexOf ( "(" );
                        int end = FileName . LastIndexOf ( ")." ) - FileName . LastIndexOf ( "(" ) + 2;
                        FileName = FileName . Replace ( FileName . Substring ( start ,end ) ,string . Format ( "({0})." ,count ) );
                    }
                    else
                    {
                        FileName = FileName . Replace ( "." ,string . Format ( "({0})." ,count ) );
                    }
                    count++;
                }

                if ( FileName . LastIndexOf ( ".xlsx" ) >= FileName . Length - 5 )
                {
                    XlsxExportOptions options = new XlsxExportOptions ( );
                    link . ExportToXlsx ( FileName ,options );
                }
                else
                {
                    XlsExportOptions options = new XlsExportOptions ( );
                    link . ExportToXls ( FileName ,options );
                }
                if ( DevExpress . XtraEditors . XtraMessageBox . Show ( "保存成功，是否打开文件？" ,"提示" ,MessageBoxButtons . YesNo ,MessageBoxIcon . Information ) == DialogResult . Yes )
                    System . Diagnostics . Process . Start ( FileName );//打开指定路径下的文件
            }
            catch ( Exception ex )
            {
                DevExpress . XtraEditors . XtraMessageBox . Show ( ex . Message );
            }
        }
        /// <summary>
        /// 创建打印Componet
        /// </summary>
        /// <param name="printable"></param>
        /// <returns></returns>
        static PrintableComponentLink CreatePrintableLink ( IPrintable printable )
        {
            ChartControl chart = printable as ChartControl;
            if ( chart != null )
                chart . OptionsPrint . SizeMode = DevExpress . XtraCharts . Printing . PrintSizeMode . Stretch;
            PrintableComponentLink printableLink = new PrintableComponentLink ( ) { Component = printable };
            return printableLink;
        }

        //没有格式要求，想快速导出DataTable数据到Excel可以采用以下方式实现，开发人员在调试过程中有时候也可以用来记录比较。100w的数据导出也就1~2秒的时间
        /*
        void Main ( )
        {
            DataTable dataTable = new DataTable ( );

            for ( int i = 0 ; i < 10 ; i++ )
            {
                dataTable . Columns . Add ( "col" + i );
            }
            for ( int i = 0 ; i < 1000000 ; i++ )
            {
                var dr = dataTable . NewRow ( );
                foreach ( DataColumn col in dataTable . Columns )
                {
                    dr [ col ] = col . ColumnName + i;
                }
                dataTable . Rows . Add ( dr );
            }
            string fileName = "excel.csv";
            Stopwatch watch = new Stopwatch ( );
            watch . Start ( );
            ExportToExcel ( dataTable ,fileName );
            watch . Stop ( );
            ( "导出完毕,用时:" + watch . Elapsed ) . Dump ( );
        }
        public static void ExportToExcel ( DataTable dataTable ,string fileName ,bool isOpen = false )
        {
            var lines = new List<string> ( );
            string [ ] columnNames = dataTable . Columns
                                            . Cast<DataColumn> ( )
                                            . Select ( column => column . ColumnName )
                                            . ToArray ( );
            var header = string . Join ( "," ,columnNames );
            lines . Add ( header );
            var valueLines = dataTable . AsEnumerable ( )
                            . Select ( row => string . Join ( "," ,row . ItemArray ) );
            lines . AddRange ( valueLines );
            File . WriteAllLines ( fileName ,lines );
            if ( isOpen )
                Process . Start ( fileName );
        }
        */

    }
}
