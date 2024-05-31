using CompreDemo.Forms;
using CompreDemo.Services;
using CSharpKit.FileManagement;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using PaddleOCRSharp;
using Services;
using ThridLibray;
using LogType = CompreDemo.Services.LogType;

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
        public Dictionary<string, MotionControl> Controllers = [];
        //相机列表
        public Dictionary<string, HuarayCamera> Cameras = [];
        //使用的设备
        public Dictionary<string, string[]> UsingDevices = [];
        //字符识别引擎
        readonly PaddleOCREngine engine;
        #endregion

        public Dictionary<string, int[]> ROIDic = [];

        public DeviceManager()
        {
            OCRParameter oCRParameter = new();
            engine = new PaddleOCREngine(null, oCRParameter);
        }

        //设备加载
        public void InitializeDevices(string usingDevice)
        {
            #region 设备配置加载
            Enumerator.EnumerateDevices();
            Controllers = LoadConfig<MotionControl>(BaseAxis.RootPath, "Motion.json");
            foreach (var controller in Controllers.Values)
                controller.Initialize();//加载轴
            Cameras = LoadConfig<HuarayCamera>("Cameras", "HuarayCameraList.json");
            ROIDic = LoadConfig<int[]>("Cameras", "ROIList.json");
            UsingDevices = LoadConfig<string[]>("Config", "UsingDevices.json");
            #endregion

            #region 设备初始化
            if (Controllers!.TryGetValue(UsingDevices[usingDevice][0], out var controller1))
            {
                if (controller1.Connect())
                {
                    //轴参数重新初始化
                    foreach (var item in controller1.Axes.Values)
                        item.Initialize();
                    NotifyHandle.Record("控制卡初始化完成。", LogType.Modification);
                }
                else
                {
                    NotifyHandle.Record("控制卡连接失败。", LogType.Error);
                }
            }
            else
            {
                NotifyHandle.Record("控制卡未在设备列表中。", LogType.Error);
            }
            if (Cameras!.TryGetValue(UsingDevices[usingDevice][3], out var camera1))
            {
                camera1.OpenCamera();
                if (camera1.Device == null)
                    NotifyHandle.Record("相机打开失败。", LogType.Error);
                else
                {
                    camera1.Device.TriggerSet.Open(TriggerSourceEnum.Software);
                    if (camera1.StartGrab())
                        NotifyHandle.Record("相机开始采集。", LogType.Modification);
                }
            }
            else
            {
                NotifyHandle.Record("相机未在设备列表中。", LogType.Error);
            }
            #endregion
        }

        #region 方法
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

        public static Dictionary<string, T> LoadConfig<T>(string path, string fileName, Dictionary<string, T>? defaultData = null)
        {
            var dic = JsonManager.ReadJsonString<Dictionary<string, T>>(path, fileName);
            if (dic == null)
            {
                if (defaultData != null)
                    dic = defaultData;
                else
                    dic = [];
                JsonManager.SaveJsonString(path, fileName, dic);
            }
            return dic;
        }

        public static void SaveConfig<T>(string path, string fileName, Dictionary<string, T> data)
        {
            data ??= [];
            JsonManager.SaveJsonString(path, fileName, data);
        }
        #endregion

        #region 相机
        /// <summary>
        /// 保存相机列表
        /// </summary>
        public void SaveCameraConfig(string path = "Cameras")
        {
            Cameras ??= [];
            JsonManager.SaveJsonString(path, "HuarayCameraList.json", Cameras);
        }

        public static List<IDeviceInfo> GetCameraList()
        {
            return Enumerator.EnumerateDevices();
        }

        public void AddCamera(string name, string key)
        {
            HuarayCamera huarayCamera = new(name, key);
            huarayCamera.GetDevice();
            if (!Cameras.TryAdd(name, huarayCamera))
                Cameras[name] = huarayCamera;
            SaveCameraConfig();
        }

        public void DeleteCamera(string? name)
        {
            if (name == null) return;
            if (Cameras!.TryGetValue(name, out var huarayCamera))
            {
                huarayCamera.CloseCamera();
                Cameras.Remove(name);
                SaveCameraConfig();
            }
        }

        #endregion

        #region 控制器
        public void SaveControllerConfig()
        {
            string path = BaseAxis.RootPath;
            Controllers ??= [];
            JsonManager.SaveJsonString(path, "Motion.json", Controllers);
        }

        public MotionControl? GetController(string controllerName)
        {
            if (Controllers.TryGetValue(controllerName, out MotionControl? value))
                return value;
            return default;
        }

        /// <summary>
        /// 得到指定控制器的一个轴
        /// </summary>
        /// <param name="controllerName">控制器名</param>
        /// <param name="axisName">轴名称</param>
        /// <returns>指定轴</returns>
        public BaseAxis? GetAxis(string controllerName, string axisName)
        {
            if (Controllers.TryGetValue(controllerName, out var controller))
            {
                if (controller.Axes.TryGetValue(axisName, out var axis))
                    return axis;
            }
            return default;
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
                        Controllers!.Add(controllerName, new TrioMotionControl(controllerName, ip, [.. axesName]));
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
                foreach (string axisName in axesName)
                    //添加轴
                    controller.AddAxis(axisName);
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
                //轴参数重新初始化
                foreach (var item in controller.Axes.Values)
                    item.Initialize();
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

        #region 设备动作
        public static readonly ManualResetEvent ProcessControl = new(false);

        public void DoWork(string usingDevice, params string[] targetCode)
        {
            if (!Controllers.TryGetValue(UsingDevices[usingDevice][0], out var motion)) return;
            if (!Cameras.TryGetValue(UsingDevices[usingDevice][3], out var camera)) return;
            for (int i = 0; i < 3; i++)
            {
                //切换表值
                motion.SetOutput(0, 1);
                Thread.Sleep(1000);
                motion.SetOutput(0, 0);
                //相机图像捕获
                camera.Device!.ExecuteSoftwareTrigger();
                var image = camera.CatchImage();
                if (image != null)
                {
                    //图像处理
                    Mat imageMat = BitmapConverter.ToMat(image);
                    if (ROIDic.TryGetValue(UsingDevices[usingDevice][4], out var roi))
                        imageMat = new(imageMat, new Rect(roi[0], roi[1], roi[2], roi[3]));
                    string code = OCR(imageMat.ToBitmap());

                    MessageBox.Show(code);

                    //if (code != "default")
                    //{
                    //    if (code == targetCode[i])
                    //    {
                    //        //成功
                    //    }
                    //    else
                    //    {
                    //        RecordAndShow("识别字符不匹配。");
                    //        //第i次识别失败
                    //    }
                    //}
                    //else
                    //{
                    //    RecordAndShow("图像识别失败。");
                    //    //图像识别不成功
                    //}
                }
                else
                {
                    NotifyHandle.Record("捕获图像失败。请检查相机。", LogType.Error);
                }
            }
        }

        public void AutoRun1(string usingDevice, int times, double startX, double startY, double intervalX, double targetY)
        {
            string[] deviceList = UsingDevices[usingDevice];
            if (deviceList.Length < 3) return;
            if (!Controllers.TryGetValue(deviceList[0], out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[1], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[2], out var axis2)) return;
            if (axis1 == null || axis2 == null) return;
            axis1.SingleAbsoluteMove(startX);
            axis2.SingleAbsoluteMove(startY);
            axis1.Wait();
            axis2.Wait();

            //if (BGW_Auto.CancellationPending) return;
            //suspend.WaitOne();
            if (times % 2 != 0)
            {
                //int remainder = cutTimes % 2;
                for (int i = 0; i < times / 2; i++)
                {
                    axis2.SingleAbsoluteMove(targetY);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    axis2.SingleAbsoluteMove(0);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    //suspend.WaitOne();
                }
                axis2.SingleAbsoluteMove(targetY);
                axis2.Wait();
            }
            else
            {
                for (int i = 0; i < times / 2; i++)
                {
                    axis2.SingleAbsoluteMove(targetY);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    axis2.SingleAbsoluteMove(0);
                    axis2.Wait();
                    axis1.SingleRelativeMove(intervalX);
                    axis1.Wait();
                    //if (BGW_Auto.CancellationPending) return;
                    //suspend.WaitOne();
                }
            }
        }

        public void AutoRun2(string usingDevice, int times, double startX, double startY, double intervalX, double targetY)
        {
            string[] deviceList = UsingDevices[usingDevice];
            if (deviceList.Length < 3) return;
            if (!Controllers.TryGetValue(deviceList[0], out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[1], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[2], out var axis2)) return;
            if (axis1 == null || axis2 == null) return;
            axis1.SingleAbsoluteMove(startX);
            axis2.SingleAbsoluteMove(startY);
            axis1.Wait();
            axis2.Wait();

            for (int i = 0; i < times; i++)
            {
                motion.SetOutput(0, 1);
                axis2.SingleAbsoluteMove(targetY);
                axis2.Wait();
                motion.SetOutput(0, 0);
                axis1.SingleRelativeMove(intervalX);
                axis1.Wait();
                axis2.SingleAbsoluteMove(0);
                axis2.Wait();
                //if (BGW_Auto.CancellationPending) return;
                //suspend.WaitOne();
            }
        }

        public void AutoRun3(string usingDevice, int times, double startX, double startY, double length, double intervalX, double intervalY)
        {
            string[] deviceList = UsingDevices[usingDevice];
            if (deviceList.Length < 4) return;
            if (!Controllers.TryGetValue(deviceList[0], out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[1], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[2], out var axis2)) return;
            if (!Cameras.TryGetValue(deviceList[3], out var camera)) return;
            if (axis1 == null || axis2 == null) return;
            if (camera.Device == null) return;
            axis1.SingleAbsoluteMove(startX);
            axis2.SingleAbsoluteMove(startY);
            axis1.Wait();
            axis2.Wait();

            if (camera.Device == null)
            {
                NotifyHandle.Record("未打开相机。", LogType.Error);
                return;
            }
            NotifyHandle.Record("设备准备完成，测试开始。", LogType.Modification);
            Thread.Sleep(1000);

            for (int i = 1; i <= times; i++)
            {
                //ProcessControl.WaitOne();
                DoWork(usingDevice);

                if (i == times) continue;
                if (i % (int)length == 0)
                {
                    axis1.SingleAbsoluteMove(startX);
                    axis2.SingleRelativeMove(intervalY);
                    axis1.Wait();
                    axis2.Wait();
                }
                else
                {
                    axis1.SingleRelativeMove(intervalX);
                    axis1.Wait();
                }
            }
        }
        #endregion

    }
}
