using CompreDemo.Forms;
using CSharpKit.FileManagement;
using ThridLibray;

namespace CompreDemo
{
    public partial class MainForm : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;

        public MainForm()
        {
            InitializeComponent();
        }

        public static void InitializeDevices(DeviceManager device)
        {
            Enumerator.EnumerateDevices();
            device.LoadControllerConfig();
            device.LoadCameraConfig();
            if (device.Controllers!.TryGetValue("Zmotion1", out var controller1))
            {
                if (controller1.Connect())
                {
                    //轴卡重新初始化
                    controller1.Initialize();
                    //轴重新初始化，加载连接后的Handle
                    controller1.ReinitializeAxes();
                    //轴参数重新初始化
                    foreach (var item in controller1.Axes.Values)
                        item.Initialize();
                }
                else
                {
                    //连接控制器失败
                }
            }
            else
            {
                //得到控制器失败
            }
            if (device.CameraList!.TryGetValue("cam1", out var camera1))
            {
                camera1.OpenCamera();
                camera1.Device?.TriggerSet.Open(TriggerSourceEnum.Software);
                //Task.Run(camera1.WaitImage);//连续向队列中取图，用于非触发模式
            }
            else
            {
                FileManager.AppendLog("Log", "错误记录", "相机加载失败。");
            }
        }

        private void TSM控制卡配置_Click(object sender, EventArgs e)
        {
            MotionSetting motionSetting = new();
            motionSetting.Show();
        }

        private void TSM相机配置_Click(object sender, EventArgs e)
        {
            CameraSetting cameraSetting = new();
            cameraSetting.ShowDialog();
        }
    }
}
