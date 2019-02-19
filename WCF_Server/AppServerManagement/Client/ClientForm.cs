using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        Communicator Communi = new Communicator();

        System.Windows.Threading.DispatcherTimer backGroundProcessTimer = new System.Windows.Threading.DispatcherTimer();


        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            textBoxServerAddress.Text = Properties.Settings.Default.ServerAddress;
            textBoxUserName.Text = Properties.Settings.Default.UserName;

            backGroundProcessTimer.Tick += new EventHandler(BackgroundProcessTimedEvent);
            backGroundProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            backGroundProcessTimer.Start();

            Communi.Init(textBoxServerAddress.Text);

            CommonLib.DevLog.Write(string.Format("이 머신의 IP: {0}", CommonLib.Util.IPString(true)));
        }

        private void BackgroundProcessTimedEvent(object sender, EventArgs e)
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


            CommonLib.InnerMsg innerMsg;

            if (CommonLib.InnerMessageManager.GetMsg(out innerMsg))
            {
                ProcessInnerMsg(innerMsg);
            }
        }

        // 접속하기
        private void button1_Click(object sender, EventArgs e)
        {
            if (Communi.Connect(textBoxUserName.Text))
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
            if (Communi.Disconnect())
            {
                listViewAgent.Items.Clear();
                listViewAgent.Refresh();

                CommonLib.DevLog.Write(string.Format("서버 접속 종료 !!!"));
            }
            else
            {
                CommonLib.DevLog.Write(string.Format("서버에 아직 접속하지 않았습니다 ;;;"));
            }
        }


        void ProcessInnerMsg(CommonLib.InnerMsg msg)
        {
            switch (msg.type)
            {
                case CommonLib.InnerMsgType.AGENT_INFO_LIST:
                    ProcessInnerMsgAgentList(jsonFormatMsg: msg.value1);
                    break;

                case CommonLib.InnerMsgType.AGENT_REMOVED:
                    ProcessInnerMsgRemoveAgent(msg.connectionID);
                    break;

                case CommonLib.InnerMsgType.RECEIVE_REDIS_CHECK_RESULT:
                    ProcessInnerMsgRedisStatusResult(jsonFormatMsg: msg.value1);
                    break;
            }
        }

        void ProcessInnerMsgAgentList(string jsonFormatMsg)
        {
            var agentList = JsonConvert.DeserializeObject<CommonLib.MsgAgentInfoToClientList>(jsonFormatMsg);

            foreach(var agent in agentList.list)
            {
                
                bool 새로운_에이전트 = true;

                foreach (ListViewItem lvi in listViewAgent.Items)
                {
                    if (lvi.SubItems[0].Text == agent.ConnectionID)
                    {
                        새로운_에이전트 = false;

                        lvi.SubItems[2].Text = agent.AppServer실행중 ? "실행" : "중지";
                        lvi.SubItems[3].Text = agent.전체_CPU_사용량;
                        lvi.SubItems[4].Text = agent.AppServer메모리_사용량;
                        lvi.SubItems[5].Text = AppServer상태표시(agent);
                        break;
                    }
                }

                if (새로운_에이전트)
                {
                    ListViewItem lvi = new ListViewItem(agent.ConnectionID);
                    lvi.SubItems.Add(agent.IPAddress);
                    lvi.SubItems.Add(agent.AppServer실행중 ? "실행" : "중지");
                    lvi.SubItems.Add(agent.전체_CPU_사용량);
                    lvi.SubItems.Add(agent.AppServer메모리_사용량);
                    lvi.SubItems.Add(AppServer상태표시(agent));

                    listViewAgent.Items.Add(lvi);
                }
            }
            
            listViewAgent.Refresh();
        }

        string AppServer상태표시(CommonLib.MsgAgentInfoToClient msg)
        {
            var AppServer통신가능_여부 = "N";
            if (msg.Agent와AppServer통신가능_여부)
            {
                AppServer통신가능_여부 = "Y";
            }

            return string.Format("Agent-GameServer 통신:{0}", AppServer통신가능_여부);
        }

        void ProcessInnerMsgRemoveAgent(string connectionID)
        {
            foreach (ListViewItem lvi in listViewAgent.Items)
            {
                if (lvi.SubItems[0].Text == connectionID)
                {
                    listViewAgent.Items.Remove(lvi);
                    break;
                }
            }

            listViewAgent.Refresh();
        }

        void ProcessInnerMsgRedisStatusResult(string jsonFormatMsg)
        {
            var result = JsonConvert.DeserializeObject<Tuple<string, string, string>>(jsonFormatMsg);

            
            bool 새로운_레디스 = true;

            foreach (ListViewItem lvi in listViewRedis.Items)
            {
                if (lvi.SubItems[0].Text == result.Item2 && lvi.SubItems[1].Text == result.Item1)
                {
                    새로운_레디스 = false;

                    lvi.SubItems[2].Text = result.Item3;
                    break;
                }
            }

            if (새로운_레디스)
            {
                ListViewItem lvi = new ListViewItem(result.Item2);
                lvi.SubItems.Add(result.Item1);
                lvi.SubItems.Add(result.Item3);
                listViewRedis.Items.Add(lvi);
            }
            
            listViewRedis.Refresh();
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

        private void buttonApp서버_실행_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAgent.SelectedItems)
            {
                Communi.관리서버에_AppServer실행요청(lvi.SubItems[0].Text);
            }
        }

        private void buttonApp서버_종료_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAgent.SelectedItems)
            {
                Communi.관리서버에_AppServer종료요청(lvi.SubItems[0].Text);
            }
        }

        private void buttonApp서버_파일패치_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewAgent.SelectedItems)
            {
                Communi.관리서버에_AppServerSVN패치요청(lvi.SubItems[0].Text);
            }
        }

    }
}
