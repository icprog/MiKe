namespace LineProductMes . ChildForm
{
    partial class FormUserChoise
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
            this.EMP001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EMP002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EMP007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EMP005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DAA002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EMP023 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(781, 365);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.EMP001,
            this.EMP002,
            this.EMP007,
            this.EMP005,
            this.DAA002,
            this.EMP023});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // EMP001
            // 
            this.EMP001.Caption = "工号";
            this.EMP001.FieldName = "EMP001";
            this.EMP001.Name = "EMP001";
            this.EMP001.Visible = true;
            this.EMP001.VisibleIndex = 0;
            // 
            // EMP002
            // 
            this.EMP002.Caption = "姓名";
            this.EMP002.FieldName = "EMP002";
            this.EMP002.Name = "EMP002";
            this.EMP002.Visible = true;
            this.EMP002.VisibleIndex = 1;
            // 
            // EMP007
            // 
            this.EMP007.Caption = "岗位";
            this.EMP007.FieldName = "EMP007";
            this.EMP007.Name = "EMP007";
            this.EMP007.Visible = true;
            this.EMP007.VisibleIndex = 2;
            // 
            // EMP005
            // 
            this.EMP005.Caption = "班组编号";
            this.EMP005.FieldName = "EMP005";
            this.EMP005.Name = "EMP005";
            this.EMP005.Visible = true;
            this.EMP005.VisibleIndex = 3;
            // 
            // DAA002
            // 
            this.DAA002.Caption = "班组";
            this.DAA002.FieldName = "DAA002";
            this.DAA002.Name = "DAA002";
            this.DAA002.Visible = true;
            this.DAA002.VisibleIndex = 4;
            // 
            // EMP023
            // 
            this.EMP023.Caption = "入职时间";
            this.EMP023.FieldName = "EMP023";
            this.EMP023.Name = "EMP023";
            this.EMP023.Visible = true;
            this.EMP023.VisibleIndex = 5;
            // 
            // FormUserChoise
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 365);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormUserChoise";
            this.Text = "人员信息";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn EMP001;
        private DevExpress . XtraGrid . Columns . GridColumn EMP002;
        private DevExpress . XtraGrid . Columns . GridColumn EMP007;
        private DevExpress . XtraGrid . Columns . GridColumn EMP005;
        private DevExpress . XtraGrid . Columns . GridColumn DAA002;
        private DevExpress . XtraGrid . Columns . GridColumn EMP023;
    }
}