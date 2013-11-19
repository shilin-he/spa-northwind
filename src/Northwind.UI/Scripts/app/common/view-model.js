define(function () {
    return kendo.data.ObservableObject.extend({
        init: function (data) {
            var that = this;
            this.router = data.router;
            this.modelConstructor = data.modelConstructor;
            this.modelName = this.modelConstructor.modelName();
            this.dataSource = data.modelConstructor.dataSource();
            kendo.data.ObservableObject.fn.init.call(that, data);
        },
        routeName: function (op) {
            return op + '-' + this.modelName;
        },
        create: function () {
            var router = this.router;
            router.navigate(router.routeFor(this.routeName('create')));
        },
        update: function () {
            var router = this.router;
            router.navigate(router.routeFor(this.routeName('update'), { id: this.get('model').get('id') }));
        },
        destroy: function () {
            var router = this.router,
                listRouteName = this.routeName('list');

            this.modelConstructor.destroy(this.get('model'));
            this.modelConstructor.saveChanges(function () {
                router.navigate(router.routeFor(listRouteName));
            });
        },
        save: function () {
            var router = this.router,
                viewRouteName = this.routeName('view'),
                model = this.get('model');

            // To handle remote call failure,
            // check the uid of model to decide if it should be added again. ??
            if (model.isNew()) {
                modelConstructor.create(model);
            }
            modelConstructor.saveChanges(function (e) {
                router.navigate(router.routeFor(viewRouteName, { id: model.get('id') }));
            });

            // Prevent form post.
            return false;
        },
        cancel: function () {
            var router = this.router,
                modelId = this.get('model').get('id');

            this.modelConstructor.cancelChanges();
            if (modelId) {
                router.navigate(router.routeFor(this.routeName('view'), { id: this.get('model').get('id') }));
            } else {
                router.navigate(this.routeName('list'));
            }

            // Prevent form post.
            return false;
        },
        list: function () {
            var router = this.router;

            router.navigate(router.routeFor(this.routeName('list')));

            // Prevent form post.
            return false;
        }
    });
});