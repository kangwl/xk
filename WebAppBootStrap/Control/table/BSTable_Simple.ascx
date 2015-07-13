<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BSTable_Simple.ascx.cs" Inherits="WebAppBootStrap.Control.table.BSTable_Simple" %>
<%:System.Web.Optimization.Styles.Render("~/bs_table_css") %>
<%:System.Web.Optimization.Scripts.Render("~/bs_table_js") %>
111
<asp:Literal runat="server" ID="lit_bs_table"></asp:Literal>
<script>
   var $table = $('#<%:BSTableID%>');
    $(function () {
        $table.bootstrapTable({
            url: "<%:BSUrl%>",
            height: "<%:BSTableHeight%>",
            classes: "<%:BSTableClass%>"
        });
    });
    //适应页面窗体调整
    $(window).on("resize", function () {
        $table.bootstrapTable('resetView');
    });
</script>