using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Drawing;
using System . Text;
using System . Linq;
using System . Threading . Tasks;
using System . Windows . Forms;
using DevExpress . XtraEditors;

namespace LineProductMes . ChildForm
{
    public partial class FormBatOpera :FormBase
    {
        DataTable table;

        LineProductMesBll.Bll.ProductPlanPreviewBll _bll;

        public FormBatOpera ( string text ,DataTable table )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . ProductPlanPreviewBll ( );

            this . Text = text;

            if ( text . Equals ( "增加排产天数" ) )
                P3 . Visible = P4 . Visible = P6 . Visible = false;
            if ( text . Equals ( "批量清空" ) )
                P5 . Visible = P6 . Visible = false;
            if ( text . Equals ( "批量分摊" ) )
                P5 . Visible = false;

            this . table = table;
            gridControl1 . DataSource = this . table;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void btnSure_Click ( object sender ,EventArgs e )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            bool result = true;

            if ( this . Text . Equals ( "增加排产天数" ) )
            {
                foreach ( DataRow row in table . Rows )
                {
                    if ( string . IsNullOrEmpty ( row [ "P5" ] . ToString ( ) ) )
                    {
                        XtraMessageBox . Show ( "请输入天数" );
                        result = false;
                        break;
                    }
                }

                if ( result == false )
                    return;

                result = _bll . SaveDay ( table );
                if ( result )
                {
                    XtraMessageBox . Show ( "增加成功" );
                    this . DialogResult = DialogResult . OK;
                }
                else
                    XtraMessageBox . Show ( "增加失败" );
            }
            else if ( this . Text . Equals ( "批量清空" ) )
            {
                foreach ( DataRow row in table . Rows )
                {
                    if ( string . IsNullOrEmpty ( row [ "P3" ] . ToString ( ) ) )
                    {
                        XtraMessageBox . Show ( "请选择开始日期" );
                        result = false;
                        break;
                    }
                    if ( string . IsNullOrEmpty ( row [ "P4" ] . ToString ( ) ) )
                    {
                        XtraMessageBox . Show ( "请选择结束日期" );
                        result = false;
                        break;
                    }
                }
                if ( result == false )
                    return;

                this . DialogResult = DialogResult . OK;
            }
            else if ( this . Text . Equals ( "批量分摊" ) )
            {
                foreach ( DataRow row in table . Rows )
                {
                    if ( string . IsNullOrEmpty ( row [ "P3" ] . ToString ( ) ) )
                    {
                        XtraMessageBox . Show ( "请选择开始日期" );
                        result = false;
                        break;
                    }
                    if ( string . IsNullOrEmpty ( row [ "P4" ] . ToString ( ) ) )
                    {
                        XtraMessageBox . Show ( "请选择结束日期" );
                        result = false;
                        break;
                    }
                    if ( string . IsNullOrEmpty ( row [ "P6" ] . ToString ( ) ) )
                    {
                        XtraMessageBox . Show ( "请录入调整量" );
                        result = false;
                        break;
                    }
                }
                if ( result == false )
                    return;

                this . DialogResult = DialogResult . OK;
            }
        }

        public DataTable getTable
        {
            get
            {
                return table;
            }
        }

    }
}