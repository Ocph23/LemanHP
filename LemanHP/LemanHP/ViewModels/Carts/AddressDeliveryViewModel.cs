using System.Collections.ObjectModel;
using LemanHP.Models;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Carts
{
    internal class AddressDeliveryViewModel
    {
        private INavigation navigation;
        private ObservableCollection<CartItem> carts;


        public AddressDeliveryViewModel(INavigation navigation, ObservableCollection<CartItem> carts)
        {
            this.navigation = navigation;
            this.carts = carts;
        }
        public AddressDeliveryViewModel()
        {

        }

    }
}