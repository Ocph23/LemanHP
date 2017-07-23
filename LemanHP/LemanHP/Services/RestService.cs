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
           Task.Run(async()=> await this.CekTokenAsync());
        }

        private async Task CekTokenAsync()
        {
            var main = await Helpers.Helper.GetMainPageAsync();
            if (main.Token != null)
            {
                this.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue(main.Token.token_type, main.Token.access_token);
            }
        }

        public async Task<AuthenticationToken> GenerateTokenAsync(string user, string password)
        {
            try
            {
                var str = string.Format("grant_type=password&username={0}&password={1}",user,password);
                var result = PostAsync("Token", new StringContent(str, Encoding.UTF8)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var Token = JsonConvert.DeserializeObject<AuthenticationToken>(content);

                    if(Token!=null)
                    {
                        Token.Email = user;
                    }

                    var main = await Helpers.Helper.GetMainPageAsync();
                    main.Token= Token;
                    return Token;
                }else
                {
                    throw new System.Exception(result.StatusCode.ToString());
                }

            }
            catch (Exception ex)
            {
                Helpers.Alert.Show("Alert", ex.Message);
                return null;
            }
         
        }
    }


    public class AuthenticationToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string Email { get; internal set; }
    }
}
