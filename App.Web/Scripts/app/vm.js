define('vm',
[
        'vm.playlists',
        'vm.playlist',
        'vm.video'
],
    function (playlists, playlist, video) {
        return {
            playlists: playlists,
            playlist: playlist,
            video: video
        };
    });