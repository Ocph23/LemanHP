using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using LemanHP.Helpers;

[assembly: Dependency(typeof(LemanHP.Services.UserService))]
namespace LemanHP.Services
{
    public class UserService
    {
        internal async Task<Pelanggan> GetUserProfile(string email)
        {
            using (var service = new RestService())
            {
                try
                {
                    var response = await service.GetAsync("api/profile?email="+email);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                       return JsonConvert.DeserializeObject<Pelanggan>(content);
                    }
                    else
                    {
                        MessagingCenter.Send(new MessagingCenterAlert
                        {
                            Title = "Error",
                            Message ="Terjadi Kesalahan",
                            Cancel = "OK"
                        }, "message");
                        return null;
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

        internal async Task<bool> Register(Pelanggan pelanggan)
        {
            using (var service = new RestService())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(pelanggan, Formatting.Indented);
                    var cnt = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await service.PostAsync("api/account", cnt);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        throw new System.Exception(content);
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
                    return false;
                }
            }
            
        }
    }
}
