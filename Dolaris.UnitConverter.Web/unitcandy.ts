/// <reference path="Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="Scripts/typings/jqueryui/jqueryui.d.ts"/>


let recalculateUnit: Recalculate;


class Recalculate {

    public constructor() { }

    public recalculate(unitElement: JQuery): void {

        const unitName = unitElement.data('unitname');  /* e.g. "Meter" */
        //const unitGroupName = unitElement.data('unitgroupname'); /* e.g. "Length" */
        const unitValue = unitElement.val();  /* e.g. 3.5 */

        $.ajax({
            url: 'UnitCandyService.svc/Recalculate',
            async: true,
            method: 'GET',
            data: { unitName: unitName, unitValue: unitValue },
            dataType: 'json',
            beforeSend: function () { document.body.style.cursor = "wait"; },

            success: (data, status, xhr) => {

                $.each(data.d,
                    (index, result) => {

                        const unitElement = $('[data-unitname="' + result.UnitName + '"]');

                        unitElement.val(result.UnitValue);
                        unitElement.attr('placeholder', '');
                    });


                document.body.style.cursor = "auto";

                return null;
            },

            error: function (xhr, status, error) {

                document.body.style.cursor = "auto";
                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                //alert('Error: ' + xhr.statusText + xhr.responseText);
                return null;
            }
        });
    }
}


$(document).ready(() => {

    const unitElements: JQuery = $('[data-id^="UnitTextBox-"]');

    unitElements.on('focusout',
        (e) => {

            const dataID = e.target.attributes['data-id'].value;
            const elementUnit: JQuery = $('[data-id="' + dataID + '"]');

            recalculateUnit = new Recalculate();
            recalculateUnit.recalculate(elementUnit);
        });

    unitElements.on('keypress',
        (e) => {

            const key = e.keyCode || e.which;

            if (key === 13) {

                const dataID = e.target.attributes['data-id'].value;
                const elementUnit: JQuery = $('[data-id="' + dataID + '"]');

                recalculateUnit = new Recalculate();
                recalculateUnit.recalculate(elementUnit);
            }
        });

    const elementAnyUnit: JQuery = $('#inputFindUnit');

    elementAnyUnit.on('keypress',
        (e) => {

            const key = e.keyCode || e.which;

            if (key === 13) {

                $.ajax({
                    url: 'UnitCandyService.svc/FindUnit',
                    async: true,
                    method: 'GET',
                    data: { inputstring: elementAnyUnit.val(), unitName: null },
                    dataType: 'json',
                    beforeSend: () => { document.body.style.cursor = "wait"; },

                    success: (data, status, xhr) => {

                        const dataID = 'UnitTextBox-' + data.d.UnitType + '-' + data.d.UnitName;
                        const unitElement = $('[data-id="' + dataID + '"]');

                        unitElement.val(data.d.UnitValue).focus().trigger($.Event("keypress", { which: 13 }));

                        document.body.style.cursor = "auto";

                        return null;
                    },

                    error: function (xhr, status, error) {

                        document.body.style.cursor = "auto";
                        console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                        //alert('Error: ' + xhr.statusText + xhr.responseText);
                        return null;
                    }
                });
            }
        });


    // DEBUG

    //data-test="isUnitElement"

        const test = $('[data-id^="UnitTextBox-"]').parents('[data-test="isUnitElement"]');
    try {

    } catch (e) {

    const stopp = 3;
    }


    ////////
});







//var lastUnitName = '';
//var lastUnitGroupName = '';
//var lastUnitValue = '';
//var anyUnitSelectedValue = '';



//function unitChangedError(err) {
//    document.body.style.cursor = "auto";
//}



//function unitChangedWithHelperAction(action, e) {

//    try {
//        var unitName = e.srcElement.parentElement.parentElement.parentElement.dataset.helper;
//        var controls = $('[data-unitname=' + unitName + ']');
//        var unitValue = controls[0].value;

//        document.body.style.cursor = "wait";

//        var recalc = www.unitcandy.com.ws.UnitCandyService.RecalculateWithHelperAction(unitName, unitValue, action, unitChangedCompleted, unitChangedError);

//    } catch (e) {
//    }

//    return false;
//}


//function copyUnitsToClipboard() {

//    var text = "";

//    var controls = $('[data-unitgroupname=' + lastUnitGroupName + ']');

//    for (var i = 0; i < controls.length; i++) {

//        text += controls[i].dataset.unitname + ":\t" + controls[i].value + controls[i].dataset.unitsymbol + "\n";
//    }

//    if (lastUnitGroupName != '') {
//        text += "\n" + getLinkToUnit(lastUnitGroupName);
//    }

//    copyToClipboard(text);
//}

//function copyLinkToClipboard(unitGroupName) {

//    if (unitGroupName === null) return;
//    if (unitGroupName === '') return;

//    var text = getLinkToUnit(unitGroupName);

//    if (text !== null) {
//        copyToClipboard(text);
//    }
//}

//function sendFeedback() {

//    var email = $('#emailAddr')[0].value;
//    var msg = $('#feedbackMsg')[0].value;

//    www.unitcandy.com.ws.UnitCandyService.SendFeedbackMail(email, msg, sendFeedbackCompleted);
//}

