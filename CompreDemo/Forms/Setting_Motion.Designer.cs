namespace CompreDemo.Forms
{
    partial class Setting_Motion
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
            BTN轴参数设置 = new Button();
            BTN轴卡设置 = new Button();
            CB轴 = new ComboBox();
            CB轴卡 = new ComboBox();
            LB轴选择 = new Label();
            LB轴卡选择 = new Label();
            MS菜单 = new MenuStrip();
            TSM连接管理 = new ToolStripMenuItem();
            TSM连接当前卡 = new ToolStripMenuItem();
            TSM断开当前卡 = new ToolStripMenuItem();
            TSM测试 = new ToolStripMenuItem();
            TSM打开测试窗口 = new ToolStripMenuItem();
            TSM自动轨迹测试 = new ToolStripMenuItem();
            TST轨迹 = new ToolStripTextBox();
            LBIP地址 = new Label();
            TBIP地址 = new TextBox();
            TB轴名称 = new TextBox();
            CB控制卡类型 = new ComboBox();
            LB轴名称 = new Label();
            LB轴卡类型 = new Label();
            LB轴卡信息 = new Label();
            BTN轴卡删除 = new Button();
            BTN轴控制 = new Button();
            BTN轴删除 = new Button();
            BTN轴添加 = new Button();
            MS菜单.SuspendLayout();
            SuspendLayout();
            // 
            // BTN轴参数设置
            // 
            BTN轴参数设置.Location = new Point(210, 70);
            BTN轴参数设置.Name = "BTN轴参数设置";
            BTN轴参数设置.Size = new Size(80, 25);
            BTN轴参数设置.TabIndex = 2;
            BTN轴参数设置.Text = "轴参数设置";
            BTN轴参数设置.UseVisualStyleBackColor = true;
            // 
            // BTN轴卡设置
            // 
            BTN轴卡设置.Location = new Point(210, 40);
            BTN轴卡设置.Name = "BTN轴卡设置";
            BTN轴卡设置.Size = new Size(80, 25);
            BTN轴卡设置.TabIndex = 3;
            BTN轴卡设置.Text = "轴卡设置";
            BTN轴卡设置.UseVisualStyleBackColor = true;
            // 
            // CB轴
            // 
            CB轴.FormattingEnabled = true;
            CB轴.Location = new Point(80, 70);
            CB轴.Name = "CB轴";
            CB轴.Size = new Size(120, 25);
            CB轴.TabIndex = 2;
            // 
            // CB轴卡
            // 
            CB轴卡.FormattingEnabled = true;
            CB轴卡.Location = new Point(80, 40);
            CB轴卡.Name = "CB轴卡";
            CB轴卡.Size = new Size(120, 25);
            CB轴卡.TabIndex = 2;
            // 
            // LB轴选择
            // 
            LB轴选择.AutoSize = true;
            LB轴选择.Location = new Point(18, 74);
            LB轴选择.Name = "LB轴选择";
            LB轴选择.Size = new Size(44, 17);
            LB轴选择.TabIndex = 1;
            LB轴选择.Text = "轴选择";
            // 
            // LB轴卡选择
            // 
            LB轴卡选择.AutoSize = true;
            LB轴卡选择.Location = new Point(18, 44);
            LB轴卡选择.Name = "LB轴卡选择";
            LB轴卡选择.Size = new Size(56, 17);
            LB轴卡选择.TabIndex = 0;
            LB轴卡选择.Text = "轴卡选择";
            // 
            // MS菜单
            // 
            MS菜单.Items.AddRange(new ToolStripItem[] { TSM连接管理, TSM测试 });
            MS菜单.Location = new Point(0, 0);
            MS菜单.Name = "MS菜单";
            MS菜单.Size = new Size(800, 25);
            MS菜单.TabIndex = 1;
            MS菜单.Text = "菜单";
            // 
            // TSM连接管理
            // 
            TSM连接管理.DropDownItems.AddRange(new ToolStripItem[] { TSM连接当前卡, TSM断开当前卡 });
            TSM连接管理.Name = "TSM连接管理";
            TSM连接管理.Size = new Size(68, 21);
            TSM连接管理.Text = "连接管理";
            // 
            // TSM连接当前卡
            // 
            TSM连接当前卡.Name = "TSM连接当前卡";
            TSM连接当前卡.Size = new Size(136, 22);
            TSM连接当前卡.Text = "连接当前卡";
            // 
            // TSM断开当前卡
            // 
            TSM断开当前卡.Name = "TSM断开当前卡";
            TSM断开当前卡.Size = new Size(136, 22);
            TSM断开当前卡.Text = "断开当前卡";
            // 
            // TSM测试
            // 
            TSM测试.DropDownItems.AddRange(new ToolStripItem[] { TSM打开测试窗口, TSM自动轨迹测试 });
            TSM测试.Name = "TSM测试";
            TSM测试.Size = new Size(44, 21);
            TSM测试.Text = "测试";
            // 
            // TSM打开测试窗口
            // 
            TSM打开测试窗口.Name = "TSM打开测试窗口";
            TSM打开测试窗口.Size = new Size(148, 22);
            TSM打开测试窗口.Text = "打开测试窗口";
            // 
            // TSM自动轨迹测试
            // 
            TSM自动轨迹测试.DropDownItems.AddRange(new ToolStripItem[] { TST轨迹 });
            TSM自动轨迹测试.Name = "TSM自动轨迹测试";
            TSM自动轨迹测试.Size = new Size(148, 22);
            TSM自动轨迹测试.Text = "自动轨迹测试";
            // 
            // TST轨迹
            // 
            TST轨迹.Name = "TST轨迹";
            TST轨迹.Size = new Size(100, 23);
            TST轨迹.Text = "2";
            TST轨迹.ToolTipText = "轨迹";
            // 
            // LBIP地址
            // 
            LBIP地址.AutoSize = true;
            LBIP地址.Location = new Point(18, 104);
            LBIP地址.Name = "LBIP地址";
            LBIP地址.Size = new Size(43, 17);
            LBIP地址.TabIndex = 4;
            LBIP地址.Text = "IP地址";
            // 
            // TBIP地址
            // 
            TBIP地址.Location = new Point(80, 101);
            TBIP地址.Name = "TBIP地址";
            TBIP地址.Size = new Size(120, 23);
            TBIP地址.TabIndex = 5;
            // 
            // TB轴名称
            // 
            TB轴名称.Location = new Point(80, 130);
            TB轴名称.Name = "TB轴名称";
            TB轴名称.Size = new Size(120, 23);
            TB轴名称.TabIndex = 6;
            // 
            // CB控制卡类型
            // 
            CB控制卡类型.FormattingEnabled = true;
            CB控制卡类型.Items.AddRange(new object[] { "Zmotion", "Trio" });
            CB控制卡类型.Location = new Point(80, 159);
            CB控制卡类型.Name = "CB控制卡类型";
            CB控制卡类型.Size = new Size(120, 25);
            CB控制卡类型.TabIndex = 7;
            // 
            // LB轴名称
            // 
            LB轴名称.AutoSize = true;
            LB轴名称.Location = new Point(18, 133);
            LB轴名称.Name = "LB轴名称";
            LB轴名称.Size = new Size(44, 17);
            LB轴名称.TabIndex = 8;
            LB轴名称.Text = "轴名称";
            // 
            // LB轴卡类型
            // 
            LB轴卡类型.AutoSize = true;
            LB轴卡类型.Location = new Point(18, 162);
            LB轴卡类型.Name = "LB轴卡类型";
            LB轴卡类型.Size = new Size(56, 17);
            LB轴卡类型.TabIndex = 9;
            LB轴卡类型.Text = "轴卡类型";
            // 
            // LB轴卡信息
            // 
            LB轴卡信息.AutoSize = true;
            LB轴卡信息.Location = new Point(394, 42);
            LB轴卡信息.Name = "LB轴卡信息";
            LB轴卡信息.Size = new Size(32, 17);
            LB轴卡信息.TabIndex = 10;
            LB轴卡信息.Text = "信息";
            // 
            // BTN轴卡删除
            // 
            BTN轴卡删除.Location = new Point(296, 40);
            BTN轴卡删除.Name = "BTN轴卡删除";
            BTN轴卡删除.Size = new Size(80, 25);
            BTN轴卡删除.TabIndex = 11;
            BTN轴卡删除.Text = "轴卡删除";
            BTN轴卡删除.UseVisualStyleBackColor = true;
            // 
            // BTN轴控制
            // 
            BTN轴控制.Location = new Point(296, 70);
            BTN轴控制.Name = "BTN轴控制";
            BTN轴控制.Size = new Size(80, 25);
            BTN轴控制.TabIndex = 12;
            BTN轴控制.Text = "轴控制";
            BTN轴控制.UseVisualStyleBackColor = true;
            // 
            // BTN轴删除
            // 
            BTN轴删除.Location = new Point(296, 130);
            BTN轴删除.Name = "BTN轴删除";
            BTN轴删除.Size = new Size(80, 25);
            BTN轴删除.TabIndex = 14;
            BTN轴删除.Text = "轴删除";
            BTN轴删除.UseVisualStyleBackColor = true;
            // 
            // BTN轴添加
            // 
            BTN轴添加.Location = new Point(210, 130);
            BTN轴添加.Name = "BTN轴添加";
            BTN轴添加.Size = new Size(80, 25);
            BTN轴添加.TabIndex = 13;
            BTN轴添加.Text = "轴添加";
            BTN轴添加.UseVisualStyleBackColor = true;
            // 
            // Setting_Motion
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(BTN轴删除);
            Controls.Add(BTN轴添加);
            Controls.Add(BTN轴控制);
            Controls.Add(BTN轴卡删除);
            Controls.Add(LB轴卡信息);
            Controls.Add(LB轴卡类型);
            Controls.Add(LB轴名称);
            Controls.Add(CB控制卡类型);
            Controls.Add(TB轴名称);
            Controls.Add(TBIP地址);
            Controls.Add(LBIP地址);
            Controls.Add(BTN轴参数设置);
            Controls.Add(BTN轴卡设置);
            Controls.Add(MS菜单);
            Controls.Add(CB轴);
            Controls.Add(LB轴卡选择);
            Controls.Add(CB轴卡);
            Controls.Add(LB轴选择);
            MainMenuStrip = MS菜单;
            Name = "Setting_Motion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MotionSetting";
            FormClosing += MotionSetting_FormClosing;
            MS菜单.ResumeLayout(false);
            MS菜单.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip MS菜单;
        private Label LB轴选择;
        private Label LB轴卡选择;
        private ToolStripMenuItem TSM测试;
        private Label LBIP地址;
        private Label LB轴名称;
        private Label LB轴卡类型;
        private ToolStripTextBox TST轨迹;
        private ToolStripMenuItem TSM连接管理;
        public ComboBox CB轴;
        public ComboBox CB轴卡;
        public Button BTN轴参数设置;
        public Button BTN轴卡设置;
        public TextBox TBIP地址;
        public TextBox TB轴名称;
        public ComboBox CB控制卡类型;
        public Label LB轴卡信息;
        public Button BTN轴卡删除;
        public Button BTN轴控制;
        public Button BTN轴删除;
        public Button BTN轴添加;
        public ToolStripMenuItem TSM打开测试窗口;
        public ToolStripMenuItem TSM自动轨迹测试;
        public ToolStripMenuItem TSM连接当前卡;
        public ToolStripMenuItem TSM断开当前卡;
    }
}