using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace LemanWeb.DataModels 
{ 
     [TableName("kain")] 
     public class Kain:Barang
    {

          [DbColumn("Bahan")] 
          public string Bahan 
          { 
               get{return _bahan;} 
               set{ 
                      _bahan=value; 
                     OnPropertyChange("Bahan");
                     }
          } 

          [DbColumn("Luntur")] 
          public string Luntur 
          { 
               get{return _luntur;} 
               set{ 
                      _luntur=value; 
                     OnPropertyChange("Luntur");
                     }
          } 

          [DbColumn("Motif")] 
          public string Motif 
          { 
               get{return _motif;} 
               set{ 
                      _motif=value; 
                     OnPropertyChange("Motif");
                     }
          }

            public KainData GetKainData() {
                return new KainData { Bahan = this.Bahan, Id = this.Id, Luntur = this.Luntur, Motif = this.Motif };
            }
           private string  _bahan;
           private string  _luntur;
           private string  _motif;
      }
}


