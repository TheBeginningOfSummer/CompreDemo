﻿using CSharpKit.FileManagement;
using cszmcaux;
using System.Text.Json.Serialization;
using TrioMotion.TrioPC_NET;

namespace Models
{
    #region 轴
    public abstract class BaseAxis : IParameterManager
    {
        #region 参数
        public string? ControllerName { get; set; }
        public string Name { get; set; } = "defaultAxis";
        public int Number { get; set; }

        public virtual double Type { get; set; }
        public virtual double Units { get; set; }
        public virtual double Sramp { get; set; }
        public virtual double Speed { get; set; }
        public virtual double Creep { get; set; }
        public virtual double JogSpeed { get; set; }
        public virtual double Accele { get; set; }
        public virtual double Decele { get; set; }
        public virtual double FastDecele { get; set; }
        public virtual double FsLimit { get; set; }//正软限位
        public virtual double RsLimit { get; set; }//负软限位
        #endregion

        #region 信号
        public virtual double DatumIn { get; set; }
        public virtual double ForwardIn { get; set; }
        public virtual double ReverseIn { get; set; }
        public virtual double ForwardJogIn { get; set; }
        public virtual double ReverseJogIn { get; set; }
        public virtual double FastJogIn { get; set; }
        #endregion

        #region 状态
        public string State = "";
        public virtual bool IsMoving { get; set; }
        public virtual double TargetPosition { get; set; }
        public virtual double CurrentPosition { get; set; }
        public virtual double CurrentSpeed { get; set; }
        #endregion

        public static string RootPath = "Motion";

        #region 不同控制卡的连接
        //翠欧
        public TrioPC AxisTrio = new TrioPC();
        //正运动
        public nint AxisZmotion;
        #endregion

        public BaseAxis()
        {

        }

        public void Save()
        {
            JsonManager.SaveJsonString($"{RootPath}\\{ControllerName}", $"{Name}.json", this);
        }

        public virtual void Initialize()
        {

        }

        public abstract void DefPos(double position = 0);

        public abstract void UpdateState();

        public abstract bool Enable();

        public abstract void Disenable();

        #region 运动方法
        public abstract void Stop(int mode);

        public abstract void Wait();

        public abstract void Datum(int mode);

        public abstract void Forward();

        public abstract void Reverse();

        public abstract void SingleRelativeMove(double distance);

        public abstract void SingleAbsoluteMove(double coord);
        #endregion
    }

    public class TrioAxis : BaseAxis
    {
        #region 状态
        private bool isMoving = false;
        public override bool IsMoving
        {
            get
            {
                AxisTrio.GetAxisParameter(AxisParameter.IDLE, Number, out double movingStatus);
                if (movingStatus == 0)
                    isMoving = true;
                else if (movingStatus == -1)
                    isMoving = false;
                return isMoving;
            }
            set
            {
                isMoving = value;
            }
        }
        private double targetPosition = 0;
        public override double TargetPosition
        {
            get
            {
                AxisTrio.GetAxisVariable("DPOS", Number, out targetPosition); // 获取第numAxis个轴目标位置的参数
                targetPosition = Math.Round(targetPosition, 2); // 保留两位小数
                return targetPosition;
            }
            set { targetPosition = value; }
        }
        private double currentPosition = 0;
        public override double CurrentPosition
        {
            get
            {
                AxisTrio.GetAxisParameter(AxisParameter.MPOS, Number, out currentPosition);
                //trio.GetAxisVariable("MPOS", Number, out currentPosition); // 获取第numAxis个轴的实际位置的参数
                currentPosition = Math.Round(currentPosition, 2); // 保留两位小数
                return currentPosition;
            }
            set { currentPosition = value; }
        }
        private double currentSpeed = 0;
        public override double CurrentSpeed
        {
            get
            {
                AxisTrio.GetAxisParameter(AxisParameter.MSPEED, Number, out currentSpeed);
                //trio.GetAxisVariable("MSPEED", Number, out currentSpeed); // 获取第numAxis个轴的实际速度
                currentSpeed = Math.Round(currentSpeed, 2); // 保留两位小数
                return currentSpeed;
            }
            set { currentSpeed = value; }
        }
        #endregion

        public TrioAxis(TrioPC instance, string axisName, int axisNumber, string controllerName)
        {
            AxisTrio = instance;
            Name = axisName;
            Number = axisNumber;
            ControllerName = controllerName;
        }

        public TrioAxis() { }

