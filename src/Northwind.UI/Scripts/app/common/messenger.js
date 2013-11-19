define(function() {
    var messages = {};

    return {
        send: function(recipient, subject, body) {
            messages[recipient] = messages[recipient] || {};
            messages[recipient][subject] = body;
        },
        receive: function (recipient) {
            var temp = messages[recipient];
            delete messages[recipient];
            return temp;
        }
    };
});