using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.FacebookLogin
{
    public interface IRestForms
    {
        Task<T> Post<T>(string url, Dictionary<string, object> simpleparams = null, List<Param> complexparams = null);

        Task<T> Post<T, K>(string url, K objecttosend);

        Task<T> Get<T>(string url, Dictionary<string, object> formdata = null);

        Task<T> Delete<T>(string url, int id);
    }
}