        #region 设置
        /// <summary>
        /// 初始化轴参数
        /// </summary>
        public override void Initialize()
        {
            AxisTrio.SetAxisParameter(AxisParameter.ATYPE, Number, Type);
            AxisTrio.SetAxisVariable("UNITS", Number, Units); //脉冲当量
            AxisTrio.SetAxisParameter(AxisParameter.SRAMP, Number, Sramp);
            AxisTrio.SetAxisVariable("SPEED", Number, Speed); // 设置轴速度
            AxisTrio.SetAxisVariable("CREEP", Number, Creep); // 设置爬行速度
            AxisTrio.SetAxisVariable("JOGSPEED", Number, JogSpeed); // 设置Jog速度
            AxisTrio.SetAxisVariable("ACCEL", Number, Accele); // 设置加速度
            AxisTrio.SetAxisVariable("DECEL", Number, Decele); // 设置减速度
            AxisTrio.SetAxisParameter(AxisParameter.FASTDEC, Number, FastDecele);
            AxisTrio.SetAxisVariable("FS_LIMIT", Number, FsLimit); // 设置正向软限位（绝对位置）
            AxisTrio.SetAxisVariable("RS_LIMIT", Number, RsLimit); // 设置反向软限位（绝对位置）

            AxisTrio.SetAxisVariable("DATUM_IN", Number, DatumIn); // 回原点输入为输入0
            AxisTrio.SetAxisVariable("FWD_IN", Number, ForwardIn); // 正向软限位输入为输入1
            AxisTrio.SetAxisVariable("REV_IN", Number, ReverseIn); // 正向软限位输入为输入2
            AxisTrio.SetAxisVariable("FWD_JOG", Number, ForwardJogIn); // 设置正向JOG运动的输入
            AxisTrio.SetAxisVariable("REV_JOG", Number, ReverseJogIn); // 设置反向JOG输入
            AxisTrio.SetAxisParameter(AxisParameter.FAST_JOG, Number, FastJogIn);

            AxisTrio.SetAxisVariable("FE_LIMIT", Number, 20000); // 设置跟随误差极大值
            AxisTrio.SetAxisVariable("FE_RANGE", Number, 10000); // 设置跟随误差报告范围
            //trio.SetAxisVariable("REP_DIST", Number, 200000000000); // 设置重复距离
            AxisTrio.SetAxisVariable("SERVO", Number, 1); // SERVO=1:进行闭环运动，SERVO=0：开环运动
            AxisTrio.SetAxisVariable("AXIS_ENABLE", Number, 1); // 轴使能（其实初始时，每个轴默认处于使能状态）
        }
        /// <summary>
        /// 定义当前位置
        /// </summary>
        /// <param name="position">当前位置</param>
        public override void DefPos(double position = 0)
        {
            AxisTrio.Defpos(position, Number);
            AxisTrio.Execute("WAIT UNTIL OFFPOS=0");//Ensures DEFPOS is complete before next line
        }
        /// <summary>
        /// 更新轴状态
        /// </summary>
        public override void UpdateState()
        {
            double axisState = -1;
            AxisTrio.GetAxisVariable("AXISSTATUS", Number, out axisState);
            int axis0State = (int)axisState;
            if (axis0State == 0)
            {
                State = "轴状态：正  常";
            }
            else if ((axis0State >> 2 & 1) == 1)
            {
                State = "轴状态：与伺服通讯错误！";
            }
            else if ((axis0State >> 3 & 1) == 1)
            {
                State = "轴状态：伺服错误！";
            }
            else if ((axis0State >> 4 & 1) == 1)
            {
                State = "轴状态：正向硬限位报警！";
            }
            else if ((axis0State >> 5 & 1) == 1)
            {
                State = "轴状态：负向硬限位报警！";
            }
            else if ((axis0State >> 8 & 1) == 1)
            {
                State = "轴状态：跟随误差超限出错！";
            }
            else if ((axis0State >> 9 & 1) == 1)
            {
                State = "轴状态：超过正向软限位报警！";
            }
            else if ((axis0State >> 10 & 1) == 1)
            {
                State = "轴状态：超过负向软限位报警！";
            }
            else
            {
                State = "";
            }
        }
        #endregion

        #region 运动控制
        /// <summary>
        /// 使能
        /// </summary>
        /// <returns>1为成功-1为失败</returns>
        public override bool Enable()
        {
            double value = -1;
            AxisTrio.SetVariable("WDOG", 1);
            AxisTrio.GetVariable("WDOG", out value);
            if (value == 1) return true;
            else return false;
        }
        /// <summary>
        /// 关闭使能
        /// </summary>
        public override void Disenable()
        {
            AxisTrio.SetVariable("WDOG", 0);
        }

