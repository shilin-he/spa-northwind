define(['common/layout',
        'common/template-loader',
        'models/category-model',
        'view-models/category-view-model',
        'common/messenger'], function (
        layout,
        templateLoader,
        CategoryModel,
        categoryViewModelFunc,
        messenger) {

            var categoryEditView,
                categoryViewModel;

            return function (router) {
                return function () {
                    var returnUrl,
                        message = messenger.receive('/category/create'),
                        category = new CategoryModel();

                    categoryViewModel || (categoryViewModel = categoryViewModelFunc(router));
                    categoryViewModel.set('model', category);
                    categoryViewModel.set('pageHeader', 'Create New Category');

                    // ???
                    if (message && message.returnUrl) {
                        returnUrl = message.returnUrl;
                    }
                    categoryViewModel.set('returnUrl', returnUrl);

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
                };
            };
        });