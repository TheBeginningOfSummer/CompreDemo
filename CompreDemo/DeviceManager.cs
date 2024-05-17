using CSharpKit.FileManagement;
using PaddleOCRSharp;
using Services;
using ThridLibray;

namespace CompreDemo
{
    public class DeviceManager
    {
        #region 单例模式
        private static DeviceManager? _instance;
        private static readonly object _instanceLock = new();
        public static DeviceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                        _instance = new DeviceManager();
                }
                return _instance;
            }
        }
        #endregion

        #region 设备
        //运动控制卡列表
        public Dictionary<string, MotionControl>? Controllers = [];
        //相机列表
        public Dictionary<string, HuarayCamera>? CameraList = [];
        //自动使用的设备
        MotionControl? controller1;
        HuarayCamera? camera1;
        #endregion

        readonly PaddleOCREngine engine;
        
        public DeviceManager()
        {
            try
            {
                InitializeDevices();
            }
            catch (Exception e)
            {
                FormMethod.ShowErrorBox(e.Message);
            }
            OCRParameter oCRParameter = new();
            engine = new PaddleOCREngine(null, oCRParameter);
        }

        //加载设备
        public void InitializeDevices()
        {
            Enumerator.EnumerateDevices();
            LoadControllerConfig();
            LoadCameraConfig();
            if (Controllers!.TryGetValue("Zmotion1", out controller1))
            {
                if (controller1.Connect())
                {
                    //轴卡重新初始化
                    controller1.Initialize();
                    //轴重新初始化，加载连接后的Handle
                    controller1.ReinitializeAxes();
                    //轴参数重新初始化
                    foreach (var item in controller1.Axes.Values)
                        item.Initialize();
                }
                else
                {
                    //连接控制器失败
                }
            }
            else
            {
                //得到控制器失败
            }
            if (CameraList!.TryGetValue("cam1", out camera1))
            {
                camera1.OpenCamera();
                camera1.Device?.TriggerSet.Open(TriggerSourceEnum.Software);
                Task.Run(camera1.WaitImage);//连续向队列中取图，用于非触发模式
            }
            else
            {
                FileManager.AppendLog("Log", "错误记录", "相机加载失败。");
            }
        }

        #region 方法
        public void RecordAndShow(string message)
        {
            FileManager.AppendLog("Log", "错误记录", message);
        }

        public string OCR(Bitmap bitmap)
        {
            var oCRResult = engine.DetectText(bitmap);
            if (oCRResult != null)
            {
                var text = oCRResult.Text;
                return text;
            }
            else
            {
                return "default";
            }
        }
        #endregion

        #region 相机
        /// <summary>
        /// 加载相机列表
        /// </summary>
        public void LoadCameraConfig(string path = "Cameras")
        {
            CameraList = JsonManager.ReadJsonString<Dictionary<string, HuarayCamera>>(path, "HuarayCameraList.json");
            if (CameraList == null)
            {
                CameraList = [];
                JsonManager.SaveJsonString(path, "HuarayCameraList.json", CameraList);
            }
        }
        /// <summary>
        /// 保存相机列表
        /// </summary>
        public void SaveCameraConfig(string path = "Cameras")
        {
            CameraList ??= [];
            JsonManager.SaveJsonString(path, "HuarayCameraList.json", CameraList);
        }

        public static List<IDeviceInfo> GetCameraList()
        {
            return Enumerator.EnumerateDevices();
        }

        public void AddCamera(string name, string key)
        {
            HuarayCamera huarayCamera = new(name, key);
            huarayCamera.Initialize();
            CameraList?.Add(name, huarayCamera);
            SaveCameraConfig();
        }

        public void DeleteCamera(string? name)
        {
            if (name == null) return;
            if (CameraList!.TryGetValue(name, out var huarayCamera))
            {
                huarayCamera.CloseCamera();
                CameraList.Remove(name);
                SaveCameraConfig();
            }
        }

        #region 链接事件
        //相机打开回调
        public void OnCameraOpen(object? sender, EventArgs e)
        {
            var device = (IDevice?)sender;

        }
        //相机丢失回调
        public void OnConnectLoss(object? sender, EventArgs e)
        {
            var device = (IDevice?)sender;
            device?.ShutdownGrab();
            device?.Close();
            device?.Dispose();
        }
        //相机关闭回调
        public void OnCameraClose(object? sender, EventArgs e)
        {
            var device = (IDevice?)sender;
        }
        #endregion

        #endregion

        #region 控制器
        public void LoadControllerConfig()
        {
            string path = BaseAxis.RootPath;
            Controllers = JsonManager.ReadJsonString<Dictionary<string, MotionControl>>(path, "Motion.json");
            if (Controllers == null)
            {
                Controllers = [];
                JsonManager.SaveJsonString(path, "Motion.json", Controllers);
            }
            foreach (var controller in Controllers.Values)
                controller.ReinitializeAxes();
        }

        public void SaveControllerConfig()
        {
            string path = BaseAxis.RootPath;
            Controllers ??= [];
            JsonManager.SaveJsonString(path, "Motion.json", Controllers);
        }

        public MotionControl? GetController(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName)) return null;
            if (Controllers == null) return null;
            if (!Controllers.TryGetValue(controllerName, out MotionControl? value)) return null;
            return value;
        }
        /// <summary>
        /// 得到指定控制器的所有轴
        /// </summary>
        /// <param name="controllerName">控制器名</param>
        /// <returns>所有轴</returns>
        public Dictionary<string, BaseAxis>? GetAxes(string controllerName)
        {
            var controller = GetController(controllerName);
            if (controller == null) return null;
            return controller.Axes;
        }
        /// <summary>
        /// 得到指定控制器的一个轴
        /// </summary>
        /// <param name="controllerName">控制器名</param>
        /// <param name="axisName">轴名称</param>
        /// <returns>指定轴</returns>
        public BaseAxis? GetAxis(string controllerName, string axisName)
        {
            var axes = GetAxes(controllerName);
            if (axes == null) return null;
            if (!axes.TryGetValue(axisName, out BaseAxis? value)) return null;
            return value;
        }

        /// <summary>
        /// 添加轴
        /// </summary>
        /// <param name="controllerName">控制器名</param>
        /// <param name="axisName">要添加的轴名称</param>
        public void AddAxis(string controllerName, string axisName)
        {
            var controller = GetController(controllerName);
            if (controller == null) return;
            controller.AddAxis(axisName);
        }
        /// <summary>
        /// 清除所有轴的位置
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        public void ClearPosition(string controllerName)
        {
            var axes = GetAxes(controllerName);
            if (axes == null) return;
            foreach (var item in axes.Values)
                item.DefPos();
        }

        public bool ModifyInfo(string controllerName, string ip, List<string> axesName, string type)
        {
            var controller = GetController(controllerName);
            if (controller == null)
            {
                //如果不存在，则添加轴卡
                if (string.IsNullOrEmpty(ip)) return false;
                if (axesName == null) return false;
                foreach (var item in axesName)
                    if (string.IsNullOrEmpty(item)) return false;
                if (string.IsNullOrEmpty(type)) return false;
                switch (type)
                {
                    case "Zmotion":
                        Controllers!.Add(controllerName, new ZmotionMotionControl(controllerName, ip, [.. axesName]));
                        break;
                    case "Trio":
                        Controllers!.Add(controllerName, new TrioMotionControl(controllerName, ip, axesName.ToArray()));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //如果控制器已经存在，则添加轴，修改IP
                if (axesName == null) return false;
                foreach (var item in axesName)
                    if (string.IsNullOrEmpty(item)) return false;
                controller.IP = ip;
                //controller.ReinitializeAxes();
                foreach (string axisName in axesName)
                    //添加轴
                    controller.AddAxis(axisName);
                //controller.ReinitializeAxes();
            }
            SaveControllerConfig();
            return true;
        }

        public bool RemoveInfo(string controllerName, string? axisName = null)
        {
            if (string.IsNullOrEmpty(controllerName)) return false;
            if (Controllers == null) return false;
            var controller = GetController(controllerName);
            if (controller == null) return false;

            if (string.IsNullOrEmpty(axisName))
            {
                Controllers.Remove(controllerName);
                SaveControllerConfig();
                //UpdateData?.Invoke(this);
                return true;
            }
            else
            {
                if (controller.RemoveAxis(axisName))
                {
                    SaveControllerConfig();
                    //UpdateData?.Invoke(this);
                    return true;
                }
                return false;
            }
        }

        public bool Connect(string controllerName)
        {
            var controller = GetController(controllerName);
            if (controller == null) return false;
            if (controller.Connect())
            {
                //轴卡重新初始化
                controller.Initialize();
                //轴重新初始化，加载连接后的Handle
                controller.ReinitializeAxes();
                //轴参数重新初始化
                foreach (var item in controller.Axes.Values)
                    item.Initialize();
                //MessageBox.Show("连接成功");
                return true;
            }
            else
            {
                MessageBox.Show("连接失败");
            }
            return false;
        }

        public void Disconnect(string controllerName)
        {
            var controller = GetController(controllerName);
            if (controller == null) return;
            controller.Disconnect();
            MessageBox.Show("断开连接");
        }

        #endregion

        #region 自动轨迹
        ManualResetEvent autoRun = new(false);
        public static void Track1(MotionControl motion, double startX, int times, double targetPosY1, double intervalX1)
        {
            BaseAxis axis1 = motion.Axes["Axis1"];
            BaseAxis axis2 = motion.Axes["Axis2"];
            if (axis1 == null || axis2 == null) return;
            axis1.SingleAbsoluteMove(startX); axis1.Wait();
            //if (BGW_Auto.CancellationPending) return;
            //suspend.WaitOne();
            if (times % 2 != 0)
            {
                //int remainder = cutTimes % 2;
                for (int i = 0; i < times / 2; i++)
                {
                    axis2.SingleAbsoluteMove(targetPosY1);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX1);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    axis2.SingleAbsoluteMove(0);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX1);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    //suspend.WaitOne();
                }
                axis2.SingleAbsoluteMove(targetPosY1);
                axis2.Wait();
            }
            else
            {
                for (int i = 0; i < times / 2; i++)
                {
                    axis2.SingleAbsoluteMove(targetPosY1);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX1);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    axis2.SingleAbsoluteMove(0);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX1);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    //suspend.WaitOne();
                }
            }
        }

        public static void Track2(MotionControl motion, double startX, int times, double targetPosY1, double intervalX1)
        {
            BaseAxis axis1 = motion.Axes["Axis1"];
            BaseAxis axis2 = motion.Axes["Axis2"];
            if (axis1 == null || axis2 == null) return;
            axis1.SingleAbsoluteMove(startX);
            axis1.Wait();
            for (int i = 0; i < times; i++)
            {
                motion.SetOutput(0, 1);
                axis2.SingleAbsoluteMove(targetPosY1);
                axis2.Wait();
                motion.SetOutput(0, 0);
                axis1.SingleRelativeMove(intervalX1);
                axis1.Wait();
                axis2.SingleAbsoluteMove(0);
                axis2.Wait();
                //if (BGW_Auto.CancellationPending) return;
                //suspend.WaitOne();
            }
        }

        public void Track3(MotionControl motion, double startX, double startY, double intervalX, double intervalY)
        {
            BaseAxis axis1 = motion.Axes["Axis1"];
            BaseAxis axis2 = motion.Axes["Axis2"];
            if (axis1 == null || axis2 == null) return;
            axis1.SingleAbsoluteMove(startX);
            axis2.SingleAbsoluteMove(startY);
            axis1.Wait();
            axis2.Wait();

            if(camera1 == null)
            {
                FileManager.AppendLog("Log", "错误记录", "没有相机，自动运行停止。");
                return;
            }
            if(camera1.Device == null)
            {
                FileManager.AppendLog("Log", "错误记录", "没有相机，自动运行停止。");
                return;
            }
            camera1.Device.ExecuteSoftwareTrigger();

            autoRun.WaitOne(1000);
            //if (true) return;

            for (int i = 0; i < 12; i++)
            {

            }
        }
        #endregion

    }
}
