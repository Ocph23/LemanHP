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
    public class KainsController : ApiController
    {
        BarangCollection collection = new BarangCollection();
        // GET: api/Kains
        [HttpGet]
        public IQueryable<Kain> GetKains()
        {
            return collection.Barangs<Kain>().AsQueryable<Kain>();
           
        }

        // GET: api/Kains/5
        [HttpGet]
        [ResponseType(typeof(Kain))]
        public IHttpActionResult GetKain(int id)
        {


            var data = collection.BarangsById<Kain>(id) as Kain;
            if (data ==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
           

        }

        // PUT: api/Kains/5
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKain(int id, Kain kain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kain.Id)
            {
                return BadRequest();
            }

            try
            {
                var kk = collection.BarangsById<Kain>(id);


                bool result = collection.Update<Kain>(kk, kain);
                return Ok(kain);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }
        // POST: api/Kains
        [Authorize(Roles = "Administrator")]
        [ResponseType(typeof(Kain))]
        [HttpPost]
        public IHttpActionResult PostKain(Kain kain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Kain data = collection.Insert<Kain>(kain);

            return CreatedAtRoute("DefaultApi", new { id = data.Id }, data);
        }

        // DELETE: api/Kains/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ResponseType(typeof(Kain))]
        public async Task<IHttpActionResult> DeleteKain(int id)
        {
            var isDeleted = collection.Delete<Kain>(id);
            if (isDeleted)
                return Ok(isDeleted);
            else
                return InternalServerError(new SystemException("Data Tidak Dapat Dihapus"));

        }

      
    }
}