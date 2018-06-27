using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Context.Model
{
    public class Usuario
    {
            public int id { get; set; }
            public string Usuario_IdFace { get; set; }
            public string Password { get; set; }
            public string Nombre { get; set; }
            public string Email { get; set; }
            public bool Estado { get; set; }
            public int id_nivelUsuario { get; set; }
            public string TipoNivel { get; set; }
    

        public class UsuarioJson
        {
            public Usuario Usuario { get; set; }
        }

    }
}
