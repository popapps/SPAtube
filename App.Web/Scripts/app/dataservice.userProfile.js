define('dataservice.userProfile',
    ['amplify'],
    function (amplify) {
        var init = function () {

            amplify.request.define('getCurrentUser', 'ajax', {
                url: '/api/userProfiles/current',
                dataType: 'json',
                type: 'GET'
            });
        },
            getCurrentUser = function (callbacks) {
                return amplify.request({
                    resourceId: 'getCurrentUser',
                    success: callbacks.success,
                    error: callbacks.error
                });
            };


        init();

        return {
            getCurrentUser: getCurrentUser
        };
    });