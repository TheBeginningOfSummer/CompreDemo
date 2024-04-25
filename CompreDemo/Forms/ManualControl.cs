using Services;

namespace CompreDemo.Forms
{
    public partial class ManualControl : Form
    {
        #region 需要控制的轴
        private readonly BaseAxis? baseAxis;
        #endregion

        public bool IsUpdate = false;

        public ManualControl(BaseAxis axis, string message = "")
        {
            InitializeComponent();

            baseAxis = axis;
        }

        private void UpdateAxisState()
        {
            while (IsUpdate)
            {
                Thread.Sleep(100);
                LB轴信息.Invoke(new Action(() =>
                {
                    if (baseAxis == null) return;
                    baseAxis.UpdateState();
                    LB轴信息.Text += $"轴{baseAxis.Number}{baseAxis.State}{Environment.NewLine}";
                    LB轴信息.Text += $"轴{baseAxis.Number}当前位置：{baseAxis.CurrentPosition}{Environment.NewLine}";
                    LB轴信息.Text += $"轴{baseAxis.Number}当前速度：{baseAxis.CurrentSpeed}{Environment.NewLine}";
                }));

                //double[] @in = Controllers[controllerName]?.GetInputs(inputCount);
                //for (int i = 0; i < @in.Length; i++)
                //{
                //    if (i % 3 == 0) stateInfo[3] += Environment.NewLine;
                //    stateInfo[3] += $"信号{i}：{@in[i]} ";
                //}
                //UpdateState?.Invoke(stateInfo);
            }
        }

        private void BTN位置清零_Click(object sender, EventArgs e)
        {

        }
    }
}
