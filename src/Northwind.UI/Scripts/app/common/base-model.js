define(function () {
    var dataSourceCache = {},
        defaultPageSize = 10;

    return function (options) {
        var modelName, displayName, modelId, Model, baseUrl;

        if (typeof options === 'string') {
            options = { modelName: options };
        }
        modelName = options['modelName'];
        displayName = options['displayName'];
        modelId = modelName + 'Id';
        baseUrl = 'api/' + modelName + '/';
        options['id'] = modelId;

        var defaultErrorHandler = function (e) {
            // e = { xhr: xhr, status: status, errorThrown: errorThrown }
            alert(e.xhr.responseText || 'Error happened in ' + modelName + ' data source.');
        };

        Model = kendo.data.Model.define(options);

        Model.modelName = function() {
            return modelName;
        };

        Model.displayName = function() {
            return displayName;
        };

        // The kendo DataSource accepts an array returned from the remote call, an object will be ignored.
        // Normally a WebAPI call returns an object when the operations are GET(by id) and POST.
        //
        // kendo.data.js DataSource->success line 2256:
        // that._data = that._observe(data);
        // DataSource->_observe line 2357:
        // data = new ObservableArray(data, that.reader.model);
        // ObservableArray->init line 86:
        // that.wrapAll(array, that); ** at this point 'array' is a JS object, the function returns an empty ObservableArray.

        // The workaround is to add a 'fields' 
        // attribute to the model class(constructor function). This is probably not the good way to fix the problem...
        //
        // kendo.data.js line 1645 (DataReader)
        // If the model has a fields attribute, the getters variable in init method will be set.
        // function wrapDataAccess line 1608 checks if the getters is empty, if it's not, the data which is an plain JS 
        // object will be converted into an array. 
        // Model.fields = {
        //    modelId: modelId
        // };

        // The better solution is to use a function to return data in schema. See the following code.

        Model.dataSource = function () {
            return dataSourceCache[modelName] || (dataSourceCache[modelName] = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: baseUrl,
                        type: 'GET'
                    },
                    create: {
                        url: baseUrl,
                        type: 'POST'
                    },
                    update: {
                        url: function (model) {
                            return baseUrl + model[modelId];
                        },
                        type: 'PUT'
                    },
                    destroy: {
                        url: function (model) {
                            return baseUrl + model[modelId];
                        },
                        type: 'DELETE'
                    }
                },
                schema: {
                    // GET: { data: [...], total: ... }
                    // POST: { ... }
                    data: function (data) {
                        // Convert the returned object into a array, 
                        // otherwise the DataSource won't pick it up.
                        return $.isArray(data) ? data : data['data'] || [data];
                    },
                    total: function (data) {
                        return $.isArray(data) ? data.length : data['total'] || 1;
                    },
                    model: Model
                },
                serverPaging: options['serverPaging'] !== undefined ? !!options['serverPaging'] : false,
                pageSize: parseInt(options['pageSize']) || defaultPageSize,
                serverFiltering: options['serverFiltering'] !== undefined ? !!options['serverFiltering'] : false,
                serverSorting: options['serverSorting'] !== undefined ? !!options['serverSorting'] : false,
                serverGrouping: options['serverGrouping'] !== undefined ? !!options['serverGrouping'] : false,
                serverAggregates: options['serverAggregates'] !== undefined ? !!options['serverAggregates'] : false,
                //                error: options['errorHandler'] !== undefined ? options['errorHandler'] : defaultErrorHandler,
                // Add this type and load kendo.aspnetmvc.js on the page to enable kendo ASP.Net MVC support.
                // Therefore we can use kendo server side helpers such as DataSourceRequest and DataSourceResult etc..
                type: "aspnetmvc-ajax"
            }));
        };

        Model.getById = function (id, callback) {
            Model.dataSource().fetch(function() {
                callback(this.get(id));
            });
        };

        Model.data = function (callback) {
            Model.dataSource().fetch(function () {
                callback(this.data());
            });
        };

        Model.create = function (model) {
            Model.dataSource().add(model);
        };

        Model.destroy = function (model) {
            Model.dataSource().remove(model);
        };

        Model.cancelChanges = function () {
            Model.dataSource().cancelChanges();
        };

        Model.saveChanges = function (callback, errorHandler) {
            // Use 'one' instead of 'bind' to avoid multiple event handlers to be bound to sync event.
            Model.dataSource().one('sync', function () {
                this.unbind('error');
                callback.apply(this, arguments);
            });

            // change to use a fixed error handler, the sync handler should be removed inside that error handler.
            Model.dataSource().one('error', function () {
                this.unbind('sync');

                // Temporary solution to avoid the same model object being added multiple times.
                // Should be removed once the better solution is found.
                if (_.some(this.data(), function (item) { return item.isNew(); })) {
                    this.cancelChanges();
                }

                (errorHandler || defaultErrorHandler).apply(this, arguments);
            });

            Model.dataSource().sync();
        };

        return Model;
    };
});