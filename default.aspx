<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs"
    Inherits="AdminTool._default" %>

<html>
<head>
    <head>
        <title>Infobyd | Complete Zoho Solution | CRM Solution Provider </title>
        <link href="images/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <!-- Google Fonts -->
        <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed:300italic,400italic,700italic,400,300,700'
            rel='stylesheet' type='text/css'>
        <link href='http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,300,600,700'
            rel='stylesheet' type='text/css'>
        <!-- Font awesome -->
        <link rel="stylesheet" href="css-js/vendor/font-awesome/css/font-awesome.min.css">
        <!-- mmenu -->
        <link type="text/css" rel="stylesheet" href="css-js/vendor/mmenu/css/jquery.mmenu.css" />
        <!-- Bootstrap css -->
        <link rel="stylesheet" href="css-js/vendor/bootstrap/css/bootstrap.css">
        <!-- Animate css -->
        <link rel="stylesheet" href="css-js/css/animate.css">
        <link rel="stylesheet" href="css-js/css/style.css">
        <link rel="stylesheet" type="text/css" href="css-js/css/slide_login_register.css">
    </head>
    <body>
        <div class="main">
            <header class="header-part">
                <div id="home" class="wrapper">
                    <!-- Fixed navbar -->
                    <div class="navi navbar-default" role="navigation">
                        <div class="container">
                            <div class="navbar-header page-scroll">
                                <a href="#menu">
                                    <button type="button" data-effect="st-effect-1" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                                        <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span>
                                        <span class="icon-bar"></span>
                                        <span class="icon-bar"></span>
                                    </button>
                                </a>
                                <a class="navbar-brand" href="http://infobyd.com/">
                                    <img src="images/logo-infobyd.png" alt="TechGut" style="position: relative; bottom: 20px;"></a>
                            </div>
                            <div id="navbar" class="navbar-collapse collapse pull-right hidden-xs">
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="http://infobyd.com/">Home</a></li>
                                    <li><a href="http://infobyd.com/#about">about us</a></li>
                                    <li class="dropdown">
                                        <a class="page-scroll drop dropdown-toggle" href="#">Product</a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a href="http://mail2lead.infobyd.com/">IB-Mail2Lead</a></li>
                                            <li><a href="http://mail2lead.infobyd.com/">IB-SMS</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="http://infobyd.com/zoho-pricing.php?inr">Plans</a></li>
                                    <li><a class="page-scroll" href="http://infobyd.com/#services">services</a></li>
                                    <li><a href="http://infobyd.com/team.php">team</a></li>
                                    <li><a href="http://infobyd.com/zoho-knowledge-center/">Knowledge centre</a></li>
                                    <li style="border-right: 1px solid #e4e4e4;"><a class="page-scroll" href="http://infobyd.com/#contact">Contact</a></li>
                                    <li><a href="http://infobyd.com/faq.php">FAQ</a></li>
                                </ul>
                            </div>
                            <!--/.nav-collapse -->
                        </div>
                    </div>
                    <!-- End of Nav -->
                </div>
            </header>
        </div>
        <!-- End of class="main" -->
        <div class="container" style="border-bottom: 1px solid #CCCCCC;">
            <div class="wow bounceInLeft" align="center">
                <div class="col-sm-6" id="video">
                    <!--responsive video tag-->
                    <h1>watch product video</h1>
                    <hr>
                    <div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/D9LMW6340pU"
                            frameborder="0" allowfullscreen></iframe>
                    </div>
                </div>
                <!--class="col-sm-12"-->
            </div>
            <!--class="col-sm-6"-->
            <div class="wow bounceInRight" align="center" id="form">
                <div class="col-sm-6">
                    <div class="row" id="right-div-boder">
                        <div class="col-sm-5" id="right-div-head-login">
                            <h2>
                                <span><a href="#" class="slidelink" id="showlogin">Login</a></span></h2>
                        </div>
                        <div class="col-sm-7" id="right-div-head-request">
                            <h2>
                                <span><a href="#" class="leftsidelink" id="showregister">Request for demo</a></span></h2>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-sm-12" align="center">
                            <div id="w">
                                <div id="page">

                                    <div id="content-login">
                                        <div class="content">
                                            <asp:Label runat="server" ID="ErrorMessage" Text="Invalid username or password" Font-Size="15" Visible="false" Style="margin-left: -25%;" ForeColor="Red"></asp:Label>

                                            <form id="login" runat="server" name="login" action="default.aspx" method="post"
                                                siq_id="autopick_6887">
                                                <asp:TextBox runat="server" ID="UserName" class="txtfield" TabIndex="1" autocomplete="off"
                                                    placeholder=" Username" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                                    CssClass="text-danger" ErrorMessage="required." Font-Size="10" />
                                                <asp:TextBox runat="server" ID="Password" TextMode="Password" class="txtfield" TabIndex="1"
                                                    autocomplete="off" placeholder="Password" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                                                    CssClass="text-danger" ErrorMessage="required." Font-Size="10" />
                                                <br />
                                                <p align="right" style="margin:10px 0;">
                                                    <asp:Button runat="server" ID="forgotPassword" OnClick="LoginButton_Click" UseSubmitBehavior="false"
                                                        Text="forgot Password" class="btn" TabIndex="1" />
                                               
                                                    <asp:Button runat="server" ID="LoginButton" OnClick="LoginButton_Click" UseSubmitBehavior="false"
                                                        Text="Login" class="btn" TabIndex="1" />
                                                </p>

                                            </form>
                                        </div>
                                    </div>
                                    <!-- /end #content-login -->
                                    <div id="content-register">
                                        <div class="content">
                                            <iframe src="http://infobyd.com/form.php" style="height: 500px; width: 100%;"></iframe>
                                        </div>
                                    </div>
                                    <!-- /end #content-register -->
                                </div>
                                <!-- /end #page -->
                            </div>
                            <!-- /end #w -->
                        </div>
                    </div>
                </div>
            </div>
            <!--class="col-sm-6"-->
        </div>
        </div>
        <!-- End of class="main" -->
        <!--id="box"-->
        <section class="wow zoomIn testimmonial section-padding animated" style="visibility: visible; animation-name: zoomIn;">
            <div class="container">
                <div class="row">
                    <div class="wow zoomIn col-sm-6 text-center p-padding animated animated" style="visibility: visible; animation-name: zoomIn; border-right: 1px solid rgb(204, 204, 204);">
                        <h1 class="section-title">Client Says</h1>
                        <img src="images/red-box.png"></br>
                        <br>
                        <br>
                        <marquee direction="up" height="350px" scrolldelay="170" onmouseover="this.stop();" onmouseout="this.start();" style="height: 350px;">
						<div>
							<img src="Images/4pillars-min.jpg" align="left" style="margin-right:20px;" width="40%">
							<span>“Infobyd is a world class expert in the Zoho Product (CRM/Campaign/Book/Creater). They helps us customize the solution and Migration. Always getting quick response whenever we need them urgently. Highly recommend.. excellent work. <br>Thank you”</span><br>
						</div><br><br>
						<div>
							<img src="Images/Globalcrossroad_logo-min.png" align="left" style="margin-right:20px;" width="40%">
							“Infobyd team worked with me when I had just finished having a negative experience with another elancer and won me back. They have been incredibly responsive positive and helpful. I am giving them plenty of further work as they have the ability to quarterback and get done most things all with a smile on their face. I'd strongly recommend them, will be working with them again and suggest you do too!”<br>
						</div><br><br>
						<div>
							<img src="Images/ifre_logo-min.png" align="left" style="margin-right:20px;" width="40%">
							“I have enjoyed working with Infobyd team and will hire them again. They was extremely patient with me as I didnt understand Zoho and was more than happy to explain in detail (sometimes more than once LOL). They was efficient and got the job done! Thank you Infobyd Team.”<br>
						</div><br><br>
						<div>
							<img src="Images/Optimum_Insurance_Logo-min.png" align="left" style="margin-right:20px;" width="40%">
							“Very much appreciated working with infobyd, clear communication and quality of work.”<br>
							<!--<span class="pull-right"> <q>ZoHo CRM implementer</q></span>-->
						</div>
					</marquee>
                    </div>
                    <div class="wow zoomIn col-sm-6 text-center p-padding animated animated" style="visibility: visible; animation-name: zoomIn;">
                        <h1 class="section-title">Client Video</h1>
                        <img src="images/red-box.png"></br>
                        <br>
                        <br>
                        <div class="embed-responsive embed-responsive-16by9">
                            <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/Kz9rTOu7gKs"
                                frameborder="0" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- testimonial -->
        <!-- footer -->
        <footer>
            <div class="footer-wrapper section-padding">
                <div class="container">
                    <div class="row">
                        <div class="wow zoomIn col-xs-12 col-sm-6 col-md-4">
                            <a href="http://infobyd.com/">
                                <h1 style="color: #FFFFFF;">Infobyd</h1>
                                <!--<img src="img/footer-logo.png" alt="TechGut">-->
                            </a>
                            <p class="footer-content">We make quick and informed decisions in a complex, fast-paced, competitive business environment and adopt innovations in technologies to create more value for customers which maximizing customer opportunity across the relationship lifecycle with offerings needed to gain repeat business and stability.</p>
                        </div>
                        <!-- /.col-xs-12 .col-sm-3 .col-md-3 -->

                        <div class="wow zoomIn col-xs-12 col-sm-6 col-md-4">
                            <p class="footer-heading">find us</p>
                            <ul class="footercontact">
                                <li><i class="flaticon-mainpage"></i><span>address:</span> 164, Chhoti Khazrani, MIG, Indore - 452010</li>
                                <li><i class="flaticon-phone16"></i><span>phone:</span><a href="tel:0731 400 3293">0731 400 3293</a></li>
                                <li><i class="flaticon-email21"></i><span>e-mail:</span><a href="mailto:team@infobyd.com"> team@infobyd.com</a></li>
                                <li><i class="flaticon-world91"></i><span>web:</span><a href="http://infobyd.com/" target="_blank"> www.infobyd.com</a></li>
                            </ul>
                            <i class="flaticon-home78"></i>
                        </div>
                        <!-- /.col-xs-12 .col-sm-3 .col-md-3 -->

                        <div class="wow zoomIn col-xs-12 col-sm-3 col-md-4" align="center">
                            <p class="footer-heading">ZOHO Partner</p>
                            <img src="./images/PS1PSSHIN9NT-180x180.PNG" height="140">
                            <img src="./images/Authorized-partner.png">
                        </div>
                        <!-- /.col-xs-12 .col-sm-3 .col-md-3 -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.container -->
            </div>
            <div class="footer-bottom">
                <div class="container">
                    <div class="row">
                        <div class="wow zoomIn col-xs-12">
                            <p>© 2015 All rights reserved. <a href="http://infobyd.com/" target="_blank">infobyd.com</a></p>
                        </div>
                        <!-- /.col-xs-12 -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.container -->
            </div>
            <!-- /.creditwrapper -->
        </footer>
        <!-- /Footer -->
        <!-- MENU -->
        <nav id="menu">
            <ul>
                <li><a href="http://infobyd.com/">home</a></li>
                <li><a href="http://infobyd.com/#about">about us</a></li>
                <li>
                    <a href="">Product</a>
                    <ul>
                        <li><a href="http://mail2lead.infobyd.com/">IB-Mail2Lead</a></li>
                        <li><a href="http://mail2lead.infobyd.com/">IB-SMS</a></li>
                    </ul>
                </li>
                <li><a href="http://infobyd.com/zoho-pricing.php?inr">plans</a></li>
                <li><a href="http://infobyd.com/#services">services</a></li>
                <li><a href="http://infobyd.com/team.php">team</a></li>
                <li><a href="http://infobyd.com/blog/">knowledge centre</a></li>
                <li><a href="http://infobyd.com/faq.php">faq</a></li>
                <li><a href="http://infobyd.com/#contact">contact</a></li>
            </ul>
        </nav>
        <!-- /#menu -->
        </div>
        <!-- /.main -->
        <!-- jQuery JS -->
        <script data-cfasync="false" src="css-js/js/jquery-1.11.1.js"></script>
        <!-- Modernizr JS -->
        <script data-cfasync="false" src="css-js/js/modernizr-2.6.2.min.js"></script>
        <!-- REVOLUTION Slider  -->
        <script data-cfasync="false" type="text/javascript" src="css-js/vendor/rs-plugin/js/jquery.themepunch.plugins.min.js"></script>
        <script data-cfasync="false" type="text/javascript" src="css-js/vendor/rs-plugin/js/jquery.themepunch.revolution.js"></script>
        <!-- Shuffle JS -->
        <script data-cfasync="false" src="css-js/js/jquery.shuffle.min.js"></script>
        <!-- mmenu -->
        <script data-cfasync="false" type="text/javascript" src="css-js/vendor/mmenu/js/jquery.mmenu.min.js"></script>
        <!-- Owl Carosel -->
        <script data-cfasync="false" src="css-js/vendor/owl/js/owl.carousel.min.js"></script>
        <script data-cfasync="false" src="css-js/js/wow.min.js"></script>
        <!-- waypoints JS-->
        <script data-cfasync="false" src="css-js/js/waypoints.min.js"></script>
        <!-- Counterup JS -->
        <script data-cfasync="false" src="css-js/js/jquery.counterup.min.js"></script>
        <!-- Custom Script JS -->
        <script data-cfasync="false" src="css-js/js/script.js"></script>
        <!-- Email JS -->
        <script data-cfasync="false" src="css-js/js/email.js"></script>
        <script type="text/javascript" src="css-js/js/formslider.js"></script>
    </body>
</html>
