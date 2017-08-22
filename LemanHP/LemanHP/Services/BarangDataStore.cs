using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using LemanHP.ViewModels;

namespace LemanHP.Services
{
    public class BarangDataStore : BaseViewModel
    {
        public async Task<List<Barang>> GetItemByKategori(Kategori kategori)
        {
            List<Barang> list = new List<Barang>();
            var kains = await KainDataStore.GetItemsAsync();
            foreach(var item in kains)
            {
                foreach(var k in item.BarangKategoris.Where(O=>O.KategoriId==kategori.Id))
                {
                    list.Add((Barang)item);
                }
            }

            var produks = await ProdukDataStore.GetItemsAsync();
            foreach (var item in produks)
            {
                foreach (var k in item.BarangKategoris.Where(O => O.KategoriId == kategori.Id))
                {
                    list.Add((Barang)item);
                }
            }

            return list;
        }

        public async Task< List<Produk> >GetItemsByJenisBarang(JenisProduk jenis)
        {
            List<Produk> list = new List<Produk>();
            var produks = await ProdukDataStore.GetItemsAsync();
            foreach (var item in produks.Where(O=>O.JenisProdukId==jenis.Id))
            {
                list.Add(item);
            }

            return list;
        }
    }
}
