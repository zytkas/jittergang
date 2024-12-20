namespace jittergang
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxProcesses;
        private NumericUpDown numericUpDownStrength;
        private NumericUpDown numericUpDownDelay;
        private NumericUpDown numericUpDownPullDownStrength;
        private Button buttonStart;
        private Button buttonStop;
        private ComboBox comboBoxToggleKey;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBoxAdsOnly;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LinkLabel linkLabel1;
        private ToolTip toolTip1;

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

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            comboBoxProcesses = new ComboBox();
            numericUpDownStrength = new NumericUpDown();
            numericUpDownDelay = new NumericUpDown();
            buttonStart = new Button();
            buttonStop = new Button();
            comboBoxToggleKey = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            linkLabel1 = new LinkLabel();
            toolTip1 = new ToolTip(components);
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            label5 = new Label();
            numericUpDownPullDownStrength = new NumericUpDown();
            checkBoxAdsOnly = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPullDownStrength).BeginInit();
            SuspendLayout();
            // 
            // comboBoxProcesses
            // 
            comboBoxProcesses.BackColor = Color.FromArgb(33, 33, 33);
            comboBoxProcesses.Font = new Font("Segoe UI", 11F);
            comboBoxProcesses.ForeColor = Color.White;
            comboBoxProcesses.Location = new Point(63, 37);
            comboBoxProcesses.Name = "comboBoxProcesses";
            comboBoxProcesses.Size = new Size(199, 28);
            comboBoxProcesses.TabIndex = 0;
            comboBoxProcesses.DropDown += ComboBoxProcesses_DropDown;
            comboBoxProcesses.SelectedIndexChanged += comboBoxProcesses_SelectedIndexChanged;
            // 
            // numericUpDownStrength
            // 
            numericUpDownStrength.BackColor = Color.FromArgb(33, 33, 33);
            numericUpDownStrength.Font = new Font("Segoe UI", 11F);
            numericUpDownStrength.ForeColor = Color.White;
            numericUpDownStrength.Location = new Point(102, 97);
            numericUpDownStrength.Name = "numericUpDownStrength";
            numericUpDownStrength.Size = new Size(120, 27);
            numericUpDownStrength.TabIndex = 1;
            numericUpDownStrength.ValueChanged += numericUpDownStrength_ValueChanged;
            // 
            // numericUpDownDelay
            // 
            numericUpDownDelay.BackColor = Color.FromArgb(33, 33, 33);
            numericUpDownDelay.Font = new Font("Segoe UI", 11F);
            numericUpDownDelay.ForeColor = Color.White;
            numericUpDownDelay.Location = new Point(102, 156);
            numericUpDownDelay.Name = "numericUpDownDelay";
            numericUpDownDelay.Size = new Size(120, 27);
            numericUpDownDelay.TabIndex = 2;
            // 
            // buttonStart
            // 
            buttonStart.FlatAppearance.BorderSize = 0;
            buttonStart.Location = new Point(63, 410);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(74, 26);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Start";
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.FlatAppearance.BorderSize = 0;
            buttonStop.Location = new Point(187, 410);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(75, 26);
            buttonStop.TabIndex = 5;
            buttonStop.Text = "Stop";
            buttonStop.Click += buttonStop_Click;
            // 
            // comboBoxToggleKey
            // 
            comboBoxToggleKey.BackColor = Color.FromArgb(33, 33, 33);
            comboBoxToggleKey.Font = new Font("Segoe UI", 11F);
            comboBoxToggleKey.ForeColor = Color.White;
            comboBoxToggleKey.Location = new Point(102, 274);
            comboBoxToggleKey.Name = "comboBoxToggleKey";
            comboBoxToggleKey.Size = new Size(120, 28);
            comboBoxToggleKey.TabIndex = 9;
            comboBoxToggleKey.SelectedIndexChanged += comboBoxToggleKey_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(133, 11);
            label1.Name = "label1";
            label1.Size = new Size(58, 20);
            label1.TabIndex = 10;
            label1.Text = "Process\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(130, 71);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 11;
            label2.Text = "Strength";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(139, 130);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 12;
            label3.Text = "Delay";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(115, 248);
            label4.Name = "label4";
            label4.Size = new Size(95, 20);
            label4.TabIndex = 13;
            label4.Text = "Turn ON/OFF";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel1.LinkColor = Color.WhiteSmoke;
            linkLabel1.Location = new Point(112, 456);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(105, 20);
            linkLabel1.TabIndex = 14;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "made by zytka";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.ForeColor = Color.White;
            checkBox2.Location = new Point(118, 308);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(97, 24);
            checkBox2.TabIndex = 16;
            checkBox2.Text = "СircleJitter";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            checkBox2.Click += checkBox1_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(116, 338);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 24);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "Controller?";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox1.Click += checkBox2_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(127, 189);
            label5.Name = "label5";
            label5.Size = new Size(70, 20);
            label5.TabIndex = 19;
            label5.Text = "Pulldown";
            // 
            // numericUpDownPullDownStrength
            // 
            numericUpDownPullDownStrength.BackColor = Color.FromArgb(33, 33, 33);
            numericUpDownPullDownStrength.Font = new Font("Segoe UI", 11F);
            numericUpDownPullDownStrength.ForeColor = Color.White;
            numericUpDownPullDownStrength.Location = new Point(102, 215);
            numericUpDownPullDownStrength.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDownPullDownStrength.Name = "numericUpDownPullDownStrength";
            numericUpDownPullDownStrength.Size = new Size(120, 27);
            numericUpDownPullDownStrength.TabIndex = 20;
            numericUpDownPullDownStrength.ValueChanged += numericUpDownPullDownStrength_ValueChanged;
            // 
            // checkBoxAdsOnly
            // 
            checkBoxAdsOnly.AutoSize = true;
            checkBoxAdsOnly.Location = new Point(122, 370);
            checkBoxAdsOnly.Name = "checkBoxAdsOnly";
            checkBoxAdsOnly.Size = new Size(89, 24);
            checkBoxAdsOnly.TabIndex = 21;
            checkBoxAdsOnly.Text = "ADS only";
            checkBoxAdsOnly.UseVisualStyleBackColor = true;
            checkBoxAdsOnly.CheckedChanged += checkBoxAdsOnly_CheckedChanged;
            checkBoxAdsOnly.Click += checkBoxAdsOnly_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            BackColor = Color.FromArgb(33, 33, 33);
            ClientSize = new Size(318, 489);
            Controls.Add(checkBoxAdsOnly);
            Controls.Add(numericUpDownPullDownStrength);
            Controls.Add(label5);
            Controls.Add(checkBox1);
            Controls.Add(checkBox2);
            Controls.Add(linkLabel1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBoxProcesses);
            Controls.Add(numericUpDownStrength);
            Controls.Add(numericUpDownDelay);
            Controls.Add(buttonStart);
            Controls.Add(buttonStop);
            Controls.Add(comboBoxToggleKey);
            Font = new Font("Segoe UI", 11F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "jittergang";
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPullDownStrength).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
    }
}