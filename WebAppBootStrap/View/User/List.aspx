<%@ Page Title="用户列表" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebAppBootStrap.View.User.List" %>

<%@ Register Src="~/Control/table/BSTable_Page.ascx" TagPrefix="uc1" TagName="BSTable_Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:BSTable_Page runat="server" ID="BSTable_Page" 
         BSTableID="user_table"
         BSTableFields="ID:#,Name:姓名,Age:年龄,Sex:性别,MobilePhone:手机"
         BSUrl="/ViewHandler/User.ashx"
         HasCheck="True"
         
     />
</asp:Content>
