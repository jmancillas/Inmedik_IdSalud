var objectToFormData = function (obj, form, namespace) {

    var fd = form || new FormData();
    var formKey;

    for (var property in obj) {
        if (obj.hasOwnProperty(property) && obj[property] != null && obj[property] !== undefined) {

            if (namespace) {
                formKey = namespace + '[' + property + ']';
            } else {
                formKey = property;
            }

            // if the property is an object, but not a File,
            // use recursivity.
            if (obj[property] instanceof Date) {
                fd.append(formKey, obj[property].toISOString());
            }
            else if (typeof obj[property] === 'object' && !(obj[property] instanceof File)) {

                objectToFormData(obj[property], fd, formKey);

            } else {

                // if it's a string or a File object
                fd.append(formKey, obj[property]);
            }

        }
    }

    return fd;

};
(function () {
    "use strict"
    angular.module("akFileUploader", [])
        .factory("akFileUploaderService", ["$q", "$http",
               function ($q, $http) {
                   var getModelAsFormData = function (data) {
                       var dataAsFormData = new FormData();
                       //angular.forEach(data, function (value, key) {
                       //    dataAsFormData.append(key, value);
                       //});
                       dataAsFormData = objectToFormData(data, dataAsFormData);
                       return dataAsFormData;
                   };

                   var saveModel = function (data, url) {
                       var deferred = $q.defer();
                       $http({
                           url: url,
                           method: "POST",
                           data: getModelAsFormData(data),
                           transformRequest: angular.identity,
                           headers: { 'Content-Type': undefined }
                       }).then(function (result) {
                           deferred.resolve(result);
                       }, function (result, status) {
                           deferred.reject(status);
                       });
                       return deferred.promise;
                   };

                   return {
                       saveModel: saveModel
                   }

               }])
        .directive("akFileModel", ["$parse",
                function ($parse) {
                    return {
                        restrict: "A",
                        link: function (scope, element, attrs) {
                            var model = $parse(attrs.akFileModel);
                            var modelSetter = model.assign;
                            element.bind("change", function () {
                                scope.$apply(function () {
                                    modelSetter(scope, element[0].files[0]);
                                });
                            });
                        }
                    };
                }]);
})(window,document);
