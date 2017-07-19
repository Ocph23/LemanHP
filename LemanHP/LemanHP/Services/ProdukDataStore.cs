using LemanHP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LemanHP.Services.ProdukDataStore))]

namespace LemanHP.Services
{

   public class ProdukDataStore:IDataStore<Produk>
    {
        bool isInitialized;
        List<Produk> items;

        public Task<bool> AddItemAsync(Produk item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Produk item)
        {
            throw new NotImplementedException();
        }

        public Task<Produk> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Produk> GetItemAsync(int id)
        {
            await Initialize();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Produk>> GetItemsAsync(bool forceRefresh = false)
        {
            await Initialize();
            return await Task.FromResult(items);
        }

        public async Task Initialize()
        {
            if (isInitialized)
                return;

            items = new List<Models.Produk>();
            using (var service = new RestService())
            {
                try
                {
                    var response = await service.GetAsync("api/Produks");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<Produk>>(content);
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

        public Task<bool> UpdateItemAsync(Produk item)
        {
            throw new NotImplementedException();
        }
    }
}
