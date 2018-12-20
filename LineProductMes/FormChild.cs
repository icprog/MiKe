using DevExpress . XtraBars;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using LineProductMesBll;
using System;
using System . Collections . Generic;
using System . Windows . Forms;

namespace LineProductMes
{
    public partial class FormChild :FormBaseChild
    {
        public FormChild ( )
        {
            InitializeComponent ( );

            this . FormClosing += FormChild_FormClosing1;

            //Power ( );
        }

        private void FormChild_FormClosing1 ( object sender ,FormClosingEventArgs e )
        {
            Form form = ( Form ) ( sender );
            if ( FormClosingState . mdiChildForm . Contains ( form . Name ) )
                FormClosingState . mdiChildForm . Remove ( form . Name );
        }

        private void FormChild_Load ( object sender ,EventArgs e )
        {
            //Form form = ( Form ) ( sender );
            //LineProductMesBll . UserInfoMation . programName = form . Name;
            //LineProductMesBll . UserInfoMation . programText = form . Text;

            toolState ( );
        }

        void Power ( )
        {
            if ( UserInfoMation . userNum . Equals ( "DS" ) )
                return;

            List<LineProductMesEntityu . PowerEntity> modelList = UserInfoMation . PowList . FindAll ( ( x ) =>
              {
                  return x . POW002 == UserInfoMation . userNum && x . POW003 == UserInfoMation . programName;
              } );
            if ( modelList . Count > 0 )
            {
                foreach ( LineProductMesEntityu . PowerEntity model in modelList )
                {
                    if ( barTool . LinksPersistInfo . Count > toolCanecl . Id && barTool . LinksPersistInfo [ toolCanecl . Id ] . Item . Name == "toolCanecl" )
                    {
                        if ( model . POW011 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolCanecl . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolSave . Id && barTool . LinksPersistInfo [ toolSave . Id ] . Item . Name == "toolSave" )
                    {
                        if ( model . POW010 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolSave . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolExport . Id && barTool . LinksPersistInfo [ toolExport . Id ] . Item . Name == "toolExport" )
                    {
                        if ( model . POW017 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolExport . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolPrint . Id && barTool . LinksPersistInfo [ toolPrint . Id ] . Item . Name == "toolPrint" )
                    {
                        if ( model . POW016 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolPrint . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolCancellation . Id && barTool . LinksPersistInfo [ toolCancellation . Id ] . Item . Name == "toolCancellation" )
                    {
                        if ( model . POW012 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolCancellation . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolExamin . Id && barTool . LinksPersistInfo [ toolExamin . Id ] . Item . Name == "toolExamin" )
                    {
                        if ( model . POW009 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolExamin . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolDelete . Id && barTool . LinksPersistInfo [ toolDelete . Id ] . Item . Name == "toolDelete" )
                    {
                        if ( model . POW006 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolDelete . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolEdit . Id && barTool . LinksPersistInfo [ toolEdit . Id ] . Item . Name == "toolEdit" )
                    {
                        if ( model . POW007 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolEdit . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolAdd . Id && barTool . LinksPersistInfo [ toolAdd . Id ] . Item . Name == "toolAdd" )
                    {
                        if ( model . POW005 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolAdd . Id );
                    }
                    if ( barTool . LinksPersistInfo . Count > toolQuery . Id && barTool . LinksPersistInfo [ toolQuery . Id ] . Item . Name == "toolQuery" )
                    {
                        if ( model . POW004 == false )
                            barTool . LinksPersistInfo . RemoveAt ( toolQuery . Id );
                    }
                }
            }
            else if ( UserInfoMation . userNum != "DS" )
            {
                barTool . Visible = false;
            }
        }

        protected void closeForm ( )
        {

        }

        protected void toolState ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }

        protected virtual int Query ( )
        {

            return 0;
        }
        protected void QueryTool ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            if ( toolCancellation . Caption . Equals ( "反注销" ) )
            {
                //Concell1 . Show ( );
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
            if ( toolExamin . Caption . Equals ( "反审核" ) )
            {
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }

        }
        private void toolQuery_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "查询";
            Query ( );
        }

        protected virtual int Add ( )
        {

            return 0;
        }
        private void toolAdd_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "新增";
            Add ( );
        }
        protected void addTool ( )
        {
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }


        protected virtual int Edit ( )
        {     
            return 0;
        }
        private void toolEdit_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "编辑";
            Edit ( );
        }
        protected void editTool ( )
        {
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }

        protected virtual int Delete ( )
        {
            
            return 0;
        }
        protected void deleteTool ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }
        private void toolDelete_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "删除";
            if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                Delete ( );
        }

        protected virtual int Examine ( )
        {

            return 0;
        }
        private void toolExamin_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "审核";
            Examine ( );
        }
        protected void examineTool ( string state )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            if ( state . Equals ( "审核" ) )
            {
                toolExamin . Caption = "反审核";
                //Concell1 . Show ( );           
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
            else
            {
                toolExamin . Caption = "审核";
                //Concell1 . Hide ( );
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
        }

        protected virtual int Save ( )
        {

            return 0;
        }
        private void toolSave_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "保存";
            Save ( );
        }
        protected void saveTool ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolCancellation . Caption = "注销";
            toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExamin . Caption = "审核";
            toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }

        protected virtual int Cancel ( )
        {

            return 0;
        }
        private void toolCanecl_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "取消";
            Cancel ( );
        }
        protected void cancelTool ( string state )
        {
            if ( state . Equals ( "add" ) )
            {
                toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
            else if ( state . Equals ( "edit" ) )
            {
                toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
        }

        protected void cancelltionTool ( string stateOfCancell )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            if ( stateOfCancell . Equals ( "注销" ) )
            {
                toolCancellation . Caption = "反注销";
                //Concell1 . Show ( );           
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
            else
            {
                toolCancellation . Caption = "注销";
                //Concell1 . Hide ( );
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCancellation . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExamin . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
        }
        protected virtual int Cancellation ( )
        {

            return 0;
        }
        private void toolCancellation_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "注销";
            Cancellation ( );
        }

        protected virtual int Print ( )
        {

            return 0;
        }
        private void toolPrint_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "打印";
            Print ( );
        }

        protected virtual int PrintReport ( )
        {
            return 0;
        }
        private void toolPrintReport_ItemClick ( object sender ,ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "报工单打印";
            PrintReport ( );
        }

        protected virtual int PrintWork ( )
        {
            return 0;
        }
        private void toolPrintWork_ItemClick ( object sender ,ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "入库单打印";
            PrintWork ( );
        }

        protected virtual int PrintBase ( )
        {
            UserInfoMation . TypeOfOper = "打印";
            return 0;
        }
        private void toolPrintBase_ItemClick ( object sender ,ItemClickEventArgs e )
        {
            PrintBase ( );
        }



        protected virtual int Export ( )
        {

            return 0;
        }
        private void toolExport_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "导出";
            Export ( );
        }

        protected virtual int ExportBase ( )
        {
            return 0;
        }
        private void toolExportBase_ItemClick ( object sender ,ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "导出";
            ExportBase ( );
        }

        protected virtual int ExportReport ( )
        {
            return 0;
        }
        private void toopExprotReport_ItemClick ( object sender ,ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "导出报工单";
            ExportReport ( );
        }

        protected virtual int ExportWork ( )
        {
            return 0;
        }
        private void toolExportWork_ItemClick ( object sender ,ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "导出入库单";
            ExportWork ( );
        }
    }
}
