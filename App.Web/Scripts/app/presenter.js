define('presenter',
    ['jquery'],
    function ($) {
        var
            transitionOptions = {
                ease: 'swing',
                fadeOut: 100,
                floatIn: 500,
                offsetLeft: '20px',
                offsetRight: '-20px'
            },

            entranceThemeTransition = function ($view) {
                $view.css({
                    display: 'block',
                    visibility: 'visible'
                }).addClass('view-active').animate({
                    marginRight: 0,
                    marginLeft: 0,
                    opacity: 1
                }, transitionOptions.floatIn, transitionOptions.ease);
            },

            resetViews = function () {
                $('.view').css({
                    marginLeft: transitionOptions.offsetLeft,
                    marginRight: transitionOptions.offsetRight,
                    opacity: 0
                });
            },

            toggleActivity = function (show) {
                $('#busyindicator').activity(show);
            },

            transitionTo = function ($view) {
                var $activeViews = $('.view-active');


                if ($activeViews.length) {
                    $activeViews.fadeOut(transitionOptions.fadeOut, function () {
                        resetViews();
                        entranceThemeTransition($view);
                    });
                    $('.view').removeClass('view-active');
                } else {
                    resetViews();
                    entranceThemeTransition($view);
                }


            };


        return {
            toggleActivity: toggleActivity,
            transitionOptions: transitionOptions,
            transitionTo: transitionTo
        };
    });
