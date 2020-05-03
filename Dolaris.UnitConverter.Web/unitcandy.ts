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



class UnitElement {

    public constructor(element: JQuery) {

        this.ID = this.type = '';

        if (element !== null) {

            this.ID = element.data(UnitElement.UnitTextboxAttr);
            this.type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            this.element = element;
            this.value = element.val();
        }
    }


    public static UnitTextboxAttr = 'unit-textbox';
    public static UnitTypeAttr = 'unit-type';


    public ID: string;

    public type: string;

    public element: JQuery;

    public get value(): string {
        return this.element !== null ? this.element.val() : '';
    }

    public set value(s: string) {

        if (this.element !== null) {
            this.element.val(s);
        }
    }
}


class UnitCandyUI {

    public constructor() {

        this.initializeUnitElements();
        this.initializeClearButtons();
        this.initializeGotoUnitgroupButtons();

        //this.lastRecalculatedUnit = this.units[0];
    }


    public units: Array<UnitElement> = new Array<UnitElement>();

    /** sets the unit identified by unitID to the value 
        example: unitID = "NauticalMiles", value = "305.72" */
    public setUnitToValue(unitID: string, unitValue: string): void {

        const unit = this.getUnitById(unitID);

        unit.value = '';
        setTimeout(() => unit.value = unitValue, Math.random() * 500);
    }

    /** sets the cursor to Wait */
    public setWaitCursor(): void {
        document.body.style.cursor = "wait";
    }

    /** sets the cursor to Auto */
    public setAutoCursor(): void {
        document.body.style.cursor = "auto";
    }


    //protected textboxUnit = $('[data-unit-textbox]');
    protected buttonCopy = $('[data-button-copy]');
    protected buttonEmbed = $('[data-button-embed]');
    protected buttonClear = $('[data-button-clear]');
    protected buttonGotoUnitGroup = $('[data-goto-unitgroup]');

    public lastRecalculatedUnit: UnitElement;
    public lastFocusedUnit: UnitElement;


    protected initializeUnitElements(): void {

        $('[data-' + UnitElement.UnitTextboxAttr + ']').each((index, item) => {

            const unit = new UnitElement($(item));

            unit.element.keypress((e) => {

                const key = e.keyCode || e.which;

                if (key === 13) {

                    const element = $(e.target);
                    UI.lastRecalculatedUnit = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));

                    recalculateUnit = new Recalculate();
                    recalculateUnit.recalculate(element);
                }
            });

            unit.element.focusin((e) => {

                UI.lastFocusedUnit = new UnitElement($(e.target));
            });

            unit.element.focusout((e) => {

                const element = $(e.target);
                const unit = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));

                if (unit.value !== UI.lastRecalculatedUnit.value) {

                    UI.lastRecalculatedUnit = unit;

                    recalculateUnit = new Recalculate();
                    recalculateUnit.recalculate(element);
                }
            });

            this.units.push(unit);
        });
    }

    protected initializeClearButtons(): void {

        this.buttonClear.click((e) => {

            const element = $(e.target);
            const type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            const unitsOfSameType = this.getUnitsOfSameType(type);

            for (let i = 0; i < unitsOfSameType.length; i++) {

                setTimeout(() => unitsOfSameType[i].value = '', Math.random() * 500);
            }

            this.lastFocusedUnit.element.focus();
        });
    }

    protected initializeGotoUnitgroupButtons(): void {

        this.buttonGotoUnitGroup.on('click',
            (e) => {

                const unitgroupType = $(e.target).data('goto-unitgroup');
                this.showOnlyUnitGroup(unitgroupType);
            });
    }

    protected showOnlyUnitGroup(unitGroupType: string): void {

        const sectionUnitGroups = $('[data-' + UnitElement.UnitTypeAttr + ']');

        for (let i = 0; i < sectionUnitGroups.length; i++) {

            if ($(sectionUnitGroups[i]).data(UnitElement.UnitTypeAttr) === unitGroupType ?? '') {
                $(sectionUnitGroups[i]).show();

            } else {
                $(sectionUnitGroups[i]).hide();
            }
        }
    }

    protected getUnitById(id: string): UnitElement {

        for (let i = 0; i < this.units.length; i++) {

            if (this.units[i].ID === id) {
                return this.units[i];
            }
        }

        return null;
    }

    /** returns all units that are of the same type as the provided unit
        for example: "Fahrenheit" is a Temperature. The function returns "Fahrenheit", "Celsius", and "Kelvin". */
    protected getUnitsOfSameType(type: string): Array<UnitElement> {

        const unitsOfSameType = new Array<UnitElement>();

        for (let i = 0; i < this.units.length; i++) {

            if (this.units[i].type === type) {
                unitsOfSameType.push(this.units[i]);
            }
        }

        return unitsOfSameType;
    }
}



$(document).ready(() => {

    UI = new UnitCandyUI();


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
});






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

