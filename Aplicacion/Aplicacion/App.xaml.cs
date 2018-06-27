using Aplicacion.FacebookLogin;
using Applicacion.FacebookLogin;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Aplicacion.Login;
[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Aplicacion
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage( new Login.Login() );
           //MainPage = new FacebookOAuthRequest("185999915438012", accesstokenresult: AccessToken);

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
