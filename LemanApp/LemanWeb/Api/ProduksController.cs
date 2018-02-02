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
    [Authorize(Roles = "Administrator")]
    public class ProduksController : ApiController
    {
        BarangCollection collection = new BarangCollection();
        // GET: api/Produks
        [AllowAnonymous]
        public IQueryable<Produk> GetProduks()
        {
            return collection.Barangs<Produk>().AsQueryable<Produk>();
        }

        // GET: api/Produks/5
        [AllowAnonymous]
        [ResponseType(typeof(Produk))]
        public async Task<IHttpActionResult> GetProduk(int id)
        {

            var data = collection.BarangsById<Produk>(id) as Produk;
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
         
        }

        // PUT: api/Produks/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutProduk(int id, Produk produk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produk.Id)
            {
                return BadRequest();
            }

            try
            {
                var kk = collection.BarangsById<Produk>(id);


                bool result = collection.Update<Produk>(kk, produk);
                return Ok(produk);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        // POST: api/Produks
        [ResponseType(typeof(Produk))]
        [HttpPost]
        public IHttpActionResult PostProduk(Produk produk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Produk data = collection.Insert<Produk>(produk);

            return CreatedAtRoute("DefaultApi", new { id = data.Id }, data);
        }

        // DELETE: api/Produks/5
        [ResponseType(typeof(Produk))]
        public IHttpActionResult DeleteProduk(int id)
        {
            var isDeleted = collection.Delete<Produk>(id);
            if (isDeleted)
                return Ok(isDeleted);
            else
                return InternalServerError(new SystemException("Data Tidak Dapat Dihapus"));
        }

    }
}