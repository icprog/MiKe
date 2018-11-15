using System . Data;
using Utility;
using System . Reflection;
using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using DevExpress . XtraEditors;
using System;
using System . Windows . Forms;
using System . Collections . Generic;
using System . Threading;
using System . Collections;
using System . ComponentModel;
using DevExpress . XtraGrid . Views . Grid;
using DevExpress . XtraGrid . Views . Grid . ViewInfo;
using DevExpress . Utils;

namespace LineProductMes
{
    public partial class FormArt :FormChild
    {
        LineProductMesEntityu.ArtEntity model=null;
        LineProductMesEntityu.ArsEntity body=null;
        LineProductMesEntityu.AruEntity _bodyOne=null;
        LineProductMesBll.Bll.ArtBll _bll=null;
        DataTable tableView,tableBody,tableBodyOne,tableEmp,tableArt,tableModel;
        bool result=false;
        string state=string.Empty,focuseName=string.Empty;
        List<string> idxList=new List<string>(); List<string> idxListOne=new List<string>();

        Thread thread;
        SynchronizationContext m_SyncContext = null;
        Hashtable table=new Hashtable();

        public FormArt ( )
        {
            InitializeComponent ( );

            model = new LineProductMesEntityu . ArtEntity ( );
            body = new LineProductMesEntityu . ArsEntity ( );
            _bll = new LineProductMesBll . Bll . ArtBll ( );
            _bodyOne = new LineProductMesEntityu . AruEntity ( );

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext . Current;

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,gridView2 ,gridView3 ,View1 ,View4 ,View2 ,View3 ,gridView4 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,gridView2 ,gridView3 ,View1 ,View4 ,View2 ,View3 ,gridView4 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolCancellation,toolExamin } );

