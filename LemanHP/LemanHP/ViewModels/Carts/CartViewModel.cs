using LemanHP.Helpers;
using LemanHP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Carts
{
    public class CartViewModel:BaseViewModel
    {
        public ObservableCollection<CartItem> Carts { get; private set; }
        
        private INavigation navigation;

        public CartViewModel(INavigation navigation)
        {
            
            this.navigation = navigation;
            Carts = new ObservableCollection<CartItem>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            DetailCommand = new Command((x) => DetailAction(x));
            CheckOutCommand = new Command((X) => CheckOutAction(X));
            ExecuteLoadItemsCommand(null);
        }

        private void CheckOutAction(object x)
        {
           navigation.PushAsync(new Views.Carts.AddressDelivery(Carts));
        }

        private void DetailAction(object x)
        {
           // throw new NotImplementedException();
        }

        public Command LoadItemsCommand { get; private set; }
        public Command DetailCommand { get; private set; }
        public Command CheckOutCommand { get; private set; }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Carts.Clear();
                var carts = await CartDataStore.GetItemsAsync(true);
                foreach(var item in carts)
                {
                    Carts.Add(item);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
