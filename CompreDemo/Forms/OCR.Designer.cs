namespace CompreDemo.Forms
{
    partial class OCR
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PB图片 = new PictureBox();
            BTN打开 = new Button();
            LBText = new Label();
            ((System.ComponentModel.ISupportInitialize)PB图片).BeginInit();
            SuspendLayout();
            // 
            // PB图片
            // 
            PB图片.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PB图片.Location = new Point(12, 61);
            PB图片.Name = "PB图片";
            PB图片.Size = new Size(688, 377);
            PB图片.TabIndex = 0;
            PB图片.TabStop = false;
            // 
            // BTN打开
            // 
            BTN打开.Location = new Point(713, 415);
            BTN打开.Name = "BTN打开";
            BTN打开.Size = new Size(75, 23);
            BTN打开.TabIndex = 1;
            BTN打开.Text = "打开";
            BTN打开.UseVisualStyleBackColor = true;
            BTN打开.Click += BTN打开_Click;
            // 
            // LBText
            // 
            LBText.AutoSize = true;
            LBText.Font = new Font("Microsoft YaHei UI", 20F);
            LBText.Location = new Point(12, 12);
            LBText.Name = "LBText";
            LBText.Size = new Size(77, 35);
            LBText.TabIndex = 2;
            LBText.Text = "TEXT";
            // 
            // Test
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LBText);
            Controls.Add(BTN打开);
            Controls.Add(PB图片);
            Name = "Test";
            Text = "Test";
            ((System.ComponentModel.ISupportInitialize)PB图片).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PB图片;
        private Button BTN打开;
        private Label LBText;
    }
}