        public override void Stop(int mode = 2)
        {
            AxisTrio.Cancel(mode, Number);// 取消轴0上的运动
        }

        public override void Wait()
        {
            //trio.Execute($"WAIT IDLE AXIS({Number})");
            Thread.Sleep(100);
            do
            {
                Thread.Sleep(50);
            } while (IsMoving);
        }

        public override void Datum(int mode = 3)
        {
            Stop(); // 取消numAxis轴上的运动
            AxisTrio.Datum(mode, Number); // 模式3:以SPEED速度正向回原点; 模式4:以SPEED速度反向回原点 （已经通过SetAxisVariable设置了回原点输入DATUM_IN）
        }

        public override void Forward()
        {
            if (Number >= 0)
            {
                Stop();
                AxisTrio.Forward(Number);
            }
        }

        public override void Reverse()
        {
            if (Number >= 0)
            {
                Stop();
                AxisTrio.Reverse(Number);
            }
        }

        public override void SingleRelativeMove(double distance)
        {
            if (Number >= 0)
            {
                double[] dist = new double[] { distance };
                AxisTrio.MoveRel(dist, Number);
            }
        }

        public override void SingleAbsoluteMove(double coord)
        {
            if (Number >= 0)
            {
                double[] dist = new double[] { coord };
                AxisTrio.MoveAbs(dist, Number);
            }
        }

        public void RelativeMove(double[] dist, int axes)
        {
            if (Number >= 0)
            {
                AxisTrio.MoveRel(dist, axes, Number);
            }
        }

        public void AbsoluteMove(double[] dist, int axes)
        {
            if (Number >= 0)
            {
                AxisTrio.MoveAbs(dist, axes, Number);
            }
        }
        #endregion
    }

    public class ZmotionAxis : BaseAxis
    {
        #region 状态
        private bool isMoving = false;
        public override bool IsMoving
        {
            get
            {
                int movingStatus = -1;
                Zmcaux.ZAux_Direct_GetIfIdle(AxisZmotion, Number, ref movingStatus);
                if (movingStatus == 0)
                    isMoving = true;
                else if (movingStatus == -1)
                    isMoving = false;
                return isMoving;
            }
            set
            {
                isMoving = value;
            }
        }
        private float targetPosition = 0;
        public override double TargetPosition
        {
            get
            {
                Zmcaux.ZAux_Direct_GetDpos(AxisZmotion, Number, ref targetPosition);
                targetPosition = (float)Math.Round(targetPosition / (float)Units, 2); // 保留两位小数
                if (double.IsNaN(targetPosition)) targetPosition = 0;
                return targetPosition;
            }
            set { targetPosition = (float)value; }
        }
        private double currentPosition = 0;
        public override double CurrentPosition
        {
            get
            {
                float position = 0;
                Zmcaux.ZAux_Direct_GetMpos(AxisZmotion, Number, ref position);
                currentPosition = Math.Round(position / Units, 2); // 保留两位小数
                if (double.IsNaN(currentPosition)) currentPosition = 0;
                return currentPosition;
            }
            set { currentPosition = value; }
        }
        private double currentSpeed = 0;
        public override double CurrentSpeed
        {
            get
            {
                float speed = 0;
                Zmcaux.ZAux_Direct_GetMspeed(AxisZmotion, Number, ref speed);
                currentSpeed = Math.Round(speed / Units, 2); // 保留两位小数
                if (double.IsNaN(currentSpeed)) currentSpeed = 0;
                return currentSpeed;
            }
            set { currentSpeed = value; }
        }
        #endregion

        public ZmotionAxis(nint handle, string axisName, int axisNumber, string controllerName)
        {
            AxisZmotion = handle;
            Name = axisName;
            Number = axisNumber;
            ControllerName = controllerName;
        }

        public ZmotionAxis() { }

