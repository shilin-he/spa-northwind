define(function (require) {
    var templateLoader = require('common/template-loader'),
        layout = require('common/layout'),
        templates = {
            'list': 'list',
            'view': 'view',
            'update': 'edit',
            'create': 'edit'
        };
    return kendo.Class.extend({
        init: function (router) {
            this.router = router;
        },

        handle: function () {
            return $.proxy(function (modelName, operation, id) {
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
            }, this);
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
            this.viewModel.set('pageHeader', 'Create New ' + this.ModelConstructor.displayName());
            this._showView();
        },

        _view: function () {
            var that = this;
            this.ModelConstructor.getById(this.id, function (model) {
                if (!model) {
                    that.router.navigate(that.router.routeFor('list-' + that.ModelConstructor.modelName()));
                    return;
                }
                that.viewModel.set('model', model);
                that.viewModel.set('pageHeader', that.__getPageHeader());
                that._showView();
            });
        },

        _update: function () {
            return this._view();
        },

        _showView: function () {
            var that = this,
                templateName = this.ModelConstructor.modelName() + '-' + templates[this.operation];
            templateLoader(templateName, function (tmpl) {
                var view = new kendo.View(tmpl, { model: that.viewModel });
                layout.showIn('#content', view);
            });
        },

        __getPageHeader: function () {
            var op = this.operation;
            return op.substring(0, 1).toUpperCase() + op.substring(1) + ' ' + this.ModelConstructor.modelName();
        }
    });
});