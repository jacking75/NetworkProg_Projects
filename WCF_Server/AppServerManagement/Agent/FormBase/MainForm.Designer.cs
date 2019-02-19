namespace FormBase
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxTestMsg = new System.Windows.Forms.TextBox();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.checkBoxEnableTraceLogWrite = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAppServerDirFullPath = new System.Windows.Forms.TextBox();
            this.checkBox서버에_자동접속 = new System.Windows.Forms.CheckBox();
            this.textBoxSVN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxAppServerRunning = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxAppServerCommunicate = new System.Windows.Forms.TextBox();
            this.labelServerConnectStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(679, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 21);
            this.button3.TabIndex = 15;
            this.button3.Text = "테스트";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxTestMsg
            // 
            this.textBoxTestMsg.Location = new System.Drawing.Point(574, 7);
            this.textBoxTestMsg.Name = "textBoxTestMsg";
            this.textBoxTestMsg.Size = new System.Drawing.Size(99, 21);
            this.textBoxTestMsg.TabIndex = 14;
            this.textBoxTestMsg.Text = "test11";
            this.textBoxTestMsg.Visible = false;
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(72, 8);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(175, 21);
            this.textBoxServerAddress.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "서버 주소:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(315, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 21);
            this.button2.TabIndex = 11;
            this.button2.Text = "끊기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 21);
            this.button1.TabIndex = 10;
            this.button1.Text = "접속";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(14, 194);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(723, 268);
            this.listBoxLog.TabIndex = 9;
            // 
            // checkBoxEnableTraceLogWrite
            // 
            this.checkBoxEnableTraceLogWrite.AutoSize = true;
            this.checkBoxEnableTraceLogWrite.Checked = true;
            this.checkBoxEnableTraceLogWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableTraceLogWrite.Location = new System.Drawing.Point(13, 172);
            this.checkBoxEnableTraceLogWrite.Name = "checkBoxEnableTraceLogWrite";
            this.checkBoxEnableTraceLogWrite.Size = new System.Drawing.Size(108, 16);
            this.checkBoxEnableTraceLogWrite.TabIndex = 16;
            this.checkBoxEnableTraceLogWrite.Text = "trace 로그 출력";
            this.checkBoxEnableTraceLogWrite.UseVisualStyleBackColor = true;
            this.checkBoxEnableTraceLogWrite.CheckedChanged += new System.EventHandler(this.checkBoxEnableTraceLogWrite_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "App 서버 디렉토리:";
            // 
            // textBoxAppServerDirFullPath
            // 
            this.textBoxAppServerDirFullPath.Location = new System.Drawing.Point(124, 32);
            this.textBoxAppServerDirFullPath.Name = "textBoxAppServerDirFullPath";
            this.textBoxAppServerDirFullPath.Size = new System.Drawing.Size(611, 21);
            this.textBoxAppServerDirFullPath.TabIndex = 18;
            // 
            // checkBox서버에_자동접속
            // 
            this.checkBox서버에_자동접속.AutoSize = true;
            this.checkBox서버에_자동접속.Checked = true;
            this.checkBox서버에_자동접속.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox서버에_자동접속.Location = new System.Drawing.Point(14, 86);
            this.checkBox서버에_자동접속.Name = "checkBox서버에_자동접속";
            this.checkBox서버에_자동접속.Size = new System.Drawing.Size(140, 16);
            this.checkBox서버에_자동접속.TabIndex = 19;
            this.checkBox서버에_자동접속.Text = "서버에 자동 접속하기";
            this.checkBox서버에_자동접속.UseVisualStyleBackColor = true;
            // 
            // textBoxSVN
            // 
            this.textBoxSVN.Location = new System.Drawing.Point(124, 59);
            this.textBoxSVN.Name = "textBoxSVN";
            this.textBoxSVN.Size = new System.Drawing.Size(436, 21);
            this.textBoxSVN.TabIndex = 21;
            this.textBoxSVN.Text = "D:\\Temp\\test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "svn 주소:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(566, 59);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(123, 21);
            this.button4.TabIndex = 22;
            this.button4.Text = "SVN Update 테스트";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBoxAppServerRunning
            // 
            this.textBoxAppServerRunning.Enabled = false;
            this.textBoxAppServerRunning.Location = new System.Drawing.Point(6, 21);
            this.textBoxAppServerRunning.Name = "textBoxAppServerRunning";
            this.textBoxAppServerRunning.Size = new System.Drawing.Size(136, 21);
            this.textBoxAppServerRunning.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAppServerCommunicate);
            this.groupBox1.Controls.Add(this.textBoxAppServerRunning);
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 53);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "App 서버 상태";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(349, 19);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(45, 22);
            this.button6.TabIndex = 27;
            this.button6.Text = "받기";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(293, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 22);
            this.button5.TabIndex = 26;
            this.button5.Text = "보내기";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(281, 21);
            this.textBox1.TabIndex = 25;
            // 
            // textBoxAppServerCommunicate
            // 
            this.textBoxAppServerCommunicate.Enabled = false;
            this.textBoxAppServerCommunicate.Location = new System.Drawing.Point(148, 21);
            this.textBoxAppServerCommunicate.Name = "textBoxAppServerCommunicate";
            this.textBoxAppServerCommunicate.Size = new System.Drawing.Size(136, 21);
            this.textBoxAppServerCommunicate.TabIndex = 24;
            // 
            // labelServerConnectStatus
            // 
            this.labelServerConnectStatus.AutoSize = true;
            this.labelServerConnectStatus.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelServerConnectStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelServerConnectStatus.Location = new System.Drawing.Point(365, 11);
            this.labelServerConnectStatus.Name = "labelServerConnectStatus";
            this.labelServerConnectStatus.Size = new System.Drawing.Size(207, 15);
            this.labelServerConnectStatus.TabIndex = 25;
            this.labelServerConnectStatus.Text = "메인 관리 서버와 접속 여부";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Location = new System.Drawing.Point(335, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 48);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AppServer과 IPC 통신 테스트";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 470);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelServerConnectStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBoxSVN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox서버에_자동접속);
            this.Controls.Add(this.textBoxAppServerDirFullPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxEnableTraceLogWrite);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBoxTestMsg);
            this.Controls.Add(this.textBoxServerAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Agent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxTestMsg;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.CheckBox checkBoxEnableTraceLogWrite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAppServerDirFullPath;
        private System.Windows.Forms.CheckBox checkBox서버에_자동접속;
        private System.Windows.Forms.TextBox textBoxSVN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxAppServerRunning;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxAppServerCommunicate;
        private System.Windows.Forms.Label labelServerConnectStatus;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

