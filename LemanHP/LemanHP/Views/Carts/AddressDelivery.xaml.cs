using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.Carts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddressDelivery : ContentPage
	{
        private ObservableCollection<CartItem> carts;

        public AddressDelivery ()
		{
			InitializeComponent ();
		}

        public AddressDelivery(ObservableCollection<CartItem> carts)
        {
            InitializeComponent();
            this.carts = carts;
            this.BindingContext = new ViewModels.Carts.AddressDeliveryViewModel(Navigation, carts);
        }
    }
}