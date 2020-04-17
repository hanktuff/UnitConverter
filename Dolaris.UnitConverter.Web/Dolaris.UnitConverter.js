﻿
var lastUnitName = '';
var lastUnitGroupName = '';
var lastUnitValue = '';
var anyUnitSelectedValue = '';


function unitChangedKeyPressed(e) {

    lastUnitName = e.srcElement.dataset.unitname;
    lastUnitGroupName = e.srcElement.dataset.unitgroupname;
    lastUnitValue = e.srcElement.value;

    var key = e.keyCode || e.which;
    if (key === 13) {
        unitChanged(e);
    }
};


function unitChanged(e) {

    var unitName = e.srcElement.dataset.unitname;
    var unitGroupName = e.srcElement.dataset.unitgroupname;
    var unitValue = e.srcElement.value;

    document.body.style.cursor = "wait";

    var recalc = www.unitcandy.com.ws.UnitCandyService.Recalculate(unitName, unitValue, unitChangedCompleted, unitChangedError);
    return false;
};

function unitChangedCompleted(result) {

    if (result !== null) {

        for (var i = 0; i < result.length; i++) {

            var unitName = result[i].UnitName;
            var unitValue = result[i].UnitValue;
            var errorString = result[i].ErrorString;

            var controls = $('[data-unitname=' + unitName + ']');

            if (controls.length === 1) {

                if (errorString === '') {
                    controls[0].value = unitValue;
                    controls[0].placeholder = '';

                } else {
                    controls[0].value = '';
                    controls[0].placeholder = errorString;
                }

                controls[0].disabled = false;
            }
        }
    }

    document.body.style.cursor = "auto";
};

function unitChangedError(err) {
    document.body.style.cursor = "auto";
};



function gotoInfoArea() {

    var aTag = $('#ContactUnitCandy');
    $('html,body').animate({ scrollTop: aTag.offset().top }, 2000);
}




function unitChangedWithHelperAction(action, e) {

    try {
        var unitName = e.srcElement.parentElement.parentElement.parentElement.dataset.helper;
        var controls = $('[data-unitname=' + unitName + ']');
        var unitValue = controls[0].value;

        document.body.style.cursor = "wait";

        var recalc = www.unitcandy.com.ws.UnitCandyService.RecalculateWithHelperAction(unitName, unitValue, action, unitChangedCompleted, unitChangedError);

    } catch (e) {
    }

    return false;
}


function copyUnitsToClipboard() {

    var text = "";

    var controls = $('[data-unitgroupname=' + lastUnitGroupName + ']');

    for (var i = 0; i < controls.length; i++) {

        text += controls[i].dataset.unitname + ":\t" + controls[i].value + controls[i].dataset.unitsymbol + "\n";
    }

    if (lastUnitGroupName != '') {
        text += "\n" + getLinkToUnit(lastUnitGroupName);
    }

    copyToClipboard(text);
}

function copyLinkToClipboard(unitGroupName) {

    if (unitGroupName === null) return;
    if (unitGroupName === '') return;

    var text = getLinkToUnit(unitGroupName);

    if (text !== null) {
        copyToClipboard(text);
    }
}

function sendFeedback() {

    var email = $('#emailAddr')[0].value;
    var msg = $('#feedbackMsg')[0].value;
    
    www.unitcandy.com.ws.UnitCandyService.SendFeedbackMail(email, msg, sendFeedbackCompleted);
}

function sendFeedbackCompleted(result) {

    if (result === "") {
        $('#EmailSuccess').show();
        $('#EmailFailure').hide();

    } else {
        $('#EmailSuccess').hide();
        $('#EmailFailure').show();

        $('#EmailFailureMessage').text(result);
    }
}

function copyToClipboard(text) {

    var textarea = document.createElement("textarea");

    try {
        document.activeElement.appendChild(textarea);

        textarea.value = text;
        textarea.select();

        document.execCommand("copy");

        textarea.parentElement.removeChild(textarea);

    } catch (e) {
        textarea.parentElement.removeChild(textarea);
    }
}


function findUnitKeyPressed(e) {

    lastUnitName = 'AnyUnit';

    var key = e.keyCode || e.which;
    if (key === 13) {
        findUnit();
    }
};

