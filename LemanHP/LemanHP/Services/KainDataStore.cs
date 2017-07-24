using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

[assembly: Dependency(typeof(LemanHP.Services.KainDataStore))]
namespace LemanHP.Services
{
    public class KainDataStore : IDataStore<Models.Kain>
    {
        bool isInitialized;
        List<Kain> items;

        public Task<bool> AddItemAsync(Kain item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Kain item)
        {
            throw new NotImplementedException();
        }

        public Task<Kain> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Kain> GetItemAsync(int id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Kain>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<Models.Kain>();
            using (var service = new RestService())
            {
                try
                {
                    var response = await service.GetAsync("api/Kains");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<Kain>>(content);
                        foreach (var item in Items)
                        {
                            items.Add(item);
                        }
                        isInitialized = true;
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

        public Task<bool> PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Kain item)
        {
            throw new NotImplementedException();
        }
    }
}
