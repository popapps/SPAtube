define('dataservice.video',
    ['amplify'],
    function (amplify) {
        var
            init = function () {

                amplify.request.define('videos', 'ajax', {
                    url: '/api/videos',
                    dataType: 'json',
                    type: 'GET'
                });
                amplify.request.define('searchVideos', 'ajax', {
                    url: '/api/videos/search?q={q}',
                    dataType: 'json',
                    type: 'GET'
                });


                amplify.request.define('video', 'ajax', {
                    url: '/api/videos/get/{id}',
                    dataType: 'json',
                    type: 'GET'
                });
                amplify.request.define('videoDelete', 'ajax', {
                    url: '/api/videos/delete/{id}',
                    dataType: 'json',
                    type: 'DELETE'
                });

                amplify.request.define('videoUpdate', 'ajax', {
                    url: '/api/videos',
                    dataType: 'json',
                    type: 'PUT'
                });
                amplify.request.define('videoCreate', 'ajax', {
                    url: '/api/videos',
                    dataType: 'json',
                    type: 'POST'
                });
            },

            getVideos = function (query, callbacks) {
                if (query)
                    return amplify.request({
                        resourceId: 'searchVideos',
                        data: { q: query },
                        success: callbacks.success,
                        error: callbacks.error
                    });
                return amplify.request({
                    resourceId: 'videos',
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            getVideo = function (callbacks, id) {
                return amplify.request({
                    resourceId: 'video',
                    data: { id: id },
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            deleteVideo = function (callbacks, id) {
                return amplify.request({
                    resourceId: 'videoDelete',
                    data: { id: id },
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            createVideo = function (callbacks, data) {
                return amplify.request({
                    resourceId: 'videoCreate',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            updateVideo = function (callbacks, data) {
                return amplify.request({
                    resourceId: 'videoUpdate',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            };

        init();

        return {
            getVideo: getVideo,
            getVideos: getVideos,
            createVideo: createVideo,
            deleteVideo: deleteVideo,
            updateVideo: updateVideo
        };
    });