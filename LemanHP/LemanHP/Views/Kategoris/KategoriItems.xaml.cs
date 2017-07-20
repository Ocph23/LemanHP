using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LemanHP.ViewModels.Kategoris;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}