            Query ( );
            controlUnEnable ( );
            controlClear ( );
            InitData ( );
        }

        #region Main
        protected override int Query ( )
        {
            tableView = _bll . getTableViewAll ( );
            gridControl2 . DataSource = tableView;

            if ( tableView != null && tableView . Rows . Count > 0 )
            {
                table = new Hashtable ( );
                model . ART001 = tableView . Rows [ 0 ] [ "ARS001" ] . ToString ( );

                LineProductMesEntityu . ArsEntity body = _bll . getModel ( model . ART001 );
                setValue ( body );
                tableBody = _bll . getTableViewMain ( model . ART001 );
                gridControl1 . DataSource = tableBody;
                if ( tableBody != null && tableBody . Rows . Count > 0 )
                {
                    _bodyOne . ARU001 = tableBody . Rows [ 0 ] [ "ART001" ] . ToString ( );
                    _bodyOne . ARU002 = tableBody . Rows [ 0 ] [ "ART011" ] . ToString ( );
                    tableBodyOne = _bll . getTableViewBody ( _bodyOne . ARU001 ,_bodyOne . ARU002 );
                }
                else
                    tableBodyOne = _bll . getTableViewBody ( string . Empty ,string . Empty );
                gridControl3 . DataSource = tableBodyOne;
                if ( tableBodyOne != null && tableBodyOne . Rows . Count > 0 )
                {
                    _bodyOne . ARU001 = tableBodyOne . Rows [ 0 ] [ "ARU001" ] . ToString ( );
                    _bodyOne . ARU002 = tableBodyOne . Rows [ 0 ] [ "ARU002" ] . ToString ( );
                    table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
                }
            }

            QueryTool ( );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Query ( );
        }
        protected override int Add ( )
        {
            addTool ( );
            state = "add";
            controlEnable ( );
            controlClear ( );
            txtARS001 . Enabled = true;
            tableBody = _bll . getTableViewMain ( "1=2" );
            gridControl1 . DataSource = tableBody;
            tableBodyOne = _bll . getTableViewBody ( string . Empty ,string . Empty );
            gridControl3 . DataSource = tableBodyOne;

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            editTool ( );
            state = "edit";
            controlEnable ( );
            txtARS001 . Enabled = false;

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string.IsNullOrEmpty(txtARS001.Text) )
            {
                XtraMessageBox . Show ( "请选择需要删除的品号" );
                return 0;
            }
            result = _bll . Delete ( txtARS001 . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                controlClear ( );
                Query ( );
                deleteTool ( );
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getValue ( ) == false )
            {
                ClassForMain . FormClosingState . formClost = false;
                return 0;
            }

            if ( state . Equals ( "add" ) )
            {
                if ( _bll . Exists ( txtARS001 . Text ) )
                {
                    XtraMessageBox . Show ( "系统中已经存在此品号,请查询" );
                    ClassForMain.FormClosingState.formClost = false;
                    return 0;
                }
                result = _bll . Save ( body ,tableBody ,table );
            }
            else
                result = _bll . Edit ( body ,tableBody ,idxList ,table ,idxListOne );
            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                 ClassForMain.FormClosingState.formClost = true;
                saveTool ( );
                controlUnEnable ( );
                this . thread = new Thread ( new ThreadStart ( this . SetTextSafePost ) );
                this . thread . Start ( );
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
            else
            {
                XtraMessageBox . Show ( "保存失败" );
                 ClassForMain.FormClosingState.formClost = false;
            }

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            if ( state . Equals ( "add" ) )
                controlClear ( );
            controlUnEnable ( );
            cancelTool ( state );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Cancel ( );
        }
        protected override int Export ( )
        {
            int [ ] selectRows = gridView2 . GetSelectedRows ( );
            if ( selectRows . Length < 1 )
            {
                XtraMessageBox . Show ( "请选择需要导出的内容" );
                return 0;
            }

            List<string> piList = new List<string> ( );
            foreach ( int i in selectRows )
            {
                DataRow row = gridView2 . GetDataRow ( i );
                if ( row != null )
                {
                    piList . Add ( row [ "ARS001" ] . ToString ( ) );
                }
            }

            DataTable tableExport = _bll . getTableExport ( piList );
            tableExport . TableName = "TableOne";
            Export ( new DataTable [ ] { tableExport } ,"工艺信息.frx" );

            return base . Export ( );
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtARS001 . ReadOnly = txtARS008.ReadOnly= txtARS011.ReadOnly= true;
            //gridView1 . OptionsBehavior . Editable = false;
            gridView4 . OptionsBehavior . Editable = false;
            btnCopy . Enabled = true;
        }
        void controlEnable ( )
        {
            txtARS001 . ReadOnly = txtARS008 . ReadOnly = txtARS011 . ReadOnly = false;
            //gridView1 . OptionsBehavior . Editable = true;
            gridView4 . OptionsBehavior . Editable = true;
            btnCopy . Enabled = false;
        }
        void controlClear ( )
        {
            txtARS001 . EditValue = null;
            txtARS001 . Text = txtARS002 . Text = txtARS003 . Text = txtARS004 . Text = txtARS005 . Text = txtARS006 . Text = txtARS007 . Text = txtARS008 . Text = txtARS009 . Text = txtdaa002 . Text = txtdda003 . Text = txtdea010 . Text = txtARS011 . Text = string . Empty;
            gridControl1 . DataSource = null;
            gridControl3 . DataSource = null;
        }
        void InitData ( )
        {
            txtARS001 . Properties . DataSource = _bll . getTableProInfo ( );
            txtARS001 . Properties . DisplayMember = "DEA001";
            txtARS001 . Properties . ValueMember = "DEA001";

            tableEmp = _bll . getTableView ( );
            emNum . DataSource = tableEmp;
            emNum . DisplayMember = "MAP001";
            emNum . ValueMember = "MAP001";

            tableModel = _bll . getTableMould ( );
            EditModel . DataSource = tableModel;
            EditModel . DisplayMember = "MOI001";
            EditModel . ValueMember = "MOI001";

            cmb . Items . Clear ( );
            DataTable tablePost = _bll . getTablePost ( );
            if ( tablePost == null || tablePost . Rows . Count < 1 )
                return;
            cmb . Items . Add ( string . Empty );
            foreach ( DataRow row in tablePost . Rows )
            {
                cmb . Items . Add ( row [ "EMP007" ] );
            }
        }
        void SetTextSafePost (  )
        {
            tableView = _bll . getTableViewAll ( );
            //在线程中更新UI（通过UI线程同步上下文m_SyncContext）
            m_SyncContext . Post ( getTable ,tableView );
        }
        void setValue ( LineProductMesEntityu.ArsEntity model )
        {
            txtARS001 . EditValue = model . ARS001;
            txtARS001 . Text = model . ARS001;
            txtARS002 . Text = model . ARS002;
            txtARS003 . Text = model . ARS003;
            txtARS004 . Text = model . ARS004;
            txtARS005 . Text = model . ARS005;
            txtARS006 . Text = model . ARS006;
            txtARS007 . Text = model . ARS007;
            txtARS008 . Text = Convert . ToDecimal ( model . ARS008 ) . ToString ( "0.###" );
            txtARS009 . Text = model . ARS009;
            txtARS011 . Text = Convert . ToDecimal ( model . ARS011 ) . ToString ( "0.######" );
        }
        bool getValue ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtARS001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择品号" );
                return false;
            }

            decimal outValue = 0M;
            if ( !string . IsNullOrEmpty ( txtARS011 . Text ) && decimal . TryParse ( txtARS011 . Text ,out outValue )==false )
            {
                MessageBox . Show ( "体积请填写数字" );
                return false;
            }
            body . ARS001 = txtARS001 . Text;
            body . ARS002 = txtARS002 . Text;
            body . ARS003 = txtARS003 . Text;
            body . ARS004 = txtARS004 . Text;
            body . ARS005 = txtARS005 . Text;
            body . ARS006 = txtARS006 . Text;
            body . ARS007 = txtARS007 . Text;
            body . ARS008 = string . IsNullOrEmpty ( txtARS008 . Text ) == true ? 0 : Convert . ToDecimal ( txtARS008 . Text );
            body . ARS009 = txtARS009 . Text;
            body . ARS011 = string . IsNullOrEmpty ( txtARS011 . Text ) == true ? 0 : Convert . ToDecimal ( txtARS011 . Text );

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            gridView4 . CloseEditor ( );
            gridView4 . UpdateCurrentRow ( );

            if ( table == null || table . Count < 1 )
                table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
            else
            {
                if ( table . ContainsKey ( _bodyOne . ARU001 + _bodyOne . ARU002 ) )
                {
                    table . Remove ( _bodyOne . ARU001 + _bodyOne . ARU002 );
                    table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
                }else
                    table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
            }

            return result;
        }
        void getTable (object text )
        {
            gridControl2 . DataSource =(DataTable) text;
        }
        void getViewTable ( )
        {
            tableBodyOne = _bll . getTableViewBody ( _bodyOne . ARU001 ,_bodyOne . ARU002 );
            gridControl3 . DataSource = tableBodyOne;
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
        private void gridView4_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"提示" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] );
                if ( model . idx > 0 && !idxList . Contains ( model . idx . ToString ( ) ) )
                    idxList . Add ( model . idx . ToString ( ) );
                tableBody . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
            }
        }
        private void gridControl3_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            DataRow row = gridView4 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"提示" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] );
                if ( _bodyOne . idx > 0 && !idxListOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                    idxListOne . Add ( _bodyOne . idx . ToString ( ) );
                tableBodyOne . Rows . Remove ( row );
                gridControl3 . RefreshDataSource ( );
            }
        }
        private void txtARS001_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtARS001 . EditValue == null || txtARS001 . EditValue . ToString ( ) == string . Empty )
            {
                txtARS002 . Text = txtARS003 . Text = txtARS004 . Text = txtARS005 . Text = txtARS006 . Text = txtARS007 . Text = txtARS009 . Text = string . Empty;
                return;
            }
            DataRow row = View4 . GetFocusedDataRow ( );
            if ( row == null )
            {
                if ( string . IsNullOrEmpty ( txtARS001 . Text ) )
                    return;
                DataTable tab = _bll . getTableProInfo ( txtARS001 . Text );
                if ( tab == null || tab . Rows . Count < 1 )
                    return;
                DataRow r = tab . Rows [ 0 ];
                txtdea010 . Text = r [ "DEA010" ] . ToString ( );
                txtdaa002 . Text = r [ "DAA002" ] . ToString ( );
                txtdda003 . Text = r [ "DDA003" ] . ToString ( );
                return;
            }
            txtARS002 . Text = row [ "DEA002" ] . ToString ( );
            txtARS003 . Text = row [ "DEA008" ] . ToString ( );
            txtARS004 . Text = row [ "DEA057" ] . ToString ( );
            txtARS005 . Text = row [ "DEA962" ] . ToString ( );
            txtARS006 . Text = row [ "DEA003" ] . ToString ( );
            txtARS007 . Text = row [ "DEA009" ] . ToString ( );
            txtARS009 . Text = row [ "DEA076" ] . ToString ( );
            txtdea010 . Text = row [ "DEA010" ] . ToString ( );
            txtdaa002 . Text = row [ "DAA002" ] . ToString ( );
            txtdda003 . Text = row [ "DDA003" ] . ToString ( );
        }
        private void artInfo_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( tableArt == null || tableArt . Rows . Count < 1 )
                return;
            BaseEdit edit = gridView1 . ActiveEditor;
            if ( gridView1 . FocusedColumn . FieldName == "ART002" )
            {
                if ( edit . EditValue == null )
                    return;
                if ( tableArt . Select ( "QBA001='" + edit . EditValue + "'" ) . Length < 1 )
                    return;
                DataRow row = tableArt . Select ( "QBA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                model . ART002 = edit . EditValue . ToString ( );
                model . ART003 = row [ "QBA002" ] . ToString ( );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ART003" ] ,model . ART003 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ART002" ] ,model . ART002 );
            }
        }
        private void emNum_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView1 . ActiveEditor;
            if ( gridView1 . FocusedColumn . FieldName == "ART006" )
            {
                if ( txtARS001 . Text == string . Empty )
                {
                    XtraMessageBox . Show ( "请选择品号" );
                    return;
                }
                if ( edit . EditValue == null )
                    return;
                if ( tableEmp . Select ( "MAP001='" + edit . EditValue + "'" ) . Length < 1 )
                    return;
                DataRow row = tableEmp . Select ( "MAP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                model . ART006 = edit . EditValue . ToString ( );
                model . ART007 = row [ "MAP002" ] . ToString ( );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ART007" ] ,model . ART007 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ART006" ] ,model . ART006 );
            }
        }
        private void EditModel_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView4 . ActiveEditor;
            if ( gridView4 . FocusedColumn . FieldName == "ARU003" )
            {
                if ( txtARS001 . Text == string . Empty )
                {
                    XtraMessageBox . Show ( "请选择品号" );
                    return;
                }
                if ( edit . EditValue == null )
                    return;
                if ( tableModel . Select ( "MOI001='" + edit . EditValue + "'" ) . Length < 1 )
                    return;
                DataRow row = tableModel . Select ( "MOI001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( tableBodyOne . Select ( "ARU003='" + edit . EditValue + "'" ) . Length > 0 )
                    edit . EditValue = null;
                if ( edit . EditValue == null )
                {
                    gridView4 . CloseEditor ( );
                    gridView4 . UpdateCurrentRow ( );

                    tableBodyOne . Rows . RemoveAt ( tableBodyOne . Rows . Count - 1 );
                    return;
                }
                _bodyOne .ARU004 = row [ "MOI002" ] . ToString ( );
                gridView4 . SetFocusedRowCellValue ( gridView4 . Columns [ "ARU004" ] ,_bodyOne . ARU004 );
            }
        }
        private void gridView2_DoubleClick ( object sender ,EventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
                return;
            DataRow row = gridView2 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            model . ART001 = row [ "ARS001" ] . ToString ( );
            xtraTabControl1 . SelectedTabPage = TabPageOne;
            LineProductMesEntityu . ArsEntity body = _bll . getModel ( model . ART001 );
            setValue ( body );
            tableBody = _bll . getTableViewMain ( model . ART001 );
            gridControl1 . DataSource = tableBody;
            if ( tableBody != null && tableBody . Rows . Count > 0 )
            {
                _bodyOne . ARU001 = tableBody . Rows [ 0 ] [ "ART001" ] . ToString ( );
                _bodyOne . ARU002 = tableBody . Rows [ 0 ] [ "ART011" ] . ToString ( );
                tableBodyOne = _bll . getTableViewBody ( _bodyOne . ARU001 ,_bodyOne . ARU002 );
            }
            else
                tableBodyOne = _bll . getTableViewBody ( string . Empty ,string . Empty );
            gridControl3 . DataSource = tableBodyOne;
            QueryTool ( );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }
        private void gridView1_InitNewRow ( object sender ,DevExpress . XtraGrid . Views . Grid . InitNewRowEventArgs e )
        {
            DevExpress . XtraGrid . Views . Grid . GridView view = sender as DevExpress . XtraGrid . Views . Grid . GridView;
            model . ART011 = string . IsNullOrEmpty ( tableBody . Compute ( "MAX(ART011)" ,null ) . ToString ( ) ) == true ? "001" : ( Convert . ToInt32 ( tableBody . Compute ( "MAX(ART011)" ,null ) ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
            view . SetRowCellValue ( e . RowHandle ,view . Columns [ "ART011" ] ,model . ART011 );
        }
        private void gridView4_InitNewRow ( object sender ,DevExpress . XtraGrid . Views . Grid . InitNewRowEventArgs e )
        {
            DevExpress . XtraGrid . Views . Grid . GridView view = sender as DevExpress . XtraGrid . Views . Grid . GridView;
            view . SetRowCellValue ( e . RowHandle ,view . Columns [ "ARU002" ] ,_bodyOne . ARU002 );
            view . SetRowCellValue ( e . RowHandle ,view . Columns [ "ART003" ] ,_bodyOne . ARU005 );
        }
        private void gridView1_FocusedRowChanged ( object sender ,DevExpress . XtraGrid . Views . Base . FocusedRowChangedEventArgs e )
        {
            DataRow row = gridView1 . GetDataRow ( e . PrevFocusedRowHandle );
            if ( row == null )
                return;
            if ( row [ "ART002" ] == null || row [ "ART002" ] . ToString ( ) == string . Empty )
            {
                XtraMessageBox . Show ( "请选择工艺" );
                gridView1 . FocusedRowHandle = e . PrevFocusedRowHandle;
                return;
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName != "ART002" && e . Column . FieldName != "ART003" && e . Column . FieldName != "ART011" )
            {
                if ( row [ "ART002" ] == null || row [ "ART002" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "请选择工艺" );
                    row [ e . Column . FieldName ] = DBNull . Value;
                }
            }
        }
        private void txtARS009_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtARS009 . Text == string . Empty )
                return;
            tableArt = _bll . getTableArt ( txtARS009 . Text );
            artInfo . DataSource = tableArt;
            artInfo . DisplayMember = "QBA001";
            artInfo . ValueMember = "QBA001";

            //if ( tableArt == null || tableArt . Rows . Count < 1 )
            //    return;
            //if ( tableArt . Select ( "QBA006='" + txtARS009 . Text + "'" ) . Length < 1 )
            //    return;
            //DataRow [ ] rows = tableArt . Select ( "QBA006='" + txtARS009 . Text + "'" );
            //if ( rows == null )
            //    return;
            //foreach ( DataRow row in rows )
            //{
            //    model . ART011 = tableBody . Compute ( "MAX(ART011)" ,null ) == null ? "001" : tableBody . Compute ( "MAX(ART011)" ,null ) . ToString ( );
            //    model . ART011 = model . ART011 == string . Empty ? "001" : ( Convert . ToInt32 ( model . ART011 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
            //    DataRow ro = tableBody . NewRow ( );
            //    ro [ "ART002" ] = row [ "QBA001" ];
            //    ro [ "ART003" ] = row [ "QBA002" ];
            //    ro [ "ART011" ] = model . ART011;
            //    tableBody . Rows . Add ( ro );
            //}
            //gridControl2 . RefreshDataSource ( );
        }
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            
            gridView4 . CloseEditor ( );
            gridView4 . UpdateCurrentRow ( );

            _bodyOne . ARU001 = txtARS001 . Text;
            _bodyOne . ARU002 = row [ "ART011" ] . ToString ( );
            _bodyOne . ARU005 = row [ "ART003" ] . ToString ( );
            if ( table == null || table . Count < 1 )
                getViewTable ( );
            else
            {
                if ( table . ContainsKey ( _bodyOne . ARU001 + _bodyOne . ARU002 ) )
                    tableBodyOne = ( DataTable ) table [ _bodyOne . ARU001 + _bodyOne . ARU002 ];
                else
                    getViewTable ( );
            }
            if ( table == null || table . Count < 1 )
                table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
            else
            {
                if ( table . ContainsKey ( _bodyOne . ARU001 + _bodyOne . ARU002 ) )
                {
                    table . Remove ( _bodyOne . ARU001 + _bodyOne . ARU002 );
                    table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
                }
                else
                    table . Add ( _bodyOne . ARU001 + _bodyOne . ARU002 ,tableBodyOne );
            }

            gridControl3 . DataSource = tableBodyOne;
            gridControl3 . RefreshDataSource ( );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtARS001 . Text == string . Empty || tableBody == null || tableBody . Rows . Count < 1 )
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
        //复制
        private void btnCopy_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtARS001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择需要复制的单据" );
                return;
            }

            editTool ( );
            state = "add";
            controlEnable ( );

            txtARS001 . EditValue = null;
            txtARS001 . Text = txtARS002 . Text = txtARS003 . Text = txtARS004 . Text = txtARS005 . Text = txtARS006 . Text = txtARS007 . Text = txtARS008 . Text = txtARS009 . Text = txtdaa002 . Text = txtdda003 . Text = txtdea010 . Text = string . Empty;
            txtARS001 . ReadOnly = txtARS008 . ReadOnly = false;
        }
        //复制
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        private void gridView1_RowCellClick ( object sender ,RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void contextMenuStrip2_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView4 ,focuseName );
        }
        private void gridView4_RowCellClick ( object sender ,RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        #endregion

    }
}