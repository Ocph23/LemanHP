using LemanHP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http;

[assembly: Dependency(typeof(LemanHP.Services.PembelianService))]

namespace LemanHP.Services
{
    public class PembelianService
    {
        public async Task<bool> InsertPembelian(Pembelian pembelian)
        {

            using (var service = new RestService())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(pembelian, Formatting.Indented);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await service.PostAsync("api/Pembelians",content );
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
