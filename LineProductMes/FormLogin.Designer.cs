namespace LineProductMes
{
    partial class FormLogin
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
            this.txtUserName = new UILibrary.SkinTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassW = new UILibrary.SkinTextBox();
            this.chxUserName = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labUserName = new System.Windows.Forms.Label();
            this.labPassW = new System.Windows.Forms.Label();
            this.labTip = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtUserName.BackColor = System.Drawing.Color.Transparent;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Icon = null;
            this.txtUserName.IconIsButton = false;
            this.txtUserName.IsPasswordChat = '\0';
            this.txtUserName.IsSystemPasswordChar = false;
            this.txtUserName.LeftIcon = null;
            this.txtUserName.Lines = new string[0];
            this.txtUserName.Location = new System.Drawing.Point(124, 32);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserName.MaxLength = 32767;
            this.txtUserName.MinimumSize = new System.Drawing.Size(2, 28);
            this.txtUserName.MouseBack = null;
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NormlBack = null;
            this.txtUserName.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserName.ReadOnly = false;
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.Size = new System.Drawing.Size(154, 28);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUserName.WaterColor = System.Drawing.Color.DarkGray;
            this.txtUserName.WaterText = "请输入用户名";
            this.txtUserName.WordWrap = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(54, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(54, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "密  码：";
            // 
            // txtPassW
            // 
            this.txtPassW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPassW.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPassW.BackColor = System.Drawing.Color.Transparent;
            this.txtPassW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassW.Icon = null;
            this.txtPassW.IconIsButton = false;
            this.txtPassW.IsPasswordChat = '*';
            this.txtPassW.IsSystemPasswordChar = false;
            this.txtPassW.LeftIcon = null;
            this.txtPassW.Lines = new string[0];
            this.txtPassW.Location = new System.Drawing.Point(124, 86);
            this.txtPassW.Margin = new System.Windows.Forms.Padding(0);
            this.txtPassW.MaxLength = 32767;
            this.txtPassW.MinimumSize = new System.Drawing.Size(2, 28);
            this.txtPassW.MouseBack = null;
            this.txtPassW.Multiline = false;
            this.txtPassW.Name = "txtPassW";
            this.txtPassW.NormlBack = null;
            this.txtPassW.Padding = new System.Windows.Forms.Padding(5);
            this.txtPassW.ReadOnly = false;
            this.txtPassW.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassW.Size = new System.Drawing.Size(154, 28);
            this.txtPassW.TabIndex = 2;
            this.txtPassW.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPassW.WaterColor = System.Drawing.Color.DarkGray;
            this.txtPassW.WaterText = "请输入密码";
            this.txtPassW.WordWrap = true;
            // 
            // chxUserName
            // 
            this.chxUserName.AutoSize = true;
            this.chxUserName.Checked = true;
            this.chxUserName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUserName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chxUserName.Location = new System.Drawing.Point(60, 148);
            this.chxUserName.Name = "chxUserName";
            this.chxUserName.Size = new System.Drawing.Size(96, 18);
            this.chxUserName.TabIndex = 4;
            this.chxUserName.Text = "记住用户名";
            this.chxUserName.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(68, 193);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 28);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(201, 193);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 28);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUserName.ForeColor = System.Drawing.Color.Red;
            this.labUserName.Location = new System.Drawing.Point(129, 66);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(0, 14);
            this.labUserName.TabIndex = 7;
            // 
            // labPassW
            // 
            this.labPassW.AutoSize = true;
            this.labPassW.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPassW.ForeColor = System.Drawing.Color.Red;
            this.labPassW.Location = new System.Drawing.Point(129, 121);
            this.labPassW.Name = "labPassW";
            this.labPassW.Size = new System.Drawing.Size(0, 14);
            this.labPassW.TabIndex = 8;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTip.ForeColor = System.Drawing.Color.Red;
            this.labTip.Location = new System.Drawing.Point(63, 175);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(0, 14);
            this.labTip.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(164, 147);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 21);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FormLogin
            // 
            this.AcceptButton = this.btnOk;
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 243);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labTip);
            this.Controls.Add(this.labPassW);
            this.Controls.Add(this.labUserName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chxUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassW);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UILibrary . SkinTextBox txtUserName;
        private System . Windows . Forms . Label label1;
        private System . Windows . Forms . Label label2;
        private UILibrary . SkinTextBox txtPassW;
        private System . Windows . Forms . CheckBox chxUserName;
        private System . Windows . Forms . Button btnOk;
        private System . Windows . Forms . Button btnCancel;
        private System . Windows . Forms . Label labUserName;
        private System . Windows . Forms . Label labPassW;
        private System . Windows . Forms . Label labTip;
        private System . Windows . Forms . PictureBox pictureBox1;
    }
}