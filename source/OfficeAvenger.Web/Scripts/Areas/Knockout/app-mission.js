(function (ns) {

    var baseAddress = '/api';
    var editMissionUrl = baseAddress + '/mission/';
    var startMissionUrl = baseAddress + '/mission/start';
    
    ns.Mission = function(jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);

        // props
        var self = this;
        self.editMissionUri = ko.computed(function () {
            return editMissionUrl + self.Id();
        });

        // methods
        self.startMission = function (form) {
            var id = $('[name=id]', form).val();
            $.post(startMissionUrl + '/' + id)
                .done(function (data) {
                    ko.mapping.fromJS(data, {}, this);
                });
            event.preventDefault();
        };
    };

    ns.MissionMapping = {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (opt) {
            return new ns.Mission(opt.data);
        }
    };
    
})(window.Avenger = window.Avenger || {});