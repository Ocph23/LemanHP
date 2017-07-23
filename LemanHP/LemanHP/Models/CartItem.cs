using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Models
{
   public class CartItem:Barang
    {
        private int _count;
        private double _total;

        public int Count
        {
            get { return _count; }
            set {
                if(value!=0)
                { 
                     SetProperty(ref _count, value);
                    Total = value;
                }
            }
        }

        public double Total
        {
            get { return Convert.ToDouble( _count) * Harga; }
            set
            {
                  SetProperty(ref _total, value);
            }
        }

        internal void SetBarang(Barang barang)
        {
            this.Count = 1;
            this.BarangKategoris = barang.BarangKategoris;
            this.Berat = barang.Berat;
            this.Fotoes = barang.Fotoes;
            this.Harga = barang.Harga;
            this.Id = barang.Id;
            this.Nama = barang.Nama;
            this.Satuan = barang.Satuan;
            this.Stock = barang.Stock;
        }
        public CartItem()
        {
            Jumlahs = new List<int>()
            {
                1,2,3,4,5
            };
        }

        public List<int> Jumlahs = new List<int>();
        
       

    }
}
