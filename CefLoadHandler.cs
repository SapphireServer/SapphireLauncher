using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace SapphireBootWPF
{
    public class CefLoadHandler : ILoadHandler
    {
        public void OnLoadError( IWebBrowser browserControl, LoadErrorEventArgs loadErrorArgs )
        {
            if ( loadErrorArgs.ErrorCode == CefErrorCode.Aborted )
            {
                return;
            }

            var errorDetail = string.Format( "{0} for {1}", loadErrorArgs.ErrorText, loadErrorArgs.FailedUrl );

            var errorContent = Properties.Resources.server.Replace( "{SAPPHIRE_BROWSER_ERROR_INFO}", errorDetail );

            loadErrorArgs.Frame.LoadStringForUrl( errorContent, loadErrorArgs.FailedUrl );
        }

        public void OnFrameLoadEnd( IWebBrowser browserControl, FrameLoadEndEventArgs frameLoadEndArgs )
        {

        }

        public void OnFrameLoadStart( IWebBrowser browserControl, FrameLoadStartEventArgs frameLoadStartArgs )
        {

        }

        public void OnLoadingStateChange( IWebBrowser browserControl, LoadingStateChangedEventArgs loadingStateChangedArgs )
        {

        }
    }
}
