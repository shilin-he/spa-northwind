define(function(require) {
    var ViewModel = require('common/view-model'),
        EmployeeModel = require('models/employee-model'),
        employeeViewModel;

    // EmployeeViewModel = ViewModel.extend({});

    return function (router) {
        return employeeViewModel || (employeeViewModel = new ViewModel({
            router: router,
            modelConstructor: EmployeeModel
        }));
    };
});