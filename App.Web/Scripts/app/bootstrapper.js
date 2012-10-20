define('bootstrapper',
    ['jquery', 'config', 'route-config', 'presenter', 'binder'],
    function ($, config, routeConfig, presenter, binder) {
        var
            run = function () {
                presenter.toggleActivity(true);

                config.dataserviceInit();
                binder.bind();
                routeConfig.register();
                
                presenter.toggleActivity(false);
            };

        return {
            run: run
        };
    });