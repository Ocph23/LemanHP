using LemanHP.Helpers;

namespace LemanHP.Models
{
    public class Foto : ObservableObject
    {
        private byte[] _picture;

        public int Id { get; set; }
        public byte[] Picture {
            get { return _picture; }
            set { SetProperty(ref _picture, value); }

        }
        public int BarangId { get; set; }
    }
}