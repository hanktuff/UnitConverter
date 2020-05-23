/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>



$(document).ready(() => {

    // register popovers
    $('[data-toggle="popover"]').popover();

    // in case uri has params
    const ucui = new UnitCandyUI();
    ucui.setUnitFromUri(location.href);

    // if icon clicked, reload the page
    $('#unitcandy-icon').on('click', () => { setTimeout(() => { window.location.href = location.origin + location.pathname }, 1000); });
});
