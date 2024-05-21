using CompreDemo.Forms;
using CSharpKit.FileManagement;
using ThridLibray;

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
        }

        private void TSM���ƿ�����_Click(object sender, EventArgs e)
        {
            motionSetting.Show();
        }

        private void TSM�������_Click(object sender, EventArgs e)
        {
            cameraSetting.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
