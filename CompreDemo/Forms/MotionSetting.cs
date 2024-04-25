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

        }

        private void TSM打开测试窗口_Click(object sender, EventArgs e)
        {
            MotionTest motionTest = new MotionTest();
            motionTest.Show();
        }
    }
}
