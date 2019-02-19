using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FormBase
{
    public partial class MainForm : Form
    {
        bool ServerConnected = true;

        AgentLib.MainLib mainLib = new AgentLib.MainLib();
               
        System.Windows.Threading.DispatcherTimer backGroundProcessTimer = new System.Windows.Threading.DispatcherTimer();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBoxServerAddress.Text = Properties.Settings.Default.ServerAddress;
            textBoxAppServerDirFullPath.Text = Properties.Settings.Default.AppServerDirFullPath;

            var myIP = Properties.Settings.Default.MyIP;
            
            var serverAddress = textBoxServerAddress.Text;
            
            var appServerConfig = new AgentLib.AppServerConfig
            {
                IPCUseHttp  = Properties.Settings.Default.IPC_USE_HTTP,
                IPAddress = Properties.Settings.Default.MyIP,
                AppServerName = Properties.Settings.Default.AppServerName,
                AppServerFullPathDir = Properties.Settings.Default.AppServerDirFullPath,
                AppServerExeFileName = Properties.Settings.Default.AppServerExeName,
            };
            
            var ipcConfog = new AgentLib.IPCCommuConfig
            {
                MyPort = Properties.Settings.Default.IPCMyPort,
                OtherPort = Properties.Settings.Default.IPCOtherPort,
                MaxPacketSize = Properties.Settings.Default.IPCMaxPacketSize,
                MaxPacketBufferSize = Properties.Settings.Default.IPCMaxPacketBufferSize,
            };


            backGroundProcessTimer.Tick += new EventHandler(BackgroundProcessTimedEvent);
            backGroundProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            backGroundProcessTimer.Start();

            
            mainLib.Init(true, serverAddress, appServerConfig, ipcConfog);

            CommonLib.DevLog.Write(string.Format("이 머신의 IP: {0}", myIP));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainLib.Disconnect();
        }

        private void BackgroundProcessTimedEvent(object sender, EventArgs e)
        {
            MessageToLogUI();

            서버에_자동_접속();
            
            mainLib.Update();
            
            ProcessInnerMsg();

            ServerConnectStatusUI();
        }

        void ProcessInnerMsg()
        {
            CommonLib.InnerMsg innerMsg;
            if (CommonLib.InnerMessageManager.GetMsg(out innerMsg) == false)
            {
                return;
            }

            switch (innerMsg.type)
            {
                case CommonLib.InnerMsgType.APP_SERVER_STATUS_RUNNING:
                    textBoxAppServerRunning.Text = innerMsg.value1;
                    break;

                case CommonLib.InnerMsgType.APP_SERVER_STATUS_COMMUNICATE:
                    textBoxAppServerCommunicate.Text = innerMsg.value1;
                    break;
            }
        }


        void MessageToLogUI()
        {
            while (true)
            {
                string msg;

                if (CommonLib.DevLog.GetLog(out msg))
                {
                    if (listBoxLog.Items.Count > 100)
                    {
                        listBoxLog.Items.Clear();
                    }

                    listBoxLog.Items.Add(msg);
                    listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
                }
                else
                {
                    break;
                }
            }
        }

        void 서버에_자동_접속()
        {
            if (checkBox서버에_자동접속.Checked)
            {
                mainLib.서버와_자동_접속_시도();
            }
        }
                
        void ServerConnectStatusUI()
        {
            if (mainLib.관리서버와연결중인가() == false)
            {
                if (ServerConnected)
                {
                    labelServerConnectStatus.Text = "메인 관리 서버와 미 접속";
                    ServerConnected = false;
                }
            }
            else
            {
                if (ServerConnected == false)
                {
                    labelServerConnectStatus.Text = "메인 관리 서버와 접속 중";
                    ServerConnected = true;
                }
            }
        }

        void DisConnect()
        {
            if (mainLib.Disconnect())
            {
                checkBox서버에_자동접속.Checked = false;
                CommonLib.DevLog.Write(string.Format("서버 접속 종료 !!!"));
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("서버에 아직 접속하지 않았습니다 ;;;"));
            }
        }

        // 접속하기
        private void button1_Click(object sender, EventArgs e)
        {
            if (mainLib.Connect())
            {
                CommonLib.DevLog.Write(string.Format("서버에 접속 성공!!!"));
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("서버 접속에 실패 했습니다 ;;;"));
            }
        }

        // 접속 끊기
        private void button2_Click(object sender, EventArgs e)
        {
            DisConnect();
        }

        // 테스트 메시지 보내기 
        private void button3_Click(object sender, EventArgs e)
        {
            //mainLib.TestSendMsg(textBoxTestMsg.Text);
            //mainLib.테스트_메시지보내기(textBoxTestMsg.Text);
            //AgentLib.AppServerInfo.App서버_상태();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgentLib.SVNManage.Update(textBoxSVN.Text);
        }

        private void checkBoxEnableTraceLogWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableTraceLogWrite.Checked)
            {
                CommonLib.DevLog.ChangeLogLevel(CommonLib.LOG_LEVEL.TRACE);
            }
            else
            {
                CommonLib.DevLog.ChangeLogLevel(CommonLib.LOG_LEVEL.INFO);
            }
        }

        // IPC 메시지 보내기
        private void button5_Click(object sender, EventArgs e)
        {
            var result = mainLib.IPCClientSendTest(2, textBox1.Text);
            if (result == false)
            {
                CommonLib.DevLog.Write("AppServer와 IPC Send 불가", CommonLib.LOG_LEVEL.INFO);
            }
        }
        // IPC 메시지 받기
        private void button6_Click(object sender, EventArgs e)
        {
            var packet = mainLib.IPCClientReceiveTest();
            if (packet.Item1 == 0)
            {
                return;
            }

            if (packet.Item1 == 10001)
            {
                CommonLib.DevLog.Write("AppServer와 IPC Receive 불가", CommonLib.LOG_LEVEL.INFO);
                return;
            }

            CommonLib.DevLog.Write(string.Format("받은 IPC 메시지. {0}, {1}", packet.Item1, packet.Item2), CommonLib.LOG_LEVEL.DEBUG);
        }

        
    }
}
