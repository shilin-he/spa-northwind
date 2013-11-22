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
                var handler;

                that.router = router;

                ['list', 'view', 'create', 'update'].indexOf(operation) >= 0 || (operation = 'list');
                handler = eval('that._' + operation);
                return handler.call(that);
            };
        },

        __getViewModel: function () {
            return this.viewModelFunc(this.router);
        },

        _showView: function (operation) {
            var that = this,
                templateName = this.ModelConstructor.modelName() + '-' + operation;
            this.templateLoader(templateName, function (tmpl) {
                var view = new kendo.View(tmpl, { model: that.__getViewModel() });
                that.layout.showIn('#content', view);
            });
        },

        _list: function () {
            var that = this;
            return function () {
                that._showView('list');
            };
        },

        _create: function () {
            var that = this;
            return function () {
                var viewModel = that.__getViewModel(),
                    model = viewModel.get('model');

                if (!model || !model.isNew()) {
                    model = new that.ModelConstructor();
                    viewModel.set('model', model);
                }
                viewModel.set('pageHeader', 'Create New ' + that.ModelConstructor.displayName());
                that._showView('edit');
            };
        },

        __viewUpdateBase: function (operation, pageHeader) {
            var that = this;
            return function (id) {
                var router = that.router,
                    viewModel = that.__getViewModel();
                that.ModelConstructor.getById(id, function (model) {
                    if (!model) {
                        router.navigate(router.routeFor('list-' + that.ModelConstructor.modelName()));
                        return;
                    }
                    viewModel.set('model', model);
                    viewModel.set('pageHeader', pageHeader);
                    that._showView(operation);
                });
            };
        },

        _view: function () {
            return this.__viewUpdateBase('view', this.ModelConstructor.displayName() + ' Details');
        },

        _update: function () {
            return this.__viewUpdateBase('edit', 'Edit ' + this.ModelConstructor.displayName());
        }
    });
});