using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.ViewModels.Accounts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        private LoginViewModel vm;

        public LoginView ()
		{
			InitializeComponent ();
           this.vm=new ViewModels.Accounts.LoginViewModel(Navigation);
            this.BindingContext = this.vm;

        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var password = (Entry)sender;
            if (!string.IsNullOrEmpty(password.Text))
                vm.Password = password.Text;
        }
    }
}