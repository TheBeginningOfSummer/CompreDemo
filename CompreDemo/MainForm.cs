using CompreDemo.Forms;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TSM���ƿ�����_Click(object sender, EventArgs e)
        {
            MotionSetting motionSetting = new();
            motionSetting.ShowDialog();
        }
    }
}
