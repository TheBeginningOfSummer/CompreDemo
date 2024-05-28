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
            BTN保存 = new Button();
            MS菜单 = new MenuStrip();
            LB使用设备列表 = new ListBox();
            TBText = new TextBox();
            BTN添加 = new Button();
            CMS设备列表 = new ContextMenuStrip(components);
            TSM删除 = new ToolStripMenuItem();
            CMS设备列表.SuspendLayout();
            SuspendLayout();
            // 
            // BTN保存
            // 
            BTN保存.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN保存.Location = new Point(497, 331);
            BTN保存.Name = "BTN保存";
            BTN保存.Size = new Size(75, 23);
            BTN保存.TabIndex = 1;
            BTN保存.Text = "保存";
            BTN保存.UseVisualStyleBackColor = true;
            BTN保存.Click += BTN保存_Click;
            // 
            // MS菜单
            // 
            MS菜单.Location = new Point(0, 0);
            MS菜单.Name = "MS菜单";
            MS菜单.Size = new Size(584, 24);
            MS菜单.TabIndex = 3;
            MS菜单.Text = "menuStrip1";
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
            // TBText
            // 
            TBText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TBText.Location = new Point(198, 27);
            TBText.Multiline = true;
            TBText.Name = "TBText";
            TBText.ScrollBars = ScrollBars.Vertical;
            TBText.Size = new Size(374, 298);
            TBText.TabIndex = 5;
            // 
            // BTN添加
            // 
            BTN添加.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN添加.Location = new Point(416, 331);
            BTN添加.Name = "BTN添加";
            BTN添加.Size = new Size(75, 23);
            BTN添加.TabIndex = 6;
            BTN添加.Text = "添加";
            BTN添加.UseVisualStyleBackColor = true;
            BTN添加.Click += BTN添加_Click;
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
            // UsingListSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(BTN添加);
            Controls.Add(TBText);
            Controls.Add(LB使用设备列表);
            Controls.Add(BTN保存);
            Controls.Add(MS菜单);
            MainMenuStrip = MS菜单;
            Name = "UsingListSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "设备列表";
            CMS设备列表.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BTN保存;
        private MenuStrip MS菜单;
        private ListBox LB使用设备列表;
        private TextBox TBText;
        private Button BTN添加;
        private ContextMenuStrip CMS设备列表;
        private ToolStripMenuItem TSM删除;
    }
}