//function sendFeedbackCompleted(result) {

//    if (result === "") {
//        $('#EmailSuccess').show();
//        $('#EmailFailure').hide();

//    } else {
//        $('#EmailSuccess').hide();
//        $('#EmailFailure').show();

//        $('#EmailFailureMessage').text(result);
//    }
//}

//function copyToClipboard(text) {

//    var textarea = document.createElement("textarea");

//    try {
//        document.activeElement.appendChild(textarea);

//        textarea.value = text;
//        textarea.select();

//        document.execCommand("copy");

//        textarea.parentElement.removeChild(textarea);

//    } catch (e) {
//        textarea.parentElement.removeChild(textarea);
//    }
//}


//function findUnitKeyPressed(e) {

//    lastUnitName = 'AnyUnit';

//    var key = e.keyCode || e.which;
//    if (key === 13) {
//        findUnit();
//    }
//};

//function findUnitDropdownSelectionChanged(value) {
//    anyUnitSelectedValue = value;
//    findUnit();
//}

//function findUnit() {

//    var controls = $('#inputFindUnit');
//    var dropdown = $('#AnyUnitDropdown');

//    if (controls.length === 1) {
//        var f = www.unitcandy.com.ws.UnitCandyService.FindUnit(controls[0].value, anyUnitSelectedValue, findUnitComplete, findUnitError);
//    }

//    return false;
//}

//function findUnitComplete(result) {

//    try {
//        var controls = $('#inputFindUnit');

//        if (controls.length === 1) {
//            controls[0].value = result.UnitValue + ' ' + result.UnitName;
//        }

//        var recalc = www.unitcandy.com.ws.UnitCandyService.Recalculate(result.UnitName, result.UnitValue, unitChangedCompleted, unitChangedError);

//        var aTag = $('#' + result.UnitType);
//        $('html,body').animate({ scrollTop: aTag.offset().top }, 'easeInOutExpo');

//        anyUnitSelectedValue = '';
//        lastUnitName = result.UnitName;
//        lastUnitGroupName = result.UnitType;

//    } catch (e) {
//    }

//    return false;
//}

//function findUnitError(err) {

//    var controls = $('#inputFindUnit');

//    if (controls.length === 1) {
//        controls[0].value = '???';
//    }
//}


//function setFocusToLastUnitControl() {

//    if (lastUnitName === 'AnyUnit') {

//        findUnit();

//    } else if (lastUnitName !== '') {

//        var controls = $('[data-unitname=' + lastUnitName + ']');

//        if (controls.length === 1) {

//            try {
//                controls[0].focus();
//                controls[0].select();
//            } catch (e) {
//            }
//        }
//    }
//}

//function clearLastUnitGroup() {

//    if (lastUnitGroupName !== '') {

//        var lastUnitTextboxes = $('[data-unitgroupname=' + lastUnitGroupName + ']');

//        for (var i = 0; i < lastUnitTextboxes.length; i++) {
//            lastUnitTextboxes[i].value = '';
//        }
//    }

//    var controls = $('#inputFindUnit');

//    if (controls.length === 1) {
//        controls[0].value = '';
//        anyUnitSelectedValue = '';
//    }
//}

//function clearUnitGroup(unitGroupName) {

//    var lastUnitTextboxes = $('[data-unitgroupname=' + unitGroupName + ']');

//    for (var i = 0; i < lastUnitTextboxes.length; i++) {
//        lastUnitTextboxes[i].value = '';
//    }
//}

//function clearAllUnitGroups() {

//    var unitTextboxes = $('[data-unitgroupname]');

//    for (var i = 0; i < unitTextboxes.length; i++) {
//        unitTextboxes[i].value = '';
//    }
//}

//function getLinkToUnit(unitGroupName) {

//    var text = null;

//    if (lastUnitName != '' && lastUnitValue !== '') {
//        text = document.location.origin + "?" + lastUnitValue + lastUnitName;

//    } else if (unitGroupName !== null) {
//        text = document.location.origin + "#" + unitGroupName;
//    }

//    return text;
//}




//window.onload = function () {

//    var myUrl = '';

//    var uc1 = 'https://www.unitcandy.com';
//    var uc2 = uc1 + '/';

//    if (myUrl.toUpperCase() === uc1.toUpperCase() || myUrl.toUpperCase() === uc2.toUpperCase()) {
//        // there is not parameter in the URL so no need to go to the server
//        return;
//    }

//    //www.unitcandy.com.ws.UnitCandyService.FindUnitFromUrl(window.location.href, findUnitComplete, findUnitError);

//    $('#EmailSuccess').hide();
//    $('#EmailFailure').hide();
//}

//$('input').on('focus', function (e) {

//    var unitname = e.srcElement.dataset.unitname;

//    if (unitname !== null) {

//        $('[data-helper]').hide();
//        $('[data-helper=' + unitname + ']').show();
//    }
//});

//// on very small screens hide the menu after clicking on an item
//$(document).on('click', '.navbar-collapse.in', function (e) {
//    if ($(e.target).is('a')) {
//        $(this).collapse('hide');
//    }
//});

//$(function () {
//    $('[data-toggle="tooltip"]').tooltip({ delay: { show: 250, hide: 500 } })
//})

