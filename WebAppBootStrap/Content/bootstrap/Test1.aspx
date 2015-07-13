<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Test1.aspx.cs" Inherits="WebAppBootStrap.Content.bootstrap.Test1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../script/bs.modal.js"></script>
    <script src="../script/bs.alert.js"></script>
    <script>
        $(function () {
            //modal
             //$.bsModal("提示", "操作成功", { backdrop: true });//{ backdrop: 'false' }
            //$.bsModalIframe("提示", "http://www.baidu.com/");
            //alert
            $.bsAlertSuccess("操作成功", "#showmsg", 3);
            setTimeout(function() { $.bsAlertSuccess("操作成功", "", 3); }, 2000);
        })
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">Panel heading</div>
        <div id="panelbody" class="panel-body">
            <div id="showmsg">
                
            </div>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit.
        </div>
    </div>
</asp:Content>
