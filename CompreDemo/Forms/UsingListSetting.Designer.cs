namespace CompreDemo.Forms
{
    partial class UsingListSetting
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
            TSM添加 = new ToolStripMenuItem();
            TSM保存 = new ToolStripMenuItem();
            LB使用设备列表 = new ListBox();
            CMS设备列表 = new ContextMenuStrip(components);
            TSM删除 = new ToolStripMenuItem();
            TBText = new TextBox();
            BTN添加 = new Button();
            PN添加设备 = new Panel();
            CBDeviceType = new ComboBox();
            MS菜单.SuspendLayout();
            CMS设备列表.SuspendLayout();
            PN添加设备.SuspendLayout();
            SuspendLayout();
            // 
            // MS菜单
            // 
            MS菜单.Items.AddRange(new ToolStripItem[] { TSM添加, TSM保存 });
            MS菜单.Location = new Point(0, 0);
            MS菜单.Name = "MS菜单";
            MS菜单.Size = new Size(584, 25);
            MS菜单.TabIndex = 3;
            MS菜单.Text = "menuStrip1";
            // 
            // TSM添加
            // 
            TSM添加.Name = "TSM添加";
            TSM添加.Size = new Size(68, 21);
            TSM添加.Text = "添加列表";
            TSM添加.Click += TSM添加_Click;
            // 
            // TSM保存
            // 
            TSM保存.Name = "TSM保存";
            TSM保存.Size = new Size(44, 21);
            TSM保存.Text = "保存";
            TSM保存.Click += TSM保存_Click;
            // 
            // LB使用设备列表
            // 
            LB使用设备列表.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            LB使用设备列表.ContextMenuStrip = CMS设备列表;
            LB使用设备列表.FormattingEnabled = true;
            LB使用设备列表.ItemHeight = 17;
            LB使用设备列表.Location = new Point(12, 27);
            LB使用设备列表.Name = "LB使用设备列表";
            LB使用设备列表.Size = new Size(180, 327);
            LB使用设备列表.TabIndex = 4;
            LB使用设备列表.SelectedIndexChanged += LB使用设备列表_SelectedIndexChanged;
            // 
            // CMS设备列表
            // 
            CMS设备列表.Items.AddRange(new ToolStripItem[] { TSM删除 });
            CMS设备列表.Name = "CMS设备列表";
            CMS设备列表.Size = new Size(101, 26);
            // 
            // TSM删除
            // 
            TSM删除.Name = "TSM删除";
            TSM删除.Size = new Size(100, 22);
            TSM删除.Text = "删除";
            TSM删除.Click += TSM删除_Click;
            // 
            // TBText
            // 
            TBText.Dock = DockStyle.Fill;
            TBText.Location = new Point(0, 0);
            TBText.Multiline = true;
            TBText.Name = "TBText";
            TBText.ScrollBars = ScrollBars.Vertical;
            TBText.Size = new Size(374, 297);
            TBText.TabIndex = 5;
            // 
            // BTN添加
            // 
            BTN添加.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN添加.Location = new Point(497, 331);
            BTN添加.Name = "BTN添加";
            BTN添加.Size = new Size(75, 23);
            BTN添加.TabIndex = 6;
            BTN添加.Text = "添加设备";
            BTN添加.UseVisualStyleBackColor = true;
            BTN添加.Click += BTN添加_Click;
            // 
            // PN添加设备
            // 
            PN添加设备.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PN添加设备.AutoScroll = true;
            PN添加设备.Controls.Add(TBText);
            PN添加设备.Location = new Point(198, 28);
            PN添加设备.Name = "PN添加设备";
            PN添加设备.Size = new Size(374, 297);
            PN添加设备.TabIndex = 7;
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
            Controls.Add(BTN添加);
            Controls.Add(LB使用设备列表);
            Controls.Add(MS菜单);
            MainMenuStrip = MS菜单;
            Name = "UsingListSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "设备方案";
            MS菜单.ResumeLayout(false);
            MS菜单.PerformLayout();
            CMS设备列表.ResumeLayout(false);
            PN添加设备.ResumeLayout(false);
            PN添加设备.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip MS菜单;
        private ListBox LB使用设备列表;
        private TextBox TBText;
        private Button BTN添加;
        private ContextMenuStrip CMS设备列表;
        private ToolStripMenuItem TSM删除;
        private ToolStripMenuItem TSM添加;
        private ToolStripMenuItem TSM保存;
        private Panel PN添加设备;
        private ComboBox CBDeviceType;
    }
}