using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.FacebookLogin
{
    public class FacebookOAuthResult
    {
        public string AccessToken { get; set; }
        public FacebookOAuthRequest FacebookPage { get; set; }
    }
}
