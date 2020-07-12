/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>



/** The UnitCandyData class contains function
 *  the deal with calling web service functions. */
class UnitCandyData {

    /** creates a new instance of the UnitCandyData class */
    public constructor() { }

    /**
     * recalculates all units of the same type based on the provided unit
     * 
     * @param {UnitElement} unit unit on which recalculation is based
     * @param {(unitName: string, unitValue: string) => void} reportUpdate callback function reporting updates to units
     */
    public static recalculateUnits(unit: UnitElement, reportUpdate: (unitName: string, unitValue: string) => void): void {

        $.ajax({
            url: 'UnitCandyService.svc/Recalculate',
            async: true,
            method: 'GET',
            data: { unitName: unit.ID, unitValue: unit.value },
            dataType: 'json',
            beforeSend: () => { },

            success: (data, status, xhr) => {

                $.each(data.d,
                    (index, result) => {
                        if (result.UnitName !== unit.name) {
                            reportUpdate(result.UnitName, result.UnitValue);
                        }
                    });

                return null;
            },

            error: (xhr, status, error) => {

                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                return null;
            }
        });
    }

    /**
     * recalculates all units of the same type based on the provided unit and helper
     * 
     * @param {UnitElement} unit the unit recalculation is based on
     * @param {string} helper helper action to take (for example "+1" or "x10")
     * @param {(unitName: string, unitValue: string) => void} reportUpdate callback function reporting updates to units
     */
    public static recalculateUnitsWithHelper(unit: UnitElement, helper: string, reportUpdate: (unitName: string, unitValue: string) => void): void {

        $.ajax({
            url: 'UnitCandyService.svc/RecalculateWithHelperAction',
            async: true,
            method: 'GET',
            data: { unitName: unit.ID, unitValue: unit.value, action: helper },
            dataType: 'json',
            beforeSend: () => { },

            success: (data, status, xhr) => {

                $.each(data.d,
                    (index, result) => {
                        reportUpdate(result.UnitName, result.UnitValue)
                    });

                return null;
            },

            error: (xhr, status, error) => {

                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                return null;
            }
        });
    }

    /**
     * finds a unit based on the provided search string;
     * for example "12m" -> 12 Meters
     * 
     * @param {string} searchstr search string (for example "12m", "68 Fahr")
     * @param {(unitName: string, unitValue: string) => void} reportUnit callback function reporting which unit has been found
     */
    public static findUnit(searchstr: string, reportUnit: (unitName: string, unitValue: string) => void): void {

        $.ajax({
            url: 'UnitCandyService.svc/FindUnit',
            async: true,
            method: 'GET',
            data: { inputstring: searchstr, unitName: null },
            dataType: 'json',
            beforeSend: () => { },

            success: (data, status, xhr) => {

                const unitName = data.d !== null ? data.d.UnitName : null;
                const unitValue = data.d !== null ? data.d.UnitValue : null;

                reportUnit(unitName, unitValue);

                return null;
            },

            error: (xhr, status, error) => {

                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                return null;
            }
        });
    }
}
