namespace ChatClient
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTitle = new System.Windows.Forms.Panel();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxPw = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewNetwork = new System.Windows.Forms.ListView();
            this.labelNetworkStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewRoom = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.labelRoomNumber = new System.Windows.Forms.Label();
            this.buttonRoomEnter = new System.Windows.Forms.Button();
            this.listViewChat = new System.Windows.Forms.ListView();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.buttonChatEnter = new System.Windows.Forms.Button();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.LightCoral;
            this.panelTitle.Controls.Add(this.pictureBoxClose);
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(646, 25);
            this.panelTitle.TabIndex = 0;
            this.panelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTitle_MouseMove);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClose.Image")));
            this.pictureBoxClose.Location = new System.Drawing.Point(617, 4);
            this.pictureBoxClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(23, 18);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 1;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(6, 1);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(94, 21);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Chat Client";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonChatEnter);
            this.panelMain.Controls.Add(this.textBoxChat);
            this.panelMain.Controls.Add(this.listViewChat);
            this.panelMain.Controls.Add(this.buttonRoomEnter);
            this.panelMain.Controls.Add(this.labelRoomNumber);
            this.panelMain.Controls.Add(this.label6);
            this.panelMain.Controls.Add(this.listViewRoom);
            this.panelMain.Controls.Add(this.buttonLogout);
            this.panelMain.Controls.Add(this.buttonLogin);
            this.panelMain.Controls.Add(this.textBoxPw);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.textBoxId);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.listViewNetwork);
            this.panelMain.Controls.Add(this.labelNetworkStatus);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.buttonDisconnect);
            this.panelMain.Controls.Add(this.buttonConnect);
            this.panelMain.Controls.Add(this.textBoxPort);
            this.panelMain.Controls.Add(this.textBoxIP);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(646, 650);
            this.panelMain.TabIndex = 1;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(169, 274);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(149, 39);
            this.buttonLogout.TabIndex = 20;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(7, 274);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(149, 39);
            this.buttonLogin.TabIndex = 19;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxPw
            // 
            this.textBoxPw.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPw.Location = new System.Drawing.Point(78, 227);
            this.textBoxPw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPw.Name = "textBoxPw";
            this.textBoxPw.Size = new System.Drawing.Size(240, 43);
            this.textBoxPw.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(-2, 231);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 36);
            this.label5.TabIndex = 17;
            this.label5.Text = "PW";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxId
            // 
            this.textBoxId.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxId.Location = new System.Drawing.Point(78, 184);
            this.textBoxId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(240, 43);
            this.textBoxId.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 187);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 36);
            this.label2.TabIndex = 15;
            this.label2.Text = "ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewNetwork
            // 
            this.listViewNetwork.HideSelection = false;
            this.listViewNetwork.Location = new System.Drawing.Point(11, 96);
            this.listViewNetwork.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewNetwork.Name = "listViewNetwork";
            this.listViewNetwork.Size = new System.Drawing.Size(631, 76);
            this.listViewNetwork.TabIndex = 14;
            this.listViewNetwork.UseCompatibleStateImageBehavior = false;
            // 
            // labelNetworkStatus
            // 
            this.labelNetworkStatus.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNetworkStatus.ForeColor = System.Drawing.Color.Red;
            this.labelNetworkStatus.Location = new System.Drawing.Point(163, 58);
            this.labelNetworkStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNetworkStatus.Name = "labelNetworkStatus";
            this.labelNetworkStatus.Size = new System.Drawing.Size(172, 36);
            this.labelNetworkStatus.TabIndex = 13;
            this.labelNetworkStatus.Text = "Disconnect";
            this.labelNetworkStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 36);
            this.label1.TabIndex = 12;
            this.label1.Text = "STATUS :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(551, 9);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(88, 39);
            this.buttonDisconnect.TabIndex = 11;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(460, 9);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(88, 39);
            this.buttonConnect.TabIndex = 10;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPort.Location = new System.Drawing.Point(383, 9);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(75, 43);
            this.textBoxPort.TabIndex = 9;
            this.textBoxPort.Text = "6000";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIP.Location = new System.Drawing.Point(48, 9);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(240, 43);
            this.textBoxIP.TabIndex = 8;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(290, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 36);
            this.label3.TabIndex = 7;
            this.label3.Text = "PORT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(2, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 36);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewRoom
            // 
            this.listViewRoom.HideSelection = false;
            this.listViewRoom.Location = new System.Drawing.Point(324, 184);
            this.listViewRoom.Margin = new System.Windows.Forms.Padding(2);
            this.listViewRoom.Name = "listViewRoom";
            this.listViewRoom.Size = new System.Drawing.Size(320, 86);
            this.listViewRoom.TabIndex = 21;
            this.listViewRoom.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(322, 272);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 36);
            this.label6.TabIndex = 22;
            this.label6.Text = "ROOM :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRoomNumber
            // 
            this.labelRoomNumber.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRoomNumber.ForeColor = System.Drawing.Color.White;
            this.labelRoomNumber.Location = new System.Drawing.Point(445, 272);
            this.labelRoomNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRoomNumber.Name = "labelRoomNumber";
            this.labelRoomNumber.Size = new System.Drawing.Size(119, 36);
            this.labelRoomNumber.TabIndex = 23;
            this.labelRoomNumber.Text = "-";
            this.labelRoomNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRoomEnter
            // 
            this.buttonRoomEnter.Location = new System.Drawing.Point(579, 274);
            this.buttonRoomEnter.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRoomEnter.Name = "buttonRoomEnter";
            this.buttonRoomEnter.Size = new System.Drawing.Size(65, 39);
            this.buttonRoomEnter.TabIndex = 24;
            this.buttonRoomEnter.Text = "Enter";
            this.buttonRoomEnter.UseVisualStyleBackColor = true;
            this.buttonRoomEnter.Click += new System.EventHandler(this.buttonRoomEnter_Click);
            // 
            // listViewChat
            // 
            this.listViewChat.HideSelection = false;
            this.listViewChat.Location = new System.Drawing.Point(7, 317);
            this.listViewChat.Margin = new System.Windows.Forms.Padding(2);
            this.listViewChat.Name = "listViewChat";
            this.listViewChat.Size = new System.Drawing.Size(637, 263);
            this.listViewChat.TabIndex = 25;
            this.listViewChat.UseCompatibleStateImageBehavior = false;
            // 
            // textBoxChat
            // 
            this.textBoxChat.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxChat.Location = new System.Drawing.Point(7, 584);
            this.textBoxChat.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(541, 43);
            this.textBoxChat.TabIndex = 26;
            // 
            // buttonChatEnter
            // 
            this.buttonChatEnter.Location = new System.Drawing.Point(552, 584);
            this.buttonChatEnter.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChatEnter.Name = "buttonChatEnter";
            this.buttonChatEnter.Size = new System.Drawing.Size(92, 43);
            this.buttonChatEnter.TabIndex = 27;
            this.buttonChatEnter.Text = "Enter";
            this.buttonChatEnter.UseVisualStyleBackColor = true;
            this.buttonChatEnter.Click += new System.EventHandler(this.buttonChatEnter_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(646, 675);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "ChatClient";
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelNetworkStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewNetwork;
        private System.Windows.Forms.TextBox textBoxPw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonRoomEnter;
        private System.Windows.Forms.Label labelRoomNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listViewRoom;
        private System.Windows.Forms.Button buttonChatEnter;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.ListView listViewChat;
    }
}

