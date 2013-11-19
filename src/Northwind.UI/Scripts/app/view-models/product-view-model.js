define(function (require) {
    var messenger = require('common/messenger'),
        ProductModel = require('models/product-model');
    
    return function (router) {
        return kendo.observable({
            addCategory: function() {
                var productId = this.get('model').get('productId'),
                    currentUrl = productId ? '/product/update/' + productId : '/product/create',
                    createCategoryUrl = '/category/create';

                messenger.send(createCategoryUrl, 'returnUrl', currentUrl);
                router.navigate(createCategoryUrl);
            },
            create: function () {
                router.navigate('/product/create');
            },
            update: function () {
                router.navigate('/product/update/' + this.model.productId);
            },
            toggleStatus: function () {
                var model = this.get('model'),
                    status = !!model.get('discontinued');
                model.set('discontinued', !status);
                
                ProductModel.saveChanges();
            },
            toggleStatusBtnText: function () {
                return !!this.get('model').get('discontinued') ? 'Continue' : 'Discontinue';
            },
            destroy: function () {
                ProductModel.destroy(this.get('model'));
                ProductModel.saveChanges(function () {
                    router.navigate('/product');
                });
            },
            save: function () {
                var that = this;

                // To handle remote call failure,
                // check the uid of model to decide if it should be added again.
                if (this.model.isNew()) {
                    ProductModel.create(this.model);
                }
                ProductModel.saveChanges(function (e) {
                    router.navigate('/product/view/' + that.get('model').productId);
                });

                // Prevent the default button behavior.
                return false;
            },
            cancel: function () {
                var productId = this.get('model').get('productId');

                ProductModel.cancelChanges();
                if (productId) {
                    router.navigate('/product/view/' + productId);
                } else {
                    router.navigate('/product');
                }

                // Prevent the default button behavior.
                return false;
            },
            search: function () {
                router.navigate('/product');

                // Prevent the default button behavior.
                return false;
            }
        });
    };
});