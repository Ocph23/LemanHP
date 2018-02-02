using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LemanWeb.DataModels 
{ 
     [TableName("barang")] 
     public class Barang:BaseNotifyProperty  
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

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("Satuan")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Satuan Satuan 
          { 
               get{return _satuan;} 
               set{ 
                      _satuan=value; 
                     OnPropertyChange("Satuan");
                     }
          } 

          [DbColumn("Berat")] 
          public double Berat 
          { 
               get{return _berat;} 
               set{ 
                      _berat=value; 
                     OnPropertyChange("Berat");
                     }
          } 

          [DbColumn("Harga")] 
          public double Harga 
          { 
               get{return _harga;} 
               set{ 
                      _harga=value; 
                     OnPropertyChange("Harga");
                     }
          } 

          [DbColumn("Stock")] 
          public double Stock 
          { 
               get{return _stock;} 
               set{ 
                      _stock=value; 
                     OnPropertyChange("Stock");
                     }
          } 

          [DbColumn("BarangType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BarangType BarangType 
          { 
               get{return _barangtype;} 
               set{ 
                      _barangtype=value; 
                     OnPropertyChange("BarangType");
                     }
          } 

          [DbColumn("Discount")] 
          public int Discount 
          { 
               get{return _discount;} 
               set{ 
                      _discount=value; 
                     OnPropertyChange("Discount");
                     }
          } 

          [DbColumn("Keterangan")] 
          public string Keterangan 
          { 
               get{return _keterangan;} 
               set{ 
                      _keterangan=value; 
                     OnPropertyChange("Keterangan");
                     }
          } 
       public List<BarangKategori> BarangKategoris { get; set; }
        public IEnumerable<Photo> Fotoes { get; internal set; }
        private int  _id;
           private string  _nama;
           private Satuan  _satuan;
           private double  _berat;
           private double  _harga;
           private double  _stock;
           private BarangType  _barangtype;
           private int  _discount;
           private string  _keterangan;
      }
}


