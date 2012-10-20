define('vm.playlist',
['ko', 'dataservice', 'config', 'router', 'messenger'],

function (ko, dataservice, config, router, messenger) {
    var PlayListViewModel = function () {
        var self = this;
        this.isRefreshing = ko.observable(false);
        this.videos = ko.observableArray();
        this.activate = function (routeData, callback) {
            messenger.publish.viewModelActivated({ canleaveCallback: self.canLeave });
            self.getVideos(routeData.q);
        };
        this.getVideos = function (query) {
            if (!self.isRefreshing()) {
                self.isRefreshing(true);
                dataservice.video.getVideos(query, {
                    success: function (data) {
                        self.videos(data);
                        self.isRefreshing(false);
                    },
                    error: function () {
                        self.isRefreshing(false);
                    }
                });
            }
        };
        this.openVideo = function (selectedVideo) {
            if (selectedVideo && selectedVideo.id) {
                router.navigateTo(config.hashes.video + '/' + selectedVideo.id);
            }
            return false;
        };

        this.canLeave = function () {
            return true;
        };
    };

    return new PlayListViewModel();

});
