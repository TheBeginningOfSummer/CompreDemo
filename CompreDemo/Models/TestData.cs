using Services;

namespace CompreDemo.Models
{
    public class TestData
    {
        public string Code { get; set; }
        public int Pos { get; set; }

        private int result;
        public int Result
        {
            get { return result; }
            set
            {
                result = value;
                if (result == 0)
                    FormKit.OnThread(Status, () => Status.BackColor = Color.Gray);
                else if (result == 1)
                    FormKit.OnThread(Status, () => Status.BackColor = Color.Lime);
                else if (result == 2)
                    FormKit.OnThread(Status, () => Status.BackColor = Color.OrangeRed);
                else if (result == 2)
                    FormKit.OnThread(Status, () => Status.BackColor = Color.Yellow);
                else
                    FormKit.OnThread(Status, () => Status.BackColor = Color.Red);
            }
        }

        public Label Status;

        public TestData(string code, int pos, int result)
        {
            Status = new Label
            {
                Name = code,
                Width = 24,
                Height = 24,
                Text = pos.ToString()
            };

            Code = code;
            Pos = pos;
            Result = result;
        }

    }

    public class Tray
    {
        public Dictionary<int, TestData> Tests { get; set; }

        public Tray(int count = 12)
        {
            Tests = [];
            for (int i = 0; i < count; i++)
            {
                TestData data = new(DateTime.Now.ToString("G"), i + 1, 0);
                Tests.Add(i + 1, data);
            }
        }
    }

}
