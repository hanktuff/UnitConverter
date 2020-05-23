﻿/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>



class UnitCandyUI {

    public constructor() {

        this.initializeUnitElements();
        this.initializeUnitHelpers();
        this.initializeAnyUnitTextBox();

        this.initializeCopyButtons();
        this.initializeClearButtons();

        this.initializeGotoUnitgroupButtons();
    }


    public units: Array<UnitElement> = new Array<UnitElement>();

    /** sets the unit identified by unitID to the value 
        example: unitID = "NauticalMiles", value = "305.72" */
    public setUnitToValue(unitID: string, unitValue: string): void {

        const unit = this.getUnitById(unitID);

        if (this.lastEditedUnit === undefined || unit.ID !== this.lastEditedUnit.ID) {

            unit.value = '';
            setTimeout(() => unit.value = unitValue, Math.random() * 500);

        } else {
            //unit.value = unitValue;
        }
    }

    public setUnitFromSearchstr(unitID: string, unitValue: string): void {

        if (unitID !== null) {

            const unit = this.getUnitById(unitID);

            unit.value = unitValue;
            //this.showUnitGroup(unit.type);
            this.scrollToUnitGroup(unit.type);
            this.recalculateUnit(unit);

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

        this.appearBusy();

        UnitCandyData.findUnit(param, (id, v) => this.setUnitFromSearchstr(id, v));
    }

    protected recalculateUnit(unit: UnitElement, helper: string = null): void {

        this.appearBusy();

        if (helper === null) {
            UnitCandyData.recalculateUnits(unit, (n, v) => this.setUnitToValue(n, v));

        } else {
            UnitCandyData.recalculateUnitsWithHelper(unit, helper, (n, v) => this.setUnitToValue(n, v));
        }
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

    protected lastEditedUnit: UnitElement = new UnitElement($('[data-' + UnitElement.UnitTextboxAttr + ']').first());
    public lastUnitHelperGroup: JQuery;
    public lastValueOfUnit: string;

    protected initializeUnitElements(): void {

        $('[data-' + UnitElement.UnitTextboxAttr + ']').each((index, item) => {

            const unit = new UnitElement($(item));

            // recalculate unit when Enter key pressed

            unit.element.on('keyup',
                (e) => {

                    try {

                        const element = $(e.target);
                        const unit = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));

                        unit.value = element.val();

                        if (isNaN(Number(unit.value))) { return; }

                        //this.lastEditedUnit = unit;

                        this.recalculateUnit(unit);


                    } catch (e) {
                        const x = 1;
                    }
                });

            // show unit helpers when focus received

            unit.element.on('focusin',
                (e) => {

                    const element = $(e.target);
                    const unit = this.getUnitById(element.data(UnitElement.UnitTextboxAttr));

                    if (this.lastUnitHelperGroup !== undefined) {
                        this.lastUnitHelperGroup.hide();
                    }

                    unit.elementHelperGroup.show();
                    this.lastUnitHelperGroup = unit.elementHelperGroup;

                    this.lastValueOfUnit = unit.value;
                });

            this.units.push(unit);
        });
    }

    protected initializeUnitHelpers(): void {

        $('[data-unit-helper-action]').on('click',
            (e) => {

                this.appearBusy();

                const element = $(e.target);
                const unitID = element.parents('[data-' + UnitElement.UnitHelperGroupAttr + ']').data(UnitElement.UnitHelperGroupAttr);
                const unit = this.getUnitById(unitID);

                const helper = element.data('unit-helper-action');

                this.recalculateUnit(unit, helper);
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

            if (this.lastEditedUnit !== undefined) {
                text += location.origin + location.pathname + '?' + this.lastEditedUnit.value + this.lastEditedUnit.symbol;
            }

            this.copyTextToClipboard(text);

            this.lastEditedUnit.element.focus();
        });
    }

    protected initializeClearButtons(): void {

        this.clearButtons.on('click',
            (e) => {

                const element = $(e.target);
                const type = element.parents('[data-' + UnitElement.UnitTypeAttr + ']').data(UnitElement.UnitTypeAttr);
                const unitsOfSameType = this.getUnitsOfSameType(type);

                for (let i = 0; i < unitsOfSameType.length; i++) {

                    setTimeout(() => unitsOfSameType[i].element.attr("disabled", "disabled"), Math.random() * 300);
                    setTimeout(() => unitsOfSameType[i].value = '', Math.random() * 300 + 300);
                    setTimeout(() => unitsOfSameType[i].element.removeAttr('disabled'), Math.random() * 300 + 600);
                }

                this.getLastEditedUnit()?.element.focus();
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

                    this.appearBusy();

                    const searchstr = $(e.target).val();
                    UnitCandyData.findUnit(searchstr, (id, v) => this.setUnitFromSearchstr(id, v));
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

    /** scrolls to top of unit group */
    protected scrollToUnitGroup(unitGroupType: string): void {

        $('html, body').animate({
            scrollTop: $('#' + unitGroupType).offset().top - 75
        }, 'slow');
    }

    /** returns the unit object matching the id; returns null if not found */
    protected getUnitById(id: string): UnitElement {

        for (let i = 0; i < this.units.length; i++) {

            if (this.units[i].ID === id) {
                return this.units[i];
            }
        }

        return null;
    }

    /** returns unit object that is the base unit (for length this is Meter); 
     * if there is no base unit returns the first unit */
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

    protected appearBusy(duration = 500): void {

        document.body.style.cursor = "wait";
        setTimeout(() => document.body.style.cursor = "auto", duration);
    }



    protected setLastEditedUnit(unit: UnitElement): void {
        this.lastEditedUnit = unit;
    }

    protected getLastEditedUnit(): UnitElement {
        return this.lastEditedUnit;
    }
}

