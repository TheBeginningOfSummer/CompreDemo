namespace CompreDemo.Forms
{
    partial class MotionTest
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
            BTN清除 = new Button();
            SuspendLayout();
            // 
            // BTN清除
            // 
            BTN清除.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN清除.Location = new Point(797, 476);
            BTN清除.Name = "BTN清除";
            BTN清除.Size = new Size(75, 23);
            BTN清除.TabIndex = 0;
            BTN清除.Text = "清除";
            BTN清除.UseVisualStyleBackColor = true;
            BTN清除.Click += TSM清除_Click;
            // 
            // MotionTest
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 511);
            Controls.Add(BTN清除);
            HelpButton = true;
            Name = "MotionTest";
            Text = "MotionTest";
            WindowState = FormWindowState.Maximized;
            FormClosing += MotionTest_FormClosing;
            Paint += MotionTest_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button BTN清除;
    }
}