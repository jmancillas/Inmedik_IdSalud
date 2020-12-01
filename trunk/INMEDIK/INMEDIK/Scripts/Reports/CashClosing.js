INMEDIKApp.controller('CashClosingController', function ($scope, $compile, $sce, $http, DTOptionsBuilder, DTColumnBuilder) {
    var dt = this;
    dt.Ready = false;
    dt.LoadUrl = "";
    dt.dtInstance = {};
    dt.medics = [];
    dt.errors = [];
    dt.clinics = [];
    dt.Filters = {};
    dt.ccURLDetail = '';
    dt.detail = true;
    dt.Cierre = {};
    dt.Cierre.denominationByCashCloseAux = [];
    dt.Cierre.expensesAux = [];
    dt.numberWithCommas = function (n) {
        return Ut_numberWithCommas(n);
    }
    var GetUrl = function () {
        return dt.LoadUrl;
    }
    function createdRow(row, data, dataIndex) {
        $compile(angular.element(row).contents())($scope);
    }
    function viewBtn(data, type, full, meta) {
        return '<div class="text-center"><button type="button" ng-click="cc.LoadElement(' + data + ')" class="btn btn-info btn-xs">Ver detalle</button></div>';
    }
    dt.LoadElement = function (id) {
        $http({
            method: 'POST',
            url: dt.ccURLDetail,
            params: {
                "id": id
            },
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {

            if (res.data.success) {
                dt.Cierre = res.data.data;
                dt.Cierre.calcTotalSell = Math.round((dt.Cierre.TotalSell - dt.Cierre.TotalCancelation) * 100) / 100;
                dt.Cierre.logicTotal = Math.round((dt.Cierre.InitialCash + dt.Cierre.TotalSell - dt.Cierre.TotalCancelation - dt.Cierre.TotalWithdrawal) * 100) / 100;
                dt.Cierre.physicalTotal = Math.round((dt.Cierre.TotalCash + dt.Cierre.TotalCrediCard + dt.Cierre.TotalVoucher + dt.Cierre.TotalExpense) * 100) / 100;
                $("#modalCashInfo").modal("show");
            }
            else if (res.data.success == undefined && res.data.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (res.data.errors != undefined && res.data.errors && res.data.errors.length > 0) {
                    dt.errors = res.data.errors;
                }
                else {
                    dt.errors[0] = res.data.message;
                }
                $("#resultModal").modal('show');
            }

        }, function errorCallback(res) {
            console.log("error: " + res);
        });
    };
    dt.Ut_numberWithCommas = Ut_numberWithCommas;
    function currencyFormat(data, type, full, meta) {
        return dt.Ut_numberWithCommas(Math.round(data * 100) / 100);
    }
    dt.clearFilters = function () {
        dt.Filters = {};
        dt.ApplyFilters();
    }
    dt.InitTable = function (url, employeelabel) {
        dt.LoadUrl = url;

        dt.dtOptions = DTOptionsBuilder.newOptions()
                .withOption("ajax", {
                    dataType: "json",
                    type: "POST",
                    url: GetUrl(),
                    data: function (d) {
                        mostrarPantallaCarga();
                        d.filter = {
                            medicId: dt.Filters.medicId,
                            dateStart: dt.Filters.dateStart ? dt.Filters.dateStart.startOf('day').toDate().toISOString() : null,
                            dateEnd: dt.Filters.dateEnd ? dt.Filters.dateEnd.endOf('day').toDate().toISOString() : null,
                            clinics: dt.Filters.clinics
                        };
                    }
                })
                .withDataProp("data")
                .withPaginationType('full_numbers')
                .withOption('order', [0, 'desc'])
                .withOption('serverSide', true)
                .withOption('createdRow', createdRow)
                .withOption('responsive', true)
                .withOption('sDom', '<"top"l>rt<"bottom"ip><"clear">')
                .withOption('lengthMenu', [[50, 100, 200, -1], [50, 100, 200, "Todos"]])
                .withOption("iDisplayLength", 50)
            .withOption('footerCallback', function () {
                quitarPantallaCarga();
            })
                //.withOption('footerCallback', function (row, data, start, end, display) {
                //    var api = this.api();

                //    api.columns('.sum').every(function () {
                //        var sum = this
                //          .data()
                //          .reduce(function (a, b) {
                //              var x = parseFloat(a) || 0;
                //              var y = parseFloat(b) || 0;
                //              return x + y;
                //          }, 0);
                //        console.log(sum); //alert(sum);
                //        $(this.footer()).html(dt.Ut_numberWithCommas(Math.round(sum * 100) / 100));
                //    });
                //})
                .withLanguage({
                    "sEmptyTable": "No existen resultados",
                    "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                    "sInfoEmpty": "Mostrando de 0 a 0 de 0 entradas",
                    "sInfoFiltered": "(Filtrado de un todal de _MAX_ entradas)",
                    "sInfoPostFix": "",
                    "sInfoThousands": ",",
                    "sLengthMenu": "Mostrando _MENU_ entradas",
                    "sLoadingRecords": "Cargando...",
                    "sProcessing": "Procesando...",
                    "sSearch": "Buscar:",
                    "sZeroRecords": "No se encontraron resultados",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Ultimo",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": ascendiente",
                        "sSortDescending": ": descendiente"
                    }
                });

        dt.dtColumns = [
                    DTColumnBuilder.newColumn("number").withTitle("Número"),
                    DTColumnBuilder.newColumn("employeeName").withTitle(employeelabel),
                    DTColumnBuilder.newColumn("efective").withTitle("Efectivo").renderWith(currencyFormat),
                    DTColumnBuilder.newColumn("creditCard").withTitle("Tarjeta de Credito").renderWith(currencyFormat),
                    DTColumnBuilder.newColumn("vales").withTitle("Vales").renderWith(currencyFormat),
                    DTColumnBuilder.newColumn("totalSale").withTitle("Total Venta").renderWith(currencyFormat),
                    DTColumnBuilder.newColumn("dateString").withTitle("Fecha"),
                    DTColumnBuilder.newColumn("soldItems").withTitle("Ventas"),
                    DTColumnBuilder.newColumn("id").withTitle("Acción").renderWith(viewBtn)
        ];

        dt.InstanceCallback = function (instance) {
            // Setup - add a text input to each footer cell
            dt.dtInstance = instance;
        };
    };
    function pageCount(data, type, full) {
        _cont++;
        return _cont;
    }

    var init = function (urlMedics, urlClinics) {
        $http({
            method: 'POST',
            url: urlMedics,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.medics = res.data.data;
                dt.Ready = true;
            }
            else if (res.data.success == undefined && res.data.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (res.data.errors != undefined && res.data.errors && res.data.errors.length > 0) {
                    dt.errors = res.data.errors;
                }
                else {
                    dt.errors[0] = res.data.message;
                }
                $("#resultModal").modal('show');
            }

        }, function errorCallback(res) {
            console.log("error: " + res);
        });
        $http({
            method: 'POST',
            url: urlClinics,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.clinics = res.data.data;
            }
            else if (res.data.success == undefined && res.data.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (res.data.errors != undefined && res.data.errors && res.data.errors.length > 0) {
                    dt.errors = res.data.errors;
                }
                else {
                    dt.errors[0] = res.data.message;
                }
                $("#resultModal").modal('show');
            }

        }, function errorCallback(res) {
            console.log("error: " + res);
        });
    };

    dt.ApplyFilters = function () {
        dt.dtInstance.reloadData();
    };
    dt.StartController = function (urlMedics, urlClinics) {
        if (!dt.Ready) {
            init(urlMedics, urlClinics);
        }
    };
});