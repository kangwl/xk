﻿jQuery.fn.extend({
	autoHeight: function () {
		return this.each(function () {
			var $this = jQuery(this);
			if (!$this.attr('_initAdjustHeight')) {
				$this.attr('_initAdjustHeight', $this.outerHeight());
			}
			_adjustH(this).on('input', function () {
				_adjustH(this);
			});
		});
		/**
		 * 重置高度 
		 * @param {Object} elem
		 */
		function _adjustH(elem) {
			var $obj = jQuery(elem);
			return $obj.css({ height: $obj.attr('_initAdjustHeight'), 'overflow-y': 'hidden' })
					.height(elem.scrollHeight);
		}
	}
});