using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBase
{
    class FormUIManager
    {
        static System.Windows.Forms.ListView ListViewAgent;
        static System.Windows.Forms.ListView ListViewRedis;


        public static void SetUIControl(System.Windows.Forms.ListView listViewAgent,
                                        System.Windows.Forms.ListView listViewRedis)
        {
            ListViewAgent = listViewAgent;
            ListViewRedis = listViewRedis;
        }

        public static void AddAgentListView(CommonLib.InnerMsgAgentStatus agentStatus)
        {
            var lvi = new System.Windows.Forms.ListViewItem(agentStatus.connectID);
            lvi.SubItems.Add(agentStatus.IP);
            lvi.SubItems.Add(agentStatus.AppServerName);
            lvi.SubItems.Add(agentStatus.AppServerFullPath);
            lvi.SubItems.Add("N");
            lvi.SubItems.Add(agentStatus.전체_CPU_사용량);
            lvi.SubItems.Add(agentStatus.프로세스_CPU_사용량);
            lvi.SubItems.Add(agentStatus.AppServer메모리_사용량);
            lvi.SubItems.Add("모름");

            ListViewAgent.Items.Add(lvi);
            ListViewAgent.Refresh();
        }

        public static void RemoveAgentListView(string connectionID)
        {
            foreach (System.Windows.Forms.ListViewItem lvi in ListViewAgent.Items)
            {
                if (lvi.SubItems[0].Text == connectionID)
                {
                    ListViewAgent.Items.Remove(lvi);
                    return;
                }
            }
        }

        public static void ChangeAgentListView(string connectionID, CommonLib.MsgAppServerStatus msg)
        {
            foreach (System.Windows.Forms.ListViewItem lvi in ListViewAgent.Items)
            {
                if (lvi.SubItems[0].Text == connectionID)
                {
                    var 서버실행중 = "N";

                    if (msg.AppServer실행중)
                    {
                        서버실행중 = "Y";
                    }

                    if (lvi.SubItems[4].Text == "Y" && 서버실행중 == "N")
                    {
                        var agent = MainLib.MainLib.GetAgent(connectionID);

                        if (agent == null || agent.정상_AppServer_종료_여부(DateTime.Now) == false)
                        {
                            lvi.BackColor = System.Drawing.Color.Red;
                        }
                    }
                    else if (lvi.SubItems[4].Text == "N" && 서버실행중 == "Y")
                    {
                        lvi.BackColor = System.Drawing.Color.White;
                    }

                    lvi.SubItems[4].Text = 서버실행중;
                    lvi.SubItems[5].Text = msg.전체_CPU_사용량;
                    lvi.SubItems[6].Text = msg.프로세스_CPU_사용량;
                    lvi.SubItems[7].Text = msg.AppServer메모리_사용량;
                    lvi.SubItems[8].Text = AppServer상태표시(msg);
                    return;
                }
            }

            ListViewAgent.Refresh();
        }

        public static void ChangeAgentListView(string connectionID, string notifyMsg)
        {
            foreach (System.Windows.Forms.ListViewItem lvi in ListViewAgent.Items)
            {
                if (lvi.SubItems[0].Text == connectionID)
                {
                    lvi.BackColor = System.Drawing.Color.DimGray;
                    lvi.SubItems[7].Text = notifyMsg;
                    return;
                }
            }

            ListViewAgent.Refresh();
        }

        static string AppServer상태표시(CommonLib.MsgAppServerStatus msg)
        {
            var AppServer통신가능_여부 = "N";
            if (msg.Agent와AppServer통신가능_여부)
            {
                AppServer통신가능_여부 = "Y";
            }

            return string.Format("App 서버와 통신:{0}, 현재인원:{1}", AppServer통신가능_여부.ToString(), msg.UserCount);
        }



        public static void ChangeRedisStatus(Tuple<string, string, string> newStatus)
        {
            foreach (System.Windows.Forms.ListViewItem lvi in ListViewRedis.Items)
            {
                if (lvi.SubItems[0].Text == newStatus.Item2 && lvi.SubItems[1].Text == newStatus.Item1)
                {
                    lvi.SubItems[2].Text = newStatus.Item3;
                }
            }

            ListViewRedis.Refresh();
        }

    }
}
