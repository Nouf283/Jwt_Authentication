using Cookie_Based_Authentication.Authorization;
using Cookie_Based_Authentication.DTO;
using Cookie_Based_Authentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System.Net.Http.Headers;

namespace Cookie_Based_Authentication.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> WeatherForecastItems { get; set; }

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGetAsync()
        {
            //get toekn from session
            //JwtToken token = null;
            //var strTokenObj = HttpContext.Session.GetString("access_token");
            //if (string.IsNullOrWhiteSpace(strTokenObj))
            //{
            //    token = await Authenticate();
            //}
            //else
            //    token = JsonConvert.DeserializeObject <JwtToken>(strTokenObj);

            //if(token == null || string.IsNullOrWhiteSpace(token.AccessToken)
            //    || token.ExpiresAt <= DateTime.UtcNow)
            //{
            //    token = await Authenticate();
            //}

            //var httpClient = httpClientFactory.CreateClient("OurWebAPI");
            ////authentication and getting token
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            //WeatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
            WeatherForecastItems = await InvokeEndPoint<List<WeatherForecastDTO>>("OurWebAPI", "WeatherForecast");
        }

        private async Task<T> InvokeEndPoint<T>(string clientName,string url)
        {
            JwtToken token = null;
            var strTokenObj = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrWhiteSpace(strTokenObj))
            {
                token = await Authenticate();
            }
            else
                token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj);

            if (token == null || string.IsNullOrWhiteSpace(token.AccessToken)
                || token.ExpiresAt <= DateTime.UtcNow)
            {
                token = await Authenticate();
            }

            var httpClient = httpClientFactory.CreateClient(clientName);
            //authentication and getting token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
           return await httpClient.GetFromJsonAsync<T>(url);

        }

        private async Task<JwtToken> Authenticate()
        {
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");
            var res = await httpClient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "password" });
            res.EnsureSuccessStatusCode();
            string strJwt = await res.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("access_token", strJwt);
            return JsonConvert.DeserializeObject<JwtToken>(strJwt);
        }
    }
}
