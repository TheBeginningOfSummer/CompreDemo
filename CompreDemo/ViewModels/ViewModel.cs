using CommunityToolkit.Mvvm.Input;
using CompreDemo;
using CompreDemo.Forms;
using CSharpKit.DataManagement;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using Services;
using System.ComponentModel;
using Models;
using Microsoft.VisualBasic;
using ThridLibray;

namespace ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region 组件
        readonly OpenFileDialog ofd = new();
        readonly DeviceManager device = DeviceManager.Instance;
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region 窗口
        public Setting_Motion SettingMotion { get; set; }
        public Setting_Camera SettingCamera { get; set; }
        #endregion

        #region 窗口数据
        public readonly BindingList<StringWrapper> ControllerList = [];//控制卡设置列表
        public readonly BindingList<StringWrapper> CurrentAxes = [];//当前控制卡轴列表
        public readonly BindingList<StringWrapper> ExistingCameraList = [];//相机遍历列表
        public readonly BindingList<StringWrapper> SavedCameraList = [];//存储的相机列表
        public readonly BindingList<StringWrapper> ROINameList = [];//图片ROI列表

        private string settingCameraInfo = "";
        public string SettingCameraInfo
        {
            get { return settingCameraInfo; }
            set
            {
                settingCameraInfo = $"{value}{Environment.NewLine}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SettingCameraInfo)));
            }
        }
        //选定的相机
        HuarayCamera? selectedCamera;
        public HuarayCamera? SelectedCamera
        {
            get
            {
                string[] message = SettingCamera.LB相机列表.Text.Split('[');
                if (device.Cameras!.TryGetValue(message[0], out selectedCamera))
                    return selectedCamera;
                return null;
            }
            set
            {
                selectedCamera = value;
            }
        }
        //当前图片
        public Bitmap? CurrentImage { get; set; }
        //当前区域
        public int[] CurrentROI = [0, 0, 10, 10];
        #endregion

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
        #endregion

        public ViewModel(Setting_Motion setting_Motion, Setting_Camera setting_Camera)
        {
            SettingMotion = setting_Motion;
            SettingCamera = setting_Camera;
            MouseCallbackEvent = new MouseCallback(MouseDraw);
            BindingCommand();
            Initialize();
            
        }

        #region 方法
        private void BindingCommand()
        {
            #region 轴卡设置窗口方法绑定
            SettingMotion.CB轴卡.SelectedIndexChanged += CB轴卡_SelectedIndexChanged;
            FormKit.ListBinding(SettingMotion.CB轴卡, ControllerList, "Value", "Value");//绑定控制器列表
            FormKit.ListBinding(SettingMotion.CB轴, CurrentAxes, "Value", "Value");//绑定当前轴列表
            SettingMotion.TSM打开测试窗口.Command = new RelayCommand(打开测试窗口);
            SettingMotion.TSM自动轨迹测试.Command = new RelayCommand(自动轨迹测试);
            SettingMotion.TSM连接当前卡.Command = new RelayCommand(连接当前卡);
            SettingMotion.TSM断开当前卡.Command = new RelayCommand(断开当前卡);
            SettingMotion.BTN轴卡设置.Command = new RelayCommand(轴卡设置);
            SettingMotion.BTN轴卡删除.Command = new RelayCommand(轴卡删除);
            SettingMotion.BTN轴添加.Command = new RelayCommand(轴添加);
            SettingMotion.BTN轴删除.Command = new RelayCommand(轴删除);
            SettingMotion.BTN轴参数设置.Command = new RelayCommand(轴参数设置);
            SettingMotion.BTN轴控制.Command = new RelayCommand(轴控制);
            #endregion

            #region 相机设置窗口方法绑定
            SettingCamera.CB目标区域.SelectedIndexChanged += CB目标区域_SelectedIndexChanged;
            FormKit.ListBinding(SettingCamera.CB相机列表, ExistingCameraList, "Value", "Value");//绑定遍历到的相机列表
            FormKit.ListBinding(SettingCamera.LB相机列表, SavedCameraList, "Value", "Value");//绑定存储的相机列表
            FormKit.ListBinding(SettingCamera.CB目标区域, ROINameList, "Value", "Value");
            FormKit.TextBinding(SettingCamera.TB信息, this, nameof(SettingCameraInfo));
            SettingCamera.BTN查找设备.Command = new RelayCommand(查找设备);
            SettingCamera.BTN捕获图片.Command = new RelayCommand(捕获图片);
            SettingCamera.BTN目标区域.Command = new RelayCommand(目标区域);
            SettingCamera.TSM添加相机.Command = new RelayCommand(添加相机);
            SettingCamera.TSM截取区域.Command = new RelayCommand(截取区域);
            SettingCamera.TSM打开图片.Command = new RelayCommand(打开图片);
            SettingCamera.TSM识别.Command = new RelayCommand(识别);
            SettingCamera.TSM选择区域.Command = new RelayCommand(选择区域);
            //SettingCamera.TSM打开软触发.Command = new RelayCommand(打开软触发);
            //SettingCamera.TSM软触发.Command = new RelayCommand(软触发);
            SettingCamera.TSM连接.Command = new RelayCommand(相机连接);
            SettingCamera.TSM断开.Command = new RelayCommand(相机断开);
            SettingCamera.TSM开始采集.Command = new RelayCommand(开始采集);
            SettingCamera.TSM停止采集.Command = new RelayCommand(停止采集);
            SettingCamera.TSM参数设置.Command = new RelayCommand(参数设置);
            SettingCamera.TSM删除.Command = new RelayCommand(相机删除);
            #endregion
        }
        /// <summary>
        /// 初始化界面
        /// </summary>
        public void Initialize()
        {
            UpdateControllerList();
            UpdateSavedCamera();
            UpdateROIName();
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
        /// <summary>
        /// 打开指定位置的图片
        /// </summary>
        /// <returns></returns>
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

        #region 控制卡设置窗口命令
        private void CB轴卡_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is null) return;
            ComboBox comboBox = (ComboBox)sender;
            UpdateCurrentCardInfo(comboBox.SelectedIndex);//只需要自动切换到第一个项
        }

        public void UpdateControllerList()
        {
            ControllerList.Clear();
            foreach (var controller in device.Controllers.Values)
                ControllerList.Add(new StringWrapper(controller.Name));
            UpdateCurrentCardInfo(0);
        }

        public void UpdateCurrentCardInfo(int index)
        {
            //轴卡加载
            var controller = device.GetController(ControllerList[index].Value);
            if (controller == null) return;

            SettingMotion.TBIP地址.Text = controller.IP;

            #region 轴卡信息显示
            string axes = $"{Environment.NewLine}轴列表：{Environment.NewLine}";
            for (int i = 0; i < controller.AxesName.Count; i++)
            {
                axes += $"            {controller.AxesName[i]}  [{i}]{Environment.NewLine}";
            }
            SettingMotion.LB轴卡信息.Text = $"控制器：{SettingMotion.CB轴卡.Text}{Environment.NewLine}" +
                $"{Environment.NewLine}IP地址：{controller.IP}{Environment.NewLine}{axes}" +
                $"{Environment.NewLine}类型：{controller.GetType().ToString().Split('.').LastOrDefault()}";
            #endregion

            CurrentAxes.Clear();
            foreach (var axisName in controller.AxesName)
                CurrentAxes.Add(new StringWrapper(axisName));
        }

        public void 打开测试窗口()
        {
            if (SettingMotion.CB轴卡.Text == null) return;
            if (!device.Controllers.TryGetValue(SettingMotion.CB轴卡.Text, out var motion)) return;
            if (!motion.Axes.TryGetValue(motion.AxesName[0], out var axis1)) return;
            if (!motion.Axes.TryGetValue(motion.AxesName[1], out var axis2)) return;
            if (axis1 == null || axis2 == null) return;

            Motion_Test motionTest = new(axis1, axis2);
            motionTest.Show();
        }

        public void 自动轨迹测试()
        {
            //switch (TST轨迹.Text)
            //{
            //    case "0":
            //        Processkit.StartTask(ref testMotion, new Action(() => device.AutoRun1("Device2", 7, 0, 0, 50, 400)));
            //        break;
            //    case "1":
            //        Processkit.StartTask(ref testMotion, new Action(() => device.AutoRun2("Device2", 5, 0, 0, 50, 300)));
            //        break;
            //    case "2":
            //        if (testMotion != null)
            //            if (!testMotion.IsCompleted) break;
            //        Processkit.StartTask(ref testMotion, new Action(() => device.AutoRun3("Device1", 12, 0, 0, 3, 50, 100)));
            //        break;
            //}
        }

        private void 连接当前卡()
        {
            if (string.IsNullOrEmpty(SettingMotion.CB轴卡.Text)) return;
            if (device.Connect(SettingMotion.CB轴卡.Text))
                FormKit.ShowInfoBox("连接成功。");
        }

        private void 断开当前卡()
        {
            device.Disconnect(SettingMotion.CB轴卡.Text);
        }

        private void 轴卡设置()
        {
            List<string> axes = [.. SettingMotion.TB轴名称.Text.Split(';')];
            if (device.SetController(SettingMotion.CB轴卡.Text, SettingMotion.TBIP地址.Text, axes, SettingMotion.CB控制卡类型.Text))
                FormKit.ShowInfoBox("已修改。");
            UpdateControllerList();
        }

        private void 轴卡删除()
        {
            var result = FormKit.ShowQuestionBox("是否确定删除轴卡数据？");
            if (result == DialogResult.Yes)
            {
                if (device.RemoveInfo(SettingMotion.CB轴卡.Text))
                    FormKit.ShowInfoBox("已删除。");
                UpdateControllerList();
            }
        }

        private void 轴添加()
        {
            List<string> axes = [.. SettingMotion.TB轴名称.Text.Split(';')];
            if (device.AddAxes(SettingMotion.CB轴卡.Text, axes))
                FormKit.ShowInfoBox("已添加。");
            UpdateControllerList();
        }

        private void 轴删除()
        {
            var result = FormKit.ShowQuestionBox("是否确定删除轴数据？");
            if (result == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(SettingMotion.TB轴名称.Text)) return;
                if (device.RemoveInfo(SettingMotion.CB轴卡.Text, SettingMotion.TB轴名称.Text))
                    FormKit.ShowInfoBox("已删除。");
                UpdateControllerList();
            }
        }

        private void 轴参数设置()
        {
            var currentAxis = device.GetAxis(SettingMotion.CB轴卡.Text, SettingMotion.CB轴.Text);
            if (currentAxis == null) return;
            Setting axisSetting = new(currentAxis);
            axisSetting.Show();
        }

        private void 轴控制()
        {
            var currentAxis = device.GetAxis(SettingMotion.CB轴卡.Text, SettingMotion.CB轴.Text);
            if (currentAxis == null) return;
            Motion_Manual manualControl = new(currentAxis);
            manualControl.Show();
        }
        #endregion

        #region 相机设置窗口命令
        private void CB目标区域_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is null) return;
            ComboBox comboBox = (ComboBox)sender;
            device.ROIDic.TryGetValue(comboBox.Text, out CurrentROI!);
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
                CurrentROI[0] = StartPoint.X; CurrentROI[1] = StartPoint.Y;
                CurrentROI[2] = width; CurrentROI[3] = height;
                if (width > 0 && height > 0)
                {
                    drawingImage.CopyTo(drewImage);
                    window?.ShowImage(drewImage);
                    string input = Interaction.InputBox($"选定范围为[{CurrentROI[0]} ,{CurrentROI[1]} ,{width} ,{height}]，请输入ROI名称：", "提示", "ROI1");
                    if (!string.IsNullOrEmpty(input))
                    {
                        int[] roi = new int[4];
                        CurrentROI.CopyTo(roi, 0);
                        if (!device.ROIDic.TryAdd(input, roi))
                            device.ROIDic[input] = roi;
                        device.SaveROI();
                        UpdateROIName();
                    }
                    window?.Dispose();
                }
            }
        }

        private void UpdateExistingCamera()
        {
            ExistingCameraList.Clear();
            foreach (var item in DeviceManager.GetCameraList())
                ExistingCameraList.Add(new StringWrapper(item.Key));
            if (ExistingCameraList.Count == 0)
            {
                FormKit.ShowInfoBox("无法获取相机，请检查网线。");
                return;
            }
        }

        private void UpdateSavedCamera()
        {
            SavedCameraList.Clear();
            foreach (var camera in device.Cameras)
                SavedCameraList.Add(new StringWrapper($"{camera.Key}[{camera.Value.Key}]"));
        }

        private void UpdateROIName()
        {
            ROINameList.Clear();
            foreach (var item in device.ROIDic)
                ROINameList.Add(new StringWrapper(item.Key));
        }

        private void 查找设备()
        {
            UpdateExistingCamera();
        }

        private void 捕获图片()
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                SelectedCamera!.Device?.ExecuteSoftwareTrigger();
                CurrentImage = SelectedCamera.CatchImage();
                if (CheckItem(CurrentImage, "相机捕获图片失败，请连接相机并开启采集。"))
                {
                    Mat image = BitmapConverter.ToMat(CurrentImage!);
                    image.CopyTo(drewImage);
                    SettingCamera.PB图片.Image = CurrentImage;
                }
            }
        }

        private void 目标区域()
        {
            if (CurrentImage == null) return;
            if (device.ROIDic.TryGetValue(SettingCamera.CB目标区域.Text, out var roi))
            {
                OpenCvSharp.Point point1 = new(roi[0], roi[1]);
                OpenCvSharp.Point point2 = new(roi[0] + roi[2], roi[1] + roi[3]);
                Mat image = CurrentImage.ToMat();
                Cv2.Rectangle(image, point1, point2, Scalar.Red, 2);
                SettingCamera.PB图片.Image = image.ToBitmap();
            }
        }

        private void 添加相机()
        {
            try
            {
                if (string.IsNullOrEmpty(SettingCamera.CB相机列表.Text))
                {
                    FormKit.ShowInfoBox("没有选定相机。");
                    return;
                }
                string input = Interaction.InputBox($"选定设备为[{SettingCamera.CB相机列表.Text}]，请输入相机名称：", "提示", "cam1");
                if (!string.IsNullOrEmpty(input))
                {
                    device.AddCamera(input, SettingCamera.CB相机列表.Text);
                    UpdateSavedCamera();
                    FormKit.ShowInfoBox("已保存。");
                }
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox("保存失败。" + ex.Message);
            }
        }

        private void 截取区域()
        {
            if (CheckItem(CurrentImage, "截取图片为空。"))
            {
                Mat image = BitmapConverter.ToMat(CurrentImage!);
                image = new(image, new Rect(CurrentROI[0], CurrentROI[1], CurrentROI[2], CurrentROI[3]));
                SettingCamera.PB图片.Image = BitmapConverter.ToBitmap(image);
            }
        }

        private void 打开图片()
        {
            CurrentImage = OpenPicture();
            if (CurrentImage == null) return;
            Mat image = BitmapConverter.ToMat(CurrentImage);
            image.CopyTo(drewImage);
            SettingCamera.PB图片.Image = CurrentImage;
        }

        private void 识别()
        {
            if (CheckItem(CurrentImage, "识别图片为空。"))
            {
                Mat image = BitmapConverter.ToMat(CurrentImage!);
                Mat roiMat = new(image, new Rect(CurrentROI[0], CurrentROI[1], CurrentROI[2], CurrentROI[3]));
                SettingCameraInfo += device.OCR(roiMat.ToBitmap());
            }
        }

        private void 选择区域()
        {
            window = new Window("选择区域");
            window.SetMouseCallback(MouseCallbackEvent);
            window.Move(100, 100);
            window?.ShowImage(drewImage);
        }

        private void 打开软触发()
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (CheckItem(SelectedCamera!.Device, "相机连接断开，请重新连接。"))
                {
                    if (SelectedCamera!.Device!.TriggerSet.Open(TriggerSourceEnum.Software))
                    {
                        //ShowMessage("打开软触发。");
                    }
                }
            }
        }

        private void 关闭触发()
        {
            if (SelectedCamera == null)
            {
                //ShowMessage("没有选定相机。");
                return;
            }
            if (SelectedCamera.Device == null)
            {
                //ShowMessage("相机连接断开，请重新连接。");
                return;
            }
            if (SelectedCamera.Device.TriggerSet.Close())
            {
                //ShowMessage("关闭软触发。");
            }
        }

        private void 相机连接()
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                var camera = SettingCamera.LB相机列表.SelectedItem;
                if (camera == null) return;
                if (SelectedCamera!.OpenCamera())
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {((StringWrapper)camera).Value}连接成功。";
                    //SelectedCamera.Device!.CameraOpened += OnCameraOpen;
                    //SelectedCamera.Device!.ConnectionLost += OnConnectLoss;
                    //SelectedCamera.Device!.CameraClosed += OnCameraClose;
                }
                else
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {((StringWrapper)camera).Value}连接失败。";
                }
            }
        }

        private void 相机断开()
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.CloseCamera())
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {SettingCamera.LB相机列表.Text}断开。";
                }
                else
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {SettingCamera.LB相机列表.Text}断开失败。";
                }
            }
        }

        private void 开始采集()
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.Device == null)
                {
                    SettingCameraInfo += $"[{DateTime.Now}] 相机连接断开，请重新连接。";
                    return;
                }
                if (SelectedCamera.StartGrab())
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {SettingCamera.LB相机列表.Text}开始采集。";
                }
                else
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {SettingCamera.LB相机列表.Text}采集失败。";
                }
            }
        }

        private void 停止采集()
        {
            if (CheckItem(SelectedCamera, "没有选定相机。"))
            {
                if (SelectedCamera!.Device == null)
                {
                    SettingCameraInfo += $"[{DateTime.Now}] 相机连接断开，请重新连接。";
                    return;
                }
                if (SelectedCamera.StopGrab())
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {SettingCamera.LB相机列表.Text}停止采集。";
                }
                else
                {
                    SettingCameraInfo += $"[{DateTime.Now}] {SettingCamera.LB相机列表.Text}停止采集失败。";
                }
            }
        }

        private void 参数设置()
        {
            if (SelectedCamera == null) return;
            Setting cameraSetting = new(SelectedCamera);
            cameraSetting.Show();
        }

        private void 相机删除()
        {
            try
            {
                string? name = SettingCamera.LB相机列表.SelectedItem?.ToString();
                if (name == null) return;
                device.DeleteCamera(name.Split('-')[0]);
                UpdateSavedCamera();
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox("删除失败。" + ex.Message);
            }
        }
        #endregion
    }

}
