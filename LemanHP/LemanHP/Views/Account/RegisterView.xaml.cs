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
	public partial class RegisterView : ContentPage
	{
        private RegisterViewModel vm;

        public RegisterView ()
		{
			InitializeComponent ();
            this.vm= new ViewModels.Accounts.RegisterViewModel();
            this.BindingContext = vm;

        }

       

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var password = (Entry)sender;
            if(password.Text!=null)
             vm.DataPelanggan.Password = password.Text;
        }

        private void Entry_PropertyChanged_1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }
    }
}