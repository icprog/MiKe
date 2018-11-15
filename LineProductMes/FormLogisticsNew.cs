using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Linq;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;
using Utility;

namespace LineProductMes
{
    public partial class FormLogisticsNew :FormChild
    {
        LineProductMesEntityu.LogisticsNewHeaderEntity _model=null;
        LineProductMesEntityu.LogisticsNewBodyOneEntity _bodyOne=null;
        LineProductMesEntityu.LogisticsNewBodyTwoEntity _bodyTwo=null;
        LineProductMesBll.Bll.LogisticsNewBll _bll=null;

        DataTable tableOne,tableTwo,tableViewOne,tableViewTwo,tableOtherSur;
        DateTime dt;

        List<string> listOne=new List<string>();
        List<string> listTwo=new List<string>();

        string state=string.Empty,focuseName=string.Empty;
        bool result=false;

        public FormLogisticsNew ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . LogisticsNewBll ( );
            _model = new LineProductMesEntityu . LogisticsNewHeaderEntity ( );
            _bodyOne = new LineProductMesEntityu . LogisticsNewBodyOneEntity ( );
            _bodyTwo = new LineProductMesEntityu . LogisticsNewBodyTwoEntity ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolExport ,toolPrint } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 ,View2 } );

            dt = LineProductMesBll . UserInfoMation . sysTime;

            Editd1 . VistaEditTime = Editd2 . VistaEditTime = Editd3 . VistaEditTime = Editd4 . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            InitData ( );
            controlUnEnable ( );
            controlClear ( );
            wait . Hide ( );
            layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormLogisticsQuery from = new ChildForm . FormLogisticsQuery ( );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                _model . LGN001 = from . getCode;

                _model = _bll . getModel ( _model . LGN001 );
                txtLGN001 . Text = _model . LGN001;
                txtLGN002 . Text = Convert . ToDateTime ( _model . LGN002 ) . ToString ( "yyyy-MM-dd" );
                txtLGN005 . Text = Convert . ToDecimal ( _model . LGN005 ) . ToString ( "0.#" );
                txtLGN006 . Text = Convert . ToDecimal ( _model . LGN006 ) . ToString ( "0.#" );
                txtLGN007 . Text = _model . LGN007;

                layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                Graph . grPicW ( pictureEdit1 ,"反" );
                if ( _model . LGN003 )
                {
                    layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicW ( pictureEdit1 ,"审 核" );
                    examineTool ( "审核" );
                }
                else
                    examineTool ( "反审核" );
                if ( _model . LGN004 )
                {
                    layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicW ( pictureEdit1 ,"注 销" );
                    cancelltionTool ( "注销" );
                }
                else
                    cancelltionTool ( "反注销" );

                tableViewOne = _bll . getTableViewOne ( _model . LGN001 );
                gridControl1 . DataSource = tableViewOne;
                tableViewTwo = _bll . getTableViewTwo ( _model . LGN001 );
                gridControl2 . DataSource = tableViewTwo;

                calcuTsumTime ( );

                editOtherSur ( string . Empty ,string . Empty );

                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );

            state = "add";

            _model . LGN001 = txtLGN001 . Text = _bll . getCode ( );
            txtLGN002 . Text = dt . ToString ( "yyyy-MM-dd" );

            tableViewOne = _bll . getTableViewOne ( _model . LGN001 );
            gridControl1 . DataSource = tableViewOne;
            tableViewTwo = _bll . getTableViewTwo ( _model . LGN001 );
            gridControl2 . DataSource = tableViewTwo;

            addUser ( );
            txtLGN007 . Text = "计件";

            addTool ( );

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );

            state = "edit";

            editTool ( );

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                return 0;
            if ( string . IsNullOrEmpty ( txtLGN001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择需要删除的单据" );
                return 0;
            }

            result = _bll . Delete ( txtLGN001 . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                controlUnEnable ( );
                controlClear ( );
                deleteTool ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getData ( ) == false )
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
            if ( state . Equals ( "add" ) )
                controlClear ( );
            controlUnEnable ( );
            cancelTool ( state );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( string . IsNullOrEmpty ( txtLGN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _model . LGN003 = true;
            else
                _model . LGN003 = false;
            result = _bll . Exanmie ( txtLGN001 . Text ,_model . LGN003 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicW ( pictureEdit1 ,"审 核" );
                }
                else
                {
                    layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicW ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtLGN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _model . LGN004 = true;
            else
                _model . LGN004 = false;
            result = _bll . CancelTion ( txtLGN001 . Text ,_model . LGN004 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicW ( pictureEdit1 ,"注 销" );
                }
                else
                {
                    layoutControlItem3 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicW ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        #endregion

        #region Evnet
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void bandedGridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _model ,tableViewOne ,tableViewTwo );
            else if ( state . Equals ( "edit" ) )
                result = _bll . Edit ( _model ,tableViewOne ,tableViewTwo ,listOne ,listTwo );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功保存" );
                     ClassForMain.FormClosingState.formClost = true;
                    controlUnEnable ( );
                    if ( state . Equals ( "add" ) )
                        _model . LGN001 = txtLGN001 . Text = LineProductMesBll . UserInfoMation . oddNum;
                    else
                        _model . LGN001 = txtLGN001 . Text;

                    tableViewOne = _bll . getTableViewOne ( _model . LGN001 );
                    gridControl1 . DataSource = tableViewOne;
                    tableViewTwo = _bll . getTableViewTwo ( _model . LGN001 );
                    gridControl2 . DataSource = tableViewTwo;

                    editOtherSur ( string . Empty ,string . Empty );

                    saveTool ( );
                }
                else
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败" );
                }
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "LOG008" )
            {
                setSalary ( );
            }
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            DataRow row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "LGP002" || e . Column . FieldName == "LGP003" || e . Column . FieldName == "LGP004" || e . Column . FieldName == "LGP006" || e . Column . FieldName == "LGP014" )
            {
                if ( txtLGN007 . Text . Equals ( "计件" ) )
                {
                    if ( row [ "LGP009" ] == null || row [ "LGP009" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LGP009" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "LGP010" ] == null || row [ "LGP010" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LGP010" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                else if ( txtLGN007 . Text . Equals ( "计时" ) )
                {
                    if ( row [ "LGP007" ] == null || row [ "LGP007" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LGP007" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "LGP008" ] == null || row [ "LGP008" ] . ToString ( ) == string . Empty )
                    {
                        row [ "LGP008" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                if ( row [ "LGP005" ] == null || row [ "LGP005" ] . ToString ( ) == string . Empty )
                {
                    row [ "LGP005" ] = "在职";
                }
                calcuTsumTime ( );
            }
            else if ( e . Column . FieldName == "LGP005" )
            {
                _bodyTwo . LGP005 = row [ "LGP005" ] . ToString ( );
                if ( _bodyTwo . LGP005 == string . Empty || _bodyTwo . LGP005 . Equals ( "离职" ) || _bodyTwo . LGP005 . Equals ( "未上班" ) )
                {
                    row [ "LGP007" ] = DBNull . Value;
                    row [ "LGP008" ] = DBNull . Value;
                    row [ "LGP009" ] = DBNull . Value;
                    row [ "LGP010" ] = DBNull . Value;
                    row [ "LGP011" ] = DBNull . Value;
                    row [ "LGP012" ] = DBNull . Value;
                    row [ "LGP013" ] = DBNull . Value;
                }
            }
            else if ( e . Column . FieldName == "LGP007" )
            {
                if ( workShopTime . startCenTime ( row ,e . Value ,"LGP010" ,"LGP008" ,"LGP009" ) == false )
                {
                    row [ "LGP007" ] = DBNull . Value;
                }
                addRow ( "LGP007" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "LGP008" )
            {
                if ( workShopTime . endCenTime ( row ,e . Value ,"LGP007" ,"LGP009" ,"LGP010" ) == false )
                {
                    row [ "LGP008" ] = DBNull . Value;
                }
                addRow ( "LGP008" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "LGP009" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"LGP010" ,"LGP007" ,"LGP008" ) == false )
                {
                    row [ "LGP009" ] = DBNull . Value;
                }
                addRow ( "LGP009" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "LGP010" )
            {
                if ( workShopTime . endTime ( row ,e . Value ,"LGP009" ,"LGP007" ,"LGP008" ) == false )
                {
                    row [ "LGP010" ] = DBNull . Value;
                }
                addRow ( "LGP010" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "LGP014" )
            {
                setSalary ( );
            }
        }
        private void Edit2_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "LGP002":
                if ( edit . EditValue == null )
                    return;
                DataRow row = tableOne . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    if ( tableViewTwo . Select ( "LGP002='" + edit . EditValue + "'" ) . Length > 0 )
                    {
                        edit . EditValue = null;
                        return;
                    }
                }
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "LGP003" ] ,row [ "EMP002" ] . ToString ( ) );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "LGP004" ] ,row [ "EMP007" ] . ToString ( ) );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "LGP005" ] ,"在职" );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "LGP006" ] ,row [ "DAA002" ] . ToString ( ) );
                break;
            }
        }
        private void btnEdit_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow rows = gridView1 . GetFocusedDataRow ( );
            
            tableTwo = _bll . getTableKEB ( );
            ChildForm . FormLogisticsNewQuery from = new ChildForm . FormLogisticsNewQuery ( tableTwo );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow row = from . getRow;
                if ( rows == null )
                {
                    rows = tableViewOne . NewRow ( );
                    rows [ "LOG002" ] = row [ "KEB001" ];
                    rows [ "LOG003" ] = row [ "KEB002" ];
                    rows [ "LOG004" ] = row [ "KEB003" ];
                    rows [ "LOG005" ] = row [ "KEB004" ];
                    rows [ "LOG006" ] = row [ "KEB007" ];
                    rows [ "LOG007" ] = row [ "KEB010" ];
                    rows [ "LOG009" ] = row [ "ARS011" ];
                    tableViewOne . Rows . Add ( rows );
                }
                else
                {
                    rows [ "LOG002" ] = row [ "KEB001" ];
                    rows [ "LOG003" ] = row [ "KEB002" ];
                    rows [ "LOG004" ] = row [ "KEB003" ];
                    rows [ "LOG005" ] = row [ "KEB004" ];
                    rows [ "LOG006" ] = row [ "KEB007" ];
                    rows [ "LOG007" ] = row [ "KEB010" ];
                    rows [ "LOG009" ] = row [ "ARS011" ];
                }
                gridControl2 . RefreshDataSource ( );
                //rows [ "LOG009" ] = row [ "KEB001" ];

                editOtherSur ( row [ "KEB001" ] . ToString ( ) ,row [ "KEB003" ] . ToString ( ) );
            }
        }
        private void gridControl1_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( e . KeyChar == ( char ) Keys . Enter )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                DataRow row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyOne . idx > 0 && !listOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                    listOne . Add ( _bodyOne . idx . ToString ( ) );
                tableViewOne . Rows . Remove ( row );
                gridControl1 . Refresh ( );
            }
        }
        private void gridControl2_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( e . KeyChar == ( char ) Keys . Enter )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                DataRow row = bandedGridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyTwo . idx > 0 && !listTwo . Contains ( _bodyTwo . idx . ToString ( ) ) )
                    listTwo . Add ( _bodyTwo . idx . ToString ( ) );
                tableViewTwo . Rows . Remove ( row );
                gridControl2 . Refresh ( );
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtLGN001 . Text == string . Empty || tableViewOne == null || tableViewOne . Rows . Count < 1 )
                    return;
                if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    Save ( );
                    if (  ClassForMain.FormClosingState.formClost == false )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        private void txtLGN005_SelectedValueChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtLGN006_SelectedValueChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtLGN007_SelectedValueChanged ( object sender ,EventArgs e )
        {
            addTime ( );
        }
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        private void contextMenuStrip2_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focuseName );
        }
        #endregion

        #region Method
        void controlClear ( )
        {
            txtLGN001 . Text = txtLGN002 . Text = txtLGN005 . Text = txtLGN006 . Text = txtLGN007 . Text = string . Empty;
            gridControl1 . DataSource = null;
            gridControl2 . DataSource = null;
        }
        void controlUnEnable ( )
        {
            txtLGN005 . ReadOnly = txtLGN006 . ReadOnly =txtLGN007.ReadOnly= true;
            gridView1 . OptionsBehavior.Editable = false;
            bandedGridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtLGN005 . ReadOnly = txtLGN006 . ReadOnly = txtLGN007 . ReadOnly = false;
            gridView1 . OptionsBehavior . Editable = true;
            bandedGridView1 . OptionsBehavior . Editable = true;
        }
        void InitData ( )
        {
            tableOne = _bll . getTableEMP ( );
            Edit2 . DataSource = tableOne;
            Edit2 . DisplayMember = "EMP001";
            Edit2 . ValueMember = "EMP001";

            
            //Edit1 . DataSource = tableTwo;
            //Edit1 . DisplayMember = "KEB001";
            //Edit1 . ValueMember = "KEB001";
        }
        bool getData ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtLGN001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return false;
            }

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                return false;

            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return false;

            var query = from p in tableViewOne . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "LOG002" ) ,
                            p2 = p . Field<string> ( "LOG003" )
                        } into m
                        select new
                        {
                            log002 = m . Key . p1 ,
                            log003 = m . Key . p2 ,
                            count = m . Count ( )
                        };

            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "销货单号:" + x . log002 + "\n\r序号:" + x . log003 + "\n\r重复,请核实" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            foreach ( DataRow row in tableViewOne . Rows )
            {
                _bodyOne . LOG006 = string . IsNullOrEmpty ( row [ "U4" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "U4" ] . ToString ( ) );
                _bodyOne . LOG008 = string . IsNullOrEmpty ( row [ "LOG008" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "LOG008" ] . ToString ( ) );
                if ( _bodyOne . LOG008 > _bodyOne . LOG006 )
                {
                    XtraMessageBox . Show ( "销货单号:" + row [ "LOG002" ] + "\n\r序号:" + row [ "LOG003" ] + "\n\r完工数量多于未完工数量" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            //var que = from p in tableViewOne . AsEnumerable ( )
            //          group p by new
            //          {
            //              p1 = p . Field<string> ( "LOG002" ) ,
            //              p2 = p . Field<string> ( "LOG003" ) ,
            //              p3 = p . Field<int?> ( "LOG006" ) == null ? 0 : p . Field<int> ( "LOG006" ) ,
            //              p4 = p . Field<int?> ( "LOG008" ) == null ? 0 : p . Field<int> ( "LOG008" )
            //          } into m
            //          select new
            //          {
            //              log002 = m . Key . p1 ,
            //              log003 = m . Key . p2 ,
            //              log006 = m . Key . p3 ,
            //              log008 = m . Key . p4
            //          };

            //if ( que != null )
            //{
            //    _bodyOne . LOG001 = txtLGN001 . Text;
            //    foreach ( var x in que )
            //    {
            //        _bodyOne . LOG006 = x . log006;
            //        _bodyOne . LOG008 = x . log008;
            //        _bodyOne . LOG002 = x . log002;
            //        _bodyOne . LOG003 = x . log003;
            //        if ( _bll . Exists ( _bodyOne ) )
            //        {
            //            XtraMessageBox . Show ( "销货单号: " + _bodyOne . LOG002 + "\n\r序号: " + _bodyOne . LOG003 + "\n\r完工数量多于销货单数量" );
            //            result = false;
            //            break;
            //        }
            //    }
            //}

            //if ( result == false )
            //    return false;

            var qu = from p in tableViewTwo . AsEnumerable ( )
                     group p by new
                     {
                         p1 = p . Field<string> ( "LGP002" )
                     } into m
                     select new
                     {
                         lgp002 = m . Key . p1 ,
                         count = m . Count ( )
                     };

            if ( qu != null )
            {
                foreach ( var x in qu )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . lgp002 + "\n\r重复,请核实" );
                        result = false;
                        break;
                    }
                }
            }

            _model . LGN001 = txtLGN001 . Text;
            _model . LGN002 = Convert . ToDateTime ( txtLGN002 . Text );
            _model . LGN005 = string . IsNullOrEmpty ( txtLGN005 . Text ) == true ? 0 : Convert . ToDecimal ( txtLGN005 . Text );
            _model . LGN006 = string . IsNullOrEmpty ( txtLGN006 . Text ) == true ? 0 : Convert . ToDecimal ( txtLGN006 . Text );
            _model . LGN007 = txtLGN007 . Text;

            return result;
        }
        void calcuTsumTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;
            //要不要加午休和晚休

            decimal lgn005 = txtLGN005 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtLGN005 . Text ) * 60;
            decimal lgn006 = txtLGN006 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtLGN006 . Text ) * 60;

            DateTime dtOne, dtTwo;
            decimal u0 = 0M;

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "LGP005" ] . ToString ( ) ) || row [ "LGP005" ] . ToString ( ) . Equals ( "离职" ) || row [ "LGP005" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "LGP011" ] = 0;
                    row [ "LGP012" ] = 0;
                    row [ "LGP013" ] = 0;
                    continue;
                }

                if ( !string . IsNullOrEmpty ( row [ "LGP007" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "LGP008" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "LGP007" ] );
                    dtTwo = Convert . ToDateTime ( row [ "LGP008" ] );

                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - lgn005 ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - lgn005 - lgn006 ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - lgn006 ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "LGP012" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "LGP012" ] = 0;

                if ( !string . IsNullOrEmpty ( row [ "LGP009" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "LGP010" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "LGP009" ] );
                    dtTwo = Convert . ToDateTime ( row [ "LGP010" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;
                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - lgn005 ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - lgn005 - lgn006 ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - lgn006 ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "LGP011" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "LGP011" ] = 0;
            }
            setSalary ( );
        }
        void setSalary ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            decimal numFor = 0M, totalTime = 0, userTime = 0;
            if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
            {
                foreach ( DataRow row in tableViewOne . Rows )
                {
                    numFor += ( string . IsNullOrEmpty ( row [ "LOG007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG007" ] . ToString ( ) ) ) * ( string . IsNullOrEmpty ( row [ "LOG008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG008" ] . ToString ( ) ) ) * ( string . IsNullOrEmpty ( row [ "LOG009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LOG009" ] . ToString ( ) ) );
                }
            }
            numFor += ( U1 . SummaryItem . SummaryValue == null ? 0 : Convert . ToDecimal ( U1 . SummaryItem . SummaryValue ) );
            totalTime = ( U0 . SummaryItem . SummaryValue == null ? 0 : Convert . ToDecimal ( U0 . SummaryItem . SummaryValue ) );
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                userTime = ( string . IsNullOrEmpty ( row [ "LGP011" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LGP011" ] ) ) + ( string . IsNullOrEmpty ( row [ "LGP012" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "LGP012" ] ) );
                row [ "LGP013" ] = totalTime == 0 ? 0 . ToString ( ) : ( numFor / totalTime * userTime ) . ToString ( "0.#" );
            }
        }

        void addUser ( )
        {
            if ( tableOne == null || tableOne . Rows . Count < 1 )
                return;
            DataRow [ ] rows = tableOne . Select ( "DAA002='物流组'" );
            if ( rows . Length < 1 )
                return;
            DataRow r;
            foreach ( DataRow row in rows )
            {
                r = tableViewTwo . NewRow ( );
                r [ "LGP002" ] = row [ "EMP001" ];
                r [ "LGP003" ] = row [ "EMP002" ];
                r [ "LGP004" ] = row [ "EMP007" ];
                r [ "LGP005" ] = "在职";
                r [ "LGP006" ] = row [ "DAA002" ];
                tableViewTwo . Rows . Add ( r );
            }
        }
        void addTime ( )
        {
            result = true;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;

            DateTime startTime = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 08:00" ) );
            DateTime endTime = Convert . ToDateTime ( dt . ToString ( "yyyy-MM-dd 17:00" ) );

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                if ( row == null )
                    continue;
                if ( txtLGN007 . Text . Equals ( "计件" ) )
                {
                    row [ "LGP007" ] = DBNull . Value;
                    row [ "LGP008" ] = DBNull . Value;
                    row [ "LGP012" ] = DBNull . Value;
                    if ( workShopTime . startTime ( row ,row [ "LGP009" ] ,"LGP010" ,"LGP007" ,"LGP008" ) == false )
                    {
                        row [ "LGP009" ] = DBNull . Value;
                        row [ "LGP011" ] = 0;
                        result = false;
                    }
                    if ( result )
                    {
                        if ( workShopTime . startTime ( row ,startTime ,"LGP010" ,"LGP007" ,"LGP008" ) == false )
                        {
                            row [ "LGP009" ] = DBNull . Value;
                            row [ "LGP011" ] = 0;
                            result = false;
                        }
                        else
                        {
                            row . BeginEdit ( );
                            row [ "LGP009" ] = startTime;
                            row . EndEdit ( );
                        }
                    }

                    result = true;
                    row [ "LGP007" ] = DBNull . Value;
                    row [ "LGP008" ] = DBNull . Value;
                    row [ "LGP012" ] = DBNull . Value;
                    if ( workShopTime . endTime ( row ,row [ "LGP010" ] ,"LGP009" ,"LGP007" ,"LGP008" ) == false )
                    {
                        row [ "LGP010" ] = DBNull . Value;
                        row [ "LGP011" ] = 0;
                        result = false;
                    }
                    if ( result )
                    {
                        if ( workShopTime . endTime ( row ,endTime ,"LGP009" ,"LGP007" ,"LGP008" ) == false )
                        {
                            row [ "LGP010" ] = DBNull . Value;
                            row [ "LGP011" ] = 0;
                            result = false;
                        }
                        else
                        {
                            row . BeginEdit ( );
                            row [ "LGP010" ] = endTime;
                            row . EndEdit ( );
                        }
                    }
                }
                else if ( txtLGN007 . Text . Equals ( "计时" ) )
                {
                    row [ "LGP009" ] = DBNull . Value;
                    row [ "LGP010" ] = DBNull . Value;
                    row [ "LGP011" ] = 0;
                    if ( workShopTime . startCenTime ( row ,row [ "LGP007" ] ,"LGP010" ,"LGP008" ,"LGP009" ) == false )
                    {
                        row [ "LGP007" ] = DBNull . Value;
                        row [ "LGP012" ] = 0;
                        result = false;
                    }
                    if ( result )
                    {
                        if ( workShopTime . startCenTime ( row ,startTime ,"LGP010" ,"LGP008" ,"LGP009" ) == false )
                        {
                            row [ "LGP007" ] = DBNull . Value;
                            row [ "LGP012" ] = 0;
                            result = false;
                        }
                        else
                        {
                            row . BeginEdit ( );
                            row [ "LGP007" ] = startTime;
                            row . EndEdit ( );
                        }
                    }

                    result = true;
                    if ( workShopTime . endCenTime ( row ,row [ "LGP008" ] ,"LGP007" ,"LGP009" ,"LGP010" ) == false )
                    {
                        row [ "LGP008" ] = DBNull . Value;
                        row [ "LGP012" ] = 0;
                        result = false;
                    }
                    if ( result )
                    {
                        if ( workShopTime . endCenTime ( row ,endTime ,"LGP007" ,"LGP009" ,"LGP010" ) == false )
                        {
                            row [ "LGP008" ] = DBNull . Value;
                            row [ "LGP012" ] = 0;
                            result = false;
                        }
                        else
                        {
                            row . BeginEdit ( );
                            row [ "LGP008" ] = endTime;
                            row . EndEdit ( );
                        }
                    }
                }
            }
            calcuTsumTime ( );
        }
        void addRow ( string column ,int selectIdx ,object value )
        {
            if ( selectIdx < 0 )
                return;
            if ( selectIdx < tableViewTwo . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableViewTwo . Rows . Count ; i++ )
                {
                    row = tableViewTwo . Rows [ i ];
                    if ( i + 1 != tableViewTwo . Rows . Count )
                    {
                        if ( txtLGN007 . Text . Equals ( "计件" ) )
                        {
                            if ( column . Equals ( "LGP009" ) )
                            {
                                if ( workShopTime . startTime ( row ,row [ "LGP009" ] ,"LGP010" ,"LGP007" ,"LGP008" ) )
                                {
                                    ro = tableViewTwo . Rows [ i + 1 ];
                                    if ( ro [ "LGP009" ] == null || ro [ "LGP009" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "LGP009" ] = /*row [ "LGP009" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "LGP010" ) )
                            {
                                if ( workShopTime . endTime ( row ,row [ "LGP010" ] ,"LGP009" ,"LGP007" ,"LGP008" ) )
                                {
                                    ro = tableViewTwo . Rows [ i + 1 ];
                                    if ( ro [ "LGP010" ] == null || ro [ "LGP010" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "LGP010" ] = /*row [ "LGP010" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ( column . Equals ( "LGP007" ) )
                            {
                                if ( workShopTime . startCenTime ( row ,row [ "LGP007" ] ,"LGP010" ,"LGP008" ,"LGP009 " ) )
                                {
                                    ro = tableViewTwo . Rows [ i + 1 ];
                                    if ( ro [ "LGP007" ] == null || ro [ "LGP007" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "LGP007" ] = /*row [ "LGP007" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "LGP008" ) )
                            {
                                if ( workShopTime . endCenTime ( row ,row [ "LGP008" ] ,"LGP009" ,"LGP007" ,"LGP008" ) )
                                {
                                    ro = tableViewTwo . Rows [ i + 1 ];
                                    if ( ro [ "LGP008" ] == null || ro [ "LGP008" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "LGP008" ] = /*row [ "LGP008" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            gridControl2 . RefreshDataSource ( );
            calcuTsumTime ( );
        }
        void editOtherSur ( string orderNum,string proNum )
        {
            _bodyOne . LOG001 = txtLGN001 . Text;
            _bodyOne . LOG002 = orderNum;
            _bodyOne . LOG004 = proNum;

            if ( _bodyOne . LOG002 == string . Empty || _bodyOne . LOG004 == string . Empty )
            {
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    foreach ( DataRow row in tableViewOne . Rows )
                    {
                        _bodyOne . LOG002 = row [ "LOG002" ] . ToString ( );
                        _bodyOne . LOG004 = row [ "LOG004" ] . ToString ( );
                        tableOtherSur = _bll . getTableOtherSur ( _bodyOne . LOG002 ,_bodyOne . LOG004 ,_bodyOne . LOG001 );
                        if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "LOG" ];
                        }
                        else
                        {
                            row [ "U4" ] = row [ "LOG006" ];
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne . LOG002 ,_bodyOne . LOG004 ,_bodyOne . LOG001 );
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewOne . Select ( "LOG002='" + _bodyOne . LOG002 + "' AND LOG004='" + _bodyOne . LOG004 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = tableOtherSur . Select ( "LOG002='" + row [ "LOG002" ] + "' AND LOG004='" + row [ "LOG004" ] + "'" ) [ 0 ] [ "LOG" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = row [ "LOG006" ];
                        }
                    }
                }
            }
        }
        #endregion

    }
}