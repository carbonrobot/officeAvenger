$(function () {
    var model = new Avenger.ViewModel();
    ko.bindingHandlers.debug =
    {
        init: function (element, valueAccessor) {
            console.log('Knockoutbinding:');
            console.log(element);
            console.log(valueAccessor());
        }
    };
    ko.applyBindings(model);

    model.loadTeam();
    model.loadMissions();
});