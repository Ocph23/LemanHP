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
    public class JenisProduksController : ApiController
    {

        // GET: api/JenisProduks

        public IQueryable<JenisProduk> GetJenisProduks()
        {

            using (var db = new OcphDbContext())
            {
                return db.JenisProduks.Select();
            }
           
        }

        // GET: api/JenisProduks/5
        [ResponseType(typeof(JenisProduk))]
        public IHttpActionResult GetJenisProduk(int id)
        {

            using (var db = new OcphDbContext())
            {
                JenisProduk jenisProduk = db.JenisProduks.Where(O=>O.Id==id).FirstOrDefault();
                if (jenisProduk == null)
                {
                    return NotFound();
                }

                return Ok(jenisProduk);
            }
          
        }

        // PUT: api/JenisProduks/5
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public IHttpActionResult PutJenisProduk(int id, JenisProduk jenisProduk)
        {

            using (var db = new OcphDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != jenisProduk.Id)
                {
                    return BadRequest();
                }

                

                try
                {
                    var isUpdate = db.JenisProduks.Update(O =>new { O.Nama, O.Keterangan },
                                    jenisProduk,O => O.Id == jenisProduk.Id);
                    if (isUpdate)
                        return Ok(jenisProduk);
                }
                catch (Exception ex)
                {
                    InternalServerError(ex);
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
           
        }

        // POST: api/JenisProduks
        [ResponseType(typeof(JenisProduk))]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult PostJenisProduk(JenisProduk jenisProduk)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    jenisProduk.Id = db.JenisProduks.InsertAndGetLastID(jenisProduk);
                    if (jenisProduk.Id > 0)
                        return CreatedAtRoute("DefaultApi", new { id = jenisProduk.Id }, jenisProduk);
                    else
                        throw new SystemException("Data Gagal Ditambahkan");


                }
               
            }
            catch (Exception ex)
            {

               return InternalServerError(ex);
            }


        }

        // DELETE: api/JenisProduks/5
        [ResponseType(typeof(JenisProduk))]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult DeleteJenisProduk(int id)
        {
            try
            {

                using (var db = new OcphDbContext())
                {
                    var jenisProduk = db.JenisProduks.Where(O => O.Id == id).FirstOrDefault();

                    if (jenisProduk == null)
                    {
                        return NotFound();
                    }

                    var isDeleted = db.JenisProduks.Delete(O => O.Id == id);
                    if (isDeleted)
                        return Ok(jenisProduk);
                    else
                        throw new SystemException("Data Tidak Dapat Dihapus");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }
        
    }
}