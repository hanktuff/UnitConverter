<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="Dolaris.UnitConverter.Web.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <!-- Your Basic Site Informations -->
    <title>Test Page</title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="UnitCandy lets you convert units of measurement." />
    <meta name="keywords" content="Dolaris, UnitCandy, units, measurement, Landing Page" />
    <meta name="author" content="Dolaris" />

    <!-- Mobile Specific Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />

    <!-- Fonts -->
    <link href="http://fonts.googleapis.com/css?family=Noto+Sans:400,700,400italic" rel="stylesheet" type="text/css" />
    <link href="http://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css" />

    <!-- Stylesheets -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/slick.css" />
    <link rel="stylesheet" href="css/slick-theme.css" />
    <link rel="stylesheet" href="css/jquery.fancybox.css" />
    <link rel="stylesheet" href="css/animate.min.css" />
    <link rel="stylesheet" href="css/style.css" />

    <!-- Custom Colors -->
    <link rel="stylesheet" href="css/colors/blue.css" />

    <%--<noscript><link rel="stylesheet" href="css/no-js.css" /></noscript>--%>
    <link rel="stylesheet" href="css/no-js.css" />

    <!-- Favicons -->
    <link rel="unitcandy-icon" sizes="80x58" href="images/unitcandy_icon.png" />
    <link rel="shortcut icon" sizes="16x16" href="images/favicon.ico" />
    <link rel="shortcut icon" href="images/unitcandy_icon.png" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/smoothness/jquery-ui.css" />

</head>


