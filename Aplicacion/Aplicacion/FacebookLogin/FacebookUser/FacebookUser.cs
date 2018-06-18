using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.FacebookLogin.FacebookUser
{
    public class FacebookUser
    {
        public string id { get; set; }
        public string name { get; set; }
        
        public Picture picture { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
    }
}
