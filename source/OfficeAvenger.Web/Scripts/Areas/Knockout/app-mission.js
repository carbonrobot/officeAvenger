(function (ns) {

    // mappings
    ns.emptyMission = { Id: "", Name: "", Duration: "" };
    ns.missionMapping = {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (opt) {
            return new ns.Mission(opt.data);
        },
        "Team": ns.teamMapping
    };

    // Model
    ns.Mission = function(jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);

        var self = this;
    };
    
    // Viewmodel
    ns.MissionViewModel = function (teamViewModel) {
        var self = this;

        // props
        this.loading = ko.observableArray([]);
        this.missions = ko.observableArray();
        this.selectedMission = ko.observable(ns.emptyMission);
        this.standbyTeam = ko.computed(function () {
            var team = teamViewModel.team.slice(0);

            if (self.selectedMission().Team) {
                for (var i = 0; i < self.selectedMission().Team().length; i++) {
                    var av = self.selectedMission().Team()[i];
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

        // initializes the knockout bindings
        self.init = function () {
            ko.applyBindings(self, ns.views.missionsView);
            ko.applyBindings(self, ns.views.editMissionModal);
            self.loadMissions();
            return self;
        };

        // add a new mission
        self.addMission = function () {
            self.editMission(ns.emptyMission);
        };

        // delete the selected mission
        self.deleteMission = function (mission) {
            $.ajax({
                url: ns.urls.deleteMissionUrl + '/' + mission.Id(),
                type: 'DELETE'
            }).done(function (data) {
                self.missions.pop(mission);
            });
        };

        // edit the selected mission
        self.editMission = function (mission) {
            self.selectedMission(mission);
            ns.modal.open(ns.views.editMissionModal);
        };

        // get the selected mission by id
        self.getMission = function (id, callback) {
            $.get(ns.urls.getMissionsUrl + '/' + id)
                .done(function (data) {
                    callback(data);
                });
        };

        // load all mission
        self.loadMissions = function () {
            self.loading.push(true);

            $.getJSON(ns.urls.getMissionsUrl)
                .done(function (data) {
                    ko.mapping.fromJS(data, ns.missionMapping, self.missions);
                    self.loading.pop();
                });
        };

        // update the mission parameters
        self.updateMission = function (form) {
            $.post(ns.urls.editMissionUrl, $(form).serialize())
                .done(function (data) {
                    var match = ko.utils.arrayFirst(self.missions(), function (item) {
                        return item.Id() == self.selectedMission().Id;
                    });
                    if (match) {
                        ko.mapping.fromJS(data, { ignore: ["Team"] }, match);
                    }
                    else {
                        match = new ns.Mission(data);
                        self.missions.push(match);
                    }
                    self.selectedMission(match);
                });
        };

        // start the mission
        self.startMission = function (mission) {
            var id = mission.Id();
            $.post(ns.urls.startMissionUrl + '/' + id)
                .done(function (data) {
                    updateMission(id, data, { ignore: ["Team"] });
                });
        };

        // change an avengers assignment
        self.assignHero = function (form, url) {
            $.post(url, $(form).serialize())
                .done(function () {
                    var id = self.selectedMission().Id();
                    self.getMission(id, function (data) {
                        updateMission(id, data, {});
                    });
                });
        };

        // remove an avenger from the mission
        self.removeHero = function (form) {
            self.assignHero(form, ns.urls.removeHeroUrl);
        };

        // add an avenger to the mission
        self.addHero = function (form) {
            self.assignHero(form, ns.urls.addHeroUrl);
        };

        // internal mission updater
        var updateMission = function (id, data, opt) {
            var match = ko.utils.arrayFirst(self.missions(), function (item) {
                return item.Id() == id;
            });
            ko.mapping.fromJS(data, opt, match);
            self.selectedMission(match)
        };
        
        return this.init();
    };
    
})(window.officeAvenger = window.officeAvenger || {});