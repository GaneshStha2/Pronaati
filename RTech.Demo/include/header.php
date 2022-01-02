<!DOCTYPE html>
<html>

<head>
    <title>Pro Naati Pty Ltd</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" href="images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="css/uikit.min.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
     <link rel="stylesheet" href="css/dataTables.uikit.min.css" />
    <link rel="stylesheet" href="css/uk-design.css" />
    <script src="js/jquery-3.3.1.js"></script>
    <script src="js/uikit.min.js"></script>
    <script src="js/uikit-icons.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.uikit.min.js"></script>
    <script>
        $(document).ready(function() {
    $('.uk-table-searchable').DataTable();
} );
    </script>
    <script type='text/javascript' src='//platform-api.sharethis.com/js/sharethis.js#property=5d61664c0887cc001233142c&product=inline-share-buttons' async='async'></script>
</head>

<body>
    <!-- header -->
    <header data-uk-sticky="show-on-up: true; animation: uk-animation-slide-top; sel-target:   cls-active: uk-navbar-sticky   ; bottom: #scrollup-dropdown">
        <div class="uk-position-relative ">
            <div class="uk-container ">
                <nav class="uk-navbar-container   uk-navbar-transparent" uk-navbar="dropbar: false;">
                    <div class="uk-navbar-left">
                        <a class="uk-navbar-item uk-logo" href="index.php"><img src="images/logo.png" alt="logo"></a>
                    </div>
                    <div class="uk-navbar-right ">
                        <ul class="uk-navbar-nav uk-visible@m">
                            <li> <a href="index.php"> Home </a> </li>
                            <li> <a href="about-us.php"> About Us </a> </li>
                            <li> <a href="naati-languages.php"> Naati Languages</a> </li>
                           <!--  <li> <a href="packages.php"> Packages </a> </li>
                            <li> <a href="mock-test.php"> Mock Test </a> </li> -->
                            <li> <a href="testimonials.php"> Testimonials </a> </li>
                            <li> <a href="blog.php"> Blog </a> </li>
                            <li> <a href="contact-us.php"> Contact Us </a> </li>
                        </ul>
                        <a href="login.php" class="uk-button uk-button-primary uk-button-small uk-margin-left uk-margin-right"> Login </a>
                        <a uk-navbar-toggle-icon="" uk-toggle="target: #offcanvas-push" class="uk-margin-right uk-icon uk-navbar-toggle-icon  uk-hidden@m"></a>
                    </div>
                </nav>
            </div>
        </div>
        <!-- menu -->
        <div id="offcanvas-push" uk-offcanvas="mode: push; flip:true; overlay: true">
            <div class="uk-offcanvas-bar uk-box-shadow-large  ">
                <button class="uk-offcanvas-close" type="button" uk-close></button>
                <nav class="uk-card-small uk-card-body  uk-nav  ">
                    <ul class="uk-nav-default uk-nav-parent-icon uk-list-divider uk-nav" uk-nav>
                        <li> <a href="index.php"> Home </a> </li>
                        <li> <a href="about-us.php"> About Us </a> </li>
                        <li> <a href="packages.php"> Packages </a> </li>
                        <li> <a href="packages.php"> Mock Test </a> </li>
                        <li> <a href="testimonials.php"> Testimonials </a> </li>
                        <li> <a href="blog.php"> Blog </a> </li>
                        <li> <a href="contact-us.php"> Contact Us </a> </li>
                    </ul>
                    </ul>
                </nav>
                <nav class="uk-card-body">
                    <ul class="uk-iconnav uk-flex-center">
                        <li>
                            <a href="#" title="Facebook" uk-icon="facebook"></a>
                        </li>
                        <li>
                            <a href="#" title="Twitter" uk-icon="twitter"></a>
                        </li>
                        <li>
                            <a href="#" title="YouTube" uk-icon="youtube"></a>
                        </li>
                        <li>
                            <a href="#" title="Instagram" uk-icon="instagram"></a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
        <!-- menu -->
    </header>
    <!-- header end -->

    <!-- main -->
    <main>