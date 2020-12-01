INMEDIKApp.controller('WaitingTimesController', function ($scope, $compile, $sce, $http, DTOptionsBuilder, DTColumnBuilder) {
    var dt = this;
    dt.Ready = false;
    dt.LoadUrl = "";
    dt.LoadUrlGraph = "";
    dt.TimeRows = [];
    dt.errors = [];
    dt.clinics = [];
    dt.categories = [];
    dt.Filters = {};
    var GetUrl = function () {
        return dt.LoadUrl;
    }
    var GetUrlGraph = function () {
        return dt.LoadUrlGraph;
    }

    
    dt.clearFilters = function () {
        dt.Filters = {};
        dt.ApplyFilters();
    }
    function pageCount(data, type, full) {
        _cont++;
        return _cont;
    }

    //dt.InitChart = function (url) {

    //}

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
                           dateStart: dt.Filters.dateStart ? dt.Filters.dateStart.startOf('day').toDate().toISOString() : null,
                           clinic: dt.Filters.clinic
                       };
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
                       console.log(sum); //alert(sum);
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

                    DTColumnBuilder.newColumn("Id").withTitle("Id").withOption('visible', false),
                    //DTColumnBuilder.newColumn("ConsultId").withTitle("Consulta"),
                    //DTColumnBuilder.newColumn("PatientId").withTitle("Paciente"),
                    DTColumnBuilder.newColumn("FullName").withTitle("Nombre"),
                    //DTColumnBuilder.newColumn("Created").withTitle("Fecha"),
                    //DTColumnBuilder.newColumn("ClinicId").withTitle("Id Clínica"),
                    //DTColumnBuilder.newColumn("Clinic").withTitle("Nombre Clínica"),
                    DTColumnBuilder.newColumn("StartProcess").withTitle("Inicio Proceso"),
                    DTColumnBuilder.newColumn("StartStageReception").withTitle("Inicio Etapa Recepción").notSortable(),
                    DTColumnBuilder.newColumn("EndStageReception").withTitle("Final Etapa Recepción").notSortable(),
                    DTColumnBuilder.newColumn("TAPReception").withTitle("TAP Minutos Recepción").notSortable(),
                    DTColumnBuilder.newColumn("TEPRecNur").withTitle("TEP Minutos Enfermería").notSortable(),
                    DTColumnBuilder.newColumn("StartStageNursery").withTitle("Inicio Etapa Enfermería").notSortable(),
                    DTColumnBuilder.newColumn("EndStageNursery").withTitle("Final Etapa Enfermería").notSortable(),
                    DTColumnBuilder.newColumn("TAPNursery").withTitle("TAP Minutos Enfermería").notSortable(),
                    DTColumnBuilder.newColumn("TEPNurMed").withTitle("TEP Minutos Consulta").notSortable(),
                    DTColumnBuilder.newColumn("StartStageMedic").withTitle("Inicio Etapa Consulta").notSortable(),
                    DTColumnBuilder.newColumn("EndStageMedic").withTitle("Final Etapa Consulta").notSortable(),
                    DTColumnBuilder.newColumn("TAPMedic").withTitle("TAP Minutos Consulta").notSortable(),
                    //DTColumnBuilder.newColumn("EndProcess").withTitle("Final Proceso"),
                    DTColumnBuilder.newColumn("TotalMinutes").withTitle("Tiempo Total Minutos")
        ];

        dt.InstanceCallback = function (instance) {
            // Setup - add a text input to each footer cell
            dt.dtInstance = instance;
        };
    };

    dt.LoadGraph = function (urlGraph) {
        if (urlGraph != undefined) {
            dt.LoadUrlGraph = urlGraph;
        }
        $http({
            method: "POST",
            url: GetUrlGraph(),
            data: {filter: dt.Filters
            }, headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                LabelArray = [];
                for (i = 0; i < res.data.data.LabelsArray.length; i++) {
                    LabelArray.push(res.data.data.LabelsArray[i].Labels);
                }
                RecLimitArray = [];
                RecDataArray = [];
                for (i = 0; i < res.data.data.DataArray.length; i++) {
                    RecDataArray.push(res.data.data.DataArray[i].RecData),
                    RecLimitArray.push(10);
                }
                RecNurLimitArray = [];
                RecNurDataArray = [];
                for (i = 0; i < res.data.data.DataArray.length; i++) {
                    RecNurDataArray.push(res.data.data.DataArray[i].RecNurData),
                    RecNurLimitArray.push(10);
                }
                NurLimitArray = [];
                NurDataArray = [];
                for (i = 0; i < res.data.data.DataArray.length; i++) {
                    NurDataArray.push(res.data.data.DataArray[i].NurData),
                    NurLimitArray.push(10);
                }
                NurMedLimitArray = [];
                NurMedDataArray = [];
                for (i = 0; i < res.data.data.DataArray.length; i++) {
                    NurMedDataArray.push(res.data.data.DataArray[i].NurMedData)
                    NurMedLimitArray.push(15);
                }
                MedLimitArray = [];
                MedDataArray = [];
                for (i = 0; i < res.data.data.DataArray.length; i++) {
                    MedDataArray.push(res.data.data.DataArray[i].MedData)
                    MedLimitArray.push(20);
                }
                $scope.labels = LabelArray;
                $scope.series = ['TAP', 'Tiempo Límite'];
                $scope.colours = [{
                    fill: false,
                    backgroundColor: "#cb2431"
                }]
                    
                $scope.data = [RecDataArray, RecLimitArray];
                $scope.onClick = function (points, evt) {
                    console.log(points, evt);
                };
                $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
                $scope.options = {
                    color: '#cb2431',
                    scales: {
                        yAxes: [
                          {
                              id: 'y-axis-1',
                              type: 'linear',
                              position: 'left',
                              ticks: {
                                  beginAtZero: true,
                                  min: 0,
                                  max: 30
                              }
                          },
                          {
                              id: 'y-axis-2',
                              type: 'linear',
                              display: false,
                              position: 'right',
                              ticks: {
                                  min: 0,
                                  max: 30
                              }
                          }
                        ]
                    }
                };
                $scope.labels = LabelArray;
                $scope.series1 = ['TEP', 'Tiempo Límite'];
                $scope.colours1 = [{
                    fill: false,
                    backgroundColor: "#F7464A"
                }]

                $scope.data1 = [RecNurDataArray, RecNurLimitArray];
                $scope.onClick = function (points, evt) {
                    console.log(points, evt);
                };
                $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
                $scope.options1 = {
                    scales: {
                        yAxes: [
                          {
                              type: 'linear',
                              position: 'left',
                              ticks: {
                                  beginAtZero: true,
                                  min: 0,
                                  max: 60
                              }
                          },
                          {
                              id: 'y-axis-2',
                              type: 'linear',
                              display: false,
                              position: 'right',
                              ticks: {
                                  min: 0,
                                  max: 30
                              }
                          }

                        ]
                    }
                };
                $scope.labels = LabelArray;
                $scope.series2 = ['TAP', 'Tiempo Límite'];
                $scope.colours = [{
                    fill: false,
                    backgroundColor: "#46BFBD"
                }]

                $scope.data2 = [NurDataArray, NurLimitArray];
                $scope.onClick = function (points, evt) {
                    console.log(points, evt);
                };
                $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
                $scope.options2 = {
                    scales: {
                        yAxes: [
                          {
                              type: 'linear',
                              position: 'left',
                              ticks: {
                                  beginAtZero: true,
                                  min: 0,
                                  max: 30
                              }
                          },
                          {
                              id: 'y-axis-2',
                              type: 'linear',
                              display: false,
                              position: 'right',
                              ticks: {
                                  min: 0,
                                  max: 30
                              }
                          }

                        ]
                    }
                };
                $scope.labels = LabelArray;
                $scope.series3 = ['TEP', 'Tiempo Límite'];
                $scope.colours = [{
                    fill: false,
                    backgroundColor: "#FDB45C"
                }]

                $scope.data3 = [NurMedDataArray, NurMedLimitArray];
                $scope.onClick = function (points, evt) {
                    console.log(points, evt);
                };
                $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
                $scope.options3 = {
                    scales: {
                        yAxes: [
                          {
                              type: 'linear',
                              position: 'left',
                              ticks: {
                                  beginAtZero: true,
                                  min: 0,
                                  max: 60
                              }
                          },
                          {
                              id: 'y-axis-2',
                              type: 'linear',
                              display: false,
                              position: 'right',
                              ticks: {
                                  min: 0,
                                  max: 30
                              }
                          }

                        ]
                    }
                };
                $scope.labels = LabelArray;
                $scope.series4 = ['TAP', 'Tiempo Límite'];
                $scope.colours = [{
                    fill: false,
                    backgroundColor: "#4D5360"
                }]

                $scope.data4 = [MedDataArray, MedLimitArray];
                $scope.onClick = function (points, evt) {
                    console.log(points, evt);
                };
                $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
                $scope.options4 = {
                    scales: {
                        yAxes: [
                          {
                              type: 'linear',
                              position: 'left',
                              ticks: {
                                  beginAtZero: true,
                                  min: 0,
                                  max: 30
                              }
                          },
                          {
                              id: 'y-axis-2',
                              type: 'linear',
                              display: false,
                              position: 'right',
                              ticks: {
                                  min: 0,
                                  max: 30
                              }
                          }

                        ]
                    }
                };

            }
        }, function errorCallback(res) {
            console.log("error: " + res);
        });
    }
       


    var init = function (urlClinics, urlGraph) {
        $http({
            method: 'POST',
            url: urlClinics,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {

            if (res.data.success) {
                dt.clini = res.data.data;
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
        dt.LoadGraph(urlGraph);
    };
    dt.ApplyFilters = function () {
        dt.dtInstance.reloadData();
        console.log(dt.Filters);
    };
    dt.StartController = function (urlClinics,urlGraph) {
        if (!dt.Ready) {
            init(urlClinics,urlGraph);
        }
    };
});