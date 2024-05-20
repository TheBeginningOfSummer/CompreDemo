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
                    //�Ῠ���³�ʼ��
                    controller1.Initialize();
                    //�����³�ʼ�����������Ӻ��Handle
                    controller1.ReinitializeAxes();
                    //��������³�ʼ��
                    foreach (var item in controller1.Axes.Values)
                        item.Initialize();
                }
                else
                {
                    //���ӿ�����ʧ��
                }
            }
            else
            {
                //�õ�������ʧ��
            }
            if (device.CameraList!.TryGetValue("cam1", out var camera1))
            {
                camera1.OpenCamera();
                camera1.Device?.TriggerSet.Open(TriggerSourceEnum.Software);
                //Task.Run(camera1.WaitImage);//�����������ȡͼ�����ڷǴ���ģʽ
            }
            else
            {
                FileManager.AppendLog("Log", "�����¼", "�������ʧ�ܡ�");
            }
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
