define(['common/layout',
        'common/template-loader',
        'common/messenger',
        'models/product-model',
        'models/category-model',
        'models/supplier-model',
        'view-models/product-view-model'], function (
        layout,
        templateLoader,
        messenger,
        ProductModel,
        CategoryModel,
        SupplierModel,
        productViewModelFunc) {
            var productViewModel;

            return function (router) {
                return function () {
                    var model,
                        message = messenger.receive(router.routeFor('create-product'));

                    productViewModel || (productViewModel = productViewModelFunc(router));

                    model = productViewModel.get('model');
                    if (!model || !model.isNew()) {
                        model = new ProductModel();
                        model.set('categoryId', null);
                        productViewModel.set('model', model);
                    }
                    if (message && message['categoryId']) {
                        model.set('categoryId', message['categoryId']);
                    }

                    productViewModel.set('pageHeader', 'Create New Product');

                    CategoryModel.data(function (categories) {
                        productViewModel.set('categories', categories);
                    });

                    SupplierModel.data(function (suppliers) {
                        productViewModel.set('suppliers', suppliers);
                    });

                    templateLoader('product-edit', function (templ) {
                        layout.showIn('#content', new kendo.View(templ, { model: productViewModel }));
                    });
                };
            };
        });