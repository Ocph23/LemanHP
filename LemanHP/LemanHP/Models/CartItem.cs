using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LemanHP.Models
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class CartItem:Barang
    {
        public delegate void SubTotalEvent();
        public delegate void DetailEvent(object sender);
        public delegate void DeleteEvent(CartItem sender);
        private int _count;
        private double _total;



        public event SubTotalEvent OnChangeitem;
        public event DetailEvent OnClickDetail;
        public event DeleteEvent OnDeleteItem;

        public int Count
        {
            get { return _count; }
            set {
                if(value!=0)
                { 
                     SetProperty(ref _count, value);
                   
                    Total = value;
                    OnLog();
                }
            }
        }

        public double Total
        {
            get { return Convert.ToDouble( _count) * Harga; }
            set
            {
                  SetProperty(ref _total, value);
            }
        }

        public Command DetailCommand { get; private set; }
        public Command DeleteCommand { get; private set; }

        internal void SetBarang(Barang barang)
        {
            this.Count = 1;
            this.BarangKategoris = barang.BarangKategoris;
            this.Berat = barang.Berat;
            this.Fotoes = barang.Fotoes;
            this.Harga = barang.Harga;
            this.Id = barang.Id;
            this.Nama = barang.Nama;
            this.Satuan = barang.Satuan;
            this.Stock = barang.Stock;
            this.Discount = barang.Discount;
        }




        public CartItem()
        {
            DetailCommand = new Command((x) => DetailAction(x));
            DeleteCommand = new Command((x) => DeleteAction(x));


            Jumlahs = new List<int>()
            {
                1,2,3,4,5
            };
        }

        private void DeleteAction(object x)
        {
           if(OnDeleteItem!=null)
            {
                OnDeleteItem(this);
            }
        }

        protected void DetailAction(object x)
        {
            if (OnClickDetail != null)
            {
                OnClickDetail((Barang)this);
            }
        }

        public List<int> Jumlahs = new List<int>();


        protected void OnLog()
        {
            if (OnChangeitem != null)
            {
                OnChangeitem();
            }
        }

        

    }
}
