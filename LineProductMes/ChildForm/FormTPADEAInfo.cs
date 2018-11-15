using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormTPADEAInfo :FormBaseChild
    {
        string result=string.Empty;
        
        public FormTPADEAInfo ( string text ,DataTable tableView)
        {
            InitializeComponent ( );

            this . Text = text;
            //if ( text . Equals ( "所用原料" ) )
            //    DEA057 . Visible = false;

            this . gridControl1 . DataSource = tableView;

            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );
        }

        private void btnOk_Click ( object sender ,EventArgs e )
        {
            int [ ] rows = gridView1 . GetSelectedRows ( );
            if ( rows . Length < 1 )
            {
                XtraMessageBox . Show ( "请选择" );
                return;
            }

            result = string . Empty;

            DataRow row;
            foreach ( int i in rows )
            {
                row = gridView1 . GetDataRow ( i );
                if ( this . Text . Equals ( "所用原料" ) )
                {
                    if ( row != null )
                    {
                        if ( string . IsNullOrEmpty ( result ) )
                            result = row [ "DEA001" ] + "," + row [ "DEA002" ];
                        else
                            result += "|" + row [ "DEA001" ] + "," + row [ "DEA002" ];
                    }
                }
                else
                {
                    if ( row != null )
                    {
                        if ( string . IsNullOrEmpty ( result ) )
                            result = row [ "DEA001" ] + "," + row [ "DEA002" ] + "," + row [ "DEA057" ];
                        else
                            result += "|" + row [ "DEA001" ] + "," + row [ "DEA002" ] + "," + row [ "DEA057" ];
                    }
                }
            }

            if ( string . IsNullOrEmpty ( result ) )
                return;
            this . DialogResult = DialogResult . OK;
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        public string getResult
        {
            get
            {
                return result;
            }
        }

    }
}
