namespace LineProductMes . ChildForm
{
    partial class PowerEdit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.cekExample = new System.Windows.Forms.CheckBox();
            this.cekPrint = new System.Windows.Forms.CheckBox();
            this.cekCancell = new System.Windows.Forms.CheckBox();
            this.cekExamin = new System.Windows.Forms.CheckBox();
            this.cekReview = new System.Windows.Forms.CheckBox();
            this.cekCancel = new System.Windows.Forms.CheckBox();
            this.cekSave = new System.Windows.Forms.CheckBox();
            this.cekEdit = new System.Windows.Forms.CheckBox();
            this.cekDelete = new System.Windows.Forms.CheckBox();
            this.cekAdd = new System.Windows.Forms.CheckBox();
            this.cekQuery = new System.Windows.Forms.CheckBox();
            this.cekRun = new System.Windows.Forms.CheckBox();
            this.lupNamePro = new DevExpress.XtraEditors.LookUpEdit();
            this.lupNamePerson = new DevExpress.XtraEditors.LookUpEdit();
            this.btnCan = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupNamePro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNamePerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnCheckAll);
            this.panel1.Controls.Add(this.cekExample);
            this.panel1.Controls.Add(this.cekPrint);
            this.panel1.Controls.Add(this.cekCancell);
            this.panel1.Controls.Add(this.cekExamin);
            this.panel1.Controls.Add(this.cekReview);
            this.panel1.Controls.Add(this.cekCancel);
            this.panel1.Controls.Add(this.cekSave);
            this.panel1.Controls.Add(this.cekEdit);
            this.panel1.Controls.Add(this.cekDelete);
            this.panel1.Controls.Add(this.cekAdd);
            this.panel1.Controls.Add(this.cekQuery);
            this.panel1.Controls.Add(this.cekRun);
            this.panel1.Controls.Add(this.lupNamePro);
            this.panel1.Controls.Add(this.lupNamePerson);
            this.panel1.Controls.Add(this.btnCan);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 264);
            this.panel1.TabIndex = 0;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.AutoSize = true;
            this.btnCheckAll.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCheckAll.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheckAll.Location = new System.Drawing.Point(26, 221);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(58, 30);
            this.btnCheckAll.TabIndex = 49;
            this.btnCheckAll.Text = "全选";
            this.btnCheckAll.UseVisualStyleBackColor = false;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // cekExample
            // 
            this.cekExample.AutoSize = true;
            this.cekExample.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekExample.Location = new System.Drawing.Point(145, 188);
            this.cekExample.Name = "cekExample";
            this.cekExample.Size = new System.Drawing.Size(54, 18);
            this.cekExample.TabIndex = 48;
            this.cekExample.Text = "导出";
            this.cekExample.UseVisualStyleBackColor = true;
            this.cekExample.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekPrint
            // 
            this.cekPrint.AutoSize = true;
            this.cekPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekPrint.Location = new System.Drawing.Point(52, 188);
            this.cekPrint.Name = "cekPrint";
            this.cekPrint.Size = new System.Drawing.Size(54, 18);
            this.cekPrint.TabIndex = 47;
            this.cekPrint.Text = "打印";
            this.cekPrint.UseVisualStyleBackColor = true;
            this.cekPrint.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekCancell
            // 
            this.cekCancell.AutoSize = true;
            this.cekCancell.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekCancell.Location = new System.Drawing.Point(145, 158);
            this.cekCancell.Name = "cekCancell";
            this.cekCancell.Size = new System.Drawing.Size(54, 18);
            this.cekCancell.TabIndex = 46;
            this.cekCancell.Text = "注销";
            this.cekCancell.UseVisualStyleBackColor = true;
            this.cekCancell.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekExamin
            // 
            this.cekExamin.AutoSize = true;
            this.cekExamin.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekExamin.Location = new System.Drawing.Point(249, 158);
            this.cekExamin.Name = "cekExamin";
            this.cekExamin.Size = new System.Drawing.Size(54, 18);
            this.cekExamin.TabIndex = 45;
            this.cekExamin.Text = "审核";
            this.cekExamin.UseVisualStyleBackColor = true;
            this.cekExamin.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekReview
            // 
            this.cekReview.AutoSize = true;
            this.cekReview.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekReview.Location = new System.Drawing.Point(249, 188);
            this.cekReview.Name = "cekReview";
            this.cekReview.Size = new System.Drawing.Size(54, 18);
            this.cekReview.TabIndex = 44;
            this.cekReview.Text = "送审";
            this.cekReview.UseVisualStyleBackColor = true;
            this.cekReview.Visible = false;
            this.cekReview.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekCancel
            // 
            this.cekCancel.AutoSize = true;
            this.cekCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekCancel.Location = new System.Drawing.Point(52, 158);
            this.cekCancel.Name = "cekCancel";
            this.cekCancel.Size = new System.Drawing.Size(54, 18);
            this.cekCancel.TabIndex = 43;
            this.cekCancel.Text = "取消";
            this.cekCancel.UseVisualStyleBackColor = true;
            this.cekCancel.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekSave
            // 
            this.cekSave.AutoSize = true;
            this.cekSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekSave.Location = new System.Drawing.Point(249, 128);
            this.cekSave.Name = "cekSave";
            this.cekSave.Size = new System.Drawing.Size(54, 18);
            this.cekSave.TabIndex = 42;
            this.cekSave.Text = "保存";
            this.cekSave.UseVisualStyleBackColor = true;
            this.cekSave.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekEdit
            // 
            this.cekEdit.AutoSize = true;
            this.cekEdit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekEdit.Location = new System.Drawing.Point(145, 128);
            this.cekEdit.Name = "cekEdit";
            this.cekEdit.Size = new System.Drawing.Size(54, 18);
            this.cekEdit.TabIndex = 41;
            this.cekEdit.Text = "编辑";
            this.cekEdit.UseVisualStyleBackColor = true;
            this.cekEdit.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekDelete
            // 
            this.cekDelete.AutoSize = true;
            this.cekDelete.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekDelete.Location = new System.Drawing.Point(52, 128);
            this.cekDelete.Name = "cekDelete";
            this.cekDelete.Size = new System.Drawing.Size(54, 18);
            this.cekDelete.TabIndex = 40;
            this.cekDelete.Text = "删除";
            this.cekDelete.UseVisualStyleBackColor = true;
            this.cekDelete.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekAdd
            // 
            this.cekAdd.AutoSize = true;
            this.cekAdd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekAdd.Location = new System.Drawing.Point(249, 97);
            this.cekAdd.Name = "cekAdd";
            this.cekAdd.Size = new System.Drawing.Size(54, 18);
            this.cekAdd.TabIndex = 39;
            this.cekAdd.Text = "新增";
            this.cekAdd.UseVisualStyleBackColor = true;
            this.cekAdd.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekQuery
            // 
            this.cekQuery.AutoSize = true;
            this.cekQuery.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekQuery.Location = new System.Drawing.Point(145, 97);
            this.cekQuery.Name = "cekQuery";
            this.cekQuery.Size = new System.Drawing.Size(54, 18);
            this.cekQuery.TabIndex = 38;
            this.cekQuery.Text = "查询";
            this.cekQuery.UseVisualStyleBackColor = true;
            this.cekQuery.CheckedChanged += new System.EventHandler(this.cekQuery_CheckedChanged);
            // 
            // cekRun
            // 
            this.cekRun.AutoSize = true;
            this.cekRun.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cekRun.Location = new System.Drawing.Point(52, 97);
            this.cekRun.Name = "cekRun";
            this.cekRun.Size = new System.Drawing.Size(54, 18);
            this.cekRun.TabIndex = 37;
            this.cekRun.Text = "运行";
            this.cekRun.UseVisualStyleBackColor = true;
            this.cekRun.CheckedChanged += new System.EventHandler(this.cekRun_CheckedChanged);
            // 
            // lupNamePro
            // 
            this.lupNamePro.Location = new System.Drawing.Point(106, 56);
            this.lupNamePro.Name = "lupNamePro";
            this.lupNamePro.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupNamePro.Properties.Appearance.Options.UseFont = true;
            this.lupNamePro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNamePro.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FOR003", 50, "编号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FOR004", 55, "名称")});
            this.lupNamePro.Properties.NullText = "";
            this.lupNamePro.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.lupNamePro.Properties.PopupFormMinSize = new System.Drawing.Size(400, 200);
            this.lupNamePro.Properties.PopupWidth = 400;
            this.lupNamePro.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.lupNamePro.Properties.ShowFooter = false;
            this.lupNamePro.Size = new System.Drawing.Size(192, 20);
            this.lupNamePro.TabIndex = 32;
            // 
            // lupNamePerson
            // 
            this.lupNamePerson.Location = new System.Drawing.Point(106, 17);
            this.lupNamePerson.Name = "lupNamePerson";
            this.lupNamePerson.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lupNamePerson.Properties.Appearance.Options.UseFont = true;
            this.lupNamePerson.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNamePerson.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EMP001", 35, "编号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EMP002", 55, "姓名")});
            this.lupNamePerson.Properties.NullText = "";
            this.lupNamePerson.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.lupNamePerson.Properties.ShowFooter = false;
            this.lupNamePerson.Size = new System.Drawing.Size(192, 20);
            this.lupNamePerson.TabIndex = 31;
            // 
            // btnCan
            // 
            this.btnCan.AutoSize = true;
            this.btnCan.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCan.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCan.Location = new System.Drawing.Point(224, 221);
            this.btnCan.Name = "btnCan";
            this.btnCan.Size = new System.Drawing.Size(87, 30);
            this.btnCan.TabIndex = 28;
            this.btnCan.Text = "取消";
            this.btnCan.UseVisualStyleBackColor = false;
            this.btnCan.Click += new System.EventHandler(this.btnCan_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(119, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 30);
            this.btnOk.TabIndex = 27;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(23, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "程序名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "人员姓名：";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // PowerEdit
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 264);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PowerEdit";
            this.Text = "PowerEdit";
            this.Load += new System.EventHandler(this.PowerEdit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupNamePro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNamePerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System . Windows . Forms . Panel panel1;
        private System . Windows . Forms . Label label1;
        private System . Windows . Forms . Label label4;
        private System . Windows . Forms . Button btnCan;
        private System . Windows . Forms . Button btnOk;
        private System . Windows . Forms . ErrorProvider errorProvider1;
        private DevExpress . XtraEditors . LookUpEdit lupNamePerson;
        private DevExpress . XtraEditors . LookUpEdit lupNamePro;
        private System . Windows . Forms . CheckBox cekRun;
        private System . Windows . Forms . CheckBox cekQuery;
        private System . Windows . Forms . CheckBox cekAdd;
        private System . Windows . Forms . CheckBox cekExample;
        private System . Windows . Forms . CheckBox cekPrint;
        private System . Windows . Forms . CheckBox cekCancell;
        private System . Windows . Forms . CheckBox cekExamin;
        private System . Windows . Forms . CheckBox cekReview;
        private System . Windows . Forms . CheckBox cekCancel;
        private System . Windows . Forms . CheckBox cekSave;
        private System . Windows . Forms . CheckBox cekEdit;
        private System . Windows . Forms . CheckBox cekDelete;
        private System . Windows . Forms . Button btnCheckAll;
    }
}