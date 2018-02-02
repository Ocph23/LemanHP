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

namespace LemanWeb.Api
{
    [Authorize]
    public class PembayaransController : ApiController
    {
        // GET: api/Pembayarans/5
        [ResponseType(typeof(Pembayaran))]
        public IHttpActionResult GetPembayaran(int id)
        {

            using (var db = new OcphDbContext())
            {
                Pembayaran pembayaran = db.Pembayarans.Where(O => O.Id == id).FirstOrDefault();
                if (pembayaran == null)
                {
                    return NotFound();
                }

                return Ok(pembayaran);
            }

        }

        // PUT: api/Pembayarans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPembayaran(int id, Pembayaran pembayaran)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pembayaran.Id)
            {
                return BadRequest();
            }


            try
            {

                using (var db = new OcphDbContext())
                {
                    var isSaved = db.Pembayarans.Update(O => new { O.Admin, O.BankTujuan, O.BuktiTransfer, O.Id, O.NilaiTransfer, O.Pengirim, O.RekeningTujuan, O.StatusPembayaran }, pembayaran, O => O.Id == id);
                    if (isSaved)
                        return Ok(isSaved);
                    else
                        return InternalServerError(new SystemException("Data Tidak Dapat Dihapus"));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

          //  return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pembayarans
        [ResponseType(typeof(Pembayaran))]
        public IHttpActionResult PostPembayaran(Pembayaran pembayaran)
        {
            return Ok();
        }

        // DELETE: api/Pembayarans/5
        [ResponseType(typeof(Pembayaran))]
        public IHttpActionResult DeletePembayaran(int id)
        {

            return Ok(id);
        }

       
    }
}