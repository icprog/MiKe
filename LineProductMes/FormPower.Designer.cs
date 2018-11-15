namespace LineProductMes
{
    partial class FormPower
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
            this.EMP002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW013 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW008 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW016 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW017 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW010 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.POW012 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.idx = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCl = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1238, 378);
            this.gridControl1.TabIndex = 1;
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
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.EMP002,
            this.POW002,
            this.FOR004,
            this.POW003,
            this.POW013,
            this.POW004,
            this.POW005,
            this.POW006,
            this.POW007,
            this.POW008,
            this.POW009,
            this.POW016,
            this.POW017,
            this.POW010,
            this.POW011,
            this.POW012,
            this.idx});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 45;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // EMP002
            // 
            this.EMP002.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.EMP002.AppearanceCell.Options.UseFont = true;
            this.EMP002.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.EMP002.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.EMP002.AppearanceHeader.Options.UseBackColor = true;
            this.EMP002.AppearanceHeader.Options.UseFont = true;
            this.EMP002.Caption = "人员姓名";
            this.EMP002.FieldName = "EMP002";
            this.EMP002.Name = "EMP002";
            this.EMP002.OptionsColumn.AllowEdit = false;
            this.EMP002.Visible = true;
            this.EMP002.VisibleIndex = 0;
            this.EMP002.Width = 72;
            // 
            // POW002
            // 
            this.POW002.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW002.AppearanceCell.Options.UseFont = true;
            this.POW002.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW002.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW002.AppearanceHeader.Options.UseBackColor = true;
            this.POW002.AppearanceHeader.Options.UseFont = true;
            this.POW002.Caption = "人员编号";
            this.POW002.FieldName = "POW002";
            this.POW002.Name = "POW002";
            this.POW002.OptionsColumn.AllowEdit = false;
            this.POW002.Visible = true;
            this.POW002.VisibleIndex = 1;
            this.POW002.Width = 63;
            // 
            // FOR004
            // 
            this.FOR004.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FOR004.AppearanceCell.Options.UseFont = true;
            this.FOR004.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.FOR004.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FOR004.AppearanceHeader.Options.UseBackColor = true;
            this.FOR004.AppearanceHeader.Options.UseFont = true;
            this.FOR004.Caption = "程序名称";
            this.FOR004.FieldName = "FOR004";
            this.FOR004.Name = "FOR004";
            this.FOR004.OptionsColumn.AllowEdit = false;
            this.FOR004.Visible = true;
            this.FOR004.VisibleIndex = 2;
            this.FOR004.Width = 148;
            // 
            // POW003
            // 
            this.POW003.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW003.AppearanceCell.Options.UseFont = true;
            this.POW003.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW003.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW003.AppearanceHeader.Options.UseBackColor = true;
            this.POW003.AppearanceHeader.Options.UseFont = true;
            this.POW003.Caption = "程序编号";
            this.POW003.FieldName = "POW003";
            this.POW003.Name = "POW003";
            this.POW003.OptionsColumn.AllowEdit = false;
            this.POW003.Visible = true;
            this.POW003.VisibleIndex = 3;
            this.POW003.Width = 138;
            // 
            // POW013
            // 
            this.POW013.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW013.AppearanceCell.Options.UseFont = true;
            this.POW013.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW013.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW013.AppearanceHeader.Options.UseBackColor = true;
            this.POW013.AppearanceHeader.Options.UseFont = true;
            this.POW013.Caption = "运行";
            this.POW013.FieldName = "POW013";
            this.POW013.Name = "POW013";
            this.POW013.Visible = true;
            this.POW013.VisibleIndex = 4;
            this.POW013.Width = 55;
            // 
            // POW004
            // 
            this.POW004.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW004.AppearanceCell.Options.UseFont = true;
            this.POW004.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW004.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW004.AppearanceHeader.Options.UseBackColor = true;
            this.POW004.AppearanceHeader.Options.UseFont = true;
            this.POW004.Caption = "查询";
            this.POW004.FieldName = "POW004";
            this.POW004.Name = "POW004";
            this.POW004.Visible = true;
            this.POW004.VisibleIndex = 5;
            this.POW004.Width = 52;
            // 
            // POW005
            // 
            this.POW005.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW005.AppearanceCell.Options.UseFont = true;
            this.POW005.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW005.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW005.AppearanceHeader.Options.UseBackColor = true;
            this.POW005.AppearanceHeader.Options.UseFont = true;
            this.POW005.Caption = "新增";
            this.POW005.FieldName = "POW005";
            this.POW005.Name = "POW005";
            this.POW005.Visible = true;
            this.POW005.VisibleIndex = 6;
            this.POW005.Width = 50;
            // 
            // POW006
            // 
            this.POW006.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW006.AppearanceCell.Options.UseFont = true;
            this.POW006.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW006.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW006.AppearanceHeader.Options.UseBackColor = true;
            this.POW006.AppearanceHeader.Options.UseFont = true;
            this.POW006.Caption = "删除";
            this.POW006.FieldName = "POW006";
            this.POW006.Name = "POW006";
            this.POW006.Visible = true;
            this.POW006.VisibleIndex = 7;
            this.POW006.Width = 54;
            // 
            // POW007
            // 
            this.POW007.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW007.AppearanceCell.Options.UseFont = true;
            this.POW007.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW007.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW007.AppearanceHeader.Options.UseBackColor = true;
            this.POW007.AppearanceHeader.Options.UseFont = true;
            this.POW007.Caption = "编辑";
            this.POW007.FieldName = "POW007";
            this.POW007.Name = "POW007";
            this.POW007.Visible = true;
            this.POW007.VisibleIndex = 8;
            this.POW007.Width = 51;
            // 
            // POW008
            // 
            this.POW008.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW008.AppearanceCell.Options.UseFont = true;
            this.POW008.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW008.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW008.AppearanceHeader.Options.UseBackColor = true;
            this.POW008.AppearanceHeader.Options.UseFont = true;
            this.POW008.Caption = "送审";
            this.POW008.FieldName = "POW008";
            this.POW008.Name = "POW008";
            this.POW008.Width = 45;
            // 
            // POW009
            // 
            this.POW009.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW009.AppearanceCell.Options.UseFont = true;
            this.POW009.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW009.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW009.AppearanceHeader.Options.UseBackColor = true;
            this.POW009.AppearanceHeader.Options.UseFont = true;
            this.POW009.Caption = "审核";
            this.POW009.FieldName = "POW009";
            this.POW009.Name = "POW009";
            this.POW009.Visible = true;
            this.POW009.VisibleIndex = 9;
            this.POW009.Width = 48;
            // 
            // POW016
            // 
            this.POW016.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW016.AppearanceCell.Options.UseFont = true;
            this.POW016.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW016.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW016.AppearanceHeader.Options.UseBackColor = true;
            this.POW016.AppearanceHeader.Options.UseFont = true;
            this.POW016.Caption = "打印";
            this.POW016.FieldName = "POW016";
            this.POW016.Name = "POW016";
            this.POW016.Visible = true;
            this.POW016.VisibleIndex = 10;
            this.POW016.Width = 46;
            // 
            // POW017
            // 
            this.POW017.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW017.AppearanceCell.Options.UseFont = true;
            this.POW017.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW017.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW017.AppearanceHeader.Options.UseBackColor = true;
            this.POW017.AppearanceHeader.Options.UseFont = true;
            this.POW017.Caption = "导出";
            this.POW017.FieldName = "POW017";
            this.POW017.Name = "POW017";
            this.POW017.Visible = true;
            this.POW017.VisibleIndex = 11;
            this.POW017.Width = 49;
            // 
            // POW010
            // 
            this.POW010.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW010.AppearanceCell.Options.UseFont = true;
            this.POW010.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW010.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW010.AppearanceHeader.Options.UseBackColor = true;
            this.POW010.AppearanceHeader.Options.UseFont = true;
            this.POW010.Caption = "保存";
            this.POW010.FieldName = "POW010";
            this.POW010.Name = "POW010";
            this.POW010.Visible = true;
            this.POW010.VisibleIndex = 12;
            this.POW010.Width = 48;
            // 
            // POW011
            // 
            this.POW011.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW011.AppearanceCell.Options.UseFont = true;
            this.POW011.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW011.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW011.AppearanceHeader.Options.UseBackColor = true;
            this.POW011.AppearanceHeader.Options.UseFont = true;
            this.POW011.Caption = "取消";
            this.POW011.FieldName = "POW011";
            this.POW011.Name = "POW011";
            this.POW011.Visible = true;
            this.POW011.VisibleIndex = 13;
            this.POW011.Width = 44;
            // 
            // POW012
            // 
            this.POW012.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW012.AppearanceCell.Options.UseFont = true;
            this.POW012.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.POW012.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.POW012.AppearanceHeader.Options.UseBackColor = true;
            this.POW012.AppearanceHeader.Options.UseFont = true;
            this.POW012.Caption = "注销";
            this.POW012.FieldName = "POW012";
            this.POW012.Name = "POW012";
            this.POW012.Visible = true;
            this.POW012.VisibleIndex = 14;
            this.POW012.Width = 86;
            // 
            // idx
            // 
            this.idx.Caption = "编号";
            this.idx.FieldName = "idx";
            this.idx.Name = "idx";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(358, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 14);
            this.labelControl2.TabIndex = 31;
            this.labelControl2.Text = "程序信息：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(49, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 14);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "人员信息：";
            // 
            // btnCl
            // 
            this.btnCl.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnCl.Appearance.Options.UseFont = true;
            this.btnCl.Location = new System.Drawing.Point(611, 5);
            this.btnCl.Name = "btnCl";
            this.btnCl.Size = new System.Drawing.Size(75, 23);
            this.btnCl.TabIndex = 29;
            this.btnCl.Text = "清除";
            this.btnCl.Click += new System.EventHandler(this.btnCl_Click_1);
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.Location = new System.Drawing.Point(434, 6);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lookUpEdit2.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lookUpEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.lookUpEdit2.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit2.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.Transparent;
            this.lookUpEdit2.Properties.AppearanceDropDown.Options.UseBackColor = true;
            this.lookUpEdit2.Properties.AppearanceDropDownHeader.BackColor = System.Drawing.Color.Transparent;
            this.lookUpEdit2.Properties.AppearanceDropDownHeader.Options.UseBackColor = true;
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FOR003", 100, "编号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FOR004", 100, "名称")});
            this.lookUpEdit2.Properties.NullText = "";
            this.lookUpEdit2.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lookUpEdit2.Properties.PopupFormMinSize = new System.Drawing.Size(300, 0);
            this.lookUpEdit2.Properties.PopupWidth = 220;
            this.lookUpEdit2.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.lookUpEdit2.Size = new System.Drawing.Size(148, 20);
            this.lookUpEdit2.TabIndex = 3;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(130, 6);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lookUpEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.Transparent;
            this.lookUpEdit1.Properties.AppearanceDropDown.Options.UseBackColor = true;
            this.lookUpEdit1.Properties.AppearanceDropDownHeader.BackColor = System.Drawing.Color.Transparent;
            this.lookUpEdit1.Properties.AppearanceDropDownHeader.Options.UseBackColor = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EMP001", 35, "编号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EMP002", 55, "姓名")});
            this.lookUpEdit1.Properties.LookAndFeel.SkinMaskColor = System.Drawing.Color.WhiteSmoke;
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lookUpEdit1.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.lookUpEdit1.Size = new System.Drawing.Size(148, 20);
            this.lookUpEdit1.TabIndex = 2;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 26);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.labelControl2);
            this.splitContainerControl2.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl2.Panel1.Controls.Add(this.lookUpEdit1);
            this.splitContainerControl2.Panel1.Controls.Add(this.btnCl);
            this.splitContainerControl2.Panel1.Controls.Add(this.lookUpEdit2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1238, 426);
            this.splitContainerControl2.SplitterPosition = 36;
            this.splitContainerControl2.TabIndex = 4;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // FormPower
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 452);
            this.Controls.Add(this.splitContainerControl2);
            this.Name = "FormPower";
            this.Text = "权限设置";
            this.Controls.SetChildIndex(this.splitContainerControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn EMP002;
        private DevExpress . XtraGrid . Columns . GridColumn POW002;
        private DevExpress . XtraGrid . Columns . GridColumn FOR004;
        private DevExpress . XtraGrid . Columns . GridColumn POW003;
        private DevExpress . XtraGrid . Columns . GridColumn POW004;
        private DevExpress . XtraGrid . Columns . GridColumn POW005;
        private DevExpress . XtraGrid . Columns . GridColumn POW006;
        private DevExpress . XtraGrid . Columns . GridColumn POW007;
        private DevExpress . XtraGrid . Columns . GridColumn POW008;
        private DevExpress . XtraGrid . Columns . GridColumn POW009;
        private DevExpress . XtraGrid . Columns . GridColumn POW010;
        private DevExpress . XtraGrid . Columns . GridColumn POW011;
        private DevExpress . XtraGrid . Columns . GridColumn POW012;
        private DevExpress . XtraEditors . LookUpEdit lookUpEdit1;
        private DevExpress . XtraEditors . LookUpEdit lookUpEdit2;
        private DevExpress . XtraGrid . Columns . GridColumn idx;
        private DevExpress . XtraGrid . Columns . GridColumn POW013;
        private DevExpress . XtraGrid . Columns . GridColumn POW016;
        private DevExpress . XtraGrid . Columns . GridColumn POW017;
        private DevExpress . XtraEditors . SimpleButton btnCl;
        private DevExpress . XtraEditors . LabelControl labelControl1;
        private DevExpress . XtraEditors . LabelControl labelControl2;
        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl2;
    }
}