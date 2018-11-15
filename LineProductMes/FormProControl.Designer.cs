namespace LineProductMes
{
    partial class FormProControl
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
            this.FOR001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.idx = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR006 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 24);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1231, 425);
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
            this.FOR001,
            this.FOR002,
            this.FOR003,
            this.FOR004,
            this.FOR005,
            this.idx,
            this.FOR006});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // FOR001
            // 
            this.FOR001.Caption = "节点";
            this.FOR001.FieldName = "FOR001";
            this.FOR001.Name = "FOR001";
            this.FOR001.Visible = true;
            this.FOR001.VisibleIndex = 0;
            this.FOR001.Width = 141;
            // 
            // FOR002
            // 
            this.FOR002.Caption = "父节点";
            this.FOR002.FieldName = "FOR002";
            this.FOR002.Name = "FOR002";
            this.FOR002.Visible = true;
            this.FOR002.VisibleIndex = 1;
            this.FOR002.Width = 160;
            // 
            // FOR003
            // 
            this.FOR003.Caption = "程序代号";
            this.FOR003.FieldName = "FOR003";
            this.FOR003.Name = "FOR003";
            this.FOR003.Visible = true;
            this.FOR003.VisibleIndex = 2;
            this.FOR003.Width = 255;
            // 
            // FOR004
            // 
            this.FOR004.Caption = "程序名称";
            this.FOR004.FieldName = "FOR004";
            this.FOR004.Name = "FOR004";
            this.FOR004.Visible = true;
            this.FOR004.VisibleIndex = 3;
            this.FOR004.Width = 255;
            // 
            // FOR005
            // 
            this.FOR005.Caption = "程序用表";
            this.FOR005.FieldName = "FOR005";
            this.FOR005.Name = "FOR005";
            this.FOR005.Visible = true;
            this.FOR005.VisibleIndex = 4;
            this.FOR005.Width = 264;
            // 
            // idx
            // 
            this.idx.Caption = "编号";
            this.idx.FieldName = "idx";
            this.idx.Name = "idx";
            // 
            // FOR006
            // 
            this.FOR006.Caption = "程序类型";
            this.FOR006.FieldName = "FOR006";
            this.FOR006.Name = "FOR006";
            this.FOR006.Visible = true;
            this.FOR006.VisibleIndex = 5;
            // 
            // FormProControl
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 449);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormProControl";
            this.Text = "程序管控";
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn FOR001;
        private DevExpress . XtraGrid . Columns . GridColumn FOR002;
        private DevExpress . XtraGrid . Columns . GridColumn FOR003;
        private DevExpress . XtraGrid . Columns . GridColumn FOR004;
        private DevExpress . XtraGrid . Columns . GridColumn FOR005;
        private DevExpress . XtraGrid . Columns . GridColumn idx;
        private DevExpress . XtraGrid . Columns . GridColumn FOR006;
    }
}