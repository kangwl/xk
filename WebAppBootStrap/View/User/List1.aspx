<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="List1.aspx.cs" Inherits="WebAppBS.View.User.List1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/Css/pagination.css" rel="stylesheet" />
    <style type="text/css">
       
    </style>
    <script src="/Content/script/jquer.pager.js"></script>
    <script src="/Content/script/json2template.js"></script>
    <script>
       // var pager = { currentPage: 0, pageSize: 10 };

        $(function() {
            getUsers();
            $("#pager").pagination(50, { items_per_page: 5, current_page: 0, callback: pageCallBack });
        });
        function pageCallBack() {
           // alert("page")
        }
        function getUsers() {
            $.ajax({
                type: "get",
                url: "/ViewHandler/User.ashx",
                data: {},
                beforeSend: function() {
                    $("#showhere").text("加载中...");
                },
                dataType: "json",
                error: function(e) {
                    console.log(e);
                    alert("error");
                },
                success: successCall
            });
        }

        function successCall(json) {
            var total = json.total;
            var dataArr = json.rows;
            $.fillJson2Template(dataArr, $("#showhere"), $("#userinfo_template"));
        }

        $.extend({
            fillJson2Template: function(json, $show_selector, $template_selector) {
                var data = { item: {} };
                data.item = json;
                $show_selector.bindTemplate({
                    source: data,
                    template: $template_selector
                });
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="showhere">
        
    </div>
    <div id="pager" class="pagination"> 
        
    </div>
    <div id="userinfo_template" style="display: none">
        <table style="width: 100%">
            <tr>
                <th>姓名</th>
                <th>年龄</th>
                <th>注册时间</th>
            </tr>
            <tr class="{{item}}">
                <td>{{Name}}</td>
                <td>{{Age}}</td>
                <td>{{AddDateTime}}</td>
            </tr>
        </table>
    </div>
</asp:Content>
