/// <reference path="Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="Scripts/typings/jqueryui/jqueryui.d.ts"/>
var recalculateUnit;
var UI;
var Recalculate = /** @class */ (function () {
    function Recalculate() {
    }
    Recalculate.prototype.recalculate = function (unitElement) {
        $.ajax({
            url: 'UnitCandyService.svc/Recalculate',
            async: true,
            method: 'GET',
            data: { unitName: unitElement.ID, unitValue: unitElement.value },
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
    Recalculate.prototype.recalculateWithHelperAction = function (unitElement, helperAction) {
        $.ajax({
            url: 'UnitCandyService.svc/RecalculateWithHelperAction',
            async: true,
            method: 'GET',
            data: { unitName: unitElement.ID, unitValue: unitElement.value, action: helperAction },
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
    Recalculate.prototype.findUnit = function (findstr) {
        $.ajax({
            url: 'UnitCandyService.svc/FindUnit',
            async: true,
            method: 'GET',
            data: { inputstring: findstr, unitName: null },
            dataType: 'json',
            beforeSend: function () { UI.setWaitCursor(); setTimeout(function () { return UI.setAutoCursor(); }, 1000); },
            success: function (data, status, xhr) {
                var unitName = data.d !== null ? data.d.UnitName : null;
                var unitValue = data.d !== null ? data.d.UnitValue : null;
                UI.setUnitFromFindstr(unitName, unitValue);
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
            this.isBaseUnit = element.data('unit-baseunit') === 'True' ? true : false;
            this.elementHelperGroup = $('[data-' + UnitElement.UnitHelperGroupAttr + '="' + this.ID + '"]');
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
    UnitElement.UnitHelperGroupAttr = 'unit-helper-group';
    return UnitElement;
}());
var UnitCandyUI = /** @class */ (function () {
    function UnitCandyUI() {
        this.units = new Array();
        this.buttonGotoUnitGroup = $('[data-goto-unitgroup]');
        this.copyButtons = $('[data-button-copy]');
        this.embedButtons = $('[data-button-embed]');
        this.clearButtons = $('[data-button-clear]');
        this.unitHelperGroup = $('[data-' + UnitElement.UnitHelperGroupAttr + ']');
        this.anyUnitTextBox = $('#any-unit');
        this.initializeUnitElements();
        this.initializeCopyButtons();
        this.initializeClearButtons();
        this.initializeGotoUnitgroupButtons();
        this.initializeAnyUnitTextBox();
        $('#unitcandy-icon').on('click', function () { setTimeout(function () { window.location.href = location.origin + location.pathname; }, 1000); });
    }
    /** sets the unit identified by unitID to the value
        example: unitID = "NauticalMiles", value = "305.72" */
    UnitCandyUI.prototype.setUnitToValue = function (unitID, unitValue) {
        var unit = this.getUnitById(unitID);
        if (this.lastRecalculatedUnit === undefined || unit.ID !== this.lastRecalculatedUnit.ID) {
            unit.value = '';
            setTimeout(function () { return unit.value = unitValue; }, Math.random() * 500);
        }
        else {
            unit.value = unitValue;
        }
    };
    UnitCandyUI.prototype.setUnitFromFindstr = function (unitID, unitValue) {
        var _this = this;
        if (unitID !== null) {
            var unit = this.getUnitById(unitID);
            unit.value = unitValue;
            this.showUnitGroup(unit.type);
            this.recalculateUnit(unit, true);
            this.anyUnitTextBox.val(unit.value + ' ' + unit.symbol);
        }
        else {
            this.anyUnitTextBox.val('');
            this.anyUnitTextBox.popover('show');
            setTimeout(function () { return _this.anyUnitTextBox.popover('hide'); }, 3000);
        }
    };
    UnitCandyUI.prototype.setUnitFromUri = function (uri) {
        // handle uri with parameters
        // for example: https://www.unitcandy.com?68Fahrenheit
        var indexOfQuestionMark = uri.indexOf('?');
        if (indexOfQuestionMark < 0) {
            return;
        }
        var param = location.href.substring(indexOfQuestionMark + 1);
        for (var i = 0; i < this.units.length; i++) {
            var unitGroupType = this.units[i].type;
            if (unitGroupType.toLowerCase() === param.toLowerCase()) {
                this.showUnitGroup(unitGroupType);
                this.getBaseUnitOrDefault(unitGroupType).element.focus();
                return;
            }
        }
        recalculateUnit.findUnit(param);
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
    /** copy provided text to clipboard */
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
            unit.element.on('focusin', function (e) {
                var element = $(e.target);
                var unit = _this.getUnitById(element.data(UnitElement.UnitTextboxAttr));
                if (_this.lastUnitHelperGroup !== undefined) {
                    _this.lastUnitHelperGroup.hide();
                }
                unit.elementHelperGroup.show();
                _this.lastUnitHelperGroup = unit.elementHelperGroup;
            });
            _this.units.push(unit);
        });
        $('[data-unit-helper-action]').on('click', function (e) {
            var element = $(e.target);
            var unitID = element.parents('[data-' + UnitElement.UnitHelperGroupAttr + ']').data(UnitElement.UnitHelperGroupAttr);
            var unit = _this.getUnitById(unitID);
            var helperAction = element.data('unit-helper-action');
            recalculateUnit.recalculateWithHelperAction(unit, helperAction);
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
            if (_this.lastRecalculatedUnit !== undefined) {
                text += location.origin + location.pathname + '?' + _this.lastRecalculatedUnit.value + _this.lastRecalculatedUnit.symbol;
            }
            _this.copyTextToClipboard(text);
            _this.lastRecalculatedUnit.element.focus();
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
            _this.lastRecalculatedUnit.element.focus();
        });
    };
    UnitCandyUI.prototype.initializeGotoUnitgroupButtons = function () {
        var _this = this;
        this.buttonGotoUnitGroup.on('click', function (e) {
            var unitgroupType = $(e.target).data('goto-unitgroup');
            _this.showUnitGroup(unitgroupType);
            _this.getBaseUnitOrDefault(unitgroupType).element.focus();
        });
    };
    UnitCandyUI.prototype.initializeAnyUnitTextBox = function () {
        this.anyUnitTextBox.on('keypress', function (e) {
            var key = e.keyCode || e.which;
            if (key === 13) {
                var findstr = $(e.target).val();
                recalculateUnit.findUnit(findstr);
            }
        });
    };
    /** shows only the unit group of the provided type and hides all others;
     * unitGroupType = "DigitalStorage"
     * Null hides all; "all" shows all */
    UnitCandyUI.prototype.showUnitGroup = function (unitGroupType) {
        var _a;
        var unitGroups = $('[data-' + UnitElement.UnitTypeAttr + ']');
        for (var i = 0; i < unitGroups.length; i++) {
            if (((_a = $(unitGroups[i]).data(UnitElement.UnitTypeAttr) === unitGroupType) !== null && _a !== void 0 ? _a : '') || unitGroupType === 'all') {
                $(unitGroups[i]).fadeIn(500);
            }
            else {
                $(unitGroups[i]).hide();
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
    UnitCandyUI.prototype.getBaseUnitOrDefault = function (unitGroupType) {
        var units = this.getUnitsOfSameType(unitGroupType);
        for (var i = 0; i < units.length; i++) {
            if (units[i].isBaseUnit === true) {
                return units[i];
            }
        }
        return units[0];
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
    // register popovers
    $('[data-toggle="popover"]').popover();
    recalculateUnit = new Recalculate();
    UI = new UnitCandyUI();
    // in case uri has params
    UI.setUnitFromUri(location.href);
});
//# sourceMappingURL=unitcandy.js.map