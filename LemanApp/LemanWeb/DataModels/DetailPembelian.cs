using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("detailpembelian")] 
     public class DetailPembelian:BaseNotifyProperty  
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

          [DbColumn("Discount")] 
          public double Discount 
          { 
               get{return _discount;} 
               set{ 
                      _discount=value; 
                     OnPropertyChange("Discount");
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

          [DbColumn("Jumlah")] 
          public double Jumlah 
          { 
               get{return _jumlah;} 
               set{ 
                      _jumlah=value; 
                     OnPropertyChange("Jumlah");
                     }
          } 

          [DbColumn("PembelianId")] 
          public int PembelianId 
          { 
               get{return _pembelianid;} 
               set{ 
                      _pembelianid=value; 
                     OnPropertyChange("PembelianId");
                     }
          } 

          [DbColumn("BarangId")] 
          public int BarangId 
          { 
               get{return _barangid;} 
               set{ 
                      _barangid=value; 
                     OnPropertyChange("BarangId");
                     }
          }

        public Barang Barang { get; internal set; }

        private int  _id;
           private double  _discount;
           private double  _harga;
           private double  _jumlah;
           private int  _pembelianid;
           private int  _barangid;
      }
}


