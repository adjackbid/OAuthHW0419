namespace OAuthHW
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gLogin = new System.Windows.Forms.GroupBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNotifyStatus = new System.Windows.Forms.Label();
            this.btnCheckNotify = new System.Windows.Forms.Button();
            this.btnRevokeNotify = new System.Windows.Forms.Button();
            this.btnNotifyApply = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.ddlTarget = new System.Windows.Forms.ComboBox();
            this.btnSendNotify = new System.Windows.Forms.Button();
            this.btnGetTargetList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rlogBox = new System.Windows.Forms.RichTextBox();
            this.gLogin.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gLogin
            // 
            this.gLogin.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gLogin.Controls.Add(this.lblLogin);
            this.gLogin.Controls.Add(this.btnLogin);
            this.gLogin.Location = new System.Drawing.Point(27, 12);
            this.gLogin.Name = "gLogin";
            this.gLogin.Size = new System.Drawing.Size(349, 705);
            this.gLogin.TabIndex = 0;
            this.gLogin.TabStop = false;
            this.gLogin.Text = "Login Area";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLogin.Location = new System.Drawing.Point(16, 393);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(327, 35);
            this.lblLogin.TabIndex = 1;
            this.lblLogin.Text = "點選Login，使用Line登入";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(90, 278);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(181, 100);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.lblNotifyStatus);
            this.groupBox1.Controls.Add(this.btnCheckNotify);
            this.groupBox1.Controls.Add(this.btnRevokeNotify);
            this.groupBox1.Controls.Add(this.btnNotifyApply);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(394, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 705);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LineNotify Status";
            // 
            // lblNotifyStatus
            // 
            this.lblNotifyStatus.AutoSize = true;
            this.lblNotifyStatus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNotifyStatus.Location = new System.Drawing.Point(24, 184);
            this.lblNotifyStatus.Name = "lblNotifyStatus";
            this.lblNotifyStatus.Size = new System.Drawing.Size(308, 35);
            this.lblNotifyStatus.TabIndex = 1;
            this.lblNotifyStatus.Text = "LineNotify狀態：未登入";
            // 
            // btnCheckNotify
            // 
            this.btnCheckNotify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCheckNotify.Enabled = false;
            this.btnCheckNotify.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCheckNotify.Location = new System.Drawing.Point(24, 105);
            this.btnCheckNotify.Name = "btnCheckNotify";
            this.btnCheckNotify.Size = new System.Drawing.Size(225, 53);
            this.btnCheckNotify.TabIndex = 0;
            this.btnCheckNotify.Text = "檢查Notify狀態";
            this.btnCheckNotify.UseVisualStyleBackColor = false;
            this.btnCheckNotify.Click += new System.EventHandler(this.btnCheckNotify_Click);
            // 
            // btnRevokeNotify
            // 
            this.btnRevokeNotify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRevokeNotify.Enabled = false;
            this.btnRevokeNotify.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRevokeNotify.Location = new System.Drawing.Point(24, 325);
            this.btnRevokeNotify.Name = "btnRevokeNotify";
            this.btnRevokeNotify.Size = new System.Drawing.Size(225, 53);
            this.btnRevokeNotify.TabIndex = 0;
            this.btnRevokeNotify.Text = "取消Notify連動";
            this.btnRevokeNotify.UseVisualStyleBackColor = false;
            this.btnRevokeNotify.Click += new System.EventHandler(this.btnRevokeNotify_Click);
            // 
            // btnNotifyApply
            // 
            this.btnNotifyApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNotifyApply.Enabled = false;
            this.btnNotifyApply.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNotifyApply.Location = new System.Drawing.Point(24, 243);
            this.btnNotifyApply.Name = "btnNotifyApply";
            this.btnNotifyApply.Size = new System.Drawing.Size(225, 53);
            this.btnNotifyApply.TabIndex = 0;
            this.btnNotifyApply.Text = "LineNotify申請";
            this.btnNotifyApply.UseVisualStyleBackColor = false;
            this.btnNotifyApply.Click += new System.EventHandler(this.btnNotifyApply_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblName.Location = new System.Drawing.Point(24, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(200, 35);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name：未登入";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.rtbMessage);
            this.groupBox2.Controls.Add(this.ddlTarget);
            this.groupBox2.Controls.Add(this.btnSendNotify);
            this.groupBox2.Controls.Add(this.btnGetTargetList);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(763, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 705);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LineNotify Admin";
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(28, 243);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(299, 96);
            this.rtbMessage.TabIndex = 2;
            this.rtbMessage.Text = "";
            // 
            // ddlTarget
            // 
            this.ddlTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlTarget.FormattingEnabled = true;
            this.ddlTarget.Location = new System.Drawing.Point(23, 127);
            this.ddlTarget.Name = "ddlTarget";
            this.ddlTarget.Size = new System.Drawing.Size(304, 31);
            this.ddlTarget.TabIndex = 1;
            // 
            // btnSendNotify
            // 
            this.btnSendNotify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSendNotify.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSendNotify.Location = new System.Drawing.Point(23, 364);
            this.btnSendNotify.Name = "btnSendNotify";
            this.btnSendNotify.Size = new System.Drawing.Size(304, 53);
            this.btnSendNotify.TabIndex = 0;
            this.btnSendNotify.Text = "發送訊息";
            this.btnSendNotify.UseVisualStyleBackColor = false;
            this.btnSendNotify.Click += new System.EventHandler(this.btnSendNotify_Click);
            // 
            // btnGetTargetList
            // 
            this.btnGetTargetList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnGetTargetList.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGetTargetList.Location = new System.Drawing.Point(23, 47);
            this.btnGetTargetList.Name = "btnGetTargetList";
            this.btnGetTargetList.Size = new System.Drawing.Size(304, 53);
            this.btnGetTargetList.TabIndex = 0;
            this.btnGetTargetList.Text = "取得可發送清單";
            this.btnGetTargetList.UseVisualStyleBackColor = false;
            this.btnGetTargetList.Click += new System.EventHandler(this.btnGetTargetList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(23, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "訊息內容";
            // 
            // rlogBox
            // 
            this.rlogBox.Location = new System.Drawing.Point(1135, 12);
            this.rlogBox.Name = "rlogBox";
            this.rlogBox.Size = new System.Drawing.Size(572, 705);
            this.rlogBox.TabIndex = 3;
            this.rlogBox.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1730, 740);
            this.Controls.Add(this.rlogBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gLogin);
            this.Name = "Main";
            this.Text = "Main";
            this.gLogin.ResumeLayout(false);
            this.gLogin.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gLogin;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        public RichTextBox rlogBox; //for controller
        private Label lblLogin;
        private Button btnLogin;
        private Label lblName;
        private Label lblNotifyStatus;
        private Button btnNotifyApply;
        public Button btnCheckNotify;
        private RichTextBox rtbMessage;
        private ComboBox ddlTarget;
        public Button btnSendNotify;
        public Button btnGetTargetList;
        private Label label1;
        private Button btnRevokeNotify;
    }
}