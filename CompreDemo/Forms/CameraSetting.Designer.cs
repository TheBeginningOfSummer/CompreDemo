namespace CompreDemo.Forms
{
    partial class CameraSetting
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
            TSM选择区域 = new ToolStripMenuItem();
            TSM打开软触发 = new ToolStripMenuItem();
            TSM关闭触发 = new ToolStripMenuItem();
            TSM测试 = new ToolStripMenuItem();
            TSM截取区域 = new ToolStripMenuItem();
            TSM识别 = new ToolStripMenuItem();
            CB相机列表 = new ComboBox();
            BTN查找设备 = new Button();
            TB信息 = new TextBox();
            PB图片 = new PictureBox();
            LB相机列表 = new ListBox();
            CMS相机列表 = new ContextMenuStrip(components);
            TSM连接断开 = new ToolStripMenuItem();
            TSM断开 = new ToolStripMenuItem();
            TSM开始采集 = new ToolStripMenuItem();
            TSM停止采集 = new ToolStripMenuItem();
            TSM删除 = new ToolStripMenuItem();
            TB相机名称 = new TextBox();
            label1 = new Label();
            BTN添加相机 = new Button();
            BTN触发图片 = new Button();
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
            TSM设置.DropDownItems.AddRange(new ToolStripItem[] { TSM选择区域, TSM打开软触发, TSM关闭触发 });
            TSM设置.Name = "TSM设置";
            TSM设置.Size = new Size(44, 21);
            TSM设置.Text = "设置";
            // 
            // TSM选择区域
            // 
            TSM选择区域.Name = "TSM选择区域";
            TSM选择区域.Size = new Size(136, 22);
            TSM选择区域.Text = "选择区域";
            TSM选择区域.Click += TSM选择区域_Click;
            // 
            // TSM打开软触发
            // 
            TSM打开软触发.Name = "TSM打开软触发";
            TSM打开软触发.Size = new Size(136, 22);
            TSM打开软触发.Text = "打开软触发";
            TSM打开软触发.Click += TSM打开软触发_Click;
            // 
            // TSM关闭触发
            // 
            TSM关闭触发.Name = "TSM关闭触发";
            TSM关闭触发.Size = new Size(136, 22);
            TSM关闭触发.Text = "关闭触发";
            TSM关闭触发.Click += TSM关闭触发_Click;
            // 
            // TSM测试
            // 
            TSM测试.DropDownItems.AddRange(new ToolStripItem[] { TSM截取区域, TSM识别 });
            TSM测试.Name = "TSM测试";
            TSM测试.Size = new Size(44, 21);
            TSM测试.Text = "测试";
            // 
            // TSM截取区域
            // 
            TSM截取区域.Name = "TSM截取区域";
            TSM截取区域.Size = new Size(124, 22);
            TSM截取区域.Text = "截取区域";
            TSM截取区域.Click += TSM截取区域_Click;
            // 
            // TSM识别
            // 
            TSM识别.Name = "TSM识别";
            TSM识别.Size = new Size(124, 22);
            TSM识别.Text = "识别";
            TSM识别.Click += TSM识别_Click;
            // 
            // CB相机列表
            // 
            CB相机列表.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CB相机列表.FormattingEnabled = true;
            CB相机列表.Location = new Point(182, 27);
            CB相机列表.Name = "CB相机列表";
            CB相机列表.Size = new Size(500, 25);
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
            BTN查找设备.Click += BTN查找设备_Click;
            // 
            // TB信息
            // 
            TB信息.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TB信息.Location = new Point(182, 344);
            TB信息.Multiline = true;
            TB信息.Name = "TB信息";
            TB信息.ScrollBars = ScrollBars.Vertical;
            TB信息.Size = new Size(500, 96);
            TB信息.TabIndex = 3;
            // 
            // PB图片
            // 
            PB图片.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PB图片.Location = new Point(182, 58);
            PB图片.Name = "PB图片";
            PB图片.Size = new Size(500, 280);
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
            LB相机列表.Location = new Point(10, 62);
            LB相机列表.Margin = new Padding(1);
            LB相机列表.Name = "LB相机列表";
            LB相机列表.Size = new Size(168, 378);
            LB相机列表.TabIndex = 5;
            LB相机列表.SelectedIndexChanged += LB相机列表_SelectedIndexChanged;
            // 
            // CMS相机列表
            // 
            CMS相机列表.Items.AddRange(new ToolStripItem[] { TSM连接断开, TSM断开, TSM开始采集, TSM停止采集, TSM删除 });
            CMS相机列表.Name = "CMS相机列表";
            CMS相机列表.Size = new Size(125, 114);
            CMS相机列表.Text = "相机列表";
            // 
            // TSM连接断开
            // 
            TSM连接断开.Name = "TSM连接断开";
            TSM连接断开.Size = new Size(124, 22);
            TSM连接断开.Text = "连接";
            TSM连接断开.Click += TSM连接_Click;
            // 
            // TSM断开
            // 
            TSM断开.Name = "TSM断开";
            TSM断开.Size = new Size(124, 22);
            TSM断开.Text = "断开";
            TSM断开.Click += TSM断开_Click;
            // 
            // TSM开始采集
            // 
            TSM开始采集.Name = "TSM开始采集";
            TSM开始采集.Size = new Size(124, 22);
            TSM开始采集.Text = "开始采集";
            TSM开始采集.Click += TSM开始采集_Click;
            // 
            // TSM停止采集
            // 
            TSM停止采集.Name = "TSM停止采集";
            TSM停止采集.Size = new Size(124, 22);
            TSM停止采集.Text = "停止采集";
            TSM停止采集.Click += TSM停止采集_Click;
            // 
            // TSM删除
            // 
            TSM删除.Name = "TSM删除";
            TSM删除.Size = new Size(124, 22);
            TSM删除.Text = "删除";
            TSM删除.Click += TSM删除_Click;
            // 
            // TB相机名称
            // 
            TB相机名称.Location = new Point(70, 28);
            TB相机名称.Name = "TB相机名称";
            TB相机名称.Size = new Size(106, 23);
            TB相机名称.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 31);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 7;
            label1.Text = "相机名称";
            // 
            // BTN添加相机
            // 
            BTN添加相机.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN添加相机.Location = new Point(688, 58);
            BTN添加相机.Name = "BTN添加相机";
            BTN添加相机.Size = new Size(100, 25);
            BTN添加相机.TabIndex = 8;
            BTN添加相机.Text = "添加相机";
            BTN添加相机.UseVisualStyleBackColor = true;
            BTN添加相机.Click += BTN添加相机_Click;
            // 
            // BTN触发图片
            // 
            BTN触发图片.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN触发图片.Location = new Point(688, 89);
            BTN触发图片.Name = "BTN触发图片";
            BTN触发图片.Size = new Size(100, 25);
            BTN触发图片.TabIndex = 9;
            BTN触发图片.Text = "触发图片";
            BTN触发图片.UseVisualStyleBackColor = true;
            BTN触发图片.Click += BTN触发图片_Click;
            // 
            // CameraSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTN触发图片);
            Controls.Add(BTN添加相机);
            Controls.Add(label1);
            Controls.Add(TB相机名称);
            Controls.Add(LB相机列表);
            Controls.Add(PB图片);
            Controls.Add(TB信息);
            Controls.Add(BTN查找设备);
            Controls.Add(CB相机列表);
            Controls.Add(MS相机);
            MainMenuStrip = MS相机;
            Name = "CameraSetting";
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
        private ComboBox CB相机列表;
        private Button BTN查找设备;
        private TextBox TB信息;
        private ToolStripMenuItem TSM设置;
        private ToolStripMenuItem TSM测试;
        private PictureBox PB图片;
        private ListBox LB相机列表;
        private TextBox TB相机名称;
        private Label label1;
        private ToolStripMenuItem TSM截取区域;
        private ToolStripMenuItem TSM识别;
        private ToolStripMenuItem TSM选择区域;
        private ContextMenuStrip CMS相机列表;
        private ToolStripMenuItem TSM连接断开;
        private ToolStripMenuItem TSM删除;
        private ToolStripMenuItem TSM开始采集;
        private ToolStripMenuItem TSM断开;
        private ToolStripMenuItem TSM打开软触发;
        private ToolStripMenuItem TSM关闭触发;
        private Button BTN添加相机;
        private Button BTN触发图片;
        private ToolStripMenuItem TSM停止采集;
    }
}