using CompreDemo.Forms;
using CompreDemo.Models;
using Services;
using ViewModels;
using static CSharpKit.FileManagement.NotifyRecord;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        readonly ViewModel viewModel;
        Tray currentTest = new();

        public MainForm()
        {
            InitializeComponent();

            try
            {
                Notify += ShowMessage;
                autoRun.DoWork += AutoRun_DoWork;
                device.InitializeDevices();
            }
            catch (Exception e)
            {
                Record($"程序初始化失败。{e.Message}", LogType.Error);
            }

            viewModel = new();
        }

        private void ShowMessage(string message)
        {
            FormKit.OnThread(TB信息, () => TB信息.Text += $"[{DateTime.Now:G}] {message}{Environment.NewLine}");
        }

        private void AutoRun_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            InitializeCurrentTest(12, GB测试结果, currentTest);
            //device.AutoRun3("Device1", 0, 0, 50, 100);
        }

        public static void InitializeCurrentTest(int count, Control control, Tray testTray)
        {
            control.Controls.Clear();
            List<Point> location = FormKit.GetLocation(30, 30, count, 3, 25, 25);
            testTray = new(count);
            for (int i = 0; i < testTray.Tests.Count; i++)
            {
                FormKit.OnThread(testTray.Tests[i + 1].Status, () => testTray.Tests[i + 1].Status.Location = location[i]);
                FormKit.OnThread(control, () => control.Controls.Add(testTray.Tests[i + 1].Status));
            }
        }

        private void TSM控制卡配置_Click(object sender, EventArgs e)
        {
            viewModel.SettingMotion.Show();
        }

        private void TSM相机配置_Click(object sender, EventArgs e)
        {
            viewModel.SettingCamera.ShowDialog();
        }

        private void TSM设备方案设置_Click(object sender, EventArgs e)
        {
            UsingPlan listSetting = new();
            listSetting.Show();
        }

        private void BTN开始测试_Click(object sender, EventArgs e)
        {
            if (FormKit.ShowQuestionBox("是否开始测试？") == DialogResult.Yes)
            {
                if (autoRun.IsBusy)
                {
                    FormKit.ShowInfoBox("运行中。");
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
