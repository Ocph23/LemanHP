using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("pengiriman")] 
     public class Pengiriman:BaseNotifyProperty  
   {
          [DbColumn("NomorResi")] 
          public string NomorResi 
          { 
               get{return _nomorresi;} 
               set{ 
                      _nomorresi=value; 
                     OnPropertyChange("NomorResi");
                     }
          } 

          [DbColumn("Ekspedisi")] 
          public string Ekspedisi 
          { 
               get{return _ekspedisi;} 
               set{ 
                      _ekspedisi=value; 
                     OnPropertyChange("Ekspedisi");
                     }
          } 

          [DbColumn("Kota")] 
          public string Kota 
          { 
               get{return _kota;} 
               set{ 
                      _kota=value; 
                     OnPropertyChange("Kota");
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

          [DbColumn("Tarif")] 
          public double Tarif 
          { 
               get{return _tarif;} 
               set{ 
                      _tarif=value; 
                     OnPropertyChange("Tarif");
                     }
          } 

          [DbColumn("TanggalKirim")] 
          public DateTime? TanggalKirim 
          { 
               get{return _tanggalkirim;} 
               set{ 
                      _tanggalkirim=value; 
                     OnPropertyChange("TanggalKirim");
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

          private string  _nomorresi;
           private string  _ekspedisi;
           private string  _kota;
           private double  _berat;
           private double  _tarif;
           private DateTime?  _tanggalkirim;
           private int  _id;
      }
}


