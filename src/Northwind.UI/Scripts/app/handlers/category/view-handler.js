define([
    'common/template-loader',
    'common/layout',
    'models/category-model',
    'view-models/category-view-model'], function (
    templateLoader,
    layout,
    CategoryModel,
    categoryViewModelFunc) {
    var categoryDetailsView,
        categoryViewModel;

    return function (router) {
        return function (id) {
            if (!(id = parseInt(id))) {
                searchCategorys();
                return;
            }

            function searchCategorys() {
                router.navigate('/category');
            }

            CategoryModel.getById(id, function (category) {
                if (!category) {
                    searchCategorys();
                    return;
                }

                categoryViewModel || (categoryViewModel = categoryViewModelFunc(router));
                categoryViewModel.set('model', category);
                categoryViewModel.set('pageHeader', 'Category Details');

                function showCategoryDetails() {
                    layout.showIn('#content', categoryDetailsView);
                }

                templateLoader('category-view', function (templ) {
                    categoryDetailsView = new kendo.View(templ, { model: categoryViewModel });
                    showCategoryDetails();
                });
            });
        };
    };
});