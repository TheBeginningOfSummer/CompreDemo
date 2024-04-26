using CSharpKit.FileManagement;
using Services;

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

        //运动控制卡列表
        public Dictionary<string, MotionControl>? Controllers = [];

        public DeviceManager()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            Controllers = JsonManager.ReadJsonString<Dictionary<string, MotionControl>>($"{BaseAxis.RootPath}", "Motion.json");
            Controllers ??= [];
            foreach (var controller in Controllers.Values)
                controller.ReinitializeAxes();
        }

        public void SaveConfig()
        {
            Controllers ??= [];
            JsonManager.SaveJsonString("Motion", "Motion.json", Controllers);
        }

        #region 控制卡操作
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
            SaveConfig();
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
                SaveConfig();
                //UpdateData?.Invoke(this);
                return true;
            }
            else
            {
                if (controller.RemoveAxis(axisName))
                {
                    SaveConfig();
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

        //private void BGW_Auto_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Track1(e.Argument.ToString(), 0, 7, 400, 50);
        //    //Track2(e.Argument.ToString(), 300, 5, 300, 50);
        //}

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

        #endregion

    }
}
