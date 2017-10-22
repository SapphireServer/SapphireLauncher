using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SapphireBootWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [Conditional( "DEBUG" )]
        private void EnableCefDebugging( CefSharp.CefSettings settings )
        {
            settings.RemoteDebuggingPort = 8080;
        }

        public App()
        {
            var settings = new CefSharp.CefSettings();

            EnableCefDebugging( settings );
            
            // fix horrid offscreen rendering performance
            settings.SetOffScreenRenderingBestPerformanceArgs();
            //settings.CefCommandLineArgs.Add( "disable-gpu-compositing", "1" );
            //settings.CefCommandLineArgs.Add( "enable-begin-frame-scheduling", "1" );

            CefSharp.Cef.Initialize( settings );
        }
    }
}
