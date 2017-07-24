using LemanHP.Helpers;
using LemanHP.Models;
using LemanHP.Services;

using Xamarin.Forms;

namespace LemanHP.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IDataStore<Kategori> KategoriDataStore => DependencyService.Get<IDataStore<Kategori>>();
        public IDataStore<Kain> KainDataStore => DependencyService.Get<IDataStore<Kain>>();
        public IDataStore<Produk> ProdukDataStore => DependencyService.Get<IDataStore<Produk>>();
        public IDataStore<CartItem> CartDataStore => DependencyService.Get<IDataStore<CartItem>>();
        public ICityDataStore CityDataStore => DependencyService.Get<ICityDataStore>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

