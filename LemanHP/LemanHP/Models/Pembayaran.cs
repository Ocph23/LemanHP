//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LemanHP.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;

    public class Pembayaran
    {
        public int Id { get; set; }
        public string BankTujuan { get; set; }
        public string RekeningTujuan { get; set; }
        public double NilaiTransfer { get; set; }
        public byte[] BuktiTransfer { get; set; }
        public System.DateTime TanggalBayar { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusPembayaran StatusPembayaran { get; set; }
        public string Admin { get; set; }
        public string Pengirim { get; set; }
    }
}
