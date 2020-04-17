<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitGroupControl.ascx.cs" Inherits="Dolaris.UnitConverter.Web.UnitGroupControl" ClientIDMode="Static" %>

<div id="MainDiv" runat="server">
    <div class="wrap-container60">
        <div class="header-content">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">
                        <div class="header-text">
                            <div class="header-heading">
                                <h1 id="UnitGroupTitle" runat="server" data-toggle="collapse" data-target=".unitscollapse" class="animation" data-animation="animation-fade-in-left"></h1>
                                <h5 id="UnitGroupDescription" runat="server" class="unitscollapse animation" data-animation="animation-fade-in-left" data-delay="3000"></h5>
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
                                        <%--<li><a id="CopyAnchorToClipboard" runat="server" href="javascript:void(0)" onclick="copyAnchorToClipboard('')">Copy Anchor</a></li>--%>
                                        <%--<li role="separator" class="divider"></li>--%>
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
</div>
