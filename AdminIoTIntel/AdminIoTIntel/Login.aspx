<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="adminiotintel.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="shortcut icon" href="../Images/favicon.ico" type="image/x-icon" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="AAdministrator's IoT Intel" name="keywords" />
    <meta content="Administrator's IoT Intel" name="description" />
    <title>Administrator's IoT Intel</title>
    <meta property="og:image" itemprop="thumbnailFacebook" content="http://adminmotolok.way.vn/images/logo1.png" />
    <link href="../css/login.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-1.8.3.js" type="text/javascript"></script>
    <link href="../js/stickytooltip.css" rel="stylesheet" type="text/css" />
    <script src="../js/stickytooltip.js" type="text/javascript"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            $(".username").focus(function () {
                var text = $(".username").val();
                if (text == "Username") {
                    $(".username").attr("value", "");
                    $(".username").focus();
                }
            })
        });
        $(".username").focusout(function () {
            var text = $(".username").val();
            if (text == "") {
                $("username").attr("value", "Username");
            }
        });
        $(".pass").focus(function () {
            var text = $(".pass").val();
            if (text = "test") {
                $(".pass").attr("value", "");
                $(".pass").focus();
            }
        });
        $(".pass").focusout(function () {
            var text = $(".pass").val();
            if (text == "") {
                $(".pass").attr("value", "test");
            }
        });
        $(".username").keyup(function (event) {
            if (event.ke == 13) {
                $(".button")[0].click();
            }
        });
        $(".pass").keyup(function (event) {
            if (event.keyCode == 13) {
                $(".button")[0].click();

            }
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class ="mainPage">
            <div class ="leftimg">
                 <img src="images/computer.png" alt="IT Services" />
            </div>
            <div class ="loginright">
                <img src ="../images/logo1.png" alt ="Intel" width ="40%" />
                <div class ="subtitle">Service Management</div>
                <a href ="#" data-tooltip ="sticky1" class="tooltipnow">
                    <img src="../images/help.jpg" alt ="" /><span>Support to login</span>
                </a>
                <div id="mystickytooltip" class="stickytooltip">
                    <div id='sticky1' class='atip'>
                         <label>
                            Support to login.
                        </label>
                        <span>Example your's email: </span>
                        <p style="text-align: center;">
                            <b>lappv@way.vn</b>
                        </p>
                        <p>
                            <b style="width: 50px;">Username:</b> lappv</p>
                        <p>
                            <b style="width: 50px;">PassWord:</b> default is 123456</p>
                        <p>
                            (Please change password after login)</p>
                    </div>
                </div>
                <div class ="logininput">
                      <asp:TextBox ID="txtUserName" runat="server" Text="Username" CssClass="username"></asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="pass"></asp:TextBox>
                   <asp:LinkButton ID="lbtLogin" runat="server" CssClass="button" OnClick="lbtLogin_Click" BackColor="Blue"
                    ToolTip="Login system">Login</asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
