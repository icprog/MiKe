using System . Data;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using Utility;
using DevExpress . XtraEditors;
using System . Windows . Forms;
using System;
using System . ComponentModel;
using System . Linq;

namespace LineProductMes
{
    public partial class FormWages :FormChild
    {
        LineProductMesBll.Bll.WagesBll _bll=null;
        LineProductMesEntityu.WagesHeaderEntity model=null;
        DataTable tableView;
        bool result=false;
        string focuseName=string.Empty,state=string.Empty;

        public FormWages ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . WagesBll ( );
            model = new LineProductMesEntityu . WagesHeaderEntity ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolPrint, toolCancellation  } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            controlClear ( );
            controlUnEnable ( );

            wait . Hide ( );
            layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormWagesQuery from = new ChildForm . FormWagesQuery ( );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                model . WAG001 = from . getCode;

                model = _bll . getModel ( model . WAG001 );
                txtCode . Text = model . WAG001;
                dateEdit1 . Text = Convert . ToDateTime ( model . WAG002 ) . ToString ( "yyyy-MM-dd" );

                if ( model . WAG003 )
                {
                    layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicW ( pictureEdit1 ,"审 核" );
                    examineTool ( "审核" );
                }
                else
                {
                    layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicW ( pictureEdit1 ,"反" );
                    examineTool ( "反审核" );
                }


                tableView = _bll . getTableView ( model . WAG001 );
                gridControl1 . DataSource = tableView;

                dateEdit1 . Enabled = false;
                btnRead . Enabled = false;
                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            txtCode . Text = _bll . getCode ( );

            tableView = _bll . getTableView ( "" );
            gridControl1 . DataSource = tableView;
            controlEnable ( );
            addTool ( );

            state = "add";

            dateEdit1 . Text = string . Empty;
            layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            editTool ( );

            state = "edit";

            dateEdit1 . Enabled = false;

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            result = _bll . Delete ( txtCode . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                controlUnEnable ( );
                deleteTool ( );
                controlClear ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Save ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请读取数据" );
                return 0;
            }
            if ( getValue ( ) == false )
                return 0;

