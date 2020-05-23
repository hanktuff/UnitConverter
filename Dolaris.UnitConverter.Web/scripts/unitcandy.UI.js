/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>
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
        this.initializeUnitHelpers();
        this.initializeAnyUnitTextBox();
        this.initializeCopyButtons();
        this.initializeClearButtons();
        this.initializeGotoUnitgroupButtons();
    }
    /** sets the unit identified by unitID to the value
        example: unitID = "NauticalMiles", value = "305.72" */
    UnitCandyUI.prototype.setUnitToValue = function (unitID, unitValue) {
        var unit = this.getUnitById(unitID);
        if (isNaN(Number(unit.value)) || isNaN(Number(unitValue))) {
            unit.value = unitValue;
        }
        else if (unit.value !== unitValue) {
            unit.value = '';
            setTimeout(function () { return unit.value = unitValue; }, Math.random() * 500);
        }
    };
    UnitCandyUI.prototype.setUnitFromSearchstr = function (unitID, unitValue) {
        var _this = this;
        if (unitID !== null) {
            var unit = this.getUnitById(unitID);
            unit.value = unitValue;
            //this.showUnitGroup(unit.type);
            this.scrollToUnitGroup(unit.type);
            this.recalculateUnits(unit);
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
        var _this = this;
        var indexOfQuestionMark = uri.indexOf('?');
        if (indexOfQuestionMark < 0) {
            return;
        }
        var param = location.href.substring(indexOfQuestionMark + 1);
        for (var i = 0; i < this.units.length; i++) {
            var unitGroupType = this.units[i].type;
            if (unitGroupType.toLowerCase() === param.toLowerCase()) {
                //this.showUnitGroup(unitGroupType);
                this.scrollToUnitGroup(unitGroupType);
                this.getBaseUnitOrDefault(unitGroupType).element.focus();
                return;
            }
        }
        this.appearBusy();
        UnitCandyData.findUnit(param, function (id, v) { return _this.setUnitFromSearchstr(id, v); });
    };
    UnitCandyUI.prototype.recalculateUnits = function (unit, helper) {
        var _this = this;
        if (helper === void 0) { helper = null; }
        this.appearBusy();
        if (helper === null) {
            UnitCandyData.recalculateUnits(unit, function (n, v) { return _this.setUnitToValue(n, v); });
        }
        else {
            UnitCandyData.recalculateUnitsWithHelper(unit, helper, function (n, v) { return _this.setUnitToValue(n, v); });
        }
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
            unit.onValueChanged(function (unit) {
                if (isNaN(Number(unit.value))) {
                    return;
                }
                _this.lastEditedUnit = unit;
                _this.recalculateUnits(unit);
            });
            unit.onGetFocus(function (unit) {
                _this.units.forEach(function (item) { return item.elementHelperGroup.hide(); });
                unit.elementHelperGroup.show();
            });
            _this.units.push(unit);
        });
    };
    UnitCandyUI.prototype.initializeUnitHelpers = function () {
        var _this = this;
        $('[data-unit-helper-action]').on('click', function (e) {
            _this.appearBusy();
            var element = $(e.target);
            var unitID = element.parents('[data-' + UnitElement.UnitHelperGroupAttr + ']').data(UnitElement.UnitHelperGroupAttr);
            var unit = _this.getUnitById(unitID);
            var helper = element.data('unit-helper-action');
            _this.recalculateUnits(unit, helper);
        });
    };
    UnitCandyUI.prototype.initializeCopyButtons = function () {
        var _this = this;
        this.copyButtons.click(function (e) {
            var element = $(e.target);
            var type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            var unitsOfSameType = _this.getUnitsByType(type);
            var text = type + '\n';
            for (var i = 0; i < unitsOfSameType.length; i++) {
                text += unitsOfSameType[i].plural + ': ' + unitsOfSameType[i].value + ' ' + unitsOfSameType[i].symbol + '\n';
            }
            if (_this.lastEditedUnit !== undefined) {
                text += location.origin + location.pathname + '?' + _this.lastEditedUnit.value + _this.lastEditedUnit.symbol;
            }
            _this.copyTextToClipboard(text);
            _this.lastEditedUnit.element.focus();
        });
    };
    UnitCandyUI.prototype.initializeClearButtons = function () {
        var _this = this;
        this.clearButtons.on('click', function (e) {
            var _a;
            var element = $(e.target);
            var type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            var unitsOfSameType = _this.getUnitsByType(type);
            var _loop_1 = function (i) {
                setTimeout(function () { return unitsOfSameType[i].element.attr("disabled", "disabled"); }, Math.random() * 300);
                setTimeout(function () { return unitsOfSameType[i].value = ''; }, Math.random() * 300 + 300);
                setTimeout(function () { return unitsOfSameType[i].element.removeAttr('disabled'); }, Math.random() * 300 + 600);
            };
            for (var i = 0; i < unitsOfSameType.length; i++) {
                _loop_1(i);
            }
            if (((_a = _this.lastEditedUnit) === null || _a === void 0 ? void 0 : _a.type) === type) {
                setTimeout(function () { var _a, _b; return (_b = (_a = _this.lastEditedUnit) === null || _a === void 0 ? void 0 : _a.element) === null || _b === void 0 ? void 0 : _b.focus(); }, 1000);
            }
        });
    };
    UnitCandyUI.prototype.initializeGotoUnitgroupButtons = function () {
        var _this = this;
        this.buttonGotoUnitGroup.on('click', function (e) {
            var unitgroupType = $(e.target).data('goto-unitgroup');
            _this.scrollToUnitGroup(unitgroupType);
        });
    };
    UnitCandyUI.prototype.initializeAnyUnitTextBox = function () {
        var _this = this;
        this.anyUnitTextBox.on('keypress', function (e) {
            var key = e.keyCode || e.which;
            if (key === 13) {
                _this.appearBusy();
                var searchstr = $(e.target).val();
                UnitCandyData.findUnit(searchstr, function (id, v) { return _this.setUnitFromSearchstr(id, v); });
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
    /** scrolls to top of unit group */
    UnitCandyUI.prototype.scrollToUnitGroup = function (unitGroupType) {
        $('html, body').animate({
            scrollTop: $('#' + unitGroupType).offset().top - 75
        }, 'slow');
    };
    /** returns the unit object matching the id; returns null if not found */
    UnitCandyUI.prototype.getUnitById = function (id) {
        for (var i = 0; i < this.units.length; i++) {
            if (this.units[i].ID === id) {
                return this.units[i];
            }
        }
        return null;
    };
    /** returns unit object that is the base unit (for length this is Meter);
     * if there is no base unit returns the first unit */
    UnitCandyUI.prototype.getBaseUnitOrDefault = function (type) {
        var units = this.getUnitsByType(type);
        for (var i = 0; i < units.length; i++) {
            if (units[i].isBaseUnit === true) {
                return units[i];
            }
        }
        return units[0];
    };
    /** returns all units that are of the same type as the provided unit
        for example: "Fahrenheit" is a Temperature. The function returns "Fahrenheit", "Celsius", and "Kelvin". */
    UnitCandyUI.prototype.getUnitsByType = function (type) {
        var unitsOfSameType = new Array();
        for (var i = 0; i < this.units.length; i++) {
            if (this.units[i].type === type) {
                unitsOfSameType.push(this.units[i]);
            }
        }
        return unitsOfSameType;
    };
    UnitCandyUI.prototype.appearBusy = function (duration) {
        if (duration === void 0) { duration = 500; }
        document.body.style.cursor = "wait";
        setTimeout(function () { return document.body.style.cursor = "auto"; }, duration);
    };
    return UnitCandyUI;
}());
//# sourceMappingURL=unitcandy.ui.js.map