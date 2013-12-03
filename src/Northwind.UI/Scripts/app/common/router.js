define(function () {
    return kendo.Router.extend({
        init: function (options) {
            this.namedRoutes = {};
            kendo.Router.fn.init.call(this, options);
        },
        route: function (name, route, callback) {
            if (arguments.length == 2) {
                callback = route;
                route = name;
                name = 'catchall';
            }
            this.namedRoutes[name] = { route: route, callback: callback };
            kendo.Router.fn.route.call(this, route, callback);
        },
        routeFor: function (name, params) {
            var model, op, id, temp;
            
            if (this.namedRoutes[name]) {
                return this.namedRoutes[name]['route'].replace(/\:(\w+)/g, function(_, param) {
                    return params[param];
                });
            } else {
                temp = name.split('-');
                model = temp[1];
                op = temp[0];
                id = params && params['id'];

                return '/' + model + '/' + op + (id ? '/' + id : '');
            }
        }
    });
});