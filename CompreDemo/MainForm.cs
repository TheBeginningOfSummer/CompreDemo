using CompreDemo.Forms;
using Services;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        readonly MotionSetting motionSetting = new();
        readonly CameraSetting cameraSetting = new();

        public MainForm()
        {
            InitializeComponent();
            device.ErrorAction += ShowError;
            autoRun.DoWork += AutoRun_DoWork;
        }

        private void ShowError(string message)
        {
            TB信息.Invoke(() => { TB信息.Text += message + Environment.NewLine; });
        }

        private void AutoRun_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            device.AutoRun3("Device1", 0, 0, 50, 100);
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

        private void BTN自动运行_Click(object sender, EventArgs e)
        {
            if (autoRun.IsBusy)
            {
                FormMethod.ShowInfoBox("运行中。");
                return;
            }
            autoRun.RunWorkerAsync();
        }

        
    }
}
