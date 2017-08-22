using LemanHP.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LemanHP.Models
{
    public class Barang : ObservableObject
    {
        private ObservableCollection<Foto> _fotoes;
        private double _afterDiscount;

        public Barang()
        {
            this.BarangKategoris = new HashSet<BarangKategori>();
            this.Fotoes = new ObservableCollection<Foto>();
        }
        public int Id { get; set; }
        public string Nama { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Satuan Satuan { get; set; }
        public double Berat { get; set; }
        public double Harga { get; set; }
        public double Stock { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BarangType BarangType { get; set; }
        public double Discount { get; set; }
        public string Keterangan { get; set; }
        public double HargaAfterDiscount {
            get { return Harga-( Harga * (Convert.ToDouble(Discount )/ 100)); }
            set
            {
                SetProperty(ref _afterDiscount, value);
            }
        }
        public virtual ICollection<BarangKategori> BarangKategoris { get; set; }
        public virtual ObservableCollection<Foto> Fotoes {
            get { return _fotoes; }
            set
            {
                SetProperty(ref _fotoes, value);
            }

        }
    }
}

   