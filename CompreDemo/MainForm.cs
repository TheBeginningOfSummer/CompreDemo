using CompreDemo.Forms;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;

        public MainForm()
        {
            InitializeComponent();
        }

        private void TSM���ƿ�����_Click(object sender, EventArgs e)
        {
            MotionSetting motionSetting = new();
            motionSetting.Show();
        }

        private void TSM�������_Click(object sender, EventArgs e)
        {
            CameraSetting cameraSetting = new();
            cameraSetting.ShowDialog();
        }
    }
}
