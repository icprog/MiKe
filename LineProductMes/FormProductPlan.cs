using System;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using System . Data;
using DevExpress . XtraEditors;
using System . Linq;
using System . Collections . Generic;
using System . Windows . Forms;
using System . ComponentModel;

namespace LineProductMes
{
    public partial class FormProductPlan :FormChild
    {
        LineProductMesEntityu.ProductPlanHeaderEntity _header=null;
        LineProductMesEntityu.ProductPlanBodyEntity _body=null;
        LineProductMesBll.Bll.ProductPlanBll _bll=null;
        DataTable tableView;
        DataRow row,rows,rowes;
        int selectIdx=0;
        DateTime dtTime;
        List<string> idxList=new List<string>();
        
        string state=string.Empty,focuseName=string.Empty;
        bool result=false,check=false;
        
        public FormProductPlan ( DataRow row )
        {
            InitializeComponent ( );

            _header = new LineProductMesEntityu . ProductPlanHeaderEntity ( );
            _body = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            _bll = new LineProductMesBll . Bll . ProductPlanBll ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolCancellation, toolExport ,toolPrint ,toolExamin ,toolDelete } );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            //toolExamin . Caption = "弃用";
            //toolCancellation . Caption = "复制";
            controlUnEnable ( );
            controlClear ( );

            dtTime = LineProductMesBll . UserInfoMation . sysTime;

