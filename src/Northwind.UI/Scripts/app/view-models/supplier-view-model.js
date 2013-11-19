define(function(require) {
    var ViewModel = require('common/view-model'),
        SupplierModel = require('models/supplier-model'),
        supplierViewModel;

    // SupplierViewModel = ViewModel.extend({});

    return function (router) {
        return supplierViewModel || (supplierViewModel = new ViewModel({
            router: router,
            modelConstructor: SupplierModel
        }));
    };
});