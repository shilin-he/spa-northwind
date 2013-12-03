define(function(require) {
    var ViewModel = require('common/view-model'),
        ShipperModel = require('models/shipper-model'),
        shipperViewModel;

    // OrderViewModel = ViewModel.extend({});

    return function (router) {
        return shipperViewModel || (shipperViewModel = new ViewModel({
            router: router,
            modelConstructor: ShipperModel
        }));
    };
});