using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LemanHP.ViewModels.Kategoris;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LemanHP.Models;

namespace LemanHP.Views.Kategoris
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KategoriItems : ContentPage
    {
        private KategorisItemViewModel kategorisItemViewModel;

        public ObservableCollection<string> Items { get; set; }

        public KategoriItems()
        {
            InitializeComponent();
        }
        public KategoriItems(KategorisItemViewModel kategorisItemViewModel)
        {
            InitializeComponent();
            this.kategorisItemViewModel = kategorisItemViewModel;
            BindingContext = this.kategorisItemViewModel;
        }

        async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void KategoriItemsOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem== null)
                return;

            var type = e.SelectedItem.GetType();
            if(type.Name == typeof(Kain).Name)
            {
                var item = e.SelectedItem as Kain;
                await Navigation.PushAsync(new Views.Barangs.KainDetailItem(new ViewModels.Barangs.KainDetailItemViewModel(item)));
            }
            else if (type.Name == typeof(Produk).Name)
            {
                var item = e.SelectedItem as Produk;
                await Navigation.PushAsync(new Views.Barangs.ProdukDetailItem(new ViewModels.Barangs.ProdukDetailItemViewModel(item)));
            }


            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
    }
}