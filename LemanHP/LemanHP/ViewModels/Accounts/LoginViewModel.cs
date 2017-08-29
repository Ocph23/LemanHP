using LemanHP.Helpers;
using LemanHP.Models;
using LemanHP.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Accounts
{
    public class LoginViewModel : BaseViewModel
    {
        private INavigation navigation;
        private string _password;
        private string _email;
        private ICommand _loginCommand;

        public LoginViewModel(INavigation navigation, UserProfileViewModel profile)
        {
            this.Profile = profile;
            Title = "Login";
           // Email = "Ocph23@gmail.com";
            this.navigation = navigation;
            LoginCommand = new Command(async(x) => await LoginActionAsync(x), LoginValidation);
            RegisterCommand = new Command(RegisterAction,(x)=>true);
        }

        private async Task LoginActionAsync(object x)
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (var res = new Services.RestService())
                {
                    var token = await res.GenerateTokenAsync(this.Email, Password);
                    if (token != null)
                    {
                        MessagingCenter.Send(new MessagingCenterAlert
                        {
                            Title = "Login",
                            Message= "Login Success",
                            Cancel = "OK"
                        }, "message");

                        this.Profile.Load();
                        await navigation.PopToRootAsync();
                    }
                    else
                    {
                        MessagingCenter.Send(new MessagingCenterAlert
                        {
                            Title = "Error",
                            Message = "Gagal Login",
                            Cancel = "OK"
                        }, "message");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

        private void RegisterAction(object obj)
        {
            navigation.PushModalAsync(new Views.Account.RegisterView());
        }

        private bool LoginValidation(object arg)
        {
           if(string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_email))
            {
                return false;
            }else
                return true;
        }

        public string Password {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                LoginCommand = new Command((x) => LoginActionAsync(x), LoginValidation);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                LoginCommand = new Command((x) => LoginActionAsync(x), LoginValidation);
            }
        }

        public ICommand LoginCommand {
            get
            {
                return _loginCommand;
            }
            set
            {
                SetProperty(ref _loginCommand, value);
            }
        }

        public Command RegisterCommand { get; private set; }
        internal UserProfileViewModel Profile { get; private set; }
    }
}
