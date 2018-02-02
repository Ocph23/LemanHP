using LemanHP.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LemanHP.Services
{
   public  class RestService: HttpClient
    {

        public RestService()
        {
           
           // this.MaxResponseContentBufferSize = 256000;
            //var a = ConfigurationManager.AppSettings["IP"];
            this.BaseAddress = new Uri("http://192.168.10.1/");
           // this.BaseAddress = new Uri("http://192.168.1.22/");
            //this.BaseAddress = new Uri("http://batikpapua.gear.host/");
            this.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //key api = 57557c4f25f436213fe34a2090a266e2
           this.CekTokenAsync();
        }

        public RestService(string apiUrl)
        {
            this.BaseAddress = new Uri(apiUrl);
          
        }

        private async void CekTokenAsync()
        {
            var main = await Helpers.Helper.GetMainPageAsync();
            if (main!=null && main.Token != null)
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
                var result = await PostAsync("Token", new StringContent(str, Encoding.UTF8));
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
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
                    Cancel = "OK"
                }, "message");
                return null;
            }
         
        }
    }

    internal class NameValueCollection
    {
        internal void Add(string v1, string v2)
        {
            throw new NotImplementedException();
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