function findUnitDropdownSelectionChanged(value) {
    anyUnitSelectedValue = value;
    findUnit();
}

function findUnit() {

    var controls = $('#inputFindUnit');
    var dropdown = $('#AnyUnitDropdown');

    if (controls.length === 1) {
        var f = www.unitcandy.com.ws.UnitCandyService.FindUnit(controls[0].value, anyUnitSelectedValue, findUnitComplete, findUnitError);
    }

    return false;
}

function findUnitComplete(result) {

    try {
        var controls = $('#inputFindUnit');

        if (controls.length === 1) {
            controls[0].value = result.UnitValue + ' ' + result.UnitName;
        }

        var recalc = www.unitcandy.com.ws.UnitCandyService.Recalculate(result.UnitName, result.UnitValue, unitChangedCompleted, unitChangedError);

        var aTag = $('#' + result.UnitType);
        $('html,body').animate({ scrollTop: aTag.offset().top }, 'easeInOutExpo');

        anyUnitSelectedValue = '';
        lastUnitName = result.UnitName;
        lastUnitGroupName = result.UnitType;

    } catch (e) {
    }

    return false;
}

function findUnitError(err) {

    var controls = $('#inputFindUnit');

    if (controls.length === 1) {
        controls[0].value = '???';
    }
}


function setFocusToLastUnitControl() {

    if (lastUnitName === 'AnyUnit') {

        findUnit();

    } else if (lastUnitName !== '') {

        var controls = $('[data-unitname=' + lastUnitName + ']');

        if (controls.length === 1) {

            try {
                controls[0].focus();
                controls[0].select();
            } catch (e) {
            }
        }
    }
}

function clearLastUnitGroup() {

    if (lastUnitGroupName !== '') {

        var lastUnitTextboxes = $('[data-unitgroupname=' + lastUnitGroupName + ']');

        for (var i = 0; i < lastUnitTextboxes.length; i++) {
            lastUnitTextboxes[i].value = '';
        }
    }

    var controls = $('#inputFindUnit');

    if (controls.length === 1) {
        controls[0].value = '';
        anyUnitSelectedValue = '';
    }
}

function clearUnitGroup(unitGroupName) {

    var lastUnitTextboxes = $('[data-unitgroupname=' + unitGroupName + ']');

    for (var i = 0; i < lastUnitTextboxes.length; i++) {
        lastUnitTextboxes[i].value = '';
    }
}

function clearAllUnitGroups() {

    var unitTextboxes = $('[data-unitgroupname]');

    for (var i = 0; i < unitTextboxes.length; i++) {
        unitTextboxes[i].value = '';
    }
}

function getLinkToUnit(unitGroupName) {

    var text = null;

    if (lastUnitName != '' && lastUnitValue !== '') {
        text = document.location.origin + "?" + lastUnitValue + lastUnitName;

    } else if (unitGroupName !== null) {
        text = document.location.origin + "#" + unitGroupName;
    }

    return text;
}

function debug_alert(message) {

    if (message === null) {
        message = '';
    }

    alert(message);
}



window.onload = function () {

    var myUrl = '';

    var uc1 = 'https://www.unitcandy.com';
    var uc2 = uc1 + '/';

    if (myUrl.toUpperCase() === uc1.toUpperCase() || myUrl.toUpperCase() === uc2.toUpperCase()) {
        // there is not parameter in the URL so no need to go to the server
        return;
    }

    //www.unitcandy.com.ws.UnitCandyService.FindUnitFromUrl(window.location.href, findUnitComplete, findUnitError);

    $('#EmailSuccess').hide();
    $('#EmailFailure').hide();
}

$('input').on('focus', function (e) {

    var unitname = e.srcElement.dataset.unitname;

    if (unitname !== null) {

        $('[data-helper]').hide();
        $('[data-helper=' + unitname + ']').show();
    }
});

// on very small screens hide the menu after clicking on an item
$(document).on('click', '.navbar-collapse.in', function (e) {
    if ($(e.target).is('a')) {
        $(this).collapse('hide');
    }
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip({ delay: { show: 250, hide: 500 } })
})