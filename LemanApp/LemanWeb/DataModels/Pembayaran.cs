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
     [TableName("pembayaran")] 
     public class Pembayaran:BaseNotifyProperty  
   {
          [DbColumn("BankTujuan")] 
          public string BankTujuan 
          { 
               get{return _banktujuan;} 
               set{ 
                      _banktujuan=value; 
                     OnPropertyChange("BankTujuan");
                     }
          } 

          [DbColumn("RekeningTujuan")] 
          public string RekeningTujuan 
          { 
               get{return _rekeningtujuan;} 
               set{ 
                      _rekeningtujuan=value; 
                     OnPropertyChange("RekeningTujuan");
                     }
          } 

          [DbColumn("NilaiTransfer")] 
          public double NilaiTransfer 
          { 
               get{return _nilaitransfer;} 
               set{ 
                      _nilaitransfer=value; 
                     OnPropertyChange("NilaiTransfer");
                     }
          } 

          [DbColumn("BuktiTransfer")] 
          public byte[] BuktiTransfer 
          { 
               get{return _buktitransfer;} 
               set{ 
                      _buktitransfer=value; 
                     OnPropertyChange("BuktiTransfer");
                     }
          } 

          [DbColumn("StatusPembayaran")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusPembayaran StatusPembayaran 
          { 
               get{return _statuspembayaran;} 
               set{ 
                      _statuspembayaran=value; 
                     OnPropertyChange("StatusPembayaran");
                     }
          } 

          [DbColumn("Admin")] 
          public string Admin 
          { 
               get{return _admin;} 
               set{ 
                      _admin=value; 
                     OnPropertyChange("Admin");
                     }
          } 

          [DbColumn("Pengirim")] 
          public string Pengirim 
          { 
               get{return _pengirim;} 
               set{ 
                      _pengirim=value; 
                     OnPropertyChange("Pengirim");
                     }
          } 

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

          private string  _banktujuan;
           private string  _rekeningtujuan;
           private double  _nilaitransfer;
           private byte[]  _buktitransfer;
           private StatusPembayaran _statuspembayaran;
           private string  _admin;
           private string  _pengirim;
           private int  _id;
      }
}


