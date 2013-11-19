define(function (require) {
    var messenger = require('common/messenger'),
        CategoryModel = require('models/category-model'),
        categoryViewModel;

    return function (router) {
        return categoryViewModel || (categoryViewModel = kendo.observable({
            create: function () {
                router.navigate(router.routeFor('create-category'));
            },
            update: function () {
                router.navigate(router.routeFor('update-category', { id: this.model.categoryId }));
            },
            isDeletable: function () {
                return !this.model.get('isDeletable');
            },
            categoryPictureUrl: function () {
                var pictureId = this.get('model').get('pictureId');
                return '/Image/CategoryPicture/' + (pictureId ? pictureId : '');
            },
            destroy: function () {
                CategoryModel.destroy(this.get('model'));
                CategoryModel.saveChanges(function () {
                    router.navigate(router.routeFor('list-category'));
                });
            },
            save: function () {
                var that = this,
                    model = this.get('model');

                // To handle remote call failure,
                // check the uid of model to decide if it should be added again.
                if (model.isNew()) {
                    model.set('pictureId', '');
                    CategoryModel.create(model);
                }
                CategoryModel.saveChanges(function (e) {
                    router.navigate(router.routeFor('view-category', { id: that.get('model').categoryId} ));
                    // Refresh products
                    // ProductModel.refresh();
                });

                // Prevent the default button behavior.
                return false;
            },
            cancel: function () {
                var categoryId = this.get('model').categoryId;

                CategoryModel.cancelChanges();
                if (categoryId) {
                    router.navigate(router.routeFor('view-category', { id: this.get('model').categoryId} ));
                } else {
                    this.done();
                }

                // Prevent the default button behavior.
                return false;
            },
            search: function () {
                router.navigate(router.routeFor('list-category'));

                // Prevent the default button behavior.
                return false;
            },
            done: function () {
                var returnUrl = this.get('returnUrl');
                if (returnUrl) {
                    messenger.send(returnUrl, 'categoryId', this.get('model').get('categoryId'));
                    router.navigate(returnUrl);
                } else {
                    router.navigate(router.routeFor('list-category'));
                }
            }
        }));
    };
});