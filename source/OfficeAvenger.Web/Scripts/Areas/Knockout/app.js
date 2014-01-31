var baseAddress = '/api';
var getTeamUrl = baseAddress + '/team';

function AvengerViewModel() {
    var self = this;

    self.Team = ko.observableArray([]);

    self.loadTeam = function () {
        self.loading.push(true);

        $.ajax({
            dataType: 'json',
            url: getTeamUrl,
            xhrFields: {
                withCredentials: true
            }
        }).done(function (data) {
                self.Team(data);
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
});

