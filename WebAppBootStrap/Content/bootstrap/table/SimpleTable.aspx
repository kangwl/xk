<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="SimpleTable.aspx.cs" Inherits="WebAppBootStrap.Content.bootstrap.table.SimpleTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="dist/bootstrap-table.css" rel="stylesheet" />
    <script src="dist/bootstrap-table.js"></script>
    <script src="dist/locale/bootstrap-table-zh-CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="table"  data-show-columns="true">
        <thead>
        <tr>
            <th data-field="id">ID</th>
            <th data-field="name">Item Name</th>
            <th data-field="price">Item Price</th>
            <th data-field="operate" data-events="operateEvents" data-formatter="operateFormatter">操作</th>
        </tr>
        </thead>
    </table>
    <script>
        var $table = $('#table');

        $(function() {
            $table.bootstrapTable({
                url: "data/data.aspx?act=products",
                pagination: true,
                sidePagination: "server",
                height: "auto",
                classes: "table table-hover table-no-bordered"
            });
        });

        function operateFormatter(value, row, index) {
            return [
                '<a class="like" href="javascript:void(0)" title="Like">',
                '<i class="glyphicon glyphicon-heart"></i>',
                '</a>  ',
                '<a class="remove" href="javascript:void(0)" title="Remove">',
                '<i class="glyphicon glyphicon-remove"></i>',
                '</a>'
            ].join('');
        }

        window.operateEvents = {
            'click .like': function (e, value, row, index) {
                alert('You click like action, row: ' + JSON.stringify(row));
            },
            'click .remove': function (e, value, row, index) {
                $table.bootstrapTable('remove', {
                    field: 'id',
                    values: [row.id]
                });
            }
        };

        /* METHODS BELOW
        1.Return all selected rows, when no record selected, am empty array will return: 
            todo:$table.bootstrapTable('getSelections');
        2.Get the loaded data of table: 
            todo:$table.bootstrapTable('getData');
        3.Load the data to table, the old rows will be removed: 
            todo:$table.bootstrapTable('load', data);
        4.Append the data to table: 
            todo:$table.bootstrapTable('append', data);
        5.Prepend the data to table: 
            todo:$table.bootstrapTable('prepend', data);
        6.Remove data from table, the params contains two properties:
            field: the field name of remove rows.
            values: the array of values for rows which should be removed.
            todo:$table.bootstrapTable('remove', {field: 'id', values: ids});
        7.Insert a new row, the param contains following properties:
            index: the row index to insert into.
            row: the row data.
            todo:$table.bootstrapTable('insertRow', {index: 1, row: row});

        8.Update the specified row, the param contains following properties: 
            index: the row index to be updated. 
            row: the row data.
            todo:$table.bootstrapTable('updateRow', {index: 1, row: row});
        9.Show/Hide the specified row.
            todo:$table.bootstrapTable('showRow', {index:1});
            todo:$table.bootstrapTable('hideRow', {index:1});
        10.Merge some cells to one cell, the options contains following properties:
            index: the row index.
            field: the field name.
            rowspan: the rowspan count to be merged.
            colspan: the colspan count to be merged.
            todo:$table.bootstrapTable('mergeCells', {index: 1, field: 'name', colspan: 2, rowspan: 3});
        11.Check/Uncheck all current page rows.
            todo:$table.bootstrapTable('checkAll');
            todo:$table.bootstrapTable('uncheckAll');
        12.Check/Uncheck a row, the row index start with 0.
            todo:$table.bootstrapTable('check', 1);
            todo:$table.bootstrapTable('uncheck', 1);
        13.Check/Uncheck rows by array of values.
            todo:$table.bootstrapTable('checkBy', {field:'id', values:[1, 2, 3]});
            todo:$table.bootstrapTable('uncheckBy', {field:'id', values:[1, 2, 3]});
        14.Refresh the remote server data, you can set {silent: true} to refresh the data silently, and set {url: newUrl} to change the url. To supply query params specific to this request, set {query: {foo: 'bar'}}.
            todo:$table.bootstrapTable('refresh');
        15.Reset the bootstrap table view, for example reset the table height: 
            todo:$table.bootstrapTable('resetView');
        16.Destroy the bootstrap table: 
            todo:$table.bootstrapTable('destroy');
        17.Show/Hide the specified column.
            todo:$table.bootstrapTable('showColumn', 'name');
            todo:$table.bootstrapTable('hideColumn', 'name');
        18.Scroll to the number value position, set 'bottom' means scroll to the bottom.
            todo:$table.bootstrapTable('scrollTo', 0);
            todo:$table.bootstrapTable('scrollTo', 'bottom');
        19.Go to the a specified/previous/next page.
            todo:$table.bootstrapTable('selectPage', 1);
            todo:$table.bootstrapTable('prevPage');
            todo:$table.bootstrapTable('nextPage');
        20.Toggle the pagination option: 
            todo:$table.bootstrapTable('togglePagination');
        21.Toggle the card/table view: 
            todo:$table.bootstrapTable('toggleView');
        */

        //$(function () {
        //    var data = [
        //        {
        //            "id": 0,
        //            "name": "Item 0",
        //            "price": "$0"
        //        },
        //        {
        //            "id": 1,
        //            "name": "Item 1",
        //            "price": "$1"
        //        },
        //        {
        //            "id": 2,
        //            "name": "Item 2",
        //            "price": "$2"
        //        },
        //        {
        //            "id": 3,
        //            "name": "Item 3",
        //            "price": "$3"
        //        },
        //        {
        //            "id": 4,
        //            "name": "Item 4",
        //            "price": "$4"
        //        },
        //        {
        //            "id": 5,
        //            "name": "Item 5",
        //            "price": "$5"
        //        }
        //    ];
        //    $table.bootstrapTable({ data: data });
        //});
</script>
</asp:Content>
