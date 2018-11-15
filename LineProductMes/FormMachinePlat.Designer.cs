namespace LineProductMes
{
    partial class FormMachinePlat
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
            this.MAP003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.idx = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.worker = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copy = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.worker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
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
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.worker});
            this.gridControl1.Size = new System.Drawing.Size(1012, 412);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MAP003,
            this.MAP004,
            this.MAP001,
            this.MAP002,
            this.MAP005,
            this.MAP006,
            this.idx,
            this.MAP007});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // MAP003
            // 
            this.MAP003.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP003.AppearanceCell.Options.UseFont = true;
            this.MAP003.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP003.AppearanceHeader.Options.UseFont = true;
            this.MAP003.Caption = "车间编号";
            this.MAP003.FieldName = "MAP003";
            this.MAP003.Name = "MAP003";
            // 
            // MAP004
            // 
            this.MAP004.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP004.AppearanceCell.Options.UseFont = true;
            this.MAP004.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP004.AppearanceHeader.Options.UseFont = true;
            this.MAP004.Caption = "车间";
            this.MAP004.FieldName = "MAP004";
            this.MAP004.Name = "MAP004";
            this.MAP004.Visible = true;
            this.MAP004.VisibleIndex = 0;
            this.MAP004.Width = 119;
            // 
            // MAP001
            // 
            this.MAP001.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP001.AppearanceCell.Options.UseFont = true;
            this.MAP001.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP001.AppearanceHeader.Options.UseFont = true;
            this.MAP001.Caption = "机台编号";
            this.MAP001.FieldName = "MAP001";
            this.MAP001.Name = "MAP001";
            this.MAP001.Visible = true;
            this.MAP001.VisibleIndex = 1;
            this.MAP001.Width = 105;
            // 
            // MAP002
            // 
            this.MAP002.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP002.AppearanceCell.Options.UseFont = true;
            this.MAP002.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP002.AppearanceHeader.Options.UseFont = true;
            this.MAP002.Caption = "机台名称";
            this.MAP002.FieldName = "MAP002";
            this.MAP002.Name = "MAP002";
            this.MAP002.Visible = true;
            this.MAP002.VisibleIndex = 2;
            this.MAP002.Width = 199;
            // 
            // MAP005
            // 
            this.MAP005.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP005.AppearanceCell.Options.UseFont = true;
            this.MAP005.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP005.AppearanceHeader.Options.UseFont = true;
            this.MAP005.Caption = "机台规格";
            this.MAP005.FieldName = "MAP005";
            this.MAP005.Name = "MAP005";
            this.MAP005.Visible = true;
            this.MAP005.VisibleIndex = 3;
            this.MAP005.Width = 194;
            // 
            // MAP006
            // 
            this.MAP006.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP006.AppearanceCell.Options.UseFont = true;
            this.MAP006.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP006.AppearanceHeader.Options.UseFont = true;
            this.MAP006.Caption = "备注";
            this.MAP006.FieldName = "MAP006";
            this.MAP006.Name = "MAP006";
            this.MAP006.Visible = true;
            this.MAP006.VisibleIndex = 4;
            this.MAP006.Width = 417;
            // 
            // idx
            // 
            this.idx.Caption = "编号";
            this.idx.FieldName = "idx";
            this.idx.Name = "idx";
            // 
            // MAP007
            // 
            this.MAP007.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP007.AppearanceCell.Options.UseFont = true;
            this.MAP007.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MAP007.AppearanceHeader.Options.UseFont = true;
            this.MAP007.Caption = "注销";
            this.MAP007.FieldName = "MAP007";
            this.MAP007.Name = "MAP007";
            this.MAP007.Visible = true;
            this.MAP007.VisibleIndex = 5;
            this.MAP007.Width = 44;
            // 
            // worker
            // 
            this.worker.AutoHeight = false;
            this.worker.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.worker.Name = "worker";
            this.worker.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
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
            // FormMachinePlat
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 438);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormMachinePlat";
            this.Text = "机台信息";
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.worker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn MAP004;
        private DevExpress . XtraGrid . Columns . GridColumn MAP001;
        private DevExpress . XtraGrid . Columns . GridColumn MAP002;
        private DevExpress . XtraGrid . Columns . GridColumn MAP005;
        private DevExpress . XtraGrid . Columns . GridColumn MAP006;
        private DevExpress . XtraGrid . Columns . GridColumn MAP003;
        private DevExpress . XtraEditors . Repository . RepositoryItemGridLookUpEdit worker;
        private DevExpress . XtraGrid . Views . Grid . GridView repositoryItemGridLookUpEdit1View;
        private DevExpress . XtraGrid . Columns . GridColumn idx;
        private DevExpress . XtraGrid . Columns . GridColumn MAP007;
        private System . Windows . Forms . ContextMenuStrip contextMenuStrip1;
        private System . Windows . Forms . ToolStripMenuItem copy;
    }
}