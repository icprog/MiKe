using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class ProControlChild :FormBaseChild
    {
        LineProductMesBll.Bll.ProControlBll _bll=null;
        LineProductMesEntityu.MainEntity model=null;

        public ProControlChild (string text ,LineProductMesEntityu . MainEntity model )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . ProControlBll ( );
            GridViewMoHuSelect . SetFilter ( View );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { this . View } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            this . Text = this . Text + text;
            getTable ( );

            this . model = model;
            setValue ( );
        }
        
        protected void getTable ( )
        {
            DataTable tableView = _bll . getPInfo ( );
            txtPid . Properties . DataSource = tableView;
            txtPid . Properties . DisplayMember = "FOR004";
            txtPid . Properties . ValueMember = "FOR001";
        }
        protected void setValue ( )
        {
            if ( model == null )
                return;
            txtId . Text = model . FOR001 . ToString ( );
            txtPid . EditValue = model . FOR002 . ToString ( );
            txtProId . Text = model . FOR003;
            txtProName . Text = model . FOR004;
            txtTable . Text = model . FOR005;
            txtType . Text = model . FOR006;
        }
        protected bool getValue ( )
        {
            if ( string . IsNullOrEmpty ( txtPid . Text ) )
            {
                XtraMessageBox . Show ( "请选择父节点" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtId . Text ) )
            {
                XtraMessageBox . Show ( "请选择子节点" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtType . Text ) )
            {
                XtraMessageBox . Show ( "请程序类别" );
                return false;
            }
            int outResult = 0;
            if ( !string . IsNullOrEmpty ( txtId . Text ) && int . TryParse ( txtId . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "节点为整数" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtProName . Text ) )
            {
                XtraMessageBox . Show ( "请填写程序名称" );
                return false;
            }
            model = new LineProductMesEntityu . MainEntity ( );
            model . FOR001 = Convert . ToInt32 ( txtId . Text );
            model . FOR002 = Convert . ToInt32 ( txtPid . EditValue );
            model . FOR003 = txtProId . Text;
            model . FOR004 = txtProName . Text;
            model . FOR005 = txtTable . Text;
            model . FOR006 = txtType . Text;

            return true;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void btnOk_Click ( object sender ,EventArgs e )
        {
            if ( getValue ( ) == false )
                return;
            bool result = false;
            result = _bll . Save ( model );
            if ( result )
            {
                XtraMessageBox . Show ( "保存成功" );
                this . DialogResult = DialogResult . OK;
            }
            else
                XtraMessageBox . Show ( "保存失败" );
        }

        public LineProductMesEntityu . MainEntity getModel
        {
            get
            {
                return model;
            }
        }

    }
}