            if ( row != null )
            {
                this . rows = row;
            }
        }

        private void FormProductPlan_Load ( object sender ,EventArgs e )
        {
            if ( rows != null )
            {
                controlEnable ( );
                editTool ( );
                state = "edit";
                _body . PRE004 = rows [ "主件品号" ] . ToString ( );
                tableView = _bll . getTableViewFor ( _body . PRE004 );
                gridControl1 . DataSource = tableView;
            }
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormProductPlanQuery from = new ChildForm . FormProductPlanQuery ( );
            from . StartPosition = FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                _header . PRD001 = from . getOdd;
                _header = _bll . getModel ( _header . PRD001 );

                setValue ( );
                tableView = _bll . getTableView ( _header . PRD001 );
                gridControl1 . DataSource = tableView;

                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            txtPRD001 . Text = _bll . getOddNum ( );
            txtPRD002 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );
            state = "add";
            tableView = _bll . getTableView ( "1=2" );
            gridControl1 . DataSource = tableView;
            addTool ( );

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            editTool ( );
            state = "edit";

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtPRD001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            result = _bll . Delete ( txtPRD001 . Text ,tableView );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                controlClear ( );
                controlUnEnable ( );
                deleteTool ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getValue ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            wait . Show ( );
            backgroundWorker1 . RunWorkerAsync ( );

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            _bll . Cancel ( tableView );
            this . DialogResult = DialogResult . Cancel;

            //controlUnEnable ( );
            //cancelTool ( state );
            //if ( state . Equals ( "add" ) )
            //    controlClear ( );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( string . IsNullOrEmpty ( txtPRD001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = "";
            state = toolExamin . Caption;
            if ( toolExamin . Caption . Equals ( "弃用" ) )
                _header . PRD003 = false;
            else
                _header . PRD003 = true;
            result = _bll . Examine ( _header . PRD001 ,_header . PRD003 == true ? 1 : 0 );
            if ( result )
            {
                XtraMessageBox . Show ( "已" + state );
                if ( state . Equals ( "弃用" ) )
                {
                    layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicSMIN ( pictureEdit1 ,state );
                    toolExamin . Caption = "启用";
                    toolVisibleFalse ( );
                }
                else
                {
                    layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicSMIN ( pictureEdit1 ,"反" );
                    toolExamin . Caption = "弃用";
                    toolVisibleTrue ( );
                }
            }
            else
                XtraMessageBox . Show ( "已" + state );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( copy ( ) == false )
                return 0;

            result = _bll . Copy ( tableView ,_header . PRD001 );
            if ( result )
            {
                XtraMessageBox . Show ( "成功复制" );

                _header . PRD001 = LineProductMesBll . UserInfoMation . oddNum;
                _header = _bll . getModel ( _header . PRD001 );

                setValue ( );
                tableView = _bll . getTableView ( _header . PRD001 );
                gridControl1 . DataSource = tableView;
                QueryTool ( );
                addTool ( );
                state = "eidt";
                controlEnable ( );
            }
            else
                XtraMessageBox . Show ( "复制失败" );

            return base . Cancellation ( );
        }
        #endregion

        #region Event
        private void ReaderOrder_Click ( object sender ,EventArgs e )
        {
            ChildForm . FormReadOrder from = new ChildForm . FormReadOrder ( tableView );
            from . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( from . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                DataTable tableResult = from . getTable;
                if ( tableResult == null || tableResult . Rows . Count < 1 )
                    return;
                orderArray ( tableResult );
                gridControl1 . RefreshDataSource ( );
            }
        }
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . CellValue == null || e . CellValue . ToString ( ) == string . Empty )
                return;
            if ( e . Column . FieldName == "PRE010" )
            {
                _body . PRE010 = Convert . ToInt32 ( e . CellValue );
            }
            if ( e . Column . FieldName == "PRE008" )
            {
                _body . PRE008 = Convert . ToInt32 ( e . CellValue );
                if ( _body . PRE008 != _body . PRE010 )
                    e . Appearance . BackColor = System . Drawing . Color . Yellow;
            }
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;

            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                _body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _body . idx > 0 )
                {
                    XtraMessageBox . Show ( "已经写入主计划一览表,不允许删除" );
                    return;
                }
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;           
                if ( _body . idx > 0 && !idxList . Contains ( _body . idx . ToString ( ) ) )
                    idxList . Add ( _body . idx . ToString ( ) );

                gridView1 . CloseEditor ( );
                gridView1 . UpdateCurrentRow ( );
                object obj = tableView . Compute ( "MIN(PRE005)" ,"PRE002='" + row [ "PRE002" ] + "' AND PRE003='" + row [ "PRE003" ] + "' AND PRE004='" + row [ "PRE004" ] + "' AND PRE005>'" + row [ "PRE005" ] + "'" );
                if ( obj != null && obj . ToString ( ) != string . Empty )
                {
                    _body . PRE005 = Convert . ToDateTime ( obj );
                    DataRow rows = tableView . Select ( "PRE002='" + row [ "PRE002" ] + "' AND PRE003='" + row [ "PRE003" ] + "' AND PRE004='" + row [ "PRE004" ] + "' AND PRE005='" + _body . PRE005 + "'" ) [ 0 ];
                    obj = rows [ "PRE008" ];
                    if ( obj == null || obj . ToString ( ) == string . Empty )
                        _body . PRE008 = 0;
                    else
                        _body . PRE008 = Convert . ToInt32 ( obj );
                    _body . PRE008 += Convert . ToInt32 ( row [ "PRE008" ] );
                    rows [ "PRE008" ] = _body . PRE008;
                }

                tableView . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,System . ComponentModel . DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableView );
            else
                result = _bll . Edit ( _header ,tableView ,idxList );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,System . ComponentModel . RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功保存" );
                     ClassForMain.FormClosingState.formClost = true;
                    this . DialogResult = DialogResult . OK;
                    //saveTool ( );
                    //toolExamin . Caption = "弃用";
                    //toolCancellation . Caption = "复制";
                    //controlUnEnable ( );
                    //if ( state . Equals ( "add" ) )
                    //    _header . PRD001 = txtPRD001 . Text = LineProductMesBll . UserInfoMation . oddNum;

                    //tableView = _bll . getTableView ( _header . PRD001 );
                    //gridControl1 . DataSource = tableView;
                }
                else
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败" );
                }
            }
        }
        private void gridView1_CellValueChanging ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "PRE008" )
            {
                check = true;
                selectIdx = e . RowHandle;
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName != "PRE008" )
                return;
            if ( row == null )
                return;
            if ( _body . PRE010 == 0 )
                return;
            DataRow ro = gridView1 . GetDataRow ( selectIdx );
            if ( ro == null )
                return;
            _body . PRE009 = Convert . ToInt32 ( e . Value );

            if ( _body . PRE010 <= _body . PRE009 )
                return;

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            object obj = tableView . Compute ( "MIN(PRE005)" ,"PRE002='" + ro [ "PRE002" ] + "' AND PRE003='" + ro [ "PRE003" ] + "' AND PRE004='" + ro [ "PRE004" ] + "' AND PRE005>'" + ro [ "PRE005" ] + "'" );
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                _body . PRE005 = Convert . ToDateTime ( obj );
                DataRow rows = tableView . Select ( "PRE002='" + ro [ "PRE002" ] + "' AND PRE003='" + ro [ "PRE003" ] + "' AND PRE004='" + ro [ "PRE004" ] + "' AND PRE005='" + _body . PRE005 + "'" ) [ 0 ];
                obj = rows [ "PRE008" ];
                if ( obj == null && obj . ToString ( ) == string . Empty )
                    _body . PRE007 = 0;
                else
                    _body . PRE007 = Convert . ToInt32 ( obj );
                _body . PRE007 = _body . PRE010 - _body . PRE009 + _body . PRE007;
                rows [ "PRE008" ] = _body . PRE007;
            }
            check = false;
        }
        private void gridView1_MouseEnter ( object sender ,EventArgs e )
        {
            if ( check == false )
            {
                row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                if ( row [ "PRE008" ] == null || row [ "PRE008" ] . ToString ( ) == string . Empty )
                    return;
                _body . PRE010 = Convert . ToInt32 ( row [ "PRE008" ] );
            }
        }
        //生成ERP计划
        private void btnGener_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtPRD001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择计划" );
                return;
            }
            result = _bll . AddPlan ( txtPRD001 . Text );
            if ( result )
                XtraMessageBox . Show ( "成功写入计划" );
            else
                XtraMessageBox . Show ( "写入计划失败" );
        }
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            rowes = gridView1 . GetFocusedDataRow ( );
        }
        //复制一行
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            if ( e . ClickedItem . Name == "ItemOne" )
            {
                if ( rowes == null )
                {
                    XtraMessageBox . Show ( "请选择需要复制的行" );
                    return;
                }

                _body . PRE001 = rowes [ "PRE001" ] . ToString ( );
                _body . PRE002 = rowes [ "PRE002" ] . ToString ( );
                _body . PRE003 = rowes [ "PRE003" ] . ToString ( );
                _body . PRE004 = rowes [ "PRE004" ] . ToString ( );
                if ( rowes [ "PRE005" ] == null )
                    _body . PRE005 = null;
                else
                    _body . PRE005 = Convert . ToDateTime ( rowes [ "PRE005" ] . ToString ( ) );
                if ( rowes [ "PRE006" ] == null )
                    _body . PRE006 = null;
                else
                    _body . PRE006 = Convert . ToDateTime ( rowes [ "PRE006" ] . ToString ( ) );
                if ( rowes [ "PRE007" ] == null )
                    _body . PRE007 = null;
                else
                    _body . PRE007 = Convert . ToInt32 ( rowes [ "PRE007" ] . ToString ( ) );
                if ( rowes [ "PRE008" ] == null )
                    _body . PRE008 = null;
                else
                    _body . PRE008 = Convert . ToInt32 ( rowes [ "PRE008" ] . ToString ( ) );
                if ( rowes [ "PRE009" ] == null )
                    _body . PRE009 = null;
                else
                    _body . PRE009 = Convert . ToInt32 ( rowes [ "PRE009" ] . ToString ( ) );
                if ( rowes [ "PRE010" ] == null )
                    _body . PRE010 = null;
                else
                    _body . PRE010 = Convert . ToInt32 ( rowes [ "PRE010" ] . ToString ( ) );

                object obj = tableView . Compute ( "MAX(PRE005)" ,"PRE002='" + _body . PRE002 + "' AND PRE003='" + _body . PRE003 + "' AND PRE004='" + _body . PRE004 + "'" );
                if ( obj != null )
                {
                    _body . PRE005 = Convert . ToDateTime ( obj ) . AddDays ( 1 );
                    DataRow row = tableView . NewRow ( );
                    row [ "PRE001" ] = _body . PRE001;
                    row [ "PRE002" ] = _body . PRE002;
                    row [ "PRE003" ] = _body . PRE003;
                    row [ "PRE004" ] = _body . PRE004;
                    row [ "PRE005" ] = _body . PRE005;
                    row [ "PRE006" ] = _body . PRE006;
                    row [ "PRE007" ] = _body . PRE007;
                    row [ "PRE008" ] = _body . PRE008;
                    row [ "PRE009" ] = _body . PRE009;
                    row [ "PRE010" ] = _body . PRE010;
                    tableView . Rows . Add ( row );
                }
            }
            else if ( e . ClickedItem . Name == "ItemTwo" )
            {
                CopyUtils . copyResult ( gridView1 ,focuseName );
            }
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void gridView1_ShowingEditor ( object sender ,System . ComponentModel . CancelEventArgs e )
        {
            DataRow row = gridView1 . GetDataRow ( gridView1 . FocusedRowHandle );
            if ( row != null )
            {
                if ( row [ "idx" ] == null || row [ "idx" ] . ToString ( )==string.Empty )
                    return;
                if ( ( gridView1 . FocusedColumn . FieldName == "PRE008" || gridView1 . FocusedColumn . FieldName == "PRE005" ) && ( Convert . ToDateTime ( Convert . ToDateTime ( row [ "PRE005" ] ) . ToString ( "yyyy-MM-dd" ) ) - Convert . ToDateTime ( dtTime . ToString ( "yyyy-MM-dd" ) ) ) . Days <= 0 )
                {
                    e . Cancel = true;
                }
                if ( gridView1 . FocusedColumn . FieldName == "PRE011" && ( Convert . ToDateTime ( Convert . ToDateTime ( row [ "PRE005" ] ) . ToString ( "yyyy-MM-dd" ) ) - Convert . ToDateTime ( dtTime . ToString ( "yyyy-MM-dd" ) ) ) . Days <= 0 )
                {
                    e . Cancel = true;
                }
            }
        }
        //protected override void OnClosing ( CancelEventArgs e )
        //{
        //    if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
        //    {
        //        if (  tableView == null || tableView . Rows . Count < 1 )
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
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            ReaderOrder . Enabled = false;
            gridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            ReaderOrder . Enabled = true;
            gridView1 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtPRD001 . Text = txtPRD002 . Text = string . Empty;
            gridControl1 . DataSource = null;
            layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            wait . Hide ( );
        }
        void orderArray ( DataTable table )
        {
            int dayNum, arrayNum, dayResultNum, dtDay;
            DateTime dtStart;

            if (! string . IsNullOrEmpty ( txtPRD001 . Text ) )
                _header . PRD001 = txtPRD001 . Text;
            else
                _header . PRD001 = tableView . Rows [ 0 ] [ "PRE001" ] . ToString ( );

            foreach ( DataRow row in table . Rows )
            {
                dtDay = 0;
                dayNum = Convert . ToInt32 ( row [ "IBB009" ] );
                arrayNum = Convert . ToInt32 ( row [ "IBB010" ]  );
                dayResultNum = arrayNum % dayNum;
                dtStart = Convert . ToDateTime ( row [ "IBB007" ] );
                dtDay = arrayNum / dayNum;
                if ( dayResultNum > 0 )
                    dtDay += 1;
                for ( int i = 0 ; i < dtDay ; i++ )
                {
                    DataRow rows = tableView . NewRow ( );
                    rows [ "PRE001" ] = _header . PRD001;
                    rows [ "PRE002" ] = row [ "IBB001" ];
                    rows [ "PRE003" ] = row [ "IBB002" ];
                    rows [ "PRE004" ] = row [ "IBB003" ];
                    rows [ "PRE005" ] = dtStart . AddDays ( i );
                    rows [ "PRE006" ] = row [ "IBB013" ];
                    rows [ "PRE007" ] = row [ "IBB006" ];
                    if ( i < dtDay - 1 )
                        rows [ "PRE008" ] = dayNum;
                    else if ( dayResultNum > 0 )
                        rows [ "PRE008" ] = dayResultNum;
                    else
                        rows [ "PRE008" ] = dayNum;
                    rows [ "PRE010" ] = row [ "IBB009" ];
                    rows [ "PRE009" ] = 0;
                    tableView . Rows . Add ( rows );
                }
            }
        }
        bool getValue ( )
        {
            result = true;

            if ( state . Equals ( "add" ) )
            {
                _header . PRD001 = txtPRD001 . Text;
                _header . PRD002 = Convert . ToDateTime ( txtPRD002 . Text );
                _header . PRD003 = false;
            }
            else
                _header . PRD001 = tableView . Rows [ 0 ] [ "PRE001" ] . ToString ( );

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            gridView1 . ClearColumnErrors ( );

            foreach ( DataRow row in tableView . Rows )
            {
                if ( row [ "PRE005" ] == null || row [ "PRE005" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "请选择" );
                    result = false;
                    break;
                }
                if ( row [ "PRE008" ] == null || row [ "PRE008" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "请录入" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "PRE002" ) ,
                            p2 = p . Field<string> ( "PRE003" ) ,
                            p3 = p . Field<string> ( "PRE004" ) ,
                            P4 = p . Field<int> ( "PRE007" )
                        } into m
                        let sum = m . Sum ( t => t . Field<int> ( "PRE008" ) )
                        orderby sum descending
                        select new
                        {
                            pre002 = m . Key . p1 ,
                            pre003 = m . Key . p2 ,
                            pre004 = m . Key . p3 ,
                            pre007 = m . Key . P4 ,
                            sum = sum
                        };
            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . pre007 < x . sum )
                    {
                        XtraMessageBox . Show ( "订单:" + x . pre002 + "\n\r序号:" + x . pre003 + "\n\r品号:" + x . pre004 + "\n\r排产数量总和多于订单量,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            if ( _header . PRD001 != null )
            {
                DataTable tableAll = _bll . getTableAllOrder ( tableView ,_header . PRD001 );
                if ( tableAll == null || tableAll . Rows . Count < 1 )
                    return true;
                if ( query != null )
                {
                    foreach ( var x in query )
                    {
                        if ( tableAll . Select ( "PRE002='" + x . pre002 + "' AND PRE003='" + x . pre003 + "' AND PRE004='" + x . pre004 + "'" ) . Length > 0 )
                        {
                            _body . PRE008 = x . sum + Convert . ToInt32 ( tableAll . Select ( "PRE002='" + x . pre002 + "' AND PRE003='" + x . pre003 + "' AND PRE004='" + x . pre004 + "'" ) [ 0 ] [ "PRE008" ] );
                            if ( _body . PRE008 > x . pre007 )
                            {
                                XtraMessageBox . Show ( "订单:" + x . pre002 + "\n\r序号:" + x . pre003 + "\n\r品号:" + x . pre004 + "\n\r排产数量总和多于订单量,请核实" ,"提示" );
                                result = false;
                                break;
                            }
                        }
                    }
                }

                if ( result == false )
                    return false;
            }

            var que = from p in tableView . AsEnumerable ( )
                      group p by new
                      {
                          p1 = p . Field<string> ( "PRE002" ) ,
                          p2 = p . Field<string> ( "PRE003" ) ,
                          p3 = p . Field<string> ( "PRE004" ) ,
                          p4 = p . Field<DateTime> ( "PRE005" )
                      } into m
                      select new
                      {
                          pre002 = m . Key . p1 ,
                          pre003 = m . Key . p2 ,
                          pre004 = m . Key . p3 ,
                          pre005 = m . Key . p4 ,
                          count = m . Count ( )
                      };

            if ( que != null )
            {
                foreach ( var x in que )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "订单:" + x . pre002 + "\n\r序号:" + x . pre003 + "\n\r品号:" + x . pre004 + "\n\r日期:" + x . pre005 + "\n\r重复,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            DataTable tableTime = _bll . getTableAllTime ( tableView );
            if ( tableTime == null || tableTime . Rows . Count < 1 )
                return true;
            if ( que != null )
            {
                foreach ( var x in que )
                {
                    if ( tableTime . Select ( "PRE002='" + x . pre002 + "' AND PRE003='" + x . pre003 + "' AND PRE004='" + x . pre004 + "'AND PRE005='" + x . pre005 + "'" ) . Length > 0 )
                    {
                        if ( Convert . ToInt32 ( tableTime . Select ( "PRE002='" + x . pre002 + "' AND PRE003='" + x . pre003 + "' AND PRE004='" + x . pre004 + "'AND PRE005='" + x . pre005 + "'" ) [ 0 ] [ "COUN" ] ) > 1 )
                        {
                            XtraMessageBox . Show ( "订单:" + x . pre002 + "\n\r序号:" + x . pre003 + "\n\r品号:" + x . pre004 + "\n\r日期:" + x . pre005 + "\n\r已排,请核实" ,"提示" );
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }
        bool copy ( )
        {
            result = true;
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请选择需要复制的内容" );
                return false;
            }
            DataTable tableOther = _bll . getTableCopyOne ( _header . PRD001 );
            if ( tableOther == null || tableOther . Rows . Count < 1 )
                return true;
            foreach ( DataRow row in tableView . Rows )
            {
                _body . PRE002 = row [ "PRE002" ] . ToString ( );
                _body . PRE003 = row [ "PRE003" ] . ToString ( );
                _body . PRE004 = row [ "PRE004" ] . ToString ( );
                _body . PRE005 = Convert . ToDateTime ( row [ "PRE005" ] . ToString ( ) );
                _body . PRE006 = Convert . ToDateTime ( row [ "PRE006" ] . ToString ( ) );
                _body . PRE007 = Convert . ToInt32 ( row [ "PRE007" ] . ToString ( ) );
                _body . PRE008 = Convert . ToInt32 ( row [ "PRE008" ] . ToString ( ) );
                _body . PRE010 = Convert . ToInt32 ( row [ "PRE010" ] . ToString ( ) );

                if ( tableOther . Select ( "PRE002='" + _body . PRE002 + "' AND PRE003='" + _body . PRE003 + "' AND PRE004='" + _body . PRE004 + "' AND PRE005='" + _body . PRE005 + "'" ) . Length > 0 )
                {
                    XtraMessageBox . Show ( "订单号:" + _body . PRE002 + "\n\r序号:" + _body . PRE003 + "\n\r品号:" + _body . PRE004 + "\n\r日期:" + _body . PRE005 + "\n\r已排,请核实" );
                    result = false;
                    break;
                }
            }

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "PRE002" ) ,
                            p2 = p . Field<string> ( "PRE003" ) ,
                            p3 = p . Field<string> ( "PRE004" ) ,
                            p4 = p . Field<int> ( "PRE007" )
                        } 
                        into m
                        let sum = m . Sum ( t => t . Field<int> ( "PRE008" ) )
                        orderby sum ascending
                        select new
                        {
                            pre002 = m . Key . p1 ,
                            pre003 = m . Key . p2 ,
                            pre004 = m . Key . p3 ,
                            pre007 = m . Key . p4 ,
                            sum = sum
                        };

            var que = from p in tableOther . AsEnumerable ( )
                      group p by new
                      {
                          p1 = p . Field<string> ( "PRE002" ) ,
                          p2 = p . Field<string> ( "PRE003" ) ,
                          p3 = p . Field<string> ( "PRE004" )
                      } into m
                      let sum = m . Sum ( t => t . Field<int> ( "PRE008" ) )
                      orderby sum ascending
                      select new
                      {
                          pre002 = m . Key . p1 ,
                          pre003 = m . Key . p2 ,
                          pre004 = m . Key . p3 ,
                          sum = sum
                      };

            foreach ( var x in query )
            {
                foreach ( var y in que )
                {
                    if ( x . pre002 == y . pre002 && x . pre003 == y . pre003 && x . pre004 == y . pre004 && x . sum + y . sum > x . pre007 )
                    {
                        XtraMessageBox . Show ( "订单号:" + x . pre002 + "\n\r序号:" + x . pre003 + "\n\r品号:" + x . pre004 + "\n\r历史排产数量之和多于订单数量,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
                if ( result == false )
                    break;
            }

            return result;
        }
        void setValue ( )
        {
            txtPRD001 . Text = _header . PRD001;
            txtPRD002 . Text = Convert . ToDateTime ( _header . PRD002 ) . ToString ( "yyyy-MM-dd" );
            layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPicSMIN ( pictureEdit1 ,"反" );
            if ( _header . PRD003 )
            {
                layoutControlItem4 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicSMIN ( pictureEdit1 ,"弃用" );
                toolExamin . Caption = "启用";
                toolVisibleFalse ( );
            }
            else
            {
                toolExamin . Caption = "弃用";
                toolVisibleTrue ( );
            }
        }
        void toolVisibleTrue ( )
        {
            //启用
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }
        void toolVisibleFalse ( )
        {
            //弃用
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }
        #endregion

    }
}



/*
用商品信息表的最后一个自定义数字的字段做日产量的设置(DEA985)，用订单单身的最后一个自定义数字字段做已排数量的回写(IBB985) 
*/
