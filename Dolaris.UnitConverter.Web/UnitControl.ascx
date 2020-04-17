﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitControl.ascx.cs" Inherits="Dolaris.UnitConverter.Web.UnitControl" %>

<div id="UnitHeadElement" runat="server" class="form-group">
    <div class="form-inline">
        <label id="UnitNameLabel" runat="server" for="UnitTextBox" class="control-label col-sm-4 col-md-3 textbox"></label>
        <div class="input-group">
            <input type="text" id="UnitTextBox" runat="server" class="form-control" onchange="unitChanged(event)" onkeypress="unitChangedKeyPressed(event)" data-unitname="" data-unitgroupname="" data-unitsymbol="" data-toggle="tooltip" data-placement="top" title="" />
            <div id="UnitTextBoxSymbol" runat="server" class="input-group-addon" style="width: 55px; border-width: 0px"></div>
        </div>
        <label class="control-label col-md-3"></label>
        <div class="input-group col-md-7">
            <div id="UnitHelper" runat="server" hidden="hidden" data-helper="">
                <ul class="list-inline">
                    <li><a href="javascript:void(0)" tabindex="-1" onclick="unitChangedWithHelperAction('+1',event)" class="help-block label label-info" title="add one">+1</a></li>
                    <li><a href="javascript:void(0)" tabindex="-1" onclick="javascript:unitChangedWithHelperAction('-1',event);" class="help-block label label-info" title="subtract one">-1</a></li>
                    <li><a href="javascript:void(0)" tabindex="-1" onclick="javascript:unitChangedWithHelperAction('x10',event);" class="help-block label label-info" title="multiply by ten">x10</a></li>
                    <li><a href="javascript:void(0)" tabindex="-1" onclick="javascript:unitChangedWithHelperAction('/10',event);" class="help-block label label-info" title="divide by ten">&divide;10</a></li>
                    <li><a id="UnitHelperActionClean" runat="server" tabindex="-1" href="javascript:void(0)" onclick="javascript:unitChangedWithHelperAction('Clear',event);" class="help-block label label-info" title="set to zero">Zero</a></li>
                    <li><a id="UnitHelperActionMin" runat="server" tabindex="-1" href="javascript:void(0)" onclick="javascript:unitChangedWithHelperAction('Min',event);" class="help-block label label-info" title="smallest value">Min</a></li>
                    <li><a id="UnitHelperActionMax" runat="server" tabindex="-1" href="javascript:void(0)" onclick="javascript:unitChangedWithHelperAction('Max',event);" class="help-block label label-info" title="greatest value">Max</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
