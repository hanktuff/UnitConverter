<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitGroupControl.ascx.cs" Inherits="Dolaris.UnitConverter.Web.UnitGroupControl" ClientIDMode="Static" %>

<%--<div id="MainDiv" runat="server">
    <div class="wrap-container60">
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">
                        <div class="header-text">
                            <div class="header-heading">
                                <h1 id="OldUnitGroupTitle" runat="server" data-toggle="collapse" data-target=".unitscollapse" class="animation" data-animation="animation-fade-in-left"></h1>
                                <h5 id="OldUnitGroupDescription" runat="server" class="unitscollapse animation" data-animation="animation-fade-in-left" data-delay="3000"></h5>
                                <style>
                                    a, a:visited, a:hover, a:active {
                                        color: inherit;
                                    }
                                </style>
                                <div class="dropdown animation" data-animation="animation-fade-in-up">
                                    <a href="#" class="dropdown-toggle h3" data-toggle="dropdown">
                                        <i class="fa fa-edit"></i>
                                    </a>
                                    <ul class="dropdown-menu">
                                        <li><a id="CopyUnitsToClipboard" runat="server" href="javascript:void(0)" onclick="copyUnitsToClipboard()">Copy</a></li>
                                        <li><a id="CopyLinkToClipboard" runat="server" href="javascript:void(0)" onclick="copyLinkToClipboard('')">Copy Link</a></li>
                                        <li><a id="CopyAnchorToClipboard" runat="server" href="javascript:void(0)" onclick="copyAnchorToClipboard('')">Copy Anchor</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li class="disabled"><a id="SetEmbedText" runat="server" href="javascript:void(0)" onclick="">Embed</a></li>
                                    </ul>
                                    <a id="ClearAllFields" runat="server" class="h3" href="javascript:void(0);" onclick="clearLastUnitGroup()" style="">
                                        <i class="fa fa-trash" data-toggle="tooltip" title="Clear"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 animation" data-animation="animation-fade-in-right">
                        <asp:PlaceHolder ID="UnitControlPlaceHolder" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>--%>


<div class="row">
    <div class="col-lg-2 offset-lg-2">
        <h2 class="mb-1"><strong>
            <asp:PlaceHolder ID="UnitGroupTitle" runat="server" />
        </strong></h2>
        <h6 class="mb-3">
            <asp:PlaceHolder ID="UnitGroupDescription" runat="server" />
        </h6>
        <div class="d-flex flex-row">
            <button class="btn btn-xs btn-primary text-uppercase mr-2" data-toggle="tooltip" title="Copy to Clipboard">Copy</button>
            <button class="btn btn-xs u-btn-yellow text-uppercase mr-2">Embed</button>
            <button class="btn btn-xs btn-secondary text-uppercase mr-2">Clear</button>
        </div>
    </div>

    <div class="col-lg-3">
        <div class="table-responsive table-borderless">
            <table class="table">
                <tbody>
                    <asp:PlaceHolder ID="UnitControlPlaceHolder" runat="server" />
                    <%--<tr>
                        <td class="align-middle">Meter:</td>
                        <td>
                            <input class="form-control text-danger" type="text" /></td>
                        <td class="align-middle">m</td>
                    </tr>--%>
                    <%--<tr>
                        <td class="align-middle">Yard:</td>
                        <td>
                            <input class="form-control text-danger" type="text" /></td>
                        <td class="align-middle">yd</td>
                    </tr>--%>
                    <%--<tr>
                        <td class="align-middle">Inch:</td>
                        <td>
                            <input class="form-control text-danger" type="text" /></td>
                        <td class="align-middle">in</td>
                    </tr>--%>
                    <%--<tr>
                        <td class="align-middle">Lightyear:</td>
                        <td>
                            <input class="form-control text-danger" type="text" /></td>
                        <td class="align-middle">ly</td>
                    </tr>--%>
                </tbody>
            </table>
        </div>
    </div>

    <div class="col-lg-4"></div>
</div>

