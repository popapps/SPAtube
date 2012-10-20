define('vm.video',
['ko', 'dataservice', 'config', 'router', 'messenger'],

function (ko, dataservice, config, router, messenger) {
    var VideoViewModel = function () {
        var self = this;
        this.isRefreshing = ko.observable(false);
        this.video = ko.observable(null);
        this.activate = function (routeData, callback) {
            messenger.publish.viewModelActivated({ canleaveCallback: self.canLeave });
            self.getVideo(routeData.id);
        };
        
        this.playerUrl = ko.computed(function () {
            return self.video() != null ? 
                'http://www.youtube.com/embed/' + self.video().id() + '?enablejsapi=1' : '';
        }, this);
        this.getVideo = function (id) {
            if (!self.isRefreshing()) {
                self.isRefreshing(true);
                dataservice.video.getVideo({
                    success: function (data) {
                        self.video(ko.mapping.fromJS(data));
                        self.isRefreshing(false);
                    },
                    error: function () {
                        self.isRefreshing(false);
                    }
                }, id);
            }
        };
        this.canLeave = function () {
            return true;
        };
    };

    return new VideoViewModel();

});

