/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>
var UnitElement = /** @class */ (function () {
    function UnitElement(element) {
        var _this = this;
        this.valueChangedHandler = null;
        this.getFocusHandler = null;
        this.ID = '';
        this.type = '';
        if (element !== null) {
            this.element = element;
            this.ID = element.data(UnitElement.UnitTextboxAttr);
            this.type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            this.name = element.data('unit-name');
            this.plural = element.data('unit-plural');
            this.symbol = element.data('unit-symbol');
            this.isBaseUnit = element.data('unit-baseunit') === 'True' ? true : false;
            this.elementHelperGroup = $('[data-' + UnitElement.UnitHelperGroupAttr + '="' + this.ID + '"]');
            this.element.on('keyup', function () {
                if (_this.valueChangedHandler !== null) {
                    _this.valueChangedHandler(_this);
                }
            });
            this.element.on('focus', function () {
                if (_this.getFocusHandler !== null) {
                    _this.getFocusHandler(_this);
                }
            });
        }
    }
    UnitElement.prototype.onValueChanged = function (handler) {
        this.valueChangedHandler = handler;
    };
    UnitElement.prototype.onGetFocus = function (handler) {
        this.getFocusHandler = handler;
    };
    Object.defineProperty(UnitElement.prototype, "value", {
        get: function () {
            return this.element !== null ? this.element.val() : '';
        },
        set: function (s) {
            this.previousValue = this.value;
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
//# sourceMappingURL=unitcandy.unitelement.js.map