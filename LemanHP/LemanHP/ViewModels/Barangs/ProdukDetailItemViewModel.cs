using LemanHP.Models;

namespace LemanHP.ViewModels.Barangs
{
    public class ProdukDetailItemViewModel:BaseViewModel
    {
        private Produk _produk;

        public ProdukDetailItemViewModel(Produk item)
        {
            Title = item.Nama;
            this.produk = item;
        }

        public Produk produk {
            get
            {
                return _produk;
            }
            set
            {
                SetProperty(ref _produk, value);
            }


        }



    }
}