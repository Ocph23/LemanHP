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
    public partial class KainDetailItem : ContentPage
    {
        private KainDetailItemViewModel kainDetailItemViewModel;
      
        public KainDetailItem(KainDetailItemViewModel kainDetailItemViewModel)
        {
            InitializeComponent();
            this.kainDetailItemViewModel = kainDetailItemViewModel;
            this.BindingContext = kainDetailItemViewModel;
        }
        public KainDetailItem()
        {
            InitializeComponent();
        }
    }
}