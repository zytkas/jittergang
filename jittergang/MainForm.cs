using JitterGang;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;


namespace jittergang
{
    public partial class MainForm : Form
    {
        private JitterLogic jitterLogic;
        private JitterTimer jitterTimer;
        private bool isProcessListInitialized = false;
        private System.ComponentModel.IContainer components;
        private string settingsFilePath;

        // Form elements
        private ComboBox comboBoxProcesses;
        private NumericUpDown numericUpDownStrength;
        private NumericUpDown numericUpDownDelay;
        private NumericUpDown numericUpDownPullDownStrength;
        private Button buttonStart;
        private Button buttonStop;
        private ComboBox comboBoxToggleKey;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LinkLabel linkLabel1;
        private ToolTip toolTip1;


        public MainForm()
        {
            InitializeComponent();
            InitializeSettingsPath();
            jitterLogic = new JitterLogic();
            jitterTimer = new JitterTimer(jitterLogic);
        }

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
            buttonStart.Location = new Point(63, 379);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(74, 26);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Start";
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.FlatAppearance.BorderSize = 0;
            buttonStop.Location = new Point(187, 379);
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
            linkLabel1.Location = new Point(112, 435);
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
            checkBox2.Location = new Point(114, 308);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(96, 24);
            checkBox2.TabIndex = 16;
            checkBox2.Text = "СircleJitter";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(112, 338);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(100, 24);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "Contorller?";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
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
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            BackColor = Color.FromArgb(33, 33, 33);
            ClientSize = new Size(318, 464);
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
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDownStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPullDownStrength).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            LoadSettings();
            UpdateProcessList();
            isProcessListInitialized = true;
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateParameters();

                if (!jitterTimer.IsRunning)
                {
                    TimeSpan interval = TimeSpan.FromMilliseconds((int)numericUpDownDelay.Value);
                    jitterLogic.UpdateStrength((int)numericUpDownStrength.Value);
                    jitterLogic.UpdatePullDownStrength((int)numericUpDownPullDownStrength.Value);
                    jitterLogic.UpdateJitters();
                    jitterTimer.Start(interval);
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (jitterTimer.IsRunning)
            {
                jitterTimer.Stop();
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
                jitterLogic.StopJitter();
            }
        }



        private void numericUpDownStrength_ValueChanged(object sender, EventArgs e)
        {
            jitterLogic.UpdateStrength((int)numericUpDownStrength.Value);
            jitterLogic.UpdateJitters();
            SaveSettings();
        }

        private void numericUpDownPullDownStrength_ValueChanged(object sender, EventArgs e)
        {
            jitterLogic.UpdatePullDownStrength((int)numericUpDownPullDownStrength.Value);
            jitterLogic.UpdateJitters();
            SaveSettings();
        }



