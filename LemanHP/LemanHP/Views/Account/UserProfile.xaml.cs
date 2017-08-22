
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LemanHP.ViewModels.Accounts;

namespace LemanHP.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        private UserProfileViewModel vm;

        public UserProfile()
        {
            InitializeComponent();
            this.vm=new ViewModels.Accounts.UserProfileViewModel(Navigation);
            this.BindingContext = vm;
        }

    }
}