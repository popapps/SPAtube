define('dataservice.playlist',
    ['amplify'],
    function (amplify) {
        var
            init = function () {

                amplify.request.define('playlists', 'ajax', {
                    url: '/api/playlists',
                    dataType: 'json',
                    type: 'GET'
                    //cache: true
                    //cache: 60000 // 1 minute
                    //cache: 'persist'

                });


                amplify.request.define('playlist', 'ajax', {
                    url: '/api/playlists/{id}',
                    dataType: 'json',
                    type: 'GET'
                });
                amplify.request.define('playlistDelete', 'ajax', {
                    url: '/api/playlists/delete/{id}',
                    dataType: 'json',
                    type: 'DELETE'
                });

                amplify.request.define('playlistUpdate', 'ajax', {
                    url: '/api/playlists',
                    dataType: 'json',
                    type: 'PUT'
                });
                amplify.request.define('playlistCreate', 'ajax', {
                    url: '/api/playlists/post',
                    dataType: 'json',
                    type: 'POST'
                });
            },

            getPlaylists = function (callbacks) {
                return amplify.request({
                    resourceId: 'playlists',
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            getPlaylist = function (callbacks, id) {
                return amplify.request({
                    resourceId: 'playlist',
                    data: { id: id },
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            deletePlaylist = function (callbacks, id) {
                return amplify.request({
                    resourceId: 'playlistDelete',
                    data: { id: id },
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            createPlaylist = function (callbacks, data) {
                return amplify.request({
                    resourceId: 'playlistCreate',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },
            updatePlaylist = function (callbacks, data) {
                return amplify.request({
                    resourceId: 'playlistUpdate',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            };

        init();

        return {
            getPlaylists: getPlaylists,
            getPlaylist: getPlaylist,
            createPlaylist: createPlaylist,
            deletePlaylist: deletePlaylist,
            updatePlaylist: updatePlaylist
        };
    });