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
using Newtonsoft.Json;

namespace LemanWeb.Api
{
    
    public class KategorisController : ApiController
    {

        // GET: api/Kategoris
        public HttpResponseMessage GetKategoris()
        {

            using (var db = new OcphDbContext())
            {
                var result = db.Kategories.Select().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }   
          
        }

        // GET: api/Kategoris/5
        [ResponseType(typeof(Kategori))]
        [HttpGet]

        public IHttpActionResult GetKategori(int id)
        {

            using (var db = new OcphDbContext())
            {
                Kategori kategori = db.Kategories.Where(o => o.Id == id).FirstOrDefault();
                if (kategori == null)
                {
                    return NotFound();
                }

                return Ok(kategori);
            }

        }

        // PUT: api/Kategoris/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult PutKategori(int id, Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kategori.Id)
            {
                return BadRequest();
            }

            try
            {
                using (var db = new OcphDbContext())
                {
                    db.Kategories.Delete(O => O.Id == id);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kategoris
        [ResponseType(typeof(Kategori))]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult PostKategori(Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (var db = new OcphDbContext())
            {
                kategori.Id = db.Kategories.InsertAndGetLastID(kategori);
            }

            return CreatedAtRoute("DefaultApi", new { id = kategori.Id }, kategori);
        }

        // DELETE: api/Kategoris/5
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Kategori))]
        [HttpDelete]
        public IHttpActionResult DeleteKategori(int id)
        {

            using (var db = new OcphDbContext())
            {
                Kategori kategori = db.Kategories.Where(o => o.Id == id).FirstOrDefault();
                if (kategori == null)
                {
                    return NotFound();
                }
                var isDeleted = db.Kategories.Delete(O => O.Id == id);
                if (isDeleted)
                    return Ok(kategori);
                else
                    return InternalServerError(new SystemException("Data Tidak Dapat Dihapus"));
            }

        }
        
    }
}