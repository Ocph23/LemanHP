angular.module("app.service", [])

    .factory("BaseUrl", function () {
        var service = {};
        service.Url = "http://localhost:56966";
        return service;
    })
    .factory("HelperEnum", function ($http,BaseUrl) {
        var service = {};
        service.Satuans = [];
        $http({
            method: 'GET',
            url: BaseUrl.Url + '/api/HelperEnum'
        }).then(function (response) {
            service.Satuans = response.data.Satuans;
        }, function (error) {
            alert(error.data.message);
        });

        service.GetSatuans = function ()
        {
            return service.Satuans;
        }

        return service;
    })

    .factory("KategoriService", function ($http,$q,BaseUrl) {
        var service = {};
        var Token = { "Content-Type": "application/json" };
        var url =BaseUrl.Url+'/api/Kategoris';
        service.source = function ()
        {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: url,
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert(error.data.message);
                    deferred.reject(error);
            });
            return deferred.promise;
        }

       

        service.put = function(model)
        {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: BaseUrl.Url +'/api/Kategoris/' + model.Id,
                data:model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Diubah");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        service.post = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.Url +'/api/Kategoris',
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Ditambahkan");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: BaseUrl.Url +'/api/Kategoris/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Dihapus");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        return service;
    })

    .factory("BarangService", function ($http, $q, BaseUrl) {
        var service = {};

        service.source = function () {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: BaseUrl.Url +'/api/Barangs'
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan");
                    deferred.reject(error);
            });
            return deferred.promise;
        };


        service.put = function (model) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: BaseUrl.Url +'/api/Barangs/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Diubah");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        service.post = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.Url +'/api/Barangs',
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Ditambah");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'Delete',
                url: BaseUrl.Url +'/api/Barangs/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Dihapus");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        return service;
    })

    .factory("JenisBarangService", function ($http, $q, BaseUrl) {
        var service = {};

        service.source = function () {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: BaseUrl.Url+ '/api/JenisProduks'
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan");
                deferred.reject(error);
            });
            return deferred.promise;
        };


        service.put = function (model) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: BaseUrl.Url+ '/api/JenisProduks/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Diubah");
                deferred.reject(error);
            });
            return deferred.promise;
        }


        service.post = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.Url+ '/api/JenisProduks',
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Ditambahkan");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'Delete',
                url: BaseUrl.Url+ '/api/JenisProduks/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
                }, function (error) {
                    alert("Terjadi Kesalahan, Data Tidak Dapat Dihapus");
                    deferred.reject(error);
            });
            return deferred.promise;
        }


        return service;
    })

    .factory("KainService", function ($http, $q, BaseUrl) {
        var service = {};

         service.source = function () {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: BaseUrl.Url + '/api/Kains'
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan");
                deferred.reject(error);
            });
            return deferred.promise;
        };


        service.put = function (model) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: BaseUrl.Url + '/api/Kains/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak DapatDiubah");
           //     deferred.reject(error);
            });
            return deferred.promise;
        }


        service.post = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.Url + '/api/Kains',
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Ditambah");
                deferred.reject(error);
            });
            return deferred.promise;
        }


        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'Delete',
                url: BaseUrl.Url + '/api/Kains/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Dihapus");
                deferred.reject(error);
            });
            return deferred.promise;
        }


        return service;

    })
    .factory("ProdukService", function ($http, $q, BaseUrl) {
        var service = {};

        service.source = function () {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: BaseUrl.Url + '/api/Produks'
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert(error.data.message);
                deferred.reject(error);
            });
            return deferred.promise;
        };


        service.put = function (model) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: BaseUrl.Url + '/api/Produks/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Diubah");
                deferred.reject(error);
            });
            return deferred.promise;
        }


        service.post = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.Url + '/api/Produks',
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Ditambah");
                deferred.reject(error);
            });
            return deferred.promise;
        }


        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'Delete',
                url: BaseUrl.Url + '/api/Produks/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Dihapus");
            });
            return deferred.promise;
        }


        return service;

    })

    .factory("PembelianService", function ($http, $q, BaseUrl) {
        var service = {};
        var Token = { "Content-Type": "application/json" };
        var url = BaseUrl.Url + '/api/Pembelians';

        service.source = function () {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: url,
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan");
                deferred.reject(error);
            });
            return deferred.promise;
        }



        service.put = function (model) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: BaseUrl.Url + '/api/Pembelians/' + model.Id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat DiUbah");
                deferred.reject(error);
            });
            return deferred.promise;
        }



        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: BaseUrl.Url + '/api/Kategoris/' + model.id,
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert("Terjadi Kesalahan, Data Tidak Dapat Dihapus");
                deferred.reject(error);
            });
            return deferred.promise;
        }


        return service;
    })

    .factory("PagenationService", function ($filter) {

        var service = {};
        service.items = [];
        service.Load = function (items,q,size)
        {
            this.pageSize = size;
            this.items = items;
            this.q = q;
            this.numberOfPages = Math.ceil(this.items.length / this.pageSize);
            return $filter('filter')(this.items, this.q);
        }
        service.currentPage = 0;
        service.pageSize = 10;
        service.q = '';
        service.numberOfPages = 0;


        service.numberOfPagesData = function () {
            var data = [];
            for (var i = 0; i < this.numberOfPages; i++) {
                data.push(i);
            }
            return data;
        }

        return service;

    })
    .filter('startFrom', function () {
        return function (input, start) {
            start = +start; //parse to int
            return input.slice(start);
        }
    })

    .factory("ApprovedPaymentService", function ($http, $q, BaseUrl) {
        var service = {};
        var Token = { "Content-Type": "application/json" };
        var url = BaseUrl.Url + '/api/ApprovedPayment';
               service.post = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.Url + '/api/Kategoris',
                data: model
            }).then(function (data) {
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                deferred.resolve(data);
            }, function (error) {
                alert(error.data.message);
                deferred.reject(error);
            });
            return deferred.promise;
        }

        

        return service;
    })


    ;