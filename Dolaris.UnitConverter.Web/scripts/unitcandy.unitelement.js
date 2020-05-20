/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>
var UnitElement = /** @class */ (function () {
    function UnitElement(element) {
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
//# sourceMappingURL=unitcandy.unitelement.js.map