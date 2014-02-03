(function (ns, $, undefined) {
    
    var baseAddress = '/api';
    var getTeamUrl = baseAddress + '/team';
    var getMissionsUrl = baseAddress + '/mission';
    

    var missionMapping = {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (opt) {
            return new ns.Mission(opt.data);
        }
    };

    ns.ViewModel = function() {
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
                    ko.mapping.fromJS(data, missionMapping, self.Missions);
                    self.loading.pop();
                });
        };

        // for tracking loading state
        self.loading = ko.observableArray();
    };
    
})(window.Avenger = window.Avenger || {}, jQuery);