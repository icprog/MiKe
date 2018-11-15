using NPOI . HSSF . UserModel;
using NPOI . SS . UserModel;
using NPOI . SS . Util;
using System;
using System . Collections . Generic;
using System . Data;
using System . IO;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMes . ClassForMain
{
    public class NPOIHelper
    {
        #region 实例化
        /// <summary>
        /// 文件名称
        /// </summary>
        public string _fileName
        {
            get; private set;
        }
        /// <summary>
        /// 工作薄名称，多工作薄以";"隔开，例如：测试1;测试2;测试3
        /// </summary>
        public string _sheetName
        {
            get; private set;
        }
        /// <summary>
        /// 每个工作薄只能有一个标题，多工作薄时，标题以";"隔开
        /// </summary>
        public string _title
        {
            get; private set;
        }
        /// <summary>
        /// 表头格式
        /// 相邻父列头之间用'#'分隔,父列头与子列头用空格(' ')分隔,相邻子列头用逗号分隔(',')
        /// 两行：序号#分公司#组别#本日成功签约单数 预警,续约,流失,合计#累计成功签约单数 预警,续约,流失,合计#任务数#完成比例#排名 
        /// 三行：等级#级别#上期结存 件数,重量,比例#本期调入 收购调入 件数,重量,比例#本期发出 车间投料 件数,重量,比例#本期发出 产品外销百分比 件数,重量,比例#平均值 
        /// 三行时请注意：列头要重复  
        /// </summary>
        public string _header
        {
            get; private set;
        }
        /// <summary>
        /// 文件路径 如果WEB导出时，路径可为空；
        /// </summary>
        public string _filePath
        {
            get; private set;
        }
        /// <summary>
        /// 导出方式 1：WEB导出 2：路径导出
        /// </summary>
        public int _exportMode
        {
            get; private set;
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public DataSet _dsSource
        {
            get; private set;
        }
        /// <summary>
        /// 数据库采用字段
        /// </summary>
        public string _filed
        {
            get; private set;
        }
        /// <summary>
        /// HSSFWorkbook 对象
        /// </summary>
        private HSSFWorkbook _workbook;
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="sheetName">工作薄名称，多工作薄以";"隔开，例如：测试1;测试2;测试3</param>
        /// <param name="header">
        /// 相邻父列头之间用'#'分隔,父列头与子列头用空格(' ')分隔,相邻子列头用逗号分隔(',')
        /// 两行：序号#分公司#组别#本日成功签约单数 预警,续约,流失,合计#累计成功签约单数 预警,续约,流失,合计#任务数#完成比例#排名 
        /// 三行：等级#级别#上期结存 件数,重量,比例#本期调入 收购调入 件数,重量,比例#本期发出 车间投料 件数,重量,比例#本期发出 产品外销百分比 件数,重量,比例#平均值 
        /// 三行时请注意：列头要重复
        /// </param>
        /// <param name="ds">数据来源</param>
        /// <param name="filed">数据字段</param>
        public NPOIHelper ( string fileName ,string sheetName ,string header ,DataSet ds ,string filed )
        {
            // 判断文件后缀名是否存在
            if ( string . IsNullOrEmpty ( fileName ) )
                fileName = "新建Excel.xls";
            else if ( fileName . IndexOf ( ".xls" ) == -1 && fileName . IndexOf ( ".xlsx" ) == -1 )
                fileName += ".xls";

            this . _workbook = new HSSFWorkbook ( );
            this . _fileName = fileName;
            this . _header = header;
            if ( string . IsNullOrEmpty ( sheetName ) )
                throw new ArgumentNullException ( "sheetName" ,"工作薄名称不能为空" );
            this . _sheetName = sheetName;
            this . _exportMode = 1;
            this . _dsSource = ds;
            this . _filed = filed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="sheetName">工作薄名称，多工作薄以";"隔开，例如：测试1;测试2;测试3</param>
        /// <param name="header">
        /// 相邻父列头之间用'#'分隔,父列头与子列头用空格(' ')分隔,相邻子列头用逗号分隔(',')
        /// 两行：序号#分公司#组别#本日成功签约单数 预警,续约,流失,合计#累计成功签约单数 预警,续约,流失,合计#任务数#完成比例#排名 
        /// 三行：等级#级别#上期结存 件数,重量,比例#本期调入 收购调入 件数,重量,比例#本期发出 车间投料 件数,重量,比例#本期发出 产品外销百分比 件数,重量,比例#平均值 
        /// 三行时请注意：列头要重复
        /// </param>
        /// <param name="title">每个工作薄只能有一个标题，多工作薄时，标题以";"隔开</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="exportMode">导出方式 1：WEB导出 2：路径导出</param>
        /// <param name="ds">数据来源</param>
        /// <param name="filed">数据字段</param>
        public NPOIHelper ( string fileName ,string sheetName ,string header ,string title ,DataSet ds ,string filed ,string filePath = null ,int exportMode = 1 )
        {
            // 判断文件后缀名是否存在
            if ( string . IsNullOrEmpty ( fileName ) )
                fileName = "新建Excel.xls";
            else if ( fileName . IndexOf ( ".xls" ) == -1 && fileName . IndexOf ( ".xlsx" ) == -1 )
                fileName += ".xls";

            this . _workbook = new HSSFWorkbook ( );
            this . _fileName = fileName;

            if ( string . IsNullOrEmpty ( sheetName ) )
                throw new ArgumentNullException ( "sheetName" ,"工作薄名称不能为空" );
            this . _sheetName = sheetName;
            this . _header = header;
            this . _title = title;

            if ( 2 == exportMode && string . IsNullOrEmpty ( filePath ) )
                filePath = Environment . GetFolderPath ( Environment . SpecialFolder . Desktop );

            this . _filePath = filePath;
            this . _exportMode = exportMode;
            this . _dsSource = ds;
            this . _filed = filed;
        }
        #endregion

        #region 功能实现
        /// <summary>
        /// 实现导出功能
        /// </summary>
        public void Export ( )
        {
            #region 变量声明
            string [ ] tableTitle = this . _header . Split ( new char [ ] { ';' } ,StringSplitOptions . RemoveEmptyEntries );
            // 表头数组
            string [ ] newHeaders = null;
            // 数据字段
            string [ ] files = this . _filed . Split ( new char [ ] { ';' } ,StringSplitOptions . RemoveEmptyEntries );
            // 数据字段
            string [ ] file = null;
            // 临时数组
            string [ ] temp = null;
            // 临时表头
            string tempHeader = string . Empty;
            // 工作薄名称
            string [ ] sheetNames = this . _sheetName . Split ( new string [ ] { ";" } ,StringSplitOptions . RemoveEmptyEntries );
            // 表头名称
            string [ ] titles = this . _title == null ? null : this . _title . Split ( new string [ ] { ";" } ,StringSplitOptions . RemoveEmptyEntries );
            // 获取行数
            int rows = GetRowCount ( this . _header );
            // 列数计数器
            int cols = 0;
            // 列头跨行数
            int rowSpans = 0;
            // 列头跨列数
            int colSpans = GetColCount ( this . _header );
            // HSSFSheet 对象
            HSSFSheet sheet = null;
            // IRow 对象
            IRow row = null;
            // 表头行添加
            int trow = ( string . IsNullOrEmpty ( this . _title ) ? 0 : 1 );
            DataTable dt;
            #endregion

            #region 单元格样式
            ICellStyle style = _workbook . CreateCellStyle ( );
            style . Alignment = HorizontalAlignment . Center; //居中
            style . VerticalAlignment = VerticalAlignment . Center;//垂直居中 
            style . WrapText = true;//自动换行
            // 边框
            style . BorderBottom = BorderStyle . Thin;
            style . BorderLeft = BorderStyle . Thin;
            style . BorderRight = BorderStyle . Thin;
            style . BorderTop = BorderStyle . Thin;
            // 字体
            IFont font = _workbook . CreateFont ( );
            font . FontHeightInPoints = 10;
            font . FontName = "宋体";
            style . SetFont ( font );

            ICellStyle titleType = _workbook . CreateCellStyle ( );
            titleType . Alignment = HorizontalAlignment . Center; //居中
            titleType . VerticalAlignment = VerticalAlignment . Center;//垂直居中 
            titleType . WrapText = true;//自动换行
            // 边框
            titleType . BorderBottom = BorderStyle . Thin;
            titleType . BorderLeft = BorderStyle . Thin;
            titleType . BorderRight = BorderStyle . Thin;
            titleType . BorderTop = BorderStyle . Thin;

            IFont font2 = _workbook . CreateFont ( );
            font2 . FontHeightInPoints = 14;
            font2 . FontName = "宋体";
            font2 . Boldweight = ( short ) FontBoldWeight . Bold;
            titleType . SetFont ( font2 );
            #endregion

            // 表格绘制
            for ( int k = 0 ; k < sheetNames . Length ; k++ )
            {
                #region 表头绘制
                newHeaders = tableTitle [ k ] . Split ( new char [ ] { '#' } ,StringSplitOptions . RemoveEmptyEntries );
                sheet = ( HSSFSheet ) _workbook . CreateSheet ( sheetNames [ k ] );
                for ( int m = 0 ; m < rows + trow ; m++ ) // 创建行
                {
                    if ( m == 0 && trow > 0 )
                    {
                        row = sheet . CreateRow ( 0 );
                        CellRangeAddress region = new CellRangeAddress ( 0 ,0 ,0 ,colSpans - 1 );
                        sheet . AddMergedRegion ( region );
                        row . CreateCell ( 0 ) . SetCellValue ( titles [ k ] );
                        row . GetCell ( 0 ) . CellStyle = titleType;
                        row . Height = 20 * 20;
                        continue;
                    }
                    cols = 0;
                    for ( int i = 0 ; i < newHeaders . Length ; i++ ) // 创建列
                    {

                        tempHeader = newHeaders [ i ];
                        // 获取列头跨行数
                        rowSpans = GetRowSpan ( tempHeader ,rows );
                        // 获取列头跨列数
                        colSpans = GetColSpan ( tempHeader );

                        // 如果表头还可以划分
                        temp = tempHeader . Split ( new char [ ] { ' ' } );
                        if ( temp . Length == rows )
                            tempHeader = temp [ m - trow ];
                        else
                            tempHeader = temp [ 0 ];



                        if ( 1 == rowSpans )
                        {
                            // 获取行
                            row = sheet . GetRow ( m );
                            if ( row == null )
                                row = sheet . CreateRow ( m );

                            // 未跨列
                            if ( 1 == colSpans )
                            {
                                row . CreateCell ( cols ) . SetCellValue ( tempHeader );
                                row . GetCell ( cols ) . CellStyle = style;
                            }
                            else // 跨列
                            {
                                temp = tempHeader . Split ( new char [ ] { ',' } );
                                if ( temp . Length > 1 )
                                {
                                    for ( int j = 0 ; j < temp . Length ; j++ )
                                    {
                                        row . CreateCell ( j + cols ) . SetCellValue ( temp [ j ] );
                                        row . GetCell ( j + cols ) . CellStyle = style;
                                    }
                                }
                                else
                                {
                                    // 创建范围
                                    //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                                    CellRangeAddress region = new CellRangeAddress ( m ,m ,cols ,cols + colSpans - 1 );
                                    sheet . AddMergedRegion ( region );
                                    row . CreateCell ( cols ) . SetCellValue ( tempHeader );
                                    row . GetCell ( cols ) . CellStyle = style;
                                }
                                cols += colSpans - 1;
                            }
                        }
                        else if ( rowSpans > 1 && m < 2 )
                        {
                            // 获取行
                            row = sheet . GetRow ( m );
                            if ( row == null )
                                row = sheet . CreateRow ( m );

                            // 未跨列
                            if ( 1 == colSpans )
                            {
                                // 创建范围
                                //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                                CellRangeAddress region = new CellRangeAddress ( m ,rowSpans - 1 + trow ,cols ,cols );
                                sheet . AddMergedRegion ( region );
                                row . CreateCell ( cols ) . SetCellValue ( tempHeader );
                                row . GetCell ( cols ) . CellStyle = style;
                            }
                            else
                            {
                                // 创建范围
                                //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                                CellRangeAddress region = new CellRangeAddress ( m ,rowSpans - 1 + trow ,cols ,cols + colSpans - 1 );
                                sheet . AddMergedRegion ( region );
                                row . CreateCell ( cols ) . SetCellValue ( tempHeader );
                                row . GetCell ( cols ) . CellStyle = style;
                                cols += colSpans - 1;
                            }
                        }
                        // 列计数器
                        cols += 1;
                    }
                }
                #endregion

                #region 数据源
                int rowIndex = rows + trow;
                foreach ( DataRow dr in this . _dsSource . Tables [ k ] . Rows )
                {
                    var dataRow = sheet . CreateRow ( rowIndex );
                    foreach ( DataColumn column in this . _dsSource . Tables [ k ] . Columns )
                    {
                        var newCell = dataRow . CreateCell ( column . Ordinal );

                        string drValue = dr [ column ] . ToString ( );

                        switch ( column . DataType . ToString ( ) )
                        {
                            case "System.String"://字符串类型
                            newCell . SetCellValue ( drValue );
                            break;
                            case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime . TryParse ( drValue ,out dateV );
                            newCell . SetCellValue ( dateV );

                            //newCell.CellStyle = dateStyle;//格式化显示
                            break;
                            case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool . TryParse ( drValue ,out boolV );
                            newCell . SetCellValue ( boolV );
                            break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                            int intV = 0;
                            int . TryParse ( drValue ,out intV );
                            newCell . SetCellValue ( intV );
                            break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                            double doubV = 0;
                            double . TryParse ( drValue ,out doubV );
                            newCell . SetCellValue ( doubV );
                            break;
                            case "System.DBNull"://空值处理
                            newCell . SetCellValue ( "" );
                            break;
                            default:
                            newCell . SetCellValue ( "" );
                            break;
                        }

                    }
                    rowIndex++;
                }
                #endregion
            }

            #region 数据导出
            // WEB导出
            //if ( 1 == this . _exportMode )
            //{
            //    System . Web . HttpContext . Current . Response . ContentType = "application/vnd.ms-excel";
            //    //设置下载的Excel文件名
            //    System . Web . HttpContext . Current . Response . AddHeader ( "Content-Disposition" ,string . Format ( "attachment;filename={0}" ,this . _fileName ) );

            //    using ( MemoryStream ms = new MemoryStream ( ) )
            //    {
            //        //将工作簿的内容放到内存流中
            //        _workbook . Write ( ms );
            //        //将内存流转换成字节数组发送到客户端
            //        System . Web . HttpContext . Current . Response . BinaryWrite ( ms . GetBuffer ( ) );
            //        System . Web . HttpContext . Current . Response . End ( );
            //        _workbook = null;
            //    }
            //}
            //else 
            if ( 2 == this . _exportMode )
            {
                using ( FileStream fs = File . Open ( this . _filePath ,FileMode . Append ) )
                {
                    _workbook . Write ( fs );
                    _workbook = null;
                }
            }
            #endregion
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取表头行数
        /// </summary>
        /// <param name="newHeaders">表头文字</param>
        /// <returns></returns>
        private int GetRowCount ( string newHeaders )
        {
            // 用@分割
            string [ ] ColumnNames = newHeaders . Split ( new char [ ] { '@' } );
            int Count = 0;
            if ( ColumnNames . Length <= 1 )
                ColumnNames = newHeaders . Split ( new char [ ] { '#' } );
            foreach ( string name in ColumnNames )
            {
                int TempCount = name . Split ( new char [ ] { ' ' } ) . Length;
                if ( TempCount > Count )
                    Count = TempCount;
            }
            return Count;
        }

        /// <summary>
        /// 获取表头列数
        /// </summary>
        /// <param name="newHeaders">表头文字</param>
        /// <returns></returns>
        private int GetColCount ( string newHeaders )
        {
            // 用@分割
            string [ ] ColumnNames = newHeaders . Split ( new char [ ] { '@' } );
            int Count = 0;
            if ( ColumnNames . Length <= 1 )
                ColumnNames = newHeaders . Split ( new char [ ] { '#' } );
            Count = ColumnNames . Length;
            foreach ( string name in ColumnNames )
            {
                int TempCount = name . Split ( new char [ ] { ',' } ) . Length;
                if ( TempCount > 1 )
                    Count += TempCount - 1;
            }
            return Count;
        }

        /// <summary>
        /// 列头跨列数
        /// </summary>
        /// <remarks>
        /// author:zhujt
        /// create date:2015-9-9 09:17:34
        /// </remarks>
        /// <param name="newHeaders">表头文字</param>
        /// <returns></returns>
        private int GetColSpan ( string newHeaders )
        {
            return newHeaders . Split ( ',' ) . Count ( );
        }

        /// <summary>
        /// 列头跨行数
        /// </summary> 
        /// <remarks>
        /// author:zhujt
        /// create date:2015-9-9 09:17:14
        /// </remarks>
        /// <param name="newHeaders">列头文本</param>
        /// <param name="rows">表头总行数</param>
        /// <returns></returns>
        private int GetRowSpan ( string newHeaders ,int rows )
        {
            int Count = newHeaders . Split ( new string [ ] { " " } ,StringSplitOptions . RemoveEmptyEntries ) . Length;
            // 如果总行数与当前表头所拥有行数相等
            if ( rows == Count )
                Count = 1;
            else if ( Count < rows )
                Count = 1 + ( rows - Count );
            else
                throw new Exception ( "表头格式不正确！" );
            return Count;
        }
        #endregion
    }

}
