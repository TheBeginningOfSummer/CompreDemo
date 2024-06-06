using CompreDemo.Forms;
using CompreDemo.Models;
using CompreDemo.Services;
using Services;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        readonly MotionSetting motionSetting = new();
        readonly CameraSetting cameraSetting = new();
        Tray currentTest = new();

        public MainForm()
        {
            InitializeComponent();
            try
            {
                NotifyHandle.Notify += ShowMessage;
                autoRun.DoWork += AutoRun_DoWork;
                device.InitializeDevices("Device1");
                motionSetting.Initialize();
                cameraSetting.Initialize();
            }
            catch (Exception e)
            {
                NotifyHandle.Record($"程序初始化失败。{e.Message}", LogType.Error);
            }
        }

        private void ShowMessage(string message)
        {
            FormMethod.OnThread(TB信息, () => TB信息.Text += $"[{DateTime.Now:G}] {message}{Environment.NewLine}");
        }

        private void AutoRun_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            InitializeCurrentTest(12, GB测试结果, currentTest);
            //device.AutoRun3("Device1", 0, 0, 50, 100);
        }

        public static void InitializeCurrentTest(int count, Control control, Tray testTray)
        {
            control.Controls.Clear();
            List<Point> location = FormMethod.SetLocation(30, 30, count, 3, 25, 25);
            testTray = new(count);
            for (int i = 0; i < testTray.Tests.Count; i++)
            {
                FormMethod.OnThread(testTray.Tests[i + 1].Status, () => testTray.Tests[i + 1].Status.Location = location[i]);
                FormMethod.OnThread(control, () => control.Controls.Add(testTray.Tests[i + 1].Status));
            }
        }

        private void TSM控制卡配置_Click(object sender, EventArgs e)
        {
            motionSetting.Show();
        }

        private void TSM相机配置_Click(object sender, EventArgs e)
        {
            cameraSetting.ShowDialog();
        }

        private void TSM列表设置_Click(object sender, EventArgs e)
        {
            UsingListSetting listSetting = new();
            listSetting.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void BTN开始测试_Click(object sender, EventArgs e)
        {
            if (FormMethod.ShowQuestionBox("是否开始测试？") == DialogResult.Yes)
            {
                if (autoRun.IsBusy)
                {
                    FormMethod.ShowInfoBox("运行中。");
                    return;
                }
                autoRun.RunWorkerAsync();
            }
        }

        private void BTN初始化_Click(object sender, EventArgs e)
        {
            InitializeCurrentTest(12, GB测试结果, currentTest);
        }
    }
}
