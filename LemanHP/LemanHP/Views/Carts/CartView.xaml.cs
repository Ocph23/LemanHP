using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.Carts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartView : ContentPage
    {
        public CartView()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.Carts.CartViewModel(Navigation);
        }

        private void CarttemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.CartItem;
            if (item == null)
                return;

            // await Navigation.PushAsync(new ProdukDetailItem(new ViewModels.Barangs.ProdukDetailItemViewModel(item)));

            // Manually deselect item
            CarttemsListView.SelectedItem = null;
        }
       
    }
}