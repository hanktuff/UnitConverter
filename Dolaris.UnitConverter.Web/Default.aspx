cr<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Dolaris.UnitConverter.Web.Default" %>

<%@ Register Src="~/UnitGroupControl.ascx" TagPrefix="uc1" TagName="UnitGroupControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <!-- Title -->
    <title>UnitCandy - Unit Converter</title>

    <!-- Favicons -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta http-equiv="x-ua-compatible" content="IE=edge" />

    <!-- Data for Search Engine -->
    <meta name="description" content="UnitCandy lets you convert units of measurement." />
    <meta name="keywords" content="unit converter,unit conversion,units,measurement,conversion,converter" />
    <meta name="author" content="Dolaris" />

    <!-- Facebook Information -->
    <meta property="og:url" content="https://www.unitcandy.com/" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="UnitCandy" />
    <meta property="og:description" content="UnitCandy lets you convert units of measurement." />
    <meta property="og:image" content="https://www.unitcandy.com/images/UnitcandyExample.jpg" />

    <!-- Favicons -->
    <link rel="unitcandy-icon" sizes="80x58" href="images/unitcandy_icon.png" />
    <link rel="shortcut icon" sizes="16x16" href="images/favicon.ico" />
    <link rel="shortcut icon" href="images/unitcandy_icon.png" />

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

