using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.JenisProduks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JenisProdukView : ContentPage
    {
        public JenisProdukView()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.JenisProduks.JenisProdukViewModel();
        }

        private async void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.JenisProduk;
            if (item == null)
                return;

            await Navigation.PushAsync(new Views.JenisProduks.JenisProdukItemsView(new ViewModels.JenisProduks.JenisProdukItemsViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
    }
}