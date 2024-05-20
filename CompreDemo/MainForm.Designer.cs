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
            MS菜单 = new MenuStrip();
            TSM设置 = new ToolStripMenuItem();
            TSM相机配置 = new ToolStripMenuItem();
            TSM控制卡配置 = new ToolStripMenuItem();
            label1 = new Label();
            LB相机状态 = new Label();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(93, 237);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 1;
            label1.Text = "相机状态：";
            // 
            // LB相机状态
            // 
            LB相机状态.BackColor = Color.Red;
            LB相机状态.Location = new Point(156, 238);
            LB相机状态.Name = "LB相机状态";
            LB相机状态.Size = new Size(24, 16);
            LB相机状态.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(LB相机状态);
            Controls.Add(label1);
            Controls.Add(MS菜单);
            MainMenuStrip = MS菜单;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "测试";
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
        private Label label1;
        private Label LB相机状态;
    }
}
