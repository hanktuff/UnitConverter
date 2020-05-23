/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>
$(document).ready(function () {
    // register popovers
    $('[data-toggle="popover"]').popover();
    // in case uri has params
    var ucui = new UnitCandyUI();
    ucui.setUnitFromUri(location.href);
    // if icon clicked, reload the page
    $('#unitcandy-icon').on('click', function () { setTimeout(function () { window.location.href = location.origin + location.pathname; }, 1000); });
});
//# sourceMappingURL=unitcandy.js.map