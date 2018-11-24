namespace LineProductMes
{
    partial class FormProductPlan
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtPRD001 = new DevExpress.XtraEditors.TextEdit();
            this.txtPRD002 = new DevExpress.XtraEditors.TextEdit();
            this.ReaderOrder = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btnGener = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemOne = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PRE002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE008 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.idx = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE010 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRE012 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.wait = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PRE = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPRD001.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPRD002.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 26);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1237, 412);
            this.splitContainerControl1.SplitterPosition = 75;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtPRD001);
            this.layoutControl1.Controls.Add(this.txtPRD002);
            this.layoutControl1.Controls.Add(this.ReaderOrder);
            this.layoutControl1.Controls.Add(this.pictureEdit1);
            this.layoutControl1.Controls.Add(this.btnGener);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1237, 75);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtPRD001
            // 
            this.txtPRD001.Location = new System.Drawing.Point(63, 12);
            this.txtPRD001.MenuManager = this.barManager1;
            this.txtPRD001.Name = "txtPRD001";
            this.txtPRD001.Properties.ReadOnly = true;
            this.txtPRD001.Size = new System.Drawing.Size(161, 20);
            this.txtPRD001.StyleController = this.layoutControl1;
            this.txtPRD001.TabIndex = 4;
            // 
            // txtPRD002
            // 
            this.txtPRD002.Location = new System.Drawing.Point(63, 36);
            this.txtPRD002.MenuManager = this.barManager1;
            this.txtPRD002.Name = "txtPRD002";
            this.txtPRD002.Properties.ReadOnly = true;
            this.txtPRD002.Size = new System.Drawing.Size(161, 20);
            this.txtPRD002.StyleController = this.layoutControl1;
            this.txtPRD002.TabIndex = 5;
            // 
            // ReaderOrder
            // 
            this.ReaderOrder.Location = new System.Drawing.Point(228, 12);
            this.ReaderOrder.Name = "ReaderOrder";
            this.ReaderOrder.Size = new System.Drawing.Size(83, 22);
            this.ReaderOrder.StyleController = this.layoutControl1;
            this.ReaderOrder.TabIndex = 6;
            this.ReaderOrder.Text = "读取订单";
            this.ReaderOrder.Click += new System.EventHandler(this.ReaderOrder_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.Location = new System.Drawing.Point(1055, 12);
            this.pictureEdit1.MenuManager = this.barManager1;
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(170, 51);
            this.pictureEdit1.StyleController = this.layoutControl1;
            this.pictureEdit1.TabIndex = 7;
            // 
            // btnGener
            // 
            this.btnGener.Location = new System.Drawing.Point(228, 38);
            this.btnGener.Name = "btnGener";
            this.btnGener.Size = new System.Drawing.Size(83, 22);
            this.btnGener.StyleController = this.layoutControl1;
            this.btnGener.TabIndex = 8;
            this.btnGener.Text = "生成ERP计划";
            this.btnGener.Click += new System.EventHandler(this.btnGener_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1237, 75);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtPRD001;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(216, 24);
            this.layoutControlItem1.Text = "单号";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(574, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(469, 55);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(303, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(271, 55);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ReaderOrder;
            this.layoutControlItem3.Location = new System.Drawing.Point(216, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtPRD002;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(216, 31);
            this.layoutControlItem2.Text = "制单日期";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.pictureEdit1;
            this.layoutControlItem4.Location = new System.Drawing.Point(1043, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(174, 55);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnGener;
            this.layoutControlItem5.Location = new System.Drawing.Point(216, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(87, 29);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1237, 325);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridControl1_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemOne,
            this.ItemTwo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 48);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // ItemOne
            // 
            this.ItemOne.Name = "ItemOne";
            this.ItemOne.Size = new System.Drawing.Size(112, 22);
            this.ItemOne.Text = "复制行";
            // 
            // ItemTwo
            // 
            this.ItemTwo.Name = "ItemTwo";
            this.ItemTwo.Size = new System.Drawing.Size(112, 22);
            this.ItemTwo.Text = "复制";
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PRE002,
            this.PRE003,
            this.PRE004,
            this.PRE005,
            this.PRE006,
            this.PRE007,
            this.PRE008,
            this.idx,
            this.PRE010,
            this.PRE009,
            this.PRE001,
            this.PRE011,
            this.PRE012,
            this.PRE});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanging);
            this.gridView1.MouseEnter += new System.EventHandler(this.gridView1_MouseEnter);
            // 
            // PRE002
            // 
            this.PRE002.Caption = "订单号";
            this.PRE002.FieldName = "PRE002";
            this.PRE002.Name = "PRE002";
            this.PRE002.OptionsColumn.AllowEdit = false;
            this.PRE002.Visible = true;
            this.PRE002.VisibleIndex = 0;
            this.PRE002.Width = 102;
            // 
            // PRE003
            // 
            this.PRE003.Caption = "序号";
            this.PRE003.FieldName = "PRE003";
            this.PRE003.Name = "PRE003";
            this.PRE003.OptionsColumn.AllowEdit = false;
            this.PRE003.Visible = true;
            this.PRE003.VisibleIndex = 1;
            this.PRE003.Width = 102;
            // 
            // PRE004
            // 
            this.PRE004.Caption = "品号";
            this.PRE004.FieldName = "PRE004";
            this.PRE004.Name = "PRE004";
            this.PRE004.OptionsColumn.AllowEdit = false;
            this.PRE004.Visible = true;
            this.PRE004.VisibleIndex = 2;
            this.PRE004.Width = 102;
            // 
            // PRE005
            // 
            this.PRE005.Caption = "计划日期";
            this.PRE005.FieldName = "PRE005";
            this.PRE005.Name = "PRE005";
            this.PRE005.Visible = true;
            this.PRE005.VisibleIndex = 3;
            this.PRE005.Width = 102;
            // 
            // PRE006
            // 
            this.PRE006.Caption = "交货日期";
            this.PRE006.FieldName = "PRE006";
            this.PRE006.Name = "PRE006";
            this.PRE006.OptionsColumn.AllowEdit = false;
            this.PRE006.Visible = true;
            this.PRE006.VisibleIndex = 4;
            this.PRE006.Width = 102;
            // 
            // PRE007
            // 
            this.PRE007.Caption = "订单数量";
            this.PRE007.FieldName = "PRE007";
            this.PRE007.Name = "PRE007";
            this.PRE007.OptionsColumn.AllowEdit = false;
            this.PRE007.Visible = true;
            this.PRE007.VisibleIndex = 5;
            this.PRE007.Width = 70;
            // 
            // PRE008
            // 
            this.PRE008.Caption = "排产数量";
            this.PRE008.FieldName = "PRE008";
            this.PRE008.Name = "PRE008";
            this.PRE008.Visible = true;
            this.PRE008.VisibleIndex = 8;
            this.PRE008.Width = 116;
            // 
            // idx
            // 
            this.idx.Caption = "编号";
            this.idx.FieldName = "idx";
            this.idx.Name = "idx";
            // 
            // PRE010
            // 
            this.PRE010.Caption = "日排量";
            this.PRE010.FieldName = "PRE010";
            this.PRE010.Name = "PRE010";
            this.PRE010.OptionsColumn.AllowEdit = false;
            this.PRE010.Visible = true;
            this.PRE010.VisibleIndex = 7;
            this.PRE010.Width = 56;
            // 
            // PRE009
            // 
            this.PRE009.Caption = "ERP计划量";
            this.PRE009.FieldName = "PRE009";
            this.PRE009.Name = "PRE009";
            this.PRE009.OptionsColumn.AllowEdit = false;
            this.PRE009.Visible = true;
            this.PRE009.VisibleIndex = 10;
            this.PRE009.Width = 70;
            // 
            // PRE001
            // 
            this.PRE001.Caption = "单号";
            this.PRE001.FieldName = "PRE001";
            this.PRE001.Name = "PRE001";
            // 
            // PRE011
            // 
            this.PRE011.Caption = "调整量";
            this.PRE011.FieldName = "PRE011";
            this.PRE011.Name = "PRE011";
            this.PRE011.Visible = true;
            this.PRE011.VisibleIndex = 9;
            this.PRE011.Width = 54;
            // 
            // PRE012
            // 
            this.PRE012.Caption = "ERP计划单号";
            this.PRE012.FieldName = "PRE012";
            this.PRE012.Name = "PRE012";
            this.PRE012.OptionsColumn.AllowEdit = false;
            this.PRE012.Visible = true;
            this.PRE012.VisibleIndex = 11;
            this.PRE012.Width = 119;
            // 
            // wait
            // 
            this.wait.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.wait.Appearance.Options.UseBackColor = true;
            this.wait.BarAnimationElementThickness = 2;
            this.wait.Caption = "请稍后";
            this.wait.Description = "数据加载中 ...";
            this.wait.Location = new System.Drawing.Point(495, 186);
            this.wait.LookAndFeel.UseDefaultLookAndFeel = false;
            this.wait.Name = "wait";
            this.wait.Size = new System.Drawing.Size(246, 66);
            this.wait.TabIndex = 38;
            this.wait.Text = "progressPanel1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // PRE
            // 
            this.PRE.Caption = "已排量";
            this.PRE.FieldName = "PRE";
            this.PRE.Name = "PRE";
            this.PRE.OptionsColumn.AllowEdit = false;
            this.PRE.Visible = true;
            this.PRE.VisibleIndex = 6;
            this.PRE.Width = 64;
            // 
            // FormProductPlan
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 438);
            this.Controls.Add(this.wait);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormProductPlan";
            this.Text = "成品主计划";
            this.Load += new System.EventHandler(this.FormProductPlan_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            this.Controls.SetChildIndex(this.wait, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPRD001.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPRD002.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraLayout . LayoutControl layoutControl1;
        private DevExpress . XtraEditors . TextEdit txtPRD001;
        private DevExpress . XtraEditors . TextEdit txtPRD002;
        private DevExpress . XtraLayout . LayoutControlGroup layoutControlGroup1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem2;
        private DevExpress . XtraEditors . SimpleButton ReaderOrder;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem3;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem1;
        private DevExpress . XtraGrid . Columns . GridColumn PRE002;
        private DevExpress . XtraGrid . Columns . GridColumn PRE003;
        private DevExpress . XtraGrid . Columns . GridColumn PRE004;
        private DevExpress . XtraGrid . Columns . GridColumn PRE005;
        private DevExpress . XtraGrid . Columns . GridColumn PRE006;
        private DevExpress . XtraGrid . Columns . GridColumn PRE007;
        private DevExpress . XtraGrid . Columns . GridColumn PRE008;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem2;
        private DevExpress . XtraGrid . Columns . GridColumn idx;
        private DevExpress . XtraGrid . Columns . GridColumn PRE010;
        private DevExpress . XtraWaitForm . ProgressPanel wait;
        private System . ComponentModel . BackgroundWorker backgroundWorker1;
        private DevExpress . XtraEditors . PictureEdit pictureEdit1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem4;
        private DevExpress . XtraEditors . SimpleButton btnGener;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem5;
        private DevExpress . XtraGrid . Columns . GridColumn PRE009;
        private System . Windows . Forms . ContextMenuStrip contextMenuStrip1;
        private System . Windows . Forms . ToolStripMenuItem ItemOne;
        private DevExpress . XtraGrid . Columns . GridColumn PRE001;
        private DevExpress . XtraGrid . Columns . GridColumn PRE011;
        private DevExpress . XtraGrid . Columns . GridColumn PRE012;
        private System . Windows . Forms . ToolStripMenuItem ItemTwo;
        private DevExpress . XtraGrid . Columns . GridColumn PRE;
    }
}