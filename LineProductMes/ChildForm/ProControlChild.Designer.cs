namespace LineProductMes . ChildForm
{
    partial class ProControlChild
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.txtPid = new DevExpress.XtraEditors.GridLookUpEdit();
            this.View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.FOR001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FOR004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtProId = new DevExpress.XtraEditors.TextEdit();
            this.txtProName = new DevExpress.XtraEditors.TextEdit();
            this.txtTable = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtId);
            this.layoutControl1.Controls.Add(this.txtPid);
            this.layoutControl1.Controls.Add(this.txtProId);
            this.layoutControl1.Controls.Add(this.txtProName);
            this.layoutControl1.Controls.Add(this.txtTable);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.txtType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(507, 123);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(335, 12);
            this.txtId.Name = "txtId";
            this.txtId.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtId.Properties.Appearance.Options.UseFont = true;
            this.txtId.Size = new System.Drawing.Size(160, 20);
            this.txtId.StyleController = this.layoutControl1;
            this.txtId.TabIndex = 4;
            // 
            // txtPid
            // 
            this.txtPid.Location = new System.Drawing.Point(71, 12);
            this.txtPid.Name = "txtPid";
            this.txtPid.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtPid.Properties.Appearance.Options.UseFont = true;
            this.txtPid.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.txtPid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPid.Properties.ImmediatePopup = true;
            this.txtPid.Properties.NullText = "";
            this.txtPid.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.txtPid.Properties.View = this.View;
            this.txtPid.Size = new System.Drawing.Size(201, 20);
            this.txtPid.StyleController = this.layoutControl1;
            this.txtPid.TabIndex = 5;
            // 
            // View
            // 
            this.View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.FOR001,
            this.FOR004});
            this.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.View.Name = "View";
            this.View.OptionsBehavior.Editable = false;
            this.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.View.OptionsView.ShowGroupPanel = false;
            // 
            // FOR001
            // 
            this.FOR001.Caption = "父节点";
            this.FOR001.FieldName = "FOR001";
            this.FOR001.Name = "FOR001";
            this.FOR001.Visible = true;
            this.FOR001.VisibleIndex = 0;
            // 
            // FOR004
            // 
            this.FOR004.Caption = "名称";
            this.FOR004.FieldName = "FOR004";
            this.FOR004.Name = "FOR004";
            this.FOR004.Visible = true;
            this.FOR004.VisibleIndex = 1;
            // 
            // txtProId
            // 
            this.txtProId.Location = new System.Drawing.Point(71, 36);
            this.txtProId.Name = "txtProId";
            this.txtProId.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtProId.Properties.Appearance.Options.UseFont = true;
            this.txtProId.Size = new System.Drawing.Size(201, 20);
            this.txtProId.StyleController = this.layoutControl1;
            this.txtProId.TabIndex = 6;
            // 
            // txtProName
            // 
            this.txtProName.Location = new System.Drawing.Point(335, 36);
            this.txtProName.Name = "txtProName";
            this.txtProName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtProName.Properties.Appearance.Options.UseFont = true;
            this.txtProName.Size = new System.Drawing.Size(160, 20);
            this.txtProName.StyleController = this.layoutControl1;
            this.txtProName.TabIndex = 7;
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point(71, 60);
            this.txtTable.Name = "txtTable";
            this.txtTable.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtTable.Properties.Appearance.Options.UseFont = true;
            this.txtTable.Size = new System.Drawing.Size(201, 20);
            this.txtTable.StyleController = this.layoutControl1;
            this.txtTable.TabIndex = 8;
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(309, 84);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(397, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.layoutControlItem7,
            this.emptySpaceItem1,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(507, 123);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.txtId;
            this.layoutControlItem1.CustomizationFormText = "节    点";
            this.layoutControlItem1.Location = new System.Drawing.Point(264, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItem1.Text = "节    点";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.txtPid;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(264, 24);
            this.layoutControlItem2.Text = "父节点";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.txtProId;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(264, 24);
            this.layoutControlItem3.Text = "程序代号";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.txtProName;
            this.layoutControlItem4.Location = new System.Drawing.Point(264, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItem4.Text = "程序名称";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("宋体", 10.5F);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.txtTable;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(264, 24);
            this.layoutControlItem5.Text = "程序用表";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnOk;
            this.layoutControlItem6.Location = new System.Drawing.Point(297, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(60, 31);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCancel;
            this.layoutControlItem7.Location = new System.Drawing.Point(385, 72);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(59, 31);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.txtType;
            this.layoutControlItem8.Location = new System.Drawing.Point(264, 48);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(223, 24);
            this.layoutControlItem8.Text = "程序类别";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(56, 14);
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(335, 60);
            this.txtType.Name = "txtType";
            this.txtType.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtType.Properties.Appearance.Options.UseFont = true;
            this.txtType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtType.Properties.Items.AddRange(new object[] {
            "0",
            "1"});
            this.txtType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtType.Size = new System.Drawing.Size(160, 20);
            this.txtType.StyleController = this.layoutControl1;
            this.txtType.TabIndex = 11;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(297, 31);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(357, 72);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(28, 31);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(444, 72);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(43, 31);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ProControlChild
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 123);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ProControlChild";
            this.Text = "程序管控";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraLayout . LayoutControl layoutControl1;
        private DevExpress . XtraLayout . LayoutControlGroup layoutControlGroup1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem1;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem2;
        private DevExpress . XtraEditors . TextEdit txtId;
        private DevExpress . XtraEditors . GridLookUpEdit txtPid;
        private DevExpress . XtraGrid . Views . Grid . GridView View;
        private DevExpress . XtraEditors . TextEdit txtProId;
        private DevExpress . XtraEditors . TextEdit txtProName;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem3;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem4;
        private DevExpress . XtraEditors . TextEdit txtTable;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem5;
        private DevExpress . XtraEditors . SimpleButton btnOk;
        private DevExpress . XtraEditors . SimpleButton btnCancel;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem6;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem7;
        private DevExpress . XtraGrid . Columns . GridColumn FOR001;
        private DevExpress . XtraGrid . Columns . GridColumn FOR004;
        private DevExpress . XtraEditors . ComboBoxEdit txtType;
        private DevExpress . XtraLayout . LayoutControlItem layoutControlItem8;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem1;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem2;
        private DevExpress . XtraLayout . EmptySpaceItem emptySpaceItem3;
    }
}