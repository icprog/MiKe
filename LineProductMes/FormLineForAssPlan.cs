using System . Data;
using System . Windows . Forms;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using System;
using DevExpress . XtraEditors;
using System . Linq;
using System . ComponentModel;
using System . Collections . Generic;
using LineProductMes . ChildForm;

namespace LineProductMes
{
    public partial class FormLineForAssPlan :FormChild
    {
        LineProductMesBll.Bll.LineForAssPlanBll _bll=null;
        LineProductMesEntityu.LineForAssPlanEntity model=null;
        DataTable tableView,table,tableCheck,tableViewClone;

        DateTime dtStart,dtEnd;
        string focusedName=string.Empty;

        Dictionary<string ,string> strDic = new Dictionary<string ,string> ( );

        public FormLineForAssPlan ( Dictionary<string ,string> strDic )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . LineForAssPlanBll ( );
            model = new LineProductMesEntityu . LineForAssPlanEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolExport ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit ,toolAdd  } );

            this . strDic = strDic;
        }
        
        private void FormLineForAssPlan_Load ( object sender ,EventArgs e )
        {
            dtStart = LineProductMesBll . UserInfoMation . sysTime;
            dtEnd = dtStart . AddDays ( 8 );
            dtSt . Text = dtStart . AddDays ( 1 ) . ToString ( "yyyy-MM-dd" );
            dtEn . Text = dtEnd . ToString ( "yyyy-MM-dd" );
            InitData ( );
            toolSave . Visibility = toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            tableCheck = new DataTable ( );
            tableCheck . Columns . Add ( "F1" ,typeof ( System . String ) );
            tableCheck . Columns . Add ( "F2" ,typeof ( System . String ) );
            tableCheck . Columns . Add ( "F3" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F4" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F5" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F6" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F7" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F8" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F9" ,typeof ( System . Boolean ) );
            tableCheck . Columns . Add ( "F10" ,typeof ( System . Boolean ) );
        }
        
        protected override int Query ( )
        {
            if ( !string . IsNullOrEmpty ( dtSt . Text ) )
                dtStart = Convert . ToDateTime ( dtSt . Text );
            if ( !string . IsNullOrEmpty ( dtEn . Text ) )
                dtEnd = Convert . ToDateTime ( dtEn . Text );
            //productName = string . Empty;

            InitData ( );

            return base . Query ( );
        }
        protected override int Save ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( checkTable ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            bool result = _bll . Save ( tableView );
            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                 ClassForMain.FormClosingState.formClost = true;
                this . DialogResult = DialogResult . OK;
            }
            else
            {
                 ClassForMain.FormClosingState.formClost = false;
                XtraMessageBox . Show ( "保存失败,请重试" );
            }

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            this . DialogResult = DialogResult . Cancel;
            return base . Cancel ( );
        }

        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        void InitData ( )
        {
            tableView = _bll . getTableView ( dtStart ,dtEnd ,strDic );
            if ( tableView != null && tableView . Rows . Count > 0 )
            {
                foreach ( DataColumn column in tableView . Columns )
                {
                    if ( column . ColumnName . Contains ( "[" ) || column . ColumnName . Contains ( "]" ) )
                    {
                        column . ColumnName = column . ColumnName . Replace ( '[' ,' ' );
                        column . ColumnName = column . ColumnName . Replace ( ']' ,' ' );
                    }
                }
            }
            gridControl1 . DataSource = tableView;
            gridView1 . PopulateColumns ( );

            tableViewClone = tableView . Copy ( );
            
            column ( );
        }
        void column ( )
        {
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            gridView1 . GroupCount = 1;
            gridView1 . OptionsBehavior . AutoExpandAllGroups = true;
            DateTime dt = Convert . ToDateTime ( LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" ) );
          
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                column . AppearanceCell . Options . UseTextOptions = true;
                column . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                column . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                column . BestFit ( );
                column . Summary . Clear ( );
                column . OptionsColumn . AllowEdit = false;
                column . OptionsFilter . FilterPopupMode = DevExpress . XtraGrid . Columns . FilterPopupMode . CheckedList;

                if ( column . FieldName != "产线编号" && column . FieldName != "产线" && column . FieldName != "主件品号" && column . FieldName != "主件品名" && column . FieldName != "排产量" && column . FieldName != "总排量" )
                {
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Sum ,column . FieldName );
                    column . OptionsColumn . AllowEdit = true;
                    column . Width = 45;
                    if ( ( Convert . ToDateTime ( column . FieldName ) - dt ) . Days <= 0 )
                        column . OptionsColumn . AllowEdit = false;

                    gridView1 . GroupSummary . Add (
           new DevExpress . XtraGrid . GridGroupSummaryItem ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,column ,"{0:N0}" ) );
                }
                else if ( column . FieldName == "产线编号" )
                    column . Summary . Add ( DevExpress . Data . SummaryItemType . Custom ,column . FieldName ,"合计" );
                else if ( column . FieldName == "主件品号" )
                    column . GroupIndex = 0;
            }
        }

        private void gridView1_CustomDrawRowFooterCell ( object sender ,DevExpress . XtraGrid . Views . Grid . FooterCellCustomDrawEventArgs e )
        {
            table = _bll . getTableGroupSum ( );
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                if ( e . Column == column && ( column . FieldName != "产线编号" || column . FieldName != "产线" || column . FieldName != "主件品号" || column . FieldName != "主件品名" || column . FieldName != "排产量" || column . FieldName != "总排量" ) )
                {
                    model . PRG001 = gridView1 . GetDataRow ( e . RowHandle ) [ "主件品号" ] . ToString ( );
                    if ( table . Select ( "PRG001='" + model . PRG001 + "' AND PRG002='" + column . FieldName + "'" ) . Length < 1 )
                        return;
                    model . PRG001 = table . Select ( "PRG001='" + model . PRG001 + "' AND PRG002='" + column . FieldName + "'" ) [ 0 ] [ "PRG006" ] . ToString ( );
                    e . Info . DisplayText = model . PRG001;
                }
            }
        }
        
        /// <summary>
        /// 选择产线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckLine_Click ( object sender ,EventArgs e )
        {
            if ( tableCheck . Rows . Count < 1 )
            {
                foreach ( string str in strDic . Keys )
                {
                    DataRow row = tableCheck . NewRow ( );
                    row [ "F1" ] = str;
                    row [ "F2" ] = strDic [ str ];
                    row [ "F3" ] = false;
                    row [ "F4" ] = false;
                    row [ "F5" ] = false;
                    row [ "F6" ] = false;
                    row [ "F7" ] = false;
                    row [ "F8" ] = false;
                    row [ "F9" ] = false;
                    row [ "F10" ] = false;
                    tableCheck . Rows . Add ( row );
                }
            }
            if ( tableCheck == null || tableCheck . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请查询需要排产的产品信息" );
                return;
            }
            FormCheckLine from = new FormCheckLine ( tableCheck );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                tableCheck = from . getTable;
                if ( tableCheck == null )
                    return;
                string productNum = string . Empty, line = string . Empty;
                foreach ( DataRow row in tableCheck . Rows )
                {
                    productNum = row [ "F1" ] . ToString ( );
                    DataRow ro, roNew;
                    DataRow [ ] rows;

                    for ( int i = 3 ; i < 11 ; i++ )
                    {
                        line = row [ "F" + i . ToString ( ) ] . ToString ( );
                        if ( Convert . ToBoolean ( line ) == false )
                        {
                            line = ( i - 2 ) . ToString ( ) + "线";
                            rows = tableView . Select ( "主件品号='" + productNum + "' AND 产线='" + line + "'" );
                            if ( rows . Length > 0 )
                            {
                                ro = rows [ 0 ];
                                tableView . Rows . Remove ( ro );
                            }
                        }
                        else 
                        {
                            line = ( i - 2 ) . ToString ( ) + "线";
                            rows = tableView . Select ( "主件品号='" + productNum + "' AND 产线='" + line + "'" );
                            if ( rows . Length < 1 )
                            {
                                rows = tableViewClone . Select ( "主件品号='" + productNum + "' AND 产线='" + line + "'" );
                                if ( rows . Length > 0 )
                                {
                                    ro = rows [ 0 ];
                                    roNew = tableView . NewRow ( );
                                    roNew . ItemArray = ro . ItemArray;
                                    tableView . Rows . Add ( roNew );
                                }
                            }
                        }
                    }
                    
                }

                gridControl1 . DataSource = tableView;
            }
        }

        bool checkTable ( )
        {
            bool result = true;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            foreach ( DevExpress . XtraGrid . Columns . GridColumn column in gridView1 . Columns )
            {
                if ( column . FieldName != "产线编号" && column . FieldName != "产线" && column . FieldName != "主件品号" && column . FieldName != "主件品名" && column . FieldName != "排产量" && column . FieldName != "总排量" )
                {
                    if ( ( Convert . ToDateTime ( column . FieldName ) - dt ) . Days >= 0 )
                    {
                        var query = from p in tableView . AsEnumerable ( )
                                    group p by new
                                    {
                                        p1 = p . Field<string> ( "主件品号" )
                                    } into m
                                    let sum = m . Sum ( t => string . IsNullOrEmpty ( t . Field<int?> ( column . FieldName ) . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( t . Field<int?> ( column . FieldName ) ) )
                                    select new
                                    {
                                        zj = m . Key . p1 ,
                                        sum = sum
                                    };
                        if ( query != null )
                        {
                            foreach ( var x in query )
                            {
                                if ( table != null && table . Rows . Count > 0 && table . Select ( "PRG001='" + x . zj + "' AND PRG002='" + column . FieldName + "'" ) . Length > 0 )
                                {
                                    model . PRG003 = Convert . ToInt32 ( table . Select ( "PRG001='" + x . zj + "' AND PRG002='" + column . FieldName + "'" ) [ 0 ] [ "PRG006" ] );
                                    if ( model . PRG003 < x . sum )
                                    {
                                        XtraMessageBox . Show ( "主件品号:" + x . zj + "\n\r排产日期:" + column . FieldName + "\n\r排产数量为:" + model . PRG003 + "\n\r您的排量为:" + x . sum + "\n\r请核实" );
                                        result = false;
                                        break;
                                    }

                                }
                            }
                            if ( result == false )
                                break;
                        }
                    }
                }
            }

            return result;
        }

        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focusedName = e . Column . FieldName;
        }

        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focusedName );
        }

        //protected override void OnClosing ( CancelEventArgs e )
        //{
        //    if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
        //    {
        //        if ( tableView == null || tableView . Rows . Count < 1 )
        //            return;
        //        if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
        //        {
        //            Save ( );
        //            if (  ClassForMain.FormClosingState.formClost == false )
        //                e . Cancel = true;
        //        }
        //    }

        //    base . OnClosing ( e );
        //}

    }
}