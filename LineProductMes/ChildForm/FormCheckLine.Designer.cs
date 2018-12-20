namespace LineProductMes . ChildForm
{
    partial class FormCheckLine
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
            this.F1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.F10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSure = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(799, 321);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.F1,
            this.F2,
            this.F3,
            this.F4,
            this.F5,
            this.F6,
            this.F7,
            this.F8,
            this.F9,
            this.F10});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // F1
            // 
            this.F1.Caption = "品号";
            this.F1.FieldName = "F1";
            this.F1.Name = "F1";
            this.F1.OptionsColumn.AllowEdit = false;
            this.F1.Visible = true;
            this.F1.VisibleIndex = 0;
            this.F1.Width = 166;
            // 
            // F2
            // 
            this.F2.Caption = "品名";
            this.F2.FieldName = "F2";
            this.F2.Name = "F2";
            this.F2.OptionsColumn.AllowEdit = false;
            this.F2.Visible = true;
            this.F2.VisibleIndex = 1;
            this.F2.Width = 496;
            // 
            // F3
            // 
            this.F3.Caption = "1线";
            this.F3.FieldName = "F3";
            this.F3.Name = "F3";
            this.F3.Visible = true;
            this.F3.VisibleIndex = 2;
            this.F3.Width = 49;
            // 
            // F4
            // 
            this.F4.Caption = "2线";
            this.F4.FieldName = "F4";
            this.F4.Name = "F4";
            this.F4.Visible = true;
            this.F4.VisibleIndex = 3;
            this.F4.Width = 49;
            // 
            // F5
            // 
            this.F5.Caption = "3线";
            this.F5.FieldName = "F5";
            this.F5.Name = "F5";
            this.F5.Visible = true;
            this.F5.VisibleIndex = 4;
            this.F5.Width = 49;
            // 
            // F6
            // 
            this.F6.Caption = "4线";
            this.F6.FieldName = "F6";
            this.F6.Name = "F6";
            this.F6.Visible = true;
            this.F6.VisibleIndex = 5;
            this.F6.Width = 49;
            // 
            // F7
            // 
            this.F7.Caption = "5线";
            this.F7.FieldName = "F7";
            this.F7.Name = "F7";
            this.F7.Visible = true;
            this.F7.VisibleIndex = 6;
            this.F7.Width = 49;
            // 
            // F8
            // 
            this.F8.Caption = "6线";
            this.F8.FieldName = "F8";
            this.F8.Name = "F8";
            this.F8.Visible = true;
            this.F8.VisibleIndex = 7;
            this.F8.Width = 49;
            // 
            // F9
            // 
            this.F9.Caption = "7线";
            this.F9.FieldName = "F9";
            this.F9.Name = "F9";
            this.F9.Visible = true;
            this.F9.VisibleIndex = 8;
            this.F9.Width = 49;
            // 
            // F10
            // 
            this.F10.Caption = "8线";
            this.F10.FieldName = "F10";
            this.F10.Name = "F10";
            this.F10.Visible = true;
            this.F10.VisibleIndex = 9;
            this.F10.Width = 73;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnSure);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(799, 377);
            this.splitContainerControl1.SplitterPosition = 51;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(108, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSure.Appearance.Options.UseFont = true;
            this.btnSure.Location = new System.Drawing.Point(12, 12);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 12;
            this.btnSure.Text = "确定";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // FormCheckLine
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 377);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormCheckLine";
            this.Text = "产品产线选择";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraEditors . SimpleButton btnCancel;
        private DevExpress . XtraEditors . SimpleButton btnSure;
        private DevExpress . XtraGrid . Columns . GridColumn F1;
        private DevExpress . XtraGrid . Columns . GridColumn F2;
        private DevExpress . XtraGrid . Columns . GridColumn F3;
        private DevExpress . XtraGrid . Columns . GridColumn F4;
        private DevExpress . XtraGrid . Columns . GridColumn F5;
        private DevExpress . XtraGrid . Columns . GridColumn F6;
        private DevExpress . XtraGrid . Columns . GridColumn F7;
        private DevExpress . XtraGrid . Columns . GridColumn F8;
        private DevExpress . XtraGrid . Columns . GridColumn F9;
        private DevExpress . XtraGrid . Columns . GridColumn F10;
        private DevExpress . XtraEditors . Repository . RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}