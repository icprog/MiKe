namespace LineProductMes . ChildForm
{
    partial class FormAssNewList
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
            this.ANW001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RAA001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RAA015 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW013 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW012 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ART003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ART004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RAA008 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(974, 365);
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
            this.ANW001,
            this.RAA001,
            this.RAA015,
            this.DEA002,
            this.ANW012,
            this.ANW013,
            this.ANW009,
            this.ART003,
            this.ART004,
            this.RAA008});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // ANW001
            // 
            this.ANW001.Caption = "组装单号";
            this.ANW001.FieldName = "ANW001";
            this.ANW001.Name = "ANW001";
            this.ANW001.Visible = true;
            this.ANW001.VisibleIndex = 0;
            this.ANW001.Width = 128;
            // 
            // RAA001
            // 
            this.RAA001.Caption = "来源工单号";
            this.RAA001.FieldName = "RAA001";
            this.RAA001.Name = "RAA001";
            this.RAA001.Visible = true;
            this.RAA001.VisibleIndex = 1;
            this.RAA001.Width = 172;
            // 
            // RAA015
            // 
            this.RAA015.Caption = "品号";
            this.RAA015.FieldName = "RAA015";
            this.RAA015.Name = "RAA015";
            this.RAA015.Visible = true;
            this.RAA015.VisibleIndex = 2;
            this.RAA015.Width = 111;
            // 
            // DEA002
            // 
            this.DEA002.Caption = "品名";
            this.DEA002.FieldName = "DEA002";
            this.DEA002.Name = "DEA002";
            this.DEA002.Visible = true;
            this.DEA002.VisibleIndex = 3;
            this.DEA002.Width = 127;
            // 
            // ANW013
            // 
            this.ANW013.Caption = "班组";
            this.ANW013.FieldName = "ANW013";
            this.ANW013.Name = "ANW013";
            this.ANW013.Visible = true;
            this.ANW013.VisibleIndex = 6;
            this.ANW013.Width = 54;
            // 
            // ANW012
            // 
            this.ANW012.Caption = "班组编号";
            this.ANW012.FieldName = "ANW012";
            this.ANW012.Name = "ANW012";
            this.ANW012.Visible = true;
            this.ANW012.VisibleIndex = 5;
            this.ANW012.Width = 58;
            // 
            // ANW009
            // 
            this.ANW009.Caption = "数量";
            this.ANW009.FieldName = "ANW009";
            this.ANW009.Name = "ANW009";
            this.ANW009.Visible = true;
            this.ANW009.VisibleIndex = 4;
            this.ANW009.Width = 106;
            // 
            // ART003
            // 
            this.ART003.Caption = "附件";
            this.ART003.FieldName = "ART003";
            this.ART003.Name = "ART003";
            this.ART003.Visible = true;
            this.ART003.VisibleIndex = 7;
            this.ART003.Width = 144;
            // 
            // ART004
            // 
            this.ART004.Caption = "附件单价";
            this.ART004.FieldName = "ART004";
            this.ART004.Name = "ART004";
            this.ART004.Visible = true;
            this.ART004.VisibleIndex = 8;
            this.ART004.Width = 100;
            // 
            // RAA008
            // 
            this.RAA008.Caption = "开工日期";
            this.RAA008.FieldName = "RAA008";
            this.RAA008.Name = "RAA008";
            this.RAA008.Visible = true;
            this.RAA008.VisibleIndex = 9;
            this.RAA008.Width = 78;
            // 
            // FormAssNewList
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 365);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormAssNewList";
            this.Text = "组装报工单查询";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn ANW001;
        private DevExpress . XtraGrid . Columns . GridColumn RAA001;
        private DevExpress . XtraGrid . Columns . GridColumn RAA015;
        private DevExpress . XtraGrid . Columns . GridColumn ANW013;
        private DevExpress . XtraGrid . Columns . GridColumn ANW009;
        private DevExpress . XtraGrid . Columns . GridColumn ANW012;
        private DevExpress . XtraGrid . Columns . GridColumn DEA002;
        private DevExpress . XtraGrid . Columns . GridColumn ART003;
        private DevExpress . XtraGrid . Columns . GridColumn ART004;
        private DevExpress . XtraGrid . Columns . GridColumn RAA008;
    }
}