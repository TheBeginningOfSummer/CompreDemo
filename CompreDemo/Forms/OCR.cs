using PaddleOCRSharp;

namespace CompreDemo.Forms
{
    public partial class OCR : Form
    {
        readonly OpenFileDialog ofd = new OpenFileDialog();
        OCRResult? oCRResult = null;
        OCRParameter oCRParameter = new();
        PaddleOCREngine engine;

        public OCR()
        {
            InitializeComponent();
            
            engine = new PaddleOCREngine(null, oCRParameter);
        }

        public Bitmap? OpenPicture()
        {
            ofd.Filter = "*.*|*.bmp;*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(ofd.FileName);
                return bitmap;
            }
            return default;
        }

        private void BTN打开_Click(object sender, EventArgs e)
        {
            Bitmap? pic = OpenPicture();
            PB图片.Image = pic;
            if (pic != null)
            {
                oCRResult = engine.DetectText(pic);
            }
            if (oCRResult != null)
            {
                LBText.Text = oCRResult.Text;
            }
        }
    }
}
