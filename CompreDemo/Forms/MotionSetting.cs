using CSharpKit;

namespace CompreDemo.Forms
{
    public partial class MotionSetting : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;

        public MotionSetting()
        {
            InitializeComponent();

            CB轴卡.Items.Clear();
            foreach (var controller in device.Controllers!.Values)
                CB轴卡.Items.Add(controller.Name);
        }

        private void UpdateInfo(string controllerName, Label label)
        {
            //轴卡加载
            var controller = device.GetController(controllerName);
            if (controller == null) return;

            TBIP地址.Text = controller.IP;

            #region 轴卡信息显示
            string axes = $"{Environment.NewLine}轴列表：{Environment.NewLine}";
            for (int i = 0; i < controller.AxesName.Count; i++)
            {
                axes += $"        {controller.AxesName[i]}  [{i}]{Environment.NewLine}";
            }
            label.Text = $"控制器：{controllerName}{Environment.NewLine}" +
                $"{Environment.NewLine}IP地址：{controller.IP}{Environment.NewLine}{axes}" +
                $"{Environment.NewLine}类型：{controller.GetType().ToString().Split('.').LastOrDefault()}";
            #endregion

            CB轴.Items.Clear();
            foreach (var axisName in controller.AxesName)
                CB轴.Items.Add(axisName);
        }

        private void CB轴卡_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB轴卡.Text == null) return;
            UpdateInfo(CB轴卡.Text, LB轴卡信息);
        }

        private void BTN轴卡设置_Click(object sender, EventArgs e)
        {
            List<string> axes = [.. TB轴名称.Text.Split(';')];
            if (device.ModifyInfo(CB轴卡.Text, TBIP地址.Text, axes, CB控制卡类型.Text))
                MessageBox.Show("已修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateInfo(CB轴卡.Text, LB轴卡信息);
            CB轴卡.Items.Clear();
            foreach (var controller in device.Controllers!.Values)
                CB轴卡.Items.Add(controller.Name);
        }

        private void BTN轴删除_Click(object sender, EventArgs e)
        {
            if (device.RemoveInfo(CB轴卡.Text, TB轴名称.Text))
                MessageBox.Show("已删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateInfo(CB轴卡.Text, LB轴卡信息);
            CB轴卡.Items.Clear();
            foreach (var controller in device.Controllers!.Values)
                CB轴卡.Items.Add(controller.Name);
        }

        private void BTN轴设置_Click(object sender, EventArgs e)
        {
            var currentAxis = device.GetAxis(CB轴卡.Text, CB轴.Text);
            if (currentAxis == null) return;
            Setting axissetting = new(currentAxis, "Layout1");
            axissetting.Show();
        }

        private void BTN轴控制_Click(object sender, EventArgs e)
        {
            var currentAxis = device.GetAxis(CB轴卡.Text, CB轴.Text);
            if (currentAxis == null) return;
            ManualControl manualControl = new ManualControl(currentAxis);
            manualControl.Show();
        }

        private void TSM连接_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CB轴卡.Text)) return;
            device.Connect(CB轴卡.Text);
        }

        private void TSM断开_Click(object sender, EventArgs e)
        {
            device.Disconnect(CB轴卡.Text);
        }

        private void TSM打开测试窗口_Click(object sender, EventArgs e)
        {
            var axis1 = device.GetAxis(CB轴卡.Text, TST测试轴1名称.Text);
            var axis2 = device.GetAxis(CB轴卡.Text, TST测试轴2名称.Text);
            if (axis1 == null || axis2 == null) return;
            MotionTest motionTest = new(axis1, axis2);
            motionTest.Show();
        }

        private void TSM自动轨迹测试_Click(object sender, EventArgs e)
        {
            var motion = device.GetController(CB轴卡.Text);
            if (motion == null) return;
            //Task.Run(() => { DeviceManager.Track1(motion, 0, 7, 400, 50); });

            Task.Run(() => { DeviceManager.Track2(motion, 300, 5, 300, 50); });
        }
    }
}
