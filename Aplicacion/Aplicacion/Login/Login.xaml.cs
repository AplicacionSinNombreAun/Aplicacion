using Aplicacion.Context;
using Aplicacion.Context.Model;
using Aplicacion.FacebookLogin;
using Aplicacion.FacebookLogin.FacebookUser;
using Aplicacion.Pantallas;
using Applicacion.FacebookLogin;
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
using static Aplicacion.Context.Model.Usuario;

namespace Aplicacion.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        Conexion con;
        public Login()
        {
            InitializeComponent();
            con = new Conexion();
            NavigationPage.SetHasNavigationBar(this, false);
        }


        private async void IngresarLogin(object sender, EventArgs e)
        {
            UsuarioJson User = await con.ConsultaExistenciaUsuario(UserLogin.Text, PassLogin.Text);

            if (User.Usuario == null)
            {
                // PopUp con error de usuario no existe 
            }
            else
            {
                //el usuario existe asi que se le da acceso 
                Aplicacion.App.Current.MainPage = new PaginaNavegadora();
            }
        }
        private void RegistrarUser(object sender, EventArgs e)
        {
            Aplicacion.App.Current.MainPage.Navigation.PushAsync(new Registro(),true);
        }

        private void FacebookButton(object sender, EventArgs e)
        {
            Aplicacion.App.Current.MainPage = new FacebookOAuthRequest("185999915438012", accesstokenresult: AccessToken);
        }

        private async void AccessToken(FacebookOAuthResult obj)
        {
            GraphApi api = new GraphApi(obj.AccessToken);
            //Declaro los parametros que me traigo desde facebook id , name , birthday , email , picture 
            FacebookUser user = await api.Explorer<FacebookUser>("/me?fields=id,name,birthday,email,picture{url}");

            UsuarioJson User = await con.ConsultaExistenciaUsuario(user.id,user.id);

            if (User.Usuario == null)
            {
                // Si usuario es null entonces me registro 
                JsonResponseInsert Result = await con.RegistraNuevoUsuario(user.id, user.id, user.name, user.email);

                //si el registro retorna null es porque no hay conexion 
                if (Result.Transaccion != null)
                {
                    if (Result.Transaccion.Operacion.Respuesta != "NO")
                    {
                        // si es distinto a NO se registro bien asi que lo envio a la pagina principal 
                        Aplicacion.App.Current.MainPage = new PaginaNavegadora();
                    }
                    else
                    {
                        // si retorna NO es por bd (error o condicionales no cumplidas )
                        // popup error de registro 
                    }
                    
                }
                else
                {
                    //Pupup Error de Registro atravez de facebook 
                }
            }
            else
            {
                //el usuario ya existe en BD y facebook ya valido password asi que le doy acceso
                Aplicacion.App.Current.MainPage = new PaginaNavegadora();
            }
          
            
        }

    }
}