using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LemanWeb.DataModels;
using Microsoft.AspNet.Identity;
using System.Text;

namespace LemanWeb.Api
{
    public class PembeliansController : ApiController
    {
        BarangCollection collection = new BarangCollection();
        PembelianCollection pembelianCollection = new PembelianCollection();
        // GET: api/Pembelians
        public IQueryable<Pembelian> GetPembelians()
        {

            return pembelianCollection.GetPembelian();

        }

        private void ChangeCancelPembelian(Pembelian item)
        {
            item.StatusPembelian = StatusPembelian.Batal;

            using (var db = new OcphDbContext())
            {
                var isUpdated = db.Pembelians.Update(O => new { O.StatusPembelian }, item, O => O.Id == item.Id);
                if (isUpdated)
                    Ok(isUpdated);
                else
                    InternalServerError();
            }
        }

        [ResponseType(typeof(Pembelian))]
        public IHttpActionResult GetPembelianByCode(string id)
        {
            //  Pembelian pembelian = db.Pembelians.Where(O=>O.KodePemesanan==id).FirstOrDefault();
            Pembelian data = pembelianCollection.GetByKodePemesanan(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        // PUT: api/Pembelians/5
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult PutPembelian(int id, Pembelian pembelian)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                   
                    string Message = "";
                    string Title = "";
                    if (id != pembelian.Id)
                    {
                        return BadRequest();
                    }



                    var p = db.Pembelians.Where(O => O.Id == pembelian.Id).FirstOrDefault();
                    Pelanggan pel = db.Pelanggans.Where(O => O.Id == p.PelangganId).FirstOrDefault();
                    if (pembelian.StatusPembelian == StatusPembelian.Sukses && p.StatusPembelian != StatusPembelian.Sukses)
                    {
                        p.Pembayaran.Admin = User.Identity.GetUserName();
                        p.Pembayaran.StatusPembayaran = StatusPembayaran.Lunas;
                        p.StatusPembelian = StatusPembelian.Sukses;
                        Message = string.Format(@"<h1><strong>KONFIRMASI PEMBAYARAN</strong></h1>
                            <p><strong>Terima kasih telah melakukan Pembayaran, kami akan segera memproses pesanan anda</strong></p>
                            <p><strong>dan akan mengirimi anda Nomor Resi Pengiriman</strong></p>
                            <p>&nbsp;</p>
                            <p><strong>Salam,</strong></p>
                            <p><strong>Toko Batik Papua</strong></p>
                            <p>&nbsp;</p>");

                        Title = "Konfirmasi Pembayaran";

                        foreach (var item in p.DetailPembelians)
                        {
                            item.Barang.Stock = item.Barang.Stock - item.Jumlah;

                            db.Barangs.Update(O => new { O.Stock }, item.Barang, O => O.Id == item.Barang.Stock);

                        }

                        db.Pembayarans.Update(O => new { O.Admin, O.StatusPembayaran }, p.Pembayaran, x => x.Id == p.Pembayaran.Id);
                        db.Pembelians.Update(O => new { O.StatusPembelian }, p, x => x.Id == p.Id);

                    }
                    else if (pembelian.StatusPembelian == StatusPembelian.Baru)
                    {
                        p.Pembayaran = null;
                        Message = string.Format(@"<h1><span style='color: #ff0000;'><strong>ULANGI KONFIRMASI PEMBAYARAN</strong></span></h1>
                                            < p > Silahkan Mengulangi Proses Konfirmasi Pembayaran Dengan Benar.</ p >
                                            < p >< strong > Salam,</ strong ></ p >
                                            < p >< strong > Toko Batik Papua </ strong ></ p >
                                            < p > &nbsp;</ p > ");

                        Title = "Ulangi Konfirmasi Pembayaran";
                    }
                    else if (pembelian.StatusPembelian == StatusPembelian.Batal)
                    {
                        p.Pembayaran = null;
                        p.StatusPembelian = StatusPembelian.Batal;
                        Message = string.Format(@"<h1><span style='color: #ff0000;'>PEMBATALAN PEMBELIAN</span></h1>
                                            < p > Maaf, Proses Pembelian Anda telah kami batalkan.Silahkan lakukan pembelian ulang.</ p >
                                            < p > &nbsp;</ p >
                                               < p >< strong > Salam,</ strong ></ p >
                                        < p >< strong > Toko Batik Papua</ strong ></ p > ");

                        Title = "Ulangi Konfirmasi Pembayaran";
                        db.Pembelians.Update(O => new { O.StatusPembelian }, p, x => x.Id == p.Id);
                    }

                    if (pembelian.Pengiriman.NomorResi != null)
                    {
                        p.Pengiriman.NomorResi = pembelian.Pengiriman.NomorResi;
                        p.Pengiriman.TanggalKirim = pembelian.Pengiriman.TanggalKirim;
                        Message += string.Format(@"<h1>INFORMASI PENGIRIMAN</h1>
                                            <p>Kode&nbsp;Pembelian : {0}</p>
                                            <p>Resi Pengiriman : {1}</p>
                                            <p>Tanggal Penggirim : {2}</p>
                                            <p>&nbsp;</p>
                                            <p>&nbsp;</p>
                                            <p><strong>Salam,</strong></p>
                                            <p><strong>Toko Batik Papua</strong></p>
                                            ", p.KodePemesanan, p.Pengiriman.NomorResi, p.Pengiriman.TanggalKirim.Value.ToLongDateString());

                        Title = "RESI PENGIRIMAN";
                        db.Pengirimans.Update(O => new { O.NomorResi, O.TanggalKirim }, p.Pengiriman, x => x.Id == p.Id);
                    }


                    trans.Commit();


                    Helper.SendEmail(pel.Email, Title, Message);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

                return StatusCode(HttpStatusCode.NoContent);
            }

        }

        // POST: api/Pembelians
        [ResponseType(typeof(Pembelian))]
        public IHttpActionResult PostPembelian(Pembelian pembelian)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    pembelian.Id = db.Pembelians.InsertAndGetLastID(pembelian);
                    foreach (var item in pembelian.DetailPembelians)
                    {
                        item.Id = db.DetailPembelians.InsertAndGetLastID(item);
                    }
                    pembelian.Pengiriman.Id = db.Pengirimans.InsertAndGetLastID(pembelian.Pengiriman);

                    trans.Commit();
                    StringBuilder sb = new StringBuilder();
                    Pelanggan pel = db.Pelanggans.Where(x => x.Id == pembelian.PelangganId).FirstOrDefault();

                    string header = @"<h1><strong>BATIK PAPUA</strong></h1>
                              <p>Dear,</p>";

                    sb.Append(header);
                    sb.Append("<p> " + pel.Nama + " </p>");
                    sb.Append("<p>Kode Pesanan : ").Append(pembelian.KodePemesanan).Append("</p>");
                    sb.Append("<p>Tanggal : " + pembelian.Tanggal.ToLongDateString() + " &nbsp;</p>");
                    sb.Append("<p>Terimakasih Telah melakukan pemesanan. Pesanan anda sebagai berikut :&nbsp;</p>");
                    sb.Append("<table style='border: #808080;' border='1'>");
                    sb.Append("<tbody>");
                    sb.Append(@"<tr style='background-color: #bceff5;'>
                        <td>Kode</td>
                        <td>Nama Barang</td>
                        <td>Jumlah</td>
                        <td>Harga</td>
                        <td>Biaya</td>
                        </tr>");
                    foreach (var item in pembelian.DetailPembelians)
                    {
                        Barang b = db.Barangs.Where(O => O.Id == item.BarangId).FirstOrDefault();
                        string nama = "";
                        if (b != null)
                        {
                            nama = b.Nama;

                        }

                        sb.Append(string.Format(@"<tr>
                            <td>{0:D4}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            <td>{4}</td>
                            </tr>", item.Id, nama, item.Jumlah, item.Harga, item.Jumlah * item.Harga));

                    }


                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    var tbiaya = pembelian.DetailPembelians.Sum(O => O.Harga * O.Jumlah);
                    var discount = pembelian.DetailPembelians.Sum(O => (O.Harga * O.Jumlah) * O.Discount / 100);
                    var pengiriman = pembelian.Pengiriman.Berat * pembelian.Pengiriman.Tarif;
                    sb.Append(string.Format(@"
                        <p>Discount : Rp. {0}</p>
                        <p>Biaya Pengiriman : Rp. {1}</p>
                        <p>Kode Validasi Pembayaran : Rp. {2}</p>
                        <p>&nbsp;</p>
                        <p>Total Transfer : Rp. {3} &nbsp;</p>
                            ", discount, pengiriman, pembelian.KodeValidasiPembayaran, (tbiaya - discount + pengiriman + pembelian.KodeValidasiPembayaran)));

                    sb.Append(@"<p>&nbsp;</p>
                    <p>Segera lakukan pembayaran dan pembelian anda akan kami proses.</p>
                    <p>&nbsp;</p>
                    <p>salam,</p>
                    <p>&nbsp;</p>
                    <p>&nbsp;</p>
                    <p>&nbsp;</p>");

                    Helper.SendEmail(pel.Email, "Daftar Pembelian", sb.ToString());
                    return CreatedAtRoute("DefaultApi", new { id = pembelian.Id }, pembelian);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }

            }
        }

        // DELETE: api/Pembelians/5
        [ResponseType(typeof(Pembelian))]
        public IHttpActionResult DeletePembelian(int id)
        {

            using (var db = new OcphDbContext())
            {
                Pembelian pembelian = db.Pembelians.Where(O => O.Id == id).FirstOrDefault();
                db.Pembelians.Delete(O => O.Id == id);
                return Ok(pembelian);
            }
        }


    }
}