using System;
using LemanHP.Models;
using LemanHP.Services;
using Xamarin.Forms;
using LemanHP.Helpers;
using System.Linq;

namespace LemanHP.ViewModels.Accounts
{
    public class UserProfileViewModel:BaseViewModel
    {
        private AuthenticationToken token;
        private Pelanggan _pelanggan;
        private Command _pesananSayaCommand;
        private INavigation navigation;
        private Pembelian pembelian;

        public UserProfileViewModel(INavigation navigation)
        {
            this.Title = "Profile";
            this.PesananSayaCommand = new Command(() => PesanSayaAction(), () => PesananSayaValidate());
            this.navigation = navigation;
            this.Load();
        }

        private bool PesananSayaValidate()
        {
            if (Profile != null && Profile.Pembelians.Count > 0)
            {
                this.pembelian = Profile.Pembelians.Where(O => O.StatusPembelian == StatusPembelian.Baru && O.Pembayaran == null ).FirstOrDefault();
                if (pembelian != null)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private async void PesanSayaAction()
        {
           await  navigation.PushAsync(new Views.Account.PesananSaya(pembelian,Profile,navigation));
        }

        public Pelanggan Profile { get { return _pelanggan; }
            set {
                SetProperty(ref _pelanggan, value);
            } }

        public Command PesananSayaCommand { get { return _pesananSayaCommand; }
            set { SetProperty(ref _pesananSayaCommand, value); } }

        public async void Load()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var main = await Helpers.Helper.GetMainPageAsync();
                
                if(main!=null && main.Token!=null)
                {
                    this.token = main.Token;
                    this.Profile = await UserServiceDataStore.GetUserProfile(token.Email);
                    if (this.Profile != null)
                    {
                        this.PesananSayaCommand = new Command(() => PesanSayaAction(), () => PesananSayaValidate());
                    }
                }
                else
                {
                    await navigation.PushAsync(new Views.Account.LoginView(this));
                    this.Load();
                }

              
               
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
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