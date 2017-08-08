using mshtml;
using SapphireBootWPF.Properties;
using System;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapphireBootWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 	
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainWebBrowser.AllowDrop = false;
            WebScriptApi api = new WebScriptApi(this);

            mainWebBrowser.ObjectForScripting = api;

            try
            {
                mainWebBrowser.Navigate(new System.Uri(Properties.Settings.Default.WebServerUrl, UriKind.Absolute));
            }
            catch (System.UriFormatException exc)
            {
                mainWebBrowser.Navigate("https://nothappening");
            }
            
            mainWebBrowser.LoadCompleted += WebEvent_LoadCompleted;
        }

        void WebEvent_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var document = (HTMLDocument)mainWebBrowser.Document;
            string html = document.body.outerHTML;
            if (html.Contains("ieframe.dll") || html.Contains("NewErrorPageTemplate.css") || html.Contains("<UL id=notConnectedTasks class=tasks "))
            {
                mainWebBrowser.NavigateToString(Properties.Resources.server);
                return;
            }
        }

        class Win32WindowHelper : System.Windows.Forms.IWin32Window
        {
            private System.Windows.Interop.WindowInteropHelper interopHelper;
            public Win32WindowHelper(Window w)
            {
                interopHelper = new System.Windows.Interop.WindowInteropHelper(w);
            }

            IntPtr System.Windows.Forms.IWin32Window.Handle
            {
                get { return interopHelper.Handle; }
            }
        }
    }
}
