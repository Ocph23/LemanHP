using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(LemanHP.Services.CartDataStore))]
namespace LemanHP.Services
{
    public class CartDataStore : IDataStore<Models.CartItem>
    {
        bool isInitialized;
        List<CartItem> list;
        public Task<bool> AddItemAsync(CartItem item)
        {
            Initialize();
            bool result = true;
            try
            {
                list.Add(item);
            }
            catch (Exception)
            {
                result = false;
            }
            return Task.FromResult(result);
        }

        public Task<bool> DeleteItemAsync(CartItem item)
        {
            Initialize();
            return Task.FromResult(list.Remove(item));
        }

        public Task<CartItem> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetItemAsync(int id)
        {
            Initialize();
            return Task.FromResult(list.Where(O=>O.Id==id).FirstOrDefault());
        }

        public Task<IEnumerable<CartItem>> GetItemsAsync(bool forceRefresh = false)
        {
            Initialize();

            return Task.FromResult(list as IEnumerable<CartItem>);
        }

        public void Initialize()
        {
            if (isInitialized)
                return;
            list = new List<CartItem>();
            isInitialized = true;
        }

        
        public Task<bool> PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync()
        {
            isInitialized = false;
            Initialize();
            return Task.FromResult( true);

        }

        public Task<bool> UpdateItemAsync(CartItem item)
        {
            throw new NotImplementedException();
        }

        Task IDataStore<CartItem>.InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
