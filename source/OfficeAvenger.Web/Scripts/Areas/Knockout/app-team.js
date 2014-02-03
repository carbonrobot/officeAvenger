(function (ns, undefined) {

    ns.Team = function(jsonObject) {
        ko.mapping.fromJS(jsonObject, {}, this);

        var self = this;
        
    };
    
})(window.Avenger = window.Avenger || {});