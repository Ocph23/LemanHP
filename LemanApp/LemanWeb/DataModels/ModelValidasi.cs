using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemanWeb.DataModels
{
    public class ModelValidasi
    {
        public int PembelianId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusPembayaran StatusPembayaran { get; set; }
        public string Message { get; set; }
    }
}