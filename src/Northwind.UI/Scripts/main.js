require.config({
    baseUrl: 'Scripts/app/'
});

require(['app', 'common/validation'], function (app) {
    app.start();
});