namespace LineProductMes . ChildForm
{
    partial class FormReadOrder
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IBB001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB013 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IBB010 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.U0 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(969, 350);
            this.splitContainerControl1.SplitterPosition = 52;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(969, 52);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(95, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(141, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(969, 52);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnOk;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(99, 32);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(129, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(99, 32);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(99, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(30, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(228, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(721, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(969, 286);
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
            this.IBB001,
            this.IBB002,
            this.IBB003,
            this.IBB004,
            this.IBB013,
            this.IBB006,
            this.IBB011,
            this.IBB007,
            this.IBB009,
            this.IBB010,
            this.U0});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 35;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // IBB001
            // 
            this.IBB001.Caption = "订单号";
            this.IBB001.FieldName = "IBB001";
            this.IBB001.Name = "IBB001";
            this.IBB001.OptionsColumn.AllowEdit = false;
            this.IBB001.Visible = true;
            this.IBB001.VisibleIndex = 1;
            this.IBB001.Width = 82;
            // 
            // IBB002
            // 
            this.IBB002.Caption = "序号";
            this.IBB002.FieldName = "IBB002";
            this.IBB002.Name = "IBB002";
            this.IBB002.OptionsColumn.AllowEdit = false;
            this.IBB002.Visible = true;
            this.IBB002.VisibleIndex = 2;
            this.IBB002.Width = 82;
            // 
            // IBB003
            // 
            this.IBB003.Caption = "品号";
            this.IBB003.FieldName = "IBB003";
            this.IBB003.Name = "IBB003";
            this.IBB003.OptionsColumn.AllowEdit = false;
            this.IBB003.Visible = true;
            this.IBB003.VisibleIndex = 4;
            this.IBB003.Width = 82;
            // 
            // IBB004
            // 
            this.IBB004.Caption = "品名";
            this.IBB004.FieldName = "IBB004";
            this.IBB004.Name = "IBB004";
            this.IBB004.OptionsColumn.AllowEdit = false;
            this.IBB004.Visible = true;
            this.IBB004.VisibleIndex = 3;
            this.IBB004.Width = 82;
            // 
            // IBB013
            // 
            this.IBB013.Caption = "交货日期";
            this.IBB013.FieldName = "IBB013";
            this.IBB013.Name = "IBB013";
            this.IBB013.OptionsColumn.AllowEdit = false;
            this.IBB013.Visible = true;
            this.IBB013.VisibleIndex = 5;
            this.IBB013.Width = 82;
            // 
            // IBB006
            // 
            this.IBB006.Caption = "订单数量";
            this.IBB006.FieldName = "IBB006";
            this.IBB006.Name = "IBB006";
            this.IBB006.OptionsColumn.AllowEdit = false;
            this.IBB006.Visible = true;
            this.IBB006.VisibleIndex = 6;
            this.IBB006.Width = 82;
            // 
            // IBB011
            // 
            this.IBB011.Caption = "未排数量";
            this.IBB011.FieldName = "IBB011";
            this.IBB011.Name = "IBB011";
            this.IBB011.OptionsColumn.AllowEdit = false;
            this.IBB011.Visible = true;
            this.IBB011.VisibleIndex = 7;
            this.IBB011.Width = 82;
            // 
            // IBB007
            // 
            this.IBB007.Caption = "开始日期";
            this.IBB007.FieldName = "IBB007";
            this.IBB007.Name = "IBB007";
            this.IBB007.Visible = true;
            this.IBB007.VisibleIndex = 8;
            this.IBB007.Width = 82;
            // 
            // IBB009
            // 
            this.IBB009.Caption = "日产量";
            this.IBB009.FieldName = "IBB009";
            this.IBB009.Name = "IBB009";
            this.IBB009.Visible = true;
            this.IBB009.VisibleIndex = 10;
            this.IBB009.Width = 103;
            // 
            // IBB010
            // 
            this.IBB010.Caption = "排产数量";
            this.IBB010.FieldName = "IBB010";
            this.IBB010.Name = "IBB010";
            this.IBB010.Visible = true;
            this.IBB010.VisibleIndex = 11;
            this.IBB010.Width = 118;
            // 
            // U0
            // 
            this.U0.Caption = "排差";
            this.U0.FieldName = "U0";
            this.U0.Name = "U0";
            this.U0.OptionsColumn.AllowEdit = false;
            this.U0.Visible = true;
            this.U0.VisibleIndex = 9;
            this.U0.Width = 36;
            // 
            // FormReadOrder
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 350);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormReadOrder";
            this.Text = "订单信息";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraLayout . LayoutControl layoutControl1;
        private DevExpress . XtraLayout . LayoutControlGroup layoutControlGroup1;
        private DevExpress . XtraEditors . SimpleButton btnOk;
        private DevExpress . XtraEditors . SimpleButton btnCancel;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem2;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem1;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem2;
        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn IBB001;
        private DevExpress . XtraGrid . Columns . GridColumn IBB002;
        private DevExpress . XtraGrid . Columns . GridColumn IBB003;
        private DevExpress . XtraGrid . Columns . GridColumn IBB013;
        private DevExpress . XtraGrid . Columns . GridColumn IBB006;
        private DevExpress . XtraGrid . Columns . GridColumn IBB007;
        private DevExpress . XtraGrid . Columns . GridColumn IBB009;
        private DevExpress . XtraGrid . Columns . GridColumn IBB010;
        private DevExpress . XtraGrid . Columns . GridColumn IBB004;
        private DevExpress . XtraGrid . Columns . GridColumn IBB011;
        private DevExpress . XtraGrid . Columns . GridColumn U0;
    }
}