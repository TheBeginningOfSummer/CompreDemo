﻿namespace CompreDemo.Forms
{
    partial class Setting_Camera
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
            components = new System.ComponentModel.Container();
            MS相机 = new MenuStrip();
            TSM设置 = new ToolStripMenuItem();
            TSM添加相机 = new ToolStripMenuItem();
            TSM选择区域 = new ToolStripMenuItem();
            TSM测试 = new ToolStripMenuItem();
            TSM截取区域 = new ToolStripMenuItem();
            TSM打开图片 = new ToolStripMenuItem();
            TSM识别 = new ToolStripMenuItem();
            CB相机列表 = new ComboBox();
            BTN查找设备 = new Button();
            TB信息 = new TextBox();
            PB图片 = new PictureBox();
            LB相机列表 = new ListBox();
            CMS相机列表 = new ContextMenuStrip(components);
            TSM连接 = new ToolStripMenuItem();
            TSM断开 = new ToolStripMenuItem();
            TSM开始采集 = new ToolStripMenuItem();
            TSM停止采集 = new ToolStripMenuItem();
            TSM参数设置 = new ToolStripMenuItem();
            TSM删除 = new ToolStripMenuItem();
            BTN捕获图片 = new Button();
            BTN目标区域 = new Button();
            CB目标区域 = new ComboBox();
            BTN清除 = new Button();
            MS相机.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PB图片).BeginInit();
            CMS相机列表.SuspendLayout();
            SuspendLayout();
            // 
            // MS相机
            // 
            MS相机.Items.AddRange(new ToolStripItem[] { TSM设置, TSM测试 });
            MS相机.Location = new Point(0, 0);
            MS相机.Name = "MS相机";
            MS相机.Size = new Size(800, 25);
            MS相机.TabIndex = 0;
            MS相机.Text = "相机";
            // 
            // TSM设置
            // 
            TSM设置.DropDownItems.AddRange(new ToolStripItem[] { TSM添加相机, TSM选择区域 });
            TSM设置.Name = "TSM设置";
            TSM设置.Size = new Size(44, 21);
            TSM设置.Text = "设置";
            // 
            // TSM添加相机
            // 
            TSM添加相机.Name = "TSM添加相机";
            TSM添加相机.Size = new Size(124, 22);
            TSM添加相机.Text = "添加相机";
            // 
            // TSM选择区域
            // 
            TSM选择区域.Name = "TSM选择区域";
            TSM选择区域.Size = new Size(124, 22);
            TSM选择区域.Text = "选择区域";
            // 
            // TSM测试
            // 
            TSM测试.DropDownItems.AddRange(new ToolStripItem[] { TSM截取区域, TSM打开图片, TSM识别 });
            TSM测试.Name = "TSM测试";
            TSM测试.Size = new Size(44, 21);
            TSM测试.Text = "测试";
            // 
            // TSM截取区域
            // 
            TSM截取区域.Name = "TSM截取区域";
            TSM截取区域.Size = new Size(124, 22);
            TSM截取区域.Text = "截取区域";
            // 
            // TSM打开图片
            // 
            TSM打开图片.Name = "TSM打开图片";
            TSM打开图片.Size = new Size(124, 22);
            TSM打开图片.Text = "打开图片";
            // 
            // TSM识别
            // 
            TSM识别.Name = "TSM识别";
            TSM识别.Size = new Size(124, 22);
            TSM识别.Text = "识别";
            // 
            // CB相机列表
            // 
            CB相机列表.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CB相机列表.FormattingEnabled = true;
            CB相机列表.Location = new Point(194, 27);
            CB相机列表.Name = "CB相机列表";
            CB相机列表.Size = new Size(488, 25);
            CB相机列表.TabIndex = 1;
            // 
            // BTN查找设备
            // 
            BTN查找设备.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN查找设备.Location = new Point(688, 27);
            BTN查找设备.Name = "BTN查找设备";
            BTN查找设备.Size = new Size(100, 25);
            BTN查找设备.TabIndex = 2;
            BTN查找设备.Text = "查找设备";
            BTN查找设备.UseVisualStyleBackColor = true;
            // 
            // TB信息
            // 
            TB信息.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TB信息.Location = new Point(194, 344);
            TB信息.Multiline = true;
            TB信息.Name = "TB信息";
            TB信息.ScrollBars = ScrollBars.Vertical;
            TB信息.Size = new Size(488, 96);
            TB信息.TabIndex = 3;
            TB信息.TextChanged += TB信息_TextChanged;
            // 
            // PB图片
            // 
            PB图片.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PB图片.Location = new Point(194, 58);
            PB图片.Name = "PB图片";
            PB图片.Size = new Size(488, 280);
            PB图片.SizeMode = PictureBoxSizeMode.Zoom;
            PB图片.TabIndex = 4;
            PB图片.TabStop = false;
            // 
            // LB相机列表
            // 
            LB相机列表.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            LB相机列表.ContextMenuStrip = CMS相机列表;
            LB相机列表.FormattingEnabled = true;
            LB相机列表.HorizontalScrollbar = true;
            LB相机列表.ItemHeight = 17;
            LB相机列表.Location = new Point(10, 28);
            LB相机列表.Margin = new Padding(1);
            LB相机列表.Name = "LB相机列表";
            LB相机列表.Size = new Size(180, 412);
            LB相机列表.TabIndex = 5;
            // 
            // CMS相机列表
            // 
            CMS相机列表.Items.AddRange(new ToolStripItem[] { TSM连接, TSM断开, TSM开始采集, TSM停止采集, TSM参数设置, TSM删除 });
            CMS相机列表.Name = "CMS相机列表";
            CMS相机列表.Size = new Size(125, 136);
            CMS相机列表.Text = "相机列表";
            // 
            // TSM连接
            // 
            TSM连接.Name = "TSM连接";
            TSM连接.Size = new Size(124, 22);
            TSM连接.Text = "连接";
            // 
            // TSM断开
            // 
            TSM断开.Name = "TSM断开";
            TSM断开.Size = new Size(124, 22);
            TSM断开.Text = "断开";
            // 
            // TSM开始采集
            // 
            TSM开始采集.Name = "TSM开始采集";
            TSM开始采集.Size = new Size(124, 22);
            TSM开始采集.Text = "开始采集";
            // 
            // TSM停止采集
            // 
            TSM停止采集.Name = "TSM停止采集";
            TSM停止采集.Size = new Size(124, 22);
            TSM停止采集.Text = "停止采集";
            // 
            // TSM参数设置
            // 
            TSM参数设置.Name = "TSM参数设置";
            TSM参数设置.Size = new Size(124, 22);
            TSM参数设置.Text = "参数设置";
            // 
            // TSM删除
            // 
            TSM删除.Name = "TSM删除";
            TSM删除.Size = new Size(124, 22);
            TSM删除.Text = "删除";
            // 
            // BTN捕获图片
            // 
            BTN捕获图片.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN捕获图片.Location = new Point(688, 58);
            BTN捕获图片.Name = "BTN捕获图片";
            BTN捕获图片.Size = new Size(100, 25);
            BTN捕获图片.TabIndex = 9;
            BTN捕获图片.Text = "捕获图片";
            BTN捕获图片.UseVisualStyleBackColor = true;
            // 
            // BTN目标区域
            // 
            BTN目标区域.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN目标区域.Location = new Point(688, 120);
            BTN目标区域.Name = "BTN目标区域";
            BTN目标区域.Size = new Size(100, 25);
            BTN目标区域.TabIndex = 10;
            BTN目标区域.Text = "目标区域";
            BTN目标区域.UseVisualStyleBackColor = true;
            // 
            // CB目标区域
            // 
            CB目标区域.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CB目标区域.FormattingEnabled = true;
            CB目标区域.Location = new Point(688, 89);
            CB目标区域.Name = "CB目标区域";
            CB目标区域.Size = new Size(100, 25);
            CB目标区域.TabIndex = 11;
            // 
            // BTN清除
            // 
            BTN清除.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN清除.Location = new Point(688, 151);
            BTN清除.Name = "BTN清除";
            BTN清除.Size = new Size(100, 25);
            BTN清除.TabIndex = 12;
            BTN清除.Text = "清除";
            BTN清除.UseVisualStyleBackColor = true;
            // 
            // Setting_Camera
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTN清除);
            Controls.Add(CB目标区域);
            Controls.Add(BTN目标区域);
            Controls.Add(BTN捕获图片);
            Controls.Add(LB相机列表);
            Controls.Add(PB图片);
            Controls.Add(TB信息);
            Controls.Add(BTN查找设备);
            Controls.Add(CB相机列表);
            Controls.Add(MS相机);
            MainMenuStrip = MS相机;
            Name = "Setting_Camera";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "相机设置";
            FormClosing += CameraSetting_FormClosing;
            MS相机.ResumeLayout(false);
            MS相机.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PB图片).EndInit();
            CMS相机列表.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MS相机;
        private ToolStripMenuItem TSM设置;
        private ToolStripMenuItem TSM测试;
        private ContextMenuStrip CMS相机列表;
        public ComboBox CB相机列表;
        public Button BTN查找设备;
        public PictureBox PB图片;
        public ListBox LB相机列表;
        public ToolStripMenuItem TSM截取区域;
        public ToolStripMenuItem TSM识别;
        public ToolStripMenuItem TSM选择区域;
        public Button BTN捕获图片;
        public ToolStripMenuItem TSM打开图片;
        public ToolStripMenuItem TSM添加相机;
        public Button BTN目标区域;
        public ComboBox CB目标区域;
        public ToolStripMenuItem TSM连接;
        public ToolStripMenuItem TSM删除;
        public ToolStripMenuItem TSM开始采集;
        public ToolStripMenuItem TSM断开;
        public ToolStripMenuItem TSM停止采集;
        public ToolStripMenuItem TSM参数设置;
        public TextBox TB信息;
        public Button BTN清除;
    }
}