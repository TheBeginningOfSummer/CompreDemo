namespace CompreDemo.Forms
{
    partial class Setting
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
            BTN应用 = new Button();
            BTN保存 = new Button();
            SuspendLayout();
            // 
            // BTN应用
            // 
            BTN应用.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN应用.Location = new Point(632, 415);
            BTN应用.Name = "BTN应用";
            BTN应用.Size = new Size(75, 23);
            BTN应用.TabIndex = 0;
            BTN应用.Text = "应用";
            BTN应用.UseVisualStyleBackColor = true;
            BTN应用.Click += BTN应用_Click;
            // 
            // BTN保存
            // 
            BTN保存.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN保存.Location = new Point(713, 415);
            BTN保存.Name = "BTN保存";
            BTN保存.Size = new Size(75, 23);
            BTN保存.TabIndex = 1;
            BTN保存.Text = "保存";
            BTN保存.UseVisualStyleBackColor = true;
            BTN保存.Click += BTN保存_Click;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTN保存);
            Controls.Add(BTN应用);
            Name = "Setting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Setting";
            ResumeLayout(false);
        }

        #endregion

        private Button BTN应用;
        private Button BTN保存;
    }
}