        #region 设置
        public override void Initialize()
        {
            //Zmcaux.ZAux_Direct_SetInvertStep(Handle, Number, 256 * 100 + 0);
            Zmcaux.ZAux_Direct_SetAtype(AxisZmotion, Number, (int)Type);
            Zmcaux.ZAux_Direct_SetUnits(AxisZmotion, Number, (float)Units);
            Zmcaux.ZAux_Direct_SetSramp(AxisZmotion, Number, (float)Sramp * (float)Units);
            Zmcaux.ZAux_Direct_SetSpeed(AxisZmotion, Number, (float)Speed * (float)Units);
            Zmcaux.ZAux_Direct_SetCreep(AxisZmotion, Number, (float)Creep * (float)Units);
            Zmcaux.ZAux_Direct_SetJogSpeed(AxisZmotion, Number, (float)JogSpeed * (float)Units);
            Zmcaux.ZAux_Direct_SetAccel(AxisZmotion, Number, (float)Accele * (float)Units);
            Zmcaux.ZAux_Direct_SetDecel(AxisZmotion, Number, (float)Decele * (float)Units);
            Zmcaux.ZAux_Direct_SetFastDec(AxisZmotion, Number, (float)FastDecele * (float)Units);
            Zmcaux.ZAux_Direct_SetFsLimit(AxisZmotion, Number, (float)FsLimit * (float)Units);
            Zmcaux.ZAux_Direct_SetRsLimit(AxisZmotion, Number, (float)RsLimit * (float)Units);

            Zmcaux.ZAux_Direct_SetDatumIn(AxisZmotion, Number, (int)DatumIn);
            Zmcaux.ZAux_Direct_SetFwdIn(AxisZmotion, Number, (int)ForwardIn);
            Zmcaux.ZAux_Direct_SetRevIn(AxisZmotion, Number, (int)ReverseIn);
            Zmcaux.ZAux_Direct_SetFwdJog(AxisZmotion, Number, (int)ForwardJogIn);
            Zmcaux.ZAux_Direct_SetRevJog(AxisZmotion, Number, (int)ReverseJogIn);
            Zmcaux.ZAux_Direct_SetFastJog(AxisZmotion, Number, (int)FastJogIn);

            //Zmcaux.ZAux_Direct_SetLspeed(Handle, Number, Convert.ToSingle(arg[6]) * Units);
        }

        public override void DefPos(double position = 0)
        {
            Zmcaux.ZAux_Direct_Defpos(AxisZmotion, Number, (float)position);
        }

        public override void UpdateState()
        {

        }
        #endregion

        #region 运动控制
        /// <summary>
        /// 使能
        /// </summary>
        /// <returns>1为成功-1为失败</returns>
        public override bool Enable()
        {
            int result = Zmcaux.ZAux_Direct_SetAxisEnable(AxisZmotion, Number, 1);
            if (result == 0) return true;
            else return false;
        }
        /// <summary>
        /// 关闭使能
        /// </summary>
        public override void Disenable()
        {
            int result = Zmcaux.ZAux_Direct_SetAxisEnable(AxisZmotion, Number, 0);
        }

        public override void Stop(int mode)
        {
            Zmcaux.ZAux_Direct_Single_Cancel(AxisZmotion, Number, mode);
        }

        public override void Wait()
        {
            Thread.Sleep(100);
            do
            {
                Thread.Sleep(50);
            } while (IsMoving);
        }

        public override void Datum(int mode = 3)
        {
            Zmcaux.ZAux_Direct_Single_Datum(AxisZmotion, Number, mode);
        }

        public override void Forward()
        {
            Zmcaux.ZAux_Direct_Single_Vmove(AxisZmotion, Number, 1);
        }

        public override void Reverse()
        {
            Zmcaux.ZAux_Direct_Single_Vmove(AxisZmotion, Number, -1);
        }

        public override void SingleRelativeMove(double distance)
        {
            Zmcaux.ZAux_Direct_Single_Move(AxisZmotion, Number, (float)distance * (float)Units);
        }

        public override void SingleAbsoluteMove(double coord)
        {
            Zmcaux.ZAux_Direct_Single_MoveAbs(AxisZmotion, Number, (float)coord * (float)Units);
        }

        #endregion
    }
    #endregion

    #region 控制卡
    [JsonDerivedType(typeof(TrioMotionControl), typeDiscriminator: "Trio")]
    [JsonDerivedType(typeof(ZmotionMotionControl), typeDiscriminator: "Zmotion")]

    public abstract class MotionControl
    {
        public static string RootPath = "Motion";
        public string Name { get; set; } = "DefaultController";
        public string IP { get; set; } = "127.0.0.1";
        public List<string> AxesName { get; set; } = [];

        public Dictionary<string, BaseAxis> Axes = [];

