define(function (require) {
    var RequestHandler = require('common/request-handler'),
        OrderModel = require('models/order-model'),
        orderViewModelFunc = require('view-models/order-view-model'),
        layout = require('common/layout'),
        templateLoader = require('common/template-loader');

    // var OrderRequestHandler = RequestHandler.extend({});

    return new RequestHandler({
        ModelConstructor: OrderModel,
        viewModelFunc: orderViewModelFunc,
        layout: layout,
        templateLoader: templateLoader
    });
});