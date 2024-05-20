using OpenCvSharp.Extensions;
using OpenCvSharp;
using Services;
using ThridLibray;

namespace CompreDemo.Forms
{
    public partial class CameraSetting : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        HuarayCamera? selectedCamera;
        Bitmap? currentImage;

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
        readonly int[] roi = [0, 0, 10, 10];
        #endregion

        public CameraSetting()
        {
            InitializeComponent();
            UpdateCameraLB();
            MouseCallbackEvent = new MouseCallback(MouseDraw);
            //Task.Run(UpdateImage);
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
                roi[0] = StartPoint.X; roi[1] = StartPoint.Y;
                roi[2] = width; roi[3] = height;
                if (width > 0 && height > 0)
                {
                    drawingImage.CopyTo(drewImage);
                    window?.ShowImage(drewImage);
                    FormMethod.ShowInfoBox($"选定范围：[{roi[0]} ,{roi[1]} ,{width} ,{height}]");
                    window?.Dispose();
                }
            }
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
            if (device.CameraList == null) return;
            LB相机列表.Items.Clear();
            foreach (var camera in device.CameraList)
                LB相机列表.Items.Add($"{camera.Key}-{camera.Value.Key}");
        }
        /// <summary>
        /// 得到当前选定的相机
        /// </summary>
        /// <param name="selectedItem"></param>
        private void GetSelectedCamera(string? selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem))
            {
                FormMethod.ShowInfoBox("当前选定相机名称不正确。");
                return;
            }
            string[] message = selectedItem.Split('-');
            selectedCamera?.CloseCamera();
            if (!device.CameraList!.TryGetValue(message[0], out selectedCamera))
            {
                FormMethod.ShowInfoBox("无法在设备相机列表中找到相机。");
            }
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
                FormMethod.ShowInfoBox("无法获取相机，请检查网线。");
                return;
            }
            CB相机列表.Text = CB相机列表.Items[0]?.ToString();
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
                FormMethod.ShowInfoBox(message);
                return false;
            }
            else
            {
                return true;
            }
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
        private void LB相机列表_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;
            GetSelectedCamera((string?)listBox.SelectedItem);
        }

        private void BTN查找设备_Click(object sender, EventArgs e)
        {
            UpdateCameraCB();
        }

        private void BTN添加相机_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TB相机名称.Text))
                {
                    FormMethod.ShowInfoBox("输入名称。");
                    return;
                }
                if (string.IsNullOrEmpty(CB相机列表.Text))
                {
                    FormMethod.ShowInfoBox("没有选定相机。");
                    return;
                }
                device.AddCamera(TB相机名称.Text, CB相机列表.Text);
                UpdateCameraLB();
                FormMethod.ShowInfoBox("已保存。");
            }
            catch (Exception ex)
            {
                FormMethod.ShowErrorBox("保存失败。" + ex.Message);
            }
        }

        private void BTN捕获图片_Click(object sender, EventArgs e)
        {
            if (CheckItem(selectedCamera, "没有选定相机。"))
            {
                selectedCamera!.Device?.ExecuteSoftwareTrigger();
                currentImage = selectedCamera.CatchImage();
                if (CheckItem(currentImage, "相机捕获图片失败，请重新链接相机，开启采集。"))
                {
                    Mat image = BitmapConverter.ToMat(currentImage!);
                    image.CopyTo(drewImage);
                    PB图片.Image = currentImage;
                }
            }
        }

        private void TSM截取区域_Click(object sender, EventArgs e)
        {
            if (CheckItem(currentImage, "截取图片为空。"))
            {
                Mat image = BitmapConverter.ToMat(currentImage!);
                Mat roiMat = new(image, new Rect(roi[0], roi[1], roi[2], roi[3]));
                PB图片.Image = BitmapConverter.ToBitmap(roiMat);
            }
        }

        private void TSM识别_Click(object sender, EventArgs e)
        {
            if (CheckItem(currentImage, "识别图片为空。"))
            {
                Mat image = BitmapConverter.ToMat(currentImage!);
                Mat roiMat = new(image, new Rect(roi[0], roi[1], roi[2], roi[3]));
                ShowMessage(device.OCR(roiMat.ToBitmap()));
            }
        }

        private void TSM选择区域_Click(object sender, EventArgs e)
        {
            window = new Window("选择区域");
            window.SetMouseCallback(MouseCallbackEvent);
            window?.ShowImage(drewImage);
        }

        private void TSM打开软触发_Click(object sender, EventArgs e)
        {
            if (CheckItem(selectedCamera, "没有选定相机。"))
            {
                if (CheckItem(selectedCamera!.Device, "相机连接断开，请重新连接。"))
                {
                    if (selectedCamera!.Device!.TriggerSet.Open(TriggerSourceEnum.Software))
                    {
                        ShowMessage("打开软触发。");
                    }
                }
            }
        }

        private void TSM关闭触发_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("没有选定相机。");
                return;
            }
            if (selectedCamera.Device == null)
            {
                ShowMessage("相机连接断开，请重新连接。");
                return;
            }
            if (selectedCamera.Device.TriggerSet.Close())
            {
                ShowMessage("关闭软触发。");
            }
        }

        private void TSM连接_Click(object sender, EventArgs e)
        {
            if (CheckItem(selectedCamera, "没有选定相机。"))
            {
                if (selectedCamera!.OpenCamera())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}连接成功。");
                    selectedCamera.Device!.CameraOpened += OnCameraOpen;
                    selectedCamera.Device!.ConnectionLost += OnConnectLoss;
                    selectedCamera.Device!.CameraClosed += OnCameraClose;
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}连接失败。");
                }
            }
        }

        private void TSM断开_Click(object sender, EventArgs e)
        {
            if (CheckItem(selectedCamera, "没有选定相机。"))
            {
                if (selectedCamera!.CloseCamera())
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
            if (CheckItem(selectedCamera, "没有选定相机。"))
            {
                if (selectedCamera!.Device == null)
                {
                    ShowMessage("相机连接断开，请重新连接。");
                    return;
                }
                if (selectedCamera.StartGrab())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}开始采集。");
                    //Task.Run(() => device.WaitImage(selectedCamera.Device));
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}开始采集失败。");
                }
            }
        }

        private void TSM停止采集_Click(object sender, EventArgs e)
        {
            if (CheckItem(selectedCamera, "没有选定相机。"))
            {
                if (selectedCamera!.Device == null)
                {
                    ShowMessage("相机连接断开，请重新连接。");
                    return;
                }
                if (selectedCamera.StopGrab())
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
            if (selectedCamera == null) return;
            Setting cameraSetting = new(selectedCamera);
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
                FormMethod.ShowErrorBox("删除失败。" + ex.Message);
            }
        }

        #endregion

        private void CameraSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            selectedCamera?.StopGrab();
        }

        
    }
}
