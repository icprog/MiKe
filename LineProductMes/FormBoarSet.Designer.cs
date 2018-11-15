namespace LineProductMes
{
    partial class FormBoarSet
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
            this.BOS001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Box = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.BOS002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BOS003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BOS004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BOS005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Box,
            this.repositoryItemDateEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1237, 412);
            this.gridControl1.TabIndex = 4;
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
            this.BOS001,
            this.BOS002,
            this.BOS003,
            this.BOS004,
            this.BOS005});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // BOS001
            // 
            this.BOS001.Caption = "看板车间";
            this.BOS001.ColumnEdit = this.Box;
            this.BOS001.FieldName = "BOS001";
            this.BOS001.Name = "BOS001";
            this.BOS001.OptionsColumn.AllowEdit = false;
            this.BOS001.Visible = true;
            this.BOS001.VisibleIndex = 0;
            // 
            // Box
            // 
            this.Box.AutoHeight = false;
            this.Box.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Box.Items.AddRange(new object[] {
            "组装车间",
            "五金车间",
            "注塑车间",
            "LED车间",
            "面板车间",
            "喷漆车间"});
            this.Box.Name = "Box";
            this.Box.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // BOS002
            // 
            this.BOS002.Caption = "计划天数(天)";
            this.BOS002.FieldName = "BOS002";
            this.BOS002.Name = "BOS002";
            this.BOS002.Visible = true;
            this.BOS002.VisibleIndex = 1;
            // 
            // BOS003
            // 
            this.BOS003.Caption = "数据切换(分)";
            this.BOS003.FieldName = "BOS003";
            this.BOS003.Name = "BOS003";
            this.BOS003.Visible = true;
            this.BOS003.VisibleIndex = 2;
            // 
            // BOS004
            // 
            this.BOS004.Caption = "每页显示(行)";
            this.BOS004.FieldName = "BOS004";
            this.BOS004.Name = "BOS004";
            this.BOS004.Visible = true;
            this.BOS004.VisibleIndex = 3;
            // 
            // BOS005
            // 
            this.BOS005.Caption = "显示日期";
            this.BOS005.ColumnEdit = this.repositoryItemDateEdit1;
            this.BOS005.FieldName = "BOS005";
            this.BOS005.Name = "BOS005";
            this.BOS005.Visible = true;
            this.BOS005.VisibleIndex = 4;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "yyyy-MM-dd";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM-dd";
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // FormBoarSet
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 438);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormBoarSet";
            this.Text = "车间看板显示自定义设置";
            this.Load += new System.EventHandler(this.FormBoarSet_Load);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn BOS001;
        private DevExpress . XtraGrid . Columns . GridColumn BOS002;
        private DevExpress . XtraGrid . Columns . GridColumn BOS003;
        private DevExpress . XtraGrid . Columns . GridColumn BOS004;
        private DevExpress . XtraEditors . Repository . RepositoryItemComboBox Box;
        private DevExpress . XtraGrid . Columns . GridColumn BOS005;
        private DevExpress . XtraEditors . Repository . RepositoryItemDateEdit repositoryItemDateEdit1;
    }
}