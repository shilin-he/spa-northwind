define(function (require) {
    return kendo.Class.extend({
        init: function (router) {
            this.router = router;
        },

        handle: function (modelName, operation, id) {
            var that = this;
            require(['models/' + modelName + '-model', 'view-models/' + modelName + '-view-model'], function (model, vm) {
                if (!model || !vm) {
                    that.router.navigate(that.router.root || '/');
                    return;
                }

                operation = encodeURIComponent(operation);
                that.ModelConstructor = model;
                that.operation = operation;
                that.id = id;
                that.viewModel = vm(that.router);

                (eval('that._' + operation) || that._list).call(that);
            });
        },

        _list: function () {
            this._showView();
        },

        _create: function () {
            var model = this.viewModel.get('model');

            if (!model || !model.isNew()) {
                model = new this.ModelConstructor();
                this.viewModel.set('model', model);
            }
            this.viewModel.set('pageHeader', 'Create New ' + this.ModelConstructor.dispalyName());
            this._showView('edit', this.viewModel);
        },

        _view: function () {
            var that = this;
            this.ModelConstructor.getById(id, function (model) {
                if (!model) {
                    that.router.navigate(that.router.routeFor('list-' + that.ModelConstructor.modelName()));
                    return;
                }
                that.viewModel.set('model', model);
                that.viewModel.set('pageHeader', that.getPageHeader());
                that._showView();
            });
        },

        _update: function () {
            return this._view();
        },

        __showView: function () {
            var that = this,
                templateName = this.ModelConstructor.modelName() + '-' + operation;
            this.templateLoader(templateName, function (tmpl) {
                var view = new kendo.View(tmpl, { model: that.viewModel });
                that.layout.showIn('#content', view);
            });
        },

        __getPageHeader: function () {
            var op = this.operation;
            return op.substring(0, 1).toUpperCase() + op.substring(1) + ' ' + this.ModelConstructor.modelName();
        }
    });
});