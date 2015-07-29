<%@ Page Title="用户列表" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebAppBS.View.User.List" %>

<%@ Register Src="~/Control/table/BSTable_Page.ascx" TagPrefix="uc1" TagName="BSTable_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
 
        function operateFormatter(value, row, index) {
            return [
                '<a class="detail" href="javascript:void(0)" title="详细">',
                '<span class="glyphicon glyphicon glyphicon-list-alt"></span>',
                '</a>&nbsp;&nbsp;',
                '<a class="edit" href="javascript:void(0)" title="修改">',
                '<i class="glyphicon glyphicon-pencil"></i>',
                '</a>&nbsp;&nbsp;',
                '<a class="remove text-danger" href="javascript:void(0)" title="删除">',
                '<i class="glyphicon glyphicon-remove"></i>',
                '</a>'
            ].join('');
        }

        window.operateEvents = {
            'click .edit': function (e, value, row, index) {
                alert('You click like action, row: ' + JSON.stringify(row));
            },
            'click .remove': function (e, value, row, index) {
                $table.bootstrapTable('remove', {
                    field: 'ID',
                    values: [row.ID]
                });
            },
            'click .detail': function (e, value, row, index) {
                alert(index)
            }
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default" style="min-height: 300px">
        <div class="panel-heading">
            <div class="panel-title">
                用户管理
            </div>
        </div>
        <div class="panel-body">
            <uc1:BSTable_Page runat="server" ID="BSTable_Page" 
                BSTableID="user_table"
                BSTableFields="ID:#,Name:姓名,Age:年龄,Sex:性别,MobilePhone:手机"
                BSUrl="/ViewHandler/User.ashx#list"
                HasCheck="True"
            />
        </div>
    </div>
</asp:Content>
