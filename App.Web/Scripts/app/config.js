define('config',
['toastr', 'ko'],
function (toastr, ko) {

    var
        currentUserId = 0, // Anonymous
        currentUser = ko.observable(),
        hashes = {
            home: '#/home',
            playlists: '#/playlists',
            video: '#/video',
            search: '#/search',
        },
        messages = {
            viewModelActivated: 'viewmodel-activation'
        },
        logger = toastr,
        title = 'SPAtube > ',
        toastrTimeout = 2000,
        viewIds = {
            playlists: '#playlists-view',
            playlist: '#playlist-view',
            video: '#video-view',
            search: '#search-view',
        },
        stateKeys = {
            lastView: 'state.active-hash'
        },
        toasts = {
            changesPending: 'Please save or cancel your changes before leaving the page.',
            errorSavingData: 'Data could not be saved. Please check the logs.',
            errorGettingData: 'Could not retrieve data.  Please check the logs.',
            invalidRoute: 'Cannot navigate. Invalid route',
            retreivedData: 'Data retrieved successfully',
            savedData: 'Data saved successfully'
        },
        dataserviceInit = function () {
        },
        validationInit = function () {
            ko.validation.configure({
                registerExtenders: true,    //default is true
                messagesOnModified: true,   //default is true
                insertMessages: true,       //default is true
                parseInputAttributes: true, //default is false
                writeInputAttributes: true, //default is false
                messageTemplate: null,      //default is null
                decorateElement: true       //default is false. Applies the .validationElement CSS class
            });
        },
        init = function () {
            dataserviceInit();

            toastr.options.timeOut = toastrTimeout;
            validationInit();
        };
    init();

    return {
        currentUserId: currentUserId,
        currentUser: currentUser,
        dataserviceInit: dataserviceInit,
        hashes: hashes,
        logger: logger,
        messages: messages,
        title: title,
        stateKeys: stateKeys,
        toasts: toasts,
        viewIds: viewIds
    };
});