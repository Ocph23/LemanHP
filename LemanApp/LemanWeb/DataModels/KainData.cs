using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemanWeb.DataModels
{
    [TableName("kain")]
    public class KainData:BaseNotifyProperty
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("Bahan")]
        public string Bahan
        {
            get { return _bahan; }
            set
            {
                _bahan = value;
                OnPropertyChange("Bahan");
            }
        }

        [DbColumn("Luntur")]
        public string Luntur
        {
            get { return _luntur; }
            set
            {
                _luntur = value;
                OnPropertyChange("Luntur");
            }
        }

        [DbColumn("Motif")]
        public string Motif
        {
            get { return _motif; }
            set
            {
                _motif = value;
                OnPropertyChange("Motif");
            }
        }



        private int _id;
        private string _bahan;
        private string _luntur;
        private string _motif;
    }
}