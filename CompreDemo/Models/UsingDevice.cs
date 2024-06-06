namespace CompreDemo.Models
{
    public class UsingDevice
    {
        public string Name { get; set; } = "default";

        public string[] Strings { get; set; } = [];

        //public string DeviceType { get; set; } = "default";

        public UsingDevice(string name, string[] strings)
        {
            Name = name;
            Strings = strings;
            //DeviceType = deviceType;
        }

        public UsingDevice(string info)
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
        }

        public UsingDevice() { }

        public string GetDeviceInfo()
        {
            string list = "";
            foreach (var item in Strings)
            {
                list += $"{item},";
            }
            if (list != "")
            {
                return $"{Name}:{list.Remove(list.Length - 1)}";
            }
            else
            {
                return $"{Name}";
            }
        }

    }
}
