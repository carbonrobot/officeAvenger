(function (ns) {

    var baseAddress = '/api';
    var editMissionUrl = baseAddress + '/mission/';
    var startMissionUrl = baseAddress + '/mission/start';
    
    ns.Mission = function(jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);

        var self = this;
        self.missionStarted = ko.computed(function() {
            return self.MissionStart() != null;
        });
        self.editMissionUri = ko.computed(function () {
            //return editMissionUrl + self.Id;
            return 1;
        });

        self.startMission = function (form) {
            var id = $('[name=id]', form).val();
            $.post(startMissionUrl + '/' + id)
                .done(function (data) {
                    ko.mapping.fromJS(data, {}, this);
                });
            event.preventDefault();
        };
    };
    
})(window.Avenger = window.Avenger || {});