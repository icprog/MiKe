namespace LineProductMes
{
    partial class FormInjectionBoard
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PRG001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA057 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRF3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRF003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PRG003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ANW09 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.PRF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LEH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1240, 477);
            this.splitContainerControl1.SplitterPosition = 241;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemProgressBar2});
            this.gridControl1.Size = new System.Drawing.Size(1240, 224);
            this.gridControl1.TabIndex = 2;
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
            this.PRG001,
            this.DEA002,
            this.DEA057,
            this.PRF3,
            this.PRF003,
            this.PRG003,
            this.ANW9,
            this.ANW009,
            this.ANW09,
            this.PRF,
            this.LEH});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            this.gridView1.OptionsCustomization.CustomizationFormSnapMode = DevExpress.Utils.Controls.SnapMode.OwnerControl;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsFilter.ColumnFilterPopupMode = DevExpress.XtraGrid.Columns.ColumnFilterPopupMode.Excel;
            this.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // PRG001
            // 
            this.PRG001.Caption = "品号";
            this.PRG001.FieldName = "PRG001";
            this.PRG001.Name = "PRG001";
            this.PRG001.Visible = true;
            this.PRG001.VisibleIndex = 0;
            this.PRG001.Width = 98;
            // 
            // DEA002
            // 
            this.DEA002.Caption = "品名";
            this.DEA002.FieldName = "DEA002";
            this.DEA002.Name = "DEA002";
            this.DEA002.Visible = true;
            this.DEA002.VisibleIndex = 1;
            this.DEA002.Width = 98;
            // 
            // DEA057
            // 
            this.DEA057.Caption = "规格";
            this.DEA057.FieldName = "DEA057";
            this.DEA057.Name = "DEA057";
            this.DEA057.Visible = true;
            this.DEA057.VisibleIndex = 2;
            this.DEA057.Width = 98;
            // 
            // PRF3
            // 
            this.PRF3.Caption = "计划余量";
            this.PRF3.FieldName = "PRF3";
            this.PRF3.Name = "PRF3";
            this.PRF3.Visible = true;
            this.PRF3.VisibleIndex = 3;
            // 
            // PRF003
            // 
            this.PRF003.Caption = "总计划量";
            this.PRF003.FieldName = "PRF003";
            this.PRF003.Name = "PRF003";
            this.PRF003.Visible = true;
            this.PRF003.VisibleIndex = 4;
            this.PRF003.Width = 98;
            // 
            // PRG003
            // 
            this.PRG003.Caption = "当日计划量";
            this.PRG003.DisplayFormat.FormatString = "0.######";
            this.PRG003.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.PRG003.FieldName = "PRG003";
            this.PRG003.Name = "PRG003";
            this.PRG003.Visible = true;
            this.PRG003.VisibleIndex = 6;
            this.PRG003.Width = 98;
            // 
            // ANW9
            // 
            this.ANW9.Caption = "当日完成量";
            this.ANW9.FieldName = "ANW9";
            this.ANW9.Name = "ANW9";
            this.ANW9.Visible = true;
            this.ANW9.VisibleIndex = 7;
            this.ANW9.Width = 98;
            // 
            // ANW009
            // 
            this.ANW009.Caption = "当日完成率";
            this.ANW009.ColumnEdit = this.repositoryItemProgressBar1;
            this.ANW009.FieldName = "ANW009";
            this.ANW009.Name = "ANW009";
            this.ANW009.Visible = true;
            this.ANW009.VisibleIndex = 8;
            this.ANW009.Width = 98;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar1.Step = 1;
            // 
            // ANW09
            // 
            this.ANW09.Caption = "总完成率";
            this.ANW09.ColumnEdit = this.repositoryItemProgressBar2;
            this.ANW09.FieldName = "ANW09";
            this.ANW09.Name = "ANW09";
            this.ANW09.Visible = true;
            this.ANW09.VisibleIndex = 9;
            this.ANW09.Width = 98;
            // 
            // repositoryItemProgressBar2
            // 
            this.repositoryItemProgressBar2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.repositoryItemProgressBar2.Name = "repositoryItemProgressBar2";
            this.repositoryItemProgressBar2.ShowTitle = true;
            this.repositoryItemProgressBar2.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.repositoryItemProgressBar2.Step = 1;
            // 
            // PRF
            // 
            this.PRF.Caption = "总欠产量";
            this.PRF.FieldName = "PRF";
            this.PRF.Name = "PRF";
            this.PRF.Visible = true;
            this.PRF.VisibleIndex = 10;
            this.PRF.Width = 98;
            // 
            // LEH
            // 
            this.LEH.Caption = "总完成量";
            this.LEH.FieldName = "LEH";
            this.LEH.Name = "LEH";
            this.LEH.Visible = true;
            this.LEH.VisibleIndex = 5;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1240, 241);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartControl1
            // 
            this.chartControl1.DataBindings = null;
            xyDiagram1.AxisX.GridLines.MinorVisible = true;
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.Interlaced = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.GridLines.MinorVisible = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(12, 12);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "SeriesOne";
            sideBySideBarSeriesView1.ColorEach = true;
            series1.View = sideBySideBarSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.Size = new System.Drawing.Size(1216, 217);
            this.chartControl1.TabIndex = 4;
            chartTitle1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartTitle1.Text = "产品欠产";
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1240, 241);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chartControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1220, 221);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FormInjectionBoard
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 477);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormInjectionBoard";
            this.Text = "注塑车间看板";
            this.Load += new System.EventHandler(this.FormInjectionBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn PRG001;
        private DevExpress . XtraGrid . Columns . GridColumn DEA002;
        private DevExpress . XtraGrid . Columns . GridColumn DEA057;
        private DevExpress . XtraGrid . Columns . GridColumn PRF003;
        private DevExpress . XtraGrid . Columns . GridColumn PRG003;
        private DevExpress . XtraGrid . Columns . GridColumn ANW9;
        private DevExpress . XtraGrid . Columns . GridColumn ANW009;
        private DevExpress . XtraEditors . Repository . RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress . XtraGrid . Columns . GridColumn ANW09;
        private DevExpress . XtraEditors . Repository . RepositoryItemProgressBar repositoryItemProgressBar2;
        private DevExpress . XtraGrid . Columns . GridColumn PRF;
        private DevExpress . XtraLayout . LayoutControl layoutControl1;
        private DevExpress . XtraCharts . ChartControl chartControl1;
        private DevExpress . XtraLayout . LayoutControlGroup layoutControlGroup1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem1;
        private DevExpress . XtraGrid . Columns . GridColumn LEH;
        private DevExpress . XtraGrid . Columns . GridColumn PRF3;
    }
}