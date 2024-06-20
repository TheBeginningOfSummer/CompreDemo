namespace CompreDemo.Forms
{
    partial class UsingPlan
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
            MS菜单 = new MenuStrip();
            TSM添加方案 = new ToolStripMenuItem();
            LB设备方案列表 = new ListBox();
            CMS设备方案列表 = new ContextMenuStrip(components);
            TSM删除方案 = new ToolStripMenuItem();
            BTN添加设备 = new Button();
            PN添加设备 = new Panel();
            LB设备列表 = new ListBox();
            CMS设备列表 = new ContextMenuStrip(components);
            TSM删除设备 = new ToolStripMenuItem();
            CBDeviceType = new ComboBox();
            MS菜单.SuspendLayout();
            CMS设备方案列表.SuspendLayout();
            PN添加设备.SuspendLayout();
            CMS设备列表.SuspendLayout();
            SuspendLayout();
            // 
            // MS菜单
            // 
            MS菜单.Items.AddRange(new ToolStripItem[] { TSM添加方案 });
            MS菜单.Location = new Point(0, 0);
            MS菜单.Name = "MS菜单";
            MS菜单.Size = new Size(584, 25);
            MS菜单.TabIndex = 3;
            MS菜单.Text = "menuStrip1";
            // 
            // TSM添加方案
            // 
            TSM添加方案.Name = "TSM添加方案";
            TSM添加方案.Size = new Size(68, 21);
            TSM添加方案.Text = "添加方案";
            TSM添加方案.Click += TSM添加方案_Click;
            // 
            // LB设备方案列表
            // 
            LB设备方案列表.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            LB设备方案列表.ContextMenuStrip = CMS设备方案列表;
            LB设备方案列表.FormattingEnabled = true;
            LB设备方案列表.ItemHeight = 17;
            LB设备方案列表.Location = new Point(12, 27);
            LB设备方案列表.Name = "LB设备方案列表";
            LB设备方案列表.Size = new Size(180, 327);
            LB设备方案列表.TabIndex = 4;
            LB设备方案列表.SelectedIndexChanged += LB设备方案列表_SelectedIndexChanged;
            // 
            // CMS设备方案列表
            // 
            CMS设备方案列表.Items.AddRange(new ToolStripItem[] { TSM删除方案 });
            CMS设备方案列表.Name = "CMS设备列表";
            CMS设备方案列表.Size = new Size(101, 26);
            // 
            // TSM删除方案
            // 
            TSM删除方案.Name = "TSM删除方案";
            TSM删除方案.Size = new Size(100, 22);
            TSM删除方案.Text = "删除";
            TSM删除方案.Click += TSM删除方案_Click;
            // 
            // BTN添加设备
            // 
            BTN添加设备.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN添加设备.Location = new Point(497, 331);
            BTN添加设备.Name = "BTN添加设备";
            BTN添加设备.Size = new Size(75, 23);
            BTN添加设备.TabIndex = 6;
            BTN添加设备.Text = "添加设备";
            BTN添加设备.UseVisualStyleBackColor = true;
            BTN添加设备.Click += BTN添加设备_Click;
            // 
            // PN添加设备
            // 
            PN添加设备.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PN添加设备.AutoScroll = true;
            PN添加设备.Controls.Add(LB设备列表);
            PN添加设备.Location = new Point(198, 28);
            PN添加设备.Name = "PN添加设备";
            PN添加设备.Size = new Size(374, 297);
            PN添加设备.TabIndex = 7;
            // 
            // LB设备列表
            // 
            LB设备列表.ContextMenuStrip = CMS设备列表;
            LB设备列表.Dock = DockStyle.Fill;
            LB设备列表.FormattingEnabled = true;
            LB设备列表.ItemHeight = 17;
            LB设备列表.Location = new Point(0, 0);
            LB设备列表.Name = "LB设备列表";
            LB设备列表.Size = new Size(374, 297);
            LB设备列表.TabIndex = 0;
            // 
            // CMS设备列表
            // 
            CMS设备列表.Items.AddRange(new ToolStripItem[] { TSM删除设备 });
            CMS设备列表.Name = "CMS设备列表";
            CMS设备列表.Size = new Size(101, 26);
            // 
            // TSM删除设备
            // 
            TSM删除设备.Name = "TSM删除设备";
            TSM删除设备.Size = new Size(100, 22);
            TSM删除设备.Text = "删除";
            TSM删除设备.Click += TSM删除设备_Click;
            // 
            // CBDeviceType
            // 
            CBDeviceType.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CBDeviceType.FormattingEnabled = true;
            CBDeviceType.Items.AddRange(new object[] { "ControlCard", "Camera", "ROI" });
            CBDeviceType.Location = new Point(370, 331);
            CBDeviceType.Name = "CBDeviceType";
            CBDeviceType.Size = new Size(121, 25);
            CBDeviceType.TabIndex = 8;
            CBDeviceType.Text = "ControlCard";
            // 
            // UsingListSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(CBDeviceType);
            Controls.Add(PN添加设备);
            Controls.Add(BTN添加设备);
            Controls.Add(LB设备方案列表);
            Controls.Add(MS菜单);
            MainMenuStrip = MS菜单;
            Name = "UsingListSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "设备方案";
            MS菜单.ResumeLayout(false);
            MS菜单.PerformLayout();
            CMS设备方案列表.ResumeLayout(false);
            PN添加设备.ResumeLayout(false);
            CMS设备列表.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip MS菜单;
        private ListBox LB设备方案列表;
        private Button BTN添加设备;
        private ContextMenuStrip CMS设备方案列表;
        private ToolStripMenuItem TSM删除方案;
        private ToolStripMenuItem TSM添加方案;
        private Panel PN添加设备;
        private ComboBox CBDeviceType;
        private ListBox LB设备列表;
        private ContextMenuStrip CMS设备列表;
        private ToolStripMenuItem TSM删除设备;
    }
}