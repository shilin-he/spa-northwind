define(['common/layout',
        'common/template-loader',
        'models/product-model',
        'models/category-model',
        'models/supplier-model',
        'view-models/product-view-model'], function (
        layout,
        templateLoader,
        ProductModel,
        CategoryModel,
        SupplierModel,
        productViewModelFunc) {

            var productViewModel;

            return function (router) {
                return function (id) {
                    if (!(id = parseInt(id))) {
                        searchProducts();
                        return;
                    }

                    function searchProducts() {
                        router.navigate(router.routeFor('list-product'));
                    }

                    ProductModel.getById(id, function (product) {
                        if (!product) {
                            searchProducts();
                            return;
                        }

                        productViewModel || (productViewModel = productViewModelFunc(router));
                        productViewModel.set('model', product);
                        productViewModel.set('pageHeader', 'Edit Product');

                        CategoryModel.data(function (categories) {
                            productViewModel.set('categories', categories);
                        });

                        SupplierModel.data(function (suppliers) {
                            productViewModel.set('suppliers', suppliers);
                        });

                        templateLoader('product-edit', function (templ) {
                            layout.showIn('#content', new kendo.View(templ, { model: productViewModel }));
                        });
                    });
                };
            };
        });