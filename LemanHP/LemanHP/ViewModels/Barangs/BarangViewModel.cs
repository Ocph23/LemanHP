using LemanHP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace LemanHP.ViewModels.Barangs
{
    public class BarangViewModel:BaseViewModel
    {
        public BarangViewModel()
        {
            Title = "Home";
            Kains = new ObservableCollection<Models.Kain>();
            Produks = new ObservableCollection<Models.Produk>();
            LoadItemsCommand = new Command((x)=> ExecuteLoadItemsCommand(x));
            ExecuteLoadItemsCommand(null);
        }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Kains.Clear();
                Produks.Clear();
                var kains = await KainDataStore.GetItemsAsync(true);
              foreach(var item in kains)
                {
                    Kains.Add(item);
                }
                var produks = await ProdukDataStore.GetItemsAsync(true);
               foreach(var item in produks)
                {
                    Produks.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ObservableCollection<Kain> Kains { get; private set; }
        public ICommand LoadItemsCommand { get; set; }
        public ObservableCollection<Produk> Produks { get; private set; }
    }
}
