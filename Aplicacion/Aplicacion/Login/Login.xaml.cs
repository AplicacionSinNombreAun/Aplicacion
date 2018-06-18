using Aplicacion.FacebookLogin;
using Aplicacion.FacebookLogin.FacebookUser;
using Applicacion.FacebookLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
           
		}

        private void FacebookButton(object sender, EventArgs e)
        {
            Aplicacion.App.Current.MainPage = new FacebookOAuthRequest("185999915438012", accesstokenresult: AccessToken);
        }

        private async void AccessToken(FacebookOAuthResult obj)
        {
            GraphApi api = new GraphApi(obj.AccessToken);

            FacebookUser user = await api.Explorer<FacebookUser>("/me?fields=id,name,birthday,email,picture{url}");

            Aplicacion.App.Current.MainPage = new MainPage();

        }
    }
}