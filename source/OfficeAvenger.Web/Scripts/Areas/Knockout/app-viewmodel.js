(function (ns, $, undefined) {
    
    var baseAddress = '/api';
    var getTeamUrl = baseAddress + '/team';
    var getMissionsUrl = baseAddress + '/mission';

    ns.ViewModel = function() {
        var self = this;

        // props
        self.Team = ko.observableArray([]);
        self.Missions = ko.observableArray([]);

        // methods
        self.loadTeam = function () {
            self.loading.push(true);

            $.getJSON(getTeamUrl)
                 .done(function (data) {
                     ko.mapping.fromJS(data, ns.TeamMapping, self.Team);
                     self.loading.pop();
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