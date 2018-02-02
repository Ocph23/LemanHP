using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("foto")] 
     public class Photo:BaseNotifyProperty  
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

          [DbColumn("Picture")] 
          public byte[] Picture 
          { 
               get{return _picture;} 
               set{ 
                      _picture=value; 
                     OnPropertyChange("Picture");
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

          private int  _id;
           private byte[]  _picture;
           private int  _barangid;
      }
}


