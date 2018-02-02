using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("produk")] 
     public class Produk:Barang
   {
          [DbColumn("JenisProdukId")] 
          public int JenisProdukId 
          { 
               get{return _jenisprodukid;} 
               set{ 
                      _jenisprodukid=value; 
                     OnPropertyChange("JenisProdukId");
                     }
          } 

          [DbColumn("Warna")] 
          public string Warna 
          { 
               get{return _warna;} 
               set{ 
                      _warna=value; 
                     OnPropertyChange("Warna");
                     }
          } 

          [DbColumn("Dimensi")] 
          public string Dimensi 
          { 
               get{return _dimensi;} 
               set{ 
                      _dimensi=value; 
                     OnPropertyChange("Dimensi");
                     }
          } 

          [DbColumn("Size")] 
          public string Size 
          { 
               get{return _size;} 
               set{ 
                      _size=value; 
                     OnPropertyChange("Size");
                     }
          } 

          private int  _id;
           private int  _jenisprodukid;
           private string  _warna;
           private string  _dimensi;
           private string  _size;
      }
}


