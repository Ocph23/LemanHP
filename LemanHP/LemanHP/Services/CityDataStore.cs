using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LemanHP.Services.CityDataStore))]
namespace LemanHP.Services
{
  
    public class CityDataStore:ICityDataStore
    {
        bool isInitializedProvince;
        private List<Province> _provinces;
        private bool isInitializedCity;
        private CityResult _cities;

        public async Task<List<City>> GetCities(string provinceID)
        {
            using (var service = new RestService("http://api.rajaongkir.com/"))
            {
                try
                {
                    service.DefaultRequestHeaders.Add("key", "6aa8a7ceea9a4bc561411077c8446c93");
                    var response = await service.GetAsync("starter/city?province="+provinceID);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                       var  r= JsonConvert.DeserializeObject<rajaongkircity>(content);
                        var c = new CityResult();
                        return await Task.FromResult(r.rajaongkir.results);
                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
                    return null;
                }
            }
        }

        public async Task<CityResult> GetCity(string cityId, string provinceId)
        {
            using (var service = new RestService("http://api.rajaongkir.com/"))
            {
                try
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                          new KeyValuePair<string, string>("key", "6aa8a7ceea9a4bc561411077c8446c93")
                    };
                    var encodedContent = new FormUrlEncodedContent(pairs);
                    var response = await service.PostAsync("starter/city?id="+cityId+"&province=" + provinceId, encodedContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var c = JsonConvert.DeserializeObject<CityResult>(content);
                        return await Task.FromResult(c);
                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
                    return null;
                }
            }
        }

        public async Task<CostResult> GetCost(string courier, string weight, string destination)
        {
            using (var service = new RestService("http://api.rajaongkir.com/"))
            {
                try
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                          new KeyValuePair<string, string>("key", "6aa8a7ceea9a4bc561411077c8446c93"),
                           new KeyValuePair<string, string>("origin", "158"),
                            new KeyValuePair<string, string>("destination", destination),
                             new KeyValuePair<string, string>("weight", weight),
                              new KeyValuePair<string, string>("courier", courier)
                    };
                    var encodedContent = new FormUrlEncodedContent(pairs);
                    var response = await service.PostAsync("starter/cost", encodedContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var c = JsonConvert.DeserializeObject<rajaongkircost>(content);
                        return await Task.FromResult(c.rajaongkir);
                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
                    return null;
                }
            }
        }

        public async Task<List<Province>> GetProvincies()
        {
            await InitializeProvinceAsync();
            return await Task.FromResult(_provinces);
          
        }

        public async Task InitializeCityAsync()
        {
            if (isInitializedCity)
                return;

            using (var service = new RestService("http://api.rajaongkir.com/"))
            {
                try
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                          new KeyValuePair<string, string>("key", "6aa8a7ceea9a4bc561411077c8446c93")
                    };
                    var encodedContent = new FormUrlEncodedContent(pairs);
                    var response = await service.PostAsync("starter/city", encodedContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _cities = JsonConvert.DeserializeObject<CityResult>(content);
                       isInitializedCity = true;
                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
                }
            }

        }

        public async Task InitializeProvinceAsync()
        {
            if (isInitializedProvince)
                return;

            using (var service = new RestService("http://api.rajaongkir.com/"))
            {
                try
                {
                 
                    service.DefaultRequestHeaders.Add("key", "6aa8a7ceea9a4bc561411077c8446c93");
                    var response = await service.GetAsync("starter/province");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                       var  p = JsonConvert.DeserializeObject<rajaongkirresult>(content);
                        _provinces = p.rajaongkir.results;
                        isInitializedProvince = true;
                    }
                    else
                    {
                        throw new System.Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {

                    Helpers.Alert.Show("Alert", ex.Message);
                }
            }

        }
    }
    
}
