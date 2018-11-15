using System . Collections . Generic;
using LineProductMes . ClassForMain;
using System;
using DevExpress . XtraEditors;

namespace LineProductMes . ChildForm
{
    public partial class FormSupplierMould :FormBaseChild
    {
        List<LineProductMesEntityu . PUAEntity> modelList=null;
        LineProductMesEntityu.SupplierForMouldEntity model=null;
        LineProductMesBll.Bll.SupplierForMouldBll _bll=null;

        public FormSupplierMould ( LineProductMesEntityu . SupplierForMouldEntity entity )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . SupplierForMouldBll ( );
            model = new LineProductMesEntityu . SupplierForMouldEntity ( );

            getPUA ( );
            model = entity;
            if ( model != null && model . idx > 0 )
                setValue ( );
            else
                txtSFM001 . Text = _bll . getOddNum ( );
        }

        void getPUA ( )
        {
            modelList = JsonUtils . JsonStringToVar<LineProductMesEntityu . PUAEntity> ( JsonUtils . ReadJson ( ) );
            txtSFM005 . Properties . Items . Clear ( );
            foreach ( LineProductMesEntityu . PUAEntity entity in modelList )
            {
                txtSFM005 . Properties . Items . Add ( entity . name );
            }
        }

        void setValue ( )
        {
            txtSFM001 . Text = model . SFM001;
            txtSFM002 . Text = model . SFM002;
            txtSFM003 . Text = model . SFM003;
            txtSFM004 . Text = model . SFM004;
            txtSFM005 . Text = model . SFM005;
            txtSFM007 . Text = model . SFM007;
            txtSFM008 . Text = model . SFM008;
            txtSFM009 . Text = model . SFM009;
            txtSFM010 . Text = model . SFM010;
            txtSFM001 . Tag = model . idx;
        }

        private void txtSFM005_SelectedValueChanged ( object sender ,System . EventArgs e )
        {
            txtSFM007 . Properties . Items . Clear ( );
            txtSFM007 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtSFM005 . Text ) )
                return;
            if ( modelList == null )
                return;
            List<LineProductMesEntityu . PUAEntity> modelCity = modelList . FindAll ( t =>
              {
                  return t . name == txtSFM005 . Text;
              } );
            if ( modelCity == null )
                return;
            foreach ( LineProductMesEntityu . City entity in modelCity [ 0 ] . city )
            {
                txtSFM007 . Properties . Items . Add ( entity . name );
            }
        }
        private void txtSFM007_SelectedValueChanged ( object sender ,System . EventArgs e )
        {
            txtSFM008 . Properties . Items . Clear ( );
            txtSFM008 . Text = string . Empty;
            if ( string . IsNullOrEmpty ( txtSFM005 . Text ) )
                return;
            if ( modelList == null )
                return;
            List<LineProductMesEntityu . PUAEntity> modelCity = modelList . FindAll ( t =>
            {
                return t . name == txtSFM005 . Text;
            } );
            if ( modelCity == null )
                return;
            List<LineProductMesEntityu . City> entityCity = modelCity [ 0 ] . city;
            if ( entityCity == null )
                return;
            List<LineProductMesEntityu . City> citys = entityCity . FindAll ( t =>
            {
                return t . name == txtSFM007 . Text;
            } );
            if ( citys == null )
                return;
            foreach ( string item in citys [ 0 ] . area )
            {
                txtSFM008 . Properties . Items . Add ( item );
            }
        }

        bool getValue ( )
        {
            dxErrorProvider1 . ClearErrors ( );
            if ( string . IsNullOrEmpty ( txtSFM002 . Text ) )
            {
                dxErrorProvider1 . SetError ( txtSFM002 ,"不可为空" );
                return false;
            }
            model = new LineProductMesEntityu . SupplierForMouldEntity ( );
            model . SFM001 = txtSFM001 . Text;
            model . SFM002 = txtSFM002 . Text;
            model . SFM003 = txtSFM003 . Text;
            model . SFM004 = txtSFM004 . Text;
            model . SFM005 = txtSFM005 . Text;
            model . SFM006 = false;
            model . SFM007 = txtSFM007 . Text;
            model . SFM008 = txtSFM008 . Text;
            model . SFM009 = txtSFM009 . Text;
            model . SFM010 = txtSFM010 . Text;
            model . idx = txtSFM001 . Tag == null ? 0 : Convert . ToInt32 ( txtSFM001 . Tag );

            return true;
        }
        private void simpleButton2_Click ( object sender ,System . EventArgs e )
        {
            if ( getValue ( ) == false )
                return;
            model . idx = _bll . Save ( model );
            if ( model . idx > 0 )
            {
                XtraMessageBox . Show ( "成功保存" );
                this . DialogResult = System . Windows . Forms . DialogResult . OK;
            }
            else
                XtraMessageBox . Show ( "保存失败" );
        }
        private void simpleButton1_Click ( object sender ,System . EventArgs e )
        {
            this . DialogResult = System . Windows . Forms . DialogResult . Cancel;
        }

        public LineProductMesEntityu . SupplierForMouldEntity getModel
        {
            get
            {
                return model;
            }
        }

    }
}