namespace LineProductMes
{
    partial class FormPurProductPlan
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
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.SEP001 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP002 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP003 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP004 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP005 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP006 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP009 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.U0 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP010 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP011 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.SEP012 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.SEP001,
            this.SEP002,
            this.SEP003,
            this.SEP004,
            this.SEP005,
            this.SEP006,
            this.SEP009,
            this.U0,
            this.SEP010,
            this.SEP011,
            this.SEP012});
            this.treeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 24);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.PopulateServiceColumns = true;
            this.treeList1.OptionsView.ShowAutoFilterRow = true;
            this.treeList1.Size = new System.Drawing.Size(1237, 414);
            this.treeList1.TabIndex = 5;
            this.treeList1.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.treeList1_CustomDrawNodeIndicator);
            // 
            // SEP001
            // 
            this.SEP001.Caption = "品号";
            this.SEP001.FieldName = "SEP001";
            this.SEP001.Name = "SEP001";
            this.SEP001.Visible = true;
            this.SEP001.VisibleIndex = 0;
            this.SEP001.Width = 98;
            // 
            // SEP002
            // 
            this.SEP002.Caption = "品名";
            this.SEP002.FieldName = "SEP002";
            this.SEP002.Name = "SEP002";
            this.SEP002.Visible = true;
            this.SEP002.VisibleIndex = 1;
            this.SEP002.Width = 97;
            // 
            // SEP003
            // 
            this.SEP003.Caption = "规格";
            this.SEP003.FieldName = "SEP003";
            this.SEP003.Name = "SEP003";
            this.SEP003.Visible = true;
            this.SEP003.VisibleIndex = 2;
            this.SEP003.Width = 97;
            // 
            // SEP004
            // 
            this.SEP004.Caption = "计划日期";
            this.SEP004.FieldName = "SEP004";
            this.SEP004.Name = "SEP004";
            this.SEP004.Visible = true;
            this.SEP004.VisibleIndex = 6;
            this.SEP004.Width = 109;
            // 
            // SEP005
            // 
            this.SEP005.Caption = "订单数量";
            this.SEP005.FieldName = "SEP005";
            this.SEP005.Name = "SEP005";
            this.SEP005.Visible = true;
            this.SEP005.VisibleIndex = 7;
            this.SEP005.Width = 109;
            // 
            // SEP006
            // 
            this.SEP006.Caption = "计划数量";
            this.SEP006.FieldName = "SEP006";
            this.SEP006.Format.FormatString = "0.######";
            this.SEP006.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.SEP006.Name = "SEP006";
            this.SEP006.Visible = true;
            this.SEP006.VisibleIndex = 8;
            this.SEP006.Width = 109;
            // 
            // SEP009
            // 
            this.SEP009.Caption = "组成量";
            this.SEP009.FieldName = "SEP009";
            this.SEP009.Format.FormatString = "0.######";
            this.SEP009.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.SEP009.Name = "SEP009";
            this.SEP009.Visible = true;
            this.SEP009.VisibleIndex = 9;
            this.SEP009.Width = 109;
            // 
            // U0
            // 
            this.U0.Caption = "计划总量";
            this.U0.FieldName = "U0";
            this.U0.Format.FormatString = "0.######";
            this.U0.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.U0.Name = "U0";
            this.U0.ToolTip = "[组成量] * [计划数量]";
            this.U0.UnboundExpression = "[SEP009] * [SEP006]";
            this.U0.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Decimal;
            this.U0.Visible = true;
            this.U0.VisibleIndex = 10;
            this.U0.Width = 109;
            // 
            // SEP010
            // 
            this.SEP010.Caption = "主要供应商";
            this.SEP010.FieldName = "SEP010";
            this.SEP010.Name = "SEP010";
            this.SEP010.Visible = true;
            this.SEP010.VisibleIndex = 3;
            this.SEP010.Width = 97;
            // 
            // SEP011
            // 
            this.SEP011.Caption = "仓库";
            this.SEP011.FieldName = "SEP011";
            this.SEP011.Name = "SEP011";
            this.SEP011.Visible = true;
            this.SEP011.VisibleIndex = 4;
            this.SEP011.Width = 97;
            // 
            // SEP012
            // 
            this.SEP012.Caption = "单位";
            this.SEP012.FieldName = "SEP012";
            this.SEP012.Name = "SEP012";
            this.SEP012.Visible = true;
            this.SEP012.VisibleIndex = 5;
            this.SEP012.Width = 36;
            // 
            // FormPurProductPlan
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 438);
            this.Controls.Add(this.treeList1);
            this.Name = "FormPurProductPlan";
            this.Text = "采购计划";
            this.Load += new System.EventHandler(this.FormPurProductPlan_Load);
            this.Controls.SetChildIndex(this.treeList1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraTreeList . TreeList treeList1;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP001;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP002;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP003;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP004;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP005;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP006;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP009;
        private DevExpress . XtraTreeList . Columns . TreeListColumn U0;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP010;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP011;
        private DevExpress . XtraTreeList . Columns . TreeListColumn SEP012;
    }
}