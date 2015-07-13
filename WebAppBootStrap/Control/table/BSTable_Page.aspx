<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="BSTable_Page.aspx.cs" Inherits="WebAppBootStrap.Control.table.BSTable_Page" %>

<%@ Register Src="~/Control/table/BSTable_Page.ascx" TagPrefix="uc1" TagName="BSTable_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">数据</div>
        <div class="panel-body">
            <div class="well form-inline" style="margin-bottom: 10px">
                <input type="text" id="txt_name" placeholder="名称" class="form-control input-sm"/>
                <input type="text" id="txt_price" placeholder="价格" class="form-control input-sm"/>
                <input type="button" id="btn_search" value="搜索" class="btn btn-sm btn-primary"/>
                <button id="btn_delete" class="btn btn-sm btn-danger pull-right"><span class="glyphicon glyphicon-remove"></span> &nbsp;删除</button>
            </div>
            
            <uc1:BSTable_Page runat="server" id="BSTable_Page_1"
                BSUrl="/Content/bootstrap/table/data/data.aspx?act=products"
                BSTableID="products"
                BSTableFields="id:#,name:名称,price:价格"
                HasCheck="True"
                SortFields="id,price"/>
        </div>
    
    </div>
    <script>
        var $table = $table;

        $("#btn_search").on("click", function() {
            var name = $.trim($("#txt_name").val());
            var price = $.trim($("#txt_price").val());
            var baseUrl = "<%:BSTable_Page_1.BSUrl%>";
            var searchUrl = baseUrl + "&search=" + name + "&price=" + price;
            $table.bootstrapTable('refresh', { url: searchUrl });
        });

        $("#btn_delete").on("click", function () {
            var selArr = $table.bootstrapTable('getSelections');
            if (selArr.length < 1) {
                bootbox.alert("请选择要删除项");
                return false;
            }
            if (bootbox.confirm("确定删除？", function(yes) {
                if (yes) {
                    var ids = [];
                    $(selArr).each(function(i, n) {
                        ids.push(n.id);
                    });
                    $table.bootstrapTable('remove', { field: 'id', values: ids });
                }
            }));
            // $table.bootstrapTable('refresh');
        });
    </script>
</asp:Content>
