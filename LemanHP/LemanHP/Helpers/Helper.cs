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
        public static async Task<Home> GetMainPageAsync()
        {
            var x = await Task.FromResult(Xamarin.Forms.Application.Current.MainPage);
            return x as Home;
        }

        public static Random random = new Random();
        public static string KodePemesanan()
        {
            int panjang = 7;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, panjang)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int KodeValidasi()
        {
            int panjang = 3;
            const string chars = "123456789";
            var value = new string(Enumerable.Repeat(chars, panjang)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return Int32.Parse(value);
        }

    }

   
}
