namespace LineProductMes . ChildForm
{
    partial class FormLogisticsNewQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components . Dispose ( );
            }
            base . Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.KEB001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KEB002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KEB003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KEB004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KEB007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KEB010 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ARS011 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(882, 373);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(225)))));
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(225)))));
            this.gridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(225)))));
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.KEB001,
            this.KEB002,
            this.KEB003,
            this.KEB004,
            this.KEB007,
            this.KEB010,
            this.ARS011});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // KEB001
            // 
            this.KEB001.Caption = "销货单号";
            this.KEB001.FieldName = "KEB001";
            this.KEB001.Name = "KEB001";
            this.KEB001.Visible = true;
            this.KEB001.VisibleIndex = 0;
            // 
            // KEB002
            // 
            this.KEB002.Caption = "序号";
            this.KEB002.FieldName = "KEB002";
            this.KEB002.Name = "KEB002";
            this.KEB002.Visible = true;
            this.KEB002.VisibleIndex = 1;
            // 
            // KEB003
            // 
            this.KEB003.Caption = "品号";
            this.KEB003.FieldName = "KEB003";
            this.KEB003.Name = "KEB003";
            this.KEB003.Visible = true;
            this.KEB003.VisibleIndex = 2;
            // 
            // KEB004
            // 
            this.KEB004.Caption = "品名";
            this.KEB004.FieldName = "KEB004";
            this.KEB004.Name = "KEB004";
            this.KEB004.Visible = true;
            this.KEB004.VisibleIndex = 3;
            // 
            // KEB007
            // 
            this.KEB007.Caption = "数量";
            this.KEB007.FieldName = "KEB007";
            this.KEB007.Name = "KEB007";
            this.KEB007.Visible = true;
            this.KEB007.VisibleIndex = 4;
            // 
            // KEB010
            // 
            this.KEB010.Caption = "单价";
            this.KEB010.FieldName = "KEB010";
            this.KEB010.Name = "KEB010";
            this.KEB010.Visible = true;
            this.KEB010.VisibleIndex = 5;
            // 
            // ARS011
            // 
            this.ARS011.Caption = "体积";
            this.ARS011.FieldName = "ARS011";
            this.ARS011.Name = "ARS011";
            this.ARS011.Visible = true;
            this.ARS011.VisibleIndex = 6;
            // 
            // FormLogisticsNewQuery
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 373);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormLogisticsNewQuery";
            this.Text = "销货单数据查询";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn KEB001;
        private DevExpress . XtraGrid . Columns . GridColumn KEB002;
        private DevExpress . XtraGrid . Columns . GridColumn KEB003;
        private DevExpress . XtraGrid . Columns . GridColumn KEB004;
        private DevExpress . XtraGrid . Columns . GridColumn KEB007;
        private DevExpress . XtraGrid . Columns . GridColumn KEB010;
        private DevExpress . XtraGrid . Columns . GridColumn ARS011;
    }
}