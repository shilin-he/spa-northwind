define(['handlers/product/list-handler',
        'handlers/product/view-handler',
        'handlers/product/create-handler',
        'handlers/product/update-handler',
        'handlers/category/list-handler',
        'handlers/category/view-handler',
        'handlers/category/create-handler',
        'handlers/category/update-handler'], function (
        productListHandler,
        productViewHandler,
        productCreateHandler,
        productUpdateHandler,
        categoryListHandler,
        categoryViewHandler,
        categoryCreateHandler,
        categoryUpdateHandler) {
            var routes = {
                'default-route': { routePattern: '/', routeHandler: productListHandler },
                'list-product': { routePattern: '/product', routeHandler: productListHandler },
                'view-product': { routePattern: '/product/view/:id', routeHandler: productViewHandler },
                'create-product': { routePattern: '/product/create', routeHandler: productCreateHandler },
                'update-product': { routePattern: '/product/update/:id', routeHandler: productUpdateHandler },
                'list-category': { routePattern: '/category', routeHandler: categoryListHandler },
                'view-category': { routePattern: '/category/view/:id', routeHandler: categoryViewHandler },
                'create-category': { routePattern: '/category/create', routeHandler: categoryCreateHandler },
                'update-category': { routePattern: '/category/update/:id', routeHandler: categoryUpdateHandler },
            };

            return {
                routes: function () {
                    return $.map(routes, function (route) { return route; });
                },
                getRoute: function(routeName) {
                     return routes[routeName];
                },
                urlFor: function (routeName, params) {
                    var routePattern = routes[routeName]['routePattern'];
                    return routePattern.replace(/\:(\w+)/g, function (_, param) {
                        return params[param];
                    });
                }
            };
        });
