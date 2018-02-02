using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace LemanWeb.DataModels 
{ 
     [TableName("pembelian")] 
     public class Pembelian:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("KodeValidasiPembayaran")] 
          public double KodeValidasiPembayaran 
          { 
               get{return _kodevalidasipembayaran;} 
               set{ 
                      _kodevalidasipembayaran=value; 
                     OnPropertyChange("KodeValidasiPembayaran");
                     }
          } 

          [DbColumn("Tanggal")] 
          public DateTime Tanggal 
          { 
               get{return _tanggal;} 
               set{ 
                      _tanggal=value; 
                     OnPropertyChange("Tanggal");
                     }
          } 

          [DbColumn("StatusPembelian")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusPembelian StatusPembelian 
          { 
               get{return _statuspembelian;} 
               set{ 
                      _statuspembelian=value; 
                     OnPropertyChange("StatusPembelian");
                     }
          } 

          [DbColumn("KodePemesanan")] 
          public string KodePemesanan 
          { 
               get{return _kodepemesanan;} 
               set{ 
                      _kodepemesanan=value; 
                     OnPropertyChange("KodePemesanan");
                     }
          } 

          [DbColumn("PelangganId")] 
          public int PelangganId 
          { 
               get{return _pelangganid;} 
               set{ 
                      _pelangganid=value; 
                     OnPropertyChange("PelangganId");
                     }
          } 
        public Pembayaran Pembayaran { get; set; }
        public Pengiriman Pengiriman { get; set; }
        public IEnumerable<DetailPembelian> DetailPembelians { get; internal set; }

        private int  _id;
           private double  _kodevalidasipembayaran;
           private DateTime  _tanggal;
           private StatusPembelian  _statuspembelian;
           private string  _kodepemesanan;
           private int  _pelangganid;
      }
}


