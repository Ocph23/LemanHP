using LemanHP.ViewModels.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Models
{
    public class PembelianView:BaseDataObject
    {
        private Pembelian _pembelian;
        public List<PembayaranItem> Datas { get; set; }
        public Pembelian pembelian {
            get { return _pembelian; }
            set
            {
                SetProperty(ref _pembelian, value);
            }
        }

        public string Nama { get; private set; }
        public string KodePesanan { get; private set; }
        public DateTime Tanggal { get; private set; }
        public double TotalDiscount { get; private set; }
        public double TotalBiaya { get; private set; }
        public double KodeValidasiPembayaran { get; private set; }
        public double BiayaKirim { get; private set; }
        public double TotalTrasfer { get; private set; }

        public PembelianView(Pembelian pembelian,Pelanggan pelanggan)
        {
            this.pembelian = pembelian;
            this.Datas = new List<PembayaranItem>();
            this.Nama = pelanggan.Nama;
            this.KodePesanan = pembelian.KodePemesanan;
            this.Tanggal = pembelian.Tanggal;

            foreach(var item in pembelian.DetailPembelians)
            {
                var data = new PembayaranItem();
                data.Count = Convert.ToInt32(item.Jumlah);
                data.disc= (item.Harga * data.Count)*item.Discount/100;
                data.Harga = item.Harga;
                data.Kode = item.Barang.Id;
                data.Berat = item.Barang.Berat;
                data.Nama = item.Barang.Nama;
                Datas.Add(data);
            }

            this.TotalDiscount = Datas.Sum(O => O.disc);
            this.TotalBiaya = Datas.Sum(O => O.Biaya);
            this.KodeValidasiPembayaran = pembelian.KodeValidasiPembayaran;
            this.BiayaKirim = pembelian.Pengiriman.Berat * pembelian.Pengiriman.Tarif;
            this.TotalTrasfer = TotalBiaya - TotalDiscount+BiayaKirim+KodeValidasiPembayaran; 



        }
    }
}
