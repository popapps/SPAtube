define('vm.playlists',
['ko', 'dataservice', 'config', 'router', 'presenter'],
function (ko, dataservice, config, router, presenter) {
    var 
        playlists = ko.observableArray(),
        newPlaylist = ko.observable(),
        addPlaylist = function () {
            dataservice.playlist.createPlaylist({
                success: function (data) {
                    playlists.push(data);
                }
            }, { title: newPlaylist() });
            
            newPlaylist('');
            return false;
        },
        getPlaylists = function () {
            dataservice.playlist.getPlaylists({
                success: function (data) {
                    playlists(data);
                }
            });

        },
        removePlaylist = function (elem) {
            dataservice.playlist.deletePlaylist({
                success: function (data) {
                    playlists.remove(elem);
                }
            }, elem.id);
        },
        openPlaylist = function(selectedPlaylist) {
            if (selectedPlaylist && selectedPlaylist.id()) {
                router.navigateTo(config.hashes.playlists + '/' + selectedPlaylist.id());
            }
        };
    return {
        addPlaylist: addPlaylist,
        newPlaylist: newPlaylist,
        playlists: playlists,
        removePlaylist: removePlaylist,
        getPlaylists: getPlaylists,
        openPlaylist: openPlaylist
    };
});
