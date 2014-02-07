(function (ns, undefined) {

    ns.modal = {
        open: function (elementId) {
            $(elementId).foundation('reveal', 'open');
        },
        close: function (elementId) {
            $(elementId).foundation('reveal', 'close');
        }
    };

    ns.views = {
        teamView:           document.getElementById('team-view'),
        missionsView:       document.getElementById('missions-view'),
        editTeamModal:      document.getElementById('editTeamModal'),
        editMissionModal:   document.getElementById('editMissionModal')
    };

    var baseAddress = '/api';
    ns.urls = {
        getTeamUrl:         baseAddress + '/team',
        editTeamUrl:        baseAddress + '/team',
        getMissionsUrl:     baseAddress + '/mission',
        editMissionUrl:     baseAddress + '/mission/update',
        deleteMissionUrl:   baseAddress + '/mission',
        startMissionUrl:    baseAddress + '/mission/start',
        addHeroUrl:         baseAddress + '/mission/assign',
        removeHeroUrl:      baseAddress + '/mission/remove',
        deleteHeroUrl:      baseAddress + '/team'
    };

})(window.officeAvenger = window.officeAvenger || {});