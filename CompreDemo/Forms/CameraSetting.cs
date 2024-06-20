using OpenCvSharp.Extensions;
using OpenCvSharp;
using Services;
using ThridLibray;
using Microsoft.VisualBasic;
using Models;

namespace CompreDemo.Forms
{
    public partial class CameraSetting : Form
    {
        readonly OpenFileDialog ofd = new();
        readonly DeviceManager device = DeviceManager.Instance;

        HuarayCamera? selectedCamera;
        HuarayCamera? SelectedCamera
        {
            get
            {
                string[] message = LB相机列表.Text.Split('[');
                if (device.Cameras!.TryGetValue(message[0], out selectedCamera))
                    return selectedCamera;
                return null;
            }
            set
            {
                selectedCamera = value;
            }
        }
        Bitmap? CurrentImage { get; set; }

        #region 区域选定
        public MouseCallback MouseCallbackEvent;
        //起始点
        public OpenCvSharp.Point StartPoint;
        //结束点
        public OpenCvSharp.Point EndPoint;
        //绘制中的形状
        private readonly Mat drawingImage = new();
        //绘制完成的形状
        private readonly Mat drewImage = new();
        //绘制的窗口
        private Window? window;
        //绘制的坐标
        int[] currentROI = [0, 0, 10, 10];
        #endregion

        public CameraSetting()
        {
            InitializeComponent();
            MouseCallbackEvent = new MouseCallback(MouseDraw);
        }

        public void Initialize()
        {
            UpdateCameraLB();
            UpdateROICB();
        }

        public void MouseDraw(MouseEventTypes mouseEvent, int x, int y, MouseEventFlags flags, IntPtr userData)
        {
            if (mouseEvent == MouseEventTypes.LButtonDown)
            {
                StartPoint.X = x;
                StartPoint.Y = y;
            }
            else if (mouseEvent == MouseEventTypes.MouseMove && flags == MouseEventFlags.LButton)
            {
                drewImage.CopyTo(drawingImage);
                EndPoint.X = x;
                EndPoint.Y = y;
                Cv2.Rectangle(drawingImage, StartPoint, EndPoint, Scalar.Red, 2);
                window?.ShowImage(drawingImage);
            }
            else if (mouseEvent == MouseEventTypes.LButtonUp)
            {
                drewImage.CopyTo(drawingImage);
                EndPoint.X = x;
                EndPoint.Y = y;
                Cv2.Rectangle(drawingImage, StartPoint, EndPoint, Scalar.Green, 2);
                window?.ShowImage(drawingImage);
            }
            else if (mouseEvent == MouseEventTypes.RButtonDown)
            {
                int width = EndPoint.X - StartPoint.X;
                int height = EndPoint.Y - StartPoint.Y;
                currentROI[0] = StartPoint.X; currentROI[1] = StartPoint.Y;
                currentROI[2] = width; currentROI[3] = height;
                if (width > 0 && height > 0)
                {
                    drawingImage.CopyTo(drewImage);
                    window?.ShowImage(drewImage);
                    string input = Interaction.InputBox($"选定范围为[{currentROI[0]} ,{currentROI[1]} ,{width} ,{height}]，请输入ROI名称：", "提示", "ROI1");
                    if (!string.IsNullOrEmpty(input))
                    {
                        int[] roi = new int[4];
                        currentROI.CopyTo(roi, 0);
                        if (!device.ROIDic.TryAdd(input, roi))
                            device.ROIDic[input] = roi;
                        device.SaveROI();
                        UpdateROICB();
                    }
                    window?.Dispose();
                }
            }
        }

        private void CameraSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        #region 方法
        /// <summary>
        /// 界面消息更新
        /// </summary>
        /// <param name="msg">信息</param>
        private void ShowMessage(string msg)
        {
            TB信息.Invoke(new Action(() =>
            {
                TB信息.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {msg}{Environment.NewLine}");
            }));
        }
        /// <summary>
        /// 从设备管理器更新相机列表
        /// </summary>
        private void UpdateCameraLB()
        {
            LB相机列表.Items.Clear();
            foreach (var camera in device.Cameras)
                LB相机列表.Items.Add($"{camera.Key}[{camera.Value.Key}]");
        }
        /// <summary>
        /// 得到所有相机，并更新到下拉列表
        /// </summary>
        private void UpdateCameraCB()
        {
            CB相机列表.Items.Clear();
            foreach (var item in DeviceManager.GetCameraList())
                CB相机列表.Items.Add(item.Key);
            if (CB相机列表.Items.Count == 0)
            {
                FormKit.ShowInfoBox("无法获取相机，请检查网线。");
                return;
            }
            CB相机列表.Text = CB相机列表.Items[0]?.ToString();
        }

