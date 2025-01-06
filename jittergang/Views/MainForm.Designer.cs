namespace jittergang
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxProcesses;
        private Label label1;
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
            label1 = new Label();
            toolTip1 = new ToolTip(components);
            label6 = new Label();
            airSeparator1 = new ReaLTaiizor.Controls.AirSeparator();
            hopePictureBox1 = new ReaLTaiizor.Controls.HopePictureBox();
            hopePictureBox2 = new ReaLTaiizor.Controls.HopePictureBox();
            comboBoxToggleKey = new ComboBox();
            buttonStop = new Button();
            buttonStart = new Button();
            numericUpDownDelay = new NumericUpDown();
            numericUpDownStrength = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            linkLabel1 = new LinkLabel();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            label5 = new Label();
            numericUpDownPullDownStrength = new NumericUpDown();
            checkBoxAdsOnly = new CheckBox();
            foxLabel1 = new ReaLTaiizor.Controls.FoxLabel();
            foxLabel2 = new ReaLTaiizor.Controls.FoxLabel();
            foxLabel3 = new ReaLTaiizor.Controls.FoxLabel();
            airSeparator2 = new ReaLTaiizor.Controls.AirSeparator();
            airSeparator3 = new ReaLTaiizor.Controls.AirSeparator();
            airSeparator4 = new ReaLTaiizor.Controls.AirSeparator();
            airSeparator5 = new ReaLTaiizor.Controls.AirSeparator();
            airSeparator6 = new ReaLTaiizor.Controls.AirSeparator();
            airSeparator7 = new ReaLTaiizor.Controls.AirSeparator();
            ((System.ComponentModel.ISupportInitialize)hopePictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hopePictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).BeginInit();
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
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // airSeparator1
            // 
            airSeparator1.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator1.Customization = "";
            resources.ApplyResources(airSeparator1, "airSeparator1");
            airSeparator1.Image = null;
            airSeparator1.Name = "airSeparator1";
            airSeparator1.NoRounding = false;
            airSeparator1.Transparent = false;
            airSeparator1.Click += airSeparator1_Click;
            // 
            // hopePictureBox1
            // 
            hopePictureBox1.BackColor = Color.FromArgb(192, 196, 204);
            resources.ApplyResources(hopePictureBox1, "hopePictureBox1");
            hopePictureBox1.Name = "hopePictureBox1";
            hopePictureBox1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            hopePictureBox1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            hopePictureBox1.TabStop = false;
            hopePictureBox1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // hopePictureBox2
            // 
            hopePictureBox2.BackColor = Color.FromArgb(192, 196, 204);
            resources.ApplyResources(hopePictureBox2, "hopePictureBox2");
            hopePictureBox2.Name = "hopePictureBox2";
            hopePictureBox2.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            hopePictureBox2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            hopePictureBox2.TabStop = false;
            hopePictureBox2.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // comboBoxToggleKey
            // 
            comboBoxToggleKey.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(comboBoxToggleKey, "comboBoxToggleKey");
            comboBoxToggleKey.ForeColor = Color.White;
            comboBoxToggleKey.Name = "comboBoxToggleKey";
            comboBoxToggleKey.SelectedIndexChanged += comboBoxToggleKey_SelectedIndexChanged;
            // 
            // buttonStop
            // 
            buttonStop.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.Name = "buttonStop";
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonStart
            // 
            buttonStart.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(buttonStart, "buttonStart");
            buttonStart.Name = "buttonStart";
            buttonStart.Click += buttonStart_Click;
            // 
            // numericUpDownDelay
            // 
            numericUpDownDelay.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(numericUpDownDelay, "numericUpDownDelay");
            numericUpDownDelay.ForeColor = Color.White;
            numericUpDownDelay.Name = "numericUpDownDelay";
            // 
            // numericUpDownStrength
            // 
            numericUpDownStrength.BackColor = Color.FromArgb(33, 33, 33);
            resources.ApplyResources(numericUpDownStrength, "numericUpDownStrength");
            numericUpDownStrength.ForeColor = Color.White;
            numericUpDownStrength.Name = "numericUpDownStrength";
            numericUpDownStrength.ValueChanged += numericUpDownStrength_ValueChanged;
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
            label4.Click += label4_Click;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel1.LinkColor = Color.WhiteSmoke;
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
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
            // foxLabel1
            // 
            foxLabel1.BackColor = Color.WhiteSmoke;
            resources.ApplyResources(foxLabel1, "foxLabel1");
            foxLabel1.ForeColor = Color.FromArgb(76, 88, 100);
            foxLabel1.Name = "foxLabel1";
            // 
            // foxLabel2
            // 
            foxLabel2.BackColor = Color.WhiteSmoke;
            resources.ApplyResources(foxLabel2, "foxLabel2");
            foxLabel2.ForeColor = Color.FromArgb(76, 88, 100);
            foxLabel2.Name = "foxLabel2";
            // 
            // foxLabel3
            // 
            foxLabel3.BackColor = Color.WhiteSmoke;
            resources.ApplyResources(foxLabel3, "foxLabel3");
            foxLabel3.ForeColor = Color.FromArgb(76, 88, 100);
            foxLabel3.Name = "foxLabel3";
            // 
            // airSeparator2
            // 
            airSeparator2.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator2.Customization = "";
            resources.ApplyResources(airSeparator2, "airSeparator2");
            airSeparator2.Image = null;
            airSeparator2.Name = "airSeparator2";
            airSeparator2.NoRounding = false;
            airSeparator2.Transparent = false;
            // 
            // airSeparator3
            // 
            airSeparator3.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator3.Customization = "";
            resources.ApplyResources(airSeparator3, "airSeparator3");
            airSeparator3.Image = null;
            airSeparator3.Name = "airSeparator3";
            airSeparator3.NoRounding = false;
            airSeparator3.Transparent = false;
            // 
            // airSeparator4
            // 
            airSeparator4.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator4.Customization = "";
            resources.ApplyResources(airSeparator4, "airSeparator4");
            airSeparator4.Image = null;
            airSeparator4.Name = "airSeparator4";
            airSeparator4.NoRounding = false;
            airSeparator4.Transparent = false;
            // 
            // airSeparator5
            // 
            airSeparator5.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator5.Customization = "";
            resources.ApplyResources(airSeparator5, "airSeparator5");
            airSeparator5.Image = null;
            airSeparator5.Name = "airSeparator5";
            airSeparator5.NoRounding = false;
            airSeparator5.Transparent = false;
            // 
            // airSeparator6
            // 
            airSeparator6.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator6.Customization = "";
            resources.ApplyResources(airSeparator6, "airSeparator6");
            airSeparator6.Image = null;
            airSeparator6.Name = "airSeparator6";
            airSeparator6.NoRounding = false;
            airSeparator6.Transparent = false;
            // 
            // airSeparator7
            // 
            airSeparator7.BackColor = Color.FromArgb(238, 238, 238);
            airSeparator7.Customization = "";
            resources.ApplyResources(airSeparator7, "airSeparator7");
            airSeparator7.Image = null;
            airSeparator7.Name = "airSeparator7";
            airSeparator7.NoRounding = false;
            airSeparator7.Transparent = false;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            BackColor = Color.Black;
            Controls.Add(airSeparator7);
            Controls.Add(airSeparator6);
            Controls.Add(airSeparator5);
            Controls.Add(airSeparator4);
            Controls.Add(airSeparator3);
            Controls.Add(airSeparator2);
            Controls.Add(foxLabel3);
            Controls.Add(foxLabel2);
            Controls.Add(foxLabel1);
            Controls.Add(hopePictureBox2);
            Controls.Add(hopePictureBox1);
            Controls.Add(airSeparator1);
            Controls.Add(label6);
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
            ForeColor = Color.FromArgb(229, 229, 229);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            MaximizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            Load += MainForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)hopePictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)hopePictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPullDownStrength).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private ReaLTaiizor.Controls.AirSeparator airSeparator1;
        private ReaLTaiizor.Controls.HopeGroupBox hopeGroupBox1;
        private ReaLTaiizor.Controls.HopeGroupBox hopeGroupBox2;
        private ReaLTaiizor.Controls.HopePictureBox hopePictureBox1;
        private ReaLTaiizor.Controls.HopePictureBox hopePictureBox2;
        private ComboBox comboBoxToggleKey;
        private Button buttonStop;
        private Button buttonStart;
        private NumericUpDown numericUpDownDelay;
        private NumericUpDown numericUpDownStrength;
        private Label label2;
        private Label label3;
        private Label label4;
        private LinkLabel linkLabel1;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label5;
        private NumericUpDown numericUpDownPullDownStrength;
        private CheckBox checkBoxAdsOnly;
        private ReaLTaiizor.Controls.FoxLabel foxLabel1;
        private ReaLTaiizor.Controls.FoxLabel foxLabel2;
        private ReaLTaiizor.Controls.FoxLabel foxLabel3;
        private ReaLTaiizor.Controls.AirSeparator airSeparator2;
        private ReaLTaiizor.Controls.AirSeparator airSeparator3;
        private ReaLTaiizor.Controls.AirSeparator airSeparator4;
        private ReaLTaiizor.Controls.AirSeparator airSeparator5;
        private ReaLTaiizor.Controls.AirSeparator airSeparator6;
        private ReaLTaiizor.Controls.AirSeparator airSeparator7;
    }
}