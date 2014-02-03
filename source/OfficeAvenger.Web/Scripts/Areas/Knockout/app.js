var baseAddress = '/api';
var getTeamUrl = baseAddress + '/team';
var getMissionsUrl = baseAddress + '/mission';

function AvengerViewModel() {
    var self = this;

    // props
    self.Team = ko.observableArray([]);
    self.Missions = ko.observableArray([]);

    // get teams
    self.loadTeam = function () {
        self.loading.push(true);

        $.getJSON(getTeamUrl)
             .done(function (data) {
                 self.Team(data);
                 self.loading.pop();
             });
    };

    // get missions
    self.loadMissions = function () {
        self.loading.push(true);

        $.getJSON(getMissionsUrl)
            .done(function (data) {
                self.Missions(data);
                self.loading.pop();
            });
    };

    // for tracking loading state
    self.loading = ko.observableArray();
};

$(function () {
    var model = new AvengerViewModel();
    ko.applyBindings(model);

    model.loadTeam();
    model.loadMissions();
});