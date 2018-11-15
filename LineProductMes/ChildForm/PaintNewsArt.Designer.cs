namespace LineProductMes . ChildForm
{
    partial class PaintNewsArt
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
            this.ART011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ART002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ART003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ART004 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(610, 344);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ART011,
            this.ART002,
            this.ART003,
            this.ART004});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // ART011
            // 
            this.ART011.Caption = "序号";
            this.ART011.FieldName = "ART011";
            this.ART011.Name = "ART011";
            this.ART011.Visible = true;
            this.ART011.VisibleIndex = 0;
            this.ART011.Width = 146;
            // 
            // ART002
            // 
            this.ART002.Caption = "工艺编号";
            this.ART002.FieldName = "ART002";
            this.ART002.Name = "ART002";
            this.ART002.Visible = true;
            this.ART002.VisibleIndex = 1;
            this.ART002.Width = 220;
            // 
            // ART003
            // 
            this.ART003.Caption = "工艺名称";
            this.ART003.FieldName = "ART003";
            this.ART003.Name = "ART003";
            this.ART003.Visible = true;
            this.ART003.VisibleIndex = 2;
            this.ART003.Width = 403;
            // 
            // ART004
            // 
            this.ART004.Caption = "工艺单价";
            this.ART004.DisplayFormat.FormatString = "0.####";
            this.ART004.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.ART004.FieldName = "ART004";
            this.ART004.Name = "ART004";
            this.ART004.Visible = true;
            this.ART004.VisibleIndex = 3;
            this.ART004.Width = 274;
            // 
            // PaintNewsArt
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 344);
            this.Controls.Add(this.gridControl1);
            this.Name = "PaintNewsArt";
            this.Text = "工艺信息";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn ART011;
        private DevExpress . XtraGrid . Columns . GridColumn ART002;
        private DevExpress . XtraGrid . Columns . GridColumn ART003;
        private DevExpress . XtraGrid . Columns . GridColumn ART004;
    }
}