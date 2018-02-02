using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LemanWeb.DataModels;

namespace LemanWeb.Api
{
    [Authorize]
    public class BarangsController : ApiController
    {

        // GET: api/Barangs
        [Authorize(Roles = "Administrator")]
        public IQueryable<Barang> GetBarangs()
        {

            using (var db = new OcphDbContext())
            {
                return db.Barangs.Select();
            }
        }


        // GET: api/Barangs/5
        [ResponseType(typeof(Barang))]
        [HttpGet]
        public IHttpActionResult GetBarang(int id)
        {

            using (var db = new OcphDbContext())
            {
                Barang barang = db.Barangs.Where(O=>O.Id==id).FirstOrDefault();
                if (barang == null)
                {
                    return NotFound();
                }

                return Ok(barang);
            }
          
        }

        // PUT: api/Barangs/5
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IHttpActionResult PutBarang(int id, Barang barang)
        {

            using (var db = new OcphDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != barang.Id)
                {
                    return BadRequest();
                }

               
                try
                {
                    var isUpdated = db.Barangs.Update(O => new { O.Berat, O.Discount, O.Harga, O.Keterangan, O.Nama, O.Satuan, O.Stock }, barang, O => O.Id == barang.Id);
                    if (isUpdated)
                        return Ok(barang);
                    else
                        throw new SystemException("Data Tidak Berhasil Diubah");
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Barangs
        [ResponseType(typeof(Barang))]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IHttpActionResult PostBarang(Barang barang)
        {
            

            return CreatedAtRoute("DefaultApi", new { id = barang.Id }, barang);
        }

        // DELETE: api/Barangs/5
        [ResponseType(typeof(Barang))]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult DeleteBarang(int id)
        {

            using (var db = new OcphDbContext())
            {
                Barang barang = db.Barangs.Where(O => O.Id == id).FirstOrDefault();
                if (barang == null)
                {
                    return NotFound();
                }

                if (db.Barangs.Delete(O => O.Id == id))
                    return Ok(barang);
                else
                    return StatusCode(HttpStatusCode.NoContent);
            }
            
        }

    }
}