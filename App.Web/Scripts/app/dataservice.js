define('dataservice',
    [
        'dataservice.playlist',
        'dataservice.video',
        'dataservice.userProfile'
],
    function (playlist, video, userProfile) {
        return {
            playlist: playlist,
            video: video,
            userProfile: userProfile
        };
    });