using Aplicacion.Context;
using Aplicacion.Context.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Aplicacion.Context.Conexion;

namespace Aplicacion.Pantallas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{
        Conexion con;
        public Registro ()
		{
			InitializeComponent ();
            con = new Conexion();
        }

        private async void ClickRegistrarButton(object sender, EventArgs e)
        {

            JsonResponseInsert Result = await con.RegistraNuevoUsuario(User.Text, Pass.Text, Nombre.Text, Email.Text);


            if(Result.Transaccion != null)
            {
                if (Result.Transaccion.Operacion.Respuesta == "OK")
                {
                    //Se termina el registro del nuevo usuario y se le da acceso a la pagina principal 
                    Aplicacion.App.Current.MainPage = new PaginaNavegadora();
                }
                else
                {
                    //Errores de bd (usuario ya existe , email ya existe etc .. )
                }
            }
            
        }

    }
}