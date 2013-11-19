define(function () {
    return kendo.observable({
        search: function () {
            var filters = [],
                productDataSource = this.get('productDataSource'),
                productName = $.trim(this.get('productName')),
                category = this.get('selectedCategory'),
                supplier = this.get('selectedSupplier');

            productName && filters.push({
                field: 'productName',
                operator: 'contains',
                value: productName
            });

            category && filters.push({
                field: 'categoryName',
                operator: 'eq',
                value: category.categoryName
            });

            supplier && filters.push({
                field: 'supplierCompanyName',
                operator: 'eq',
                value: supplier.companyName
            });

            !filters.length ? productDataSource.filter(null) : productDataSource.filter({
                logic: 'and',
                filters: filters
            });
        },
        showAllProducts: function() {
            this.set('productName', '');
            this.set('selectedCategory', null);
            this.set('selectedSupplier', null);

            this.get('productDataSource').filter(null);
        }
    });
});