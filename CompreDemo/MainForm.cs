using CompreDemo.Forms;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TSMøÿ÷∆ø®≈‰÷√_Click(object sender, EventArgs e)
        {
            MotionSetting motionSetting = new();
            motionSetting.ShowDialog();
        }
    }
}
