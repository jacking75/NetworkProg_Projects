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
            this.checkBoxEnableTraceLogWrite = new System.Windows.Forms.CheckBox();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonApp서버_실행 = new System.Windows.Forms.Button();
            this.buttonApp서버_종료 = new System.Windows.Forms.Button();
            this.listViewAgent = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonApp서버_파일패치 = new System.Windows.Forms.Button();
            this.listViewRedis = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxRedisStatus = new System.Windows.Forms.CheckBox();
            this.btnTEST = new System.Windows.Forms.Button();
            this.btnSelectRedisConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxEnableTraceLogWrite
            // 
            this.checkBoxEnableTraceLogWrite.AutoSize = true;
            this.checkBoxEnableTraceLogWrite.Checked = true;
            this.checkBoxEnableTraceLogWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableTraceLogWrite.Location = new System.Drawing.Point(12, 42);
            this.checkBoxEnableTraceLogWrite.Name = "checkBoxEnableTraceLogWrite";
            this.checkBoxEnableTraceLogWrite.Size = new System.Drawing.Size(108, 16);
            this.checkBoxEnableTraceLogWrite.TabIndex = 13;
            this.checkBoxEnableTraceLogWrite.Text = "trace 로그 출력";
            this.checkBoxEnableTraceLogWrite.UseVisualStyleBackColor = true;
            this.checkBoxEnableTraceLogWrite.CheckedChanged += new System.EventHandler(this.checkBoxEnableTraceLogWrite_CheckedChanged);
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(67, 12);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.ReadOnly = true;
            this.textBoxServerAddress.Size = new System.Drawing.Size(175, 21);
            this.textBoxServerAddress.TabIndex = 12;
            this.textBoxServerAddress.Text = "http://*:9080";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "서버 주소:";
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(12, 422);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(894, 172);
            this.listBoxLog.TabIndex = 10;
            // 
            // buttonApp서버_실행
            // 
            this.buttonApp서버_실행.Location = new System.Drawing.Point(268, 12);
            this.buttonApp서버_실행.Name = "buttonApp서버_실행";
            this.buttonApp서버_실행.Size = new System.Drawing.Size(110, 23);
            this.buttonApp서버_실행.TabIndex = 14;
            this.buttonApp서버_실행.Text = "App서버실행";
            this.buttonApp서버_실행.UseVisualStyleBackColor = true;
            this.buttonApp서버_실행.Click += new System.EventHandler(this.buttonApp서버_실행_Click);
            // 
            // buttonApp서버_종료
            // 
            this.buttonApp서버_종료.Location = new System.Drawing.Point(382, 12);
            this.buttonApp서버_종료.Name = "buttonApp서버_종료";
            this.buttonApp서버_종료.Size = new System.Drawing.Size(127, 23);
            this.buttonApp서버_종료.TabIndex = 16;
            this.buttonApp서버_종료.Text = "App서버종료";
            this.buttonApp서버_종료.UseVisualStyleBackColor = true;
            this.buttonApp서버_종료.Click += new System.EventHandler(this.buttonApp서버_종료_Click);
            // 
            // listViewAgent
            // 
            this.listViewAgent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader12});
            this.listViewAgent.FullRowSelect = true;
            this.listViewAgent.Location = new System.Drawing.Point(12, 64);
            this.listViewAgent.Name = "listViewAgent";
            this.listViewAgent.Size = new System.Drawing.Size(1245, 342);
            this.listViewAgent.TabIndex = 17;
            this.listViewAgent.UseCompatibleStateImageBehavior = false;
            this.listViewAgent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "connectID";
            this.columnHeader1.Width = 92;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "App 서버 이름";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "설치 디렉토리";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 250;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "실행";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 36;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "머신 CPU%";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 87;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "App CPU%";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 77;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "메모리(M)";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 72;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "App 서버 상태";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 400;
            // 
            // buttonApp서버_파일패치
            // 
            this.buttonApp서버_파일패치.Location = new System.Drawing.Point(515, 12);
            this.buttonApp서버_파일패치.Name = "buttonApp서버_파일패치";
            this.buttonApp서버_파일패치.Size = new System.Drawing.Size(162, 23);
            this.buttonApp서버_파일패치.TabIndex = 18;
            this.buttonApp서버_파일패치.Text = "App서버 파일 패치";
            this.buttonApp서버_파일패치.UseVisualStyleBackColor = true;
            this.buttonApp서버_파일패치.Click += new System.EventHandler(this.buttonApp서버_파일패치_Click);
            // 
            // listViewRedis
            // 
            this.listViewRedis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listViewRedis.FullRowSelect = true;
            this.listViewRedis.Location = new System.Drawing.Point(912, 443);
            this.listViewRedis.MultiSelect = false;
            this.listViewRedis.Name = "listViewRedis";
            this.listViewRedis.Size = new System.Drawing.Size(345, 151);
            this.listViewRedis.TabIndex = 19;
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
            this.columnHeader10.Width = 97;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "상태";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 186;
            // 
            // checkBoxRedisStatus
            // 
            this.checkBoxRedisStatus.AutoSize = true;
            this.checkBoxRedisStatus.Checked = true;
            this.checkBoxRedisStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRedisStatus.Location = new System.Drawing.Point(913, 421);
            this.checkBoxRedisStatus.Name = "checkBoxRedisStatus";
            this.checkBoxRedisStatus.Size = new System.Drawing.Size(112, 16);
            this.checkBoxRedisStatus.TabIndex = 20;
            this.checkBoxRedisStatus.Text = "Redis 상태 조사";
            this.checkBoxRedisStatus.UseVisualStyleBackColor = true;
            this.checkBoxRedisStatus.CheckedChanged += new System.EventHandler(this.checkBoxRedisStatus_CheckedChanged);
            // 
            // btnTEST
            // 
            this.btnTEST.Location = new System.Drawing.Point(1182, 5);
            this.btnTEST.Name = "btnTEST";
            this.btnTEST.Size = new System.Drawing.Size(75, 23);
            this.btnTEST.TabIndex = 21;
            this.btnTEST.Text = "TEST";
            this.btnTEST.UseVisualStyleBackColor = true;
            this.btnTEST.Click += new System.EventHandler(this.btnTEST_Click);
            // 
            // btnSelectRedisConnect
            // 
            this.btnSelectRedisConnect.Location = new System.Drawing.Point(1142, 416);
            this.btnSelectRedisConnect.Name = "btnSelectRedisConnect";
            this.btnSelectRedisConnect.Size = new System.Drawing.Size(115, 25);
            this.btnSelectRedisConnect.TabIndex = 22;
            this.btnSelectRedisConnect.Text = "선택한 Redis 연결";
            this.btnSelectRedisConnect.UseVisualStyleBackColor = true;
            this.btnSelectRedisConnect.Visible = false;
            this.btnSelectRedisConnect.Click += new System.EventHandler(this.btnSelectRedisConnect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 607);
            this.Controls.Add(this.btnSelectRedisConnect);
            this.Controls.Add(this.btnTEST);
            this.Controls.Add(this.checkBoxRedisStatus);
            this.Controls.Add(this.listViewRedis);
            this.Controls.Add(this.buttonApp서버_파일패치);
            this.Controls.Add(this.listViewAgent);
            this.Controls.Add(this.buttonApp서버_종료);
            this.Controls.Add(this.buttonApp서버_실행);
            this.Controls.Add(this.checkBoxEnableTraceLogWrite);
            this.Controls.Add(this.textBoxServerAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "관리 서버";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxEnableTraceLogWrite;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonApp서버_실행;
        private System.Windows.Forms.Button buttonApp서버_종료;
        private System.Windows.Forms.ListView listViewAgent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button buttonApp서버_파일패치;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ListView listViewRedis;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.CheckBox checkBoxRedisStatus;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Button btnTEST;
        private System.Windows.Forms.Button btnSelectRedisConnect;
    }
}