        private void UpdateROICB()
        {
            CB目标区域.Items.Clear();
            foreach (var item in device.ROIDic)
                CB目标区域.Items.Add(item.Key);
        }
        /// <summary>
        /// 检查当前项是否为空
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="item">检查项</param>
        /// <param name="message">为空时输出的消息</param>
        /// <returns>true项目不为空</returns>
        private static bool CheckItem<T>(T? item, string message = "当前项为空。")
        {
            if (item == null)
            {
                FormKit.ShowInfoBox(message);
                return false;
            }
            else
            {
                return true;
            }
        }

        private Bitmap? OpenPicture()
        {
            ofd.Filter = "*.*|*.bmp;*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new(ofd.FileName);
                return bitmap;
            }
            return default;
        }

        #endregion

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

        #region 相机
        private void CB目标区域_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentROI = device.ROIDic[CB目标区域.Text];
        }

        private void BTN查找设备_Click(object sender, EventArgs e)
        {
            UpdateCameraCB();
        }

        private void BTN捕获图片_Click(object sender, EventArgs e)
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                SelectedCamera!.Device?.ExecuteSoftwareTrigger();
                CurrentImage = SelectedCamera.CatchImage();
                if (CheckItem(CurrentImage, "相机捕获图片失败，请连接相机并开启采集。"))
                {
                    Mat image = BitmapConverter.ToMat(CurrentImage!);
                    image.CopyTo(drewImage);
                    PB图片.Image = CurrentImage;
                }
            }
        }

        private void TSM添加相机_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CB相机列表.Text))
                {
                    FormKit.ShowInfoBox("没有选定相机。");
                    return;
                }
                string input = Interaction.InputBox($"选定设备为[{CB相机列表.Text}]，请输入相机名称：", "提示", "cam1");
                if (!string.IsNullOrEmpty(input))
                {
                    device.AddCamera(input, CB相机列表.Text);
                    UpdateCameraLB();
                    FormKit.ShowInfoBox("已保存。");
                }
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox("保存失败。" + ex.Message);
            }
        }

        private void TSM截取区域_Click(object sender, EventArgs e)
        {
            if (CheckItem(CurrentImage, "截取图片为空。"))
            {
                Mat image = BitmapConverter.ToMat(CurrentImage!);
                image = new(image, new Rect(currentROI[0], currentROI[1], currentROI[2], currentROI[3]));
                PB图片.Image = BitmapConverter.ToBitmap(image);
            }
        }

        private void TSM打开图片_Click(object sender, EventArgs e)
        {
            CurrentImage = OpenPicture();
            Mat image = BitmapConverter.ToMat(CurrentImage!);
            image.CopyTo(drewImage);
            PB图片.Image = CurrentImage;
        }

        private void TSM识别_Click(object sender, EventArgs e)
        {
            if (CheckItem(CurrentImage, "识别图片为空。"))
            {
                Mat image = BitmapConverter.ToMat(CurrentImage!);
                Mat roiMat = new(image, new Rect(currentROI[0], currentROI[1], currentROI[2], currentROI[3]));
                ShowMessage(device.OCR(roiMat.ToBitmap()));
            }
        }

        private void TSM选择区域_Click(object sender, EventArgs e)
        {
            window = new Window("选择区域");
            window.SetMouseCallback(MouseCallbackEvent);
            window.Move(100, 100);
            window?.ShowImage(drewImage);
        }

        private void TSM打开软触发_Click(object sender, EventArgs e)
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (CheckItem(SelectedCamera!.Device, "相机连接断开，请重新连接。"))
                {
                    if (SelectedCamera!.Device!.TriggerSet.Open(TriggerSourceEnum.Software))
                    {
                        ShowMessage("打开软触发。");
                    }
                }
            }
        }

        private void TSM关闭触发_Click(object sender, EventArgs e)
        {
            if (SelectedCamera == null)
            {
                ShowMessage("没有选定相机。");
                return;
            }
            if (SelectedCamera.Device == null)
            {
                ShowMessage("相机连接断开，请重新连接。");
                return;
            }
            if (SelectedCamera.Device.TriggerSet.Close())
            {
                ShowMessage("关闭软触发。");
            }
        }

        private void TSM连接_Click(object sender, EventArgs e)
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.OpenCamera())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}连接成功。");
                    SelectedCamera.Device!.CameraOpened += OnCameraOpen;
                    SelectedCamera.Device!.ConnectionLost += OnConnectLoss;
                    SelectedCamera.Device!.CameraClosed += OnCameraClose;
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}连接失败。");
                }
            }
        }

        private void TSM断开_Click(object sender, EventArgs e)
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.CloseCamera())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}断开成功。");
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}断开失败。");
                }
            }
        }

        private void TSM开始采集_Click(object sender, EventArgs e)
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.Device == null)
                {
                    ShowMessage("相机连接断开，请重新连接。");
                    return;
                }
                if (SelectedCamera.StartGrab())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}开始采集。");
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}开始采集失败。");
                }
            }
        }

        private void TSM停止采集_Click(object sender, EventArgs e)
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.Device == null)
                {
                    ShowMessage("相机连接断开，请重新连接。");
                    return;
                }
                if (SelectedCamera.StopGrab())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}停止采集。");
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}停止采集失败。");
                }
            }
        }

        private void TSM参数设置_Click(object sender, EventArgs e)
        {
            if (SelectedCamera == null) return;
            Setting cameraSetting = new(SelectedCamera);
            cameraSetting.Show();
        }

        private void TSM删除_Click(object sender, EventArgs e)
        {
            try
            {
                string? name = LB相机列表.SelectedItem?.ToString();
                if (name == null) return;
                device.DeleteCamera(name.Split('-')[0]);
                UpdateCameraLB();
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox("删除失败。" + ex.Message);
            }
        }

        #endregion

        #region 图片事件
        bool isMove;
        int pictureX, pictureY = 0;
        int mouseX, mouseY = 0;
        private void PB图片_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentImage == null) return;
            isMove = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void PB图片_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentImage == null) return;
            isMove = false;
        }

        private void PB图片_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentImage == null) return;
            if (isMove)
            {
                pictureX += e.X - mouseX;
                pictureY += e.Y - mouseY;

                //PictureMove(PB图片.Height, PB图片.Width, CurrentImage.ToMat(), pictureX, pictureY);
            }
        }

        private void PB图片_Resize(object sender, EventArgs e)
        {
            //TB信息.Text = $"{PB图片.Width}:{PB图片.Height}";
        }

        public static Mat GetDisplayPicture(Mat background, int height, int width, Mat sourcePicture, int xOffset, int yOffset)
        {
            Mat display = new(sourcePicture, new Rect(0, 0, width - xOffset, height - yOffset));
            Rect rect = new(xOffset, yOffset, display.Width, display.Height);
            Mat roi = new(background, rect);
            display.CopyTo(roi);
            return background;
        }

        public void PictureMove(int height, int width, Mat sourcePicture, int pictureX, int pictureY)
        {
            Mat background = new(height, width, MatType.CV_8UC3);
            background.SetTo(new Scalar(0, 0, 0));

            if (sourcePicture.Width > width || sourcePicture.Height > height)
            {
                if (pictureX >= 0 && pictureY >= 0)
                {
                    //截取显示图片
                    Mat display = new(sourcePicture, new Rect(0, 0, Math.Min(width, sourcePicture.Width), Math.Min(height, sourcePicture.Height)));
                    //显示区域
                    Mat roi = new(background, new Rect(0, 0, display.Width, display.Height));
                    //覆盖
                    display.CopyTo(roi);
                }
                else if (pictureX < 0 && pictureY < 0)
                {
                    Mat display = new(sourcePicture, new Rect(0 - pictureX, 0 - pictureY, Math.Min(width, sourcePicture.Width), Math.Min(height, sourcePicture.Height)));
                    Mat roi = new(background, new Rect(0, 0, display.Width, display.Height));
                    display.CopyTo(roi);
                }
                else if (pictureX < 0 && pictureY > 0)
                {

                }
                else if (pictureX > 0 && pictureY < 0)
                {

                }
            }
        }
        #endregion

        private void BTN目标区域_Click(object sender, EventArgs e)
        {
            if (CurrentImage == null) return;
            if (device.ROIDic.TryGetValue(CB目标区域.Text, out var roi))
            {
                OpenCvSharp.Point point1 = new(roi[0], roi[1]);
                OpenCvSharp.Point point2 = new(roi[0] + roi[2], roi[1] + roi[3]);
                Mat image = CurrentImage.ToMat();
                Cv2.Rectangle(image, point1, point2, Scalar.Red, 2);
                PB图片.Image = image.ToBitmap();
            }
        }

        private void BTN测试_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
