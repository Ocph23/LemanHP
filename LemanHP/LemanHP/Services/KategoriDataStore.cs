using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

[assembly: Dependency(typeof(LemanHP.Services.KategoriDataStore))]
namespace LemanHP.Services
{
    public class KategoriDataStore : IDataStore<Models.Kategori>
    {
        bool isInitialized;
        List<Kategori> items;

        public Task<bool> AddItemAsync(Kategori item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Kategori item)
        {
            throw new NotImplementedException();
        }

        public  Task<Kategori> GetItemAsync(string id)
        {

            throw new NotImplementedException();
            
        }

        public async Task<Kategori> GetItemAsync(int id)
        {
            await Initialize();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Kategori>> GetItemsAsync(bool forceRefresh = false)
        {
            await Initialize();

            return await Task.FromResult(items);
        }

        public async Task Initialize()
        {
            if (isInitialized)
                return;

            items = new List<Models.Kategori>();
            using (var service = new RestService())
            {
                try
                {
                    var response = await service.GetAsync("api/Kategoris");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<List<Kategori>>(content);
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

        public Task<bool> UpdateItemAsync(Kategori item)
        {
            throw new NotImplementedException();
        }
      
    }
}
