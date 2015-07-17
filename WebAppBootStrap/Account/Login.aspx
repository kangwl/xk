<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAppBS.Account.Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录</title>
    <%: Styles.Render("~/css/main") %>
    <%: Scripts.Render("~/js/main") %>
    <style>
         html,body{background:#f1f1f1 url(data:image/gif;base64,R0lGODlhBgAGAIAAAPPz8+bm5iH5BAAAAAAALAAAAAAGAAYAAAIJRI52aroY2lsFADs=); }
         /*body{ background-color: #A9A9A9}*/
    </style>
        <style>
        html,body{background:#f1f1f1 url(data:image/gif;base64,R0lGODlhBgAGAIAAAPPz8+bm5iH5BAAAAAAALAAAAAAGAAYAAAIJRI52aroY2lsFADs=); }

        /*页脚固定 HTML START*/
         html{ position: relative;min-height: 100%;}
        #foot{ position: absolute;bottom: 0;width: 100%;margin-bottom: 0;}
        #foot_content{line-height: 50px;text-align: center}
        /*页脚固定 HTML END*/
        /*页面加载提示 START */
        #page_loading { width: 100%; height: 2px; position: fixed; left: 0; top: 0; z-index:1}
        #page_loading div { width: 1px; height: 2px; background: #337AB7;}
    /*页面加载提示 END */
    </style>
    <script>
        $(function () {
            page_loading_process(100, function () {
                $("#page_loading").fadeOut();
            });
        });

        function page_loading_process(percentNum, func) {
            $("#page_loading div").animate({ width: percentNum + "%" }, "fast", func);
        }
    </script>
  <script>
      $(function () {
         
      })

  </script>
</head>
<body>
    <div id="page_loading"><div></div></div> 
    <script type="text/javascript">
        page_loading_process(10);
    </script> 
    <form id="form1" runat="server">
    <div id="loginform" class="container">
        
    </div>
    <script type="text/javascript">
        $("#loginform").load("/Account/_Login.html");
        page_loading_process(30);
    </script> 
    </form>
</body>
</html>
