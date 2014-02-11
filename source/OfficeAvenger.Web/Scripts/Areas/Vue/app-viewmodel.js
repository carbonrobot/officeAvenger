var vm = new Vue({
    data:
        {
            baseAddress: '/api',
            getTeamUrl: baseAddress + '/team',
            editTeamUrl: baseAddress + '/team'
        },

    methods:
        {

        }

})
/*
(function (ns, $, undefined) {
    
    var baseAddress = '/api';
    var getTeamUrl = baseAddress + '/team';
    var editTeamUrl = baseAddress + '/team';
    var getMissionsUrl = baseAddress + '/mission';
    var editMissionUrl = baseAddress + '/mission';
    var startMissionUrl = baseAddress + '/mission/start';
    var addHeroUrl = baseAddress + '/mission/assign';
    var removeHeroUrl = baseAddress + '/mission/remove';

    var editTeamModal = $('#editTeamModal');
    var editMissionModal = $('#editMissionModal');

    ns.ViewModel = function() {
        var self = this;

        // props
        self.Team = ko.observableArray();
        self.Missions = ko.observableArray();
        self.SelectedAvenger = ko.observable(ns.EmptyAvenger);
        self.SelectedMission = ko.observable(ns.EmptyMission);
        self.StandbyTeam = ko.computed(function () {
            var team = self.Team.slice(0);

            if (self.SelectedMission().Team) {
                for (var i = 0; i < self.SelectedMission().Team().length; i++) {
                    var av = self.SelectedMission().Team()[i];
                    for (var j = 0; j < team.length; j++) {
                        var match = team[j];
                        if (match.Id() == av.Id()) {
                            team.splice(j, 1);
                            break;
                        }
                    }
                }
            }

            return team;
        });

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
        self.editMission = function (mission) {
            self.SelectedMission(mission);
            editMissionModal.foundation('reveal', 'open');
        };
        self.getMission = function (id, callback) {
            $.get(getMissionsUrl + '/' + id)
                .done(function (data) {
                    callback(data);
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
        self.updateMission = function (form) {
            $.post(editMissionUrl, $(form).serialize())
                .done(function (data) {
                    var match = ko.utils.arrayFirst(self.Missions(), function (item) {
                        return item.Id() == self.SelectedMission().Id();
                    });
                    if (match) {
                        ko.mapping.fromJS(data, { ignore: ["Team"] }, match);
                    }
                    else {
                        match = new ns.Mission(data);
                        self.Missions.push(match);
                    }
                    self.SelectedMission(match);
                });
        };
        self.startMission = function (form) {
            var id = $('[name=id]', form).val();
            $.post(startMissionUrl + '/' + id)
                .done(function (data) {
                    updateMission(id, data, { ignore: ["Team"] });
                });
            event.preventDefault();
        };

        // mission team members
        self.assignHero = function (form, url) {
            $.post(url, $(form).serialize())
                .done(function () {
                    var id = self.SelectedMission().Id();
                    self.getMission(id, function (data) {
                        updateMission(id, data, {});
                    });
                });
            event.preventDefault();
        };
        self.removeHero = function (form) {
            self.assignHero(form, removeHeroUrl);
        };
        self.addHero = function (form) {
            self.assignHero(form, addHeroUrl);
        };

        // for tracking loading state
        self.loading = ko.observableArray();

        // internal mission updater
        var updateMission = function (id, data, opt) {
            var match = ko.utils.arrayFirst(self.Missions(), function (item) {
                return item.Id() == id;
            });
            ko.mapping.fromJS(data, opt, match);
            self.SelectedMission(match)
        };
    };
    
})(window.Avenger = window.Avenger || {}, jQuery);
*/