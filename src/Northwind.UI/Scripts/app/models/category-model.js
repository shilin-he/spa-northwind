define(['common/base-model'], function (model) {
    return model({
        modelName: 'category',
        isValid: function () {
            return !!($.trim(this.get('categoryName')));
        }
    });
});