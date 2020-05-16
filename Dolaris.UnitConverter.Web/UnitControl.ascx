<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitControl.ascx.cs" Inherits="Dolaris.UnitConverter.Web.UnitControl" %>

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
<tr class="collapse" data-unit-helper-group="<% = UnitID.ToString() %>">
    <td class="g-hidden-lg-down"></td>
    <td colspan="2">
        <div class="ml-2">
            <a href="javascript:void(0)"><span class="small g-color-black align-top mr-2" data-toggle="tooltip" title="Add One" data-placement="bottom" data-unit-helper-action="+1">+1</span></a>
            <a href="javascript:void(0)"><span class="small g-color-black align-top mr-2" data-toggle="tooltip" title="Subtract One" data-placement="bottom" data-unit-helper-action="-1">-1</span></a>
            <a href="javascript:void(0)"><span class="small g-color-black align-top mr-2" data-toggle="tooltip" title="Multiply by Ten" data-placement="bottom" data-unit-helper-action="x10">x10</span></a>
            <a href="javascript:void(0)"><span class="small g-color-black align-top mr-2" data-toggle="tooltip" title="Divide by Ten" data-placement="bottom" data-unit-helper-action="/10">&divide;10</span></a>
            <a href="javascript:void(0)"><span class="small g-color-black align-top mr-2" data-toggle="tooltip" title="Double Value" data-placement="bottom" data-unit-helper-action="x2">x2</span></a>
            <a href="javascript:void(0)"><span class="small g-color-black align-top mr-2" data-toggle="tooltip" title="Half Value" data-placement="bottom" data-unit-helper-action="/2">&divide;2</span></a>
        </div>
    </td>
    <td class="g-hidden-lg-down"></td>
</tr>
