using System;
using System . Data;

namespace LineProductMes . ChildForm
{
    public partial class PaintNewsArt :FormBaseChild
    {
        DataRow row;

        public PaintNewsArt ( string text,DataTable table )
        {
            InitializeComponent ( );

            this . Text = this . Text + "   " + "品号:" + text;
            gridControl1 . DataSource = table;
        }

        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row != null )
                this . DialogResult = System . Windows . Forms . DialogResult . OK;
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
