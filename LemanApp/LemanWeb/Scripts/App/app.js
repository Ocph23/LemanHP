angular.module("app", ['ngRoute', 'app.service', 'app.controller'])
    .config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "main.htm"
            })
            .when("/kategori", {
                templateUrl: "kategori.htm",
                controller: "KategoriController"
            })
            .when("/kain", {
                templateUrl: "kain.htm",
                controller: "KainController"
            })

            .when("/produk", {
                templateUrl: "produk.htm",
                controller: "ProdukController"
            })
            .when("/addproduk", {
                templateUrl: "addproduk.htm",
                controller: "AddProdukController"
            })


            .when("/barang", {
                templateUrl: "barang.htm",
                controller: "BarangController"
            })
            .when("/jenisbarang", {
                templateUrl: "jenisbarang.htm",
                controller: "JenisBarangController"
            })

            .when("/pembelian", {
                templateUrl: "pembelian.htm",
                controller: "PembelianController"
            })


            .when("/laporanpenjualan", {
                templateUrl: "laporanpenjualan.htm",
                controller: "LaporanPenjualanController"
            })

            .when("/laporanstok", {
                templateUrl: "laporanstok.htm",
                controller: "LaporanStokController"
            })
            ;
    })


    .directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                        var canvas = element.parent().find('img');
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            canvas.attr('src', e.target.result)

                        };
                        reader.readAsDataURL(element[0].files[0]);
                    });
                });
            }
        };
    }])

    .directive('fileCanvas', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileCanvas);
                element.bind('click', function () {
                    var input = element[0].querySelector("input");
                    input.click();
                });


            }
        };
    }])


    .filter('numberLenght', function () {
        return function (n, len) {
            var num = parseInt(n, 10);
            len = parseInt(len, 10);
            if (isNaN(num) || isNaN(len)) {
                return n;
            }
            num = '' + num;
            while (num.length < len) {
                num = '0' + num;
            }
            return num;
        };
    })

   
    ;

