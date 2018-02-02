using DAL.DContext;
using DAL.Repository;
using LemanWeb.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LemanWeb
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {

            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public IRepository<Pelanggan> Pelanggans { get { return new Repository<Pelanggan>(this); } }
        public IRepository<Barang> Barangs { get { return new Repository<Barang>(this); } }
        public IRepository<Pembayaran> Pembayarans { get { return new Repository<Pembayaran>(this); } }
        public IRepository<Pengiriman> Pengirimans { get { return new Repository<Pengiriman>(this); } }
        public IRepository<Pembelian> Pembelians { get { return new Repository<Pembelian>(this); } }
        public IRepository<JenisProduk> JenisProduks{ get { return new Repository<JenisProduk>(this); } }
        public IRepository<KainData> Kains { get { return new Repository<KainData>(this); } }
        public IRepository<Produk> Produks { get { return new Repository<Produk>(this); } }
        public IRepository<BarangKategori> BarangKategoris { get { return new Repository<BarangKategori>(this); } }
        public IRepository<Kategori> Kategories{ get { return new Repository<Kategori>(this); } }
        public IRepository<Photo> Fotoes{ get { return new Repository<Photo>(this); } }
        public IRepository<DetailPembelian> DetailPembelians{ get { return new Repository<DetailPembelian>(this); } }



        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }



        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}