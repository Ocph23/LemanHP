using LemanHP.Views;
using System;
using System.Collections.Generic;
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

        public LoginViewModel(INavigation navigation)
        {
            Title = "Login";
            this.navigation = navigation;
            LoginCommand = new Command((x) => LoginActionAsync(x), LoginValidation);
            RegisterCommand = new Command(RegisterAction,(x)=>true);
        }

        private async void LoginActionAsync(object x)
        {
            using (var res = new Services.RestService())
            {
                try
                {
                    var token = await res.GenerateTokenAsync(this.Email, Password);
                    if (token != null)
                    {
                        Helpers.Alert.Show("Info", "Anda Berhasil Login");
                    }
                    else
                    {
                        Helpers.Alert.Show("Error", "Gagal Login !");
                    }
                }
                catch (Exception e)
                {

                    Helpers.Alert.Show("Error", e.Message);
                }
               
            }
        }

        private void RegisterAction(object obj)
        {
            navigation.PushAsync(new Views.Account.RegisterView());
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
    }
}
