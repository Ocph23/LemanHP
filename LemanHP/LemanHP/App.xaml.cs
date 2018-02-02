using System;
using LemanHP.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LemanHP
{   
    public partial class App : Application
    {
        public App()
        {
           // Assembly assembly = typeof(App).GetTypeInfo().Assembly;
          //  ConfigurationManager.Initialise(assembly.GetManifestResourceStream("LemanHP.App.config"));
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

     


    }
}
