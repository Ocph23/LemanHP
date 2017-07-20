using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.ViewModels.Barangs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views.Barangs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdukDetailItem : ContentPage
    {
        public ProdukDetailItem(ProdukDetailItemViewModel produkDetailItemViewModel)
        {
            InitializeComponent();
            this.BindingContext = produkDetailItemViewModel;
        }
        public ProdukDetailItem()
        {
            InitializeComponent();
        }
      
    }
}