<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Dolaris.UnitConverter.Web.Default" %>

<%@ Register Src="~/UnitGroupControl.ascx" TagPrefix="uc1" TagName="UnitGroupControl" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <!-- Your Basic Site Informations -->
    <title>UnitCandy - Unit Converter</title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="UnitCandy lets you convert units of measurement." />
    <meta name="keywords" content="unit converter,unit conversion,units,measurement,conversion,converter" />
    <meta name="author" content="Dolaris" />

    <meta property="og:url" content="https://www.unitcandy.com/" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="UnitCandy" />
    <meta property="og:description" content="UnitCandy lets you convert units of measurement." />
    <meta property="og:image" content="https://www.unitcandy.com/images/UnitcandyExample.jpg" />

    <!-- Mobile Specific Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />

    <!-- Fonts -->
    <%--<link href="https://fonts.googleapis.com/css?family=Noto+Sans:400,700,400italic" rel="stylesheet" type="text/css" />--%>
    <%--<link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css" />--%>

    <!-- Stylesheets -->
    <%--    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/slick.css" />
    <link rel="stylesheet" href="css/slick-theme.css" />
    <link rel="stylesheet" href="css/jquery.fancybox.css" />
    <link rel="stylesheet" href="css/animate.min.css" />
    <link rel="stylesheet" href="css/style.css" />--%>

    <%--[if lt IE 9]>
    	<script src="js/html5.js"></script>
        <script src="js/respond.min.js"></script>
	<![endif]--%>

    <%--[if lt IE 8]>
    	<link rel="stylesheet" href="css/ie-older.css">
    <![endif]--%>

    <%--<noscript><link rel="stylesheet" href="css/no-js.css" /></noscript>--%>
    <%--<link rel="stylesheet" href="css/no-js.css" />--%>

    <%--Favicons--%>
    <link rel="unitcandy-icon" sizes="80x58" href="images/unitcandy_icon.png" />
    <link rel="shortcut icon" sizes="16x16" href="images/favicon.ico" />
    <link rel="shortcut icon" href="images/unitcandy_icon.png" />
    <%--<link rel="apple-touch-icon" href="images/apple-touch-icon.png" />--%>
    <%--<link rel="apple-touch-icon" sizes="72x72" href="images/apple-touch-icon-72x72.png" />--%>
    <%--<link rel="apple-touch-icon" sizes="114x114" href="images/apple-touch-icon-114x114.png" />--%>




    <!-- Google Fonts -->
    <link href="//fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800" rel="stylesheet" />

    <!-- CSS Global Compulsory -->
    <link rel="stylesheet" href="assets/vendor/bootstrap/bootstrap.min.css" />

    <!-- CSS Implementing Plugins -->
    <link rel="stylesheet" href="assets/vendor/icon-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/vendor/icon-line/css/simple-line-icons.css" />
    <link rel="stylesheet" href="assets/vendor/icon-hs/style.css" />
    <link rel="stylesheet" href="assets/vendor/hamburgers/hamburgers.min.css" />
    <link rel="stylesheet" href="assets/vendor/animate.css" />
    <link rel="stylesheet" href="assets/vendor/dzsparallaxer/dzsparallaxer.css" />
    <link rel="stylesheet" href="assets/vendor/dzsparallaxer/dzsscroller/scroller.css" />
    <link rel="stylesheet" href="assets/vendor/dzsparallaxer/advancedscroller/plugin.css" />
    <link rel="stylesheet" href="assets/vendor/fancybox/jquery.fancybox.css" />
    <link rel="stylesheet" href="assets/vendor/cubeportfolio-full/cubeportfolio/css/cubeportfolio.min.css" />
    <link rel="stylesheet" href="assets/vendor/slick-carousel/slick/slick.css" />

    <!-- CSS Template -->
    <link rel="stylesheet" href="assets/css/styles.op-app.css" />

    <!-- CSS Customization -->
    <link rel="stylesheet" href="assets/css/custom.css" />
</head>

