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
            resources.ApplyResources(comboBoxProcesses, "comboBoxProcesses");
            comboBoxProcesses.ForeColor = Color.White;
            comboBoxProcesses.Name = "comboBoxProcesses";
            comboBoxProcesses.DropDown += ComboBoxProcesses_DropDown;
            comboBoxProcesses.SelectedIndexChanged += comboBoxProcesses_SelectedIndexChanged;
            // 
            // numericUpDownStrength
            // 
            numericUpDownStrength.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(numericUpDownStrength, "numericUpDownStrength");
            numericUpDownStrength.ForeColor = Color.White;
            numericUpDownStrength.Name = "numericUpDownStrength";
            numericUpDownStrength.ValueChanged += numericUpDownStrength_ValueChanged;
            // 
            // numericUpDownDelay
            // 
            numericUpDownDelay.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(numericUpDownDelay, "numericUpDownDelay");
            numericUpDownDelay.ForeColor = Color.White;
            numericUpDownDelay.Name = "numericUpDownDelay";
            // 
            // buttonStart
            // 
            buttonStart.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(buttonStart, "buttonStart");
            buttonStart.Name = "buttonStart";
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.Name = "buttonStop";
            buttonStop.Click += buttonStop_Click;
            // 
            // comboBoxToggleKey
            // 
            comboBoxToggleKey.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(comboBoxToggleKey, "comboBoxToggleKey");
            comboBoxToggleKey.ForeColor = Color.White;
            comboBoxToggleKey.Name = "comboBoxToggleKey";
            comboBoxToggleKey.SelectedIndexChanged += comboBoxToggleKey_SelectedIndexChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel1.LinkColor = Color.WhiteSmoke;
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(checkBox2, "checkBox2");
            checkBox2.ForeColor = Color.White;
            checkBox2.Name = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            checkBox2.Click += checkBox1_CheckedChanged;
            // 
            // checkBox1
            // 
            resources.ApplyResources(checkBox1, "checkBox1");
            checkBox1.ForeColor = Color.White;
            checkBox1.Name = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox1.Click += checkBox2_CheckedChanged;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // numericUpDownPullDownStrength
            // 
            numericUpDownPullDownStrength.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(numericUpDownPullDownStrength, "numericUpDownPullDownStrength");
            numericUpDownPullDownStrength.ForeColor = Color.White;
            numericUpDownPullDownStrength.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDownPullDownStrength.Name = "numericUpDownPullDownStrength";
            numericUpDownPullDownStrength.ValueChanged += numericUpDownPullDownStrength_ValueChanged;
            // 
            // checkBoxAdsOnly
            // 
            resources.ApplyResources(checkBoxAdsOnly, "checkBoxAdsOnly");
            checkBoxAdsOnly.Name = "checkBoxAdsOnly";
            checkBoxAdsOnly.UseVisualStyleBackColor = true;
            checkBoxAdsOnly.CheckedChanged += checkBoxAdsOnly_CheckedChanged;
            checkBoxAdsOnly.Click += checkBoxAdsOnly_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            BackColor = Color.FromArgb(33, 33, 33);
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
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            MaximizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            Load += MainForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPullDownStrength).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}