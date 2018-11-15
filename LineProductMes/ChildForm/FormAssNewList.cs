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

namespace LineProductMes . ChildForm
{
    public partial class FormAssNewList :FormBaseChild
    {
        LineProductMesBll.Bll.AssNewWorkEnclosureBll _bll=null;

        public FormAssNewList ( DataTable table )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkEnclosureBll ( );
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

        public DataRow getDataRow
        {
            get
            {
                return row;
            }
        }

    }
}