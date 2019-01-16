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
    public partial class FormAssNewList :FormBaseChild
    {
        LineProductMesBll.Bll.AssNewWorkEnclosureBll _bll=null;
        
        public FormAssNewList ( DataTable table )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkEnclosureBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1  } );
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
                return;
            this . DialogResult = DialogResult . OK;
        }
        
        public DataRow getDataRow
        {
            get
            {
                return row;
            }
        }

    }
}