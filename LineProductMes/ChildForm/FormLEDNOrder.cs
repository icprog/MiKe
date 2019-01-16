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
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormLEDNOrder :FormBase
    {
        public FormLEDNOrder ( DataTable table )
        {
            InitializeComponent ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView2 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView2 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            gridControl2 . DataSource = table;
            gridView2 . BestFitColumns ( );
        }
        DataRow row;
        private void gridView2_DoubleClick ( object sender ,EventArgs e )
        {
            row = gridView2 . GetFocusedDataRow ( );
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择来源工单" );
                return;
            }
            if ( string . IsNullOrEmpty ( row [ "RAA008" ] . ToString ( ) ) )
                return;
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