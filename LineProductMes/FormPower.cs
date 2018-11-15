using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class FormPower :FormChild
    {
        LineProductMesBll.Bll.PowerBll _bll=null;
        LineProductMesEntityu.PowerEntity _model=null;
        string nameOfPerson=string.Empty,nameOfProgram=string.Empty,strWhere=string.Empty;
        bool result=false;

        DataTable tablePro,tableQuery;
        
        public FormPower ( )
        {
            InitializeComponent ( );

            GridViewMoHuSelect . SetFilter ( gridView1 );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            _bll = new LineProductMesBll . Bll . PowerBll ( );
            _model = new LineProductMesEntityu . PowerEntity ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarButtonItem [ ] { toolExport ,toolPrint ,toolCancellation ,toolExamin } );
            
            lookUpEdit1 . Properties . DataSource = _bll . GetPerson ( );
            lookUpEdit1 . Properties . DisplayMember = "EMP002";
            lookUpEdit1 . Properties .ValueMember = "EMP001";
            tablePro = _bll . GetProgram ( );
            lookUpEdit2 . Properties . DataSource = tablePro;
            lookUpEdit2 . Properties . DisplayMember = "FOR004";
            lookUpEdit2 . Properties . ValueMember = "FOR003";

            gridView1 . OptionsBehavior . Editable = false;
        }

        #region Main
        protected override int Query ( )
        {
            strWhere = "1=1";

            if ( !string . IsNullOrEmpty ( lookUpEdit2 . Text ) )
                strWhere = strWhere + " AND POW003='" + lookUpEdit2 . EditValue . ToString ( ) + "'";
            if ( !string . IsNullOrEmpty ( lookUpEdit1 . Text ) )
                strWhere = strWhere + " AND POW002='" + lookUpEdit1 . EditValue . ToString ( ) + "'";

            tableQuery = _bll . GetDataTable ( strWhere );
            gridControl1 . DataSource = tableQuery;

            gridView1 . OptionsBehavior . Editable = false;

            QueryTool ( );

            return 0;
        }
        protected override int Delete ( )
        {
            if ( _model . idx < 1 )
            {
                XtraMessageBox . Show ( "请选择需要删除的内容" );
                return 0;
            }
            if ( XtraMessageBox . Show ( "确定删除选中内容?" ,"删除?" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
            {
                result = _bll . Delete ( _model . idx );
                if ( result == true )
                {
                    XtraMessageBox . Show ( "删除成功" );
                    Query ( );
                }
                else
                    XtraMessageBox . Show ( "删除失败,请重试" );
            }

            return 0;
        }
        protected override int Add ( )
        {
            //LineProductMes . ChildForm . PowerEdit form = new LineProductMes . ChildForm . PowerEdit ( "增加" ,null ,nameOfPerson ,nameOfProgram ,"add" );
            //form . StartPosition = FormStartPosition . CenterScreen;
            //DialogResult result = form . ShowDialog ( );
            //if ( result == System.Windows.Forms.DialogResult.OK )
            //{
            //    Query ( );
            //}

            if ( string . IsNullOrEmpty ( lookUpEdit1 . Text ) )
            {
                XtraMessageBox . Show ( "请选择人员信息" );
                return 0;
            }

            strWhere = "1=1";
            strWhere = strWhere + " AND POW002='" + lookUpEdit1 . EditValue . ToString ( ) + "'";

            tableQuery = _bll . GetDataTable ( strWhere );
            foreach ( DataRow row in tablePro . Rows )
            {
                if ( tableQuery . Select ( "POW003='" + row [ "FOR003" ] + "'" ) . Length < 1 )
                {
                    DataRow rows = tableQuery . NewRow ( );
                    rows [ "EMP002" ] = lookUpEdit1 . Text;
                    rows [ "POW002" ] = lookUpEdit1 . EditValue;
                    rows [ "FOR004" ] = row [ "FOR004" ];
                    rows [ "POW003" ] = row [ "FOR003" ];
                    rows [ "POW004" ] = 0;
                    rows [ "POW005" ] = 0;
                    rows [ "POW006" ] = 0;
                    rows [ "POW007" ] = 0;
                    rows [ "POW008" ] = 0;
                    rows [ "POW009" ] = 0;
                    rows [ "POW010" ] = 0;
                    rows [ "POW011" ] = 0;
                    rows [ "POW012" ] = 0;
                    rows [ "POW013" ] = 0;
                    rows [ "POW016" ] = 0;
                    rows [ "POW017" ] = 0;
                    tableQuery . Rows . Add ( rows );
                }
            }
            gridControl1 . DataSource = tableQuery;

            addTool ( );
            gridView1 . OptionsBehavior . Editable = true;

            return 0;
        }
        protected override int Edit ( )
        {
            //if ( _model . idx < 1 )
            //{
            //    XtraMessageBox . Show ( "请选择需要编辑的内容" );
            //    return 0;
            //}

            //edit ( );

            if ( tableQuery == null || tableQuery . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请查询需要编辑的内容" );
                return 0;
            }

            gridView1 . OptionsBehavior . Editable = true;
            editTool ( );

            return 0;
        }
        protected override int Save ( )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            result = _bll . Save ( tableQuery );
            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                saveTool ( );
                gridView1 . OptionsBehavior . Editable = false;

                tableQuery = _bll . GetDataTable ( strWhere );
                gridControl1 . DataSource = tableQuery;
            }
            else
                XtraMessageBox . Show ( "保存失败" );

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            cancelTool ( "edit" );
            gridView1 . OptionsBehavior . Editable = false;
            return base . Cancel ( );
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
        private void gridView1_RowClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowClickEventArgs e )
        {
            int num = gridView1 . FocusedRowHandle;
            if ( num >= 0 && num <= gridView1 . DataRowCount - 1 )
            {
                DataRow row = gridView1 . GetDataRow ( num );
                _model . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : int . Parse ( row [ "idx" ] . ToString ( ) );
                _model . POW002 = row [ "POW002" ] . ToString ( );
                _model . POW003 = row [ "POW003" ] . ToString ( );
                _model . POW004 = string . IsNullOrEmpty ( row [ "POW004" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW004" ] . ToString ( ) );
                _model . POW005 = string . IsNullOrEmpty ( row [ "POW005" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW005" ] . ToString ( ) );
                _model . POW006 = string . IsNullOrEmpty ( row [ "POW006" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW006" ] . ToString ( ) );
                _model . POW007 = string . IsNullOrEmpty ( row [ "POW007" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW007" ] . ToString ( ) );
                _model . POW008 = string . IsNullOrEmpty ( row [ "POW008" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW008" ] . ToString ( ) );
                _model . POW009 = string . IsNullOrEmpty ( row [ "POW009" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW009" ] . ToString ( ) );
                _model . POW010 = string . IsNullOrEmpty ( row [ "POW010" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW010" ] . ToString ( ) );
                _model . POW011 = string . IsNullOrEmpty ( row [ "POW011" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW011" ] . ToString ( ) );
                _model . POW012 = string . IsNullOrEmpty ( row [ "POW012" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW012" ] . ToString ( ) );
                _model . POW013 = string . IsNullOrEmpty ( row [ "POW013" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW013" ] . ToString ( ) );
                _model . POW016 = string . IsNullOrEmpty ( row [ "POW016" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW016" ] . ToString ( ) );
                _model . POW017 = string . IsNullOrEmpty ( row [ "POW017" ] . ToString ( ) ) == true ? false : bool . Parse ( row [ "POW017" ] . ToString ( ) );
                nameOfPerson = row [ "EMP002" ] . ToString ( );
                nameOfProgram= row [ "FOR004" ] . ToString ( );
            }
        }
        private void gridView1_DoubleClick ( object sender ,System . EventArgs e )
        {
            //edit ( );
        }
        private void btnCl_Click_1 ( object sender ,System . EventArgs e )
        {
            lookUpEdit1 . EditValue = lookUpEdit2 . EditValue = null;
            lookUpEdit1 . ItemIndex = lookUpEdit2 . ItemIndex = -1;
        }
        #endregion

        #region Method
        void edit ( )
        {
            ChildForm . PowerEdit form = new  ChildForm . PowerEdit ( "编辑" ,_model ,nameOfPerson ,nameOfProgram ,"edit" );
            form . StartPosition = FormStartPosition . CenterScreen;
            DialogResult result = form . ShowDialog ( );
            if ( result == System . Windows . Forms . DialogResult . OK )
            {
                Query ( );
            }
        }
        #endregion

    }
}
