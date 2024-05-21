using OpenCvSharp;

namespace Services
{
    public class FormMethod
    {
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
    }

    public class DisplayMat
    {
        private Mat? image;
        public Mat? Image
        {
            get { return image; }
            set
            {
                image?.Dispose();
                if (value != null)
                    image = new Mat(value, new Rect(0, 0, value.Width, value.Height));
                
            }
        }

    }

    public class Display : PictureBox
    {
        //protected displayMat dMat;
    }
}
