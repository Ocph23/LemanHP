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
        private double _subtotal;
        private Command _checkoutCommand;

        public CartViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Carts = new ObservableCollection<CartItem>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand());
            CheckOutCommand = new Command((X) => CheckOutActionAsync(X),(x)=>CheckOutCommandValidate(x));
            ClearCommand = new Command((x)=> ClearCommandAction());

            ExecuteLoadItemsCommand();
        }

        public void ClearCommandAction()
        {
            CartDataStore.SyncAsync();
            ExecuteLoadItemsCommand();
        }

        private bool CheckOutCommandValidate(object x)
        {
            return Carts.Count > 0 ? true : false;
        }

        private async void CheckOutActionAsync(object x)
        {
            var main = await Helper.GetMainPageAsync();
            if (main.Token != null)
            {
                var page = new Views.Carts.PembayaranView();
                var pelanggan = await UserServiceDataStore.GetUserProfile(main.Token.Email);
                if(pelanggan!=null)
                {
                    var pembelian = new Models.Pembelian { PelangganId = pelanggan.Id, Tanggal = DateTime.Now };
                    foreach (var item in Carts)
                    {
                        pembelian.DetailPembelians.Add(new DetailPembelian { BarangId = item.Id, Discount=item.Discount, Harga=item.Harga, Jumlah=item.Count });
                    }
                    page.BindingContext = new Carts.PembayaranViewModel(navigation, Carts, pelanggan, pembelian);
                    await navigation.PushAsync(page);
                }
                else
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = "Anda Tidak memiliki Akses Hubungi Administrator",
                        Cancel = "OK"
                    }, "message");
                }
               
            }
            else
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Anda Harus Login",
                    Cancel = "OK"
                }, "message");
            }

        }


        public Command LoadItemsCommand { get; private set; }
        public Command DetailCommand { get; private set; }
        public Command CheckOutCommand {
            get { return _checkoutCommand; }
            set
            {
                SetProperty(ref _checkoutCommand, value);
            }

        }

        private async void ExecuteLoadItemsCommand()
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
                    item.OnChangeitem += Item_OnChangeitem;
                    item.OnClickDetail += Item_OnClickDetail;
                    item.OnDeleteItem += Item_OnDeleteItem;
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

        private async void Item_OnDeleteItem(CartItem sender)
        {
            var isDeleteOnCartDataSore = await CartDataStore.DeleteItemAsync(sender);
            if(isDeleteOnCartDataSore)
            {
                Carts.Remove(sender);
            }
        }

        private  async void Item_OnClickDetail(object sender)
        {
            var barang = (Barang)sender;
            if(barang.BarangType== BarangType.Kain)
            {
                Kain kain = await KainDataStore.GetItemAsync(barang.Id);
                await navigation.PushAsync(new Views.Barangs.KainDetailItem(new Barangs.KainDetailItemViewModel(kain)));
            }else
                if (barang.BarangType == BarangType.Produk)
            {
                Produk produk = await ProdukDataStore.GetItemAsync(barang.Id);
                await navigation.PushAsync(new Views.Barangs.ProdukDetailItem(new Barangs.ProdukDetailItemViewModel(produk)));
            }
        }

        public double Subtotal
        {
            get { return _subtotal; }
            set
            {
                SetProperty(ref _subtotal, value);
            }
        }

        public Command ClearCommand { get; private set; }

        private void Item_OnChangeitem()
        {
            Subtotal = Carts.Sum(O => O.Total);
        }
    }
}
