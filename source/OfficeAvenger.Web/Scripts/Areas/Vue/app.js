$(function () {

    // debugging knockout
    ko.bindingHandlers.debug =
    {
        init: function (element, valueAccessor) {
            console.log('Knockoutbinding:');
            console.log(element);
            console.log(valueAccessor());
        }
    };

    var model = new Avenger.ViewModel();
    ko.applyBindings(model);

    model.loadTeam();
    model.loadMissions();
});