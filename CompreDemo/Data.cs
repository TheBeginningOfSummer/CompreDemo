using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
