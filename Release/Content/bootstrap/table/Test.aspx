<%@ Page Title="table" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebAppBS.Content.bootstrap.table.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="dist/bootstrap-table.css" rel="stylesheet" />
    <script src="dist/bootstrap-table.js"></script>
     <script src="dist/locale/bootstrap-table-zh-CN.js"></script>
   
    <style>
        .fixed-table-toolbar .bars{ width: 100%}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                table test
            </div>
        </div>
        <div class="panel-body">
         
            <div id="toolbar" class="well" style="margin-bottom: 0">
                <div class="form-inline">
                    <input type="text" id="txt_searchname" placeholder="产品名" class="form-control"/>
                    <input type="text" id="txt_searchprice" placeholder="价格" class="form-control"/>
                    <input type="button" id="bt_search" class="btn btn-sm btn-primary" value="搜索"/>
                    <button id="remove" class="btn btn-sm btn-danger" disabled="disabled">
                        <span class="glyphicon glyphicon-trash"></span> &nbsp;删除
                    </button>
                </div>
            </div>
            <table id="table"
               data-classes="table table-hover table-no-bordered"
               data-toolbar="#toolbar"
                data-toolbar-align="right"
               data-toggle="table"
               data-url="data/data.aspx?act=products"
               data-height="auto"
               data-side-pagination="server"
               data-pagination="true"
               data-page-list="[5, 10, 20, 50, 100, 200]"
               data-show-refresh="false"
               data-show-toggle="false"
               data-search="false"
               data-advanced-search="false"
               data-id-table="Products"
              >
            <thead>
            <tr>
                <th data-field="state" data-checkbox="true"></th>
                <th data-field="id" data-sortable="true">#</th>
                <th data-field="name">产品名</th>
                <th data-field="price" data-sortable="true">产品价格</th>
            </tr>
            </thead>
        </table>
        <script>
            var $table = $('#table'),
                $remove = $('#remove'),
                selections = [];

            $table.on('check.bs.table uncheck.bs.table ' +
                'check-all.bs.table uncheck-all.bs.table', function() {
                    $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
                    // save your data, here just save the current page
                    selections = getIdSelections();
                    // push or splice the selections if you want to save all data selections
                });

            function getIdSelections() {
                return $.map($table.bootstrapTable('getSelections'), function(row) {
                    return row.id;
                });
            }

            //html 删除
            function removeRows() {
                var ids = getIdSelections();
                $table.bootstrapTable('remove', {
                    field: 'id',
                    values: ids
                });
                $remove.prop('disabled', true);
            }
            //db 删除
            function removeData() {
                
            }

            $("#bt_search").on("click", function() {
                search();
            });
            function search() {
                var name = $.trim($("#txt_searchname").val());
                var baseUrl = "data/data.aspx?act=products";
                var searchUrl = baseUrl + "&search=" + name;
                $table.bootstrapTable('refresh', { url: searchUrl });
            }

            $remove.on("click", function() {
                bootbox.confirm("确定删除？", function(yes) {
                    if (yes) {
                        removeRows();
                        //db delete
                        removeData();
                    }
                });
            });
            //适应页面窗体调整
            $(window).on("resize", function() {
                $table.bootstrapTable('resetView');
            });
     


          function customDialog(){
                bootbox.dialog({
                    title: "That html",
                    message: '<div class="row">  ' +
                    '<div class="col-md-12"> ' +
                    '<form class="form-horizontal"> ' +
                    '<div class="form-group"> ' +
                    '<label class="col-md-4 control-label" for="name">Name</label> ' +
                    '<div class="col-md-4"> ' +
                    '<input id="name" name="name" type="text" placeholder="Your name" class="form-control input-md"> ' +
                    '<span class="help-block">Here goes your name</span> </div> ' +
                    '</div> ' +
                    '<div class="form-group"> ' +
                    '<label class="col-md-4 control-label" for="awesomeness">How awesome is this?</label> ' +
                    '<div class="col-md-4"> <div class="radio"> <label for="awesomeness-0"> ' +
                    '<input type="radio" name="awesomeness" id="awesomeness-0" value="Really awesome" checked="checked"> ' +
                    'Really awesome </label> ' +
                    '</div><div class="radio"> <label for="awesomeness-1"> ' +
                    '<input type="radio" name="awesomeness" id="awesomeness-1" value="Super awesome"> Super awesome </label> ' +
                    '</div> ' +
                    '</div> </div>' +
                    '</form> </div>  </div>',
                    buttons: {
                        success: {
                            label: "Save",
                            className: "btn-success",
                            callback: function() {
                                var name = $('#name').val();
                                var answer = $("input[name='awesomeness']:checked").val();
                                 
                            }
                        }
                    }
                });
            };

        </script>
        </div>
    </div>
</asp:Content>
