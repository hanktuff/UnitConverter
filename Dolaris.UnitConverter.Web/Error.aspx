<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Dolaris.UnitConverter.Web.GeneralError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <!-- Your Basic Site Informations -->
    <title>UnitCandy</title>
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
    <!--<link rel="stylesheet" href="css/colors/green.css">-->
    <!--<link rel="stylesheet" href="css/colors/orange.css">-->
    <!--<link rel="stylesheet" href="css/colors/pink.css">-->
    <!--<link rel="stylesheet" href="css/colors/purple.css">-->
    <!--<link rel="stylesheet" href="css/colors/yellow.css">-->

    <!--[if lt IE 9]>
    	<script src="js/html5.js"></script>
        <script src="js/respond.min.js"></script>
	<![endif]-->

    <!--[if lt IE 8]>
    	<link rel="stylesheet" href="css/ie-older.css">
    <![endif]-->

    <%--<noscript><link rel="stylesheet" href="css/no-js.css" /></noscript>--%>
    <link rel="stylesheet" href="css/no-js.css" />

    <!-- Favicons -->
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link rel="apple-touch-icon" href="images/apple-touch-icon.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="images/apple-touch-icon-72x72.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="images/apple-touch-icon-114x114.png" />
</head>


<body style="background-color: lightgray">

    <div class="container">
        <div class="row">

            <form id="form1" runat="server" class="form-horizontal">
                <br />
                <br />
                <br />
                <img src="images/content/icon/skull.png" />
                <br />
                <br />
                <h1>Oops!</h1>
                <h1>Something went wrong.</h1>
                <h1>So sorry.</h1>
                <br />
                <br />
                <a href="Default.aspx" class="btn-custom btn-more-link animation animated animation-fade-in-right" data-animation="animation-fade-in-right" data-delay="600">Go back to Unitcandy.com
                </a>
            </form>

        </div>
    </div>

</body>
</html>
