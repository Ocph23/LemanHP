using LemanWeb.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LemanWeb.Api
{
    public class HelperEnumController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage HelperEnum()
        {
            var Satuans = Enum.GetValues(typeof(Satuan));
            var obj = new HelperEnum();
            obj.Satuans = new List<string>();
            foreach(var item in Satuans)
            {
                obj.Satuans.Add(item.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }


    }

    internal class HelperEnum
    {
        public HelperEnum()
        {
        }
        public List<string> Satuans { get;  set; }
    }
}
