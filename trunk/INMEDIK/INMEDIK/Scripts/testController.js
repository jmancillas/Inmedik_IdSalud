var INMEDIKApp = angular.module('INMEDIKApp', ["ui.bootstrap", "ui.bootstrap.tpls", 'datatables', 'ui.sortable']);
INMEDIKApp.controller('TestController', function ($scope, $compile, $sce, $http, DTOptionsBuilder, DTColumnBuilder) {
    var dt = this;
    dt.toDelete = {};
    dt.submittingElement = false;
    dt.submittingForm = false;
    dt.errors = [];
    dt.creating = false;
    dt.fieldTypes = [];
    dt.currentElementKind = "module";
    dt.FieldFormTouchedAndInvalid = false;
    dt.ModuleFormTouchedAndInvalid = false;
    dt.creatingModule = false;
    dt.creatingField = false;
    dt.newElement = {
        field: {
            fieldOptionAux: []
        },
        module: {
            fieldList: []
        }
    };

    dt.form = {
        elementList: []
    };
    dt.LoadUrlForms = "";
    dt.LoadUrlFields = "";
    dt.LoadUrlModules = "";
    var GetUrlForms = function () {
        return dt.LoadUrlForms;
    }
    var GetUrlFields = function () {
        return dt.LoadUrlFields;
    }
    var GetUrlModules = function () {
        return dt.LoadUrlModules;
    }

    dt.LoadUrlGetForm = "";
    dt.LoadUrlGetField = "";
    dt.LoadUrlGetModule = "";
    var GetUrlGetForm = function () {
        return dt.LoadUrlGetForm;
    }
    var GetUrlGetField = function () {
        return dt.LoadUrlGetField;
    }
    var GetUrlGetModule = function () {
        return dt.LoadUrlGetModule;
    }

    dt.SaveFormUrl = "";
    dt.SaveModuleUrl = "";
    dt.SaveFieldUrl = "";
    var GetUrlSaveForm = function () {
        return dt.SaveFormUrl;
    };
    var GetUrlSaveModule = function () {
        return dt.SaveModuleUrl;
    };
    var GetUrlSaveField = function () {
        return dt.SaveFieldUrl;
    };

    dt.DeleteFormUrl = "";
    dt.DeleteModuleUrl = "";
    dt.DeleteFieldUrl = "";
    var GetUrlDeleteForm = function () {
        return dt.DeleteFormUrl;
    };
    var GetUrlDeleteModule = function () {
        return dt.DeleteModuleUrl;
    };
    var GetUrlDeleteField = function () {
        return dt.DeleteFieldUrl;
    };

    dt.SetUrls = function
        (formUrl, moduleUrl, fieldUrl,
        getFormUrl, getModuleUrl, getFieldUrl,
        deleteFormUrl, deleteModuleUrl, deleteFieldUrl) {
        dt.SaveFormUrl = formUrl;
        dt.SaveModuleUrl = moduleUrl;
        dt.SaveFieldUrl = fieldUrl;

        dt.LoadUrlGetField = getFieldUrl;
        dt.LoadUrlGetModule = getModuleUrl;
        dt.LoadUrlGetForm = getFormUrl;

        dt.DeleteFormUrl = deleteFormUrl;
        dt.DeleteModuleUrl = deleteModuleUrl;
        dt.DeleteFieldUrl = deleteFieldUrl;
    };
    dt.removeOption = function ($index) {
        dt.newElement.field.fieldOptionAux.splice($index, 1);
    };
    dt.GetTouchedField = function ($event) {
        dt.FieldFormTouchedAndInvalid = false;
        var elem = $event.target;
        $("#FieldForm input").each(function () {
            if ($(this).hasClass("ng-invalid") && ($(this).hasClass("ng-touched") || this == elem)) {
                dt.FieldFormTouchedAndInvalid = true;
                return false;
            }
        });
        if (dt.FieldFormTouchedAndInvalid) {
            return;
        }
        $("#FieldForm select").each(function () {
            if ($(this).hasClass("ng-invalid") && ($(this).hasClass("ng-touched") || this == elem)) {
                dt.FieldFormTouchedAndInvalid = true;
                return false;
            }
        });
        if (dt.FieldFormTouchedAndInvalid) {
            return;
        }
        $("#FieldForm textarea").each(function () {
            if ($(this).hasClass("ng-invalid") && ($(this).hasClass("ng-touched") || this == elem)) {
                dt.FieldFormTouchedAndInvalid = true;
                return false;
            }
        });
        if (dt.FieldFormTouchedAndInvalid) {
            return;
        }
    };
    dt.GetTouchedModule = function ($event) {
        dt.ModuleFormTouchedAndInvalid = false;
        var elem = $event.target;
        $("#ModuleForm input").each(function () {
            if ($(this).hasClass("ng-invalid") && ($(this).hasClass("ng-touched") || this == elem)) {
                dt.ModuleFormTouchedAndInvalid = true;
                return false;
            }
        });
        if (dt.ModuleFormTouchedAndInvalid) {
            return;
        }
        $("#ModuleForm select").each(function () {
            if ($(this).hasClass("ng-invalid") && ($(this).hasClass("ng-touched") || this == elem)) {
                dt.ModuleFormTouchedAndInvalid = true;
                return false;
            }
        });
        if (dt.ModuleFormTouchedAndInvalid) {
            return;
        }
        $("#ModuleForm textarea").each(function () {
            if ($(this).hasClass("ng-invalid") && ($(this).hasClass("ng-touched") || this == elem)) {
                dt.ModuleFormTouchedAndInvalid = true;
                return false;
            }
        });
        if (dt.ModuleFormTouchedAndInvalid) {
            return;
        }
    };

    var init = function (urlfieldTypes) {
        $http({
            method: 'POST',
            url: urlfieldTypes,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.fieldTypes = res.data.data;
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
    }

    dt.StartController = function (urlfieldTypes) {
        init(urlfieldTypes);
    };
    /*-------------------------------------------Table forms-----------------------------------------------*/
    dt.InitTableForms = function (url) {
        dt.LoadUrlForms = url;

        dt.dtOptions = DTOptionsBuilder.newOptions()
            .withOption("ajax", {
                dataType: "json",
                type: "POST",
                url: GetUrlForms(),
            })
        .withDataProp("data")
        .withPaginationType('full_numbers')
        .withOption('infoCallback', function (settings) {
        })
        .withOption('createdRow', createdRow)
        .withOption('serverSide', true)
        .withOption('sDom', '<"top"l>rt<"bottom"ip><"clear">')
        .withLanguage({
            "sEmptyTable": "No existen registros",
            "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
            "sInfoEmpty": "Mostrando de 0 a 0 de 0 entradas",
            "sInfoFiltered": "(Filtrado de un todal de _MAX_ entradas)",
            "sInfoPostFix": "",
            "sInfoThousands": ",",
            "sLengthMenu": "Mostrando _MENU_ entradas",
            "sLoadingRecords": "Cargando...",
            "sProcessing": "Procesando...",
            "sSearch": "Buscar:",
            "sZeroRecords": "No se encontraron registros",
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

        function createdRow(row, data, dataIndex) {
            $compile(angular.element(row).contents())($scope);
        }
        function detalleBtn(data, type, full, meta) {
            return '<div class="text-center"><button type="button" style="width:60%!important;" ng-click="INMEDIKApp.EditForm(' + data + ')" class="btn btn-default btn-sm">Editar</button>'+
            '<button type="button" style="width:60%!important;" ng-click="INMEDIKApp.DeleteForm(' + data + ')" class="btn btn-danger btn-sm">Eliminar</button>'+
            '</div>';
        }


        dt.dtColumns = [
            DTColumnBuilder.newColumn("name").withTitle("Nombre"),
            DTColumnBuilder.newColumn("description").withTitle("Descripción"),
            DTColumnBuilder.newColumn("fullName").withTitle("Modificado por"),
            DTColumnBuilder.newColumn("sCreated").withTitle("Fecha creación").withClass('notSearchable'),
            DTColumnBuilder.newColumn("sUpdated").withTitle("Fecha modificación").withClass('notSearchable'),
            DTColumnBuilder.newColumn("id").withTitle("Detalle").renderWith(detalleBtn).withClass('notSearchable text-center').notSortable(),
        ];
        dt.InstanceCallback = function (instance) {
            // Setup - add a text input to each footer cell
            dt.dtInstance = instance;
            var id = '#' + dt.dtInstance.id;
            $(id + ' thead th').each(function () {
                var title = $(id + ' thead th').eq($(this).index()).text();
                if (!$(this).hasClass('notSearchable')) {
                    $(this).html('<input type="text" class="form-control" placeholder="' + title + '" />');
                }
            });

            var table = dt.dtInstance.DataTable;
            // Apply the search
            table.columns().every(function () {
                var that = this;

                $('input', this.header()).on('keyup change', function (e) {
                    e.stopPropagation()
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
                $('input', this.header()).on('click', function (e) {
                    e.stopPropagation()
                });
            });
        };

    };
    /*-------------------------------------------END Table forms-----------------------------------------------*/
    /*-------------------------------------------Table modules-----------------------------------------------*/
    dt.InitTableModules = function (url) {
        dt.LoadUrlModules = url;
        dt.dtOptionsMod = DTOptionsBuilder.newOptions()
            .withOption("ajax", {
                dataType: "json",
                type: "POST",
                url: GetUrlModules()
            })
            .withDataProp("data")
            .withPaginationType('full_numbers')
            .withOption('infoCallback', function (settings) {
            })
            .withOption('createdRow', createdRowMod)
            .withOption('lengthMenu', [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]])
            .withOption("iDisplayLength", 5)
            .withOption('serverSide', true)
            .withOption('sDom', '<"top"l>rt<"bottom"ip><"clear">')
            .withLanguage({
                "sEmptyTable": "No existen registros",
                "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                "sInfoEmpty": "Mostrando de 0 a 0 de 0 entradas",
                "sInfoFiltered": "(Filtrado de un todal de _MAX_ entradas)",
                "sInfoPostFix": "",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrando _MENU_ entradas",
                "sLoadingRecords": "Cargando...",
                "sProcessing": "Procesando...",
                "sSearch": "Buscar:",
                "sZeroRecords": "No se encontraron registros",
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
        function createdRowMod(row, data, dataIndex) {
            $compile(angular.element(row).contents())($scope);
        }
        function detalleBtnMod(data, type, full, meta) {
            return '<div class="text-center" style="max-width:70px!important;">' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.AddModule(' + data + ')" class="btn btn-default btn-sm">Agregar</button>' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.EditModule(' + data + ')" class="btn btn-default btn-sm">Editar</button>' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.DeleteModule(' + data + ')" class="btn btn-danger btn-sm">Eliminar</button>' +
            '</div>';
        }

        dt.dtColumnsMod = [
            DTColumnBuilder.newColumn("moduleName").withTitle("Nombre"),
            DTColumnBuilder.newColumn("moduleDescription").withTitle("Descripción"),
            DTColumnBuilder.newColumn("modifiedBy").withTitle("Modificado por"),
            DTColumnBuilder.newColumn("sCreated").withTitle("Fecha creación").withClass('notSearchable'),
            DTColumnBuilder.newColumn("sUpdated").withTitle("Fecha modificación").withClass('notSearchable'),
            DTColumnBuilder.newColumn("id").withTitle("Acción").renderWith(detalleBtnMod).withClass('notSearchable text-center').notSortable(),

        ];
        dt.InstanceCallbackMod = function (instance) {
            // Setup - add a text input to each footer cell
            dt.dtInstanceMod = instance;
            var id = '#' + dt.dtInstanceMod.id;
            $(id + ' thead th').each(function () {
                var title = $(id + ' thead th').eq($(this).index()).text();
                if (!$(this).hasClass('notSearchable')) {
                    $(this).html('<input type="text" class="form-control" placeholder="' + title + '" />');
                }
            });

            var table = dt.dtInstanceMod.DataTable;
            // Apply the search
            table.columns().every(function () {
                var that = this;

                $('input', this.header()).on('keyup change', function (e) {
                    e.stopPropagation()
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
                $('input', this.header()).on('click', function (e) {
                    e.stopPropagation()
                });
            });
        };
    }
    /*-------------------------------------------END Table modules-----------------------------------------------*/
    /*-------------------------------------------Table fields-----------------------------------------------*/
    dt.InitTableFields = function (url) {
        dt.LoadUrlFields = url;
        dt.dtOptionsField = DTOptionsBuilder.newOptions()
            .withOption("ajax", {
                dataType: "json",
                type: "POST",
                url: GetUrlFields(),
            })
            .withDataProp("data")
            .withPaginationType('full_numbers')
            .withOption('infoCallback', function (settings) {
            })
            .withOption('createdRow', createdRowField)
            .withOption('lengthMenu', [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]])
            .withOption("iDisplayLength", 5)
            .withOption('serverSide', true)
            .withOption('sDom', '<"top"l>rt<"bottom"ip><"clear">')
            .withLanguage({
                "sEmptyTable": "No existen registros",
                "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                "sInfoEmpty": "Mostrando de 0 a 0 de 0 entradas",
                "sInfoFiltered": "(Filtrado de un todal de _MAX_ entradas)",
                "sInfoPostFix": "",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrando _MENU_ entradas",
                "sLoadingRecords": "Cargando...",
                "sProcessing": "Procesando...",
                "sSearch": "Buscar:",
                "sZeroRecords": "No se encontraron registros",
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
        function createdRowField(row, data, dataIndex) {
            $compile(angular.element(row).contents())($scope);
        }
        function detalleBtnField(data, type, full, meta) {
            return '<div class="text-center">' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.AddField(' + data + ')" class="btn btn-default btn-sm">Agregar</button>' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.EditField(' + data + ')" class="btn btn-default btn-sm">Editar</button>' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.DeleteField(' + data + ')" class="btn btn-danger btn-sm">Eliminar</button>' +
            '</div>';
        }

        dt.dtColumnsField = [
            DTColumnBuilder.newColumn("fieldName").withTitle("Nombre"),
            DTColumnBuilder.newColumn("fieldDescription").withTitle("Descripción"),
            DTColumnBuilder.newColumn("fieldTypeName").withTitle("Tipo"),
            DTColumnBuilder.newColumn("unit").withTitle("Unidad"),
            DTColumnBuilder.newColumn("modifiedBy").withTitle("Modificado por"),
            DTColumnBuilder.newColumn("sCreated").withTitle("Fecha creación").withClass('notSearchable'),
            DTColumnBuilder.newColumn("sUpdated").withTitle("Fecha modificación").withClass('notSearchable'),
            DTColumnBuilder.newColumn("id").withTitle("Acción").renderWith(detalleBtnField)
                .withClass('notSearchable text-center').notSortable(),
        ];
        dt.InstanceCallbackField = function (instance) {
            // Setup - add a text input to each footer cell
            dt.dtInstanceField = instance;
            var id = '#' + dt.dtInstanceField.id;
            $(id + ' thead th').each(function () {
                var title = $(id + ' thead th').eq($(this).index()).text();
                if (!$(this).hasClass('notSearchable')) {
                    $(this).html('<input type="text" class="form-control" placeholder="' + title + '" />');
                }
            });

            var table = dt.dtInstanceField.DataTable;
            // Apply the search
            table.columns().every(function () {
                var that = this;

                $('input', this.header()).on('keyup change', function (e) {
                    e.stopPropagation()
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
                $('input', this.header()).on('click', function (e) {
                    e.stopPropagation()
                });
            });
        };
        /*----------------------------------------------------------------------------------------------------*/
        function detalleBtnMField(data, type, full, meta) {
            return '<div class="text-center">' +
            '<button type="button" style="width:70px!important;" ng-click="INMEDIKApp.AddFieldtoModule(' + data + ')" class="btn btn-default btn-sm">Agregar</button>' +
            '</div>';
        }
        dt.dtOptionsMField = DTOptionsBuilder.newOptions()
            .withOption("ajax", {
                dataType: "json",
                type: "POST",
                url: GetUrlFields(),
            })
            .withDataProp("data")
            .withPaginationType('full_numbers')
            .withOption('infoCallback', function (settings) {
            })
            .withOption('createdRow', createdRowField)
            .withOption('serverSide', true)
            .withOption('lengthMenu', [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]])
            .withOption("iDisplayLength", 5)
            .withOption('sDom', '<"top"l>rt<"bottom"ip><"clear">')
            .withLanguage({
                "sEmptyTable": "No existen registros",
                "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                "sInfoEmpty": "Mostrando de 0 a 0 de 0 entradas",
                "sInfoFiltered": "(Filtrado de un todal de _MAX_ entradas)",
                "sInfoPostFix": "",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrando _MENU_ entradas",
                "sLoadingRecords": "Cargando...",
                "sProcessing": "Procesando...",
                "sSearch": "Buscar:",
                "sZeroRecords": "No se encontraron registros",
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
        dt.dtColumnsMField = [
            DTColumnBuilder.newColumn("fieldName").withTitle("Nombre"),
            DTColumnBuilder.newColumn("fieldDescription").withTitle("Descripción"),
            DTColumnBuilder.newColumn("fieldTypeName").withTitle("Tipo"),
            DTColumnBuilder.newColumn("unit").withTitle("Unidad"),
            DTColumnBuilder.newColumn("id").withTitle("Acción").renderWith(detalleBtnMField)
                .withClass('notSearchable text-center').notSortable(),
        ];
        dt.InstanceCallbackMField = function (instance) {
            // Setup - add a text input to each footer cell
            dt.dtInstanceMField = instance;
            var id = '#' + dt.dtInstanceMField.id;
            $(id + ' thead th').each(function () {
                var title = $(id + ' thead th').eq($(this).index()).text();
                if (!$(this).hasClass('notSearchable')) {
                    $(this).html('<input type="text" class="form-control" placeholder="' + title + '" />');
                }
            });

            var table = dt.dtInstanceMField.DataTable;
            // Apply the search
            table.columns().every(function () {
                var that = this;

                $('input', this.header()).on('keyup change', function (e) {
                    e.stopPropagation()
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
                $('input', this.header()).on('click', function (e) {
                    e.stopPropagation()
                });
            });
        };
    }
    /*-------------------------------------------END Table fields-----------------------------------------------*/

    dt.SaveForm = function () {
        dt.submittingForm = true;
        $http({
            method: 'POST',
            url: GetUrlSaveForm(),
            data: dt.form,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.dtInstance.reloadData();
                dt.creating = false;
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
        }).finally(function () {
            dt.submittingForm = false;
        });
    };
    dt.EditForm = function (id) {

        $http({
            method: 'POST',
            url: GetUrlGetForm(),
            data: { id: id },
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.form = res.data.data;
                dt.creating = true;

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
    }
    dt.SaveField = function () {
        dt.submittingElement = true;
        $http({
            method: 'POST',
            url: GetUrlSaveField(),
            data: dt.newElement.field,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {

            if (res.data.success) {
                dt.dtInstanceField.reloadData();
                dt.dtInstanceMField.reloadData();
                dt.ResetField();
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
        }).finally(function () {
            dt.submittingElement = false;
        });
    };
    dt.SaveModule = function () {
        dt.submittingElement = true;
        $http({
            method: 'POST',
            url: GetUrlSaveModule(),
            data: dt.newElement.module,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            if (res.data.success) {
                dt.dtInstanceMod.reloadData();
                dt.ResetModule();
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
        }).finally(function () {
            dt.submittingElement = false;
        });
    };

    dt.NewForm = function () {
        dt.creating = true;
        dt.form = {
            elementList: []
        };
    };

    dt.NewElement = function () {
        dt.dtInstanceField.reloadData();
        dt.dtInstanceMField.reloadData();
        dt.dtInstanceMod.reloadData();
        $("#NewElementModal").modal({ backdrop: 'static', keyboard: false });
    };
    dt.ResetForm = function () {
        dt.form = {
            elementList: []
        };
        dt.creating = false;
    };
    dt.ResetElement = function () {
        dt.newElement = {
            field: {
                fieldOptionAux: []
            },
            module: {
                fieldList: []
            }
        };
        $scope.ModuleForm.$setUntouched();
        $scope.FieldForm.$setUntouched();
        dt.ModuleFormTouchedAndInvalid = false;
        dt.FieldFormTouchedAndInvalid = false;
        dt.creatingModule = false;
        dt.creatingField = false;
    };
    dt.ResetField = function () {
        dt.newElement.field = {
            fieldOptionAux: []
        };
        $scope.FieldForm.$setUntouched();
        dt.FieldFormTouchedAndInvalid = false;
        dt.creatingField = false;
    };
    dt.ResetModule = function () {
        $scope.ModuleForm.$setUntouched();
        dt.ModuleFormTouchedAndInvalid = false;
        dt.newElement.module = {
            fieldList: []
        };
        dt.creatingModule = false;
    }
    dt.EditField = function (id) {
        dt.GetField(id, function (fieldData) {
            if (fieldData.success) {
                dt.newElement.field = fieldData.data;
                dt.creatingField = true;

            }
            else if (fieldData.success == undefined && fieldData.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (fieldData.errors != undefined && fieldData.errors && fieldData.errors.length > 0) {
                    dt.errors = fieldData.errors;
                }
                else {
                    dt.errors[0] = fieldData.message;
                }
                $("#resultModal").modal('show');
            }
        });
    }
    dt.AddField = function (id) {
        dt.GetField(id, function (fieldData) {
            if (fieldData.success) {
                dt.form.elementList.push({
                    field: fieldData.data
                });
            }
            else if (fieldData.success == undefined && fieldData.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (fieldData.errors != undefined && fieldData.errors && fieldData.errors.length > 0) {
                    dt.errors = fieldData.errors;
                }
                else {
                    dt.errors[0] = fieldData.message;
                }
                $("#resultModal").modal('show');
            }
        });
    }
    dt.AddFieldtoModule = function (id) {
        dt.GetField(id, function (fieldData) {
            if (fieldData.success) {
                dt.newElement.module.fieldList.push(fieldData.data);
            }
            else if (fieldData.success == undefined && fieldData.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (fieldData.errors != undefined && fieldData.errors && fieldData.errors.length > 0) {
                    dt.errors = fieldData.errors;
                }
                else {
                    dt.errors[0] = fieldData.message;
                }
                $("#resultModal").modal('show');
            }
        });
    }
    dt.GetField = function (id, callback) {
        $http({
            method: 'POST',
            url: GetUrlGetField(),
            data: { id: id },
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            callback(res.data);
        }, function errorCallback(res) {
            console.log("error: " + res);
        });
    }
    dt.EditModule = function (id) {
        dt.GetModule(id, function (moduleData) {
            if (moduleData.success) {
                dt.newElement.module = moduleData.data;
                dt.creatingModule = true;
            }
            else if (moduleData.success == undefined && moduleData.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (moduleData.errors != undefined && moduleData.errors && moduleData.errors.length > 0) {
                    dt.errors = moduleData.errors;
                }
                else {
                    dt.errors[0] = moduleData.message;
                }
                $("#resultModal").modal('show');
            }
        });
    }
    dt.AddModule = function (id) {
        dt.GetModule(id, function (moduleData) {
            if (moduleData.success) {
                dt.form.elementList.push({
                    module: moduleData.data
                });
            }
            else if (moduleData.success == undefined && moduleData.indexOf("SignIn") > -1) {
                dt.errors[0] = "La sesión ha caducado.";
                $("#resultModal").modal('show');
            }
            else {
                if (moduleData.errors != undefined && moduleData.errors && moduleData.errors.length > 0) {
                    dt.errors = moduleData.errors;
                }
                else {
                    dt.errors[0] = moduleData.message;
                }
                $("#resultModal").modal('show');
            }
        });
    }
    dt.GetModule = function (id, callback) {
        $http({
            method: 'POST',
            url: GetUrlGetModule(),
            data: { id: id },
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (res) {
            callback(res.data);
        }, function errorCallback(res) {
            console.log("error: " + res);
        });
    }
    dt.DeleteField = function (id) {
        $("#ConfirmDelete").modal("show");
        dt.toDelete.id = id;
        dt.toDelete.type = 'field'
    }
    dt.DeleteModule = function (id) {
        $("#ConfirmDelete").modal("show");
        dt.toDelete.id = id;
        dt.toDelete.type = 'module'
    }
    dt.DeleteForm = function (id) {
        $("#ConfirmDelete").modal("show");
        dt.toDelete.id = id;
        dt.toDelete.type = 'form'
    }
    dt.ConfirmDelete = function () {
        var deleteUrl = "";
        switch (dt.toDelete.type) {
            case 'field':
                deleteUrl = GetUrlDeleteField();
                break;
            case 'module':
                deleteUrl = GetUrlDeleteModule();
                break;
            case 'form':
                deleteUrl = GetUrlDeleteForm();
                break;
            default:
                deleteUrl = '';
        }
        if (deleteUrl.length > 0) {
            $http({
                method: 'POST',
                url: deleteUrl,
                data: { id: dt.toDelete.id },
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (res) {
                if (res.data.success) {
                    switch (dt.toDelete.type) {
                        case 'field':
                            dt.dtInstanceField.reloadData();
                            dt.dtInstanceMField.reloadData();
                            break;
                        case 'module':
                            dt.dtInstanceMod.reloadData();
                            break;
                        case 'form':
                            dt.dtInstance.reloadData();
                            break;
                        default:
                            console.log('Nothing to reload');
                    }
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
        }
    }

    dt.removeElement = function ($index) {
        dt.form.elementList.splice($index, 1);
    };
    dt.removeField = function ($index) {
        dt.newElement.module.fieldList.splice($index, 1);
    };
    $("#NewElementModal").on("hidden.bs.modal", function () {
        dt.ResetElement();
    });

    dt.sortableOptions = {
        handle: '.sorting-handle'
    };
});