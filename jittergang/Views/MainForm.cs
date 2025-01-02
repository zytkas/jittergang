using JitterGang.Models;
using JitterGang.Services;
using JitterGang.ViewModels;
using System.Diagnostics;

namespace jittergang
{
    public partial class MainForm : Form
    {
        private readonly MainViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();
            _viewModel = CreateViewModel();
            InitializeFormState();
            InitializeViewModelAsync();
        }

        private static MainViewModel CreateViewModel()
        {
            var settingsService = new SettingsService();
            var jitterService = new JitterService();
            return new MainViewModel(settingsService, jitterService);
        }
        private async void InitializeViewModelAsync()
        {
            try
            {
                await _viewModel.InitializeAsync();

                // Эти методы вызываем только после успешной инициализации
                SubscribeToEvents();
                SetupBindings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to initialize settings: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeFormState()
        {
            // Настройка комбобоксов
            InitializeComboBoxes();
            // Настройка числовых полей
            SetupNumericUpDowns();
        }

        private void InitializeComboBoxes()
        {
            comboBoxToggleKey.Items.Clear();
            comboBoxToggleKey.Items.AddRange(new object[]
            {
                "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
                "X1", "X2", "Shift", "Capslock"
            });


            if (!string.IsNullOrEmpty(_viewModel.Settings.ToggleKey) && comboBoxToggleKey.Items.Contains(_viewModel.Settings.ToggleKey))
            {
                comboBoxToggleKey.SelectedItem = _viewModel.Settings.ToggleKey;
            }
            else if (comboBoxToggleKey.Items.Count > 0)
            {
                comboBoxToggleKey.SelectedIndex = 0;
            }
        }

        private void SetupNumericUpDowns()
        {
            numericUpDownStrength.Minimum = 1;
            numericUpDownDelay.Minimum = 1;
            numericUpDownPullDownStrength.Maximum = 50;
        }

        private void SubscribeToEvents()
        {
            // Подписка на изменения в ViewModel
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;

            // События формы
            Load += MainForm_Load;
            FormClosing += MainForm_FormClosing;

            // Только события, не связанные с привязкой данных
            buttonStart.Click += buttonStart_Click;
            buttonStop.Click += buttonStop_Click;
            comboBoxProcesses.DropDown += ComboBoxProcesses_DropDown;
        }
        private void SetupBindings()
        {
            if (_viewModel?.Settings == null) return;

            // Привязка числовых полей
            numericUpDownStrength.DataBindings.Add(new Binding("Value", _viewModel.Settings, "Strength",
                true, DataSourceUpdateMode.OnPropertyChanged));

            numericUpDownPullDownStrength.DataBindings.Add(new Binding("Value", _viewModel.Settings, "PullDownStrength",
                true, DataSourceUpdateMode.OnPropertyChanged));

            numericUpDownDelay.DataBindings.Add(new Binding("Value", _viewModel.Settings, "Delay",
                true, DataSourceUpdateMode.OnPropertyChanged));

            // Привязка чекбоксов
            checkBox2.DataBindings.Add(new Binding("Checked", _viewModel.Settings, "IsCircleJitterActive",
                true, DataSourceUpdateMode.OnPropertyChanged));

            checkBoxAdsOnly.DataBindings.Add(new Binding("Checked", _viewModel.Settings, "UseAdsOnly",
                true, DataSourceUpdateMode.OnPropertyChanged));

            comboBoxToggleKey.DataBindings.Add(new Binding("SelectedItem", _viewModel.Settings, "ToggleKey",
                true, DataSourceUpdateMode.OnPropertyChanged));

            checkBox1.DataBindings.Add(new Binding("Checked", _viewModel.Settings, "UseController",
                true, DataSourceUpdateMode.OnPropertyChanged));

            // Подписываемся на изменение настроек для автоматического сохранения
            _viewModel.Settings.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(JitterSettings.UseController))
                {
                    await _viewModel.SaveSettingsAsync();
                }
            };
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.Processes))
            {
                UpdateProcessComboBox();
            }
        }

        #region Event Handlers

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing application: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateProcessComboBox()
        {
            if (_viewModel.Processes == null) return;

            comboBoxProcesses.BeginUpdate();
            comboBoxProcesses.Items.Clear();
            comboBoxProcesses.Items.AddRange(_viewModel.Processes.ToArray());

            if (!string.IsNullOrEmpty(_viewModel.Settings.SelectedProcess) &&
                comboBoxProcesses.Items.Contains(_viewModel.Settings.SelectedProcess))
            {
                comboBoxProcesses.SelectedItem = _viewModel.Settings.SelectedProcess;
            }
            else if (comboBoxProcesses.Items.Count > 0)
            {
                comboBoxProcesses.SelectedIndex = 0;
            }

            comboBoxProcesses.EndUpdate();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                await _viewModel.StartCommand.ExecuteAsync(null);
                Debug.WriteLine("Jitter started");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
            }
        }

        private async void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                buttonStop.Enabled = false;
                buttonStart.Enabled = true;
                await _viewModel.StopCommand.ExecuteAsync(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonStop.Enabled = true;
                buttonStart.Enabled = false;
            }
        }

        private void ComboBoxProcesses_DropDown(object sender, EventArgs e)
        {
            _viewModel.RefreshProcessListCommand.ExecuteAsync(null);
        }


        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                await _viewModel.UpdateControllerState(checkBox1.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Controller Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkBox1.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.Settings.IsCircleJitterActive = checkBox2.Checked;
        }

        private void checkBoxAdsOnly_CheckedChanged(object sender, EventArgs e)
        {
            _viewModel.Settings.UseAdsOnly = checkBoxAdsOnly.Checked;
        }

        private void numericUpDownStrength_ValueChanged(object sender, EventArgs e)
        {
            _viewModel.Settings.Strength = (int)numericUpDownStrength.Value;
        }

        private void numericUpDownPullDownStrength_ValueChanged(object sender, EventArgs e)
        {
            _viewModel.Settings.PullDownStrength = (int)numericUpDownPullDownStrength.Value;
        }

        private void comboBoxToggleKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxToggleKey.SelectedItem != null && _viewModel?.Settings != null)
            {
                _viewModel.Settings.ToggleKey = comboBoxToggleKey.SelectedItem.ToString();
            }
        }

        private void comboBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProcesses.SelectedItem is string selectedProcess)
            {
                _viewModel.Settings.SelectedProcess = selectedProcess;
                _viewModel.SaveSettingsAsync().ConfigureAwait(false);
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _viewModel.Cleanup();
        }

        #endregion

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}