<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BSTable_Page.ascx.cs" Inherits="WebAppBS.Control.table.BSTable_Page1" %>

<%:System.Web.Optimization.Styles.Render("~/bs_table_css") %>
<%:System.Web.Optimization.Scripts.Render("~/bs_table_js") %>

<asp:Literal runat="server" ID="lit_bs_table"></asp:Literal>
<script>
    var $table = $('#<%:BSTableID%>');
    $(function () {
        $table.bootstrapTable({
            url: "<%:BSUrl%>",
            pagination: true,
            sidePagination: "server",
            pageNumber:parseInt('<%:PageNumber%>',10),
            pageSize: parseInt('<%:PageSize%>',10),
            pageList:[<%=PageList%>],
            height: "<%:BSTableHeight%>",
            classes: "<%:BSTableClass%>",
            onLoadSuccess: function (data) {
                //console.info(data)
                return false;
            },
            onLoadError: function (status) {
                return false;
            }
        });
    });
    //适应页面窗体调整
    $(window).on("resize", function () {
        $table.bootstrapTable('resetView');
    });
</script>