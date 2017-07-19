using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LemanHP.Models
{
    public class Kain : Barang
    {
        private byte[] gambar;

        public string Bahan { get; set; }
        public bool Luntur { get; set; }
        public byte[] Gambar { get {
                if(gambar==null)
                {
                    if (Fotoes != null && Fotoes.Count > 0)
                    {
                        gambar= Fotoes.FirstOrDefault().Picture;
                    }
                }
                return gambar;

            }
            set { SetProperty(ref gambar, value); }
        }
    }
}
