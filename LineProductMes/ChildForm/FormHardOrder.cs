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
    public partial class FormHardOrder :FormBase
    {
        public FormHardOrder ( DataTable table )
        {
            InitializeComponent ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            gridControl1 . DataSource = table;
            gridView1 . BestFitColumns ( );
        }


        DataRow row;
        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
            {
                XtraMessageBox . Show ( "请选择来源工单" );
                return;
            }
            //if ( string . IsNullOrEmpty ( row [ "RAA008" ] . ToString ( ) ) )
            //    return;
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