using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LemanWeb.DataModels;

namespace LemanWeb
{
    public class PembelianCollection
    {
        private BarangCollection barangcollection = new BarangCollection();

        internal Pembelian GetByKodePemesanan(string id)
        {
            using (var db = new OcphDbContext())
            {
                var data = from a in db.Pembelians.Where(o => o.KodePemesanan == id)
                           join b in db.Pembayarans.Select() on a.Id equals b.Id
                           join c in db.Pengirimans.Select() on a.Id equals c.Id
                           join d in db.DetailPembelians.Select() on a.PelangganId equals d.Id into details
                           from d in details
                           select new Pembelian
                           {
                               DetailPembelians = details,
                               Id = a.Id,
                               KodePemesanan = a.KodePemesanan,
                               KodeValidasiPembayaran = a.KodeValidasiPembayaran,
                               PelangganId = a.PelangganId,
                               Pembayaran = b,
                               Pengiriman = c,
                               StatusPembelian = a.StatusPembelian,
                               Tanggal = a.Tanggal
                           };

                foreach(var item in data)
                {
                    foreach(var det in item.DetailPembelians)
                    {
                        det.Barang = db.Barangs.Where(O => O.Id == det.BarangId).FirstOrDefault();
                    }
                }
                return data.FirstOrDefault();

            }
        }

        internal IQueryable<Pembelian> GetPembelian()
        {
            using (var db = new OcphDbContext())
            {
                var data = from a in db.Pembelians.Select()
                           join b in db.Pembayarans.Select() on a.Id equals b.Id
                           join c in db.Pengirimans.Select() on a.Id equals c.Id
                           join d in db.DetailPembelians.Select() on a.PelangganId equals d.Id into details
                           from d in details
                           select new Pembelian
                           {
                               DetailPembelians = details,
                               Id = a.Id,
                               KodePemesanan = a.KodePemesanan,
                               KodeValidasiPembayaran = a.KodeValidasiPembayaran,
                               PelangganId = a.PelangganId,
                               Pembayaran = b,
                               Pengiriman = c,
                               StatusPembelian = a.StatusPembelian,
                               Tanggal = a.Tanggal
                           };
                return data;

            }
        }
    }
}