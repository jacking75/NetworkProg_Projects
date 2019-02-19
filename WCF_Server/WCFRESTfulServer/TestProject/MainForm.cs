using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using APILogicLib;
using DB = APILogicLib.DB;

namespace TestProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void WriteUILog(string msg)
        {
            listBoxLog.Items.Add(msg);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        // 레디스 연결
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = DB.Redis.Init(textBoxRedisAddress.Text);
                if (result == ERROR_ID.NONE)
                {
                    WriteUILog("Redis 접속 성공");

                    button2.Enabled = button3.Enabled = button4.Enabled = true;
                    button7.Enabled = button6.Enabled = button5.Enabled = true;
                    button8.Enabled = button9.Enabled = button10.Enabled = button11.Enabled = button12.Enabled = true;
                }
                else
                {
                    WriteUILog(string.Format("레디스 접속 실패. {0}", result));
                }
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
            
        }

        const string REDIS_INT_KEY = "test_int";
        const string REDIS_DOUBLE_KEY = "test_double";
        const string REDIS_STRING_KEY = "test_string";
                
        // 레디스 테스트(int, float, string) 추가
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    await DB.Redis.SetString<int>(REDIS_INT_KEY, textBoxRedisTestInt.Text.ToInt32());
                    WriteUILog(string.Format("String Set. {0} : {1}", REDIS_INT_KEY, textBoxRedisTestInt.Text));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    await DB.Redis.SetString<double>(REDIS_DOUBLE_KEY, textBoxRedisTestDouble.Text.ToDouble());
                    WriteUILog(string.Format("String Set. {0} : {1}", REDIS_DOUBLE_KEY, textBoxRedisTestDouble.Text));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    await DB.Redis.SetString<string>(REDIS_STRING_KEY, textBoxRedisTestString.Text);
                    WriteUILog(string.Format("String Set. {0} : {1}", REDIS_STRING_KEY, textBoxRedisTestString.Text));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
        }

        // 레디스 테스트(int, float, string) 검색
        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    var value = await DB.Redis.GetString<int>(REDIS_INT_KEY);
                    WriteUILog(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_INT_KEY, value.Item2, value.Item1));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    var value = await DB.Redis.GetString<double>(REDIS_DOUBLE_KEY);
                    WriteUILog(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_DOUBLE_KEY, value.Item2, value.Item1));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    var value = await DB.Redis.GetString<string>(REDIS_STRING_KEY);
                    WriteUILog(string.Format("String Get. {0} : {1}. Result:{2}", REDIS_STRING_KEY, value.Item2, value.Item1));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
        }

        // 레디스 테스트(int, float, string) 삭제
        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRedisTestInt.Text) == false)
                {
                    var value = await DB.Redis.DeleteString<int>(REDIS_INT_KEY);
                    WriteUILog(string.Format("String Delete. {0} : result({1})", REDIS_INT_KEY, value));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestDouble.Text) == false)
                {
                    var value = await DB.Redis.DeleteString<double>(REDIS_DOUBLE_KEY);
                    WriteUILog(string.Format("String Delete. {0} : result({1})", REDIS_DOUBLE_KEY, value));
                }

                if (string.IsNullOrEmpty(textBoxRedisTestString.Text) == false)
                {
                    var value = await DB.Redis.DeleteString<string>(REDIS_STRING_KEY);
                    WriteUILog(string.Format("String Delete. {0} : result({1})", REDIS_STRING_KEY, value));
                }

                textBoxRedisTestInt.Text = textBoxRedisTestDouble.Text = textBoxRedisTestString.Text = "";
            }
            catch (Exception ex)
            {
                WriteUILog(ex.ToString());
            }
        }


        const string REDIS_PERSION_KEY = "test_persion";

        // 레디스 테스트(PERSION) 추가
        private async void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestPName.Text) ||
                string.IsNullOrEmpty(textBoxRedisTestPAge.Text))
            {
                WriteUILog("Error: 이름이나 나이가 빈 값입니다");
                return;
            }

            var persion = new PERSION() { Name = textBoxRedisTestPName.Text, Age = textBoxRedisTestPAge.Text.ToInt32() };

            await DB.Redis.SetString<PERSION>(REDIS_PERSION_KEY, persion);
            WriteUILog(string.Format("PERSION Set. {0} : {1}, {2}", REDIS_PERSION_KEY, persion.Name, persion.Age));
        }
        // 레디스 테스트(PERSION) 검색
        private async void button6_Click(object sender, EventArgs e)
        {
            var value = await DB.Redis.GetString<PERSION>(REDIS_PERSION_KEY);
            WriteUILog(string.Format("PERSION Get. {0} : {1}, {2}. Result:{3}", REDIS_PERSION_KEY, value.Item2.Name, value.Item2.Age, value.Item1));
        }
        // 레디스 테스트(PERSION) 삭제
        private async void button5_Click(object sender, EventArgs e)
        {
            var value = await DB.Redis.DeleteString<PERSION>(REDIS_PERSION_KEY);
            WriteUILog(string.Format("PERSION Delete. {0} : result({1})", REDIS_PERSION_KEY, value));
        }


        const string REDIS_LIST_KEY = "test_list";

        // 레디스 테스트(List) 추가
        private async void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestList.Text))
            {
                WriteUILog("Error: 빈 값입니다");
                return;
            }

            var value = await DB.Redis.AddList<string>(REDIS_LIST_KEY, textBoxRedisTestList.Text);
            WriteUILog(string.Format("List 추가. {0} : {1}. Count:{2})", REDIS_LIST_KEY, textBoxRedisTestList.Text, value));
        }
        // 레디스 테스트(List) 검색
        private async void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestListR1.Text) ||
                string.IsNullOrEmpty(textBoxRedisTestListR2.Text))
            {
                var value = await DB.Redis.GetList<string>(REDIS_LIST_KEY, 0);
                WriteUILog(string.Format("List 추가. {0} : {1})", REDIS_LIST_KEY, string.Join(",", value)));
            }
            else
            {
                int pos1 = textBoxRedisTestListR1.Text.ToInt32();
                int pos2 = textBoxRedisTestListR2.Text.ToInt32();
                var value = await DB.Redis.GetList<string>(REDIS_LIST_KEY, pos1, pos2);
                WriteUILog(string.Format("List 추가. {0} : {1})", REDIS_LIST_KEY, string.Join(",", value)));
            }
        }
        // 레디스 테스트(List) 삭제
        private async void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRedisTestListCount.Text))
            {
                WriteUILog("Error: 빈 값입니다");
                return;
            }
            else
            {
                var deleteValue = textBoxRedisTestList.Text;
                int count = textBoxRedisTestListCount.Text.ToInt32();
                var value = await DB.Redis.DeleteList<string>(REDIS_LIST_KEY, deleteValue, count);
                WriteUILog(string.Format("List 삭제. {0} : {1})", REDIS_LIST_KEY, value));
            }
        }
        // 레디스 테스트(List) 삭제. 왼쪽에서 Pop
        private async void button11_Click(object sender, EventArgs e)
        {
            var value = await DB.Redis.DeleteList<string>(REDIS_LIST_KEY, true);
            WriteUILog(string.Format("List 왼쪽에서 Pop. {0} : {1})", REDIS_LIST_KEY, value));
        }
        // 레디스 테스트(List) 삭제. 오른쪽에서 Pop
        private async void button12_Click(object sender, EventArgs e)
        {
            var value = await DB.Redis.DeleteList<string>(REDIS_LIST_KEY, false);
            WriteUILog(string.Format("List 오른쪽에서 Pop. {0} : {1})", REDIS_LIST_KEY, value));
        }


        // 계정생성
        private async void button13_Click(object sender, EventArgs e)
        {
            var result = await ServerRequestAPI.RequestAPI(REQUEST_API_TYPE.LOGIN);

            if (result == ERROR_ID.NONE)
            {
                //listBoxLog.Items.Add(string.Format("로그인 성공. AT:{0}, LSeq:{1}", userInfo.AuthToken, userInfo.LoginSeq));
            }
            else
            {
                //listBoxLog.Items.Add(string.Format("로그인 요청 에러. {0}", (short)result));
            }
        }

        // 로그인
        private void button14_Click(object sender, EventArgs e)
        {

        }
    }


    struct PERSION
    {
        public string Name;
        public int Age;
    }
}
