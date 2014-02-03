(function (ns, undefined) {

    ns.Team = function(jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);

        var self = this;
        
    };

    ns.TeamMapping = {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (opt) {
            return new ns.Team(opt.data);
        }
    };
    
})(window.Avenger = window.Avenger || {});