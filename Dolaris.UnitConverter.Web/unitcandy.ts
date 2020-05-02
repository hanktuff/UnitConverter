/// <reference path="Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="Scripts/typings/jqueryui/jqueryui.d.ts"/>


let recalculateUnit: Recalculate;
let UI: UnitCandyUI;


class Recalculate {

    public constructor() { }

    public recalculate(unitElement: JQuery): void {

        const unitName = unitElement.data('unit-textbox');  /* e.g. "Meter" */
        //const unitGroupName = unitElement.data('unitgroupname'); /* e.g. "Length" */
        const unitValue = unitElement.val();  /* e.g. 3.5 */

        $.ajax({
            url: 'UnitCandyService.svc/Recalculate',
            async: true,
            method: 'GET',
            data: { unitName: unitName, unitValue: unitValue },
            dataType: 'json',
            beforeSend: function () { UI.setWaitCursor(); setTimeout(() => UI.setAutoCursor(), 500); },

            success: (data, status, xhr) => {

                $.each(data.d,
                    (index, result) => {
                        UI.setUnitToValue(result.UnitName, result.UnitValue)
                    });

                return null;
            },

            error: (xhr, status, error) => {
                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                //alert('Error: ' + xhr.statusText + xhr.responseText);
                return null;
            }
        });
    }
}



class UnitCandyUI {

    public constructor() {

        this.initializeTextBoxes();
        this.initializeClearButtons();
        this.initializeGotoUnitgroupButtons();
    }

    /** sets the unit identified by unitID to the value 
        example: unitID = "NauticalMiles", value = "305.72" */
    public setUnitToValue(unitID: string, value: string): void {

        const element = this.textboxUnit.filter('[data-unit-textbox="' + unitID + '"]');

        element.val('');
        setTimeout(() => element.val(value), Math.random() * 500);
    }

    /** sets the cursor to Wait */
    public setWaitCursor(): void {
        document.body.style.cursor = "wait";
    }

    /** sets the cursor to Auto */
    public setAutoCursor(): void {
        document.body.style.cursor = "auto";
    }


    protected textboxUnit = $('[data-unit-textbox]');
    protected buttonCopy = $('[data-button-copy]');
    protected buttonEmbed = $('[data-button-embed]');
    protected buttonClear = $('[data-button-clear]');
    protected buttonGotoUnitGroup = $('[data-goto-unitgroup]');

    protected initializeTextBoxes(): void {

        this.textboxUnit.on('keypress',
            (e) => {

                const key = e.keyCode || e.which;

                if (key === 13) {

                    const element: JQuery = $(e.target);

                    recalculateUnit = new Recalculate();
                    recalculateUnit.recalculate(element);
                }
            });
    }

    protected initializeClearButtons(): void {

        this.buttonClear.on('click',
            (e) => {

                this.GetUnitsOfSameType(e.target).each(
                    (index, item) => setTimeout(() => $(item).val(''), Math.random() * 1000)
                );
        });
    }

    protected initializeGotoUnitgroupButtons(): void {

        this.buttonGotoUnitGroup.on('click',
            (e) => {

                const scrolllTarget = $(e.target).data('goto-unitgroup');
                alert(scrolllTarget);
            });
    }

    /** returns all units that are of the same type as the provided unit
        for example: "Fahrenheit" is a Temperature. The function returns "Fahrenheit", "Celsius", and "Kelvin". */
    protected GetUnitsOfSameType(unit: Element): JQuery {

        const result: Array<Element> = new Array<Element>();

        const unitType = $(unit).parents('[data-unit-type]').data('unit-type');

        this.textboxUnit.each((index, item) => {

            if ($(item).parents('[data-unit-type="' + unitType + '"]').length > 0) {

                result.push(item);
            }
        });

        return $(result);
    }
}



$(document).ready(() => {

    //const unitElements: JQuery = $('[data-unit-id]');

    //unitElements.on('focusout',
    //    (e) => {

    //        const element: JQuery = $('#' + e.target.id);

    //        recalculateUnit = new Recalculate();
    //        recalculateUnit.recalculate(element);
    //    });

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


    UI = new UnitCandyUI();
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

