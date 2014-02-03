(function (ns) {

    ns.Mission = function(jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);

        var self = this;
        // no computed props yet
    };

    ns.MissionMapping = {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (opt) {
            return new ns.Mission(opt.data);
        },
        "Team": ns.TeamMapping
    };

    ns.EmptyMission = { Id: "", Name: "", Duration: "" };
    
})(window.Avenger = window.Avenger || {});