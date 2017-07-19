using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Services
{
   public  class RestService: HttpClient
    {


        public RestService()
        {
           
           // this.MaxResponseContentBufferSize = 256000;
            this.BaseAddress = new Uri("http://192.168.1.6/");
            //GenerateTokenAsync();
        }

        public AuthenticationToken Token { get; private set; }

        private async void GenerateTokenAsync()
        {
            try
            {
                var result = PostAsync("Token", new StringContent("grant_type=password&username=Ocph23@gmail.com&password=Sony@77", Encoding.UTF8)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    this.Token = JsonConvert.DeserializeObject<AuthenticationToken>(content);
                    this.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(Token.token_type, Token.access_token);
                }else
                {
                    throw new System.Exception(result.StatusCode.ToString());
                }

            }
            catch (Exception ex)
            {
                Helpers.Alert.Show("Alert", ex.Message);
            }
         
        }
    }


    public class AuthenticationToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
