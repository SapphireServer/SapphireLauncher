using CefSharp;
using System;
using System.Security.Cryptography.X509Certificates;

namespace SapphireBootWPF
{
    public class CefRequestHandler : IRequestHandler
    {
        public bool OnBeforeBrowse( IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect )
        {
            var destUrl = new Uri( request.Url );

            Uri frontierUrl = null;

            try
            {
                frontierUrl = new Uri( Properties.Settings.Default.WebServerUrl );
            }
            // don't really care if it's invalid format/argumentnull exceptions
            catch( Exception ex )
            {
                System.Diagnostics.Trace.TraceError( ex.Message );
                return false;
            }

            // if hosts don't match, open url in users browser
            if ( destUrl.Host != frontierUrl.Host )
            {
                System.Diagnostics.Process.Start( request.Url );
                return true;
            }

            // host match, load url in embedded browser
            return false;
        }

        public bool GetAuthCredentials( IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback )
        {
            return GetAuthCredentials( browserControl, browser, frame, isProxy, host, port, realm, scheme, callback );
        }

        public IResponseFilter GetResourceResponseFilter( IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response )
        {
            return null;
        }

        public CefReturnValue OnBeforeResourceLoad( IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback )
        {
            return CefReturnValue.Continue;
        }

        public bool OnCertificateError( IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback )
        {
            return OnCertificateError( browserControl, browser, errorCode, requestUrl, sslInfo, callback );
        }

        public bool OnOpenUrlFromTab( IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture )
        {
            return OnOpenUrlFromTab( browserControl, browser, frame, targetUrl, targetDisposition, userGesture );
        }

        public void OnPluginCrashed( IWebBrowser browserControl, IBrowser browser, string pluginPath )
        {

        }

        public bool OnProtocolExecution( IWebBrowser browserControl, IBrowser browser, string url )
        {
            return false;
        }

        public bool OnQuotaRequest( IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback )
        {
            return OnQuotaRequest( browserControl, browser, originUrl, newSize, callback );
        }

        public void OnRenderProcessTerminated( IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status )
        {
        }

        public void OnRenderViewReady( IWebBrowser browserControl, IBrowser browser )
        {
        }

        public void OnResourceLoadComplete( IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength )
        {
        }

        public void OnResourceRedirect( IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl )
        {
        }

        public bool OnResourceResponse( IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response )
        {
            return false;
        }

        public bool OnSelectClientCertificate( IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback )
        {
            return OnSelectClientCertificate( browserControl, browser, isProxy, host, port, certificates, callback );
        }
    }
}
