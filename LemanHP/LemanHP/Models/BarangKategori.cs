namespace LemanHP.Models
{
    public class BarangKategori
    {
        public int Id { get; set; }
        public int BarangId { get; set; }
        public int KategoriId { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}