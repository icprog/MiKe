namespace LineProductMes . ChildForm
{
    partial class FormHardOrder
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
            this.RAA001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RAA015 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA057 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RAA018 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DEA050 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RAA008 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(912, 412);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.RAA001,
            this.RAA015,
            this.DEA002,
            this.DEA003,
            this.DEA057,
            this.RAA018,
            this.DEA050,
            this.RAA008});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // RAA001
            // 
            this.RAA001.Caption = "来源工单";
            this.RAA001.FieldName = "RAA001";
            this.RAA001.Name = "RAA001";
            this.RAA001.Visible = true;
            this.RAA001.VisibleIndex = 0;
            this.RAA001.Width = 134;
            // 
            // RAA015
            // 
            this.RAA015.Caption = "品号";
            this.RAA015.FieldName = "RAA015";
            this.RAA015.Name = "RAA015";
            this.RAA015.Visible = true;
            this.RAA015.VisibleIndex = 1;
            this.RAA015.Width = 134;
            // 
            // DEA002
            // 
            this.DEA002.Caption = "品名";
            this.DEA002.FieldName = "DEA002";
            this.DEA002.Name = "DEA002";
            this.DEA002.Visible = true;
            this.DEA002.VisibleIndex = 2;
            this.DEA002.Width = 134;
            // 
            // DEA003
            // 
            this.DEA003.Caption = "单位";
            this.DEA003.FieldName = "DEA003";
            this.DEA003.Name = "DEA003";
            this.DEA003.Visible = true;
            this.DEA003.VisibleIndex = 5;
            this.DEA003.Width = 61;
            // 
            // DEA057
            // 
            this.DEA057.Caption = "规格";
            this.DEA057.FieldName = "DEA057";
            this.DEA057.Name = "DEA057";
            this.DEA057.Visible = true;
            this.DEA057.VisibleIndex = 3;
            this.DEA057.Width = 134;
            // 
            // RAA018
            // 
            this.RAA018.Caption = "数量";
            this.RAA018.FieldName = "RAA018";
            this.RAA018.Name = "RAA018";
            this.RAA018.Visible = true;
            this.RAA018.VisibleIndex = 6;
            this.RAA018.Width = 169;
            // 
            // DEA050
            // 
            this.DEA050.Caption = "单价";
            this.DEA050.FieldName = "DEA050";
            this.DEA050.Name = "DEA050";
            this.DEA050.Visible = true;
            this.DEA050.VisibleIndex = 4;
            this.DEA050.Width = 134;
            // 
            // RAA008
            // 
            this.RAA008.Caption = "开工时间";
            this.RAA008.FieldName = "RAA008";
            this.RAA008.Name = "RAA008";
            this.RAA008.Visible = true;
            this.RAA008.VisibleIndex = 7;
            this.RAA008.Width = 178;
            // 
            // FormHardOrder
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 412);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormHardOrder";
            this.Text = "来源工单";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn RAA001;
        private DevExpress . XtraGrid . Columns . GridColumn RAA015;
        private DevExpress . XtraGrid . Columns . GridColumn DEA002;
        private DevExpress . XtraGrid . Columns . GridColumn DEA003;
        private DevExpress . XtraGrid . Columns . GridColumn DEA057;
        private DevExpress . XtraGrid . Columns . GridColumn RAA018;
        private DevExpress . XtraGrid . Columns . GridColumn DEA050;
        private DevExpress . XtraGrid . Columns . GridColumn RAA008;
    }
}