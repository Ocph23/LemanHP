using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("barangkategori")] 
     public class BarangKategori:BaseNotifyProperty  
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

          [DbColumn("KategoriId")] 
          public int KategoriId 
          { 
               get{return _kategoriid;} 
               set{ 
                      _kategoriid=value; 
                     OnPropertyChange("KategoriId");
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

        public Kategori Kategori { get; internal set; }

        private int  _id;
           private int  _kategoriid;
           private int  _barangid;
      }
}


