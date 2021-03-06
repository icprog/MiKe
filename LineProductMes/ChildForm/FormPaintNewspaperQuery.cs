﻿using DevExpress . Utils . Paint;
using LineProductMes . ClassForMain;
using System;
using System . Data;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes . ChildForm
{
    public partial class FormPaintNewspaperQuery :FormBaseChild
    {
        LineProductMesBll.Bll.PaintNewspaperBll _bll;
        DataTable tableView;
        string oddNum=string.Empty,strWhere=string.Empty;
        
        public FormPaintNewspaperQuery ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . PaintNewspaperBll ( );
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,View1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Func<string> funStr = InitData;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( UI ) ,null );
        }

        string InitData ( )
        {
            if ( strWhere == string . Empty )
                strWhere = "1=1";
            tableView = _bll . tableViewQuery ( strWhere );

            return string . Empty;
        }
        
        delegate void AysnUpdateUI ( );
        void UI ( IAsyncResult result )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AysnUpdateUI ( delegate ( )
                {
                    txtPAN001 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"PAN001" );
                    txtPAN001 . Properties . DisplayMember = "PAN001";
                    txtPAN001 . Properties . ValueMember = "PAN001";

                    txtPAO002 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"PAO002" );
                    txtPAO002 . Properties . DisplayMember = "PAO002";
                    txtPAO002 . Properties . ValueMember = "PAO002";

                    txtPAO003 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"PAO003" );
                    txtPAO003 . Properties . DisplayMember = "PAO003";
                    txtPAO003 . Properties . ValueMember = "PAO003";

                    txtPAO004 . Properties . DataSource = tableView . DefaultView . ToTable ( true ,"PAO004" );
                    txtPAO004 . Properties . DisplayMember = "PAO004";
                    txtPAO004 . Properties . ValueMember = "PAO004";

                } ) );
            }
        }

        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }

        private void btnQuery_Click ( object sender ,EventArgs e )
        {
             strWhere = "1=1";
            if ( !string . IsNullOrEmpty ( txtPAN001 . Text ) )
                strWhere += " AND PAN001='" + txtPAN001 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtPAO002 . Text ) )
                strWhere += " AND PAO002='" + txtPAO002 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtPAO003 . Text ) )
                strWhere += " AND PAO003='" + txtPAO003 . Text + "'";
            if ( !string . IsNullOrEmpty ( txtPAO004 . Text ) )
                strWhere += " AND PAO004='" + txtPAO004 . Text + "'";
            if ( !string . IsNullOrEmpty ( dateEdit1 . Text ) )
                strWhere += " AND PAN006='" + Convert . ToDateTime ( dateEdit1 . Text ) . ToString ( "yyyyMMdd" ) + "'";
            if ( !string . IsNullOrEmpty ( txtSa . Text ) )
                strWhere += " AND PAN013='" + txtSa . Text + "'";

            tableView = _bll . tableViewQuery ( strWhere );
            gridControl1 . DataSource = tableView;
        }

        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtPAN001 . EditValue = txtPAO002 . EditValue = txtPAO003 . EditValue = txtPAO004 . EditValue =dateEdit1.Text= null;
        }

        private void gridView1_DoubleClick ( object sender ,EventArgs e )
        {
            oddNum = gridView1 . GetFocusedRowCellValue ( "PAN001" ) . ToString ( );
            if ( string . IsNullOrEmpty ( oddNum ) )
                return;
            this . DialogResult = DialogResult . OK;
        }

        public string getOdd
        {
            get
            {
                return oddNum;
            }
        }

    }
}
