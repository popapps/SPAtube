define('router',
    ['jquery', 'underscore', 'sammy', 'presenter', 'config', 'route-mediator', 'store'],
    function ($, _, Sammy, presenter, config, routeMediator, store) {
        var
            currentHash = '',
            defaultRoute = '',
            isRedirecting = false,
            logger = config.logger,
            startupUrl = '',
            window = config.window,

            sammy = new Sammy.Application(function () {
                if (Sammy.Title) {
                    this.use(Sammy.Title);
                    this.setTitle(config.title);
                }
            }),

            navigateBack = function () {
                window.history.back();
            },

            navigateTo = function (url) {
                sammy.setLocation(url);
            },

            register = function (options) {
                if (options.routes) {
                    // Register a list of routes
                    _.each(options.routes, function (route) {
                        registerRoute({
                            route: route.route,
                            title: route.title,
                            callback: route.callback,
                            view: options.view,
                            isDefault: !!route.isDefault
                        });
                    });
                    return;
                }

                // Register 1 route
                registerRoute(options);
            },


            registerRoute = function (options) {
                if (!options.callback) {
                    throw Error('callback must be specified.');
                }
                if (options.isDefault) {
                    defaultRoute = options.route;
                }
                sammy.get(options.route, function (context) { //context is 'this'
                    store.save(config.stateKeys.lastView, context.path);
                    $('.view').hide();
                    $(options.view).show();
                    options.callback(context.params); // Activate the viewmodel
                    if (this.title) {
                        this.title(options.title);
                    }
                });
            },

            run = function () {
                var url = store.fetch(config.stateKeys.lastView);

                // 1) if i browse to a location, use it
                // 2) otherwise, use the url i grabbed from storage
                // 3) otherwise use the default route
                var currentLocation = sammy.getLocation();
                if (currentLocation == '/')
                    currentLocation = '';
                startupUrl = currentLocation || url || defaultRoute;
                
                if (!startupUrl) {
                    logger.error('No route was indicated.');
                    return;
                }
                sammy.run();
                navigateTo(startupUrl);
            };

        return {
            navigateBack: navigateBack,
            navigateTo: navigateTo,
            register: register,
            run: run
        };
    });