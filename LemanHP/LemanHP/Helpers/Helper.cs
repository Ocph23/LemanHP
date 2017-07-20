using LemanHP.Services;
using LemanHP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Helpers
{
    public static class Helper
    {
        public static Home GetMainPage()
        {
            return (Home)Xamarin.Forms.Application.Current.MainPage;
        }

       
    }
}
