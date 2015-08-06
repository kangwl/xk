<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="go.aspx.cs" Inherits="WebAppBS.Prox.go" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Content/script/jquery-1.9.1.min.js"></script>
    <script>
        $(document).on("click", "#btn_go", function () {
            var src = $("#txt_site").val();
            $("<iframe src=" + src + " width='100%' height='100%'></iframe>").appendTo($("#content"));
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar">
        <input id="txt_site" type="text"/><input id="btn_go" type="button" value="GO"/>
    </div>
        <div id="content" style="height: 100%;min-height: 400px;width: 100%">
            
        </div>
    </form>
</body>
</html>
