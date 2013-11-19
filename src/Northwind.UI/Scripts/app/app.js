define(['common/routes', 'common/router', 'common/layout', 'common/route-handler'],
    function (routes, Router, layout, RouteHandler) {
        var idx, route,
            router = new Router({
                init: function () {
                    layout.render("#container");
                },
                routeMissing: function () {
                    router.navigate(router.urlFor('default-route'));
                }
            }),
            routeHandler = new RouteHandler(router);

        for (idx = 0; idx < routes.length; idx++) {
            route = routes[idx];
            router.route(route['name'], route['route'], route['handler'](router));
        }

        router.route(/^\/([^\/]+)\/([^\/]+)(?:\/([^\/]+))?$/, routeHandler.handle);

        return router;
    });