<body>

    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">WebSiteName</a>
            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Home</a></li>
                    <li><a href="#">Page 1</a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Page 2 <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="#">Page 2a</a></li>
                            <li><a href="#">Page 2b</a></li>
                            <li><a href="#">Page 2c</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Page 3</a></li>
                    <li><a href="#">Page 4</a></li>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="#"><span class="glyphicon glyphicon-user"></span>Sign Up</a></li>
                    <li><a href="#"><span class="glyphicon glyphicon-log-in"></span>Login</a></li>
                </ul>
            </div>
        </div>
    </nav>



    <form runat="server" id="testform1">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/UnitCandyService.svc" />
            </Services>
        </asp:ScriptManager>
        <br />
        <br />
        <input id="PingButton" type="button" value="Ping..." onclick="onPingUnitCandyService()" title="Pings the UnitCandy service." />
        <p id="pingResultParagraph"></p>

        <script type="text/javascript">
            function onPingUnitCandyService() {
                var ping = www.unitcandy.com.ws.UnitCandyService.Ping(onPingUnitCandyServiceComplete, onPingUnitCandyServiceError);
            }

            function onPingUnitCandyServiceComplete(result) {
                pingResultParagraph.innerHTML = result;
            }

            function onPingUnitCandyServiceError(err) {
                pingResultParagraph.innerHTML = err;
            }


            var lastUnitGroupName = '';

            function handleKeyPress(e, id) {

                lastUnitGroupName = e.srcElement.dataset.unitgroupname;

                var key = e.keyCode || e.which;
                if (key == 13) {
                    onUnitChanged(id);
                }
            }


            function onUnitChanged(id) {
                var control = $('#' + id);

                if (control.length == 1) {

                    var unitName = control[0].dataset.unitname;
                    var unitGroupName = control[0].dataset.unitgroupname
                    var unitValue = control[0].value;

                    var recalc = www.unitcandy.com.ws.UnitCandyService.Recalculate(unitName, unitValue, onUnitChangedCompleted, onUnitChangedError);
                }
            }

            function onUnitChangedCompleted(result) {
                if (result == null) { return; }

                for (var i = 0; i < result.length; i++) {

                    var unitName = result[i].UnitName;
                    var unitValue = result[i].UnitValue;
                    var errorString = result[i].ErrorString;

                    var controls = $('[data-unitname=' + unitName + ']');

                    if (controls.length == 1) {

                        if (errorString == '') {
                            controls[0].value = unitValue;
                            controls[0].placeholder = '';

                        } else {
                            controls[0].value = '';
                            controls[0].placeholder = errorString;
                        }
                    }
                }
            }

            function onUnitChangedError(err) {
                rndNumberResult.innerHTML = err._message;
                //alert(err._message);
            }

            function onCopyToClipboard() {
                var copyToClipboard = www.unitcandy.com.ws.UnitCandyService.GetCopyToClipboardText("Length", onCopyToClipboardComplete, onCopyToClipboardError);
                return false;
            }

            function onCopyToClipboardComplete(result) {

                //result = "abc\r\ndefghijkl\r\nmnop\r1234\nqqqqqqqq";
                //result = "This is my statement one.&#13;&#10;This is my statement2";

                //textareaCopyToClipboard.innerText = result;
                textareaCopyToClipboard.innerHTML = result;
                textareaCopyToClipboard.focus();
            }

            function onCopyToClipboardError(err) {
            }

            function onFindUnit() {
                var control = $('#inputAnyUnit')[0];
                var fndUnit = www.unitcandy.com.ws.UnitCandyService.FindUnit(control.value, onFindUnitComplete, onFindUnitError);
            }

            function onFindUnitComplete(result) {
                var control = $('#inputAnyUnit')[0];

                if (result != null) {
                    control.value = result.UnitValue + ' ' + result.UnitName;

                } else {
                    control.value = "???"
                }
            }

            function onFindUnitError(err) {
            }
        </script>

        <div class=" wrap-container6020 bg-dark">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-md-12">
                        <div class="col-md-1">Column 1</div>
                        <div class="col-md-1">Column 2</div>
                        <div class="col-md-1">Column 3</div>
                        <div class="col-md-1">Column 4</div>
                        <div class="col-md-1">Column 5</div>
                        <div class="col-md-1">Column 6</div>
                        <div class="col-md-1">Column 7</div>
                        <div class="col-md-1">Column 8</div>
                        <div class="col-md-1">Column 9</div>
                        <div class="col-md-1">Column 10</div>
                        <div class="col-md-1">Column 11</div>
                        <div class="col-md-1">Column 12</div>
                    </div>

                    <div class="col-md-3">
                        <h1>Temperature</h1>
                        <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur officia deserunt sunt in culpa qui officia deserunt mollit</p>
                    </div>

                    <div class="col-sm-10 col-md-5 col-sm-offset-1 col-md-offset-0">
                        <div class="form-group">
                            <div class="form-inline">
                                <label id="UnitNameLabel1" runat="server" for="UnitTextBox1" class="control-label col-md-3">Meters:</label>
                                <div class="input-group col-md-7">
                                    <input type="text" id="UnitTextBox1" onchange="onUnitChanged(id)" onkeypress="handleKeyPress(event, id)" data-unitname="Meter" data-unitgroupname="Length" class="form-control" title="tooltip" />
                                    <div id="UnitTextBoxSymbol1" runat="server" class="input-group-addon">m</div>
                                </div>
                            </div>
                            <label class="control-label col-md-3"></label>
                            <div class="input-group col-md-7">
                                <div hidden="hidden" data-helper="Meter">
                                    <!--   +1 | -1 | x10 | &divide; (247)10 | Min | Max | Clear   -->
                                    <a href="javascript:void(0)">+1</a>
                                    <span>&nbsp;|&nbsp;</span>
                                    <a href="javascript:void(0)">-1</a>
                                    <span>&nbsp;|&nbsp;</span>
                                    <a href="javascript:void(0)">x10</a>
                                    <span>&nbsp;|&nbsp;</span>
                                    <a href="javascript:void(0)">&divide;10</a>
                                    <span>&nbsp;|&nbsp;</span>
                                    <a href="javascript:void(0)">Clear</a>
                                    <span>&nbsp;|&nbsp;</span>
                                    <a href="javascript:void(0)">Min</a>
                                    <span>&nbsp;|&nbsp;</span>
                                    <a href="javascript:void(0)">Max</a>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-inline">
                                <label id="UnitNameLabel2" runat="server" for="UnitTextBox2" class="control-label col-md-3">Yards:</label>
                                <div class="input-group col-md-7">
                                    <%--<asp:TextBox ID="UnitTextBox2" runat="server" UnitName="Yard" UnitGroupName="Length" CssClass="form-control" AutoPostBack="true" ClientIDMode="Static" ToolTip="" BorderWidth="0px" />--%>
                                    <input type="text" id="UnitTextBox2" onchange="onUnitChanged(id)" onkeypress="handleKeyPress(event, id)" data-unitname="Yard" data-unitgroupname="Length" class="form-control" title="tooltip" />
                                    <div id="UnitTextBoxSymbol2" runat="server" class="input-group-addon">yd</div>
                                    <div hidden="hidden" data-helper="Yard">
                                    </div>
                                </div>
                                <label class="control-label col-md-3"></label>
                                <div class="input-group col-md-7">
                                    <div hidden="hidden" data-helper="Yard">
                                        <!--   +1 | -1 | x10 | &divide; (247)10 | Min | Max | Clear   -->
                                        <a href="javascript:void(0)">+1</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">-1</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">x10</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">&divide;10</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">Clear</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">Min</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">Max</a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-inline">
                                    <label id="UnitNameLabel3" runat="server" for="UnitTextBox3" class="control-label col-md-3">Feet:</label>
                                    <div class="input-group col-md-7">
                                        <%--<asp:TextBox ID="UnitTextBox3" runat="server" UnitName="Feet" UnitGroupName="Length" CssClass="form-control" AutoPostBack="true" ClientIDMode="Static" ToolTip="" BorderWidth="0px" />--%>
                                        <input type="text" id="UnitTextBox3" onchange="onUnitChanged(id)" onkeypress="handleKeyPress(event, id)" data-unitname="Foot" data-unitgroupname="Length" class="form-control" title="tooltip" />
                                        <div id="UnitTextBoxSymbol3" runat="server" class="input-group-addon">ft</div>
                                    </div>
                                </div>
                                <label class="control-label col-md-3"></label>
                                <div class="input-group col-md-7">
                                    <div hidden="hidden" data-helper="Foot">
                                        <!--   +1 | -1 | x10 | &divide; (247)10 | Min | Max | Clear   -->
                                        <a href="javascript:void(0)">+1</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">-1</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">x10</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">&divide;10</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">Clear</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">Min</a>
                                        <span>&nbsp;|&nbsp;</span>
                                        <a href="javascript:void(0)">Max</a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-inline">
                                    <label id="Label1" runat="server" for="UnitTextBox4" class="control-label col-md-3">Miles:</label>
                                    <div class="input-group col-md-7">
                                        <input type="text" id="UnitTextBox4" data-unitname="Mile" data-unitgroupname="Length" class="form-control autocomplete" title="tooltip" />
                                        <div hidden="hidden" data-helper="Mile">
                                            <!--   +1 | -1 | x10 | &divide; (247)10 | Min | Max | Clear   -->
                                            <a href="javascript:void(0)">+1</a>
                                            <span>&nbsp;|&nbsp;</span>
                                            <a href="javascript:void(0)">-1</a>
                                            <span>&nbsp;|&nbsp;</span>
                                            <a href="javascript:void(0)">x10</a>
                                            <span>&nbsp;|&nbsp;</span>
                                            <a href="javascript:void(0)">&divide;10</a>
                                            <span>&nbsp;|&nbsp;</span>
                                            <a href="javascript:void(0)">Clear</a>
                                            <span>&nbsp;|&nbsp;</span>
                                            <a href="javascript:void(0)">Min</a>
                                            <span>&nbsp;|&nbsp;</span>
                                            <a href="javascript:void(0)">Max</a>
                                        </div>
                                        <%--<div id="Div1" runat="server" class="input-group-addon">ft</div>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-inline">
                                    <label for="inputAnyUnit" class="control-label col-md-3">Any Unit:</label>
                                    <div class="input-group col-md-4">
                                        <input type="text" id="inputAnyUnit" class="form-control ui-autocomplete" title="enter something like 3.14m" />
                                        <button type="button" class="btn btn-info" onclick="onFindUnit(inputAnyUnit.Value)">Go</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="input-group">
                        <input type="text" class="form-control" aria-label="Text input with dropdown button" />
                        <div class="input-group-addon">
                            <%--<button type="button" class="btn btn-green" >Unit</button>--%>
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#" title="Click here for more units of measurement.">More...<span class="caret"></span></a>
                            <ul class="dropdown-menu dropdown-menu-right pre-scrollable">
                                <li><a href="#Speed" class="smooth-scroll">Speed</a></li>
                                <li><a href="#Energy" class="smooth-scroll">Energy</a></li>
                                <li><a href="#Power" class="smooth-scroll">Power</a></li>
                                <li><a href="#Pressure" class="smooth-scroll">Pressure</a></li>
                                <li><a href="#Time" class="smooth-scroll">Time</a></li>
                                <li><a href="#Time" class="smooth-scroll">Fuel Economy</a></li>
                                <li><a href="#Speed" class="smooth-scroll">Speed</a></li>
                                <li><a href="#Energy" class="smooth-scroll">Energy</a></li>
                                <li><a href="#Power" class="smooth-scroll">Power</a></li>
                                <li><a href="#Pressure" class="smooth-scroll">Pressure</a></li>
                                <li><a href="#Time" class="smooth-scroll">Time</a></li>
                                <li><a href="#Time" class="smooth-scroll">Fuel Economy</a></li>
                                <li><a href="#Speed" class="smooth-scroll">Speed</a></li>
                                <li><a href="#Energy" class="smooth-scroll">Energy</a></li>
                                <li><a href="#Power" class="smooth-scroll">Power</a></li>
                                <li><a href="#Pressure" class="smooth-scroll">Pressure</a></li>
                                <li><a href="#Time" class="smooth-scroll">Time</a></li>
                                <li><a href="#Time" class="smooth-scroll">Fuel Economy</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur officia deserunt sunt in culpa qui officia deserunt mollit</p>
                        <p>Excepteur fugiat occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim est laborum perspiciatis unde omnis natus esse cillum</p>
                    </div>

                    <!-- .row end -->

                </div>
                <!-- .container end -->
            </div>
        </div>

        <%-- Write to File on Server --%>
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <asp:Button ID="buttonWriteToFile" Text="Write To File" runat="server" OnClick="buttonWriteToFile_Click" CssClass="btn btn-info" ToolTip="Write to a (log)file on the server." />
                    <asp:Label ID="labelWriteToFile" Text="(result of writing to file)" runat="server" />
                    <%--<i class="fa fa-copy">Copy</i>--%>
                    <%--<asp:PlaceHolder ID="placeholderWriteToFile" runat="server" />--%>
                    <%--<asp:Literal ID="literalWriteToFile" Text="" runat="server" />--%>
                </div>
            </div>
        </div>
    </form>


    <!-- Trigger the modal with a button -->
    <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>

    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog bg-dark">

            <!-- Modal content-->
            <div class="modal-content bg-dark">
                <div class="modal-header bg-dark">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <%--<a href="" class="btn-custom btn-more-link animation animated animation-fade-in-right" data-animation="animation-fade-in-right" data-delay="600">&times;</a>--%>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">About UnitCandy</h4>
                </div>
                <div class="modal-body">
                    <div class="bg-dark">
                        <div class="container">

                            <%--<div class="row">
                                <div class="col-md-12 wrap-container10">
                                    <h4>About UnitCandy</h4>
                                </div>
                            </div>--%>

                            <div class="row">
                                <div class="col-md-2 wrap-container20">
                                    <img src="images/content/icon/robot.png" />
                                </div>
                                <div class="col-md-4 wrap-container20">
                                    <h5>UnitCandy is located at this address:</h5>
                                    <h3>Dolaris<br />
                                        P.O. Box 2187<br />
                                        Kirland, WA 98083<br />
                                        United States<br />
                                    </h3>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 wrap-container20">
                                    <p>
                                        UnitCandy is trying to help you with your everyday unit conversion problems. It contains many units of measurement commonly encountered and converts them to other units fast and accurate. UnitCandy was designed to be nice-looking and user friendly.
                                    </p>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<a href="Default.aspx" class="btn-custom btn-more-link animation animated animation-fade-in-right" data-animation="animation-fade-in-right" data-delay="600">Close</a>--%>
                    <%--<input type="button" class="close" data-dismiss="modal" value="Close" />--%>
                    <%--<a href="" class=" close btn-custom btn-more-link animation animated animation-fade-in-right" data-animation="animation-fade-in-right" data-delay="1000">Close</a>--%>
                    <a href="javascript:void(0)" class="close btn" data-animation="animation-fade-in-right" data-delay="1000">Close</a>
                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                    <%--<div class="row">
                        <div class="col-md-5 wrap-container60">
                        </div>
                    </div>--%>
                </div>
            </div>

        </div>
    </div>

    <!-- Copy to Clipboard -->
    <button type="button" class="btn btn-info btn-lg" onclick="onCopyToClipboard()" data-toggle="modal" data-target="#modalCopyToClipboard">Copy To Clipboard</button>

    <div id="modalCopyToClipboard" class="modal fade" role="dialog">
        <div class="modal-dialog bg-info">
            <div class="modal-content bg-info">
                <%--<div class="modal-header bg-dark"></div>--%>
                <div class="modal-body">
                    <div class="bg-grey">
                        <div class="container">
                            <%--<div class="row">
                                <h4>Copy to Clipboard</h4>
                            </div>--%>
                            <div class="row">
                                <%--<div class="col-md-2 wrap-container20">
                                    <img src="images/content/icon/papers.png" />
                                </div>--%>
                                <div class="col-md-5">
                                    <h5>Copy to Clipboard</h5>
                                    <h6>Select everything from the text area and press the key combination on your system that copies the text to your clipboard (on Windows this is typically CTRL+C).</h6>
                                </div>
                            </div>
                            <div class="row">
                                <div>
                                    <textarea id="textareaCopyToClipboard" rows="10" onclick="this.select()" class="col-md-5"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<a href="Default.aspx" class="btn-custom btn-more-link animation animated animation-fade-in-right" data-animation="animation-fade-in-right" data-delay="600">Close</a>--%>
                    <%--<input type="button" class="close" data-dismiss="modal" value="Close" />--%>
                    <%--<a href="" class=" close btn-custom btn-more-link animation animated animation-fade-in-right" data-animation="animation-fade-in-right" data-delay="1000">Close</a>--%>
                    <%--<a href="" class="close btn" data-animation="animation-fade-in-right" data-delay="1000">Close</a>--%>
                    <button type="button" class="btn btn-default btn-blue" data-dismiss="modal">Close</button>
                    <%--<div class="row">
                        <div class="col-md-5 wrap-container60">
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>



    <div id="MainDiv" runat="server">
        <div class="wrap-container60">
            <div class="header-content">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="header-text">
                                <div class="header-heading">
                                    <h1 id="UnitGroupTitle" runat="server" data-toggle="collapse" data-target=".colapz" style="cursor: pointer">Area</h1>
                                    <%--<a href="javascript:void(0)" class="h1" data-toggle="collapse" data-target=".colapz" title="show / hide">Area</a>--%>
                                    <h5 id="UnitGroupDescription" runat="server" class="colapz collapse">This is test of how to collapse stuff.</h5>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5 colapz collapse">
                            <div class="form-group">
                                <div class="form-inline">
                                    <label id="UnitNameLabel" runat="server" for="UnitTextBox" class="control-label col-sm-4 col-md-3">Hectar:</label>
                                    <div class="input-group">
                                        <input type="text" id="UnitTextBox" runat="server" class="form-control" onchange="unitChanged(event)" onkeypress="unitChangedKeyPressed(event)" data-unitname="" data-unitgroupname="" data-toggle="tooltip" data-placement="top" title="" />
                                        <div id="UnitTextBoxSymbol" runat="server" class="input-group-addon" style="width: 55px; border-width: 0px">hect</div>
                                    </div>
                                    <label class="control-label col-md-3"></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Region Colors --%>
    <div>
        <div class="bg-color">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-color</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-danger">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-danger</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-info">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-info</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-dark">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-dark</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-grey">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-grey</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-info">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-info</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-primary">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-primary</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-success">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-success</h3>
                            </div>
                        </div>
                    </div>


                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>

        <div class="bg-warning">
            <!-- .container -->
            <div class="container">

                <!-- .row -->
                <div class="row">

                    <div class="col-sm-10 col-md-6 col-sm-offset-1 col-md-offset-0 text-center-sm text-center-xs">
                        <div class="margin-bottom40">
                            <div class="post-heading-left text-center-sm text-center-xs">
                                <%--<h4>Flatland Landing Page</h4>--%>
                                <h3>bg-warning</h3>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- .row end -->

            </div>
            <!-- .container end -->
        </div>
    </div>

    <footer>
        <p>Footer.</p>
    </footer>


    <!-- JavaScript -->
    <script type="text/javascript" src="js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="js/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.min.js"></script>
    <script type="text/javascript" src="js/smoothscroll.js"></script>
    <script type="text/javascript" src="js/response.min.js"></script>
    <script type="text/javascript" src="js/jquery.placeholder.min.js"></script>
    <script type="text/javascript" src="js/jquery.fitvids.js"></script>
    <script type="text/javascript" src="js/jquery.imgpreload.min.js"></script>
    <script type="text/javascript" src="js/waypoints.min.js"></script>
    <script type="text/javascript" src="js/slick.min.js"></script>
    <script type="text/javascript" src="js/jquery.mousewheel-3.0.6.pack.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.pack.js"></script>
    <script type="text/javascript" src="js/parallax.min.js"></script>
    <script type="text/javascript" src="js/jquery.counterup.min.js"></script>
    <script type="text/javascript" src="js/isotope.pkgd.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>

    <script type="text/javascript">
        $(function () {
            var availableTags = [
              "ActionScript", "AppleScript", "Asp", "BASIC", "C", "C++",
              "Clojure", "COBOL", "ColdFusion", "Erlang", "Fortran",
              "Groovy", "Haskell", "Java", "JavaScript", "Lisp", "Perl",
              "PHP", "Python", "Ruby", "Scala", "Scheme"
            ];

            //$(".autocomplete").autocomplete({
            //    source: availableTags
            //});
        });


        $('input').on('focus', function (e) {

            var unitname = e.srcElement.dataset.unitname;

            if (unitname != null) {
                $('[data-helper=' + unitname + ']').show();
            }
        });

        $('input').on('blur', function (e) {

            var unitname = e.srcElement.dataset.unitname;

            if (unitname != null) {
                $('[data-helper=' + unitname + ']').hide();
            }
        });
    </script>

</body>

</html>
