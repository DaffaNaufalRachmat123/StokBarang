using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StokBarangApp
{
    public class StokBarang
    {
        public int id { get; set; }
        public string nama_produk { get; set; }
        public string merek_produk { get; set; }
        public int harga_beli { get; set; }
        public int harga_produk { get; set; }
        public int jumlah_produk { get; set; }
        public string tanggal_beli { get; set; }
        public string nama_supplier { get; set; }
        public StokBarang(int id , string nama_produk , string merek_produk , int harga_beli , int harga_produk , int jumlah_produk , string tanggal_beli , string nama_supplier)
        {
            this.id = id;
            this.nama_produk = nama_produk;
            this.merek_produk = merek_produk;
            this.harga_beli = harga_beli;
            this.harga_produk = harga_produk;
            this.jumlah_produk = jumlah_produk;
            this.tanggal_beli = tanggal_beli;
            this.nama_supplier = nama_supplier;
        }
    }
}
