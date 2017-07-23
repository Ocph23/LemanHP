using LemanHP.Models;
using System.Collections.ObjectModel;

namespace LemanHP.ViewModels.Kategoris
{
    public class KategorisItemViewModel:BaseViewModel
    {
        private Kategori item;
        public ObservableCollection<Models.Barang> Barangs { get; set; }

        public KategorisItemViewModel(Kategori item)
        {
            this.item = item;
            Title = item.Nama;
            Barangs = new ObservableCollection<Barang>();
        }
    }
}