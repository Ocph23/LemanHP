using System;
using LemanHP.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LemanHP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetMainPageWithMasterDetail();
        }

        private void SetMainPageWithMasterDetail()
        {
            Current.MainPage = new Views.Home();
        }

        
    }
}
