using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.Kategoris
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KategoriView : ContentPage
    {
        public KategoriView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Kategoris.KategoriViewModel();
        }

        async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Models.Kategori;
            if (item == null)
                return;

            await Navigation.PushAsync(new Views.Kategoris.KategoriItems(new ViewModels.Kategoris.KategorisItemViewModel(item)));

            // Manually deselect item
            //ItemsListView.SelectedItem = null;
        }
    }
}