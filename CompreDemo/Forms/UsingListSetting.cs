using CompreDemo.Models;
using Microsoft.VisualBasic;
using Services;

namespace CompreDemo.Forms
{
    public partial class UsingListSetting : Form
    {
        DeviceManager device = DeviceManager.Instance;

        public UsingListSetting()
        {
            InitializeComponent();
            FormMethod.UpdateListBox(LB使用设备列表, [.. device.UsingDevices.Keys]);
        }

        private void BTN添加_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Interaction.InputBox($"请输入名称：", "提示", "Device1");
                if (string.IsNullOrEmpty(name)) return;
                string[] devices = TBText.Text.Trim().Split("\r\n");
                List<UsingDevice> usingDevices = [];
                foreach (string device in devices)
                {
                    usingDevices.Add(new UsingDevice(device));
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

        private void BTN保存_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB使用设备列表.SelectedItem == null) return;
                string[] devices = TBText.Text.Trim().Split("\r\n");
                List<UsingDevice> usingDevices = [];
                foreach (string device in devices)
                {
                    usingDevices.Add(new UsingDevice(device));
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

        private void LB使用设备列表_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (device.UsingDevices.TryGetValue(LB使用设备列表.SelectedItem == null ? "" : LB使用设备列表.SelectedItem.ToString()!, out var list))
            {
                TBText.Clear();
                foreach (var item in list)
                {
                    TBText.Text += item.GetDeviceInfo() + Environment.NewLine;
                }
            }
        }

        
    }
}
