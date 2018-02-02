using LemanWeb.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace LemanWeb.Api
{
    public class ProfileController : ApiController
    {
        PembelianCollection coll = new PembelianCollection();

        public List<string> Get()
        {
            return new List<string>();
        }

        // GET: api/Profile

        [ResponseType(typeof(Pelanggan))]
        [HttpGet]
        // GET: api/Profile/5
        public IHttpActionResult Get(string email)
        {

            using (var db = new OcphDbContext())
            {
                var userid = User.Identity.GetUserId();
                var resul = db.Pelanggans.Where(O => O.Email.Equals(email)).ToList().FirstOrDefault();
                resul.Pembelians = new List<Pembelian>();
                var datas = db.Pembelians.Where(O => O.PelangganId == resul.Id && O.StatusPembelian== StatusPembelian.Baru).OrderByDescending(O=>O.Id);
                foreach(var item in datas)
                {
                    resul.Pembelians.Add(coll.GetByKodePemesanan(item.KodePemesanan));
                }

                return Ok(resul);
            }

        }
        
    }
}
