define([
    'common/layout',
    'common/template-loader',
    'models/product-model',
    'view-models/product-view-model'], function (
    layout,
    templateLoader,
    ProductModel,
    productViewModelFunc) {
        var productDetailsView,
            productViewModel;

        return function (router) {
            return function (id) {
                if (!(id = parseInt(id))) {
                    searchProducts();
                    return;
                }

                function searchProducts() {
                    router.navigate('/product');
                }

                ProductModel.getById(id, function (product) {
                    if (!product) {
                        searchProducts();
                        return;
                    }

                    productViewModel || (productViewModel = productViewModelFunc(router));
                    productViewModel.set('model', product);
                    productViewModel.set('pageHeader', 'Product Details');

                    function showProductDetails() {
                        layout.showIn('#content', productDetailsView);
                    }

                    templateLoader('product-view', function (templ) {
                        productDetailsView = new kendo.View(templ, { model: productViewModel });
                        showProductDetails();
                    });
                });
            };
        };
    });