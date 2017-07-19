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

namespace LemanHP.ViewModels.Barangs
{
    public class BarangViewModel:BaseViewModel
    {
        public BarangViewModel()
        {
            Title = "Home";
            Kains = new ObservableRangeCollection<Models.Kain>();
            Produks = new ObservableRangeCollection<Models.Produk>();
            //LoadItemsCommand = new Command(async()=> await ExecuteLoadItemsCommand());
            ExecuteLoadItemsCommand();
        }

        private async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Kains.Clear();
                Produks.Clear();
                var kains = await KainDataStore.GetItemsAsync(true);
                Kains.ReplaceRange(kains);
                var produks = await ProdukDataStore.GetItemsAsync(true);
                Produks.ReplaceRange(produks);

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

        public ObservableRangeCollection<Kain> Kains { get; private set; }
        public ICommand LoadItemsCommand { get; set; }
        public ObservableRangeCollection<Produk> Produks { get; private set; }
    }
}
