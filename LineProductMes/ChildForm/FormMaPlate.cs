

using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using System;
using System . Reflection;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormMaPlate :FormBaseChild
    {
        LineProductMesEntityu.MachinePlatEntity _model=null;
        LineProductMesBll.Bll.MachinePlatBll _bll=null;
        
        public FormMaPlate ( LineProductMesEntityu . MachinePlatEntity model )
        {
            InitializeComponent ( );

            _model = new LineProductMesEntityu . MachinePlatEntity ( );
            _bll = new LineProductMesBll . Bll . MachinePlatBll ( );

            GridViewMoHuSelect . SetFilter ( view );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            getTable ( );

            this . _model = model;
            setValue ( );

            if ( model == null || model . idx < 1 )
            {
                this . Text = this . Text + "新增";
                txtMAP001 . Text = _bll . getOddNum ( );
            }
            else
                this . Text = this . Text + "编辑";
        }

        void getTable ( )
        {
            txtMAP004 . Properties . DataSource = _bll . getTableWork ( );
            txtMAP004 . Properties . DisplayMember = "DAA002";
            txtMAP004 . Properties . ValueMember = "DAA001";
        }
        void setValue ( )
        {
            if ( this . _model == null )
                return;
            txtMAP001 . Tag = _model . idx;
            txtMAP001 . Text = _model . MAP001;
            txtMAP002 . Text = _model . MAP002;
            txtMAP004 . EditValue = _model . MAP003;
            txtMAP005 . Text = _model . MAP005;
            txtMAP006 . Text = _model . MAP006;
        }

        private void btnOk_Click ( object sender ,System . EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtMAP002 . Text ) )
            {
                XtraMessageBox . Show ( "请输入机台名称" );
                return;
            }
            if ( string . IsNullOrEmpty ( txtMAP004 . Text ) )
            {
                XtraMessageBox . Show ( "请选择车间" );
                return;
            }
            _model = new LineProductMesEntityu . MachinePlatEntity ( );
            _model . MAP001 = txtMAP001 . Text;
            _model . MAP002 = txtMAP002 . Text;
            _model . MAP003 = txtMAP004 . EditValue . ToString ( );
            _model . MAP004 = txtMAP004 . Text;
            _model . MAP005 = txtMAP005 . Text;
            _model . MAP006 = txtMAP006 . Text;
            _model . idx = txtMAP001 . Tag == null ? 0 : Convert . ToInt32 ( txtMAP001 . Tag );
            _model . idx = _bll . Save ( _model );
            if ( _model . idx > 0 )
            {
                XtraMessageBox . Show ( "成功保存" );
                this . DialogResult = System . Windows . Forms . DialogResult . OK;
            }
            else
                XtraMessageBox . Show ( "保存失败" );
        }

        private void btnCancel_Click ( object sender ,System . EventArgs e )
        {
            this . DialogResult = System . Windows . Forms . DialogResult . Cancel;
        }

        public LineProductMesEntityu . MachinePlatEntity getModel
        {
            get
            {
                return _model;
            }
        }

    }
}