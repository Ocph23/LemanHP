using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Models
{
    public partial class Pengiriman
    {
        public int Id { get; set; }
        public string NomorResi { get; set; }
        public string Ekspedisi { get; set; }
        public string Kota { get; set; }
        public double Berat { get; set; }
        public double Tarif { get; set; }
        public DateTime TanggalKirim { get; set; }

        public virtual Pembelian Pembelian { get; set; }
    }
}
