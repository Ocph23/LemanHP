using LemanWeb.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemanWeb
{
    public class BarangCollection
    {
        public IQueryable<T> Barangs<T>() where T:class
        {
            using (var db = new OcphDbContext())
            {
                if (typeof(T) == typeof(Kain))
                {
                    BarangType ty = BarangType.Kain;
                    var data = from a in db.Barangs.Where(O => O.BarangType == ty).AsQueryable()
                             join fo in db.Fotoes.Select() on a.Id equals fo.BarangId into fotos
                             from fo in fotos.DefaultIfEmpty()
                                 join b in db.Kains.Select() on a.Id equals b.Id
                                 join c in db.BarangKategoris.Select() on a.Id equals c.BarangId into kats
                                 from c in kats.DefaultIfEmpty()
                                 join f in db.Kategories.Select() on c.KategoriId equals f.Id
                                 select new Kain
                                 {
                                     Fotoes =fotos,
                                     BarangKategoris = (from l in kats select new BarangKategori { BarangId = c.BarangId, KategoriId = c.KategoriId, Id = c.Id, Kategori = f }).ToList(),
                                     Bahan = b.Bahan,
                                     BarangType = a.BarangType,
                                     Berat = a.Berat,
                                     Discount = a.Discount,
                                     Harga = a.Harga,
                                     Id = a.Id,
                                     Keterangan = a.Keterangan,
                                     Luntur = b.Luntur,
                                     Motif = b.Motif,
                                     Nama = a.Nama,
                                     Satuan = a.Satuan,
                                     Stock = a.Stock,
                                 };
                    return data as IQueryable<T>;
                }
                else
                {
                    BarangType ty = BarangType.Produk;
                    var data = from a in db.Barangs.Where(O => O.BarangType == ty)
                             join fo in db.Fotoes.Select() on a.Id equals fo.BarangId into fotos
                             from fo in fotos.DefaultIfEmpty()
                             join b in db.Produks.Select() on a.Id equals b.Id
                             join c in db.BarangKategoris.Select() on a.Id equals c.BarangId into kats
                             from c in kats.DefaultIfEmpty()
                             join f in db.Kategories.Select() on c.KategoriId equals f.Id
                             select new Produk
                             {
                                 BarangKategoris = (from l in kats select new BarangKategori { BarangId = c.BarangId, KategoriId = c.KategoriId, Id = c.Id, Kategori = f }).ToList(),
                                  Dimensi=b.Dimensi,
                                 BarangType = a.BarangType,
                                 Berat = a.Berat,
                                 Discount = a.Discount,
                                 Harga = a.Harga,
                                 Id = a.Id,
                                 Keterangan = a.Keterangan,
                                 JenisProdukId=b.JenisProdukId,
                                 Size=b.Size, Warna=b.Warna, 
                                 Nama = a.Nama,
                                 Satuan = a.Satuan,
                                 Stock = a.Stock,
                             };
                    return data as IQueryable<T>;
                }
            }
        }

        internal bool Delete<T>(int id)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (typeof(T) == typeof(Kain))
                    {
                        Kain kain = BarangsById<Kain>(id);
                       
                        foreach (var item in kain.BarangKategoris)
                        {
                            if (item.Id == 0)
                            {
                                db.BarangKategoris.Delete(O => O.Id == item.Id);
                            }
                        }

                        db.Kains.Delete(O => O.Id == id);
                        db.Barangs.Delete(O => O.Id == id);
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        Produk kain = BarangsById<Produk>(id);

                        foreach (var item in kain.BarangKategoris)
                        {
                            if (item.Id == 0)
                            {
                                db.BarangKategoris.Delete(O => O.Id == item.Id);
                            }
                        }

                        db.Produks.Delete(O => O.Id == id);
                        db.Barangs.Delete(O => O.Id == id);
                        trans.Commit();
                        return true;
                    }

                }
                catch (Exception)
                {
                    trans.Rollback();
                }
                return false;
            }
        }

        internal T Insert<T>(T data)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (typeof(T) == typeof(Kain))
                    {
                        Kain kain = data as Kain;
                        Barang b = data as Barang;
                        kain.Id = db.Barangs.InsertAndGetLastID(b);
                       var id= db.Kains.InsertAndGetLastID(kain.GetKainData());
                        foreach (var item in kain.BarangKategoris)
                        {
                            if (item.Id == 0)
                            {
                                item.BarangId = kain.Id;
                                item.Id = db.BarangKategoris.InsertAndGetLastID(item);
                            }
                        }
                        trans.Commit();
                        return data;
                    }
                    else
                    {
                        Produk produk =data as Produk;
                        Barang b = data as Barang;
                        produk.Id = db.Barangs.InsertAndGetLastID(b);
                        db.Produks.InsertAndGetLastID(produk);
                        foreach (var item in produk.BarangKategoris)
                        {
                            if (item.Id == 0)
                            {
                                item.Id = db.BarangKategoris.InsertAndGetLastID(item);
                            }
                        }
                        trans.Commit();
                        return data;
                    }
                    
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
                return data;
            }
           
        }

        internal bool Update<T>(T oldata, T newdata)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (typeof(T) == typeof(Kain))
                    {
                        Kain kain = newdata as Kain;
                        foreach (var item in kain.BarangKategoris)
                        {
                            if (item.Id == 0)
                            {
                                item.Id = db.BarangKategoris.InsertAndGetLastID(item);
                            }
                        }


                        Kain kk = oldata as Kain;

                        foreach (var item in kk.BarangKategoris.ToList())
                        {
                            var r = kain.BarangKategoris.Where(O => O.Id == item.Id).FirstOrDefault();
                            if (r == null)
                            {
                                db.BarangKategoris.Delete(O => O.Id == item.Id);
                            }
                        }

                        kk.Nama = kain.Nama;
                        kk.Bahan = kain.Bahan;
                        kk.Berat = kain.Berat;
                        kk.Discount = kain.Discount;
                        kk.Harga = kain.Harga;
                        kk.Keterangan = kain.Keterangan;
                        kk.Luntur = kain.Luntur;
                        kk.Motif = kain.Motif;
                        kk.Satuan = kain.Satuan;
                        kk.Stock = kain.Stock;

                        Barang b = kk as Barang;

                        db.Barangs.Update(O => new { O.Berat, O.Discount, O.Harga, O.Keterangan, O.Nama, O.Satuan, O.Stock } ,b, O => O.Id == b.Id);
                        db.Kains.Update(O => new { O.Bahan, O.Luntur, O.Motif }, kain.GetKainData(), O => O.Id == kain.Id);
                        trans.Commit();
                        return true;
                    }else
                    {
                        Produk kain = newdata as Produk;
                        foreach (var item in kain.BarangKategoris)
                        {
                            if (item.Id == 0)
                            {
                                item.Id = db.BarangKategoris.InsertAndGetLastID(item);
                            }
                        }


                        Produk kk = oldata as Produk;

                        foreach (var item in kk.BarangKategoris.ToList())
                        {
                            var r = kain.BarangKategoris.Where(O => O.Id == item.Id).FirstOrDefault();
                            if (r == null)
                            {
                                db.BarangKategoris.Delete(O => O.Id == item.Id);
                            }
                        }

                        kk.Nama = kain.Nama;
                        kk.Dimensi = kain.Dimensi;
                        kk.Berat = kain.Berat;
                        kk.Discount = kain.Discount;
                        kk.Harga = kain.Harga;
                        kk.Keterangan = kain.Keterangan;
                        kk.JenisProdukId = kain.JenisProdukId;
                        kk.Size = kain.Size;
                        kk.Warna = kain.Warna;
                        kk.Satuan = kain.Satuan;
                        kk.Stock = kain.Stock;

                        Barang b = kk as Barang;

                        db.Barangs.Update(O => new { O.Berat, O.Discount, O.Harga, O.Keterangan, O.Nama, O.Satuan, O.Stock }, b, O => O.Id == b.Id);
                        db.Produks.Update(O => new { O.Dimensi,O.JenisProdukId,O.Size,O.Warna }, kain, O => O.Id == kain.Id);
                        trans.Commit();
                        return true;
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
                return false;
            }
        }

        public T BarangsById<T>(int Id) where T : class
        {
            using (var db = new OcphDbContext())
            {
                if (typeof(T) == typeof(Kain))
                {
                    BarangType ty = BarangType.Kain;
                    var data = from a in db.Barangs.Where(O => O.BarangType ==ty && O.Id==Id).AsQueryable()
                               join fo in db.Fotoes.Select() on a.Id equals fo.BarangId into fotos
                               from fo in fotos.DefaultIfEmpty()
                               join b in db.Kains.Select() on a.Id equals b.Id
                               join c in db.BarangKategoris.Select() on a.Id equals c.BarangId into kats
                               from c in kats.DefaultIfEmpty()
                               join f in db.Kategories.Select() on c.KategoriId equals f.Id
                               select new Kain
                               {
                                   Fotoes = fotos,
                                   BarangKategoris = (from l in kats select new BarangKategori { BarangId = c.BarangId, KategoriId = c.KategoriId, Id = c.Id, Kategori = f }).ToList(),
                                   Bahan = b.Bahan,
                                   BarangType = a.BarangType,
                                   Berat = a.Berat,
                                   Discount = a.Discount,
                                   Harga = a.Harga,
                                   Id = a.Id,
                                   Keterangan = a.Keterangan,
                                   Luntur = b.Luntur,
                                   Motif = b.Motif,
                                   Nama = a.Nama,
                                   Satuan = a.Satuan,
                                   Stock = a.Stock,
                               };
                    return data.FirstOrDefault() as T;
                }
                else
                {
                    BarangType ty = BarangType.Produk;
                    var data = from a in db.Barangs.Where(O => O.BarangType == ty&& O.Id==Id)
                               join fo in db.Fotoes.Select() on a.Id equals fo.BarangId into fotos
                               from fo in fotos.DefaultIfEmpty()
                               join b in db.Produks.Select() on a.Id equals b.Id
                               join c in db.BarangKategoris.Select() on a.Id equals c.BarangId into kats
                               from c in kats.DefaultIfEmpty()
                               join f in db.Kategories.Select() on c.KategoriId equals f.Id
                               select new Produk
                               {
                                   BarangKategoris = (from l in kats select new BarangKategori { BarangId = c.BarangId, KategoriId = c.KategoriId, Id = c.Id, Kategori = f }).ToList(),
                                   Dimensi = b.Dimensi,
                                   BarangType = a.BarangType,
                                   Berat = a.Berat,
                                   Discount = a.Discount,
                                   Harga = a.Harga,
                                   Id = a.Id,
                                   Keterangan = a.Keterangan,
                                   JenisProdukId = b.JenisProdukId,
                                   Size = b.Size,
                                   Warna = b.Warna,
                                   Nama = a.Nama,
                                   Satuan = a.Satuan,
                                   Stock = a.Stock,
                               };
                    return data.FirstOrDefault() as T;
                }
            }
        }


    }
}