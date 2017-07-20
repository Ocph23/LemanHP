using LemanHP.Models;

namespace LemanHP.ViewModels.Barangs
{
    public class KainDetailItemViewModel:BaseViewModel
    {
        private Kain _kain;

        public Kain kain { get { return _kain; }
            set {
                SetProperty(ref _kain, value);
            }
        }

        public KainDetailItemViewModel(Kain item)
        {
            Title = item.Nama;
            this.kain = item;
        }
    }
}