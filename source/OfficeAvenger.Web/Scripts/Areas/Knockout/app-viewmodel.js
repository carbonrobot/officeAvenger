(function (ns, $, undefined) {
    
    var baseAddress = '/api';
    var getTeamUrl = baseAddress + '/team';
    var getMissionsUrl = baseAddress + '/mission';
    var editTeamUrl = baseAddress + '/team';

    ns.ViewModel = function() {
        var self = this;

        // props
        self.Team = ko.observableArray();
        self.Missions = ko.observableArray();
        self.SelectedAvenger = ko.observable(ns.EmptyAvenger);

        // methods
        self.addTeam = function () {
            self.SelectedAvenger(ns.EmptyAvenger);
            $('#editTeamModal').foundation('reveal', 'open');
        };
        self.editTeam = function (avenger) {
            self.SelectedAvenger(avenger);
            $('#editTeamModal').foundation('reveal', 'open');
        };
        self.loadTeam = function () {
            self.loading.push(true);

            $.getJSON(getTeamUrl)
                 .done(function (data) {
                     ko.mapping.fromJS(data, ns.TeamMapping, self.Team);
                     self.loading.pop();
                 });
        };
        self.updateTeam = function (form) {
            $('#editTeamModal').foundation('reveal', 'close');

            $.post(editTeamUrl, $(form).serialize())
                .done(function (data) {
                    var match = ko.utils.arrayFirst(self.Team(), function (item) {
                        return item.Id == self.SelectedAvenger().Id;
                    });
                    if (match)
                        ko.mapping.fromJS(data, {}, match);
                    else
                        self.Team.push(new ns.Team(data));
                });
        };

        self.loadMissions = function () {
            self.loading.push(true);

            $.getJSON(getMissionsUrl)
                .done(function (data) {
                    ko.mapping.fromJS(data, ns.MissionMapping, self.Missions);
                    self.loading.pop();
                });
        };

        // for tracking loading state
        self.loading = ko.observableArray();
    };
    
})(window.Avenger = window.Avenger || {}, jQuery);