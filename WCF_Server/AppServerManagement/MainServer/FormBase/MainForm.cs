using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Owin;
using Microsoft.Owin.Cors;

namespace FormBase
{
    public partial class MainForm : Form
    {
        MainLib.MainLib ServerLogicLib = new MainLib.MainLib();

        ProcessInnerMessage ProcessInnerMessage = new ProcessInnerMessage();

        System.Windows.Threading.DispatcherTimer workProcessTimer = new System.Windows.Threading.DispatcherTimer();

        


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var serverConfig = new MainLib.SeverConfig
            {
                ServerAddress = Properties.Settings.Default.ServerAddress,
                SaveCCUAPIAddress = Properties.Settings.Default.SaveCCUAPIAddress,
            };


            ProcessInnerMessage.Init(ServerLogicLib);

            FormUIManager.SetUIControl(listViewAgent, listViewRedis);

            textBoxServerAddress.Text = serverConfig.ServerAddress;
            

            var redisList = 레디스정보_리스뷰에_설정하기();


            workProcessTimer.Tick += new EventHandler(OnProcessTimedEvent);
            workProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            workProcessTimer.Start();


            ServerLogicLib.Init(serverConfig);
            ServerLogicLib.Init_Redis(redisList, checkBoxRedisStatus.Checked);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ServerLogicLib.Destory();
        }

        List<Tuple<string, int>> 레디스정보_리스뷰에_설정하기()
        {
            var redisList = new List<Tuple<string, int>>();

            foreach (var redis in Properties.Settings.Default.Redis)
            {
                var tokens = CommonLib.Util.패킷_문자열_파싱하기(redis);

                redisList.Add(new Tuple<string, int>(tokens[0], Convert.ToInt32(tokens[1])));

                ListViewItem lvi = new ListViewItem(tokens[1]);
                lvi.SubItems.Add(tokens[0]);
                lvi.SubItems.Add("연결안됨");

                listViewRedis.Items.Add(lvi);
                
            }

            listViewRedis.Refresh();

            return redisList;
        }

        private void OnProcessTimedEvent(object sender, EventArgs e)
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (true)
            {
                string msg;

                if (CommonLib.DevLog.GetLog(out msg))
                {
                    ++logWorkCount;

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

                if (logWorkCount > 7)
                {
                    break;
                }
            }


            CommonLib.InnerMsg innerMsg;
            
            if (CommonLib.InnerMessageManager.GetMsg(out innerMsg))
            {
                ProcessInnerMessage.Process(innerMsg);
            }


            MainLib.MainLib.주기적으로_모든_에이전트_접속상태_갱신();
        }

        
        

        private void buttonApp서버_실행_Click(object sender, EventArgs e)
        {
            var Items = listViewAgent.SelectedItems;

            foreach (ListViewItem lvi in Items)
            {
                ServerLogicLib.RequestStartAppServer(lvi.SubItems[0].Text);
            }
        }

        private void buttonApp서버_종료_Click(object sender, EventArgs e)
        {
            var Items = listViewAgent.SelectedItems;

            foreach (ListViewItem lvi in Items)
            {
                ServerLogicLib.RequestTerminateAppServer(lvi.SubItems[0].Text);
            }
        }

        private void buttonApp서버_파일패치_Click(object sender, EventArgs e)
        {
            var Items = listViewAgent.SelectedItems;

            foreach (ListViewItem lvi in Items)
            {
                ServerLogicLib.RequestSVNPatchAppServer(lvi.SubItems[0].Text);
            }
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

        private void checkBoxRedisStatus_CheckedChanged(object sender, EventArgs e)
        {
            ServerLogicLib.IsCheckRedisStatus = checkBoxRedisStatus.Checked;
        }

        private void btnTEST_Click(object sender, EventArgs e)
        {
            ServerLogicLib.testapi(234);
        }

        private void btnSelectRedisConnect_Click(object sender, EventArgs e)
        {
            // 최흥배. 아직 이 기능이 필요하지 않아서 일단 사용하지 않음
            var indexList = listViewRedis.SelectedIndices;
            if(indexList.Count < 0 )
            {
                return;
            }

            ServerLogicLib.ConnectRedis(indexList[0]);
        }
    }


    class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
            // Branch the pipeline here for requests that start with "/signalr"
            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new Microsoft.AspNet.SignalR.HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });

        }
    }
}
