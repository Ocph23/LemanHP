
angular.module("app.controller", [])
    .controller("KategoriController", function ($scope, KategoriService) {
        $scope.TambahTitle = "Tambah Kategori";
        $scope.IsNew = true;
        $scope.Kategories = [];
        KategoriService.source().then(function (response) {
            try {
                $scope.Kategories = response.data;
            } catch (e) {
                alert(e.Message);
            }
        });

         
        $scope.Simpan = function(model)
        {
            if ($scope.IsNew)
            {
                KategoriService.post(model).then(
                    function(response)
                    {
                         $scope.Kategories.push(response.data);
                        alert("Data Berhasil Ditambah");
                    }
                );
            } else
            {
                KategoriService.put($scope.model).then(
                    function (response)
                    {
                        $scope.SelectedItem.Nama = model.Nama;
                        $scope.SelectedItem.Keterangan = model.Keterangan;
                        alert("Data Berhasil Diubah");
                        $scope.model = {};
                    }
                );
            }
        }

        $scope.Edit = function(item)
        {
            $scope.SelectedItem = item;
            $scope.TambahTitle = "Edit Kategori";
            $scope.IsNew = false;
            $scope.model = angular.copy(item);
        }


        $scope.DeleteSelected = function (item)
        {
            $scope.ItemToDelete = item;
        }

        $scope.Hapus = function ()
        {
            KategoriService.delete($scope.ItemToDelete).then(function (response) {
                try {
                    var index = $scope.Kategories.indexOf($scope.ItemToDelete);
                    $scope.Kategories.splice(index, 1);
                    alert("Data Berhasil Dihapus");
                } catch (e) {
                    alert(e.Message);
                }
               

            });
        }


    })

    .controller("JenisBarangController", function ($scope, JenisBarangService) {
        $scope.TambahTitle = "Tambah Jenis Barang";
        $scope.IsNew = true;
        $scope.JenisBarangs = [];
        JenisBarangService.source().then(function (response) {
            $scope.JenisBarangs = response.data;
        });


        $scope.Simpan = function (model) {
            if ($scope.IsNew) {
                JenisBarangService.post(model).then(
                    function (response) {
                       $scope.JenisBarangs.push(response.data);
                        alert("Data Berhasil Ditambah");
                    }
                );
            } else {
                JenisBarangService.put($scope.model).then(
                    function (response) {
                        $scope.SelectedItem.nama = model.nama;
                        $scope.SelectedItem.keterangan = model.keterangan;
                        alert("Data Berhasil Diubah");
                        $scope.model = {};
                    }
                );
            }
        }

        $scope.Edit = function (item) {
            $scope.SelectedItem = item;
            $scope.TambahTitle = "Edit Jenis Barang";
            $scope.IsNew = false;
            $scope.model = angular.copy(item);
        }


        $scope.DeleteSelected = function (item) {
            $scope.ItemToDelete = item;
        }

        $scope.Hapus = function () {
            JenisBarangService.delete($scope.ItemToDelete).then(function (response) {
                try {
                    var index = $scope.JenisBarangs.indexOf($scope.ItemToDelete);
                    $scope.JenisBarangs.splice(index, 1);
                    alert("Data Berhasil Dihapus");
                } catch (e) {
                    alert(e.Message);
                }


            });
        }


    })

    .controller("BarangController", function ($scope, BarangService) {
        $scope.TambahTitle = "Tambah Barang";
        $scope.IsNew = true;
        $scope.Barangs = [];
        $scope.Init = function ()
        {
            BarangService.source().then(function (response) {
                $scope.Barangs = response.data;
            });

        }
      

        $scope.Simpan = function (model) {
            if ($scope.IsNew) {
                BarangService.post(model).then(
                    function (response) {
                        $scope.Barangs.push(response.data);
                        alert("Data Berhasil Ditambah");
                    }
                );
            } else {
                BarangService.put($scope.model).then(
                    function (response) {
                        $scope.SelectedItem.Nama = model.Nama;
                        $scope.SelectedItem.Keterangan = model.Keterangan;
                        alert("Data Berhasil Diubah");
                        $scope.model = {};
                    }
                );
            }
        }

        $scope.Edit = function (item) {
            $scope.SelectedItem = item;
            $scope.TambahTitle = "Edit Jenis Barang";
            $scope.IsNew = false;
            $scope.model = angular.copy(item);
        }


        $scope.DeleteSelected = function (item) {
            $scope.ItemToDelete = item;
        }

        $scope.Hapus = function () {
            BarangService.delete($scope.ItemToDelete).then(function (response) {
                try {
                    var index = $scope.Barangs.indexOf($scope.ItemToDelete);
                    $scope.Barangs.splice(index, 1);
                    alert("Data Berhasil Dihapus");
                } catch (e) {
                    alert(e.Message);
                }


            });
        }


    })

    .controller("KainController", function ($scope, KategoriService, KainService, BaseUrl,HelperEnum,PagenationService) {

        $scope.Kategories = [];
        $scope.Kains = [];
        $scope.Kategories = [];
        $scope.model = {};
        $scope.Pagenation = PagenationService;
        $scope.Search = '';
        $scope.Init = function ()
        {
            KainService.source().then(function (response) {
                $scope.Kains = $scope.Pagenation.Load(response.data, $scope.Search, 10);
                KategoriService.source().then(function (response) {
                    angular.forEach(response.data, function (value, key) {
                        value.IsSelect = false;
                        $scope.Kategories.push(value);
                      
                    });

                    $scope.Satuans = HelperEnum.GetSatuans();
                });
               
             
            });
           
        }

        $scope.UploadPhoto = function (item, file) {
            if (file !== undefined) {
            var url = BaseUrl.Url + "/api/Photo";
            var form = new FormData();
            form.append("file", file);
            form.append("BarangId", item.Id);
            var settings = {
                "async": true,
                "crossDomain": true,
                "url": url,
                "method": "Post",
                "headers": {
                    "cache-control": "no-cache",
                },
                "processData": false,
                "contentType": false,
                "mimeType": "multipart/form-data",
                "data": form
            }

            $.ajax(settings).done(function (response, data) {
                alert("Foto Berhasil Ditambahkan");
                item.fotoes.push(data);
            }).error(function (response, data) {
                alert(response.responseText);
                });
            } else
            {
                alert("Anda Belum Memilih File Foto");
            }
        }

        $scope.Simpan = function (model) {
            model.BarangType = "Kain";
            if (model.Id === 0) {
                var kategories = [];
                angular.forEach(model.BarangKategoris, function (value, key) {
                    if (model.Id === 0) { value.Kategori = null; }
                    if (!value.IsSelect) {
                        var index = model.BarangKategoris.indexOf(value);
                        model.BarangKategoris.splice(index, 1);
                    }
                });

                if (model.BarangKategoris.length <= 0) {
                    alert("Pilih Kategori");
                } else {
                    KainService.post(model).then(function (response) {
                        $scope.Kains.push(response.data);
                        alert('Data Berhasil Disimpan');

                    });
                }
              
               
            } else {
                angular.forEach(model.BarangKategoris, function (value1, key1) {
                    if (!value1.IsSelect) {
                        var index = model.BarangKategoris.indexOf(value1);
                        model.BarangKategoris.splice(index, 1);
                    } else if (value1.KategoriId == undefined)
                    {
                        value1.KategoriId = value1.Kategori.Id;
                    }

                    value1.Kategori = null;
                });

                if (model.BarangKategoris.length<=0)
                {
                    alert("Pilih Kategori");
                } else
                {
                    KainService.put(model).then(function (response) {
                        if (response.status===200)
                            alert("Data Berhasi Diubah");
                        else
                        {
                            alert("Data Gagal Diubah");
                        }
                    });
                }
               
            }

           

        }
        $scope.KategoriesTemp = [];
        $scope.Edit = function(item)
        {
            angular.copy(item, $scope.model);
            angular.forEach($scope.Kategories, function (value1, key1) {
                var isfount = false;
                angular.forEach($scope.model.BarangKategoris, function (value, key) {
                    if (value.KategoriId === value1.Id) {
                        isfount = true;
                        value.IsSelect = true;
                        value.Kategori = value1;
                    }
                });
                if (!isfount) {
                    var kat = { IsSelect: false, BarangId: item.Id, Id: 0, Kategori: value1, KategoriId: value1.id };
                    $scope.model.BarangKategoris.push(kat);
                }
            });
        }

        $scope.NewAdd = function()
        {
            $scope.model = {};
            $scope.model.Id = 0;
            var kategories = [];
            angular.forEach($scope.Kategories, function (value, key) {
                var BarangKategori = {};
                BarangKategori.KategoriId = value.Id;
                BarangKategori.IsSelect = false;
                BarangKategori.Kategori = value;
                kategories.push(BarangKategori);
            });
            $scope.model.BarangKategoris = kategories;
        }


        $scope.DeleteSelected = function (item) {
            $scope.ItemToDelete = item;
        }

        $scope.Hapus = function () {
            KainService.delete($scope.ItemToDelete).then(function (response) {
                try {
                    var index = $scope.Kains.indexOf($scope.ItemToDelete);
                    $scope.Kains.splice(index, 1);
                    alert("Data Berhasil Dihapus");
                } catch (e) {
                    alert(e.Message);
                }


            });
        }
    })
  

    .controller("ProdukController", function ($scope, KategoriService, ProdukService, BaseUrl, JenisBarangService, HelperEnum, PagenationService) {
        $scope.model = {};
        $scope.Kategories = [];
        $scope.Produks = [];
        $scope.Kategories = [];
        $scope.Search = '';
        $scope.Pagenation = PagenationService;

        $scope.Init = function () {
            ProdukService.source().then(function (response) {
                $scope.Produks = $scope.Pagenation.Load(response.data, $scope.Search,10);
                KategoriService.source().then(function (response) {
                    $scope.Kategories = response.data;
                    $scope.Satuans = HelperEnum.Satuans;
                    JenisBarangService.source().then(function (response) {
                        $scope.JenisBarangs = response.data;
                    });
                });

            });
        }
        $scope.UploadPhoto = function (item, file) {
            if (file !== undefined) {
                var url = BaseUrl.Url + "/api/Photo";
                var form = new FormData();
                form.append("file", file);
                form.append("BarangId", item.Id);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": url,
                    "method": "Post",
                    "headers": {
                        "cache-control": "no-cache",
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function (response, data) {
                    alert("Foto Berhasil Ditambahkan");
                    item.Fotoes.push(data);
                }).error(function (response, data) {
                    alert(response.responseText);
                });
            } else {
                alert("Anda Belum Memilih File Foto");
            }
        }
        $scope.DeleteSelected = function (item)
        {
            ProdukService.delete(item).then(function (response) {
                var index = $scope.Produks.indexOf(item);
                $scope.Produks.splice(index, 1);
                alert("Data Berhasil Dihapus");
            })
        }


        $scope.Simpan = function (model) {
            angular.forEach(model.BarangKategoris, function (value, key) {
                if (model.Id === 0) { value.Kategori = null; }
                if (!value.IsSelect) {
                    var index = model.BarangKategoris.indexOf(value);
                    model.BarangKategoris.splice(index, 1);
                }
            });

            if (model.Id === 0) {
                model.BarangType = "Produk";
               
                if (model.BarangKategoris.length <= 0) {
                    alert("Pilih Kategori dan Jenis Produk");
                } else if (model.JenisProdukId === 0) {
                    alert("Pilih Jenis Produk");
                } else {
                    ProdukService.post(model).then(function (response) {
                        $scope.Produks.push(response.data);
                        alert("Data berhasil ditambah");
                    });
                };
            } else {
                angular.forEach(model.BarangKategoris, function (value1, key1) {
                    if (!value1.IsSelect) {
                        var index = model.BarangKategoris.indexOf(value1);
                        model.BarangKategoris.splice(index, 1);
                    } else if (value1.KategoriId == undefined) {
                        value1.KategoriId = value1.Kategori.Id;
                    }

                    value1.Kategori = null;
                });
                if (model.JenisProduk != null) {
                    model.JenisProduk = null;
                }
                if (model.BarangKategoris.length <= 0) {
                    alert("Pilih Kategori");
                } else if (model.JenisBarangId === 0) {
                    alert("Pilih Jenis Barang");
                }  else {
                    ProdukService.put(model).then(function (response) {
                        alert("Data berhasil diubah");
                    });
                }
            }
        };

        $scope.Edit = function (item) {
            angular.copy(item, $scope.model);
            angular.forEach($scope.Kategories, function (value1, key1) {
                var isfount = false;
                angular.forEach($scope.model.BarangKategoris, function (value, key) {
                    if (value.KategoriId === value1.Id) {
                        isfount = true;
                        value.IsSelect = true;
                    }
                });
                if (!isfount) {
                    var kat = { IsSelect: false, BarangId: item.Id, id: 0, Kategori: value1, KategoriId: value1.Id };
                    $scope.model.BarangKategoris.push(kat);
                }
            });
        };

        $scope.NewAdd = function () {
            $scope.model = {};
            $scope.model.Id = 0;
            var kategories = [];
            angular.forEach($scope.Kategories, function (value, key) {
                var BarangKategori = {};
                BarangKategori.KategoriId = value.Id;
                BarangKategori.IsSelect = false;
                BarangKategori.Kategori = value;
                kategories.push(BarangKategori);
            });
            $scope.model.BarangKategoris = kategories;
        };

        $scope.FilterByStatus=function(value)
        {
            switch (value) {
                case 1:
                    $scope.Search = "Baru";
                    break;

                default:
                    $scope.Search = "";
            }
        }

    })

    .controller("PembelianController", function ($scope,BaseUrl,HelperEnum,PembelianService) {
        $scope.TambahTitle = "Tambah Kategori";
        $scope.IsNew = true;
        $scope.Kategories = [];
        PembelianService.source().then(function (response) {
            try {
                $scope.Pembelians = response.data;
            } catch (e) {
                alert(e.Message);
            }
        });

        $scope.ValidasiProccess = function (item)
        {
            $scope.ModelValidasi = {};
            $scope.ModelValidasi.Data = item;
            $scope.ModelValidasi.TotalBelanja = 0;
            $scope.ModelValidasi.TotalDiscount = 0;
            angular.forEach(item.DetailPembelians, function (data, key) {
                var biaya = data.Jumlah * data.Harga;
                $scope.ModelValidasi.TotalBelanja += biaya;
                $scope.ModelValidasi.TotalDiscount += (biaya * data.Discount / 100);
            })
            $scope.ModelValidasi.KodeValidasi = item.KodeValidasiPembayaran;
            $scope.ModelValidasi.Pengiriman = item.Pengiriman.Berat * item.Pengiriman.Tarif;
          
            $scope.ModelValidasi.NilaiTransfer = item.Pembayaran.NilaiTransfer;

            $scope.ModelValidasi.TotalBiaya = $scope.ModelValidasi.TotalBelanja + $scope.ModelValidasi.KodeValidasi + $scope.ModelValidasi.Pengiriman - $scope.ModelValidasi.TotalDiscount;
            $scope.ModelValidasi.TotalBiaya = 12333;
            if ($scope.ModelValidasi.TotalBiaya)
            {
                $scope.ModelValidasi.ValueValid = true;
                $scope.ModelValidasi.Message = "Terimakasih Telah Melakukan Pembayaran";
            } else
            {
                $scope.ModelValidasi.ValueValid = false;
                $scope.ModelValidasi.Message = "";
            }

        }

        $scope.Approved = function (item)
        {
            var model = item.Data;
            model.StatusPembelian = "Sukses";
            model.Pembayaran.StatusPembayaran = "Lunas";
            PembelianService.put(model).then(function (response) {
                try {
                    var dataresponse = response.data;
                } catch (e) {
                    alert(e.Message);
                }
            });
        }



        $scope.SetResi=function(item)
        {
            $scope.SelectedItem = item;
        }


        $scope.UpdateResi = function(data)
        {
            var model = $scope.SelectedItem;
            model.Pengiriman.NomorResi = data.NomorResi;
            model.Pengiriman.TanggalKirim = data.TanggalKirim;
            PembelianService.put(model).then(function (response) {
                try {
                    var dataresponse = response.data;
                } catch (e) {
                    alert(e.Message);
                }
            });
        }

        $scope.Pendding = function(item)
        {
            var model = item.Data;
            model.StatusPembelian = "Sukses";
            model.Pembayaran.StatusPembayaran = "Lunas";
            PembelianService.put(model).then(function (response) {
                try {
                    var dataresponse = response.data;
                } catch (e) {
                    alert(e.Message);
                }
            });
        }

        $scope.Cancel = function (item) {

        }

    })

    .controller("LaporanPenjualanController", function ($scope, BaseUrl, HelperEnum, PembelianService) {
        $scope.Penjualan = {};
        $scope.Penjualan.Datas = [];
        $scope.Penjualan.TotalPenjualan = 0;
        $scope.Init = function ()
        {
            PembelianService.source().then(function (response) {
                try {
                    var datas = response.data;
                    angular.forEach(datas, function (res, key) {
                        if (res.StatusPembelian=="Sukses")
                        {
                            angular.forEach(res.DetailPembelians, function (value, k) {
                                var data = {};
                                data.KodeBarang = value.Barang.Id;
                                data.NamaBarang = value.Barang.Nama;
                                data.Jumlah = value.Jumlah;
                                data.Harga = value.Harga;
                                data.Discount = value.Discount;
                                data.Biaya = (data.Jumlah * data.Harga);
                                data.NilaiDiscount = data.Biaya * data.Discount / 100;
                                data.TotalBiaya = data.Biaya - data.NilaiDiscount;
                                $scope.Penjualan.Datas.push(data);
                                $scope.Penjualan.TotalPenjualan += data.TotalBiaya;
                            })
                         
                        }



                    })
                } catch (e) {
                    alert(e.Message);
                }
            });
        }
    })

    .controller("LaporanStokController", function ($scope, BaseUrl, HelperEnum, BarangService) {
        $scope.Datas = [];
        $scope.Init = function () {
            BarangService.source().then(function (response) {
                try {
                    $scope.Datas = response.data;
                } catch (e) {
                    alert(e.Message);
                }
            });
        }
    })
    ;