using MVSCamera;

namespace CompreDemo.Forms
{
    public partial class CameraSetting : Form
    {
        readonly MVS mvs = new();

        public CameraSetting()
        {
            InitializeComponent();
        }

        private void ShowMessage(string msg)
        {
            TB信息.Invoke(new Action(() => TB信息.Text = msg));
        }

        private void BTN查找设备_Click(object sender, EventArgs e)
        {
            mvs.GetDeviceList();
            CB相机列表.Items.Clear();
            foreach (var item in mvs.DeviceNames)
                CB相机列表.Items.Add(item);
        }

        private void TSM开始采集_Click(object sender, EventArgs e)
        {
            mvs.StartGrab();
        }

        private void TSM停止采集_Click(object sender, EventArgs e)
        {
            mvs.StopGrab();
        }

        private void TSM打开设备_Click(object sender, EventArgs e)
        {
            if (mvs.DeviceList.nDeviceNum == 0 || CB相机列表.SelectedIndex == -1)
            {
                ShowMessage("No device, please select");
                return;
            }
            mvs.OpenCamera(mvs.DeviceList.pDeviceInfo[CB相机列表.SelectedIndex]);
        }

        private void TSM关闭设备_Click(object sender, EventArgs e)
        {
            mvs.CloseCamera();
        }

        private void TSM显示图片_Click(object sender, EventArgs e)
        {
            mvs.Display(PB图片.Handle);
        }
    }
}
