/*
* jHtmlAreaSmiles 0.5.0 - A Smiles Extension for jHtmlArea
* Part of the MyTripMvc Project
* http://mytripmvc.net
* Based on jHtmlArea
* http://jhtmlarea.codeplex.com
*/
(function ($) {
    if (jHtmlArea) {

        jHtmlArea.fn.smile = function () {
                var that = this;
                var editor=this;
                jHtmlAreaSmilesMenu($(".smile", this.toolbar), {
                    smileChosen: function (smile) {
                        editor.pasteHTML("<img src='" + smile + "' alt='smile'/>");
                    }
                });
        };
    }
    var menu = window.jHtmlAreaSmilesMenu = function (ownerElement, options) {
        return new jHtmlAreaSmilesMenu.fn.init(ownerElement, options);
    };
    menu.fn = menu.prototype = {
        jHtmlAreaSmilesMenu: "0.5.0",

        init: function (ownerElement, options) {
            var opts = $.extend({}, menu.defaultOptions, options);
            var that = this;
            var owner = this.owner = $(ownerElement);
            var position = owner.position();
            if (menu.instance) {
                menu.instance.hide();
            }
            jHtmlAreaSmilesMenu.instance = this;

            var picker = this.picker = $("<div/>").css({
                position: "absolute",
                left: position.left + opts.offsetLeft,
                top: position.top + owner.height() + opts.offsetTop,
                "z-index": opts["z-index"]
            }).addClass("jHtmlAreaSmiles");

            for (var i = 0; i < opts.smile.length; i++) {
                var c = opts.smile[i];
                $("<div/>").html("<img src=\""+c+"\" alt=\"smile\"/>").appendTo(picker).click(
                    (function (smile) {
                        return function () {
                            if (opts.smileChosen) {
                                opts.smileChosen.call(this, smile);
                            }
                            that.hide();
                        };
                    })(c)
                );
            }

            var autoHide = false;
            picker.appendTo(owner.parent()).
                show().
                mouseout(function () {
                    autoHide = true;
                    that.currentTimeout = window.setTimeout(function () { if (autoHide === true) { that.hide(); } },750);
                }).
                mouseover(function () {
                    if (that.currentTimeout) {
                        window.clearTimeout(that.currentTimeout);
                        that.currentTimeout = null;
                    }
                    autoHide = false;
                });
        },
        hide: function () {
            this.picker.hide();
            this.picker.remove();
        }
    };
    menu.fn.init.prototype = menu.fn;

    menu.defaultOptions = {
        "z-index": 9999,
        offsetTop: 0,
        offsetLeft: 0,
        smile: [
        "/mtm/Smile/aa",
        "/mtm/Smile/ab",
        "/mtm/Smile/ac",
        "/mtm/Smile/ad",
        "/mtm/Smile/ae",
        "/mtm/Smile/af",
        "/mtm/Smile/ag",
        "/mtm/Smile/ah",
        "/mtm/Smile/ai",
        "/mtm/Smile/aj",
        "/mtm/Smile/ak",
        "/mtm/Smile/al",
        "/mtm/Smile/am",
        "/mtm/Smile/an",
        "/mtm/Smile/ao",
        "/mtm/Smile/ap",
        "/mtm/Smile/aq",
        "/mtm/Smile/ar",
        "/mtm/Smile/_as",
        "/mtm/Smile/at",
        "/mtm/Smile/au",
        "/mtm/Smile/av",
        "/mtm/Smile/aw",
        "/mtm/Smile/ax",
        "/mtm/Smile/ay",
        "/mtm/Smile/az",
        "/mtm/Smile/ba",
        "/mtm/Smile/bb",
        "/mtm/Smile/bc",
        "/mtm/Smile/bd",
        "/mtm/Smile/be",
        "/mtm/Smile/bf",
        "/mtm/Smile/bg",
        "/mtm/Smile/bh",
        "/mtm/Smile/bi",
        "/mtm/Smile/bj",
        "/mtm/Smile/bk",
        "/mtm/Smile/bl",
        "/mtm/Smile/bm",
        "/mtm/Smile/bn",
        "/mtm/Smile/bo",
        "/mtm/Smile/bp",
        "/mtm/Smile/bq",
        "/mtm/Smile/br",
        "/mtm/Smile/bs",
        "/mtm/Smile/bt",
        "/mtm/Smile/bu",
        "/mtm/Smile/bv",
        "/mtm/Smile/bw",
        "/mtm/Smile/bx",
        "/mtm/Smile/ca",
        "/mtm/Smile/cd",
        "/mtm/Smile/ce",
        "/mtm/Smile/ch",
        "/mtm/Smile/ci"
        ],
        smileChosen: null
    };
})(jQuery);