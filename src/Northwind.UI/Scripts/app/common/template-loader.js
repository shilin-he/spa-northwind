define(function () {
    var templatesLoadUrl = window.templatesLoadUrl || '/Template/Load',
        templates = {};

    return function (templateName, callback) {
        var template;

        if (template = templates[templateName]) {
            callback(template);
        } else {
            if (template = $('#' + templateName).html()) {
                callback(templates[templateName] = template);
            } else {
                $.get(templatesLoadUrl + '/' +
                    encodeURIComponent(templateName), function (data) {
                        callback(templates[templateName] = data);
                    });
            }
        }
    };
});
