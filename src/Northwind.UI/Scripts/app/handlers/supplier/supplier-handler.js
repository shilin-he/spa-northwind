define(function (require) { var SupplierModel = require('models/supplier-model'),
        supplierViewModelFunc = require('view-models/supplier-view-model'),
        layout = require('common/layout'),
        templateLoader = require('common/template-loader');

    return function (operation) {
        function showView(templateName, viewModel) {
            templateLoader(templateName, function (tmpl) {
                var view = new kendo.View(tmpl, { model: viewModel });
                layout.showIn('#content', view);
            });
        }

        function operationHandler(router) {
            var supplierViewModel = supplierViewModelFunc(router),
                handler;

            ['list', 'view', 'create', 'update'].indexOf(operation) >= 0 || (operation = 'list');
            handler = eval(operation + 'Handler');
            return handler(router, supplierViewModel);
        }

        function listHandler(router, supplierViewModel) {
            return function () {
                showView('supplier-list', supplierViewModel);
            };
        };

        function viewUpdateBaseHandler(router, supplierViewModel, templateName, pageHeader) {
            return function (id) {
                SupplierModel.getById(id, function (supplier) {
                    if (!supplier) {
                        router.navigate(router.routeFor('list-supplier'));
                        return;
                    }
                    supplierViewModel.set('model', supplier);
                    supplierViewModel.set('pageHeader', pageHeader);
                    showView(templateName, supplierViewModel);
                });
            };
        }

        function viewHandler(router, supplierViewModel) {
            return viewUpdateBaseHandler(router, supplierViewModel, 'supplier-view', 'Supplier Details');
        }

        function updateHandler(router, supplierViewModel) {
            return viewUpdateBaseHandler(router, supplierViewModel, 'supplier-edit', 'Edit Supplier');
        }

        function createHandler(router, supplierViewModel) {
            return function () {
                var model = supplierViewModel.get('model');

                if (!model || !model.isNew()) {
                    model = new SupplierModel();
                    supplierViewModel.set('model', model);
                }
                supplierViewModel.set('pageHeader', 'Create New Supplier');
                showView('supplier-edit', supplierViewModel);
            };
        };

        return operationHandler;
    };
});