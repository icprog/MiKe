using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Drawing;
using System . Text;
using System . Windows . Forms;
using System . IO;
using System . Collections;
using System . Data . SqlClient;
using System . Diagnostics;
using System . Reflection;
using NPOI . HSSF . UserModel;
using NPOI . SS . UserModel;
using NPOI . XSSF . UserModel;

namespace LineProductMes . ClassForMain
{
    public class ExportExcel
    {
        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExcel ( DataTable dt ,string file )
        {
            IWorkbook workbook;
            string fileExt = Path . GetExtension ( file ) . ToLower ( );
            if ( fileExt == ".xlsx" )
            {
                workbook = new XSSFWorkbook ( );
            }
            else if ( fileExt == ".xls" )
            {
                workbook = new HSSFWorkbook ( );
            }
            else
            {
                workbook = null;
            }
            if ( workbook == null )
            {
                return;
            }
            ISheet sheet = string . IsNullOrEmpty ( dt . TableName ) ? workbook . CreateSheet ( "Sheet1" ) : workbook . CreateSheet ( dt . TableName );

            //表头  
            IRow row = sheet . CreateRow ( 0 );
            for ( int i = 0 ; i < dt . Columns . Count ; i++ )
            {
                ICell cell = row . CreateCell ( i );
                cell . SetCellValue ( dt . Columns [ i ] . ColumnName );
            }

            //数据  
            for ( int i = 0 ; i < dt . Rows . Count ; i++ )
            {
                IRow row1 = sheet . CreateRow ( i + 1 );
                for ( int j = 0 ; j < dt . Columns . Count ; j++ )
                {
                    ICell cell = row1 . CreateCell ( j );
                    cell . SetCellValue ( dt . Rows [ i ] [ j ] . ToString ( ) );
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream ( );
            workbook . Write ( stream );
            var buf = stream . ToArray ( );

            //保存为Excel文件  
            using ( FileStream fs = new FileStream ( file ,FileMode . Create ,FileAccess . Write ) )
            {
                fs . Write ( buf ,0 ,buf . Length );
                fs . Flush ( );
            }
            stream . Close ( );
            stream . Dispose ( );
        }

    }
}
