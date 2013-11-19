define(function () {
    kendo.ui.validator.rules['super'] = function (input) {
        if (input.filter("[type=text]").filter("[super]").length && input.val() !== "") {
            return input.val().indexOf('super') > -1;
        }
        return true;
    };
    kendo.ui.validator.messages['super'] = "No 'super' in input.";
});