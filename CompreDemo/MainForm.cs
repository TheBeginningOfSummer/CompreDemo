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
            autoRun.DoWork += AutoRun_DoWork;
        }

        private void AutoRun_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            device.Track3(device.Controllers["Zmotion1"], device.CameraList["cam1"], 0, 0, 50, 100);
        }

        private void CameraEvent()
        {
            Task.Run(() => { });
            //camera.Device.ExecuteSoftwareTrigger();
            //var image = camera.CatchImage();
            //if (image == null) { FormMethod.ShowInfoBox("捕获图像失败。"); return; }
            //Cv2.ImShow("测试", BitmapConverter.ToMat(image));
        }

        private void TSM控制卡配置_Click(object sender, EventArgs e)
        {
            motionSetting.Show();
        }

        private void TSM相机配置_Click(object sender, EventArgs e)
        {
            cameraSetting.ShowDialog();
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
