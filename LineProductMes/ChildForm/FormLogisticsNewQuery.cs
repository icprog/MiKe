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
using System . Reflection;
using Utility;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormLogisticsNewQuery :FormBaseChild
    {
        public FormLogisticsNewQuery ( DataTable table )
        {
            InitializeComponent ( );

            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            
            gridControl1 . DataSource = table;
        }

        DataRow row;

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
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