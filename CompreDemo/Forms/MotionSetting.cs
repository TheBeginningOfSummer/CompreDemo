using CSharpKit;
using Services;

namespace CompreDemo.Forms
{
    public partial class MotionSetting : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        Task? testMotion;
        CancellationTokenSource cancellation = new();

        public MotionSetting()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            UpdateCB();
        }

        public void UpdateCB()
        {
            CB轴卡.Items.Clear();
            foreach (var controller in device.Controllers.Values)
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
                axes += $"            {controller.AxesName[i]}  [{i}]{Environment.NewLine}";
            }
            label.Text = $"控制器：{controllerName}{Environment.NewLine}" +
                $"{Environment.NewLine}IP地址：{controller.IP}{Environment.NewLine}{axes}" +
                $"{Environment.NewLine}类型：{controller.GetType().ToString().Split('.').LastOrDefault()}";
            #endregion

            CB轴.Items.Clear();
            foreach (var axisName in controller.AxesName)
                CB轴.Items.Add(axisName);
        }

        private void UpdateIO(MotionControl motion, int inputCount, int outputCount)
        {
            while (!cancellation.IsCancellationRequested)
            {
                Thread.Sleep(100);
                string input = "";
                double[] @in = motion.GetInputs(inputCount, out bool isComplete);
                for (int i = 0; i < @in.Length; i++)
                {
                    if (i % 3 == 0)
                        input += Environment.NewLine;
                    input += $"输入{i}：{@in[i]} ";
                }
                //LB输入.Invoke(new Action(() => { LB输入.Text = input; }));
            }
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
            Setting axisSetting = new(currentAxis, "Layout1");
            axisSetting.Show();
        }

        private void BTN轴控制_Click(object sender, EventArgs e)
        {
            var currentAxis = device.GetAxis(CB轴卡.Text, CB轴.Text);
            if (currentAxis == null) return;
            ManualControl manualControl = new ManualControl(currentAxis);
            manualControl.Show();
        }

        private void TSM打开测试窗口_Click(object sender, EventArgs e)
        {
            string[] deviceList = device.UsingDevices[usingDevice];
            if (deviceList.Length < 3) return;
            if (!device.Controllers.TryGetValue(deviceList[0], out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[1], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[2], out var axis2)) return;
            if (axis1 == null || axis2 == null) return;

            MotionTest motionTest = new(axis1, axis2);
            motionTest.Show();
        }

        private void TSM自动轨迹测试_Click(object sender, EventArgs e)
        {
            switch (TST轨迹.Text)
            {
                case "0":
                    Processkit.StartTask(ref testMotion, new Action(() => device.AutoRun1("Device2", 7, 0, 0, 50, 400)));
                    break;
                case "1":
                    Processkit.StartTask(ref testMotion, new Action(() => device.AutoRun2("Device2", 5, 0, 0, 50, 300)));
                    break;
                case "2":
                    if (testMotion != null)
                        if (!testMotion.IsCompleted) break;
                    Processkit.StartTask(ref testMotion, new Action(() => device.AutoRun3("Device1", 12, 0, 0, 3, 50, 100)));
                    break;
            }
        }

        private void TSM连接当前卡_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CB轴卡.Text)) return;
            if (device.Connect(CB轴卡.Text))
                MessageBox.Show("连接成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TSM断开当前卡_Click(object sender, EventArgs e)
        {
            device.Disconnect(CB轴卡.Text);
            MessageBox.Show("断开连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MotionSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
