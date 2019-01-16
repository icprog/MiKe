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
using DevExpress . Utils . Paint;
using System . Reflection;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormUserChoise :FormBase
    {
        public FormUserChoise ( DataTable table )
        {
            InitializeComponent ( );

            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );

            gridControl1 . DataSource = table;
            gridView1 . BestFitColumns ( );
        }

        DataRow row;

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择人员信息" );
                return;
            }
            this . DialogResult = DialogResult . OK;
        }

        public DataRow getRow
        {
            get
            {
                return row;
            }
        }

    }
}