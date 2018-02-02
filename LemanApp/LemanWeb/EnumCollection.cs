using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemanWeb
{
    public enum StatusPembelian : int
    {
        Baru = 0,
        Batal = 1,
        Sukses = 2
    }

    public enum StatusPembayaran : int
    {
        MenungguValidasi = 0,
        Lunas = 1,
        Batal = 2
    }

    public enum Satuan : int
    {
        Meter = 0,
        Pcs = 1
    }
    public enum BarangType : int
    {
        Kain = 0,
        Produk = 1
    }
}