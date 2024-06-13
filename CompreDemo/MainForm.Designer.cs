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
            TSM设备方案设置 = new ToolStripMenuItem();
            BTN手动模式 = new Button();
            BTN自动模式 = new Button();
            BTN开始测试 = new Button();
            BTN停止测试 = new Button();
            BTN重新测试 = new Button();
            autoRun = new System.ComponentModel.BackgroundWorker();
            TB信息 = new TextBox();
            GB测试结果 = new GroupBox();
            GB信息 = new GroupBox();
            BTN清除 = new Button();
            BTN查看信息 = new Button();
            MS菜单.SuspendLayout();
            GB信息.SuspendLayout();
            SuspendLayout();
            // 
            // MS菜单
            // 
            MS菜单.BackColor = Color.Transparent;
            MS菜单.Items.AddRange(new ToolStripItem[] { TSM设置 });
            MS菜单.Location = new Point(0, 0);
            MS菜单.Name = "MS菜单";
            MS菜单.Size = new Size(1184, 25);
            MS菜单.TabIndex = 0;
            MS菜单.Text = "菜单";
            // 
            // TSM设置
            // 
            TSM设置.Alignment = ToolStripItemAlignment.Right;
            TSM设置.DropDownItems.AddRange(new ToolStripItem[] { TSM相机配置, TSM控制卡配置, TSM设备方案设置 });
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
            // TSM设备方案设置
            // 
            TSM设备方案设置.Name = "TSM设备方案设置";
            TSM设备方案设置.Size = new Size(180, 22);
            TSM设备方案设置.Text = "设备方案设置";
            TSM设备方案设置.Click += TSM设备方案设置_Click;
            // 
            // BTN手动模式
            // 
            BTN手动模式.Anchor = AnchorStyles.Bottom;
            BTN手动模式.Location = new Point(243, 663);
            BTN手动模式.Name = "BTN手动模式";
            BTN手动模式.Size = new Size(97, 46);
            BTN手动模式.TabIndex = 1;
            BTN手动模式.Text = "手动模式";
            BTN手动模式.UseVisualStyleBackColor = true;
            // 
            // BTN自动模式
            // 
            BTN自动模式.Anchor = AnchorStyles.Bottom;
            BTN自动模式.Location = new Point(416, 663);
            BTN自动模式.Name = "BTN自动模式";
            BTN自动模式.Size = new Size(97, 46);
            BTN自动模式.TabIndex = 2;
            BTN自动模式.Text = "自动模式";
            BTN自动模式.UseVisualStyleBackColor = true;
            // 
            // BTN开始测试
            // 
            BTN开始测试.Anchor = AnchorStyles.Bottom;
            BTN开始测试.Location = new Point(577, 663);
            BTN开始测试.Name = "BTN开始测试";
            BTN开始测试.Size = new Size(97, 46);
            BTN开始测试.TabIndex = 3;
            BTN开始测试.Text = "开始测试";
            BTN开始测试.UseVisualStyleBackColor = true;
            BTN开始测试.Click += BTN开始测试_Click;
            // 
            // BTN停止测试
            // 
            BTN停止测试.Anchor = AnchorStyles.Bottom;
            BTN停止测试.Location = new Point(726, 663);
            BTN停止测试.Name = "BTN停止测试";
            BTN停止测试.Size = new Size(97, 46);
            BTN停止测试.TabIndex = 4;
            BTN停止测试.Text = "停止测试";
            BTN停止测试.UseVisualStyleBackColor = true;
            // 
            // BTN重新测试
            // 
            BTN重新测试.Anchor = AnchorStyles.Bottom;
            BTN重新测试.Location = new Point(875, 663);
            BTN重新测试.Name = "BTN重新测试";
            BTN重新测试.Size = new Size(97, 46);
            BTN重新测试.TabIndex = 5;
            BTN重新测试.Text = "重新测试";
            BTN重新测试.UseVisualStyleBackColor = true;
            BTN重新测试.Click += BTN初始化_Click;
            // 
            // TB信息
            // 
            TB信息.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TB信息.Location = new Point(6, 22);
            TB信息.Multiline = true;
            TB信息.Name = "TB信息";
            TB信息.ReadOnly = true;
            TB信息.ScrollBars = ScrollBars.Vertical;
            TB信息.Size = new Size(308, 420);
            TB信息.TabIndex = 6;
            // 
            // GB测试结果
            // 
            GB测试结果.Location = new Point(82, 77);
            GB测试结果.Name = "GB测试结果";
            GB测试结果.Size = new Size(133, 155);
            GB测试结果.TabIndex = 7;
            GB测试结果.TabStop = false;
            GB测试结果.Text = "测试结果";
            // 
            // GB信息
            // 
            GB信息.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GB信息.Controls.Add(BTN清除);
            GB信息.Controls.Add(BTN查看信息);
            GB信息.Controls.Add(TB信息);
            GB信息.Location = new Point(852, 77);
            GB信息.Name = "GB信息";
            GB信息.Size = new Size(320, 500);
            GB信息.TabIndex = 8;
            GB信息.TabStop = false;
            GB信息.Text = "信息";
            // 
            // BTN清除
            // 
            BTN清除.Anchor = AnchorStyles.Bottom;
            BTN清除.Location = new Point(109, 448);
            BTN清除.Name = "BTN清除";
            BTN清除.Size = new Size(97, 46);
            BTN清除.TabIndex = 9;
            BTN清除.Text = "清除";
            BTN清除.UseVisualStyleBackColor = true;
            // 
            // BTN查看信息
            // 
            BTN查看信息.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BTN查看信息.Location = new Point(6, 448);
            BTN查看信息.Name = "BTN查看信息";
            BTN查看信息.Size = new Size(97, 46);
            BTN查看信息.TabIndex = 9;
            BTN查看信息.Text = "查看信息";
            BTN查看信息.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(GB信息);
            Controls.Add(GB测试结果);
            Controls.Add(BTN重新测试);
            Controls.Add(BTN停止测试);
            Controls.Add(BTN开始测试);
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
            GB信息.ResumeLayout(false);
            GB信息.PerformLayout();
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
        private Button BTN开始测试;
        private Button BTN停止测试;
        private Button BTN重新测试;
        private System.ComponentModel.BackgroundWorker autoRun;
        private TextBox TB信息;
        private ToolStripMenuItem TSM设备方案设置;
        private GroupBox GB测试结果;
        private GroupBox GB信息;
        private Button BTN清除;
        private Button BTN查看信息;
    }
}
