using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SapphireBootWPF;

namespace SapphireBootWPF.Control
{
    /// <summary>
    /// Interaction logic for CaptionButtons.xaml
    /// </summary>
    public partial class CaptionButtons : UserControl
    {
        /// <summary>
        /// The parent Window of this control.
        /// </summary>
        private Window window;

        public CaptionButtons()
        {
            InitializeComponent();
            Loaded += CaptionButtons_Loaded;
        }

        void CaptionButtons_Loaded(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            window.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ServerWindow nWindow = new ServerWindow();
            nWindow.Left = window.Left;
            nWindow.Top = window.Top;
            nWindow.Show();
            window.Close();
        }
    }
}
