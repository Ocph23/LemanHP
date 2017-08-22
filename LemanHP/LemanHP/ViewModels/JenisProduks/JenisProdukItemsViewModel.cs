using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using LemanHP.Services;
using System.Diagnostics;
using LemanHP.Helpers;

namespace LemanHP.ViewModels.JenisProduks
{
    public class JenisProdukItemsViewModel : BaseViewModel
    {
        private JenisProduk item;
        public ObservableCollection<Models.Produk> Barangs { get; set; }
        public Command LoadItemsCommand { get; private set; }

        public JenisProdukItemsViewModel(JenisProduk item)
        {
            Title = item.Nama;
            this.item = item;
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            Barangs = new ObservableCollection<Produk>();
            ExecuteLoadItemsCommand(null);
        }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Barangs.Clear();
                var datastore = new BarangDataStore();
                var barangs = await datastore.GetItemsByJenisBarang(item);
                foreach (var item in barangs)
                {
                    Barangs.Add(item);
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
    }
}
