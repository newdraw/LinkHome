namespace LinkHome
{
    partial class DlgConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTTL = new System.Windows.Forms.NumericUpDown();
            this.txtInterval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAccessKeyID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAccessKeySecret = new System.Windows.Forms.TextBox();
            this.chkHideForm = new System.Windows.Forms.CheckBox();
            this.btnHowToGetAccessKey = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtTTL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(188, 261);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(139, 34);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(342, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "TTL";
            // 
            // txtTTL
            // 
            this.txtTTL.Location = new System.Drawing.Point(154, 143);
            this.txtTTL.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtTTL.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.txtTTL.Name = "txtTTL";
            this.txtTTL.Size = new System.Drawing.Size(87, 21);
            this.txtTTL.TabIndex = 8;
            this.txtTTL.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // txtInterval
            // 
            this.txtInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txtInterval.Location = new System.Drawing.Point(154, 177);
            this.txtInterval.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(87, 21);
            this.txtInterval.TabIndex = 10;
            this.txtInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "IP检查频率";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "秒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "秒";
            // 
            // txtAccessKeyID
            // 
            this.txtAccessKeyID.Location = new System.Drawing.Point(154, 67);
            this.txtAccessKeyID.Name = "txtAccessKeyID";
            this.txtAccessKeyID.PasswordChar = '●';
            this.txtAccessKeyID.Size = new System.Drawing.Size(327, 21);
            this.txtAccessKeyID.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "AccessKey ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "AccessKey Secret";
            // 
            // txtAccessKeySecret
            // 
            this.txtAccessKeySecret.Location = new System.Drawing.Point(154, 105);
            this.txtAccessKeySecret.Name = "txtAccessKeySecret";
            this.txtAccessKeySecret.PasswordChar = '●';
            this.txtAccessKeySecret.Size = new System.Drawing.Size(327, 21);
            this.txtAccessKeySecret.TabIndex = 15;
            // 
            // chkHideForm
            // 
            this.chkHideForm.AutoSize = true;
            this.chkHideForm.Location = new System.Drawing.Point(38, 219);
            this.chkHideForm.Name = "chkHideForm";
            this.chkHideForm.Size = new System.Drawing.Size(120, 16);
            this.chkHideForm.TabIndex = 17;
            this.chkHideForm.Text = "启动时隐藏主窗口";
            this.chkHideForm.UseVisualStyleBackColor = true;
            // 
            // btnHowToGetAccessKey
            // 
            this.btnHowToGetAccessKey.AutoSize = true;
            this.btnHowToGetAccessKey.Location = new System.Drawing.Point(254, 36);
            this.btnHowToGetAccessKey.Name = "btnHowToGetAccessKey";
            this.btnHowToGetAccessKey.Size = new System.Drawing.Size(227, 12);
            this.btnHowToGetAccessKey.TabIndex = 18;
            this.btnHowToGetAccessKey.TabStop = true;
            this.btnHowToGetAccessKey.Text = "如何获取AccessKeyId和AccessKeySecret?";
            this.btnHowToGetAccessKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnHowToGetAccessKey_LinkClicked);
            // 
            // DlgConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(510, 315);
            this.Controls.Add(this.btnHowToGetAccessKey);
            this.Controls.Add(this.chkHideForm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAccessKeySecret);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAccessKeyID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTTL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            ((System.ComponentModel.ISupportInitialize)(this.txtTTL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtTTL;
        private System.Windows.Forms.NumericUpDown txtInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccessKeyID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAccessKeySecret;
        private System.Windows.Forms.CheckBox chkHideForm;
        private System.Windows.Forms.LinkLabel btnHowToGetAccessKey;
    }
}