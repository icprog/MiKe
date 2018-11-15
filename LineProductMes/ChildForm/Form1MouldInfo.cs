using System;
using System . Data;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using Utility;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormMouldInfo :FormBaseChild
    {
        LineProductMesEntityu.MouldInformationEntity model=null;
        LineProductMesBll.Bll.MouldInformationBll _bll=null;
        DataTable tableDEA;
        string state=string.Empty,codePrefix = string . Empty;
        
        public FormMouldInfo ( LineProductMesEntityu.MouldInformationEntity entity)
        {
            InitializeComponent ( );

            model = new LineProductMesEntityu . MouldInformationEntity ( );
            _bll = new LineProductMesBll . Bll . MouldInformationBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { View1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            getValue ( );
            model = entity;
            if ( model == null || model . idx < 1 )
                //txtMOI001 . Text = _bll . getOddNum ( );
                state = "add";
            else
                setValue ( );
        }
        
        void getValue ( )
        {
            txtMOI005 . Properties . DataSource = _bll . getTableWork ( );
            txtMOI005 . Properties . DisplayMember = "DAA002";
            txtMOI005 . Properties . ValueMember = "DAA001";
            txtMOI014 . Properties . DataSource = _bll . getTableSupp ( );
            txtMOI014 . Properties . DisplayMember = "SFM002";
            txtMOI014 . Properties . ValueMember = "SFM001";
            tableDEA= _bll . getTablePart ( );
            //txtMOI009 . Properties . DataSource = _bll . getTablePart ( );
            //txtMOI009 . Properties . DisplayMember = "DEA002";
            //txtMOI009 . Properties . ValueMember = "DEA001";
            //txtMOI010 . Properties . DataSource = _bll . getTablePart ( );
            //txtMOI010 . Properties . DisplayMember = "DEA002";
            //txtMOI010 . Properties . ValueMember = "DEA001";
            txtMOI006 . Text = "在用";
        }
        void setValue ( )
        {
            txtMOI001 . Tag = model . idx;
            txtMOI001 . Text = model . MOI001;
            txtMOI002 . Text = model . MOI002;
            txtMOI003 . Text = model . MOI003;
            txtMOI005 . EditValue = model . MOI004;
            txtMOI006 . Text = model . MOI006;
            txtMOI007 . Text = model . MOI007;
            txtMOI009 . Text = model . MOI009;
            txtMOI010 . Text = model . MOI010;
            txtMOI011 . Text = Convert . ToDateTime ( model . MOI011 ) . ToString ( "yyyy/MM/dd" );
            txtMOI012 . Text = model . MOI012;
            txtMOI014 . EditValue = model . MOI013;
            txtMOI015 . Text = model . MOI015;
            txtMOI017 . Text = model . MOI017;
            txtMOI018 . Text = model . MOI018;
            txtMOI017 . Enabled = txtMOI018 . Enabled = false;
        }
        
        bool getValueModel ( )
        {
            dxErrorProvider1 . ClearErrors ( );
            if ( string . IsNullOrEmpty ( txtMOI002 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI002 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtMOI005 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI005 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtMOI006 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI006 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtMOI007 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI007 ,"不可为空" );
                return false;
            }
            //if ( string . IsNullOrEmpty ( txtMOI010 . Text ) )
            //{
            //    dxErrorProvider1 . SetError ( txtMOI010 ,"不可为空" );
            //    return false;
            //}
            if ( string . IsNullOrEmpty ( txtMOI011 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI011 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtMOI014 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI014 ,"不可为空" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtMOI001 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtMOI001 ,"不可为空" );
                return false;
            }
            model = new LineProductMesEntityu . MouldInformationEntity ( );
            model . idx = txtMOI001 . Tag == null ? 0 : Convert . ToInt32 ( txtMOI001 . Tag );
            model . MOI001 = txtMOI001 . Text;
            model . MOI002 = txtMOI002 . Text;
            model . MOI003 = txtMOI003 . Text;
            model . MOI004 = txtMOI005 . EditValue == null ? string . Empty : txtMOI005 . EditValue . ToString ( );
            model . MOI005 = txtMOI005 . Text;
            model . MOI006 = txtMOI006 . Text;
            model . MOI007 = txtMOI007 . Text;
            model . MOI008 = txtMOI009 . EditValue == null ? string . Empty : txtMOI009 . EditValue . ToString ( );
            model . MOI009 = txtMOI009 . Text;
            model . MOI010 = txtMOI010 . Text;
            if ( string . IsNullOrEmpty ( txtMOI011 . Text ) )
                model . MOI011 = null;
            else
                model . MOI011 = Convert . ToDateTime ( txtMOI011 . Text );
            model . MOI012 = txtMOI012 . Text;
            model . MOI013 = txtMOI014 . EditValue == null ? string . Empty : txtMOI014 . EditValue . ToString ( );
            model . MOI014 = txtMOI014 . Text;
            model . MOI015 = txtMOI015 . Text;
            model . MOI016 = false;
            model . MOI017 = txtMOI017 . Text;
            model . MOI018 = txtMOI018 . Text;

            return true;
        }

        private void btnOk_Click ( object sender ,EventArgs e )
        {
            if ( getValueModel ( ) == false )
                return;
            model . idx = _bll . Save ( model ,codePrefix );
            if ( model . idx > 0 )
            {
                XtraMessageBox . Show ( "成功保存" );
                state = codePrefix = string . Empty;
                this . DialogResult = DialogResult . OK;
            }
            else
                XtraMessageBox . Show ( "保存失败" );
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        public LineProductMesEntityu . MouldInformationEntity getModel
        {
            get
            {
                return model;
            }
        }

        private void txtMOI009_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            ChildForm . FormTPADEAInfo from = new FormTPADEAInfo ( "所用原料" ,tableDEA );
            from . StartPosition = FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                txtMOI009 . Text = from . getResult;
            }
        }

        private void txtMOI010_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            ChildForm . FormTPADEAInfo from = new FormTPADEAInfo ( "使用型号" ,tableDEA );
            from . StartPosition = FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                txtMOI010 . Text = from . getResult;
            }
        }

        private void txtMOI017_SelectedValueChanged ( object sender ,EventArgs e )
        {
            txtMOI018 . Properties . Items . Clear ( );
            if ( txtMOI017 . Text . Equals ( "五金模" ) )
            {
                txtMOI018 . Properties . Items . AddRange ( new string [ ] { "拉伸" ,"切边" ,"冲孔" ,"成型" ,"落料" ,"翻边" ,"翻孔" } );
                txtMOI005 . EditValue = "0501";
            }
            else if ( txtMOI017 . Text . Equals ( "注塑模" ) )
            {
                txtMOI018 . Properties . Items . AddRange ( new string [ ] { "面板" ,"箱体" ,"灯光板" ,"风轮" ,"风道" ,"灯体" ,"格栅" ,"出风口" ,"盖类零件" } );
                txtMOI005 . EditValue = "0502";
            }
        }

        private void txtMOI018_TextChanged ( object sender ,EventArgs e )
        {
            codePrefix = string . Empty;
            if ( txtMOI018 . Text != string . Empty && state . Equals ( "add" ) )
            {
                switch ( txtMOI018 . Text )
                {
                    case "拉伸":
                    codePrefix = "W-LS";
                    break;
                    case "切边":
                    codePrefix = "W-QB";
                    break;
                    case "冲孔":
                    codePrefix = "W-CK";
                    break;
                    case "成型":
                    codePrefix = "W-CX";
                    break;
                    case "落料":
                    codePrefix = "W-LL";
                    break;
                    case "翻边":
                    codePrefix = "W-FB";
                    break;
                    case "翻孔":
                    codePrefix = "W-FK";
                    break;
                    case "面板":
                    codePrefix = "Z-MB";
                    break;
                    case "箱体":
                    codePrefix = "Z-XT";
                    break;
                    case "灯光板":
                    codePrefix = "Z-DG";
                    break;
                    case "风轮":
                    codePrefix = "Z-FL";
                    break;
                    case "风道":
                    codePrefix = "Z-FD";
                    break;
                    case "灯体":
                    codePrefix = "Z-DT";
                    break;
                    case "格栅":
                    codePrefix = "Z-GS";
                    break;
                    case "出风口":
                    codePrefix = "Z-CF";
                    break;
                    case "盖类零件":
                    codePrefix = "Z-GL";
                    break;
                }
                if ( codePrefix != string . Empty )
                {
                    txtMOI001 . Text = _bll . getOddNum ( codePrefix );
                }
            }
        }

    }
}