(function (ns, undefined) {

    // mapping json to avengers
    ns.emptyAvenger = { Id: "", Name: "", Avatar: "" };
    ns.avengerMapping = {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (opt) {
            return new ns.Avenger(opt.data);
        }
    };

    // model
    ns.Avenger = function (jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);
    };

    // view model
    ns.TeamViewModel = function () {
        var self = this;

        self.loading = ko.observableArray([]);
        self.team = ko.observableArray();
        self.selectedAvenger = ko.observable(ns.emptyAvenger);

        // initialize knockout bindings
        self.init = function () {
            ko.applyBindings(self, ns.views.teamView);
            ko.applyBindings(self, ns.views.editTeamModal);
            self.loadTeams();
            return self;
        };

        // adds a new avenger to the team
        self.addTeam = function () {
            self.editTeam(ns.emptyAvenger);
        };

        // delete the selected team
        self.deleteTeam = function (avenger) {
            $.ajax({
                url: ns.urls.deleteHeroUrl + '/' + avenger.Id(),
                type: 'DELETE'
            }).done(function (data) {
                self.team.pop(avenger);
            });
        };

        // edit the selected avenger
        self.editTeam = function (avenger) {
            self.selectedAvenger(avenger);
            ns.modal.open(ns.views.editTeamModal);
        };

        // load the team
        self.loadTeams = function () {
            self.loading.push(true);
            $.getJSON(ns.urls.getTeamUrl)
                 .done(function (data) {
                     ko.mapping.fromJS(data, ns.teamMapping, self.team);
                     self.loading.pop();
                 });
        };

        // update the selected avenger
        self.updateTeam = function (form) {
            ns.modal.close(ns.views.editTeamModal);
            $.post(ns.urls.editTeamUrl, $(form).serialize())
                .done(function (data) {
                    var match = ko.utils.arrayFirst(self.team(), function (item) {
                        return item.Id == self.selectedAvenger().Id;
                    });
                    if (match)
                        ko.mapping.fromJS(data, {}, match);
                    else
                        self.team.push(new ns.Avenger(data));
                });
        };

        return this.init();
    };

})(window.officeAvenger = window.officeAvenger || {});