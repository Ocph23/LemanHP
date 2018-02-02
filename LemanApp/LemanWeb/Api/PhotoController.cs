using LemanWeb.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace LemanWeb.Api
{

    [Authorize(Roles = "Administrator")]
    public class PhotoController : ApiController
    {
      //  [ResponseType(typeof(Foto))]
        [HttpPost]
       
        public async Task<HttpResponseMessage> Post()
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    if (!Request.Content.IsMimeMultipartContent())
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        "This request is not properly formatted"));
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    var p = new Photo();

                    foreach (HttpContent ctnt in provider.Contents)
                    {
                        var name = ctnt.Headers.ContentDisposition.Name;
                        var field = name.Substring(1, name.Length - 2);
                        if (field == "file")
                        {
                            //now read individual part into STREAM
                            var stream = await ctnt.ReadAsStreamAsync();

                            byte[] data = new byte[stream.Length];

                            if (stream.Length != 0)
                            {
                                await stream.ReadAsync(data, 0, (int)stream.Length);
                                p.Picture = Helper.ResizeImage(data, 150);
                            }
                        }
                        else if (field == "BarangId")
                        {
                            p.BarangId = Convert.ToInt32(await ctnt.ReadAsStringAsync());
                        }
                    }
                   p.Id= db.Fotoes.InsertAndGetLastID(p);
                    return Request.CreateResponse(HttpStatusCode.OK, p);
                }
              

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }


        
    }




}
