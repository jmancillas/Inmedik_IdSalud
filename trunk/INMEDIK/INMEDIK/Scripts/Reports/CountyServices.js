INMEDIKApp.controller('CountyServicesController', function ($scope, $compile, $sce, $http, DTOptionsBuilder, DTColumnBuilder) {
    var dt = this;
    dt.Ready = false;
    dt.LoadUrl = "";
    dt.ConceptsUrl = "";
    dt.dtInstance = {};
    dt.medics = [];
    dt.errors = [];
    dt.Filters = {};
    dt.clinics = [];
    dt.Charts = {};
    dt.chartOptions = {
        scales: {
            xAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        },
    };

    var GetConceptsUrl = function () {
        return dt.ConceptsUrl;
    }

    var GetUrl = function () {
        return dt.LoadUrl;
    }

    dt.Ut_numberWithCommas = Ut_numberWithCommas;
    function currencyFormat(data, type, full, meta) {
        return dt.Ut_numberWithCommas(Math.round(data * 100) / 100);
    }
    dt.clearFilters = function () {
        dt.Filters = {};
        dt.ApplyFilters();
    }
    dt.InitTable = function (url) {
        dt.LoadUrl = url;

        dt.dtOptions = DTOptionsBuilder.newOptions()
            .withOption("ajax", {
                dataType: "json",
                type: "POST",
                url: GetUrl(),
                data: function (d) {
                    mostrarPantallaCarga();
                    d.filter = {
                        concept: dt.Filters.concept,
                        dateStart: dt.Filters.dateStart ? dt.Filters.dateStart.startOf('day').toDate().toISOString() : null,
                        dateEnd: dt.Filters.dateEnd ? dt.Filters.dateEnd.endOf('day').toDate().toISOString() : null,
                        clinics: dt.Filters.clinics
                    };
                },
                complete: function (jqXHR, textStatus) {
                    var data = JSON.parse(jqXHR.responseText).data;
                    dt.Charts = {};
                    $(data).each(function () {
                        if (!dt.Charts[this.conceptId]) {
                            dt.Charts[this.conceptId] = {
                                name: this.conceptName,
                                labels: [],
                                series: ['Cantidad'],
                                countyData: []
                            }
                        }
                        var idx = dt.Charts[this.conceptId].labels.indexOf(this.countyName);
                        if (idx == -1) {
                            idx = dt.Charts[this.conceptId].labels.push(this.countyName);
                            idx = idx - 1;
                            dt.Charts[this.conceptId].countyData.push(0);
                        }
                        dt.Charts[this.conceptId].countyData[idx]++;
                    });
                    $scope.$digest();
                } 
            })
            .withDataProp("data")
            .withPaginationType('full_numbers')
            .withOption('serverSide', true)
            .withOption('responsive', true)
            .withOption('sDom', '<"top"l>rt<"bottom"ip><"clear">')
            .withOption('lengthMenu', [[50, 100, 200, -1], [50, 100, 200, "Todos"]])
            .withOption("iDisplayLength", -1)
            .withOption('footerCallback', function (row, data, start, end, display) {
                var api = this.api();

                api.columns('.sum').every(function () {
                    var sum = this
                        .data()
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0);
                    $(this.footer()).html(dt.Ut_numberWithCommas(Math.round(sum * 100) / 100));
                });
                quitarPantallaCarga();
            })
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
            DTColumnBuilder.newColumn("countyName").withTitle("Departamento"),
            DTColumnBuilder.newColumn("conceptName").withTitle("Concepto"),
            //DTColumnBuilder.newColumn("price").withTitle("Precio").renderWith(currencyFormat).withClass("sum"),
            //DTColumnBuilder.newColumn("cost").withTitle("Costo").renderWith(currencyFormat).withClass("sum"),
            //DTColumnBuilder.newColumn("profit").withTitle("Utilidad").renderWith(currencyFormat).withClass("sum"),
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
    dt.loadConcepts = function (val, cat) {
        return $http.post(GetConceptsUrl(), {
            data: JSON.stringify({ typed: val, CategoryName: cat })
        }).then(function (response) {
            return response.data.data.map(function (item) {
                return item;
            })
        });
    }
    var init = function (urlConcepts, urlClinics) {
        dt.ConceptsUrl = urlConcepts;
        dt.Ready = true;
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
    dt.StartController = function (urlConcepts, urlClinics) {
        if (!dt.Ready) {
            init(urlConcepts, urlClinics);
        }
    };
});