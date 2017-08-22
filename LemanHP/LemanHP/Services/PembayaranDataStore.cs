using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LemanHP.Services.PembayaranDataStore))]
namespace LemanHP.Services
{
    public class PembayaranDataStore
    {
        public async Task<bool> Konfirmasi(Models.Pembayaran pembayaran)
        {
            using (var service = new RestService())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(pembayaran, Formatting.Indented);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await service.PostAsync("api/pembayarans", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                        return false;
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
