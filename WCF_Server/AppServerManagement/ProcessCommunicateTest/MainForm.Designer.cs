namespace ProcessCommunicateTest
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonA1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAPort = new System.Windows.Forms.TextBox();
            this.buttonA2 = new System.Windows.Forms.Button();
            this.buttonA3 = new System.Windows.Forms.Button();
            this.textBoxAReceiveMessage = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBReceiveMessage = new System.Windows.Forms.TextBox();
            this.buttonB3 = new System.Windows.Forms.Button();
            this.buttonB2 = new System.Windows.Forms.Button();
            this.buttonB1 = new System.Windows.Forms.Button();
            this.textBoxBPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAReceiveMessage);
            this.groupBox1.Controls.Add(this.buttonA3);
            this.groupBox1.Controls.Add(this.buttonA2);
            this.groupBox1.Controls.Add(this.buttonA1);
            this.groupBox1.Controls.Add(this.textBoxAPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process A";
            // 
            // buttonA1
            // 
            this.buttonA1.Location = new System.Drawing.Point(19, 48);
            this.buttonA1.Name = "buttonA1";
            this.buttonA1.Size = new System.Drawing.Size(133, 23);
            this.buttonA1.TabIndex = 1;
            this.buttonA1.Text = "button1";
            this.buttonA1.UseVisualStyleBackColor = true;
            this.buttonA1.Click += new System.EventHandler(this.buttonA1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port:";
            // 
            // textBoxAPort
            // 
            this.textBoxAPort.Location = new System.Drawing.Point(52, 21);
            this.textBoxAPort.Name = "textBoxAPort";
            this.textBoxAPort.Size = new System.Drawing.Size(65, 21);
            this.textBoxAPort.TabIndex = 3;
            this.textBoxAPort.Text = "25673";
            // 
            // buttonA2
            // 
            this.buttonA2.Location = new System.Drawing.Point(19, 77);
            this.buttonA2.Name = "buttonA2";
            this.buttonA2.Size = new System.Drawing.Size(133, 23);
            this.buttonA2.TabIndex = 4;
            this.buttonA2.Text = "button2";
            this.buttonA2.UseVisualStyleBackColor = true;
            this.buttonA2.Click += new System.EventHandler(this.buttonA2_Click);
            // 
            // buttonA3
            // 
            this.buttonA3.Location = new System.Drawing.Point(19, 106);
            this.buttonA3.Name = "buttonA3";
            this.buttonA3.Size = new System.Drawing.Size(133, 23);
            this.buttonA3.TabIndex = 5;
            this.buttonA3.Text = "button3";
            this.buttonA3.UseVisualStyleBackColor = true;
            this.buttonA3.Click += new System.EventHandler(this.buttonA3_Click);
            // 
            // textBoxAReceiveMessage
            // 
            this.textBoxAReceiveMessage.Location = new System.Drawing.Point(158, 21);
            this.textBoxAReceiveMessage.Multiline = true;
            this.textBoxAReceiveMessage.Name = "textBoxAReceiveMessage";
            this.textBoxAReceiveMessage.Size = new System.Drawing.Size(269, 108);
            this.textBoxAReceiveMessage.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxBReceiveMessage);
            this.groupBox2.Controls.Add(this.buttonB3);
            this.groupBox2.Controls.Add(this.buttonB2);
            this.groupBox2.Controls.Add(this.buttonB1);
            this.groupBox2.Controls.Add(this.textBoxBPort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 139);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Process B";
            // 
            // textBoxBReceiveMessage
            // 
            this.textBoxBReceiveMessage.Location = new System.Drawing.Point(158, 21);
            this.textBoxBReceiveMessage.Multiline = true;
            this.textBoxBReceiveMessage.Name = "textBoxBReceiveMessage";
            this.textBoxBReceiveMessage.Size = new System.Drawing.Size(269, 108);
            this.textBoxBReceiveMessage.TabIndex = 6;
            // 
            // buttonB3
            // 
            this.buttonB3.Location = new System.Drawing.Point(19, 106);
            this.buttonB3.Name = "buttonB3";
            this.buttonB3.Size = new System.Drawing.Size(133, 23);
            this.buttonB3.TabIndex = 5;
            this.buttonB3.Text = "button4";
            this.buttonB3.UseVisualStyleBackColor = true;
            this.buttonB3.Click += new System.EventHandler(this.buttonB3_Click);
            // 
            // buttonB2
            // 
            this.buttonB2.Location = new System.Drawing.Point(19, 77);
            this.buttonB2.Name = "buttonB2";
            this.buttonB2.Size = new System.Drawing.Size(133, 23);
            this.buttonB2.TabIndex = 4;
            this.buttonB2.Text = "button5";
            this.buttonB2.UseVisualStyleBackColor = true;
            this.buttonB2.Click += new System.EventHandler(this.buttonB2_Click);
            // 
            // buttonB1
            // 
            this.buttonB1.Location = new System.Drawing.Point(19, 48);
            this.buttonB1.Name = "buttonB1";
            this.buttonB1.Size = new System.Drawing.Size(133, 23);
            this.buttonB1.TabIndex = 1;
            this.buttonB1.Text = "button6";
            this.buttonB1.UseVisualStyleBackColor = true;
            this.buttonB1.Click += new System.EventHandler(this.buttonB1_Click);
            // 
            // textBoxBPort
            // 
            this.textBoxBPort.Location = new System.Drawing.Point(52, 21);
            this.textBoxBPort.Name = "textBoxBPort";
            this.textBoxBPort.Size = new System.Drawing.Size(65, 21);
            this.textBoxBPort.TabIndex = 3;
            this.textBoxBPort.Text = "25674";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 323);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxAReceiveMessage;
        private System.Windows.Forms.Button buttonA3;
        private System.Windows.Forms.Button buttonA2;
        private System.Windows.Forms.Button buttonA1;
        private System.Windows.Forms.TextBox textBoxAPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxBReceiveMessage;
        private System.Windows.Forms.Button buttonB3;
        private System.Windows.Forms.Button buttonB2;
        private System.Windows.Forms.Button buttonB1;
        private System.Windows.Forms.TextBox textBoxBPort;
        private System.Windows.Forms.Label label2;
    }
}

