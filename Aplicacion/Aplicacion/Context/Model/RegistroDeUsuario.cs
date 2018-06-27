using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Context.Model
{
    public class RegistroDeUsuario
    {
        public string usuario_id { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }

        public RegistroDeUsuario(string usuario_id, string password, string nombre, string email)
        {
            this.usuario_id = usuario_id;
            this.password = password;
            this.nombre = nombre;
            this.email = email;
        }
    }
}
