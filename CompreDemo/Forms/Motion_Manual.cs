﻿using CSharpKit;
using Models;

namespace CompreDemo.Forms
{
    public partial class Motion_Manual : Form
    {
        Task? axisState;

        #region 需要控制的轴
        private readonly BaseAxis? baseAxis;
        #endregion

        public bool IsUpdate = false;

        public Motion_Manual(BaseAxis axis, string message = "")
        {
            InitializeComponent();
            
            baseAxis = axis;
            Text = $"{baseAxis.ControllerName} {baseAxis.Name} 轴号 {baseAxis.Number}";
            Processkit.StartTask(ref axisState, UpdateAxisState);
        }

        private void UpdateAxisState()
        {
            IsUpdate = true;
            string message = "";
            while (IsUpdate)
            {
                Thread.Sleep(100);
                try
                {
                    LB轴信息.Invoke(new Action(() =>
                    {
                        message = "";
                        if (baseAxis == null) return;
                        baseAxis.UpdateState();
                        message += $"{baseAxis.State}{Environment.NewLine}";
                        message += $"当前位置：{baseAxis.CurrentPosition}{Environment.NewLine}";
                        message += $"当前速度：{baseAxis.CurrentSpeed}{Environment.NewLine}";
                        LB轴信息.Text = message;
                    }));
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void ManualControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsUpdate = false;
        }

        private void ManualControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsUpdate = false;
        }

        private void BTN位置清零_Click(object sender, EventArgs e)
        {
            baseAxis?.DefPos();
        }

        private void BTN相对移动_Click(object sender, EventArgs e)
        {
            if (double.TryParse(TB目标位置.Text, out double position))
                baseAxis?.SingleRelativeMove(position);
            else
                baseAxis?.SingleRelativeMove(0);
        }

        private void BTN绝对移动_Click(object sender, EventArgs e)
        {
            if (double.TryParse(TB目标位置.Text, out double position))
                baseAxis?.SingleAbsoluteMove(position);
            else
                baseAxis?.SingleAbsoluteMove(0);
        }

        private void BTN后_Click(object sender, EventArgs e)
        {
            baseAxis?.Reverse();
        }

        private void BTN前_Click(object sender, EventArgs e)
        {
            baseAxis?.Forward();
        }

        private void BTN回原点_Click(object sender, EventArgs e)
        {
            baseAxis?.Datum(3);
        }

        private void BTN停止_Click(object sender, EventArgs e)
        {
            baseAxis?.Stop(2);
        }

    }
}
