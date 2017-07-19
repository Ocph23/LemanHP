using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LemanHP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMaster : ContentPage
    {
        public ListView ListView;

        public HomeMaster()
        {
            InitializeComponent();

            BindingContext = new HomeMasterViewModel();
            ListView = MenuItemsListView;
        }

        class HomeMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

            public HomeMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomeMenuItem>(new[]
                {
                    new HomeMenuItem { Id = 0, Title = "Home" },
                    new HomeMenuItem { Id = 1, Title = "Kategori" , TargetType=typeof(Views.Kategoris.KategoriView)},
                    new HomeMenuItem { Id = 2, Title = "Jenis Barang" ,TargetType=typeof(Views.JenisProduks.JenisProdukView) },
                    new HomeMenuItem { Id = 3, Title = "Keranjang" },
                    new HomeMenuItem { Id = 4, Title = "Help", TargetType=typeof(Views.Helps.Help) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}