using OpenCvSharp.Extensions;
using OpenCvSharp;
using Services;
using ThridLibray;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CompreDemo.Forms
{
    public partial class CameraSetting : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        HuarayCamera? selectedCamera;
        Bitmap? currentImage;
        Graphics? _g = null;

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

        private Window? window;

        int[] roi = new int[4];
        #endregion

        public CameraSetting()
        {
            InitializeComponent();
            UpdateLB();
            MouseCallbackEvent = new MouseCallback(MouseDraw);
            Task.Run(UpdateImage);
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
                Cv2.Rectangle(drawingImage, StartPoint, EndPoint, Scalar.AliceBlue, 1);
                window?.ShowImage(drawingImage);
            }
            else if (mouseEvent == MouseEventTypes.LButtonUp)
            {
                drewImage.CopyTo(drawingImage);
                EndPoint.X = x;
                EndPoint.Y = y;
                Cv2.Rectangle(drawingImage, StartPoint, EndPoint, Scalar.Green, 1);
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
                    FormMethod.ShowInfoBox($"X：{roi[0]} Y：{roi[1]} 宽：{width} 长：{height}");
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
        private void UpdateLB()
        {
            if (device.CameraList == null) return;
            LB相机列表.Items.Clear();
            foreach (var camera in device.CameraList)
                LB相机列表.Items.Add($"{camera.Key}-{camera.Value.Key}");
        }
        private string GetCamera()
        {
            if (LB相机列表.SelectedItem is string selected)
            {
                string[] message = selected.Split('-');
                if (message.Length >= 1)
                    return message[0];
                else
                    return "noData";
            }
            else
            {
                return "noData";
            }
        }
        private async void UpdateImage()
        {
            while (await device.Images.Reader.WaitToReadAsync())
            {
                if (device.Images.Reader.TryRead(out var image))
                {
                    var bitmap = image.ToBitmap(false);
                    PB图片.Image = bitmap;
                }
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
            device.IsCache = false;
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
            device.IsCache = false;
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
            selectedCamera?.CloseCamera();
            if (!device.CameraList!.TryGetValue(GetCamera(), out selectedCamera))
            {
                FormMethod.ShowInfoBox("无法找到相机。");
            }
        }

        private void BTN查找设备_Click(object sender, EventArgs e)
        {
            CB相机列表.Items.Clear();
            foreach (var item in DeviceManager.GetCameraList())
                CB相机列表.Items.Add(item.Key);
            CB相机列表.Text = CB相机列表.Items[0]?.ToString();
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
                UpdateLB();
                FormMethod.ShowInfoBox("已保存。");
            }
            catch (Exception ex)
            {
                FormMethod.ShowErrorBox("保存失败。" + ex.Message);
            }
        }

        private void BTN触发图片_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null) return;
            selectedCamera.Device?.ExecuteSoftwareTrigger();
            //currentImage = selectedCamera.CatchImage();
            //if (currentImage == null) return;
            //Mat image = BitmapConverter.ToMat(currentImage);
            //image.CopyTo(drewImage);
            //PB图片.Image = currentImage;
        }

        private void TSM截取区域_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;
            Mat image = BitmapConverter.ToMat(currentImage);
            Mat roiMat = new(image, new Rect(roi[0], roi[1], roi[2], roi[3]));
            PB图片.Image = BitmapConverter.ToBitmap(roiMat);
        }

        private void TSM识别_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;
            Mat image = BitmapConverter.ToMat(currentImage);
            Mat roiMat = new(image, new Rect(roi[0], roi[1], roi[2], roi[3]));
            ShowMessage(device.OCR(roiMat.ToBitmap()));
        }

        private void TSM选择区域_Click(object sender, EventArgs e)
        {
            window = new Window("选择区域");
            window.SetMouseCallback(MouseCallbackEvent);
            window?.ShowImage(drewImage);
        }

        private void TSM打开软触发_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.Device == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.Device.TriggerSet.Open(TriggerSourceEnum.Software))
            {
                ShowMessage("打开软触发。");
            }
        }

        private void TSM关闭触发_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.Device == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.Device.TriggerSet.Close())
            {
                ShowMessage("关闭软触发。");
            }
        }

        private void TSM连接_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.OpenCamera())
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

        private void TSM断开_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.CloseCamera())
            {
                ShowMessage($"{LB相机列表.SelectedItem}断开成功。");
            }
            else
            {
                ShowMessage($"{LB相机列表.SelectedItem}断开失败。");
            }
        }

        private void TSM开始采集_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.Device == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            var button = (ToolStripMenuItem)sender;
            if (selectedCamera.StartGrab())
            {
                ShowMessage($"{LB相机列表.SelectedItem}开始采集。");
                Task.Run(() => device.WaitImage(selectedCamera.Device));
            }
            else
            {
                ShowMessage($"{LB相机列表.SelectedItem}开始采集失败。");
            }
        }

        private void TSM停止采集_Click(object sender, EventArgs e)
        {
            if (selectedCamera == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            if (selectedCamera.Device == null)
            {
                ShowMessage("获取相机失败。");
                return;
            }
            var button = (ToolStripMenuItem)sender;
            if (selectedCamera.StopGrab())
            {
                ShowMessage($"{LB相机列表.SelectedItem}停止抓取。");
                device.IsCache = false;
            }
            else
            {
                ShowMessage($"{LB相机列表.SelectedItem}停止抓取失败。");
            }
        }

        private void TSM删除_Click(object sender, EventArgs e)
        {
            try
            {
                string? name = LB相机列表.SelectedItem?.ToString();
                if (name == null) return;
                device.DeleteCamera(name.Split('-')[0]);
                UpdateLB();
            }
            catch (Exception ex)
            {
                FormMethod.ShowErrorBox("删除失败。" + ex.Message);
            }
        }

        #endregion

        private void CameraSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (selectedCamera != null)
            {
                if (selectedCamera.StopGrab())
                {
                    ShowMessage($"{LB相机列表.SelectedItem}停止抓取。");
                    device.IsCache = false;
                }
                else
                {
                    ShowMessage($"{LB相机列表.SelectedItem}停止抓取失败。");
                }
            }   
        }

    }
}
