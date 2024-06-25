using Models;

namespace CompreDemo.Forms
{
    public partial class Setting_Motion : Form
    {
        
        public Setting_Motion()
        {
            InitializeComponent();
        }

        private void MotionSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void UpdateIO(MotionControl motion, int inputCount, int outputCount)
        {
            //while (!cancellation.IsCancellationRequested)
            //{
            //    Thread.Sleep(100);
            //    string input = "";
            //    double[] @in = motion.GetInputs(inputCount, out bool isComplete);
            //    for (int i = 0; i < @in.Length; i++)
            //    {
            //        if (i % 3 == 0)
            //            input += Environment.NewLine;
            //        input += $"输入{i}：{@in[i]} ";
            //    }
            //    //LB输入.Invoke(new Action(() => { LB输入.Text = input; }));
            //}
        }
        

        
    }
}
