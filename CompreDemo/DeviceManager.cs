using CompreDemo.Models;
using CSharpKit.FileManagement;
using Models;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using PaddleOCRSharp;
using Services;
using ThridLibray;
using LogType = CSharpKit.FileManagement.NotifyRecord.LogType;

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

        #region 设备配置
        public KeyValueManager Config = new("Config", "Config.json");
        //运动控制卡列表
        public Dictionary<string, MotionControl> Controllers = [];
        //相机列表
        public Dictionary<string, HuarayCamera> Cameras = [];
        //使用的设备
        public Dictionary<string, List<EquipmentPlan>> DevicePlans = [];
        //图像处理区域
        public Dictionary<string, int[]> ROIDic = [];
        //字符识别引擎
        readonly PaddleOCREngine engine;
        #endregion

        public DeviceManager()
        {
            OCRParameter oCRParameter = new();
            engine = new PaddleOCREngine(null, oCRParameter);
        }

        //设备加载
        public void InitializeDevices()
        {
            LoadConfig();
            string usingName = Config.Load("CurrentEquipmentPlan", "WorkUnit1");
            NotifyRecord.Record($"设备方案{usingName}加载。", LogType.Modification);
            if (DevicePlans.TryGetValue(usingName, out var usingEquipment))
                NotifyRecord.Record($"设备方案{usingName}加载完成。", LogType.Modification);
            if (usingEquipment == null)
            {
                NotifyRecord.Record($"设备方案{usingName}为空。", LogType.Modification);
                return;
            }
            #region 设备初始化
            foreach (var equipment in usingEquipment)
            {
                switch (equipment.Type)
                {
                    case "ControlCard":
                        if (Controllers!.TryGetValue(equipment.Name, out var controller))
                        {
                            if (controller.Connect())
                            {
                                //轴参数重新初始化
                                foreach (var item in controller.Axes.Values)
                                    item.Initialize();//加载轴配置
                                NotifyRecord.Record($"控制卡{equipment.Name}初始化完成。", LogType.Modification);
                            }
                            else
                            {
                                NotifyRecord.Record($"控制卡{equipment.Name}连接失败。", LogType.Error);
                            }
                        }
                        else
                        {
                            NotifyRecord.Record($"控制卡{equipment.Name}未在设备列表中。", LogType.Error);
                        }
                        break;
                    case "Camera":
                        if (Cameras!.TryGetValue(equipment.Name, out var camera))
                        {
                            camera.OpenCamera();
                            if (camera.Device == null)
                                NotifyRecord.Record($"相机{equipment.Name}打开失败。", LogType.Error);
                            else
                            {
                                camera.Device.TriggerSet.Open(TriggerSourceEnum.Software);
                                if (camera.StartGrab())
                                    NotifyRecord.Record($"相机{equipment.Name}开始采集。", LogType.Modification);
                            }
                        }
                        else
                        {
                            NotifyRecord.Record($"相机{equipment.Name}未在设备列表中。", LogType.Error);
                        }
                        break;
                    default:
                        break;
                }
            }
            #endregion
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

        #region 数据加载保存
        public void LoadConfig()
        {
            #region 设备配置加载
            //Enumerator.EnumerateDevices();
            Controllers = JsonManager.LoadDic<MotionControl>(BaseAxis.RootPath, "Motion.json");
            foreach (var controller in Controllers.Values)
                controller.Initialize();//加载轴
            Cameras = JsonManager.LoadDic<HuarayCamera>("Cameras", "HuarayCameraList.json");
            DevicePlans = JsonManager.LoadDic<List<EquipmentPlan>>("Config", "EquipmentPlan.json");
            ROIDic = JsonManager.LoadDic<int[]>("Cameras", "ROIList.json");
            #endregion
        }

        public void SaveControllers()
        {
            string path = BaseAxis.RootPath;
            Controllers ??= [];
            JsonManager.SaveJsonString(path, "Motion.json", Controllers);
        }

        public void SaveCameras()
        {
            Cameras ??= [];
            JsonManager.SaveJsonString("Cameras", "HuarayCameraList.json", Cameras);
        }

        public void SaveUsingPlan()
        {
            JsonManager.SaveDic("Config", "EquipmentPlan.json", DevicePlans);
        }

        public void SaveROI()
        {
            JsonManager.SaveDic("Cameras", "ROIList.json", ROIDic);
        }
        #endregion

        #region 相机
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
            SaveCameras();
        }

        public void DeleteCamera(string? name)
        {
            if (name == null) return;
            if (Cameras!.TryGetValue(name, out var huarayCamera))
            {
                huarayCamera.CloseCamera();
                Cameras.Remove(name);
                SaveCameras();
            }
        }
        #endregion

        #region 控制卡
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

        public bool AddController(string controllerName, string ip, List<string> axesName, string type)
        {
            if (string.IsNullOrEmpty(controllerName))
            {
                FormKit.ShowInfoBox("新建轴卡名称不能为空。");
                return false;
            }
            if (string.IsNullOrEmpty(ip))
            {
                FormKit.ShowInfoBox("新建轴卡IP地址不能为空。");
                return false;
            }
            if (axesName == null)
            {
                FormKit.ShowInfoBox("新建轴卡轴名称不能为空。");
                return false;
            }
            foreach (var item in axesName)
            {
                if (string.IsNullOrEmpty(item))
                {
                    FormKit.ShowInfoBox("新建轴卡轴名称不能为空。");
                    return false;
                }
            }
            if (string.IsNullOrEmpty(type))
            {
                FormKit.ShowInfoBox("请选择新建轴卡类型。");
                return false;
            }

            switch (type)
            {
                case "Zmotion":
                    Controllers!.Add(controllerName, new ZmotionMotionControl(controllerName, ip, [.. axesName]));
                    return true;
                case "Trio":
                    Controllers!.Add(controllerName, new TrioMotionControl(controllerName, ip, [.. axesName]));
                    return true;
                default:
                    return false;
            }
        }

        public bool SetController(string controllerName, string ip, List<string> axesName, string type)
        {
            var controller = GetController(controllerName);
            if (controller == null)
            {
                if (AddController(controllerName, ip, axesName, type))
                {
                    SaveControllers();
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ip))
                    controller.IP = ip;
                SaveControllers();
                return true;
            }
            return false;
        }

        public bool AddAxes(string controllerName, List<string> axesName)
        {
            var controller = GetController(controllerName);
            if (controller == null)
            {
                FormKit.ShowInfoBox("请选择一个控制卡。");
                return false;
            }
            else
            {
                foreach (string axisName in axesName)
                    //添加轴
                    if (!controller.AddAxis(axisName))
                    {
                        FormKit.ShowInfoBox("添加轴失败，检查轴名称。");
                        return false;
                    }
            }
            SaveControllers();
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
                SaveControllers();
                return true;
            }
            else
            {
                if (controller.RemoveAxis(axisName))
                {
                    SaveControllers();
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
                FormKit.ShowInfoBox("连接失败。");
            }
            return false;
        }

        public void Disconnect(string controllerName)
        {
            var controller = GetController(controllerName);
            if (controller == null) return;
            controller.Disconnect();
            FormKit.ShowInfoBox("断开连接。");
        }
        #endregion

        #region 设备动作
        public static readonly ManualResetEvent ProcessControl = new(false);

        public void DoWork(string usingDevice, params string[] targetCode)
        {
            if (!Controllers.TryGetValue(DevicePlans[usingDevice][0].Name, out var motion)) return;
            if (!Cameras.TryGetValue(DevicePlans[usingDevice][1].Name, out var camera)) return;
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
                    if (ROIDic.TryGetValue(DevicePlans[usingDevice][2].Name, out var roi))
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
                    NotifyRecord.Record("捕获图像失败。请检查相机。", LogType.Error);
                }
            }
        }

        public void AutoRun1(string usingDevice, int times, double startX, double startY, double intervalX, double targetY)
        {
            List<EquipmentPlan> deviceList = DevicePlans[usingDevice];
            if (!Controllers.TryGetValue(deviceList[0].Name, out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[0].Strings[0], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[0].Strings[1], out var axis2)) return;
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
            List<EquipmentPlan> deviceList = DevicePlans[usingDevice];
            if (!Controllers.TryGetValue(deviceList[0].Name, out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[0].Strings[0], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[0].Strings[1], out var axis2)) return;
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
            List<EquipmentPlan> deviceList = DevicePlans[usingDevice];
            if (!Controllers.TryGetValue(deviceList[0].Name, out var motion)) return;
            if (!motion.Axes.TryGetValue(deviceList[0].Strings[0], out var axis1)) return;
            if (!motion.Axes.TryGetValue(deviceList[0].Strings[1], out var axis2)) return;
            if (!Cameras.TryGetValue(deviceList[1].Name, out var camera)) return;
            if (axis1 == null || axis2 == null) return;
            if (camera.Device == null) return;
            axis1.SingleAbsoluteMove(startX);
            axis2.SingleAbsoluteMove(startY);
            axis1.Wait();
            axis2.Wait();

            if (camera.Device == null)
            {
                NotifyRecord.Record("未打开相机。", LogType.Error);
                return;
            }
            NotifyRecord.Record("设备准备完成，测试开始。", LogType.Modification);
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
