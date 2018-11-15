namespace LineProductMes
{
    partial class FormSupplierForMould
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
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SFM001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM008 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.U0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SFM010 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copy = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1161, 364);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("宋体", 10.5F);
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SFM001,
            this.SFM002,
            this.SFM003,
            this.SFM004,
            this.SFM005,
            this.SFM006,
            this.SFM007,
            this.SFM008,
            this.SFM009,
            this.U0,
            this.SFM010});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // SFM001
            // 
            this.SFM001.Caption = "供应商编号";
            this.SFM001.FieldName = "SFM001";
            this.SFM001.Name = "SFM001";
            this.SFM001.Visible = true;
            this.SFM001.VisibleIndex = 0;
            this.SFM001.Width = 68;
            // 
            // SFM002
            // 
            this.SFM002.Caption = "供应商名称";
            this.SFM002.FieldName = "SFM002";
            this.SFM002.Name = "SFM002";
            this.SFM002.Visible = true;
            this.SFM002.VisibleIndex = 1;
            this.SFM002.Width = 144;
            // 
            // SFM003
            // 
            this.SFM003.Caption = "联系人";
            this.SFM003.FieldName = "SFM003";
            this.SFM003.Name = "SFM003";
            this.SFM003.Visible = true;
            this.SFM003.VisibleIndex = 3;
            this.SFM003.Width = 126;
            // 
            // SFM004
            // 
            this.SFM004.Caption = "联系电话";
            this.SFM004.FieldName = "SFM004";
            this.SFM004.Name = "SFM004";
            this.SFM004.Visible = true;
            this.SFM004.VisibleIndex = 4;
            this.SFM004.Width = 133;
            // 
            // SFM005
            // 
            this.SFM005.Caption = "地址省";
            this.SFM005.FieldName = "SFM005";
            this.SFM005.Name = "SFM005";
            this.SFM005.Width = 315;
            // 
            // SFM006
            // 
            this.SFM006.Caption = "注销";
            this.SFM006.FieldName = "SFM006";
            this.SFM006.Name = "SFM006";
            this.SFM006.Visible = true;
            this.SFM006.VisibleIndex = 6;
            this.SFM006.Width = 70;
            // 
            // SFM007
            // 
            this.SFM007.Caption = "地址市";
            this.SFM007.FieldName = "SFM007";
            this.SFM007.Name = "SFM007";
            // 
            // SFM008
            // 
            this.SFM008.Caption = "地址区";
            this.SFM008.FieldName = "SFM008";
            this.SFM008.Name = "SFM008";
            // 
            // SFM009
            // 
            this.SFM009.Caption = "地址";
            this.SFM009.FieldName = "SFM009";
            this.SFM009.Name = "SFM009";
            // 
            // U0
            // 
            this.U0.Caption = "地址";
            this.U0.FieldName = "U0";
            this.U0.Name = "U0";
            this.U0.Visible = true;
            this.U0.VisibleIndex = 5;
            this.U0.Width = 485;
            // 
            // SFM010
            // 
            this.SFM010.Caption = "类别";
            this.SFM010.FieldName = "SFM010";
            this.SFM010.Name = "SFM010";
            this.SFM010.Visible = true;
            this.SFM010.VisibleIndex = 2;
            this.SFM010.Width = 49;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // copy
            // 
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(100, 22);
            this.copy.Text = "复制";
            // 
            // FormSupplierForMould
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 390);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormSupplierForMould";
            this.Text = "模具供应商";
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn SFM001;
        private DevExpress . XtraGrid . Columns . GridColumn SFM002;
        private DevExpress . XtraGrid . Columns . GridColumn SFM003;
        private DevExpress . XtraGrid . Columns . GridColumn SFM004;
        private DevExpress . XtraGrid . Columns . GridColumn SFM005;
        private DevExpress . XtraGrid . Columns . GridColumn SFM006;
        private DevExpress . XtraGrid . Columns . GridColumn SFM007;
        private DevExpress . XtraGrid . Columns . GridColumn SFM008;
        private DevExpress . XtraGrid . Columns . GridColumn SFM009;
        private DevExpress . XtraGrid . Columns . GridColumn U0;
        private DevExpress . XtraGrid . Columns . GridColumn SFM010;
        private System . Windows . Forms . ContextMenuStrip contextMenuStrip1;
        private System . Windows . Forms . ToolStripMenuItem copy;
    }
}