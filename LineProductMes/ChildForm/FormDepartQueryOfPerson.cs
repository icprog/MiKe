using DevExpress . Utils . Paint;
using System;
using System . Reflection;
using System . Data;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormDepartQueryOfPerson :FormBaseChild
    {
        public FormDepartQueryOfPerson ( )
        {
            InitializeComponent ( );

            Utility . GridViewMoHuSelect . SetFilter ( gridView1 );

            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

        }

        private void FormDepartQueryOfPerson_Load ( object sender ,EventArgs e )
        {
            //LineProductMesBll . Bll . DepartMentBll _bll = new LineProductMesBll . Bll . DepartMentBll ( );
            //DataTable dt = _bll . GetDataTable ( );
            //this . gridControl1 . DataSource = dt;
        }

        //public LineProductMesEntityu . DepartMentEntity GetModel
        //{
        //    get
        //    {
        //        int num = 0;
        //        num = gridView1 . FocusedRowHandle;
        //        if ( num >= 0 && num <= gridView1 . DataRowCount - 1 )
        //        {
        //            CarpenterEntity . DepartMentEntity _model = new CarpenterEntity . DepartMentEntity ( );
        //            _model . DEP001 = gridView1 . GetDataRow ( num ) [ "DEP001" ] . ToString ( );
        //            _model . DEP002 = gridView1 . GetDataRow ( num ) [ "DEP002" ] . ToString ( );
        //            return _model;
        //        }
        //        else
        //            return null;
        //    }
        //}

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            int num = 0;
            num = gridView1 . FocusedRowHandle;
            if ( num >= 0 && num <= gridView1 . DataRowCount - 1 )
            {
                this . DialogResult = System . Windows . Forms . DialogResult . OK;
            }
        }
    }
}
