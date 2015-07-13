<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebAppBS.Content.bootstrap.datepicker.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker3.css" rel="stylesheet" />
    <script src="js/bootstrap-datepicker.js"></script>
    <script src="locales/bootstrap-datepicker.zh-CN.min.js"></script>
    <script>
        $(function () {
            $('#dp1').datepicker({
                format: 'yyyy-mm-dd',
                language: 'zh-CN',
                autoclose: true 
            });

            $('#txt_date').datepicker({
                format: "yyyy-mm-dd",
                weekStart: 1,
                startDate: "-2d",
                endDate: "+10d",
                clearBtn: true,
                language: "zh-CN",
                multidate: true,
                multidateSeparator: ",",
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
                todayHighlight: true
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well form-inline">
        <label>datetime:</label>
        <input type="text" class="form-control" value="" id="dp1"/>
    </div>
    <div class="well">
        <p>测试1</p>
        <input type="text" id="txt_date" class="form-control"/>
    </div>


</asp:Content>
