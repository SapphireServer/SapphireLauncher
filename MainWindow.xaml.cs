using System;
using System.Diagnostics;
using System.Windows;

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
            InitializeComponent( );

            webBrowser.AllowDrop = false;
            webBrowser.RequestHandler = new CefRequestHandler();

            WebScriptApi api = new WebScriptApi( this );
            webBrowser.RegisterJsObject( "external", api, new CefSharp.BindingOptions
            {
                CamelCaseJavascriptNames = false
            } );


            webBrowser.Address = Properties.Settings.Default.WebServerUrl;

            webBrowser.FrameLoadEnd += WebBrowser_FrameLoadEnd;
        }

        private void WebBrowser_FrameLoadEnd( object sender, CefSharp.FrameLoadEndEventArgs e )
        {
            if ( e.HttpStatusCode != 200 )
            {

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
