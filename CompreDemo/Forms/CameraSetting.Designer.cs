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
            MS相机 = new MenuStrip();
            TSM采集 = new ToolStripMenuItem();
            TSM开始采集 = new ToolStripMenuItem();
            TSM停止采集 = new ToolStripMenuItem();
            TSM设备 = new ToolStripMenuItem();
            TSM打开设备 = new ToolStripMenuItem();
            TSM关闭设备 = new ToolStripMenuItem();
            TSM显示图片 = new ToolStripMenuItem();
            CB相机列表 = new ComboBox();
            BTN查找设备 = new Button();
            TB信息 = new TextBox();
            PB图片 = new PictureBox();
            BTN设置 = new Button();
            MS相机.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PB图片).BeginInit();
            SuspendLayout();
            // 
            // MS相机
            // 
            MS相机.Items.AddRange(new ToolStripItem[] { TSM采集, TSM设备 });
            MS相机.Location = new Point(0, 0);
            MS相机.Name = "MS相机";
            MS相机.Size = new Size(800, 25);
            MS相机.TabIndex = 0;
            MS相机.Text = "相机";
            // 
            // TSM采集
            // 
            TSM采集.DropDownItems.AddRange(new ToolStripItem[] { TSM开始采集, TSM停止采集 });
            TSM采集.Name = "TSM采集";
            TSM采集.Size = new Size(44, 21);
            TSM采集.Text = "采集";
            // 
            // TSM开始采集
            // 
            TSM开始采集.Name = "TSM开始采集";
            TSM开始采集.Size = new Size(180, 22);
            TSM开始采集.Text = "开始采集";
            TSM开始采集.Click += TSM开始采集_Click;
            // 
            // TSM停止采集
            // 
            TSM停止采集.Name = "TSM停止采集";
            TSM停止采集.Size = new Size(180, 22);
            TSM停止采集.Text = "停止采集";
            TSM停止采集.Click += TSM停止采集_Click;
            // 
            // TSM设备
            // 
            TSM设备.DropDownItems.AddRange(new ToolStripItem[] { TSM打开设备, TSM关闭设备, TSM显示图片 });
            TSM设备.Name = "TSM设备";
            TSM设备.Size = new Size(44, 21);
            TSM设备.Text = "设备";
            // 
            // TSM打开设备
            // 
            TSM打开设备.Name = "TSM打开设备";
            TSM打开设备.Size = new Size(124, 22);
            TSM打开设备.Text = "打开设备";
            TSM打开设备.Click += TSM打开设备_Click;
            // 
            // TSM关闭设备
            // 
            TSM关闭设备.Name = "TSM关闭设备";
            TSM关闭设备.Size = new Size(124, 22);
            TSM关闭设备.Text = "关闭设备";
            TSM关闭设备.Click += TSM关闭设备_Click;
            // 
            // TSM显示图片
            // 
            TSM显示图片.Name = "TSM显示图片";
            TSM显示图片.Size = new Size(124, 22);
            TSM显示图片.Text = "显示图片";
            TSM显示图片.Click += TSM显示图片_Click;
            // 
            // CB相机列表
            // 
            CB相机列表.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CB相机列表.FormattingEnabled = true;
            CB相机列表.Location = new Point(12, 27);
            CB相机列表.Name = "CB相机列表";
            CB相机列表.Size = new Size(670, 25);
            CB相机列表.TabIndex = 1;
            // 
            // BTN查找设备
            // 
            BTN查找设备.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN查找设备.Location = new Point(688, 26);
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
            TB信息.Location = new Point(12, 347);
            TB信息.Multiline = true;
            TB信息.Name = "TB信息";
            TB信息.ScrollBars = ScrollBars.Vertical;
            TB信息.Size = new Size(670, 91);
            TB信息.TabIndex = 3;
            // 
            // PB图片
            // 
            PB图片.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PB图片.Location = new Point(12, 58);
            PB图片.Name = "PB图片";
            PB图片.Size = new Size(670, 283);
            PB图片.TabIndex = 4;
            PB图片.TabStop = false;
            // 
            // BTN设置
            // 
            BTN设置.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTN设置.Location = new Point(688, 58);
            BTN设置.Name = "BTN设置";
            BTN设置.Size = new Size(100, 25);
            BTN设置.TabIndex = 5;
            BTN设置.Text = "参数设置";
            BTN设置.UseVisualStyleBackColor = true;
            // 
            // CameraSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTN设置);
            Controls.Add(PB图片);
            Controls.Add(TB信息);
            Controls.Add(BTN查找设备);
            Controls.Add(CB相机列表);
            Controls.Add(MS相机);
            MainMenuStrip = MS相机;
            Name = "CameraSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CameraSetting";
            MS相机.ResumeLayout(false);
            MS相机.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PB图片).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MS相机;
        private ComboBox CB相机列表;
        private Button BTN查找设备;
        private TextBox TB信息;
        private ToolStripMenuItem TSM采集;
        private ToolStripMenuItem TSM设备;
        private ToolStripMenuItem TSM开始采集;
        private ToolStripMenuItem TSM停止采集;
        private ToolStripMenuItem TSM打开设备;
        private ToolStripMenuItem TSM关闭设备;
        private ToolStripMenuItem TSM显示图片;
        private PictureBox PB图片;
        private Button BTN设置;
    }
}