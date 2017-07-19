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
        private ProdukDetailItemViewModel produkDetailItemViewModel;

        public ProdukDetailItem()
        {
            InitializeComponent();
        }

        public ProdukDetailItem(ProdukDetailItemViewModel produkDetailItemViewModel)
        {
          
            this.produkDetailItemViewModel = produkDetailItemViewModel;
            this.BindingContext = produkDetailItemViewModel;
        }
       
    }
}