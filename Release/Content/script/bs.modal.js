$.extend({
    /**
        title:标题
        content：显示的内容
        opt：附加
        example:
        $.bsModalIframe("test", "edit.html",  { backdrop: 'static' } );
    */
    bsModal: function (title, content, opt) {

        var bsModalOpt = { id: "my-bs-modal", url: "" };

        $.extend(bsModalOpt, opt);

        var createModal = function () {
            var _$modal = $("<div></div>").attr("id", bsModalOpt.id).attr("class", "modal fade").attr("tabindex", "-1").attr("role", "dialog")
                .attr("aria-labelledby", "myModalLabel").attr("aria-hidden", "true");
            return _$modal;
        };
        
        //modal header
        var createHeader = function (_title) {
            var $header = $("<div></div>").attr("class", "modal-header");
            $header.append($("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>"));
            $header.append($("<h4 class=\"modal-title\" id=\"myModalLabel\"></h4>").text(_title));
            return $header;
        };
        //modal body
        var createBody = function (_content) {
            var $body = $("<div></div>").attr("class", "modal-body").html(_content);
            return $body;
        }

        var $modal = createModal();
        var $modalDialog = $("<div class=\"modal-dialog\"></div>");
        var $modalContent = $("<div class=\"modal-content\"></div>");

        $modalContent.append(createHeader(title)).append(createBody(content));
        $modalDialog.append($modalContent);
        $modal.append($modalDialog);
        //backdrop: 'static' 点击空白区域不会消失
        var modalOpt = { remote: "" }
        if (opt) {
            $.extend(modalOpt, opt);
        }
        if (bsModalOpt.url !== "") {
            modalOpt.remote = bsModalOpt.url;
        }
        $modal.modal(modalOpt);
        $modal.on('hidden.bs.modal', this.bsModalAfterHideFunc);
        $modal.on('shown.bs.modal', this.bsModalShowFunc);
        return $modal;
    },
    bsModalIframe: function (title, url, width, height, opt) {
        if (!width) {
            width = "700px";
        }
        if (!height) {
            height = "600px";
        }
        var $modal = this.bsModal(title, "", opt);
        var _width = $.trim(width.toString().toLowerCase().replace("px", ""));
        var _height = $.trim(height.toString().toLowerCase().replace("px", ""));
        var mycss = { "width": _width + "px", "height": _height + "px" }
        $modal.find(".modal-dialog").css(mycss)
            .find('.modal-content').css({ height: '100%', width: '100%' })
            .find('.modal-body').css({ height: '85%' });
        var $iframe = $("<iframe></iframe>").attr({ "frameborder": "0", "scrolling":"auto"}).css({ "width": "100%", "height": "100%" });
        $iframe.attr("src", url);
        $modal.find(".modal-body").append($iframe);
    },
    bsModalHide: function ($modal) {
        $modal.modal('hide');
    },
    bsModalShow: function ($modal) {
        $modal.modal();
    },
    bsModalAfterHideFunc: function () {

    },
    bsModalShowFunc:function() {
        
    }
});