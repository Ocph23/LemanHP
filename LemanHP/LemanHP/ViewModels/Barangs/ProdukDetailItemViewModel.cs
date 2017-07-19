using LemanHP.Models;

namespace LemanHP.ViewModels.Barangs
{
    public class ProdukDetailItemViewModel:BaseViewModel
    {
        private Produk item;

        public ProdukDetailItemViewModel(Produk item)
        {
            Title = item.Nama;
            this.item = item;
        }
    }
}