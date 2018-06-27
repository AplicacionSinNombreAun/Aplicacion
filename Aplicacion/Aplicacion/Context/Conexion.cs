using Aplicacion.Context.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Aplicacion.Context.Model.Usuario;

namespace Aplicacion.Context
{
    public class Conexion
    {
        HttpClient client;
        public Conexion()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<UsuarioJson> ConsultaExistenciaUsuario(string Usuario, string Password)
        {
            UsuarioJson Items;
            var uri = new Uri(string.Format("http://esb.wisetrack.cl/BuscoProducto/Usuario?usuario_id=" + Usuario + "&password=" + Password, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                 Items = JsonConvert.DeserializeObject<UsuarioJson>(content);
            }
            else
            {
                 Items = new UsuarioJson();
            }

            return Items;

        }


        public async Task<JsonResponseInsert> RegistraNuevoUsuario(string Usuario,string Password,string Nombre,string Email)
        {
            var Url = new Uri(string.Format("http://esb.wisetrack.cl/BuscoProducto/Usuario", string.Empty));
            RegistroDeUsuario Usu = new RegistroDeUsuario(Usuario, Password, Nombre, Email);
            var json = JsonConvert.SerializeObject(Usu);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Url, content);

            JsonResponseInsert Item;
            if (response.IsSuccessStatusCode)
            {
                var ContentResponse = await response.Content.ReadAsStringAsync();
                 Item = JsonConvert.DeserializeObject<JsonResponseInsert>(ContentResponse);
            }
            else
            {
                 Item = new JsonResponseInsert();
            }
            return Item;

        }

        public class Operacion
        {
            public string Respuesta { get; set; }
        }

        public class Transaccion
        {
            public Operacion Operacion { get; set; }
        }

        public class JsonResponseInsert
        {
            public Transaccion Transaccion { get; set; }
        }

    }
}
