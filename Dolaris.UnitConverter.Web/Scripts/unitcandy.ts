/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>



let recalculateUnit: Recalculate;
let UI: UnitCandyUI;



class Recalculate {

    public constructor() { }

    public recalculate(unitElement: UnitElement): void {

        $.ajax({
            url: 'UnitCandyService.svc/Recalculate',
            async: true,
            method: 'GET',
            data: { unitName: unitElement.ID, unitValue: unitElement.value },
            dataType: 'json',
            beforeSend: function () { UI.showWaitCursor(); setTimeout(() => UI.showAutoCursor(), 500); },

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

    public recalculateWithHelperAction(unitElement: UnitElement, helperAction: string): void {

        $.ajax({
            url: 'UnitCandyService.svc/RecalculateWithHelperAction',
            async: true,
            method: 'GET',
            data: { unitName: unitElement.ID, unitValue: unitElement.value, action: helperAction },
            dataType: 'json',
            beforeSend: () => { UI.showWaitCursor(); setTimeout(() => UI.showAutoCursor(), 500); },

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

    public findUnit(findstr: string): void {

        $.ajax({
            url: 'UnitCandyService.svc/FindUnit',
            async: true,
            method: 'GET',
            data: { inputstring: findstr, unitName: null },
            dataType: 'json',
            beforeSend: () => { UI.showWaitCursor(); setTimeout(() => UI.showAutoCursor(), 1000); },

            success: (data, status, xhr) => {

                const unitName = data.d !== null ? data.d.UnitName : null;
                const unitValue = data.d !== null ? data.d.UnitValue : null;

                UI.setUnitFromFindstr(unitName, unitValue);

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

        if (this.element !== null) {
            this.element.val(s);
        }
    }

    public previousValue: string;
}



class UnitCandyUI {

    public constructor() {

        this.initializeUnitElements();
        this.initializeCopyButtons();
        this.initializeClearButtons();
        this.initializeGotoUnitgroupButtons();
        this.initializeAnyUnitTextBox();

        $('#unitcandy-icon').on('click', () => { setTimeout(() => { window.location.href = location.origin + location.pathname }, 1000); });
    }


    public units: Array<UnitElement> = new Array<UnitElement>();

    /** sets the unit identified by unitID to the value 
        example: unitID = "NauticalMiles", value = "305.72" */
    public setUnitToValue(unitID: string, unitValue: string): void {

        const unit = this.getUnitById(unitID);

        if (this.lastRecalculatedUnit === undefined || unit.ID !== this.lastRecalculatedUnit.ID) {

            unit.value = '';
            setTimeout(() => unit.value = unitValue, Math.random() * 500);

        } else {
            unit.value = unitValue
        }
    }

    public setUnitFromFindstr(unitID: string, unitValue: string): void {

        if (unitID !== null) {

            const unit = this.getUnitById(unitID);

            unit.value = unitValue;
            //this.showUnitGroup(unit.type);
            this.scrollToUnitGroup(unit.type);
            this.recalculateUnit(unit, true);

            this.anyUnitTextBox.val(unit.value + ' ' + unit.symbol);

        } else {

            this.anyUnitTextBox.val('');
            this.anyUnitTextBox.popover('show');
            setTimeout(() => this.anyUnitTextBox.popover('hide'), 3000);
        }
    }

    public setUnitFromUri(uri: string): void {

        // handle uri with parameters
        // for example: https://www.unitcandy.com?68Fahrenheit

        const indexOfQuestionMark = uri.indexOf('?');

        if (indexOfQuestionMark < 0) {
            return;
        }

        const param = location.href.substring(indexOfQuestionMark + 1);

        for (let i = 0; i < this.units.length; i++) {

            const unitGroupType = this.units[i].type;

            if (unitGroupType.toLowerCase() === param.toLowerCase()) {

                //this.showUnitGroup(unitGroupType);
                this.scrollToUnitGroup(unitGroupType);
                this.getBaseUnitOrDefault(unitGroupType).element.focus();
                return;
            }
        }

        recalculateUnit.findUnit(param);
    }

    public recalculateUnit(unit: UnitElement, forceRecalc = false): void {

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
    }

    /** sets the cursor to Wait */
    public showWaitCursor(): void {
        document.body.style.cursor = "wait";
    }

    /** sets the cursor to Auto */
    public showAutoCursor(): void {
        document.body.style.cursor = "auto";
    }

    /** copy provided text to clipboard */
    public copyTextToClipboard(text: string): void {

        const textarea = document.createElement("textarea");

        try {
            document.activeElement.appendChild(textarea);

            textarea.value = text;
            textarea.textContent = text;
            textarea.select();

            document.execCommand("copy");

            textarea.parentElement.removeChild(textarea);

        } catch (e) {
            textarea.parentElement.removeChild(textarea);
        }
    }


    protected buttonGotoUnitGroup = $('[data-goto-unitgroup]');
    protected copyButtons = $('[data-button-copy]');
    protected embedButtons = $('[data-button-embed]');
    protected clearButtons = $('[data-button-clear]');
    protected unitHelperGroup = $('[data-' + UnitElement.UnitHelperGroupAttr + ']');
    protected anyUnitTextBox = $('#any-unit');

    public lastRecalculatedUnit: UnitElement;
    public lastUnitHelperGroup: JQuery;

    protected initializeUnitElements(): void {

        $('[data-' + UnitElement.UnitTextboxAttr + ']').each((index, item) => {

            const unit = new UnitElement($(item));

            unit.element.on('keypress',
                (e) => {

                    const key = e.keyCode || e.which;

                    if (key === 13) {

                        const element = $(e.target);
                        const unitToRecalculate = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));

                        this.recalculateUnit(unitToRecalculate);
                    }
                });

            unit.element.on('focusin',
                (e) => {

                    const element = $(e.target);
                    const unit = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));

                    if (this.lastUnitHelperGroup !== undefined) {
                        this.lastUnitHelperGroup.hide();
                    }

                    unit.elementHelperGroup.show();
                    this.lastUnitHelperGroup = unit.elementHelperGroup;
                });

            this.units.push(unit);
        });

        $('[data-unit-helper-action]').on('click', (e) => {

            const element = $(e.target);

            const unitID = element.parents('[data-' + UnitElement.UnitHelperGroupAttr + ']').data(UnitElement.UnitHelperGroupAttr);
            const unit = this.getUnitById(unitID);

            const helperAction = element.data('unit-helper-action');

            recalculateUnit.recalculateWithHelperAction(unit, helperAction);
        });
    }

    protected initializeCopyButtons(): void {

        this.copyButtons.click((e) => {

            const element = $(e.target);
            const type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            const unitsOfSameType = this.getUnitsOfSameType(type);

            let text: string = type + '\n';

            for (let i = 0; i < unitsOfSameType.length; i++) {

                text += unitsOfSameType[i].plural + ': ' + unitsOfSameType[i].value + ' ' + unitsOfSameType[i].symbol + '\n';
            }

            if (this.lastRecalculatedUnit !== undefined) {
                text += location.origin + location.pathname + '?' + this.lastRecalculatedUnit.value + this.lastRecalculatedUnit.symbol;
            }

            this.copyTextToClipboard(text);

            this.lastRecalculatedUnit.element.focus();
        });
    }

    protected initializeClearButtons(): void {

        this.clearButtons.click((e) => {

            const element = $(e.target);
            const type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
            const unitsOfSameType = this.getUnitsOfSameType(type);

            for (let i = 0; i < unitsOfSameType.length; i++) {

                setTimeout(() => unitsOfSameType[i].value = '', Math.random() * 500);
            }

            this.lastRecalculatedUnit.element.focus();
        });
    }

    protected initializeGotoUnitgroupButtons(): void {

        this.buttonGotoUnitGroup.on('click',
            (e) => {

                const unitgroupType = $(e.target).data('goto-unitgroup');
                this.scrollToUnitGroup(unitgroupType);
            });
    }

    protected initializeAnyUnitTextBox(): void {

        this.anyUnitTextBox.on('keypress',
            (e) => {

                const key = e.keyCode || e.which;

                if (key === 13) {

                    const findstr = $(e.target).val();
                    recalculateUnit.findUnit(findstr);
                }
            });
    }

    /** shows only the unit group of the provided type and hides all others;
     * unitGroupType = "DigitalStorage"
     * Null hides all; "all" shows all */
    protected showUnitGroup(unitGroupType: string): void {

        const unitGroups = $('[data-' + UnitElement.UnitTypeAttr + ']');

        for (let i = 0; i < unitGroups.length; i++) {

            if (($(unitGroups[i]).data(UnitElement.UnitTypeAttr) === unitGroupType ?? '') || unitGroupType === 'all') {
                $(unitGroups[i]).fadeIn(500);

            } else {
                $(unitGroups[i]).hide();
            }
        }
    }

    protected scrollToUnitGroup(unitGroupType: string): void {

        $('html, body').animate({
            scrollTop: $('#' + unitGroupType).offset().top - 75
        }, 'slow');
    }

    protected getUnitById(id: string): UnitElement {

        for (let i = 0; i < this.units.length; i++) {

            if (this.units[i].ID === id) {
                return this.units[i];
            }
        }

        return null;
    }

    protected getBaseUnitOrDefault(unitGroupType: string): UnitElement {

        const units = this.getUnitsOfSameType(unitGroupType);

        for (let i = 0; i < units.length; i++) {

            if (units[i].isBaseUnit === true) {
                return units[i];
            }
        }

        return units[0];
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

    // register popovers
    $('[data-toggle="popover"]').popover();

    recalculateUnit = new Recalculate();
    UI = new UnitCandyUI();

    // in case uri has params
    UI.setUnitFromUri(location.href);
});