        public virtual void Initialize()
        {
            
        }
        public abstract bool Connect();
        public abstract void Disconnect();
        public abstract bool IsConnected();
        public abstract bool AddAxis(string axisName);
        public abstract bool RemoveAxis(string axisName);
        public abstract void Scram();
        //信号
        public abstract double[] GetInputs(int number, out bool isComplete);
        public abstract void SetInput(int num, int invert);
        public abstract double[] GetOutputs(int number);
        public abstract void SetOutput(int num, int value);
        //AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(MotionControl))));
        //AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetType("Trio").GetInterface(""));
        public MotionControl() { }
    }

    public class TrioMotionControl : MotionControl
    {
        public readonly TrioPC Trio = new TrioPC();

        public TrioMotionControl(string controllerName, string ip, params string[] axisName)
        {
            Name = controllerName;
            IP = ip;
            foreach (var axis in axisName)
                AddAxis(axis);
        }

        public TrioMotionControl() { }
        
        #region 设置
        public TrioAxis? LoadAxis(string axisName)
        {
            return JsonManager.ReadJsonString<TrioAxis?>($"{RootPath}\\{Name}", $"{axisName}.json");
        }

        public void LoadAxes()
        {
            foreach (var name in AxesName)
            {
                TrioAxis? axis = LoadAxis(name);
                if (axis != null)
                {
                    axis.AxisTrio = Trio;
                    Axes.TryAdd(axis.Name, axis);
                }
                else
                    AddAxis(name);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            LoadAxes();
            //Trio.SetVariable("LIMIT_BUFFERED", 64);//运动缓存区设为64条指令

        }

        public override bool Connect()
        {
            Trio.HostAddress = IP;
            if (Trio.Open(PortType.Ethernet, PortId.EthernetREMOTE))
            {
                foreach (var axis in Axes.Values)
                    axis.AxisTrio = Trio;
                return true;
            }
            return false;
        }

        public override void Disconnect()
        {
            Trio.Close(PortId.EthernetREMOTE);
        }

        public override bool IsConnected()
        {
            return Trio.IsOpen(PortId.EthernetREMOTE);
        }

        public override bool AddAxis(string axisName)
        {
            int axisNumber = 0;
            for (int i = 0; i < AxesName.Count + 1; i++)
            {
                if (!Axes.Values.Select(x => x.Number).Contains(i))
                {
                    axisNumber = i;
                    break;
                }
            }
            if (Axes.TryAdd(axisName, new TrioAxis(Trio, axisName, axisNumber, Name)))
            {
                AxesName.Add(axisName);
                return true;
            }
            return false;
        }

        public override bool RemoveAxis(string axisName)
        {
            if (Axes.Remove(axisName))
            {
                //移除轴信息
                AxesName.Remove(axisName);
                return true;
            }
            return false;
        }
        #endregion

        #region 控制
        /// <summary>
        /// 全部停止
        /// </summary>
        public override void Scram()
        {
            Trio.RapidStop(); // 取消所有的轴的当前运动
        }

        public void Wait()
        {
            Trio.Execute("WAIT IDLE");
        }

        public void Wait(int millisecond)
        {
            Trio.Execute($"WA({millisecond})"); //等待 20ms
        }
        #endregion

        #region 状态检测
        public bool ConnectState()
        {
            return Trio.IsOpen(PortId.EthernetREMOTE);
        }

        public bool EnableState()
        {
            Trio.GetVariable("WDOG", out double value);
            if (value == 1) return true;
            else return false;
        }

        public double ECState()
        {
            double state = -1;
            // 判断EC状态，若异常则进行相应的操作
            Trio.SetVr(0, -1); // 初始化VR(0)=-1，用于存放EC的状态
            Trio.Execute("ETHERCAT($22,0,0)"); // 将EtherCAT的状态返回到VR(0)中。EtherCAT指令详见Trio BASIC
            Trio.GetVr(0, out state);
            int i = 0;
            while (state != 3 && i < 3)// 控制器未连接驱动器，则重新初始化EC
            {
                Trio.Execute("ETHERCAT(0,0)"); // 重新初始化EC
                Thread.Sleep(3000);
                Trio.Execute("ETHERCAT($22,0,0)");
                Trio.GetVr(0, out state);
                i++;
            }
            return state;
        }
        #endregion

        #region IO
        public override double[] GetInputs(int number, out bool isComplete)
        {
            double[] ioState = new double[number];
            isComplete = true;
            for (int i = 0; i < number; i++)
            {
                isComplete = Trio.In(i, i, out ioState[i]);
                if (!isComplete) break;
            }
            return ioState;
        }

        public override void SetInput(int num, int invert)
        {
            Trio.InvertIn(num, invert);
        }

        public override double[] GetOutputs(int number)
        {
            return new double[number];
        }

        public override void SetOutput(int num, int value)
        {
            Trio.Op(num, num, value);
        }
        #endregion
    }

