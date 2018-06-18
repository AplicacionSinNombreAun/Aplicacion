using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Applicacion.FacebookLogin
{
    public class GraphApi
    {

        public string AccessToken
        {
            get; set;
        }

        public GraphApi(string accesstoken)
        {
            AccessToken = accesstoken;
        }

        public async Task<T> Explorer<T>(string path)
        {
            try
            {
                string requestUrl = $"https://graph.facebook.com/v3.0{path}&access_token={AccessToken}";
                HttpClient httpclient = new HttpClient();
                //var httpClient = Aplicacion.FacebookLogin.RestForms.Instance;//Applicacion.FacebookLogin.RestForms.Instance;//DevAzt.FormsX.Net.HttpClient.RestForms.Instance;
                //Debug.WriteLine(requestUrl);
                string json = await httpclient.GetStringAsync(requestUrl);
                //var response = await httpClient.Get<T>(requestUrl, new Dictionary<string, object>());
                return JsonConvert.DeserializeObject <T> (json); //response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return default(T);
        }

    }
}