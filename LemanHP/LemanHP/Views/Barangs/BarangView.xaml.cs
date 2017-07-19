using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.Barangs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarangView : TabbedPage
    {
        public BarangView()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.Barangs.BarangViewModel();
        }

        private async void KainOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.Kain;
            if (item == null)
                return;

            await Navigation.PushAsync(new KainDetailItem(new ViewModels.Barangs.KainDetailItemViewModel(item)));

            // Manually deselect item
            KainItemsListView.SelectedItem = null;
        }
        private async void ProdukOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.Produk;
            if (item == null)
                return;

            await Navigation.PushAsync(new ProdukDetailItem(new ViewModels.Barangs.ProdukDetailItemViewModel(item)));

            // Manually deselect item
            ProdukItemsListView.SelectedItem = null;
        }
    }
}