<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitGroupControl.ascx.cs" Inherits="Dolaris.UnitConverter.Web.UnitGroupControl" ClientIDMode="Static" %>


<div id="<% = UnitType.ToString() %>" data-unit-type="<% = UnitType.ToString() %>" class="row mb-5">

    <div class="col-lg-2 offset-lg-2">
        <h2 class="mb-1"><strong>
            <asp:PlaceHolder ID="TitlePlaceHolder" runat="server" />
        </strong></h2>
        <h6 class="mb-3">
            <asp:PlaceHolder ID="DescriptionPlaceHolder" runat="server" />
        </h6>
        <div class="d-flex flex-row">
            <button class="btn btn-xs u-btn-outline-blue text-uppercase mr-2" data-toggle="tooltip" title="Copy to Clipboard" data-button-copy="">Copy</button>
            <button class="btn btn-xs u-btn-outline-yellow text-uppercase mr-2" data-button-embed="" hidden="hidden">Embed</button>
            <button class="btn btn-xs u-btn-outline-red text-uppercase mr-2" data-button-clear="">Clear</button>
        </div>
    </div>

    <div class="col-lg-3">
        <div class="table-responsive table-borderless">
            <table class="table">
                <tbody>
                    <asp:PlaceHolder ID="UnitControlPlaceHolder" runat="server" />
                </tbody>
            </table>
        </div>
    </div>


    <%--<ul class="dropdown-menu">
        <li><a id="CopyUnitsToClipboard" runat="server" href="javascript:void(0)" onclick="copyUnitsToClipboard()">Copy</a></li>
        <li><a id="CopyLinkToClipboard" runat="server" href="javascript:void(0)" onclick="copyLinkToClipboard('')">Copy Link</a></li>
        <li><a id="CopyAnchorToClipboard" runat="server" href="javascript:void(0)" onclick="copyAnchorToClipboard('')">Copy Anchor</a></li>
    </ul>
        --%>
</div>

