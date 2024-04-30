using CSharpKit;
using Services;

namespace CompreDemo.Forms
{
    public partial class MotionTest : Form
    {
        #region 笔刷
        readonly Graphics graphics;
        Pen pen = new Pen(Color.LightBlue, 2);
        SolidBrush brush1 = new SolidBrush(Color.Red);
        SolidBrush brush2;
        #endregion

        bool isDraw = true;
        Task? drawing;

        public MotionTest(BaseAxis axis1, BaseAxis axis2)
        {
            InitializeComponent();
            graphics = CreateGraphics();
            brush2 = new SolidBrush(BackColor);
            Processkit.StartTask(ref drawing, () => { DrawingTrack(axis1, axis2, 100); });
        }

        private void MotionTest_Paint(object sender, PaintEventArgs e)
        {
            //防止窗口加载时Graphics被线程占用
            DrawCoordinate(e.Graphics);
        }

        private void MotionTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            isDraw = false;
            //e.Cancel = true;
            //Hide();
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
                    graphics.DrawString((step * i).ToString(), new Font("宋体", 10), brush, new PointF(step * i, y));
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
                    graphics.DrawString((step * i).ToString(), new Font("宋体", 10), brush, new PointF(x, step * i));
                }
            }
            else
            {

            }
        }

        public void DrawCoordinate(Graphics graphics, int x = 1000, int y = 850, int step = 100)
        {
            Pen pen = new(Color.Green, 2);
            DrawHLine(graphics, pen, 0, x, 0, step);
            DrawVLine(graphics, pen, 0, y, 0, step);
        }

        public void DrawingTrack(BaseAxis axis1, BaseAxis axis2, float offset = 0)
        {
            if (axis1 == null || axis2 == null) return;
            isDraw = true;
            float x = offset;
            float y = offset;
            while (isDraw)
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

        public void StartDrawing(BaseAxis axis1, BaseAxis axis2)
        {
            Processkit.StartTask(ref drawing, () => { DrawingTrack(axis1, axis2); });
        }

        public void Clear()
        {
            //graphics.Clear(Color.White);
            Refresh();
        }

        private void TSM清除_Click(object sender, EventArgs e)
        {
            Clear();
            //DrawCoordinate(graphics);
        }
    }
}
