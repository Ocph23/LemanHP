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
    using System;
    using System.Collections.Generic;

    public partial class Produk : Barang
    {
        public int JenisProdukId { get; set; }
        public string Warna { get; set; }
        public string Dimensi { get; set; }
        public string Size { get; set; }

        public virtual JenisProduk JenisProduk { get; set; }
    }
}
