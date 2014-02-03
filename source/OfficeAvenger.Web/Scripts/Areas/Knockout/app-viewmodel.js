(function (ns, $, undefined) {
    
    var baseAddress = '/api';
    var getTeamUrl = baseAddress + '/team';
    var editTeamUrl = baseAddress + '/team';
    var getMissionsUrl = baseAddress + '/mission';
    var editMissionUrl = baseAddress + '/mission';
    var startMissionUrl = baseAddress + '/mission/start';

    var editTeamModal = $('#editTeamModal');
    var editMissionModal = $('#editMissionModal');

    ns.ViewModel = function() {
        var self = this;

        // props
        self.Team = ko.observableArray();
        self.Missions = ko.observableArray();
        self.SelectedAvenger = ko.observable(ns.EmptyAvenger);
        self.SelectedMission = ko.observable(ns.EmptyMission);

        // avenger team
        self.addTeam = function () {
            self.editTeam(ns.EmptyAvenger);
        };
        self.editTeam = function (avenger) {
            self.SelectedAvenger(avenger);
            editTeamModal.foundation('reveal', 'open');
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
            editTeamModal.foundation('reveal', 'close');

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

        // missions
        self.addMission = function () {
            self.editMission(ns.EmptyMission);
        };
        self.editMission = function (avenger) {
            self.SelectedMission(avenger);
            editMissionModal.foundation('reveal', 'open');
        };
        self.loadMissions = function () {
            self.loading.push(true);

            $.getJSON(getMissionsUrl)
                .done(function (data) {
                    ko.mapping.fromJS(data, ns.MissionMapping, self.Missions);
                    self.loading.pop();
                });
        };
        self.updateMission = function (form) {
            editMissionModal.foundation('reveal', 'close');

            $.post(editMissionUrl, $(form).serialize())
                .done(function (data) {
                    var match = ko.utils.arrayFirst(self.Missions(), function (item) {
                        return item.Id == self.SelectedMission().Id;
                    });
                    if (match)
                        ko.mapping.fromJS(data, { ignore: ["Team"] }, match);
                    else
                        self.Missions.push(new ns.Mission(data));
                });
        };
        self.startMission = function (form) {
            var id = $('[name=id]', form).val();
            $.post(startMissionUrl + '/' + id)
                .done(function (data) {
                    var match = ko.utils.arrayFirst(self.Missions(), function (item) {
                        return item.Id == self.SelectedMission().Id;
                    });
                    ko.mapping.fromJS(data, {}, self);
                });
            event.preventDefault();
        };

        // for tracking loading state
        self.loading = ko.observableArray();
    };
    
})(window.Avenger = window.Avenger || {}, jQuery);