﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace LemanHP.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task Initialize();
        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();
    }
}
