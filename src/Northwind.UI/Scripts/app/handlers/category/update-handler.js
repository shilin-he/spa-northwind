define(['common/layout',
        'common/template-loader',
        'models/category-model',
        'view-models/category-view-model'], function (
        layout,
        templateLoader,
        CategoryModel,
        categoryViewModelFunc) {

            var categoryEditView,
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
                        categoryViewModel.set('pageHeader', 'Edit Category');
                        categoryViewModel.set('model', category);

                        function showCategoryEditView() {
                            var kendoUpload;

                            layout.showIn('#content', categoryEditView);

                            kendoUpload = $('#file').data('kendoUpload');
                            kendoUpload.bind('remove', function (e) {
                                e.data = {
                                    imageId: category.get('tempPictureId')
                                };
                            });
                            kendoUpload.bind('success', function (e) {
                                var tempPictureId = null;
                                e.operation === 'upload' && (tempPictureId = e.response);
                                category.set('tempPictureId', tempPictureId);
                            });
                        }

                        templateLoader('category-edit', function (templ) {
                            categoryEditView = new kendo.View(templ, { model: categoryViewModel });
                            showCategoryEditView();
                        });
                    });
                };
            };
        });