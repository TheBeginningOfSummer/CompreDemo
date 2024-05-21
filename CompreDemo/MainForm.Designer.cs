namespace CompreDemo
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MS菜单 = new MenuStrip();
            TSM设置 = new ToolStripMenuItem();
            TSM相机配置 = new ToolStripMenuItem();
            TSM控制卡配置 = new ToolStripMenuItem();
            MS菜单.SuspendLayout();
            SuspendLayout();
            // 
            // MS菜单
            // 
            MS菜单.BackColor = Color.Transparent;
            MS菜单.Items.AddRange(new ToolStripItem[] { TSM设置 });
            MS菜单.Location = new Point(0, 0);
            MS菜单.Name = "MS菜单";
            MS菜单.Size = new Size(984, 25);
            MS菜单.TabIndex = 0;
            MS菜单.Text = "菜单";
            // 
            // TSM设置
            // 
            TSM设置.Alignment = ToolStripItemAlignment.Right;
            TSM设置.DropDownItems.AddRange(new ToolStripItem[] { TSM相机配置, TSM控制卡配置 });
            TSM设置.Name = "TSM设置";
            TSM设置.Size = new Size(44, 21);
            TSM设置.Text = "设置";
            // 
            // TSM相机配置
            // 
            TSM相机配置.Name = "TSM相机配置";
            TSM相机配置.Size = new Size(136, 22);
            TSM相机配置.Text = "相机配置";
            TSM相机配置.Click += TSM相机配置_Click;
            // 
            // TSM控制卡配置
            // 
            TSM控制卡配置.Name = "TSM控制卡配置";
            TSM控制卡配置.Size = new Size(136, 22);
            TSM控制卡配置.Text = "控制卡配置";
            TSM控制卡配置.Click += TSM控制卡配置_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(MS菜单);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MS菜单;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "测试";
            FormClosing += MainForm_FormClosing;
            MS菜单.ResumeLayout(false);
            MS菜单.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MS菜单;
        private ToolStripMenuItem TSM设置;
        private ToolStripMenuItem TSM相机配置;
        private ToolStripMenuItem TSM控制卡配置;
    }
}
