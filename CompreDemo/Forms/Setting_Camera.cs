using ThridLibray;

namespace CompreDemo.Forms
{
    public partial class Setting_Camera : Form
    {

        public Setting_Camera()
        {
            InitializeComponent();
        }

        private void CameraSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        #region 链接事件
        //相机打开回调
        private void OnCameraOpen(object? sender, EventArgs e)
        {
            var device = (IDevice?)sender;

        }
        //相机丢失回调
        private void OnConnectLoss(object? sender, EventArgs e)
        {
            var camera = (IDevice?)sender;
            if (camera == null) return;
            camera.CameraOpened -= OnCameraOpen;
            camera.ConnectionLost -= OnConnectLoss;
            camera.CameraClosed -= OnCameraClose;
            camera.ShutdownGrab();
            camera.Close();
            camera.Dispose();
        }
        //相机关闭回调
        private void OnCameraClose(object? sender, EventArgs e)
        {
            var camera = (IDevice?)sender;
            if (camera == null) return;
            camera.CameraOpened -= OnCameraOpen;
            camera.ConnectionLost -= OnConnectLoss;
            camera.CameraClosed -= OnCameraClose;
            camera.ShutdownGrab();
            camera.Close();
            camera.Dispose();
        }
        #endregion

        #region 图片事件
        //bool isMove;
        //int pictureX, pictureY = 0;
        //int mouseX, mouseY = 0;
        //private void PB图片_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (CurrentImage == null) return;
        //    isMove = true;
        //    mouseX = e.X;
        //    mouseY = e.Y;
        //}

        //private void PB图片_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (CurrentImage == null) return;
        //    isMove = false;
        //}

        //private void PB图片_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (CurrentImage == null) return;
        //    if (isMove)
        //    {
        //        pictureX += e.X - mouseX;
        //        pictureY += e.Y - mouseY;

        //        //PictureMove(PB图片.Height, PB图片.Width, CurrentImage.ToMat(), pictureX, pictureY);
        //    }
        //}

        //private void PB图片_Resize(object sender, EventArgs e)
        //{
        //    //TB信息.Text = $"{PB图片.Width}:{PB图片.Height}";
        //}

        //public static Mat GetDisplayPicture(Mat background, int height, int width, Mat sourcePicture, int xOffset, int yOffset)
        //{
        //    Mat display = new(sourcePicture, new Rect(0, 0, width - xOffset, height - yOffset));
        //    Rect rect = new(xOffset, yOffset, display.Width, display.Height);
        //    Mat roi = new(background, rect);
        //    display.CopyTo(roi);
        //    return background;
        //}

        //public void PictureMove(int height, int width, Mat sourcePicture, int pictureX, int pictureY)
        //{
        //    Mat background = new(height, width, MatType.CV_8UC3);
        //    background.SetTo(new Scalar(0, 0, 0));

        //    if (sourcePicture.Width > width || sourcePicture.Height > height)
        //    {
        //        if (pictureX >= 0 && pictureY >= 0)
        //        {
        //            //截取显示图片
        //            Mat display = new(sourcePicture, new Rect(0, 0, Math.Min(width, sourcePicture.Width), Math.Min(height, sourcePicture.Height)));
        //            //显示区域
        //            Mat roi = new(background, new Rect(0, 0, display.Width, display.Height));
        //            //覆盖
        //            display.CopyTo(roi);
        //        }
        //        else if (pictureX < 0 && pictureY < 0)
        //        {
        //            Mat display = new(sourcePicture, new Rect(0 - pictureX, 0 - pictureY, Math.Min(width, sourcePicture.Width), Math.Min(height, sourcePicture.Height)));
        //            Mat roi = new(background, new Rect(0, 0, display.Width, display.Height));
        //            display.CopyTo(roi);
        //        }
        //        else if (pictureX < 0 && pictureY > 0)
        //        {

        //        }
        //        else if (pictureX > 0 && pictureY < 0)
        //        {

        //        }
        //    }
        //}
        #endregion

        private void TB信息_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
        }
    }
}
