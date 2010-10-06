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
                $("<div/>").html("<img src='"+c+"' alt='smile'/>").appendTo(picker).click(
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
        "/Content/smiles/aa.gif",
        "/Content/smiles/ab.gif",
        "/Content/smiles/ac.gif",
        "/Content/smiles/ad.gif",
        "/Content/smiles/ae.gif",
        "/Content/smiles/af.gif",
        "/Content/smiles/ag.gif",
        "/Content/smiles/ah.gif",
        "/Content/smiles/ai.gif",
        "/Content/smiles/aj.gif",
        "/Content/smiles/ak.gif",
        "/Content/smiles/al.gif",
        "/Content/smiles/am.gif",
        "/Content/smiles/an.gif",
        "/Content/smiles/ao.gif",
        "/Content/smiles/ap.gif",
        "/Content/smiles/aq.gif",
        "/Content/smiles/ar.gif",
        "/Content/smiles/as.gif",
        "/Content/smiles/at.gif",
        "/Content/smiles/au.gif",
        "/Content/smiles/av.gif",
        "/Content/smiles/aw.gif",
        "/Content/smiles/ax.gif",
        "/Content/smiles/ay.gif",
        "/Content/smiles/az.gif",
        "/Content/smiles/ba.gif",
        "/Content/smiles/bb.gif",
        "/Content/smiles/bc.gif",
        "/Content/smiles/bd.gif",
        "/Content/smiles/be.gif",
        "/Content/smiles/bf.gif",
        "/Content/smiles/bg.gif",
        "/Content/smiles/bh.gif",
        "/Content/smiles/bi.gif",
        "/Content/smiles/bj.gif",
        "/Content/smiles/bk.gif",
        "/Content/smiles/bl.gif",
        "/Content/smiles/bm.gif",
        "/Content/smiles/bn.gif",
        "/Content/smiles/bo.gif",
        "/Content/smiles/bp.gif",
        "/Content/smiles/bq.gif",
        "/Content/smiles/br.gif",
        "/Content/smiles/bs.gif",
        "/Content/smiles/bt.gif",
        "/Content/smiles/bu.gif",
        "/Content/smiles/bv.gif",
        "/Content/smiles/bw.gif",
        "/Content/smiles/bx.gif",
        "/Content/smiles/ca.gif",
        "/Content/smiles/cd.gif",
        "/Content/smiles/ce.gif",
        "/Content/smiles/ch.gif",
        "/Content/smiles/ci.gif"
        ],
        smileChosen: null
    };
})(jQuery);