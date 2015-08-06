<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="go.aspx.cs" Inherits="WebAppBS.Prox.go" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Content/script/jquery-1.9.1.min.js"></script>
    <script>
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:TextBox ID="txtURL" runat="server"></asp:TextBox>
    <asp:Button ID="btnGetHTML" runat="server" Text="获取页面HTML" OnClick="btnGetHTML_Click" />
    <br />
    页面HTML代码:<br />
    <asp:TextBox ID="txtPageHTML" runat="server" Height="186px" TextMode="MultiLine"
        Width="263px"></asp:TextBox>
        
   <div>
       <asp:Literal runat="server" ID="lit_html"></asp:Literal>
   </div>
    </form> 
</body>
</html>
