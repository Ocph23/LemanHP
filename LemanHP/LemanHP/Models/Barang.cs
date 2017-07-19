using LemanHP.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;


namespace LemanHP.Models
{
    public class Barang : ObservableObject
    {
        private ObservableRangeCollection<Foto> _fotoes;

        public Barang()
        {
            this.BarangKategoris = new HashSet<BarangKategori>();
            this.Fotoes = new ObservableRangeCollection<Foto>();
        }

        public int Id { get; set; }
        public string Nama { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Satuan Satuan { get; set; }
        public double Berat { get; set; }
        public double Harga { get; set; }
        public double Stock { get; set; }

        public virtual ICollection<BarangKategori> BarangKategoris { get; set; }
        public virtual ObservableRangeCollection<Foto> Fotoes {
            get { return _fotoes; }
            set
            {
                SetProperty(ref _fotoes, value);
            }

        }
    }
}

   