<body data-spy="scroll" data-target=".navbar" data-offset="10">

    <!-- Google Analytics -->
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-80895444-2', 'auto');
        ga('send', 'pageview');
    </script>

    <div id="fb-root"></div>
    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.8";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'))


        function test() {
            //www.unitcandy.com.ws.UnitCandyService.Recalculate()
        }
    </script>

    <style>
        #UnitTextBox {
            /*font-size: 18px;*/
        }
    </style>


    <%--Navigation--%>
    <%--<nav id="navigation" class="navbar navbar-inverse navbar-fixed-top" hidden="hidden">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="sr-only">Menu</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <img src="images/UnitcandyIcon32.png" alt="Logo" class="navbar-brand" />
                <a class="navbar-brand smooth-scroll" href="#info" title="UnitCandy lets you convert units of measurement.">UnitCandy</a>
                <div class="header-img">
                    <img src="images/unitcandy_logo.gif" alt="Logo" style="width: 154px; height: 58px" />
                </div>
            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
                <ul class="nav navbar-nav">
                    <li><a href="#Length" class="smooth-scroll">Length</a></li>
                    <li><a href="#Area" class="smooth-scroll">Area</a></li>
                    <li><a href="#Volume" class="smooth-scroll">Volume</a></li>
                    <li><a href="#Temperature" class="smooth-scroll">Temperature</a></li>
                    <li><a href="#Mass" class="smooth-scroll">Mass</a></li>
                    <li>
                        <button id="buttonConvert" class="btn navbar-btn btn-danger" onclick="setFocusToLastUnitControl()" style="margin-left: 10px; margin-right: 10px;">Convert</button>
                        <span><a href="#ContactUnitCandy" class="btn navbar-btn btn-info smooth-scroll" role="button" style="width: 90px !important; color: white !important;">INFO</a></span>
                    </li>
                     Copy to Clipboard 
                    <li><a href="javascript:void(0);" onclick="copyToClipboard()" data-toggle="modal" data-target="#modalCopyToClipboard"><i class="fa fa-copy text-success" data-toggle="tooltip" data-placement="bottom" title="Copy to Clipboard"></i></a></li>
                     Clear 
                    <li><a href="javascript:void(0);" onclick="clearLastUnitGroup()"><i class="fa fa-remove text-danger" data-toggle="tooltip" data-placement="bottom" title="Clear Input"></i></a></li>
                     Refresh 
                    <li><a href="https://www.unitcandy.com/"><i class="fa fa-refresh text-info" data-toggle="tooltip" data-placement="bottom" title="Refresh Site"></i></a></li>
                     Info 
                    <li><a href="#AboutUnitCandy" class="smooth-scroll"><i class="fa fa-info-circle" style="color: white" data-toggle="tooltip" data-placement="bottom" title="More Information"></i></a></li>
                </ul>
            </div>
        </div>
    </nav>--%>


    <section id="header">
        <div class="d-flex justify-content-center align-items-center bg-dark py-1">
            <img src="images/UnitcandyIcon32.png" alt="Logo" class="img-fluid mr-5" />
            <h2 class="mr-5 g-color-white">UnitCandy</h2>
            <button id="button-convert" class="btn btn-warning mr-4 text-uppercase">Convert</button>
            <button id="button-info" class="btn btn-primary px-4 text-uppercase">Info</button>
        </div>
    </section>

    <section id="unit-buttons">
        <div class="container pt-4 pb-2">
            <div class="row col-8 offset-2">
                <div class="d-flex flex-wrap" data-animation="fadeIn" data-animation-delay="500" data-animation-duration="1000">
                    <a href="#" class="btn btn-md u-btn-outline-primary g-mr-10 g-mb-15" data-goto-unitgroup="Length">Length</a>
                    <a href="#" class="btn btn-md u-btn-outline-red g-mr-10 g-mb-15" data-goto-unitgroup="Area">Area</a>
                    <a href="#" class="btn btn-md u-btn-outline-lightred g-mr-10 g-mb-15" data-goto-unitgroup="Volume">Volume</a>
                    <a href="#" class="btn btn-md u-btn-outline-darkred g-mr-10 g-mb-15" data-goto-unitgroup="Temperature">Temperature</a>
                    <a href="#" class="btn btn-md u-btn-outline-blue g-mr-10 g-mb-15" data-goto-unitgroup="Mass">Mass</a>
                    <a href="#" class="btn btn-md u-btn-outline-indigo g-mr-10 g-mb-15" data-goto-unitgroup="Speed">Speed</a>
                    <a href="#" class="btn btn-md u-btn-outline-purple g-mr-10 g-mb-15" data-goto-unitgroup="Energy">Energy</a>
                    <a href="#" class="btn btn-md u-btn-outline-darkpurple g-mr-10 g-mb-15" data-goto-unitgroup="Power">Power</a>
                    <a href="#" class="btn btn-md u-btn-outline-pink g-mr-10 g-mb-15" data-goto-unitgroup="Pressure">Pressure</a>
                    <a href="#" class="btn btn-md u-btn-outline-orange g-mr-10 g-mb-15" data-goto-unitgroup="Time">Time</a>
                    <a href="#" class="btn btn-md u-btn-outline-deeporange g-mr-10 g-mb-15" data-goto-unitgroup="FuelEconomy">Fuel Economy</a>
                    <a href="#" class="btn btn-md u-btn-outline-aqua g-mr-10 g-mb-15" data-goto-unitgroup="Frequency">Frequency</a>
                    <a href="#" class="btn btn-md u-btn-outline-yellow g-mr-10 g-mb-15" data-goto-unitgroup="Acceleration">Acceleration</a>
                    <a href="#" class="btn btn-md u-btn-outline-cyan g-mr-10 g-mb-15" data-goto-unitgroup="Density">Density</a>
                    <a href="#" class="btn btn-md u-btn-outline-teal g-mr-10 g-mb-15" data-goto-unitgroup="Angle">Angle</a>
                    <a href="#" class="btn btn-md u-btn-outline-brown g-mr-10 g-mb-15" data-goto-unitgroup="DigitalStorage">Digital Storage</a>
                    <%--<a href="#" class="btn btn-md u-btn-outline-bluegray g-mr-10 g-mb-15">Blue Gray</a>--%>
                    <%--<a href="#" class="btn btn-md u-btn-outline-darkgray g-mr-10 g-mb-15">Dark Gray</a>--%>
                    <%--<a href="#" class="btn btn-md u-btn-outline-black g-mr-10 g-mb-15">Black</a>--%>
                    <%--<h5 id="DEBUG_lastfocus"></h5>--%>
                </div>
            </div>
        </div>
    </section>



    <%--Info Area--%>
    <div id="info" class="wrap-container60 bg-primary" style="margin-top: 50px" hidden="hidden">
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                    <h1 style="color: white">UnitCandy</h1>
                    <h4 style="color: white">Convert Units of Measurement</h4>
                    <div class="fb-like" data-href="https://www.unitcandy.com/" data-layout="button_count" data-action="like" data-size="large" data-show-faces="true" data-share="true"></div>
                </div>
                <div class="col-md-7">
                    <p>UnitCandy is a utility that helps you convert units of measurement from one system to another. We hope UnitCandy is accurate, complete and easy to use.</p>
                    <p>A unit of measurement is for example “five yards” which represents a length. “Meter” is another unit of length and you can use UnitCandy to convert 5 yards into x meters.</p>
                    <div class="input-group" style="max-width: 375px">
                        <input id="inputFindUnit" type="text" class="form-control" onkeypress="findUnitKeyPressed(event)" autofocus="autofocus" />
                        <div class="input-group-addon">
                            <a id="AnyUnitDropdown" class="dropdown-toggle" data-toggle="dropdown" href="#" title="Select unit of measurement.">Unit&nbsp;<span class="caret"></span></a>
                            <ul class="dropdown-menu dropdown-menu-right pre-scrollable">
                                <asp:Literal ID="AnyUnitLiteral" Text="" runat="server" />
                            </ul>
                        </div>
                    </div>
                    <p class="help-block" style="color: white">Enter, for example, "24in", "60 Fahrenheit", etc.</p>
                </div>
                <div class="col-md-2">
                    <input type="hidden" name="IL_IN_ARTICLE">
                </div>
            </div>
        </div>
    </div>


    <!-- unit groups -->
    <section class="g-theme-bg-gray-light-v1">
        <div class="container-fluid py-5">
            <asp:PlaceHolder ID="UnitGroupsPlaceHolder" runat="server" />
        </div>
    </section>





    <div id="template" runat="server" class="row" hidden="hidden">

        <div class="row">
            <div class="col-lg-2 offset-lg-2">
                <h4 class="mb-3"></h4>
                <h2 class="mb-1"><strong>Length</strong></h2>
                <h6 class="mb-3">Length is any quantity with dimension distance.</h6>
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
                            <tr>
                                <td class="align-middle">Yard:</td>
                                <td>
                                    <input class="form-control text-danger" type="text" /></td>
                                <td class="align-middle">yd</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="col-lg-4"></div>
        </div>

    </div>


    <!-- Main Form -->
    <form id="mainform" runat="server" action="/" method="post">

        <asp:ScriptManager ID="scriptManagerMain" runat="server">
            <Services>
                <asp:ServiceReference Path="~/UnitCandyService.svc" />
            </Services>
        </asp:ScriptManager>

        <asp:PlaceHolder ID="WebGroupsPlaceholder" runat="server" />

    </form>

    <!-- About -->
    <section id="about">
        <div class="d-flex justify-content-center g-bg-brown-opacity-0_1 py-3">

            <div class="mr-5">
                <img src="images/UnitcandyIcon64.png" class="img-fluid" />
            </div>

            <div class="mr-5">
                <h4>UnitCandy</h4>
                <p>Created by Dolaris.</p>
                <p>
                    © 2020 Copyright.<br />
                    All Rights Reserved
                </p>
            </div>

            <div class="mr-0">
                <h4>Contact</h4>
                <p>
                    Dolaris<br />
                    P.O. Box 1801<br />
                    Bellevue, WA 98009<br />
                    United States
                </p>
                <a href="mailto:unitcandycom@gmail.com" class="text-dark">unitcandycom@gmail.com</a>
            </div>

            <div class="mr-0">
                <h4>Privacy</h4>
                <a href="#privacy-policy" class="text-dark" data-toggle="collapse" data-target="#privacy-policy">Click here to read<br />
                    our privacy policy.</a>
            </div>

        </div>
    </section>

    <!-- Links -->
    <section id="links">
        <div class="d-flex justify-content-center g-bg-beige py-4">

            <div class="m-5">
                <h1><a href="https://www.facebook.com/UnitCandycom-331411840674514/" target="_blank"><i class="fa fa-facebook-square g-color-facebook"></i></a></h1>
            </div>

            <div class="m-5">
                <h1><a href="https://twitter.com/unitcandy" target="_blank"><i class="fa fa-twitter g-color-twitter"></i></a></h1>
            </div>

            <div class="m-5">
                <div class="d-flex flex-column align-items-baseline">
                    <h1><a href="https://itunes.apple.com/us/app/unitcandy/id1225441184?mt=8" target="_blank" style="display: inline-block; overflow: hidden; background: url(//linkmaker.itunes.apple.com/assets/shared/badges/en-us/appstore-lrg.svg) no-repeat; width: 135px; height: 40px; background-size: contain;"></a></h1>
                </div>
            </div>

            <div class="m-5">
                <h1><a href="mailto:unitcandycom@gmail.com"><i class="fa fa-envelope g-color-yellow"></i></a></h1>
            </div>

        </div>
    </section>

    <!-- Privacy Policy -->
    <section id="privacy-policy" class="collapse">
        <div class="container">
            <div class="row py-5">
                <div class="col-12">

                    <h3>Privacy Policy</h3>
                    <p>Effective date: July 18, 2019</p>
                    <p>Dolaris ("us", "we", or "our") operates the https://www.unitcandy.com/ website (hereinafter referred to as the "Service").</p>
                    <p>This page informs you of our policies regarding the collection, use and disclosure of personal data when you use our Service and the choices you have associated with that data.</p>
                    <p>We use your data to provide and improve the Service. By using the Service, you agree to the collection and use of information in accordance with this policy. Unless otherwise defined in this Privacy Policy, the terms used in this Privacy Policy have the same meanings as in our Terms and Conditions, accessible from http://www.unitcandy.com/ </p>

                    <h4>Definitions</h4>
                    <ul>
                        <li>
                            <p><strong>Service</strong></p>
                            <p>Service is the https://www.unitcandy.com/ website operated by Dolaris</p>
                        </li>
                        <li>
                            <p><strong>Personal Data</strong></p>
                            <p>Personal Data means data about a living individual who can be identified from those data (or from those and other information either in our possession or likely to come into our possession).</p>
                        </li>
                        <li>
                            <p><strong>Usage Data</strong></p>
                            <p>Usage Data is data collected automatically either generated by the use of the Service or from the Service infrastructure itself (for example, the duration of a page visit).</p>
                        </li>
                        <li>
                            <p><strong>Cookies</strong></p>
                            <p>Cookies are small files stored on your device (computer or mobile device).</p>
                        </li>
                    </ul>

                    <h4>Information Collection and Use</h4>
                    <p>We collect several different types of information for various purposes to provide and improve our Service to you.</p>

                    <h5>Types of Data Collected</h5>

                    <h5>Personal Data</h5>
                    <p>While using our Service, we may ask you to provide us with certain personally identifiable information that can be used to contact or identify you ("Personal Data"). Personally identifiable information may include, but is not limited to:</p>
                    <ul>
                        <li>Cookies and Usage Data</li>
                    </ul>

                    <h4>Usage Data</h4>
                    <p>We may also collect information on how the Service is accessed and used ("Usage Data"). This Usage Data may include information such as your computer's Internet Protocol address (e.g. IP address), browser type, browser version, the pages of our Service that you visit, the time and date of your visit, the time spent on those pages, unique device identifiers and other diagnostic data.</p>

                    <h4>Tracking & Cookies Data</h4>
                    <p>We use cookies and similar tracking technologies to track the activity on our Service and we hold certain information.</p>
                    <p>Cookies are files with a small amount of data which may include an anonymous unique identifier. Cookies are sent to your browser from a website and stored on your device. Other tracking technologies are also used such as beacons, tags and scripts to collect and track information and to improve and analyse our Service.</p>
                    <p>You can instruct your browser to refuse all cookies or to indicate when a cookie is being sent. However, if you do not accept cookies, you may not be able to use some portions of our Service.</p>
                    <p>Examples of Cookies we use:</p>
                    <ul>
                        <li><strong>Session Cookies.</strong> We use Session Cookies to operate our Service.</li>
                        <li><strong>Preference Cookies.</strong> We use Preference Cookies to remember your preferences and various settings.</li>
                        <li><strong>Security Cookies.</strong> We use Security Cookies for security purposes.</li>
                        <li><strong>Advertising Cookies.</strong> Advertising Cookies are used to serve you with advertisements that may be relevant to you and your interests.</li>
                    </ul>

                    <h4>Use of Data</h4>
                    <p>Dolaris uses the collected data for various purposes:</p>
                    <ul>
                        <li>To provide and maintain our Service</li>
                        <li>To notify you about changes to our Service</li>
                        <li>To allow you to participate in interactive features of our Service when you choose to do so</li>
                        <li>To provide customer support</li>
                        <li>To gather analysis or valuable information so that we can improve our Service</li>
                        <li>To monitor the usage of our Service</li>
                        <li>To detect, prevent and address technical issues</li>
                    </ul>

                    <h4>Transfer of Data</h4>
                    <p>Your information, including Personal Data, may be transferred to — and maintained on — computers located outside of your state, province, country or other governmental jurisdiction where the data protection laws may differ from those of your jurisdiction.</p>
                    <p>If you are located outside United States and choose to provide information to us, please note that we transfer the data, including Personal Data, to United States and process it there.</p>
                    <p>Your consent to this Privacy Policy followed by your submission of such information represents your agreement to that transfer.</p>
                    <p>Dolaris will take all the steps reasonably necessary to ensure that your data is treated securely and in accordance with this Privacy Policy and no transfer of your Personal Data will take place to an organisation or a country unless there are adequate controls in place including the security of your data and other personal information.</p>

                    <h4>Disclosure of Data</h4>

                    <h5>Legal Requirements</h5>
                    <p>Dolaris may disclose your Personal Data in the good faith belief that such action is necessary to:</p>
                    <ul>
                        <li>To comply with a legal obligation</li>
                        <li>To protect and defend the rights or property of Dolaris</li>
                        <li>To prevent or investigate possible wrongdoing in connection with the Service</li>
                        <li>To protect the personal safety of users of the Service or the public</li>
                        <li>To protect against legal liability</li>
                    </ul>

                    <h4>Security of Data</h4>
                    <p>The security of your data is important to us but remember that no method of transmission over the Internet or method of electronic storage is 100% secure. While we strive to use commercially acceptable means to protect your Personal Data, we cannot guarantee its absolute security.</p>

                    <h5>Our Policy on "Do Not Track" Signals under the California Online Protection Act (CalOPPA)</h5>
                    <p>We do not support Do Not Track ("DNT"). Do Not Track is a preference you can set in your web browser to inform websites that you do not want to be tracked.</p>
                    <p>You can enable or disable Do Not Track by visiting the Preferences or Settings page of your web browser.</p>

                    <h4>Service Providers</h4>
                    <p>We may employ third party companies and individuals to facilitate our Service ("Service Providers"), provide the Service on our behalf, perform Service-related services or assist us in analysing how our Service is used.</p>
                    <p>These third parties have access to your Personal Data only to perform these tasks on our behalf and are obligated not to disclose or use it for any other purpose.</p>

                    <h5>Analytics</h5>
                    <p>We may use third-party Service Providers to monitor and analyse the use of our Service.</p>
                    <ul>
                        <li>
                            <p><strong>Google Analytics</strong></p>
                            <p>Google Analytics is a web analytics service offered by Google that tracks and reports website traffic. Google uses the data collected to track and monitor the use of our Service. This data is shared with other Google services. Google may use the collected data to contextualise and personalise the ads of its own advertising network.</p>
                            <p>You can opt-out of having made your activity on the Service available to Google Analytics by installing the Google Analytics opt-out browser add-on. The add-on prevents the Google Analytics JavaScript (ga.js, analytics.js and dc.js) from sharing information with Google Analytics about visits activity.</p>
                            <p>For more information on the privacy practices of Google, please visit the Google Privacy & Terms web page: <a href="https://policies.google.com/privacy?hl=en">https://policies.google.com/privacy?hl=en</a></p>
                        </li>
                    </ul>

                    <h5>Advertising</h5>
                    <p>We may use third-party Service Providers to show advertisements to you to help support and maintain our Service.</p>
                    <ul>
                        <li>
                            <p><strong>Bing Ads</strong></p>
                            <p>Bing Ads is an advertising service provided by Microsoft Inc.</p>
                            <p>You can opt-out from Bing Ads by following the instructions on Bing Ads Opt-out page: <a href="https://advertise.bingads.microsoft.com/en-us/resources/policies/personalized-ads">https://advertise.bingads.microsoft.com/en-us/resources/policies/personalized-ads</a></p>
                            <p>For more information about Bing Ads, please visit their Privacy Policy: <a href="https://privacy.microsoft.com/en-us/PrivacyStatement">https://privacy.microsoft.com/en-us/PrivacyStatement</a></p>
                        </li>
                    </ul>

                    <h4>Links to Other Sites</h4>
                    <p>Our Service may contain links to other sites that are not operated by us. If you click a third party link, you will be directed to that third party's site. We strongly advise you to review the Privacy Policy of every site you visit.</p>
                    <p>We have no control over and assume no responsibility for the content, privacy policies or practices of any third party sites or services.</p>

                    <h4>Children's Privacy</h4>
                    <p>Our Service does not address anyone under the age of 18 ("Children").</p>
                    <p>We do not knowingly collect personally identifiable information from anyone under the age of 18. If you are a parent or guardian and you are aware that your Child has provided us with Personal Data, please contact us. If we become aware that we have collected Personal Data from children without verification of parental consent, we take steps to remove that information from our servers.</p>

                    <h4>Changes to This Privacy Policy</h4>
                    <p>We may update our Privacy Policy from time to time. We will notify you of any changes by posting the new Privacy Policy on this page.</p>
                    <p>We will let you know via email and/or a prominent notice on our Service, prior to the change becoming effective and update the "effective date" at the top of this Privacy Policy.</p>
                    <p>You are advised to review this Privacy Policy periodically for any changes. Changes to this Privacy Policy are effective when they are posted on this page.</p>

                    <h4>Contact Us</h4>
                    <p>If you have any questions about this Privacy Policy, please contact us:</p>
                    <ul>
                        <li>By email: unitcandycom@gmail.com</li>
                        <li>By visiting this page on our website: https://www.unitcandy.com/</li>
                    </ul>
                </div>
            </div>
        </div>
    </section>


    <%-- Contact UnitCandy --%>
    <div id="ContactUnitCandy" class="wrap-container6020" hidden="hidden">
        <div class="container">
            <div class="row">
                <div class="col-md-7">
                    <br />
                    <h2><a href="https://twitter.com/unitcandy" target="_blank"><i class="fa fa-twitter" style="color: #00aced;"></i></a></h2>
                    <h3>
                        <a href="https://itunes.apple.com/us/app/unitcandy/id1225441184?mt=8" style="display: inline-block; overflow: hidden; background: url(//linkmaker.itunes.apple.com/assets/shared/badges/en-us/appstore-lrg.svg) no-repeat; width: 135px; height: 40px; background-size: contain;"></a>
                    </h3>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-md-2 wrap-container20">
                    <img src="images/UnitcandyIcon128.png" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 wrap-container20">
                    <h5>© 2017 Copyright. All Rights Reserved.</h5>
                    <h5>Created by Dolaris.</h5>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-10 col-lg-8">



                    <h1>Privacy Policy</h1>


                    <p>Effective date: July 18, 2019</p>


                    <p>Dolaris ("us", "we", or "our") operates the https://www.unitcandy.com/ website (hereinafter referred to as the "Service").</p>

                    <p>This page informs you of our policies regarding the collection, use and disclosure of personal data when you use our Service and the choices you have associated with that data.</p>

                    <h2>Definitions</h2>
                    <ul>
                        <li>
                            <p><strong>Personal Data</strong></p>
                            <p>Personal Data means data about a living individual who can be identified from those data (or from those and other information either in our possession or likely to come into our possession).</p>
                        </li>
                    </ul>


                    <h2>Information Collection and Use</h2>
                    <p>We collect several different types of information for various purposes to provide and improve our Service to you.</p>

                    <h3>Types of Data Collected</h3>

                    <h4>Tracking & Cookies Data</h4>
                    <p>We use cookies and similar tracking technologies to track the activity on our Service and we hold certain information.</p>
                    <p>Cookies are files with a small amount of data which may include an anonymous unique identifier. Cookies are sent to your browser from a website and stored on your device. Other tracking technologies are also used such as beacons, tags and scripts to collect and track information and to improve and analyse our Service.</p>
                    <p>You can instruct your browser to refuse all cookies or to indicate when a cookie is being sent. However, if you do not accept cookies, you may not be able to use some portions of our Service.</p>
                    <p>Examples of Cookies we use:</p>

                    <h2>Transfer of Data</h2>
                    <p>Your information, including Personal Data, may be transferred to — and maintained on — computers located outside of your state, province, country or other governmental jurisdiction where the data protection laws may differ from those of your jurisdiction.</p>
                    <p>If you are located outside United States and choose to provide information to us, please note that we transfer the data, including Personal Data, to United States and process it there.</p>
                    <p>Your consent to this Privacy Policy followed by your submission of such information represents your agreement to that transfer.</p>
                    <p>Dolaris will take all the steps reasonably necessary to ensure that your data is treated securely and in accordance with this Privacy Policy and no transfer of your Personal Data will take place to an organisation or a country unless there are adequate controls in place including the security of your data and other personal information.</p>

                    <h2>Disclosure of Data</h2>


                    <h3>Legal Requirements</h3>
                    <p>Dolaris may disclose your Personal Data in the good faith belief that such action is necessary to:</p>
                    <ul>
                        <li>To comply with a legal obligation</li>
                        <li>To protect and defend the rights or property of Dolaris</li>
                        <li>To prevent or investigate possible wrongdoing in connection with the Service</li>
                        <li>To protect the personal safety of users of the Service or the public</li>
                        <li>To protect against legal liability</li>
                    </ul>

                    <h2>Security of Data</h2>
                    <p>The security of your data is important to us but remember that no method of transmission over the Internet or method of electronic storage is 100% secure. While we strive to use commercially acceptable means to protect your Personal Data, we cannot guarantee its absolute security.</p>

                    <h2>Our Policy on "Do Not Track" Signals under the California Online Protection Act (CalOPPA)</h2>
                    <p>We do not support Do Not Track ("DNT"). Do Not Track is a preference you can set in your web browser to inform websites that you do not want to be tracked.</p>
                    <p>You can enable or disable Do Not Track by visiting the Preferences or Settings page of your web browser.</p>


                    <h2>Service Providers</h2>
                    <p>We may employ third party companies and individuals to facilitate our Service ("Service Providers"), provide the Service on our behalf, perform Service-related services or assist us in analysing how our Service is used.</p>
                    <p>These third parties have access to your Personal Data only to perform these tasks on our behalf and are obligated not to disclose or use it for any other purpose.</p>

                    <h3>Analytics</h3>
                    <p>We may use third-party Service Providers to monitor and analyse the use of our Service.</p>
                    <ul>
                        <li>
                            <p><strong>Google Analytics</strong></p>
                            <p>Google Analytics is a web analytics service offered by Google that tracks and reports website traffic. Google uses the data collected to track and monitor the use of our Service. This data is shared with other Google services. Google may use the collected data to contextualise and personalise the ads of its own advertising network.</p>
                            <p>You can opt-out of having made your activity on the Service available to Google Analytics by installing the Google Analytics opt-out browser add-on. The add-on prevents the Google Analytics JavaScript (ga.js, analytics.js and dc.js) from sharing information with Google Analytics about visits activity.</p>
                            <p>For more information on the privacy practices of Google, please visit the Google Privacy & Terms web page: <a href="https://policies.google.com/privacy?hl=en">https://policies.google.com/privacy?hl=en</a></p>
                        </li>
                    </ul>

                    <h3>Advertising</h3>
                    <p>We may use third-party Service Providers to show advertisements to you to help support and maintain our Service.</p>
                    <ul>
                        <li>
                            <p><strong>Bing Ads</strong></p>
                            <p>Bing Ads is an advertising service provided by Microsoft Inc.</p>
                            <p>You can opt-out from Bing Ads by following the instructions on Bing Ads Opt-out page: <a href="https://advertise.bingads.microsoft.com/en-us/resources/policies/personalized-ads">https://advertise.bingads.microsoft.com/en-us/resources/policies/personalized-ads</a></p>
                            <p>For more information about Bing Ads, please visit their Privacy Policy: <a href="https://privacy.microsoft.com/en-us/PrivacyStatement">https://privacy.microsoft.com/en-us/PrivacyStatement</a></p>
                        </li>
                    </ul>





                    <h2>Links to Other Sites</h2>
                    <p>Our Service may contain links to other sites that are not operated by us. If you click a third party link, you will be directed to that third party's site. We strongly advise you to review the Privacy Policy of every site you visit.</p>
                    <p>We have no control over and assume no responsibility for the content, privacy policies or practices of any third party sites or services.</p>


                    <h2>Children's Privacy</h2>
                    <p>Our Service does not address anyone under the age of 18 ("Children").</p>
                    <p>We do not knowingly collect personally identifiable information from anyone under the age of 18. If you are a parent or guardian and you are aware that your Child has provided us with Personal Data, please contact us. If we become aware that we have collected Personal Data from children without verification of parental consent, we take steps to remove that information from our servers.</p>


                    <h2>Changes to This Privacy Policy</h2>
                    <p>We may update our Privacy Policy from time to time. We will notify you of any changes by posting the new Privacy Policy on this page.</p>
                    <p>We will let you know via email and/or a prominent notice on our Service, prior to the change becoming effective and update the "effective date" at the top of this Privacy Policy.</p>
                    <p>You are advised to review this Privacy Policy periodically for any changes. Changes to this Privacy Policy are effective when they are posted on this page.</p>


                    <h2>Contact Us</h2>
                    <p>If you have any questions about this Privacy Policy, please contact us:</p>
                    <ul>
                        <li>By email: unitcandycom@gmail.com</li>
                        <li>By visiting this page on our website: https://www.unitcandy.com/</li>

                    </ul>
                </div>
            </div>
        </div>
    </div>


    <!-- JS Global Compulsory -->
    <script src="assets/vendor/jquery/jquery.min.js"></script>
    <script src="assets/vendor/jquery-migrate/jquery-migrate.min.js"></script>
    <script src="assets/vendor/popper.js/popper.min.js"></script>
    <script src="assets/vendor/bootstrap/bootstrap.min.js"></script>

    <!-- JS Implementing Plugins -->
    <script src="assets/vendor/appear.js"></script>
    <script src="assets/vendor/dzsparallaxer/dzsparallaxer.js"></script>
    <script src="assets/vendor/dzsparallaxer/dzsscroller/scroller.js"></script>
    <script src="assets/vendor/dzsparallaxer/advancedscroller/plugin.js"></script>
    <script src="assets/vendor/fancybox/jquery.fancybox.js"></script>
    <script src="assets/vendor/cubeportfolio-full/cubeportfolio/js/jquery.cubeportfolio.min.js"></script>
    <script src="assets/vendor/slick-carousel/slick/slick.js"></script>

    <!-- JS Unify -->
    <script src="assets/js/hs.core.js"></script>
    <script src="assets/js/components/hs.header.js"></script>
    <script src="assets/js/helpers/hs.hamburgers.js"></script>
    <script src="assets/js/components/hs.scroll-nav.js"></script>
    <script src="assets/js/components/hs.onscroll-animation.js"></script>
    <script src="assets/js/components/hs.tabs.js"></script>
    <script src="assets/js/components/hs.cubeportfolio.js"></script>
    <script src="assets/js/components/hs.popup.js"></script>
    <script src="assets/js/components/hs.carousel.js"></script>
    <script src="assets/js/components/hs.go-to.js"></script>


    <!-- JS Customization -->
    <%--<script src="../../assets/js/custom.js"></script>--%>
    <script src="unitcandy.js"></script>

    <!-- JS Plugins Init. -->
    <script>
        $(document).on('ready', function () {
            // initialization of carousel
            $.HSCore.components.HSCarousel.init('.js-carousel');

            // initialization of header
            $.HSCore.components.HSHeader.init($('#js-header'));
            $.HSCore.helpers.HSHamburgers.init('.hamburger');

            // initialization of tabs
            $.HSCore.components.HSTabs.init('[role="tablist"]');

            // initialization of scroll animation
            $.HSCore.components.HSOnScrollAnimation.init('[data-animation]');

            // initialization of go to section
            $.HSCore.components.HSGoTo.init('.js-go-to');

            // initialization of popups
            $.HSCore.components.HSPopup.init('.js-fancybox-media', {
                helpers: {
                    media: {},
                    overlay: {
                        css: {
                            'background': 'rgba(255, 255, 255, .8)'
                        }
                    }
                }
            });
        });

        $(window).on('load', function () {
            // initialization of HSScrollNav
            $.HSCore.components.HSScrollNav.init($('#js-scroll-nav'), {
                duration: 700
            });

            // initialization of cubeportfolio
            $.HSCore.components.HSCubeportfolio.init('.cbp');
        });

        $(window).on('resize', function () {
            setTimeout(function () {
                $.HSCore.components.HSTabs.init('[role="tablist"]');
            }, 200);
        });

        // Add smooth scrolling to all links
        $("a").on('click', function (event) {

            // Make sure this.hash has a value before overriding default behavior
            if (this.hash !== "") {
                // Prevent default anchor click behavior
                event.preventDefault();

                // Store hash
                var hash = this.hash;

                // Using jQuery's animate() method to add smooth page scroll
                // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function () {

                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
            } // End if
        });
    </script>













    <%--JavaScript--%>
    <%-- <script type="text/javascript" src="js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="js/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/isotope.pkgd.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>--%>

    <%--<script type="text/javascript" src="Dolaris.UnitConverter.js"></script>--%>
    <%--<script type="text/javascript" src="unitcandy.js"></script>--%>

    <script type="text/javascript">
        var infolinks_pid = 3190727;
        var infolinks_wsid = 2;
    </script>
    <script type="text/javascript" src="https://resources.infolinks.com/js/infolinks_main.js"></script>

</body>

</html>
