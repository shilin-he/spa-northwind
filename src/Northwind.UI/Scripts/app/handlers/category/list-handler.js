define([
    'common/layout',
    'common/template-loader',
    'models/category-model',
    'view-models/category-list-view-model'], function (
    layout,
    templateLoader,
    CategoryModel,
    categoryListViewModel) {
        var categoryListView;

        return function (router) {
            return function () {
                function showCategoryList() {
                    layout.showIn('#content', categoryListView);
                }

                categoryListViewModel.set("categoryDataSource", CategoryModel.dataSource());

                templateLoader('category-list', function (templ) {
                    categoryListView = new kendo.View(templ, { model: categoryListViewModel });
                    showCategoryList();
                });
            };
        };
    });