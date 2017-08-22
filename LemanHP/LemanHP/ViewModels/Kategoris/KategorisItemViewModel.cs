using LemanHP.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using LemanHP.Services;
using System.Diagnostics;
using LemanHP.Helpers;

namespace LemanHP.ViewModels.Kategoris
{
    public class KategorisItemViewModel:BaseViewModel
    {
        private Kategori item;
        public ObservableCollection<Models.Barang> Barangs { get; set; }
        public Command LoadItemsCommand { get; private set; }

        public KategorisItemViewModel(Kategori item)
        {
            this.item = item;
            Title = item.Nama;
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            Barangs = new ObservableCollection<Barang>();
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
                var barangs=await datastore.GetItemByKategori(item);
                foreach(var item in barangs)
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