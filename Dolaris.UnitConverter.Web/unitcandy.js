/// <reference path="Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="Scripts/typings/jqueryui/jqueryui.d.ts"/>
var recalculateUnit;
var UI;
var Recalculate = /** @class */ (function () {
    function Recalculate() {
    }
    Recalculate.prototype.recalculate = function (unitElement) {
        //const unitName = unitElement.data('unit-textbox');  /* e.g. "Meter" */
        //const unitGroupName = unitElement.data('unitgroupname'); /* e.g. "Length" */
        //const unitValue = unitElement.val();  /* e.g. 3.5 */
        $.ajax({
            url: 'UnitCandyService.svc/Recalculate',
            async: true,
            method: 'GET',
            data: { unitName: unitElement.name, unitValue: unitElement.value },
            dataType: 'json',
            beforeSend: function () { UI.setWaitCursor(); setTimeout(function () { return UI.setAutoCursor(); }, 500); },
            success: function (data, status, xhr) {
                $.each(data.d, function (index, result) {
                    UI.setUnitToValue(result.UnitName, result.UnitValue);
                });
                return null;
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                //alert('Error: ' + xhr.statusText + xhr.responseText);
                return null;
            }
        });
    };
    return Recalculate;
}());
var UnitElement = /** @class */ (function () {
    function UnitElement(element) {
        this.ID = this.type = '';
        if (element !== null) {
            this.element = element;
            this.ID = element.data(UnitElement.UnitTextboxAttr);
            this.type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            this.name = element.data('unit-name');
            this.plural = element.data('unit-plural');
            this.symbol = element.data('unit-symbol');
            this.isBaseUnit = element.data('unit-baseunit') === 'true' ? true : false;
        }
    }
    Object.defineProperty(UnitElement.prototype, "value", {
        get: function () {
            return this.element !== null ? this.element.val() : '';
        },
        set: function (s) {
            if (this.element !== null) {
                this.element.val(s);
            }
        },
        enumerable: true,
        configurable: true
    });
    UnitElement.UnitTextboxAttr = 'unit-textbox';
    UnitElement.UnitTypeAttr = 'unit-type';
    return UnitElement;
}());
var UnitCandyUI = /** @class */ (function () {
    function UnitCandyUI() {
        this.units = new Array();
        this.copyButtons = $('[data-button-copy]');
        /* protected embedButtons = $('[data-button-embed]'); to be implemented later */
        this.clearButtons = $('[data-button-clear]');
        this.buttonGotoUnitGroup = $('[data-goto-unitgroup]');
        this.initializeUnitElements();
        this.initializeCopyButtons();
        this.initializeClearButtons();
        this.initializeGotoUnitgroupButtons();
    }
    /** sets the unit identified by unitID to the value
        example: unitID = "NauticalMiles", value = "305.72" */
    UnitCandyUI.prototype.setUnitToValue = function (unitID, unitValue) {
        var unit = this.getUnitById(unitID);
        if (unit.ID !== this.lastRecalculatedUnit.ID) {
            unit.value = '';
            setTimeout(function () { return unit.value = unitValue; }, Math.random() * 500);
        }
    };
    UnitCandyUI.prototype.recalculateUnit = function (unit, forceRecalc) {
        if (forceRecalc === void 0) { forceRecalc = false; }
        if (forceRecalc === false) {
            if (this.lastRecalculatedUnit !== undefined) {
                if (unit.ID === this.lastRecalculatedUnit.ID && unit.value === this.lastRecalculatedUnit.previousValue) {
                    return;
                }
            }
        }
        UI.lastRecalculatedUnit = unit;
        UI.lastRecalculatedUnit.previousValue = unit.value;
        recalculateUnit = new Recalculate();
        recalculateUnit.recalculate(unit);
    };
    /** sets the cursor to Wait */
    UnitCandyUI.prototype.setWaitCursor = function () {
        document.body.style.cursor = "wait";
    };
    /** sets the cursor to Auto */
    UnitCandyUI.prototype.setAutoCursor = function () {
        document.body.style.cursor = "auto";
    };
    // copy to clipboard
    UnitCandyUI.prototype.copyTextToClipboard = function (text) {
        var textarea = document.createElement("textarea");
        try {
            document.activeElement.appendChild(textarea);
            textarea.value = text;
            textarea.textContent = text;
            textarea.select();
            document.execCommand("copy");
            textarea.parentElement.removeChild(textarea);
        }
        catch (e) {
            textarea.parentElement.removeChild(textarea);
        }
    };
    UnitCandyUI.prototype.initializeUnitElements = function () {
        var _this = this;
        $('[data-' + UnitElement.UnitTextboxAttr + ']').each(function (index, item) {
            var unit = new UnitElement($(item));
            unit.element.on('keypress', function (e) {
                var key = e.keyCode || e.which;
                if (key === 13) {
                    var element = $(e.target);
                    var unitToRecalculate = _this.getUnitById(element.data(UnitElement.UnitTextboxAttr));
                    _this.recalculateUnit(unitToRecalculate);
                }
            });
            unit.element.focusin(function (e) {
                UI.lastFocusedUnit = new UnitElement($(e.target));
                $('#DEBUG_lastfocus').text(UI.lastFocusedUnit.ID);
            });
            unit.element.on('focusout', function (e) {
                //const element = $(e.target);
                //const unit = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));
                //this.recalculateUnit(unit);
            });
            _this.units.push(unit);
        });
    };
    UnitCandyUI.prototype.initializeCopyButtons = function () {
        var _this = this;
        this.copyButtons.click(function (e) {
            var element = $(e.target);
            var type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            var unitsOfSameType = _this.getUnitsOfSameType(type);
            var text = type + '\n';
            for (var i = 0; i < unitsOfSameType.length; i++) {
                text += unitsOfSameType[i].plural + ': ' + unitsOfSameType[i].value + ' ' + unitsOfSameType[i].symbol + '\n';
            }
            text += 'https://www.unitcandy.com?#' + type + '\n';
            _this.copyTextToClipboard(text);
            _this.lastFocusedUnit.element.focus();
        });
    };
    UnitCandyUI.prototype.initializeClearButtons = function () {
        var _this = this;
        this.clearButtons.click(function (e) {
            var element = $(e.target);
            var type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            var unitsOfSameType = _this.getUnitsOfSameType(type);
            var _loop_1 = function (i) {
                setTimeout(function () { return unitsOfSameType[i].value = ''; }, Math.random() * 500);
            };
            for (var i = 0; i < unitsOfSameType.length; i++) {
                _loop_1(i);
            }
            _this.lastFocusedUnit.element.focus();
        });
    };
    UnitCandyUI.prototype.initializeGotoUnitgroupButtons = function () {
        var _this = this;
        this.buttonGotoUnitGroup.on('click', function (e) {
            var unitgroupType = $(e.target).data('goto-unitgroup');
            _this.showOnlyUnitGroup(unitgroupType);
        });
    };
    UnitCandyUI.prototype.showOnlyUnitGroup = function (unitGroupType) {
        var _a;
        var sectionUnitGroups = $('[data-' + UnitElement.UnitTypeAttr + ']');
        for (var i = 0; i < sectionUnitGroups.length; i++) {
            if ((_a = $(sectionUnitGroups[i]).data(UnitElement.UnitTypeAttr) === unitGroupType) !== null && _a !== void 0 ? _a : '') {
                $(sectionUnitGroups[i]).fadeIn(500);
            }
            else {
                $(sectionUnitGroups[i]).hide();
            }
        }
    };
    UnitCandyUI.prototype.getUnitById = function (id) {
        for (var i = 0; i < this.units.length; i++) {
            if (this.units[i].ID === id) {
                return this.units[i];
            }
        }
        return null;
    };
    /** returns all units that are of the same type as the provided unit
        for example: "Fahrenheit" is a Temperature. The function returns "Fahrenheit", "Celsius", and "Kelvin". */
    UnitCandyUI.prototype.getUnitsOfSameType = function (type) {
        var unitsOfSameType = new Array();
        for (var i = 0; i < this.units.length; i++) {
            if (this.units[i].type === type) {
                unitsOfSameType.push(this.units[i]);
            }
        }
        return unitsOfSameType;
    };
    return UnitCandyUI;
}());
$(document).ready(function () {
    UI = new UnitCandyUI();
    var elementAnyUnit = $('#inputFindUnit');
    elementAnyUnit.on('keypress', function (e) {
        var key = e.keyCode || e.which;
        if (key === 13) {
            $.ajax({
                url: 'UnitCandyService.svc/FindUnit',
                async: true,
                method: 'GET',
                data: { inputstring: elementAnyUnit.val(), unitName: null },
                dataType: 'json',
                beforeSend: function () { document.body.style.cursor = "wait"; },
                success: function (data, status, xhr) {
                    var dataID = 'UnitTextBox-' + data.d.UnitType + '-' + data.d.UnitName;
                    var unitElement = $('[data-id="' + dataID + '"]');
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
//# sourceMappingURL=unitcandy.js.map