using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using CefSharp.Wpf.Internals;

namespace SapphireBootWPF
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class WebScriptApi
    {
        private MainWindow window;

        public WebScriptApi(MainWindow window)
        {
            this.window = window;
        }

        public void Boot(string sid, string serverLobbyAddress, string serverFrontierAddress)
        {
            try
            {
                BootClient.StartClient(sid, serverLobbyAddress, serverFrontierAddress);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"An error has occured. Please check your game path and server addresses.\n\nMore info:\n{ex}",
                                        "Launch failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void OpenClientPathChooser()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXE files (*.exe)|*.exe|All files (*.*)|*.*";

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
                Properties.Settings.Default.ClientPath = openFileDialog.FileName;

            Properties.Settings.Default.Save();
            //window.webBrowser.GetBrowser( ).MainFrame.ExecuteJavaScriptAsync( string.Format( "updateClientPath({0})", Properties.Settings.Default.ClientPath ) );
        }

        public void SaveWebServerUrl(string url)
        {
            Properties.Settings.Default.WebServerUrl = url;
            Properties.Settings.Default.Save();
        }

        public void Navigate(string url)
        {
            window.webBrowser.Address = url;
		}

        public void SwitchWindows()
        {
            ServerWindow nWindow = new ServerWindow();
            nWindow.Left = window.Left;
            nWindow.Top = window.Top;
            nWindow.Show();
            window.Close();
        }
    }
}
