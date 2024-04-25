using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompreDemo.Forms
{
    public partial class MotionTest : Form
    {
        readonly Graphics graphics;
        Pen pen = new Pen(Color.LightBlue, 2);
        SolidBrush brush1 = new SolidBrush(Color.Red);
        SolidBrush brush2;

        public bool IsDraw = true;

        public MotionTest()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            brush2 = new SolidBrush(BackColor);
        }

        public void DrawingTrack(BaseAxis axis1, BaseAxis axis2, float offset = 0)
        {
            if (axis1 == null || axis2 == null) return;
            IsDraw = true;
            float x = offset;
            float y = offset;
            while (IsDraw)
            {
                float m = x;
                float n = y;
                Thread.Sleep(10);
                graphics.FillEllipse(brush2, m + 1, n, 4, 4);
                x = (float)axis1.CurrentPosition + offset;
                y = (float)axis2.CurrentPosition + offset;
                graphics.FillEllipse(brush1, x + 1, y, 4, 4);
                graphics.DrawLine(pen, m, n, x, y);
            }
        }

        private void DrawHLine(Graphics graphics, Pen pen, int x1, int x2, int y, int step = 100)
        {
            graphics.DrawLine(pen, x1, y, x2, y);
            var brush = new SolidBrush(Color.Green);
            int length = x2 - x1;
            if (length > 0)
            {
                for (int i = 0; i < length / step; i++)
                {
                    graphics.DrawLine(pen, step * i, y, step * i, y + 5);
                    graphics.DrawString((step * i).ToString(), new Font("宋体", 10), brush, new PointF(step * i, 5));
                }
            }
            else
            {

            }
        }

        private void DrawVLine(Graphics graphics, Pen pen, int y1, int y2, int x, int step = 100)
        {
            graphics.DrawLine(pen, x, y1, x, y2);
            var brush = new SolidBrush(Color.Green);
            int length = y2 - y1;
            if (length > 0)
            {
                for (int i = 0; i < length / step; i++)
                {
                    if (i == 0) continue;
                    graphics.DrawLine(pen, x, step * i, x + 5, step * i);
                    graphics.DrawString((step * i).ToString(), new Font("宋体", 10), brush, new PointF(5, step * i));
                }
            }
            else
            {

            }
        }

        public void Clear()
        {
            graphics.Clear(Color.White);
        }

        private void MotionTest_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Green, 2);
            DrawHLine(graphics, pen, 0, 1000, 0, 100);
            DrawVLine(graphics, pen, 0, 1000, 0, 100);
        }
    }
}
