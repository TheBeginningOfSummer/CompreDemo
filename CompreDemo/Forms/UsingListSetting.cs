using CompreDemo.Models;
using Microsoft.VisualBasic;
using Services;

namespace CompreDemo.Forms
{
    public partial class UsingListSetting : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        readonly List<EquipmentPlan> usingDevices = [];

        public UsingListSetting()
        {
            InitializeComponent();
            FormMethod.UpdateListBox(LB使用设备列表, [.. device.UsingDevices.Keys]);
            var item = device.Config.Load("CurrentEquipmentPlan");
            if (item != null)
                LB使用设备列表.SelectedItem = item;
        }

        private void BTN添加_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB使用设备列表.SelectedItem == null) return;
                string deviceInfo = Interaction.InputBox($"请输入设备信息：", "提示", "cam1");
                string type = CBDeviceType.Text;
                if (string.IsNullOrEmpty(deviceInfo)) return;
                if (string.IsNullOrEmpty(type)) return;
                TBText.AppendText($"[{type}]{deviceInfo}{Environment.NewLine}");
                usingDevices.Add(new EquipmentPlan(deviceInfo, type));
                device.UsingDevices[LB使用设备列表.SelectedItem.ToString()!] = usingDevices;
                device.SaveUsingDevices();
            }
            catch (Exception)
            {

            }
        }

        private void LB使用设备列表_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LB使用设备列表.SelectedItem == null) return;
            string planName = LB使用设备列表.SelectedItem.ToString()!;
            device.Config.Change("CurrentEquipmentPlan", planName);
            if (device.UsingDevices.TryGetValue(LB使用设备列表.SelectedItem == null ? "" : planName, out var equipmentList))
            {
                TBText.Clear();
                foreach (var item in equipmentList)
                {
                    TBText.Text += item.GetDeviceInfo() + Environment.NewLine;
                }
            }
        }

        private void TSM添加_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Interaction.InputBox($"请输入名称：", "提示", "WorkUnit1");
                if (string.IsNullOrEmpty(name)) return;
                usingDevices.Clear();
                string[] devices = TBText.Text.Trim().Split("\r\n");
                foreach (string device in devices)
                {
                    string[] info = device.Split(']');
                    if (info.Length > 1)
                    {
                        usingDevices.Add(new EquipmentPlan(info[1], info[0].Remove(0, 1)));
                    }
                    else
                    {
                        usingDevices.Add(new EquipmentPlan(device, "default"));
                    }
                }
                if (device.UsingDevices.TryAdd(name, usingDevices))
                {
                    device.SaveUsingDevices();
                    FormMethod.ShowInfoBox("添加成功。");
                    FormMethod.UpdateListBox(LB使用设备列表, [.. device.UsingDevices.Keys]);
                }
            }
            catch (Exception ex)
            {
                FormMethod.ShowErrorBox(ex.Message);
            }
        }

        private void TSM删除_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB使用设备列表.SelectedItem == null) return;
                device.UsingDevices.Remove(LB使用设备列表.SelectedItem.ToString()!);
                device.SaveUsingDevices();
                FormMethod.ShowInfoBox("删除成功。");
                FormMethod.UpdateListBox(LB使用设备列表, [.. device.UsingDevices.Keys]);
            }
            catch (Exception ex)
            {
                FormMethod.ShowErrorBox(ex.Message);
            }
        }

        private void TSM保存_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB使用设备列表.SelectedItem == null) return;
                usingDevices.Clear();
                string[] devices = TBText.Text.Trim().Split("\r\n");
                foreach (string device in devices)
                {
                    string[] info = device.Split(']');
                    if (info.Length > 1)
                    {
                        usingDevices.Add(new EquipmentPlan(info[1], info[0].Remove(0, 1)));
                    }
                    else
                    {
                        usingDevices.Add(new EquipmentPlan(device, "default"));
                    }
                }
                device.UsingDevices[LB使用设备列表.SelectedItem.ToString()!] = usingDevices;
                device.SaveUsingDevices();
                FormMethod.ShowInfoBox("保存成功。");
            }
            catch (Exception ex)
            {
                FormMethod.ShowErrorBox(ex.Message);
            }
        }
    }
}