    public class ZmotionMotionControl : MotionControl
    {
        public nint Zmotion;
        public int ErrorCode;

        public ZmotionMotionControl(string controllerName, string ip, params string[] axisName)
        {
            Name = controllerName;
            IP = ip;
            foreach (var axis in axisName)
                AddAxis(axis);
        }

        public ZmotionMotionControl() { }

        #region 设置
        public ZmotionAxis? LoadAxis(string axisName)
        {
            return JsonManager.ReadJsonString<ZmotionAxis?>($"{RootPath}\\{Name}", $"{axisName}.json");
        }

        public void LoadAxes()
        {
            foreach (var name in AxesName)
            {
                ZmotionAxis? axis = LoadAxis(name);
                if (axis != null)
                    Axes.TryAdd(axis.Name, axis);
                else
                    AddAxis(name);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            LoadAxes();
        }

        public void ECInitialize()
        {
            ErrorCode = Zmcaux.ZAux_BusCmd_InitBus(Zmotion);
        }

        public override bool Connect()
        {
            //链接控制器 
            ErrorCode = Zmcaux.ZAux_OpenEth(IP, out Zmotion);
            if (Zmotion != 0)
            {
                foreach (var axis in Axes.Values)
                    axis.AxisZmotion = Zmotion;
                return true;
            }
            else
                return false;
        }

        public override void Disconnect()
        {
            ErrorCode = Zmcaux.ZAux_Close(Zmotion);
            Zmotion = 0;
        }

        public override bool IsConnected()
        {
            if (Zmotion == 0) return false;
            return true;
        }

        public override bool AddAxis(string axisName)
        {
            int axisNumber = 0;
            if (string.IsNullOrEmpty(axisName)) return false;
            for (int i = 0; i < AxesName.Count + 1; i++)
            {
                if (!Axes.Values.Select(x => x.Number).Contains(i))
                {
                    axisNumber = i;
                    break;
                }
            }
            if (Axes.TryAdd(axisName, new ZmotionAxis(Zmotion, axisName, axisNumber, Name)))
            {
                if (!AxesName.Contains(axisName))
                {
                    //在通过AxesName创建轴时，AxesName集合不能更改
                    AxesName.Add(axisName);
                }
                return true;
            }
            return false;
        }

        public override bool RemoveAxis(string axisName)
        {
            if (Axes.Remove(axisName))
            {
                //移除轴信息
                AxesName.Remove(axisName);
                return true;
            }
            return false;
        }
        #endregion

        #region 控制
        /// <summary>
        /// 全部停止
        /// </summary>
        public override void Scram()
        {
            ErrorCode = Zmcaux.ZAux_Direct_Rapidstop(Zmotion, 0);
        }
        #endregion

        #region IO
        public override double[] GetInputs(int number, out bool isComplete)
        {
            double[] inputs = new double[number];
            isComplete = true;
            for (int i = 0; i < number; i++)
            {
                isComplete = GetInput(i, out uint input);
                inputs[i] = input;
                if (!isComplete) break;
            }
            return inputs;
        }

        public override void SetInput(int num, int invert)
        {
            ErrorCode = Zmcaux.ZAux_Direct_SetInvertIn(Zmotion, num, invert);
        }

        public bool GetInput(int num, out uint input)
        {
            input = 2;
            ErrorCode = Zmcaux.ZAux_Direct_GetIn(Zmotion, num, ref input);
            if (ErrorCode == 0) return true;
            return false;
        }

        public override double[] GetOutputs(int number)
        {
            double[] outputs = new double[number];
            for (int i = 0; i < number; i++)
            {
                outputs[i] = GetOutput(i);
            }
            return outputs;
        }

        public override void SetOutput(int num, int value)
        {
            ErrorCode = Zmcaux.ZAux_Direct_SetOp(Zmotion, num, (uint)value);
        }

        public uint GetOutput(int num)
        {
            uint value = 2;
            ErrorCode = Zmcaux.ZAux_Direct_GetOp(Zmotion, num, ref value);
            return value;
        }
        #endregion

    }
    #endregion

}
