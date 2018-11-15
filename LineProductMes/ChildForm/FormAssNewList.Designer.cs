namespace LineProductMes . ChildForm
{
    partial class FormAssNewList
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
            this.ANW001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW013 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW012 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(731, 365);
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
            this.ANW001,
            this.ANW002,
            this.ANW003,
            this.ANW013,
            this.ANW009,
            this.ANW012});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // ANW001
            // 
            this.ANW001.Caption = "单号";
            this.ANW001.FieldName = "ANW001";
            this.ANW001.Name = "ANW001";
            this.ANW001.Visible = true;
            this.ANW001.VisibleIndex = 0;
            // 
            // ANW002
            // 
            this.ANW002.Caption = "来源单号";
            this.ANW002.FieldName = "ANW002";
            this.ANW002.Name = "ANW002";
            this.ANW002.Visible = true;
            this.ANW002.VisibleIndex = 1;
            // 
            // ANW003
            // 
            this.ANW003.Caption = "品号";
            this.ANW003.FieldName = "ANW003";
            this.ANW003.Name = "ANW003";
            this.ANW003.Visible = true;
            this.ANW003.VisibleIndex = 2;
            // 
            // ANW013
            // 
            this.ANW013.Caption = "班组";
            this.ANW013.FieldName = "ANW013";
            this.ANW013.Name = "ANW013";
            this.ANW013.Visible = true;
            this.ANW013.VisibleIndex = 4;
            // 
            // ANW009
            // 
            this.ANW009.Caption = "报工数量";
            this.ANW009.FieldName = "ANW009";
            this.ANW009.Name = "ANW009";
            this.ANW009.Visible = true;
            this.ANW009.VisibleIndex = 5;
            // 
            // ANW012
            // 
            this.ANW012.Caption = "班组编号";
            this.ANW012.FieldName = "ANW012";
            this.ANW012.Name = "ANW012";
            this.ANW012.Visible = true;
            this.ANW012.VisibleIndex = 3;
            // 
            // FormAssNewList
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 365);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormAssNewList";
            this.Text = "组装报工单查询";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn ANW001;
        private DevExpress . XtraGrid . Columns . GridColumn ANW002;
        private DevExpress . XtraGrid . Columns . GridColumn ANW003;
        private DevExpress . XtraGrid . Columns . GridColumn ANW013;
        private DevExpress . XtraGrid . Columns . GridColumn ANW009;
        private DevExpress . XtraGrid . Columns . GridColumn ANW012;
    }
}