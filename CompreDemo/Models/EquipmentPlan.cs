namespace CompreDemo.Models
{
    public class EquipmentPlan
    {
        public string Name { get; set; } = "default";

        public string[] Strings { get; set; } = [];

        public string Type { get; set; } = "default";

        public string Description
        {
            get { return GetInfo(); }
        }

        public EquipmentPlan(string name, string[] strings, string type)
        {
            Name = name;
            Strings = strings;
            Type = type;
        }

        public EquipmentPlan(string info, string type = "ControlCard")
        {
            if (info.Contains(':'))
            {
                string[] list = info.Split(':');
                string[] strings = list[1].Split(',');
                Name = list[0];
                Strings = strings;
            }
            else
            {
                Name = info;
            }
            Type = type;
        }

        public EquipmentPlan() { }

        public string GetInfo()
        {
            string list = "";
            foreach (var item in Strings)
            {
                list += $"{item},";
            }
            if (list != "")
            {
                return $"[{Type}]{Name}:{list.Remove(list.Length - 1)}";
            }
            else
            {
                return $"[{Type}]{Name}";
            }
        }

    }
}
