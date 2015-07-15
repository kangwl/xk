<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="BSTable_Simple.aspx.cs" Inherits="WebAppBS.Control.table.BSTable_Simple1" %>

<%@ Register Src="~/Control/table/BSTable_Simple.ascx" TagPrefix="uc1" TagName="BSTable_Simple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:BSTable_Simple runat="server" ID="BSTable_Simple"
        BSTableID="simple_table"
        BSTableFields="id:#,name:名称,price:价格" 
        BSTableHeight="500"
        BSUrl="/Content/bootstrap/table/data/data.aspx?act=testsimple"/>
</asp:Content>
