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

        public KategoriItems(KategorisItemViewModel kategorisItemViewModel)
        {
            InitializeComponent();
            this.kategorisItemViewModel = kategorisItemViewModel;
            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

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