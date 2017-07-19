using LemanHP.Models;

namespace LemanHP.ViewModels.Barangs
{
    public class KainDetailItemViewModel:BaseViewModel
    {
        private Kain item;

        public KainDetailItemViewModel(Kain item)
        {
            Title = item.Nama;
            this.item = item;
        }
    }
}