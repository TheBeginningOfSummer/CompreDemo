using CSharpKit.FileManagement;
using Services;
using System.Reflection;

namespace CompreDemo.Forms
{
    public partial class Setting : Form
    {
        #region 控件列表和布局配置
        public readonly Dictionary<string, Control> ControlList = [];
        public readonly KeyValueManager LayoutConfig;
        #endregion

        #region 需要配置的数据
        private readonly BaseAxis? baseAxis;
        #endregion

        public Setting(BaseAxis axis, string layoutName = "Layout1")
        {
            InitializeComponent();
            LayoutConfig = new KeyValueManager($"{layoutName}.json", "Config\\Layout");
            baseAxis = axis;
            Text = $"{baseAxis.ControllerName} {baseAxis.Name} 轴号 {baseAxis.Number}";
            InitializeControl(axis);
        }

        #region 控件初始化
        public virtual string Translate(string message)
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
                _ => message,
            };
        }

        public virtual void InitializeControl(BaseAxis axis)
        {
            if (LayoutConfig == null) return;
            if (!int.TryParse(LayoutConfig.Load("Row", "12"), out int row)) return;
            if (!int.TryParse(LayoutConfig.Load("X", "30"), out int xInitial)) return;
            if (!int.TryParse(LayoutConfig.Load("Y", "30"), out int yInitial)) return;
            if (!int.TryParse(LayoutConfig.Load("XInterval", "200"), out int xInterval)) return;
            if (!int.TryParse(LayoutConfig.Load("YInterval", "30"), out int yInterval)) return;
            int x = xInitial;
            int y = yInitial;

            PropertyInfo[] properties = axis.GetType().GetProperties();
            int j = 0;
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(bool)) continue;
                if (property.Name == "TargetPosition" || property.Name == "CurrentPosition" || property.Name == "CurrentSpeed") continue;
                AddSettingBox(new Point(x, y), property.Name, property.GetValue(axis)!.ToString()!, Translate(property.Name));
                y += yInterval; j++;
                if (j % row == 0)
                {
                    x += xInterval;
                    y = yInitial;
                }
            }
        }
        #endregion

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

        private void BTN应用_Click(object sender, EventArgs e)
        {

        }

        private void BTN保存_Click(object sender, EventArgs e)
        {
            try
            {
                if (baseAxis == null) return;
                PropertyInfo[] properties = baseAxis.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (ControlList.TryGetValue(property.Name, out var tb))
                        baseAxis.SaveAxisConfig(property.Name, double.Parse(tb.Text));
                }
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
