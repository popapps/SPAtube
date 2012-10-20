define('binder',
    ['jquery', 'ko', 'config', 'vm'],
    function ($, ko, config, vm) {
        var
            ids = config.viewIds,

            bind = function () {
                ko.applyBindings(vm.playlist, getView(ids.playlist));
                ko.applyBindings(vm.playlists, getView(ids.playlists));
                ko.applyBindings(vm.video, getView(ids.video));
                vm.playlists.getPlaylists();
            },

            getView = function (viewName) {
                return document.getElementById(viewName.replace('#', ''));
            };

        return {
            bind: bind
        };
    });