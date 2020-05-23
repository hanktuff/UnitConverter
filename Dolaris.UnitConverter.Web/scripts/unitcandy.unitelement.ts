/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>



class UnitElement {

    public constructor(element: JQuery) {

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

            this.element.on('keyup', () => {

                if (this.valueChangedHandler !== null) {
                    this.valueChangedHandler(this);
                }
            });

            this.element.on('focus', () => {

                if (this.getFocusHandler !== null) {
                    this.getFocusHandler(this);
                }
            });
        }
    }


    private valueChangedHandler: (unit: UnitElement) => void = null;
    public onValueChanged(handler: (unit: UnitElement) => void): void {
        this.valueChangedHandler = handler;
    }

    private getFocusHandler: (unit: UnitElement) => void = null;
    public onGetFocus(handler: (unit: UnitElement) => void): void {
        this.getFocusHandler = handler;
    }


    public static UnitTextboxAttr = 'unit-textbox';
    public static UnitTypeAttr = 'unit-type';
    public static UnitHelperGroupAttr = 'unit-helper-group';


    public ID: string;

    public type: string;

    public name: string;

    public plural: string;

    public symbol: string;

    public isBaseUnit: boolean;

    public element: JQuery;

    public elementHelperGroup: JQuery;

    public get value(): string {
        return this.element !== null ? this.element.val() : '';
    }

    public set value(s: string) {

        this.previousValue = this.value;

        if (this.element !== null) {
            this.element.val(s);
        }
    }

    public previousValue: string;
}