            result = _bll . Save ( tableView ,txtCode . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                ClassForMain . FormClosingState . formClost = true;
                saveTool ( );
                controlUnEnable ( );
            }
            else
            {
                ClassForMain . FormClosingState . formClost = false;
                XtraMessageBox . Show ( "保存失败" );
            }

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            controlUnEnable ( );
            cancelTool ( "edit" );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( toolExamin . Caption . Equals ( "审核" ) )
                model . WAG003 = true;
            else
                model . WAG003 = false;
            string state = toolExamin . Caption;
            result = _bll . Examine ( txtCode . Text ,model . WAG003 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicW ( pictureEdit1 ,"审 核" );
                }
                else
                {
                    layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicW ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Export ( )
        {
            ViewExport . ExportToExcel ( this . Text + txtCode . Text ,this . gridControl1 );

            return base . Export ( );
        }
        #endregion

        #region Event
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void btnRead_Click ( object sender ,System . EventArgs e )
        {
            if ( string . IsNullOrEmpty ( dateEdit1 . Text ) )
            {
                XtraMessageBox . Show ( "请选择日期" );
                return;
            }

            if (state.Equals("add") && _bll . ExistsYearMonth ( Convert . ToDateTime ( dateEdit1 . Text ) ) )
            {
                XtraMessageBox . Show ( "已经存在本年月的工资,请查询" );
                return;
            }

            wait . Show ( );
            backgroundWorker1 . RunWorkerAsync ( );
        }
        private void backgroundWorker1_DoWork ( object sender ,System . ComponentModel . DoWorkEventArgs e )
        {
            result = _bll . Read ( Convert . ToDateTime ( dateEdit1 . Text ) ,txtCode . Text );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,System . ComponentModel . RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功读取" );
                    tableView = _bll . getTableView ( txtCode . Text );
                    gridControl1 . DataSource = tableView;
                    gridView1 . BestFitColumns ( );
                }
                else
                    XtraMessageBox . Show ( "读取失败" );
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtCode . Text == string . Empty || tableView == null || tableView . Rows . Count < 1 )
                    return;
                if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . YesNo  ) == DialogResult . Yes  )
                {
                    Save ( );
                    if (  ClassForMain.FormClosingState.formClost == false )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        //本月提留
        private void btnThisMonth_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( dateEdit1 . Text ) )
            {
                XtraMessageBox . Show ( "请选择日期" );
                return;
            }
            ChildForm . FormWagesThisMonthSalary from = new ChildForm . FormWagesThisMonthSalary ( Convert . ToDateTime ( dateEdit1 . Text ) );
            from . Show ( );
        }
        private void dateEdit1_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( dateEdit1 . Text ) )
                return;

            if (toolSave.Visibility== DevExpress.XtraBars.BarItemVisibility.Always && _bll . getCode ( Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMM" ) ) )
            {
                //XtraMessageBox . Show ( Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMM" ) + "的数据已经存在,请查询" );
                btnRead . Enabled = false;
                return;
            }
            else
                btnRead . Enabled = true;
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            //tiliu
            if ( e . ClickedItem . Name == "copy" )
                CopyUtils . copyResult ( gridView1 ,focuseName );
            else if ( e . ClickedItem . Name == "tiliu" )
            {
                DataRow row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                decimal ztl = string . IsNullOrEmpty ( row [ "WAH024" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "WAH024" ] );
                if ( ztl == 0 )
                    return;
                DataRow [ ] rows = tableView . Select ( "WAH023='" + row [ "WAH023" ] + "' AND WAH015<=0" );
                if ( rows == null || rows . Length < 1 )
                    return;
                DataRow [ ] rowes = tableView . Select ( "WAH023='" + row [ "WAH023" ] + "' AND WAH015>0" );
                decimal otherNum = 0M;
                if ( rowes != null && rowes . Length > 0 )
                {
                    foreach ( DataRow ro in rowes )
                    {
                        otherNum += Convert . ToDecimal ( ro [ "WAH015" ] );
                    }
                }
                int num = rows . Length;
                decimal everyNum = ( ztl - otherNum ) / ( num * Convert . ToDecimal ( 1.0 ) );
                foreach ( DataRow r in rows )
                {
                    r [ "WAH015" ] = everyNum;
                }
            }
        }
        #endregion

        #region Method
        void controlClear ( )
        {
            gridControl1 . DataSource = null;
        }
        void controlUnEnable ( )
        {
            dateEdit1 . Enabled = btnRead . Enabled = btnThisMonth . Enabled = false;
            gridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            dateEdit1 . Enabled = btnRead . Enabled = btnThisMonth . Enabled = true;
            gridView1 . OptionsBehavior . Editable = true;
        }
        bool getValue ( )
        {
            result = true;
            if ( tableView == null || tableView . Rows . Count < 1 )
                return false;
            //var query = from p in tableView . AsEnumerable ( )
            //            group p by new
            //            {
            //                p1 = p . Field<string> ( "WAH023" ) //,
            //                //p2 = p . Field<decimal> ( "WAH024" )
            //            } into m
            //            let sum = m . Sum ( t => t . Field<decimal?> ( "WAH015" ) == null ? 0 : t . Field<decimal> ( "WAH015" ) )
            //            select new
            //            {
            //                wah023 = m . Key . p1 ,
            //                //wah024 = m . Key . p2 ,
            //                sum = sum
            //            };

            //if ( query != null )
            //{
            //    decimal wah024 = 0M;
            //    foreach ( var x in query )
            //    {
            //        wah024 = string . IsNullOrEmpty ( tableView . Select ( "WAH023='" + x . wah023 + "'" ) [ 0 ] [ "WAH024" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( tableView . Select ( "WAH023='" + x . wah023 + "'" ) [ 0 ] [ "WAH024" ] );
            //        if ( wah024 < x . sum )
            //        {
            //            XtraMessageBox . Show ( x . wah023 + "提留工资分配总计大于提留工资,请核实" );
            //            result = false;
            //            break;
            //        }
            //    }
            //}

            string bz = string . Empty;
            decimal gr = 0M, bzSa = 0M;
            foreach ( DataRow row in tableView . Rows )
            {
                bz = row [ "WAH023" ] . ToString ( );
                gr = string . IsNullOrEmpty ( row [ "WAH024" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "WAH024" ] );
                bzSa = Convert . ToDecimal ( tableView . Compute ( "sum(WAH015)" ,"WAH023='" + bz + "'" ) );
                if ( gr < bzSa )
                {
                    XtraMessageBox . Show ( bz + "提留工资分配总计大于提留工资,请核实" );
                    result = false;
                    break;
                }
            }

            return result;
        }
        #endregion


    }
}