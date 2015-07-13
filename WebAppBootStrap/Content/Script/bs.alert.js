/*给予bootstrap的消息提示条*/
$.extend({
    bsAlertType: {success: "success", info: "info", warning: "warning", danger: "danger" },
    _bsAlertBase: function (alertType, text, showPosition, seconds, jumpHref) {
        var $alertdiv = $("<div></div>").addClass("alert alert-dismissable").html(text);
        var $closeBtn = $("<button>&times;</button>")
            .attr({ "type": "button", "class": "close", "data-dismiss": "alert", "aria-hidden": "true" });
        $alertdiv.addClass("alert-" + $.trim(alertType));
        $alertdiv.css("text-align", "center");
        $alertdiv.append($closeBtn);

        if ($.trim(showPosition) == "" || (!showPosition)) {
            $("body").prepend($alertdiv);
        } else {
            $(showPosition).append($alertdiv);
        }
        if (seconds > 0) {
            setTimeout(function () {
                $alertdiv.fadeOut();
                if ($.trim(jumpHref) != "") {
                    location.href = jumpHref;
                }
            }, seconds * 1000);
        }

        return $alertdiv;
    },
    /**
    成功信息提示
    */
    bsAlertSuccess: function (text, showPosition, seconds,jumpHref) {
        return this._bsAlertBase(this.bsAlertType.success, text, showPosition, seconds, jumpHref);
    },
    bsAlertInfo: function (text, showPosition, seconds, jumpHref) {
        return this._bsAlertBase(this.bsAlertType.info, text, showPosition, seconds, jumpHref);
    },
    bsAlertWarn: function (text, showPosition, seconds, jumpHref) {
        return this._bsAlertBase(this.bsAlertType.warning, text, showPosition, seconds, jumpHref);
    },
    bsAlertDanger: function (text, showPosition, seconds, jumpHref) {
        return this._bsAlertBase(this.bsAlertType.danger, text, showPosition, seconds, jumpHref);
    },
    bsAlertError: function (text, showPosition, seconds, jumpHref) {
        return this._bsAlertBase(this.bsAlertType.danger, text, showPosition, seconds, jumpHref);
    }
})

 