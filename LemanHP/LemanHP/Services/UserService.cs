using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

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
                    var response = await service.GetAsync("api/Profile?email="+email);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                       return JsonConvert.DeserializeObject<Pelanggan>(content);
                    }
                    else
                    {
                        Helpers.Alert.Show("Alert", "Error "+ response.StatusCode.ToString());
                        return null;
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
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
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
                    return false;
                }
            }
            
        }
    }
}
