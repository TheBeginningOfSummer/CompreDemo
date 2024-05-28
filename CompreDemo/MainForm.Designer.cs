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
            BTN手动模式 = new Button();
            BTN自动模式 = new Button();
            BTN自动运行 = new Button();
            BTN自动停止 = new Button();
            BTN初始化 = new Button();
            autoRun = new System.ComponentModel.BackgroundWorker();
            TB信息 = new TextBox();
            TSM列表设置 = new ToolStripMenuItem();
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
            TSM设置.DropDownItems.AddRange(new ToolStripItem[] { TSM相机配置, TSM控制卡配置, TSM列表设置 });
            TSM设置.Name = "TSM设置";
            TSM设置.Size = new Size(44, 21);
            TSM设置.Text = "设置";
            // 
            // TSM相机配置
            // 
            TSM相机配置.Name = "TSM相机配置";
            TSM相机配置.Size = new Size(180, 22);
            TSM相机配置.Text = "相机配置";
            TSM相机配置.Click += TSM相机配置_Click;
            // 
            // TSM控制卡配置
            // 
            TSM控制卡配置.Name = "TSM控制卡配置";
            TSM控制卡配置.Size = new Size(180, 22);
            TSM控制卡配置.Text = "控制卡配置";
            TSM控制卡配置.Click += TSM控制卡配置_Click;
            // 
            // BTN手动模式
            // 
            BTN手动模式.Location = new Point(143, 463);
            BTN手动模式.Name = "BTN手动模式";
            BTN手动模式.Size = new Size(97, 46);
            BTN手动模式.TabIndex = 1;
            BTN手动模式.Text = "手动模式";
            BTN手动模式.UseVisualStyleBackColor = true;
            // 
            // BTN自动模式
            // 
            BTN自动模式.Location = new Point(316, 463);
            BTN自动模式.Name = "BTN自动模式";
            BTN自动模式.Size = new Size(97, 46);
            BTN自动模式.TabIndex = 2;
            BTN自动模式.Text = "自动模式";
            BTN自动模式.UseVisualStyleBackColor = true;
            // 
            // BTN自动运行
            // 
            BTN自动运行.Location = new Point(477, 463);
            BTN自动运行.Name = "BTN自动运行";
            BTN自动运行.Size = new Size(97, 46);
            BTN自动运行.TabIndex = 3;
            BTN自动运行.Text = "自动运行";
            BTN自动运行.UseVisualStyleBackColor = true;
            BTN自动运行.Click += BTN自动运行_Click;
            // 
            // BTN自动停止
            // 
            BTN自动停止.Location = new Point(626, 463);
            BTN自动停止.Name = "BTN自动停止";
            BTN自动停止.Size = new Size(97, 46);
            BTN自动停止.TabIndex = 4;
            BTN自动停止.Text = "自动停止";
            BTN自动停止.UseVisualStyleBackColor = true;
            // 
            // BTN初始化
            // 
            BTN初始化.Location = new Point(775, 463);
            BTN初始化.Name = "BTN初始化";
            BTN初始化.Size = new Size(97, 46);
            BTN初始化.TabIndex = 5;
            BTN初始化.Text = "初始化";
            BTN初始化.UseVisualStyleBackColor = true;
            // 
            // TB信息
            // 
            TB信息.Location = new Point(143, 28);
            TB信息.Multiline = true;
            TB信息.Name = "TB信息";
            TB信息.Size = new Size(400, 222);
            TB信息.TabIndex = 6;
            // 
            // TSM列表设置
            // 
            TSM列表设置.Name = "TSM列表设置";
            TSM列表设置.Size = new Size(180, 22);
            TSM列表设置.Text = "列表设置";
            TSM列表设置.Click += TSM列表设置_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(TB信息);
            Controls.Add(BTN初始化);
            Controls.Add(BTN自动停止);
            Controls.Add(BTN自动运行);
            Controls.Add(BTN自动模式);
            Controls.Add(BTN手动模式);
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
        private Button BTN手动模式;
        private Button BTN自动模式;
        private Button BTN自动运行;
        private Button BTN自动停止;
        private Button BTN初始化;
        private System.ComponentModel.BackgroundWorker autoRun;
        private TextBox TB信息;
        private ToolStripMenuItem TSM列表设置;
    }
}
