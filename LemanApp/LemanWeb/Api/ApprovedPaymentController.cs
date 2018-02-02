using LemanWeb.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace LemanWeb.Api
{

  
    public class ApprovedPaymentController : ApiController
    {


        [HttpPut]
        public IHttpActionResult PutPembayaran(int id, ModelValidasi pembayaran)
        {


            using (var db = new OcphDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != pembayaran.PembelianId)
                {
                    return BadRequest();
                }

                try
                {
                    var p = db.Pembayarans.Where(O => O.Id == pembayaran.PembelianId).FirstOrDefault();
                    p.StatusPembayaran = pembayaran.StatusPembayaran;
                    if (p != null)
                    {
                        var updated = db.Pembayarans.Update(O => new { O.StatusPembayaran }, p, O => O.Id == pembayaran.PembelianId);
                        if (updated)
                        {
                            return Ok(p);
                        }
                        else
                        {
                            throw new SystemException();
                        }
                    }
                }
                catch (Exception)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return StatusCode(HttpStatusCode.NoContent);
            }


        }




    }
}
