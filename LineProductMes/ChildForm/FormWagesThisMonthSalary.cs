using System;
using System . Data;
using System . Reflection;
using DevExpress . Utils . Paint;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormWagesThisMonthSalary :FormBaseChild
    {
        LineProductMesBll.Bll.WagesBll _bll=null;

        DataTable tableView;

        public FormWagesThisMonthSalary ( DateTime dt )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . WagesBll ( );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );

            this . Text = this . Text + "(" + dt . ToString ( "yyyy-MM" ) + ")";
            tableView = _bll . getTableView ( dt );
            gridControl1 . DataSource = tableView;
        }

    }
}