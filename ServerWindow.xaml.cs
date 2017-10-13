using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SapphireBootWPF
{
    /// <summary>
    /// Interaction logic for ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {
        public ServerWindow()
        {
            InitializeComponent();

            if(Properties.Settings.Default.WebServerUrl != "https://default")
                lobbyTextBox.Text = Properties.Settings.Default.WebServerUrl;

            gamePathTextBox.Text = Properties.Settings.Default.ClientPath;
            launchParamsTextBox.Text = Properties.Settings.Default.LaunchParams;
            expansionLevelComboBox.SelectedIndex = Properties.Settings.Default.ExpansionLevel;
            languageComboBox.SelectedIndex = Properties.Settings.Default.SavedLanguage;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Left = this.Left;
            mainwindow.Top = this.Top;
            mainwindow.Show();
            this.Close();
        }

        private void selectPathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXE files (*.exe)|*.exe|All files (*.*)|*.*";

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
                gamePathTextBox.Text = openFileDialog.FileName;
        }

        private void checkDataButton_Click(object sender, RoutedEventArgs e)
        {
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var uiTaskFactory = new TaskFactory(uiScheduler);

            Parallel.Invoke(() =>
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    try
                    {
                        client.DownloadString(lobbyTextBox.Text);

                        errorLabel.Foreground = Brushes.Green;
                        errorLabel.Content = "Connection successful.";
                    }
                    catch(Exception)
                    {
                        errorLabel.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xDC, 0, 0));
                        errorLabel.Content = "Connection failed, the server may be unavailable.";
                    }
                }
            });
        }

        private void saveDataButton_Click(object sender, RoutedEventArgs e)
        {
			// todo: fix the race condition that occurs here because properties.settings isnt updated when cef uses webserverurl

			//var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
			//var uiTaskFactory = new TaskFactory(uiScheduler);

			//uiTaskFactory.StartNew(() =>
			//{
			Properties.Settings.Default.WebServerUrl = lobbyTextBox.Text;
            Properties.Settings.Default.ClientPath = gamePathTextBox.Text;
            Properties.Settings.Default.LaunchParams = launchParamsTextBox.Text;
            Properties.Settings.Default.ExpansionLevel = expansionLevelComboBox.SelectedIndex;
            Properties.Settings.Default.SavedLanguage = languageComboBox.SelectedIndex;

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            //});

            MainWindow mainwindow = new MainWindow();
            mainwindow.Left = this.Left;
            mainwindow.Top = this.Top;
            mainwindow.Show();
            this.Close();
        }
    }
}