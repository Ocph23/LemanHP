using System;
using LemanHP.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

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
            MessagingCenter.Subscribe<Helpers.MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

            });
        }

        protected override void OnStart()
        {
            base.OnStart();
            MobileCenter.Start("android=c6bc6bca-6dc9-4d8a-a0e1-92a18273c77d;" ,
                   typeof(Analytics), typeof(Crashes));
        }



    }
}
