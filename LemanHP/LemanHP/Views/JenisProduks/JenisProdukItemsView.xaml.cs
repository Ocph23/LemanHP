using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.ViewModels.JenisProduks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LemanHP.Models;

namespace LemanHP.Views.JenisProduks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JenisProdukItemsView : ContentPage
    {
        private JenisProdukItemsViewModel jenisProdukItemsViewModel;

        public JenisProdukItemsView()
        {
            InitializeComponent();
        }

        public JenisProdukItemsView(JenisProdukItemsViewModel jenisProdukItemsViewModel)
        {
            InitializeComponent();
            this.jenisProdukItemsViewModel = jenisProdukItemsViewModel;
            this.BindingContext = jenisProdukItemsViewModel;
        }

        private async void JenisProdukItemsOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null)
                return;

            var item = e.SelectedItem as Produk;
            await Navigation.PushAsync(new Views.Barangs.ProdukDetailItem(new ViewModels.Barangs.ProdukDetailItemViewModel(item)));


            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
    }
}