        private void comboBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProcesses.SelectedItem != null)
            {
                string selectedProcessName = comboBoxProcesses.SelectedItem.ToString();
                jitterLogic.SelectedProcess = Process.GetProcessesByName(selectedProcessName).FirstOrDefault();
                SaveSettings();
            }
        }

        private void ComboBoxProcesses_DropDown(object sender, EventArgs e)
        {
            if (isProcessListInitialized)
            {
                UpdateProcessList();
            }
        }

        private void UpdateProcessList()
        {
            var currentSelection = comboBoxProcesses.SelectedItem?.ToString();

            var processes = Process.GetProcesses()
                .Select(p => p.ProcessName)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            comboBoxProcesses.BeginUpdate();
            comboBoxProcesses.Items.Clear();
            comboBoxProcesses.Items.AddRange(processes.Cast<object>().ToArray());
            comboBoxProcesses.EndUpdate();

            if (!string.IsNullOrEmpty(currentSelection))
            {
                int index = comboBoxProcesses.FindStringExact(currentSelection);
                if (index != -1)
                {
                    comboBoxProcesses.SelectedIndex = index;
                }
                else if (comboBoxProcesses.Items.Count > 0)
                {
                    comboBoxProcesses.SelectedIndex = 0;
                }
            }
            else if (comboBoxProcesses.Items.Count > 0)
            {
                comboBoxProcesses.SelectedIndex = 0;
            }
        }


        private void comboBoxToggleKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxToggleKey.SelectedItem != null)
            {
                string selectedKey = comboBoxToggleKey.SelectedItem.ToString();
                SetToggleKey(selectedKey);
                SaveSettings();
            }
        }

        private void SetToggleKey(string keyName)
        {
            switch (keyName)
            {
                case "F1": jitterLogic.ToggleKey = 0x70; break;
                case "F2": jitterLogic.ToggleKey = 0x71; break;
                case "F3": jitterLogic.ToggleKey = 0x72; break;
                case "F4": jitterLogic.ToggleKey = 0x73; break;
                case "F5": jitterLogic.ToggleKey = 0x74; break;
                case "F6": jitterLogic.ToggleKey = 0x75; break;
                case "F7": jitterLogic.ToggleKey = 0x76; break;
                case "F8": jitterLogic.ToggleKey = 0x77; break;
                case "F9": jitterLogic.ToggleKey = 0x78; break;
                case "F10": jitterLogic.ToggleKey = 0x79; break;
                case "F11": jitterLogic.ToggleKey = 0x7A; break;
                case "F12": jitterLogic.ToggleKey = 0x7B; break;
                case "Shift": jitterLogic.ToggleKey = 0x10; break;
                case "X1": jitterLogic.ToggleKey = 0x05; break;
                case "X2": jitterLogic.ToggleKey = 0x06; break;
                default: jitterLogic.ToggleKey = 0x70; break;
            }
        }



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            jitterLogic.IsCircleJitterActive = checkBox2.Checked;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                jitterLogic.SetUseController(checkBox1.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox1.Checked = false;
            }
        }


        private void ValidateParameters()
        {
            if (numericUpDownStrength.Value < 1)
            {
                numericUpDownStrength.Value = 1;
            }
            if (numericUpDownDelay.Value < 1)
            {
                numericUpDownDelay.Value = 1;
            }
            if (comboBoxProcesses.SelectedItem == null && comboBoxProcesses.Items.Count > 0)
            {
                comboBoxProcesses.SelectedIndex = 0;
            }
            if (numericUpDownStrength.Value == 0 && numericUpDownPullDownStrength.Value == 0)
            {
                numericUpDownStrength.Value = 1;
            }
            if (comboBoxProcesses.SelectedItem != null)
            {
                string selectedProcessName = comboBoxProcesses.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(selectedProcessName))
                {
                    throw new ArgumentException("Cannot handle this process.");
                }
                // Убираем проверку на существование процесса, так как он может быть placeholder
            }
        }

        private void InitializeComboBoxes()
        {
            comboBoxToggleKey.Items.Clear();
            comboBoxToggleKey.Items.AddRange(new object[]
            {
        "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
        "X1", "X2", "Shift"
            });
        }

        private void InitializeSettingsPath()
        {
            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(baseFolder, "JitterGang");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            settingsFilePath = Path.Combine(appFolder, "settings.json");
            EnsureFileAccess(settingsFilePath);

            Debug.WriteLine($"Путь к файлу настроек: {settingsFilePath}");
        }

        private void EnsureFileAccess(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            SetWindowsFilePermissions(filePath);
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(string StringSecurityDescriptor, uint StringSDRevision, ref byte[] SecurityDescriptor, out uint SecurityDescriptorSize);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool SetFileSecurity(string lpFileName, System.Security.AccessControl.SecurityInfos SecurityInformation, byte[] pSecurityDescriptor, uint nLength);

        private void SetWindowsFilePermissions(string filePath)
        {
            try
            {
                string sddl = "D:PAI(A;OICI;FA;;;WD)"; // Предоставляет полный доступ для всех
                byte[] sd = new byte[0];
                uint size = 0;

                if (!ConvertStringSecurityDescriptorToSecurityDescriptor(sddl, 1, ref sd, out size))
                {
                    throw new System.ComponentModel.Win32Exception();
                }

                if (!SetFileSecurity(filePath, System.Security.AccessControl.SecurityInfos.DiscretionaryAcl, sd, size))
                {
                    throw new System.ComponentModel.Win32Exception();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Не удалось установить разрешения Windows для файла: {ex.Message}");
            }
        }



        private void SaveSettings()
        {
            try
            {
                var settings = new
                {
                    Strength = (int)numericUpDownStrength.Value,
                    Delay = (int)numericUpDownDelay.Value,
                    PullDownStrength = (int)numericUpDownPullDownStrength.Value,
                    SelectedProcess = comboBoxProcesses.SelectedItem?.ToString() ?? "",
                    ToggleKeyName = comboBoxToggleKey.SelectedItem?.ToString() ?? ""
                };

                string json = System.Text.Json.JsonSerializer.Serialize(settings);
                File.WriteAllText(settingsFilePath, json);

                Debug.WriteLine($"Настройки сохранены в файл: {settingsFilePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при сохранении настроек: {ex.Message}");
                MessageBox.Show($"Ошибка при сохранении настроек: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    string json = File.ReadAllText(settingsFilePath);
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        SetDefaultValues();
                        return;
                    }

                    var settings = System.Text.Json.JsonSerializer.Deserialize<dynamic>(json);

                    numericUpDownStrength.Value = Math.Max(1, settings.GetProperty("Strength").GetInt32());
                    numericUpDownDelay.Value = Math.Max(1, settings.GetProperty("Delay").GetInt32());
                    numericUpDownPullDownStrength.Value = Math.Max(0, settings.GetProperty("PullDownStrength").GetInt32());

                    string savedProcess = settings.GetProperty("SelectedProcess").GetString();
                    string savedToggleKey = settings.GetProperty("ToggleKeyName").GetString();

                    Debug.WriteLine($"Загрузка настроек. Сохраненный процесс: '{savedProcess}', Кнопка: '{savedToggleKey}'");

                    UpdateProcessList();

                    // Выбор сохраненного процесса
                    int processIndex = comboBoxProcesses.FindStringExact(savedProcess);
                    comboBoxProcesses.SelectedIndex = (processIndex != -1) ? processIndex : 0;

                    // Выбор сохраненной кнопки
                    int toggleKeyIndex = comboBoxToggleKey.FindStringExact(savedToggleKey);
                    comboBoxToggleKey.SelectedIndex = (toggleKeyIndex != -1) ? toggleKeyIndex : 0;

                    Debug.WriteLine($"Настройки загружены. Выбранный процесс: '{comboBoxProcesses.SelectedItem}', Кнопка: '{comboBoxToggleKey.SelectedItem}'");
                }
                else
                {
                    Debug.WriteLine("Файл настроек не найден. Используются значения по умолчанию.");
                    SetDefaultValues();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке настроек: {ex.Message}");
                SetDefaultValues();
            }
        }

        private void SetDefaultValues()
        {
            numericUpDownStrength.Value = 1;
            numericUpDownDelay.Value = 1;
            numericUpDownPullDownStrength.Value = 0;
            if (comboBoxProcesses.Items.Count > 0) comboBoxProcesses.SelectedIndex = 0;
            if (comboBoxToggleKey.Items.Count > 0) comboBoxToggleKey.SelectedIndex = 0;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            jitterTimer.Stop();
            jitterLogic.SetUseController(false);
            SaveSettings();
            base.OnFormClosing(e);
        }

    }
}