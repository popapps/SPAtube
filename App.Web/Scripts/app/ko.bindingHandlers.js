define('ko.bindingHandlers',
['jquery', 'ko', 'presenter'],
function ($, ko, presenter) {
    var unwrap = ko.utils.unwrapObservable;
    
    ko.bindingHandlers.transitionVisible = {
        update: function (element, valueAccessor) {
            var $element = $(element);
            var value = valueAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            $element.hide();

            if(!valueUnwrapped) {
                presenter.transitionTo($element);
            }
            presenter.toggleActivity(valueUnwrapped);

        }
    };

});