using LemanHP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LemanHP.Services.JenisProdukDataStore))]
namespace LemanHP.Services
{
    public class JenisProdukDataStore : IDataStore<JenisProduk>
    {
        bool isInitialized;
        List<JenisProduk> items;
        public Task<bool> AddItemAsync(JenisProduk item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(JenisProduk item)
        {
            throw new NotImplementedException();
        }

        public Task<JenisProduk> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<JenisProduk> GetItemAsync(int id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<JenisProduk>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<Models.JenisProduk>();
            using (var service = new RestService())
            {
                try
                {
                    var response = await service.GetAsync("api/JenisProduks");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<JenisProduk>>(content);
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

        public Task<bool> UpdateItemAsync(JenisProduk item)
        {
            throw new NotImplementedException();
        }
    }
}
