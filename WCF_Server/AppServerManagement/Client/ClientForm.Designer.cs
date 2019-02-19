namespace Client
{
    partial class ClientForm
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
            this.listViewAgent = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.checkBoxEnableTraceLogWrite = new System.Windows.Forms.CheckBox();
            this.buttonApp서버_파일패치 = new System.Windows.Forms.Button();
            this.buttonApp서버_종료 = new System.Windows.Forms.Button();
            this.buttonApp서버_실행 = new System.Windows.Forms.Button();
            this.listViewRedis = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewAgent
            // 
            this.listViewAgent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewAgent.FullRowSelect = true;
            this.listViewAgent.Location = new System.Drawing.Point(12, 72);
            this.listViewAgent.Name = "listViewAgent";
            this.listViewAgent.Size = new System.Drawing.Size(655, 205);
            this.listViewAgent.TabIndex = 19;
            this.listViewAgent.UseCompatibleStateImageBehavior = false;
            this.listViewAgent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ConnID";
            this.columnHeader1.Width = 74;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 110;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "실행";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 36;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "CPU";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 45;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "메모리(M)";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "App 서버 상태";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 240;
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(12, 292);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(490, 208);
            this.listBoxLog.TabIndex = 18;
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(317, 9);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.ReadOnly = true;
            this.textBoxServerAddress.Size = new System.Drawing.Size(175, 21);
            this.textBoxServerAddress.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "서버 주소:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(560, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 21);
            this.button2.TabIndex = 21;
            this.button2.Text = "끊기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(498, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 21);
            this.button1.TabIndex = 20;
            this.button1.Text = "접속";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.label2.Location = new System.Drawing.Point(10, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "접속자 이름:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxUserName.Location = new System.Drawing.Point(122, 11);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(109, 33);
            this.textBoxUserName.TabIndex = 25;
            // 
            // checkBoxEnableTraceLogWrite
            // 
            this.checkBoxEnableTraceLogWrite.AutoSize = true;
            this.checkBoxEnableTraceLogWrite.Checked = true;
            this.checkBoxEnableTraceLogWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableTraceLogWrite.Location = new System.Drawing.Point(12, 50);
            this.checkBoxEnableTraceLogWrite.Name = "checkBoxEnableTraceLogWrite";
            this.checkBoxEnableTraceLogWrite.Size = new System.Drawing.Size(108, 16);
            this.checkBoxEnableTraceLogWrite.TabIndex = 26;
            this.checkBoxEnableTraceLogWrite.Text = "trace 로그 출력";
            this.checkBoxEnableTraceLogWrite.UseVisualStyleBackColor = true;
            this.checkBoxEnableTraceLogWrite.CheckedChanged += new System.EventHandler(this.checkBoxEnableTraceLogWrite_CheckedChanged);
            // 
            // buttonApp서버_파일패치
            // 
            this.buttonApp서버_파일패치.Location = new System.Drawing.Point(444, 36);
            this.buttonApp서버_파일패치.Name = "buttonApp서버_파일패치";
            this.buttonApp서버_파일패치.Size = new System.Drawing.Size(124, 23);
            this.buttonApp서버_파일패치.TabIndex = 29;
            this.buttonApp서버_파일패치.Text = "App서버 파일 패치";
            this.buttonApp서버_파일패치.UseVisualStyleBackColor = true;
            this.buttonApp서버_파일패치.Click += new System.EventHandler(this.buttonApp서버_파일패치_Click);
            // 
            // buttonApp서버_종료
            // 
            this.buttonApp서버_종료.Location = new System.Drawing.Point(349, 36);
            this.buttonApp서버_종료.Name = "buttonApp서버_종료";
            this.buttonApp서버_종료.Size = new System.Drawing.Size(89, 23);
            this.buttonApp서버_종료.TabIndex = 28;
            this.buttonApp서버_종료.Text = "App서버종료";
            this.buttonApp서버_종료.UseVisualStyleBackColor = true;
            this.buttonApp서버_종료.Click += new System.EventHandler(this.buttonApp서버_종료_Click);
            // 
            // buttonApp서버_실행
            // 
            this.buttonApp서버_실행.Location = new System.Drawing.Point(254, 36);
            this.buttonApp서버_실행.Name = "buttonApp서버_실행";
            this.buttonApp서버_실행.Size = new System.Drawing.Size(89, 23);
            this.buttonApp서버_실행.TabIndex = 27;
            this.buttonApp서버_실행.Text = "App서버실행";
            this.buttonApp서버_실행.UseVisualStyleBackColor = true;
            this.buttonApp서버_실행.Click += new System.EventHandler(this.buttonApp서버_실행_Click);
            // 
            // listViewRedis
            // 
            this.listViewRedis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listViewRedis.FullRowSelect = true;
            this.listViewRedis.Location = new System.Drawing.Point(508, 292);
            this.listViewRedis.MultiSelect = false;
            this.listViewRedis.Name = "listViewRedis";
            this.listViewRedis.Size = new System.Drawing.Size(231, 208);
            this.listViewRedis.TabIndex = 30;
            this.listViewRedis.UseCompatibleStateImageBehavior = false;
            this.listViewRedis.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Port";
            this.columnHeader9.Width = 37;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "IP";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 46;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "상태";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 97;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 512);
            this.Controls.Add(this.listViewRedis);
            this.Controls.Add(this.buttonApp서버_파일패치);
            this.Controls.Add(this.buttonApp서버_종료);
            this.Controls.Add(this.buttonApp서버_실행);
            this.Controls.Add(this.checkBoxEnableTraceLogWrite);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxServerAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewAgent);
            this.Controls.Add(this.listBoxLog);
            this.Name = "ClientForm";
            this.Text = "관리 서버 클라이언트";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewAgent;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxEnableTraceLogWrite;
        private System.Windows.Forms.Button buttonApp서버_파일패치;
        private System.Windows.Forms.Button buttonApp서버_종료;
        private System.Windows.Forms.Button buttonApp서버_실행;
        private System.Windows.Forms.ListView listViewRedis;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
    }
}

