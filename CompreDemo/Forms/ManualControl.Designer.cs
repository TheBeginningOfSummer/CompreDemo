namespace CompreDemo.Forms
{
    partial class ManualControl
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
            LB目标位置 = new Label();
            TB目标位置 = new TextBox();
            MS运动控制 = new MenuStrip();
            BTN相对移动 = new Button();
            BTN绝对移动 = new Button();
            BTN后 = new Button();
            BTN前 = new Button();
            BTN回原点 = new Button();
            BTN停止 = new Button();
            BTN位置清零 = new Button();
            LB轴信息 = new Label();
            SuspendLayout();
            // 
            // LB目标位置
            // 
            LB目标位置.AutoSize = true;
            LB目标位置.Location = new Point(40, 43);
            LB目标位置.Name = "LB目标位置";
            LB目标位置.Size = new Size(56, 17);
            LB目标位置.TabIndex = 0;
            LB目标位置.Text = "目标位置";
            // 
            // TB目标位置
            // 
            TB目标位置.Location = new Point(102, 40);
            TB目标位置.Name = "TB目标位置";
            TB目标位置.Size = new Size(93, 23);
            TB目标位置.TabIndex = 1;
            // 
            // MS运动控制
            // 
            MS运动控制.Location = new Point(0, 0);
            MS运动控制.Name = "MS运动控制";
            MS运动控制.Size = new Size(434, 24);
            MS运动控制.TabIndex = 2;
            MS运动控制.Text = "运动控制";
            // 
            // BTN相对移动
            // 
            BTN相对移动.Location = new Point(40, 69);
            BTN相对移动.Name = "BTN相对移动";
            BTN相对移动.Size = new Size(75, 23);
            BTN相对移动.TabIndex = 3;
            BTN相对移动.Text = "相对移动";
            BTN相对移动.UseVisualStyleBackColor = true;
            BTN相对移动.Click += BTN相对移动_Click;
            // 
            // BTN绝对移动
            // 
            BTN绝对移动.Location = new Point(120, 69);
            BTN绝对移动.Name = "BTN绝对移动";
            BTN绝对移动.Size = new Size(75, 23);
            BTN绝对移动.TabIndex = 4;
            BTN绝对移动.Text = "绝对移动";
            BTN绝对移动.UseVisualStyleBackColor = true;
            BTN绝对移动.Click += BTN绝对移动_Click;
            // 
            // BTN后
            // 
            BTN后.Location = new Point(40, 98);
            BTN后.Name = "BTN后";
            BTN后.Size = new Size(75, 23);
            BTN后.TabIndex = 5;
            BTN后.Text = "←";
            BTN后.UseVisualStyleBackColor = true;
            BTN后.Click += BTN后_Click;
            // 
            // BTN前
            // 
            BTN前.Location = new Point(120, 98);
            BTN前.Name = "BTN前";
            BTN前.Size = new Size(75, 23);
            BTN前.TabIndex = 6;
            BTN前.Text = "→";
            BTN前.UseVisualStyleBackColor = true;
            BTN前.Click += BTN前_Click;
            // 
            // BTN回原点
            // 
            BTN回原点.Location = new Point(40, 127);
            BTN回原点.Name = "BTN回原点";
            BTN回原点.Size = new Size(75, 23);
            BTN回原点.TabIndex = 7;
            BTN回原点.Text = "回原点";
            BTN回原点.UseVisualStyleBackColor = true;
            BTN回原点.Click += BTN回原点_Click;
            // 
            // BTN停止
            // 
            BTN停止.Location = new Point(120, 127);
            BTN停止.Name = "BTN停止";
            BTN停止.Size = new Size(75, 23);
            BTN停止.TabIndex = 8;
            BTN停止.Text = "停止";
            BTN停止.UseVisualStyleBackColor = true;
            BTN停止.Click += BTN停止_Click;
            // 
            // BTN位置清零
            // 
            BTN位置清零.Location = new Point(200, 40);
            BTN位置清零.Name = "BTN位置清零";
            BTN位置清零.Size = new Size(75, 23);
            BTN位置清零.TabIndex = 9;
            BTN位置清零.Text = "位置清零";
            BTN位置清零.UseVisualStyleBackColor = true;
            BTN位置清零.Click += BTN位置清零_Click;
            // 
            // LB轴信息
            // 
            LB轴信息.AutoSize = true;
            LB轴信息.Location = new Point(281, 43);
            LB轴信息.Name = "LB轴信息";
            LB轴信息.Size = new Size(44, 17);
            LB轴信息.TabIndex = 10;
            LB轴信息.Text = "轴信息";
            // 
            // ManualControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 181);
            Controls.Add(LB轴信息);
            Controls.Add(BTN位置清零);
            Controls.Add(BTN停止);
            Controls.Add(BTN回原点);
            Controls.Add(BTN前);
            Controls.Add(BTN后);
            Controls.Add(BTN绝对移动);
            Controls.Add(BTN相对移动);
            Controls.Add(TB目标位置);
            Controls.Add(LB目标位置);
            Controls.Add(MS运动控制);
            MainMenuStrip = MS运动控制;
            Name = "ManualControl";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManualControl";
            FormClosing += ManualControl_FormClosing;
            FormClosed += ManualControl_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LB目标位置;
        private TextBox TB目标位置;
        private MenuStrip MS运动控制;
        private Button BTN相对移动;
        private Button BTN绝对移动;
        private Button BTN后;
        private Button BTN前;
        private Button BTN回原点;
        private Button BTN停止;
        private Button BTN位置清零;
        private Label LB轴信息;
    }
}