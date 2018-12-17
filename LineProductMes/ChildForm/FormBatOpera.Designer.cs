namespace LineProductMes . ChildForm
{
    partial class FormBatOpera
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSure = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.P1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.P2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.P3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Edit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.P4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Edit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.P5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.P6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit2.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainerControl1.Size = new System.Drawing.Size(783, 358);
            this.splitContainerControl1.SplitterPosition = 53;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(102, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSure.Appearance.Options.UseFont = true;
            this.btnSure.Location = new System.Drawing.Point(12, 12);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 27);
            this.btnSure.TabIndex = 10;
            this.btnSure.Text = "确定";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Edit1,
            this.Edit2});
            this.gridControl1.Size = new System.Drawing.Size(783, 300);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.P1,
            this.P2,
            this.P3,
            this.P4,
            this.P5,
            this.P6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // P1
            // 
            this.P1.Caption = "品号";
            this.P1.FieldName = "P1";
            this.P1.Name = "P1";
            this.P1.OptionsColumn.AllowEdit = false;
            this.P1.Visible = true;
            this.P1.VisibleIndex = 0;
            // 
            // P2
            // 
            this.P2.Caption = "品名";
            this.P2.FieldName = "P2";
            this.P2.Name = "P2";
            this.P2.OptionsColumn.AllowEdit = false;
            this.P2.Visible = true;
            this.P2.VisibleIndex = 1;
            // 
            // P3
            // 
            this.P3.Caption = "开始日期";
            this.P3.ColumnEdit = this.Edit1;
            this.P3.FieldName = "P3";
            this.P3.Name = "P3";
            this.P3.Visible = true;
            this.P3.VisibleIndex = 2;
            // 
            // Edit1
            // 
            this.Edit1.AutoHeight = false;
            this.Edit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Edit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Edit1.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.Edit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Edit1.EditFormat.FormatString = "yyyy-MM-dd";
            this.Edit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Edit1.Mask.EditMask = "yyyy-MM-dd";
            this.Edit1.Name = "Edit1";
            // 
            // P4
            // 
            this.P4.Caption = "结束日期";
            this.P4.ColumnEdit = this.Edit2;
            this.P4.FieldName = "P4";
            this.P4.Name = "P4";
            this.P4.Visible = true;
            this.P4.VisibleIndex = 3;
            // 
            // Edit2
            // 
            this.Edit2.AutoHeight = false;
            this.Edit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Edit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Edit2.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.Edit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Edit2.EditFormat.FormatString = "yyyy-MM-dd";
            this.Edit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Edit2.Mask.EditMask = "yyyy-MM-dd";
            this.Edit2.Name = "Edit2";
            // 
            // P5
            // 
            this.P5.Caption = "天数";
            this.P5.FieldName = "P5";
            this.P5.Name = "P5";
            this.P5.Visible = true;
            this.P5.VisibleIndex = 4;
            // 
            // P6
            // 
            this.P6.Caption = "调整量";
            this.P6.FieldName = "P6";
            this.P6.Name = "P6";
            this.P6.Visible = true;
            this.P6.VisibleIndex = 5;
            // 
            // FormBatOpera
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 358);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormBatOpera";
            this.Text = "FormBatOpera";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraEditors . SimpleButton btnCancel;
        private DevExpress . XtraEditors . SimpleButton btnSure;
        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn P1;
        private DevExpress . XtraGrid . Columns . GridColumn P2;
        private DevExpress . XtraGrid . Columns . GridColumn P3;
        private DevExpress . XtraEditors . Repository . RepositoryItemDateEdit Edit1;
        private DevExpress . XtraGrid . Columns . GridColumn P4;
        private DevExpress . XtraEditors . Repository . RepositoryItemDateEdit Edit2;
        private DevExpress . XtraGrid . Columns . GridColumn P5;
        private DevExpress . XtraGrid . Columns . GridColumn P6;
    }
}