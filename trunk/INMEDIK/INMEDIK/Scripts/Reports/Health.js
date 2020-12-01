INMEDIKApp.controller('HealthController', function ($scope, $compile, $sce, $http, DTOptionsBuilder, DTColumnBuilder) {
    var dt = this;
    dt.Ready = false;
    dt.LoadUrl = "";
    dt.HealthRows = [];
    dt.errors = [];
    dt.clinics = [];
    dt.categories = [];
    dt.Filters = {};
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
    function pageCount(data, type, full) {
        _cont++;
        return _cont;
    }

    var init = function (url, urlClinics) {
        dt.LoadUrl = url;
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
        mostrarPantallaCarga();
        $http({
            method: 'POST',
            url: GetUrl(),
            data: {
                dateStart: dt.Filters.dateStart ? dt.Filters.dateStart.startOf('day').toDate().toISOString() : null,
                dateEnd: dt.Filters.dateEnd ? dt.Filters.dateEnd.endOf('day').toDate().toISOString() : null,
                clinics: dt.Filters.clinics
            },
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.HealthRows = res.data.data;
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
            quitarPantallaCarga();

        }, function errorCallback(res) {
            console.log("error: " + res);
            quitarPantallaCarga();
        });
    };
    dt.StartController = function (url, urlClinics) {
        if (!dt.Ready) {
            init(url, urlClinics);
        }
    };
    dt.Print = function () {
        var divToPrint = document.getElementById("HealthTable");
        var htmlToPrint = '' +
            '<style type="text/css">' +
            'table { width:100%; }' +
            'table th, table td {' +
            'border:1px solid #000;' +
            '}' +
            '</style>';
        htmlToPrint += divToPrint.outerHTML;
        newWin = window.open("");
        newWin.document.write(htmlToPrint);
        newWin.print();
        newWin.close();
    };
});