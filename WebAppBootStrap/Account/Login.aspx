<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAppBS.Account.Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录11</title>
    <%: Styles.Render("~/css/main") %>
    <%: Scripts.Render("~/js/main") %>
    <style>
        body{ background-color: #A9A9A9}
    </style>
  <script>
      $(function() {
          $("#loginform").load("/Account/_Login.html");
      })
    
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="loginform" class="container">
        
    </div>
        
    </form>
</body>
</html>
