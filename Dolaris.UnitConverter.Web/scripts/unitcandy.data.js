/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>
/** The UnitCandyData class contains function
 *  the deal with calling web service functions. */
var UnitCandyData = /** @class */ (function () {
    /** creates a new instance of the UnitCandyData class */
    function UnitCandyData() {
    }
    /**
     * recalculates all units of the same type based on the provided unit
     *
     * @param {UnitElement} unit unit on which recalculation is based
     * @param {(unitName: string, unitValue: string) => void} reportUpdate callback function reporting updates to units
     */
    UnitCandyData.recalculateUnits = function (unit, reportUpdate) {
        $.ajax({
            url: 'UnitCandyService.svc/RecalculateString',
            async: true,
            method: 'GET',
            data: { unitName: unit.ID, unitValue: unit.value },
            dataType: 'json',
            beforeSend: function () { },
            success: function (data, status, xhr) {
                $.each(data.d, function (index, result) {
                    if (result.UnitName !== unit.name) {
                        reportUpdate(result.UnitName, result.UnitValue);
                    }
                });
                return null;
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                return null;
            }
        });
    };
    /**
     * recalculates all units of the same type based on the provided unit and helper
     *
     * @param {UnitElement} unit the unit recalculation is based on
     * @param {string} helper helper action to take (for example "+1" or "x10")
     * @param {(unitName: string, unitValue: string) => void} reportUpdate callback function reporting updates to units
     */
    UnitCandyData.recalculateUnitsWithHelper = function (unit, helper, reportUpdate) {
        $.ajax({
            url: 'UnitCandyService.svc/RecalculateWithHelperAction',
            async: true,
            method: 'GET',
            data: { unitName: unit.ID, unitValue: unit.value, action: helper },
            dataType: 'json',
            beforeSend: function () { },
            success: function (data, status, xhr) {
                $.each(data.d, function (index, result) {
                    reportUpdate(result.UnitName, result.UnitValue);
                });
                return null;
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                return null;
            }
        });
    };
    /**
     * finds a unit based on the provided search string;
     * for example "12m" -> 12 Meters
     *
     * @param {string} searchstr search string (for example "12m", "68 Fahr")
     * @param {(unitName: string, unitValue: string) => void} reportUnit callback function reporting which unit has been found
     */
    UnitCandyData.findUnit = function (searchstr, reportUnit) {
        $.ajax({
            url: 'UnitCandyService.svc/FindUnit',
            async: true,
            method: 'GET',
            data: { inputstring: searchstr, unitName: null },
            dataType: 'json',
            beforeSend: function () { },
            success: function (data, status, xhr) {
                var unitName = data.d !== null ? data.d.UnitName : null;
                var unitValue = data.d !== null ? data.d.UnitValue : null;
                reportUnit(unitName, unitValue);
                return null;
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error + ' ' + xhr.statusText + ' ' + xhr.responseText);
                return null;
            }
        });
    };
    return UnitCandyData;
}());
//# sourceMappingURL=unitcandy.data.js.map