﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitControl.ascx.cs" Inherits="Dolaris.UnitConverter.Web.UnitControl" %>

<tr>
    <td class="align-middle g-hidden-lg-down">
        <% = Name %>:
    </td>
    <td>
        <div class="g-hidden-xl-up"><% = Name %> (<% = Symbol %>):</div>
        <input class="form-control text-dark" type="text" value="<% = Value %>"
            data-unit-textbox="<% = UnitID.ToString() %>"
            data-unit-name="<% = Name %>"
            data-unit-plural="<% = Plural %>"
            data-unit-symbol="<% = Symbol %>"
            data-unit-baseunit="<% = IsBaseUnit %>" />
    </td>
    <td class="align-middle g-hidden-lg-down">
        <% = Symbol %>
    </td>
</tr>

<%--
<li><a href="javascript:void(0)" tabindex="-1" onclick="unitChangedWithHelperAction('+1',event)" class="help-block label label-info" title="add one">+1</a></li>
<li><a href="javascript:void(0)" tabindex="-1" onclick="javascript:unitChangedWithHelperAction('-1',event);" class="help-block label label-info" title="subtract one">-1</a></li>
<li><a href="javascript:void(0)" tabindex="-1" onclick="javascript:unitChangedWithHelperAction('x10',event);" class="help-block label label-info" title="multiply by ten">x10</a></li>
<li><a href="javascript:void(0)" tabindex="-1" onclick="javascript:unitChangedWithHelperAction('/10',event);" class="help-block label label-info" title="divide by ten">&divide;10</a></li>
<li><a id="UnitHelperActionClean" runat="server" tabindex="-1" href="javascript:void(0)" onclick="javascript:unitChangedWithHelperAction('Clear',event);" class="help-block label label-info" title="set to zero">Zero</a></li>
<li><a id="UnitHelperActionMin" runat="server" tabindex="-1" href="javascript:void(0)" onclick="javascript:unitChangedWithHelperAction('Min',event);" class="help-block label label-info" title="smallest value">Min</a></li>
<li><a id="UnitHelperActionMax" runat="server" tabindex="-1" href="javascript:void(0)" onclick="javascript:unitChangedWithHelperAction('Max',event);" class="help-block label label-info" title="greatest value">Max</a></li>
--%>
