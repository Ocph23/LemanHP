using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Helpers
{
    public static class Alert
    {
        public static async void Show(string title, string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Ok");

        }

        public static async Task<bool> ShowDialogMessage(string title, string message)
        {
            return await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Yes", "No");

        }
    }
}
