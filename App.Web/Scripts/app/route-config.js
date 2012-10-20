define('route-config',
    ['config', 'router', 'vm'],
    function (config, router, vm) {
        var
            logger = config.logger,

            register = function () {

                var routeData = [

                    // Home routes
                    {
                        view: config.viewIds.playlist,
                        isDefault: true,
                        route: config.hashes.home,
                        title: 'Home',
                        callback: vm.playlist.activate
                    },

                    // Search routes
                    {
                        view: config.viewIds.playlist,
                        routes:
                            [{
                                route: config.hashes.search,
                                title: 'Search',
                                callback: vm.playlist.activate
                            }]
                    },

                    {
                        view: config.viewIds.playlist,
                        route: config.hashes.playlists + '/:id',
                        title: 'Playlist',
                        callback: vm.playlist.activate
                    },
                    // Video
                    {
                        view: config.viewIds.video,
                        route: config.hashes.video + '/:id',
                        title: 'Video',
                        callback: vm.video.activate
                    }

                    //// Invalid routes
                    //{
                    //    view: '',
                    //    route: /.*/,
                    //    title: '',
                    //    callback: function () {
                    //        logger.error(config.toasts.invalidRoute);
                    //    }
                    //}
                ];

                for (var i = 0; i < routeData.length; i++) {
                    router.register(routeData[i]);
                }

                // Crank up the router
                router.run();
            };


        return {
            register: register
        };
    });