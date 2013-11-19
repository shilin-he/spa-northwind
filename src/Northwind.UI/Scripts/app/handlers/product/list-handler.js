define([
    'common/layout',
    'common/template-loader',
    'models/product-model',
    'models/category-model',
    'models/supplier-model',
    'view-models/product-list-view-model'], function (
    layout,
    templateLoader,
    ProductModel,
    CategoryModel,
    SupplierModel,
    productListViewModel) {
        var productListView;

        return function (router) {
            return function () {
                function showProductList() {
                    layout.showIn('#content', productListView);
                }

                if (productListView) {
                    showProductList();
                } else {
                    productListViewModel.set("productDataSource", ProductModel.dataSource());
                    
                    CategoryModel.data(function (categories) {
                        productListViewModel.set('categories', categories);
                    });

                    SupplierModel.data(function (suppliers) {
                        productListViewModel.set('suppliers', suppliers);
                    });

                    templateLoader('product-list', function (templ) {
                        productListView = new kendo.View(templ, { model: productListViewModel });
                        showProductList();
                    });
                }
            };
        };
    });