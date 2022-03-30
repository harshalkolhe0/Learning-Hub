using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LearningHubMVC.ServiceRepository
{
    public class ServiceRepo
    {
        public HttpClient Client { get; set; }
        /*
        public static  IHttpContextAccessor _httpContextAccessor;
        public static ISession _session;
        public ServiceRepo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _session.SetString("AuthKey", "");
        }
        */
        public ServiceRepo(string token,string un)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7203/api/");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(
                mediaType: "application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer",
                parameter: token + ":" + un);
        }
        /*
        //public HttpResponseMessage GetResponse(string url)
        public List<T> GetResponse<T>(string url)
        {
            //return Client.GetAsync(url).Result;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<List<T>>().Result;
            //return response;
        }
        public List<T> GetMyEnrollments<T>(string url)
        {
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<List<T>>().Result;
        }
        public T GetOneResponse<T>(string url)
        {
            //return Client.GetAsync(url).Result;
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<T>().Result;
            //return response;
        }
        public void PutResponse(string url, object model)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync(url, model).Result;
            response.EnsureSuccessStatusCode();
        }
        
        //public HttpResponseMessage PostResponse(string url, object model)
        //{
        //    HttpResponseMessage response = Client.PostAsJsonAsync(url, model).Result;
        //    response.EnsureSuccessStatusCode();
        //    return response;
        //}
        
        
        public void PostResponse(string url, object model)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync(url, model).Result;
            response.EnsureSuccessStatusCode();
        }
        public void DeleteResponse(string url)
        {
            HttpResponseMessage response = Client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
        }
        */
        public List<T> GetResponse<T>(string url)
        {
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<List<T>>().Result;
        }
        public List<T> GetMyEnrollments<T>(string url)
        {
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<List<T>>().Result;
        }
        public T GetOneResponse<T>(string url)
        {
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<T>().Result;
        }
        public void PutResponse(string url, object model)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync(url, model).Result;
            response.EnsureSuccessStatusCode();
        }

        public void PostResponse(string url, object model)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync(url, model).Result;
            response.EnsureSuccessStatusCode();
        }
        public void DeleteResponse(string url)
        {
            HttpResponseMessage response = Client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
        }
        public string GetToken(string url, object model)
        {
            string token = "";
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(
                mediaType: "application/json"));
            var responsemessage = Client.PostAsJsonAsync(url,model).Result;
            if(responsemessage.IsSuccessStatusCode)
            {
                var result = responsemessage.Content.ReadAsStringAsync().Result;
                token = result;
            }
            return token;
        }

        public T GetResponseTemp<T>(string url)
        {
            //Client.DefaultRequestHeaders.Clear();
            //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(
            //    mediaType: "application/json"));
            //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer",
            //    parameter: token+":"+username);
            HttpResponseMessage response = Client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<T>().Result;
        }
        
    }
}
