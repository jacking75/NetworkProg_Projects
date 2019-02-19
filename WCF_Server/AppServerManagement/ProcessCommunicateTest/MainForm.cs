using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessCommunicateTest
{
    public partial class MainForm : Form
    {
        ProcessCommunicate.ProcessCommu ProcessCommuA = new ProcessCommunicate.ProcessCommu();
        ProcessCommunicate.ProcessCommu ProcessCommuB = new ProcessCommunicate.ProcessCommu();

        System.Windows.Threading.DispatcherTimer DispatcherTimer = new System.Windows.Threading.DispatcherTimer();




        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ProcessCommuA.Init(Int16.Parse(textBoxAPort.Text), Int16.Parse(textBoxBPort.Text), 256, (256*2));
            ProcessCommuB.Init(Int16.Parse(textBoxBPort.Text), Int16.Parse(textBoxAPort.Text), 256, (256*2));
            
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 124);
            DispatcherTimer.Start();
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var packetA = ProcessCommuA.GetPacketData();
            if (packetA != null)
            {
                textBoxAReceiveMessage.Text = string.Format("ID:{0} \r\n Data:{1}", packetA.Item1, packetA.Item2);
            }

            var packetB = ProcessCommuB.GetPacketData();
            if (packetB != null)
            {
                textBoxBReceiveMessage.Text = string.Format("ID:{0} \r\n Data:{1}", packetB.Item1, packetB.Item2);
            }
        }

        private void buttonA1_Click(object sender, EventArgs e)
        {
            var message = new TestJsonData1 { N1 = 11, N2 = 12, Message ="ProcessA: 버튼 1이 클럭 되었다. ㅋㅋㅋㅋㅋ" };
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            ProcessCommuA.SendMessage(1, jsonstring);
        }

        private void buttonA2_Click(object sender, EventArgs e)
        {
            var message = new TestJsonData2 { N1 = 21, N2 = 22 };
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            ProcessCommuA.SendMessage(2, jsonstring);
        }

        private void buttonA3_Click(object sender, EventArgs e)
        {
            var message = new TestJsonData3 { Message = "ProcessA: 버튼 3이 클럭 되었다. ㅋㅋㅋㅋㅋ" };
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            ProcessCommuA.SendMessage(1, jsonstring);
        }

        private void buttonB1_Click(object sender, EventArgs e)
        {
            var message = new TestJsonData1 { N1 = 11, N2 = 12, Message = "ProcessB: 버튼 1이 클럭 되었다. ㅋㅋㅋㅋㅋ" };
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            ProcessCommuB.SendMessage(1, jsonstring);
        }

        private void buttonB2_Click(object sender, EventArgs e)
        {
            var message = new TestJsonData2 { N1 = 21, N2 = 22 };
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            ProcessCommuB.SendMessage(2, jsonstring);
        }

        private void buttonB3_Click(object sender, EventArgs e)
        {
            var message = new TestJsonData3 { Message = "ProcessB: 버튼 3이 클럭 되었다. ㅋㅋㅋㅋㅋ" };
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            ProcessCommuB.SendMessage(1, jsonstring);
        }


        class TestJsonData1
        {
            public int N1;
            public int N2;
            public string Message;
        }

        class TestJsonData2
        {
            public int N1;
            public int N2;
        }

        class TestJsonData3
        {
            public string Message;
        }
    }
}
