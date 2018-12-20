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
using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormCheckLine :FormBase
    {
        DataTable table;
        public FormCheckLine (DataTable table )
        {
            InitializeComponent ( );

            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            this . table = table;
            gridControl1 . DataSource = this . table;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void btnSure_Click ( object sender ,EventArgs e )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );
            this . DialogResult = DialogResult . OK;
        }

        public DataTable getTable
        {
            get
            {
                return this . table;
            }
        }

    }
}