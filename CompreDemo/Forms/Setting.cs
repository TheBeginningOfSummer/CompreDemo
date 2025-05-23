﻿using CSharpKit.DataManagement;
using Models;
using Services;
using System.Reflection;

namespace CompreDemo.Forms
{
    public partial class Setting : Form
    {
        #region 控件列表和布局配置
        public readonly Dictionary<string, Control> ControlList = [];
        #endregion

        #region 需要配置的数据
        private readonly BaseAxis? baseAxis;
        private readonly HuarayCamera? huarayCamera;
        #endregion

        readonly string settingType = "";

        public Setting(BaseAxis axis)
        {
            InitializeComponent(); settingType = "Axis";
            baseAxis = axis;
            Text = $"{baseAxis.ControllerName} {baseAxis.Name} 轴号 {baseAxis.Number}";
            InitializeControl(axis);
        }

        public Setting(HuarayCamera camera)
        {
            InitializeComponent(); settingType = "Camera";
            huarayCamera = camera;
            Text = camera.Key;
            InitializeControl(huarayCamera);
        }

        #region 控件操作
        public void AddLabel(Point point, string name)
        {
            Label label = new()
            {
                Name = $"LB{name}",
                Location = point,
                Text = name,
                AutoSize = true
            };
            Controls.Add(label);
        }

        public void AddSettingBox(Point point, string name, string value, string? tip = null, int width = 50, int xOffset = 80, int yOffset = -5)
        {
            tip ??= name;
            Label label = new()
            {
                Name = $"LB{name}",
                Location = point,
                Text = tip,
                AutoSize = true
            };
            TextBox textBox = new()
            {
                Name = $"TB{name}",
                Location = new Point(point.X + xOffset, point.Y + yOffset),
                Text = value,
                Size = new Size(width, 24)
            };
            Controls.Add(label);
            Controls.Add(textBox);
            ControlList.Add(name, textBox);
        }

        public static string Translate(string message)
        {
            return message switch
            {
                "Type" => "轴类型",
                "Units" => "脉冲当量",
                "Sramp" => "S曲线",
                "Speed" => "速度",
                "Creep" => "爬行速度",
                "JogSpeed" => "Jog速度",
                "Accele" => "加速度",
                "Decele" => "减速度",
                "FastDecele" => "快速减速度",
                "FsLimit" => "正软限位",
                "RsLimit" => "负软限位",
                "DatumIn" => "原点信号",
                "ForwardIn" => "正限位信号",
                "ReverseIn" => "负限位信号",
                "ForwardJogIn" => "Jog正向信号",
                "ReverseJogIn" => "Jog负向信号",
                "FastJogIn" => "快速Jog信号",

                "UserName" => "名称",
                "Key" => "键值",
                "ImageFormat" => "图片格式",
                "ExposureTime" => "曝光时间",
                "Gain" => "增益",
                _ => message,
            };
        }

        public void InitializeControl(BaseAxis axis)
        {
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingRow", "12"), out int row)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingX", "30"), out int xInitial)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingY", "30"), out int yInitial)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingXInterval", "200"), out int xInterval)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingYInterval", "30"), out int yInterval)) return;
            int x = xInitial;
            int y = yInitial;

            PropertyInfo[] properties = axis.GetType().GetProperties();
            int j = 0;
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(bool)) continue;
                if (property.Name == "TargetPosition" || property.Name == "CurrentPosition" || property.Name == "CurrentSpeed") continue;
                if (property.Name == "ControllerName" || property.Name == "Name" || property.Name == "Number") continue;
                AddSettingBox(new Point(x, y), property.Name, property.GetValue(axis)!.ToString()!, Translate(property.Name));
                y += yInterval; j++;
                if (j % row == 0)
                {
                    x += xInterval;
                    y = yInitial;
                }
            }
        }

        public void InitializeControl(HuarayCamera camera)
        {
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingRow", "12"), out int row)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingX", "30"), out int xInitial)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingY", "30"), out int yInitial)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingXInterval", "200"), out int xInterval)) return;
            if (!int.TryParse(DeviceManager.Instance.Config.Load("SettingYInterval", "30"), out int yInterval)) return;
            int x = xInitial;
            int y = yInitial;

            PropertyInfo[] properties = camera.GetType().GetProperties();
            int j = 0;
            foreach (var property in properties)
            {
                if (property.Name == "ROIList") continue;
                if (property.Name == "UserName" || property.Name == "Key")
                {
                    AddLabel(new Point(x, y), $"{Translate(property.Name)}：{property.GetValue(camera)}");
                }
                else
                {
                    string value = property.GetValue(camera) == null ? "" : property.GetValue(camera)!.ToString()!;
                    AddSettingBox(new Point(x, y), property.Name, value, Translate(property.Name));
                }
                y += yInterval; j++;
                if (j % row == 0)
                {
                    x += xInterval;
                    y = yInitial;
                }
            }
        }
        #endregion

        #region 设置保存
        private void BTN应用_Click(object sender, EventArgs e)
        {
            try
            {
                switch (settingType)
                {
                    case "Axis":
                        if (baseAxis == null)
                        {
                            FormKit.ShowInfoBox("当前设备为空。");
                            return;
                        }
                        ClassTool.UpdateClassProperty(baseAxis, ControlList.ToDictionary(entry => entry.Key, entry => entry.Value.Text));
                        baseAxis.Initialize();
                        FormKit.ShowInfoBox($"轴{baseAxis.Name}参数应用成功。");
                        break;
                    case "Camera":
                        if (huarayCamera == null)
                        {
                            FormKit.ShowInfoBox("当前设备为空。");
                            return;
                        }
                        ClassTool.UpdateClassProperty(huarayCamera, ControlList.ToDictionary(entry => entry.Key, entry => entry.Value.Text));
                        huarayCamera.SetAllParameter();
                        FormKit.ShowInfoBox($"相机{huarayCamera.UserName}参数应用成功。");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox($"参数应用失败。{ex.Message}");
            }
        }

        private void BTN保存_Click(object sender, EventArgs e)
        {
            try
            {
                switch (settingType)
                {
                    case "Axis":
                        if (baseAxis == null)
                        {
                            FormKit.ShowInfoBox("当前设备为空。");
                            return;
                        }
                        ClassTool.UpdateClassProperty(baseAxis, ControlList.ToDictionary(entry => entry.Key, entry => entry.Value.Text));
                        baseAxis.Save();
                        FormKit.ShowInfoBox($"轴{baseAxis.Name}参数保存成功。");
                        break;
                    case "Camera":
                        if (huarayCamera == null)
                        {
                            FormKit.ShowInfoBox("当前设备为空。");
                            return;
                        }
                        ClassTool.UpdateClassProperty(huarayCamera, ControlList.ToDictionary(entry => entry.Key, entry => entry.Value.Text));
                        DeviceManager.Instance.SaveCameras();
                        FormKit.ShowInfoBox($"相机{huarayCamera.UserName}参数保存成功。");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox($"参数保存失败。{ex.Message}");
            }
        }
        #endregion
    }
}
