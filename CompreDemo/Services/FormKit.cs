using System.ComponentModel;

namespace Services
{
    public class FormKit
    {
        public static void OnThread(Control control, Action method)
        {
            if (control.IsHandleCreated)
                control.Invoke(method);
            else
                method();
        }
        /// <summary>
        /// 得到一个矩形阵列的坐标
        /// </summary>
        /// <param name="x">阵列起始X坐标</param>
        /// <param name="y">阵列起始Y坐标</param>
        /// <param name="count">阵列元素个数</param>
        /// <param name="length">每行的元素个数</param>
        /// <param name="xInterval">阵列坐标x方向间距</param>
        /// <param name="yInterval">阵列坐标y方向间距</param>
        /// <returns>阵列坐标列表</returns>
        public static List<Point> GetLocation(int x, int y, int count, int length, int xInterval, int yInterval)
        {
            int o = x;
            List<Point> locationList = [];
            for (int i = 0; i < count; i++)
            {
                locationList.Add(new Point(x, y));
                x += xInterval;
                if ((i + 1) % length == 0)
                {
                    x = o;
                    y += yInterval;
                }
            }
            return locationList;
        }

        #region 消息框
        public static void ShowInfoBox(string message, string caption = "提示")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowErrorBox(string message, string caption = "错误")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowQuestionBox(string message, string caption = "提示")
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion

        public static void UpdateListBox(ListBox list, List<string> data)
        {
            list.Items.Clear();
            foreach (var item in data)
                list.Items.Add(item);
        }

        #region 数据绑定
        public static void TextBinding<T>(Control control, T viewModel, string propertyName)
        {
            control.DataBindings.Add(new Binding("Text", viewModel, propertyName));
        }

        public static void TextBinding<T>(List<Control> controls, T viewModel)
        {
            foreach (var control in controls)
            {
                if (control.Tag == null) continue;
                TextBinding(control, viewModel, (string)control.Tag);
            }
        }

        public static void ListBinding<T>(ListBox lb, BindingList<T> list, string display = "", string value = "")
        {
            lb.DataSource = list;
            lb.DisplayMember = display;
            lb.ValueMember = value;
        }

        public static void ListBinding<T>(ComboBox cb, BindingList<T> list, string display = "", string value = "")
        {
            cb.DataSource = list;
            cb.DisplayMember = display;
            cb.ValueMember = value;
        }
        #endregion
    }

}
