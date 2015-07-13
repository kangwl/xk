<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebAppBootStrap.Content.bootstrap.tagsinput.examples.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../dist/bootstrap-tagsinput.css" rel="stylesheet" />
    <script src="../dist/bootstrap-tagsinput.js"></script>
    <script>
        $(function() {
          
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well">
        <input type="text" value="Amsterdam,Washington,Sydney,Beijing,Cairo" data-role="tagsinput" />
    </div>
    <div class="well">
        <select id="select_tag" multiple="multiple" data-role="tagsinput">
          <option value="Amsterdam">Amsterdam</option>
          <option value="Washington">Washington</option>
          <option value="Sydney">Sydney</option>
          <option value="Beijing">Beijing</option>
          <option value="Cairo">Cairo</option>
        </select>
    </div>
    <div class="well">
        <input id="inputtag"/>
    </div>
</asp:Content>
