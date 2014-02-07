$(function () {

    // debugging knockout
    ko.bindingHandlers.debug =
    {
        init: function (element, valueAccessor) {
            console.log('ko:');
            console.log(element);
            console.log(valueAccessor());
        }
    };
    
    var teamViewModel = new officeAvenger.TeamViewModel();
    var missionViewModel = new officeAvenger.MissionViewModel(teamViewModel);
});