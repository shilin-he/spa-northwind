define(function () {
    return kendo.Class.extend({
        init: function (data) {
            this.ModelConstructor = data.ModelConstructor;
            this.viewModelFunc = data.viewModelFunc;
            this.layout = data.layout;
            this.templateLoader = data.templateLoader;
        },

        handle: function (operation) {
            var that = this;
            return function (router) {
                var viewModel = that.viewModelFunc(router),
                    handler;

                ['list', 'view', 'create', 'update'].indexOf(operation) >= 0 || (operation = 'list');
                handler = eval('that._' + operation);
                return handler.call(that, router, viewModel);
            };
        },

        _showView: function (operation, viewModel) {
            var that = this,
                templateName = this.ModelConstructor.modelName() + '-' + operation;
            this.templateLoader(templateName, function (tmpl) {
                var view = new kendo.View(tmpl, { model: viewModel });
                that.layout.showIn('#content', view);
            });
        },

        _list: function (router, viewModel) {
            var that = this;
            return function () {
                that._showView('list', viewModel);
            };
        },

        _create: function (router, viewModel) {
            var that = this;
            return function () {
                var model = viewModel.get('model');

                if (!model || !model.isNew()) {
                    model = new that.ModelConstructor();
                    viewModel.set('model', model);
                }
                viewModel.set('pageHeader', 'Create New ' + that.ModelConstructor.dispalyName());
                that._showView('edit', viewModel);
            };
        },

        __viewUpdateBase: function (router, viewModel, operation, pageHeader) {
            var that = this;
            return function (id) {
                that.ModelConstructor.getById(id, function (model) {
                    if (!model) {
                        router.navigate(router.routeFor('list-' + that.ModelConstructor.modelName()));
                        return;
                    }
                    viewModel.set('model', model);
                    viewModel.set('pageHeader', pageHeader);
                    that._showView(operation, viewModel);
                });
            };
        },

        _view: function (router, viewModel) {
            return this.__viewUpdateBase(router, viewModel, 'view', this.ModelConstructor.displayName() + ' Details');
        },

        _update: function (router, viewModel) {
            return this.__viewUpdateBase(router, viewModel, 'edit', 'Edit ', this.ModelConstructor.displayName());
        }
    });
});