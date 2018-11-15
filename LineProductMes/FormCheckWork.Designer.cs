namespace LineProductMes
{
    partial class FormCheckWork
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1237, 414);
            this.splitContainerControl1.SplitterPosition = 59;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1237, 350);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand5,
            this.gridBand8});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "人员编号";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "人员姓名";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "岗位";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "上班";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "下班";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "上班";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "下班";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "上班";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "下班";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "上班";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "下班";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "人员信息";
            this.gridBand1.Columns.Add(this.gridColumn1);
            this.gridBand1.Columns.Add(this.gridColumn2);
            this.gridBand1.Columns.Add(this.gridColumn3);
            this.gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 225;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "1日";
            this.gridBand2.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand3,
            this.gridBand4});
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 300;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "白班";
            this.gridBand3.Columns.Add(this.gridColumn4);
            this.gridBand3.Columns.Add(this.gridColumn5);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 0;
            this.gridBand3.Width = 150;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.Caption = "夜班";
            this.gridBand4.Columns.Add(this.gridColumn6);
            this.gridBand4.Columns.Add(this.gridColumn7);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 1;
            this.gridBand4.Width = 150;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.Caption = "2日";
            this.gridBand5.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand6,
            this.gridBand7});
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 2;
            this.gridBand5.Width = 300;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.Caption = "白班";
            this.gridBand6.Columns.Add(this.gridColumn8);
            this.gridBand6.Columns.Add(this.gridColumn9);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 0;
            this.gridBand6.Width = 150;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.Caption = "夜班";
            this.gridBand7.Columns.Add(this.gridColumn10);
            this.gridBand7.Columns.Add(this.gridColumn11);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 1;
            this.gridBand7.Width = 150;
            // 
            // gridBand8
            // 
            this.gridBand8.Caption = "gridBand8";
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.RowCount = 2;
            this.gridBand8.VisibleIndex = 3;
            this.gridBand8.Width = 150;
            // 
            // FormCheckWork
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 438);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormCheckWork";
            this.Text = "员工考勤表";
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridView bandedGridView1;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn1;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn2;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn3;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn8;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn9;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn10;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn11;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn4;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn5;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn6;
        private DevExpress . XtraGrid . Views . BandedGrid . BandedGridColumn gridColumn7;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand1;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand2;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand3;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand4;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand5;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand6;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand7;
        private DevExpress . XtraGrid . Views . BandedGrid . GridBand gridBand8;
    }
}