define(function(require) {
    var ViewModel = require('common/view-model'),
        OrderModel = require('models/order-model'),
        orderViewModel;

    // OrderViewModel = ViewModel.extend({});

    return function (router) {
        return orderViewModel || (orderViewModel = new ViewModel({
            router: router,
            modelConstructor: OrderModel
        }));
    };
});