using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Pantallas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Busqueda : ContentPage
	{
		public Busqueda ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
	}
}