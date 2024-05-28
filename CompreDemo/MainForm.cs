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
            TB��Ϣ.Invoke(() => { TB��Ϣ.Text += message + Environment.NewLine; });
        }

        private void AutoRun_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            device.AutoRun3("Device1", 0, 0, 50, 100);
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

        private void BTN�Զ�����_Click(object sender, EventArgs e)
        {
            if (autoRun.IsBusy)
            {
                FormMethod.ShowInfoBox("�����С�");
                return;
            }
            autoRun.RunWorkerAsync();
        }

        
    }
}
