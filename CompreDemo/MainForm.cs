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
                NotifyHandle.Record($"�����ʼ��ʧ�ܡ�{e.Message}", LogType.Error);
            }
        }

        private void ShowMessage(string message)
        {
            FormMethod.OnThread(TB��Ϣ, () => TB��Ϣ.Text += $"[{DateTime.Now:G}] {message}{Environment.NewLine}");
        }

        private void AutoRun_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            InitializeCurrentTest(12, GB���Խ��, currentTest);
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

        private void TSM���ƿ�����_Click(object sender, EventArgs e)
        {
            motionSetting.Show();
        }

        private void TSM�������_Click(object sender, EventArgs e)
        {
            cameraSetting.ShowDialog();
        }

        private void TSM�б�����_Click(object sender, EventArgs e)
        {
            UsingListSetting listSetting = new();
            listSetting.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void BTN��ʼ����_Click(object sender, EventArgs e)
        {
            if (FormMethod.ShowQuestionBox("�Ƿ�ʼ���ԣ�") == DialogResult.Yes)
            {
                if (autoRun.IsBusy)
                {
                    FormMethod.ShowInfoBox("�����С�");
                    return;
                }
                autoRun.RunWorkerAsync();
            }
        }

        private void BTN��ʼ��_Click(object sender, EventArgs e)
        {
            InitializeCurrentTest(12, GB���Խ��, currentTest);
        }
    }
}
