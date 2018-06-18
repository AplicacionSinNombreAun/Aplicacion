using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Aplicacion.FacebookLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookOAuthRequest : ContentPage
    {

        public string ClientId { get; set; }

        public string RedirectUrl { get; set; }

        public FacebookOAuthRequest(string clientid, string redirecturl = "https://www.facebook.com/connect/login_success.html", Action<FacebookOAuthResult> accesstokenresult = null)
        {
            InitializeComponent();
            ClientId = clientid;
            RedirectUrl = redirecturl;
            AccessTokenResult += (s, r) =>
            {
                accesstokenresult(r);
            };
            RequestAccessToken();
        }

        public event EventHandler<FacebookOAuthResult> AccessTokenResult;

        private void OnAccessTokenResult(FacebookOAuthResult oauthresult)
        {
            if (AccessTokenResult != null)
            {
                AccessTokenResult.Invoke(this, oauthresult);
            }
        }

        public void RequestAccessToken()
        {
            var apiurl = $"https://www.facebook.com/dialog/oauth?client_id={ClientId}&display=popup&response_type=token&redirect_uri={RedirectUrl}";
            WebClient.Navigated += WebClient_Navigated;
            WebClient.Source = apiurl;
        }

        private void WebClient_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                ProgressIndicatorLayout.IsVisible = false;
                WebClient.IsVisible = true;
            });
            var accesstoken = ExtractAccessTokenFromUrl(e.Url);
            if (!string.IsNullOrEmpty(accesstoken))
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressIndicatorLayout.IsVisible = true;
                    WebClient.IsVisible = false;
                });
                OnAccessTokenResult(new FacebookOAuthResult
                {
                    AccessToken = accesstoken,
                    FacebookPage = this
                });
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace($"{RedirectUrl}#access_token=", "");

                if (Xamarin.Forms.Device.OS == TargetPlatform.WinPhone || Xamarin.Forms.Device.OS == TargetPlatform.Windows)
                {
                    RedirectUrl = RedirectUrl.Replace("http", "https");
                    at = url.Replace($"{RedirectUrl}#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                return accessToken;
            }
            return string.Empty;
        }
    }
}