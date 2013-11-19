define(['common/base-model'], function (model) {
    return model({
        modelName: 'product',
        isValid: function () {
            return !!this.get('productName') &&
                !!this.get('categoryId') &&
                !!this.get('supplierId') &&
                !!this.get('unitPrice');
        }
    });
});