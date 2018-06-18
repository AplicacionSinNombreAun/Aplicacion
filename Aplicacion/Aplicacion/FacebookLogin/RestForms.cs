using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.FacebookLogin
{
    public enum RequestType
    {
        Get, Post
    }

    public class SendRequest
    {
        public RequestType RequestType { get; set; }
        public string Url { get; set; }
        public bool Success { get; set; }
        public string Parameters { get; set; }
    }

    public class RestForms
    {
        private IRestForms _service;

        public RestForms(IRestForms service)
        {
            _service = service;
        }

        public static RestForms Instance
        {
            get
            {
                var phonemanager = DependencyService.Get<IRestForms>();
                if (phonemanager == null) throw new NullReferenceException("La dependencia de servicio es null");
                return new RestForms(phonemanager);
            }
        }

        public async Task<T> Get<T>(string url, Dictionary<string, object> dictionary)
        {
            if (dictionary != null)
            {
                dictionary.Add("cache", Guid.NewGuid().ToString());
            }
            var response = await _service.Get<T>(url, dictionary);
            OnSendRequest<T>(RequestType.Post, url, response != null, dictionary);
            return response;
        }

        public async Task<T> Post<T, K>(string url, K objecttosend)
        {
            var cache = Guid.NewGuid().ToString();
            url += "?cache=" + cache;
            var result = await _service.Post<T, K>(url, objecttosend);
            OnSendRequest<K>(RequestType.Post, url, result != null, sendt: objecttosend);
            return result;
        }

        public event EventHandler<SendRequest> SendRequest;

        private void OnSendRequest<T>(RequestType type, string url, bool success, Dictionary<string, object> dictionary = null, List<Param> complex = null, T sendt = default(T))
        {
            if (SendRequest != null)
            {
                RestContent<T> content = new RestContent<T>
                {
                    ComplexParams = complex,
                    SimpleParams = dictionary,
                    SendObject = sendt
                };
                var parameters = JsonConvert.SerializeObject(content);

                SendRequest.Invoke(this, new SendRequest
                {
                    RequestType = type,
                    Url = url,
                    Success = success,
                    Parameters = parameters
                });
            }
        }

        public async Task<T> Post<T>(string url, Dictionary<string, object> simpleparams = null, List<Param> complexparams = null)
        {
            var cache = Guid.NewGuid().ToString();
            url += "?cache=" + cache;
            var result = await _service.Post<T>(url, simpleparams, complexparams);
            OnSendRequest<T>(RequestType.Post, url, result != null, dictionary: simpleparams, complex: complexparams);
            return result;
        }
    }

    public class RestContent<T>
    {
        public Dictionary<string, object> SimpleParams { get; set; }
        public List<Param> ComplexParams { get; set; }
        public T SendObject { get; set; }
    }
}
