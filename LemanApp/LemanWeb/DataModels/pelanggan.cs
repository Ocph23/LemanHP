using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("pelanggan")] 
     public class Pelanggan:BaseNotifyProperty  
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

          [DbColumn("Email")] 
          public string Email 
          { 
               get{return _email;} 
               set{ 
                      _email=value; 
                     OnPropertyChange("Email");
                     }
          } 

          [DbColumn("Telepon")] 
          public string Telepon 
          { 
               get{return _telepon;} 
               set{ 
                      _telepon=value; 
                     OnPropertyChange("Telepon");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
                     }
          } 

          [DbColumn("TanggalDaftar")] 
          public DateTime TanggalDaftar 
          { 
               get{return _tanggaldaftar;} 
               set{ 
                      _tanggaldaftar=value; 
                     OnPropertyChange("TanggalDaftar");
                     }
          } 

          [DbColumn("KodeKota")] 
          public string KodeKota 
          { 
               get{return _kodekota;} 
               set{ 
                      _kodekota=value; 
                     OnPropertyChange("KodeKota");
                     }
          } 

          [DbColumn("UserId")] 
          public string UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
                     }
          }

        public List<Pembelian> Pembelians { get; internal set; }

        private int  _id;
           private string  _nama;
           private string  _email;
           private string  _telepon;
           private string  _alamat;
           private DateTime  _tanggaldaftar;
           private string  _kodekota;
           private string  _userid;
      }
}


