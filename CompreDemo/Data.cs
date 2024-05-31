using Services;

namespace CompreDemo
{
    public class Data
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
                    FormMethod.OnThread(Status, () => Status.BackColor = Color.Gray);
                else if (result == 1)
                    FormMethod.OnThread(Status, () => Status.BackColor = Color.Lime);
                else if (result == 2)
                    FormMethod.OnThread(Status, () => Status.BackColor = Color.OrangeRed);
                else if (result == 2)
                    FormMethod.OnThread(Status, () => Status.BackColor = Color.Yellow);
                else
                    FormMethod.OnThread(Status, () => Status.BackColor = Color.Red);
            }
        }

        public Label Status;

        public Data(string code, int pos, int result)
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
        public Dictionary<int, Data> Tests { get; set; }

        public Tray(int count = 12)
        {
            Tests = [];
            for (int i = 0; i < count; i++)
            {
                Data data = new(DateTime.Now.ToString("G"), i + 1, 0);
                Tests.Add(i + 1, data);
            }
        }
    }

}
