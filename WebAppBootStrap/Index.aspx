<%@ Page Title="首页" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebAppBootStrap.Index" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%:Styles.Render("~/bs_select_css") %>
    <%:Scripts.Render("~/bs_select_js") %>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">bootstrap select</div>
        <div class="panel-body">
            <script>
                $(document).on("change", "#select_name", function(e) {
                    //alert($(e.target).val());
                });
                $(function () {
                    $("#select_name").val(3);
                });
                //赋值
                //$("#select_name").selectpicker("val", 2);
            </script>
            <select id="select_name" class="selectpicker" title="请选择" data-style="btn-primary" data-live-search="true">
                <option value="1">name1</option>
                <option value="2">name2</option>
                <option value="3">name3</option>
            </select>
        </div>
    </div>
</asp:Content>