<body id="topofpage" data-spy="scroll" data-target=".navbar" data-offset="10">

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-80895444-2"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-80895444-2');
    </script>

    <!-- Like on Facebook -->
    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.8";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'))
    </script>

    <!-- Navigation -->
    <nav id="navigation" class="navbar navbar-expand-sm sticky-top navbar-light bg-light">
        <div class="container">

            <img id="unitcandy-icon" src="images/UnitcandyIcon32.png" class="img-fluid mr-4" />
            <ul class="navbar-nav mr-5">
                <li class="nav-item">
                    <a class="nav-link" href="#topofpage">QuoteCandy</a>
                </li>
            </ul>

            <input id="any-unit" class="form-control text-dark w-50 mr-5" type="text"
                data-toggle="popover" title="Examples:" data-content="24in<br />60 Fahrenheit<br />16 gal" data-placement="bottom" data-html="true" data-trigger="hover" />

            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbar-toggler1" aria-controls="navbar-toggler1" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbar-toggler1">
                <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
                    <li class="nav-item">
                        <a class="nav-link" href="#about">About</a>
                    </li>
                </ul>
            </div>

        </div>
    </nav>

    <!-- Unit Buttons -->
    <section id="unit-buttons">
        <div class="container-fluid justify-content-center pt-4 pb-2">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-6 offset-xl-3" data-animation="fadeIn">
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-primary g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Length">Length</a>
                    <a href="javascript:void(0)" class="h5 g-color-primary m-3 g-hidden-md-up" data-goto-unitgroup="Length">Length</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-red g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Area">Area</a>
                    <a href="javascript:void(0)" class="h5 g-color-red m-3 g-hidden-md-up" data-goto-unitgroup="Area">Area</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-lightred g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Volume">Volume</a>
                    <a href="javascript:void(0)" class="h5 g-color-lightred m-3 g-hidden-md-up" data-goto-unitgroup="Volume">Volume</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-darkred g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Temperature">Temperature</a>
                    <a href="javascript:void(0)" class="h5 g-color-darkred m-3 g-hidden-md-up" data-goto-unitgroup="Temperature">Temperature</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-blue g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Mass">Mass</a>
                    <a href="javascript:void(0)" class="h5 g-color-blue m-3 g-hidden-md-up" data-goto-unitgroup="Mass">Mass</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-indigo g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Speed">Speed</a>
                    <a href="javascript:void(0)" class="h5 g-color-indigo m-3 g-hidden-md-up" data-goto-unitgroup="Speed">Speed</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-purple g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Energy">Energy</a>
                    <a href="javascript:void(0)" class="h5 g-color-purple m-3 g-hidden-md-up" data-goto-unitgroup="Energy">Energy</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-darkpurple g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Power">Power</a>
                    <a href="javascript:void(0)" class="h5 g-color-darkpurple m-3 g-hidden-md-up" data-goto-unitgroup="Power">Power</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-pink g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Pressure">Pressure</a>
                    <a href="javascript:void(0)" class="h5 g-color-pink m-3 g-hidden-md-up" data-goto-unitgroup="Pressure">Pressure</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-orange g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Time">Time</a>
                    <a href="javascript:void(0)" class="h5 g-color-orange m-3 g-hidden-md-up" data-goto-unitgroup="Time">Time</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-deeporange g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="FuelEconomy">Fuel Economy</a>
                    <a href="javascript:void(0)" class="h5 g-color-deeporange m-3 g-hidden-md-up" data-goto-unitgroup="FuelEconomy">Fuel Economy</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-aqua g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Frequency">Frequency</a>
                    <a href="javascript:void(0)" class="h5 g-color-aqua m-3 g-hidden-md-up" data-goto-unitgroup="Frequency">Frequency</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-yellow g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Acceleration">Acceleration</a>
                    <a href="javascript:void(0)" class="h5 g-color-yellow m-3 g-hidden-md-up" data-goto-unitgroup="Acceleration">Acceleration</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-cyan g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Density">Density</a>
                    <a href="javascript:void(0)" class="h5 g-color-cyan m-3 g-hidden-md-up" data-goto-unitgroup="Density">Density</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-teal g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="Angle">Angle</a>
                    <a href="javascript:void(0)" class="h5 g-color-teal m-3 g-hidden-md-up" data-goto-unitgroup="Angle">Angle</a>
                    <a href="javascript:void(0)" class="btn btn-md u-btn-outline-brown g-mr-10 g-mb-15 g-hidden-sm-down" data-goto-unitgroup="DigitalStorage">Digital Storage</a>
                    <a href="javascript:void(0)" class="h5 g-color-brown m-3 g-hidden-md-up" data-goto-unitgroup="DigitalStorage">Digital Storage</a>
                    <%--<a href="#" class="btn btn-md u-btn-outline-bluegray g-mr-10 g-mb-15 g-hidden-sm-down">Blue Gray</a>--%>
                    <%--<a href="#" class="btn btn-md u-btn-outline-darkgray g-mr-10 g-mb-15 g-hidden-sm-down">Dark Gray</a>--%>
                    <%--<a href="#" class="btn btn-md u-btn-outline-black g-mr-10 g-mb-15 g-hidden-sm-down">Black</a>--%>
                </div>
            </div>
        </div>
    </section>

    <!-- Unit Groups -->
    <section id="unit-groups" class="g-theme-bg-gray-light-v1">
        <div class="container-fluid py-5">
            <asp:PlaceHolder ID="UnitGroupsPlaceHolder" runat="server" />
        </div>
    </section>

    <!-- Main Form -->
    <form id="MainForm" runat="server" action="/" method="post">

        <asp:ScriptManager ID="scriptManagerMain" runat="server">
            <Services>
                <asp:ServiceReference Path="~/UnitCandyService.svc" />
            </Services>
        </asp:ScriptManager>

        <asp:PlaceHolder ID="WebGroupsPlaceholder" runat="server" />

    </form>

    <!-- Info -->
    <section id="info">
        <div class="container py-3">
            <div class="row">
                <div class="col-12 col-md-10 offset-md-1 col-lg-8 offset-lg-2">
                    <div class="justify-content-center">
                        <p>UnitCandy is a utility that helps you convert units of measurement from one system to another. We hope UnitCandy is accurate, complete and easy to use.</p>
                        <p>A unit of measurement is for example “five yards” which represents a length. “Meter” is another unit of length and you can use UnitCandy to convert 5 yards into x meters.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- About -->
    <section id="about">
        <div class="container-fluid bg-dark py-5">
            <div class="row justify-content-center">

                <div class="col-6 col-sm-3 col-xl-1">
                    <img src="images/UnitcandyIcon64.png" class="img-fluid align-self-center" />
                </div>

                <div class="col-6 col-sm-3 col-xl-1">
                    <h5 class="g-color-white">UnitCandy</h5>
                    <p>Created by Dolaris.</p>
                    <p>
                        © 2020 Copyright.<br />
                        All Rights Reserved
                    </p>
                </div>

                <div class="col-6 col-sm-3 col-xl-1">
                    <h5 class="g-color-white">Contact</h5>
                    <p>
                        Dolaris<br />
                        P.O. Box 1801<br />
                        Bellevue, WA 98009<br />
                        United States
                    </p>
                    <a href="mailto:unitcandycom@gmail.com" class="xx-text-dark">unitcandycom@gmail.com</a>
                </div>

                <div class="col-6 col-sm-3 col-xl-1">
                    <h5 class="g-color-white">Privacy Policy</h5>
                    <p>Click <a href="#privacy-policy" class="xxx-text-dark" data-toggle="collapse" data-target="#privacy-policy">here</a> to show or hide our privacy policy.</p>
                </div>

            </div>

            <div class="row d-flex justify-content-center py-3 py-md-5">
                <h3>
                    <span class="mr-2 mr-md-4"><a href="https://www.facebook.com/UnitCandycom-331411840674514/" target="_blank"><i class="fa fa-facebook-square g-color-gray-light-v1 g-color-facebook--hover" title="Facebook"></i></a></span>
                    <span class="mr-2 mr-md-4"><a href="https://twitter.com/unitcandy" target="_blank"><i class="fa fa-twitter g-color-gray-light-v1 g-color-twitter--hover" title="Twitter"></i></a></span>
                    <span class="mr-2 mr-md-4"><a href="mailto:unitcandycom@gmail.com"><i class="fa fa-envelope-o g-color-gray-light-v1 g-color-yellow--hover" title="E-Mail"></i></a></span>
                </h3>
            </div>

            <div class="row d-flex justify-content-center pb-3 pb-md-5">
                <h1>
                    <a href="https://itunes.apple.com/us/app/unitcandy/id1225441184?mt=8" target="_blank" style="display: inline-block; overflow: hidden; background: url(//linkmaker.itunes.apple.com/assets/shared/badges/en-us/appstore-lrg.svg) no-repeat; width: 135px; height: 40px; background-size: contain;"></a>
                </h1>
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
    <script src="scripts/unitcandy.data.js"></script>
    <script src="scripts/unitcandy.unitelement.js"></script>
    <script src="scripts/unitcandy.ui.js"></script>
    <script src="scripts/unitcandy.js"></script>

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

    <!-- InfoLinks Ads -->
    <script type="text/javascript">
        var infolinks_pid = 3190727;
        var infolinks_wsid = 2;
    </script>
    <script type="text/javascript" src="https://resources.infolinks.com/js/infolinks_main.js"></script>

</body>

</html>
