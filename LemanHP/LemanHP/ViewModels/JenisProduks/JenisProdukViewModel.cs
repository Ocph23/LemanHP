using LemanHP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using System.Diagnostics;

namespace LemanHP.ViewModels.JenisProduks
{
    public class JenisProdukViewModel:BaseViewModel
    {
        public JenisProdukViewModel()
        {
            Title = "Jenis Produk";
            Items = new ObservableRangeCollection<Models.JenisProduk>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            ExecuteLoadItemsCommand();
        }

        async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await JenisProdukDataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
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

        public ObservableRangeCollection<JenisProduk> Items { get; private set; }
        public Command LoadItemsCommand { get; private set; }
    }
}
