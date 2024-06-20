using CompreDemo.Models;
using Microsoft.VisualBasic;
using Services;
using System.ComponentModel;

namespace CompreDemo.Forms
{
    public partial class UsingPlan : Form
    {
        readonly DeviceManager device = DeviceManager.Instance;
        readonly BindingList<EquipmentPlan> currentPlan = [];

        public UsingPlan()
        {
            InitializeComponent();
            FormKit.UpdateListBox(LB设备方案列表, [.. device.DevicePlans.Keys]);//更新设备方案列表
            FormKit.ListBinding(LB设备列表, currentPlan, "Description", "Name");//绑定当前设备列表
            var currentPlanName = device.Config.Load("CurrentEquipmentPlan");//当前方案
            if (currentPlanName != null) LB设备方案列表.SelectedItem = currentPlanName;
        }

        private void LB设备方案列表_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentPlanName = LB设备方案列表.Text;
            device.Config.Change("CurrentEquipmentPlan", currentPlanName);//切换方案
            if (device.DevicePlans.TryGetValue(currentPlanName, out var equipmentList))//读取当前列表
            {
                currentPlan.Clear();
                foreach (var item in equipmentList)
                    currentPlan.Add(item);
            }
        }

        private void BTN添加设备_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB设备方案列表.SelectedItem == null) return;
                string deviceInfo = Interaction.InputBox($"请输入设备信息：", "提示", "cam1");
                string type = CBDeviceType.Text;
                if (string.IsNullOrEmpty(deviceInfo)) return;
                if (string.IsNullOrEmpty(type)) return;

                currentPlan.Add(new EquipmentPlan(deviceInfo, type));
                device.DevicePlans[LB设备方案列表.Text] = [.. currentPlan];
                device.SaveUsingPlan();
            }
            catch (Exception)
            {

            }
        }

        private void TSM删除设备_Click(object sender, EventArgs e)
        {
            try
            {
                currentPlan.RemoveAt(LB设备列表.SelectedIndex);
                device.DevicePlans[LB设备方案列表.Text] = [.. currentPlan];
                device.SaveUsingPlan();
            }
            catch (Exception)
            {

            }
        }

        private void TSM添加方案_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Interaction.InputBox($"请输入名称：", "提示", "WorkUnit1");
                if (string.IsNullOrEmpty(name)) return;

                if (device.DevicePlans.TryAdd(name, [.. currentPlan]))
                {
                    device.SaveUsingPlan();
                    FormKit.ShowInfoBox("添加成功。");
                    FormKit.UpdateListBox(LB设备方案列表, [.. device.DevicePlans.Keys]);
                }
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox(ex.Message);
            }
        }

        private void TSM删除方案_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB设备方案列表.SelectedItem == null) return;
                device.DevicePlans.Remove(LB设备方案列表.Text);
                device.SaveUsingPlan();
                FormKit.UpdateListBox(LB设备方案列表, [.. device.DevicePlans.Keys]);
            }
            catch (Exception ex)
            {
                FormKit.ShowErrorBox(ex.Message);
            }
        }


    }
}
