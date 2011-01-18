(function ($m) {
    if (!$m.Ext) { $m.Ext = {}; }
    var InfoBox = $m.Ext.InfoBox = function (title, desc, map) {
        return new InfoBox.fn.init(title, desc, map);
    };
    InfoBox.prototype = InfoBox.fn = {
        init: function (title, desc, map) {
            this._element = null;
            this._title = title;
            this._description = desc;
            this._map = map;
        },
        title: function (v) {
            if (v !== undefined) {
                this._title = v;
                return this;
            }
            return this._title;
        },
        description: function (v) {
            if (v !== undefined) {
                this._description = v;
                return this;
            }
            return this._description;
        },
        show: function (e) {
            var loc = this.map().
                tryLocationToPixel(
                    e.target.getLocation(), Microsoft.Maps.PixelReference.page
                );
            if (this._element === null) {
                this._element = $('<div>').
                    addClass('infobox').
                    appendTo($(document.body)).
                    html('<div class="mtPopupTL"/><div class="mtPopupTR"/><div class="mtPopupTC"/><div class="infoboxCon">'
                    + this.title() + this.description()
                    + '</div><div class="mtPopupBL"/><div class="mtPopupBR"/><div class="mtPopupBC"/>');
            }
            this._element.show().css({
                position: 'absolute',
                top: loc.y - 50,
                left: loc.x+5
            });
        },
        hide: function () {
            if (this._element !== null) {
                this._element.hide();
            }
        },
        map: function (v) {
            if (v) {
                this._map = v;
                return this;
            }
            return this._map;
        }
    };
    InfoBox.fn.init.prototype = InfoBox.fn;
    (function (proto) {
        if (!proto.setInfoBox) {
            proto.setInfoBox = function (infoBox) {
                this.removeInfoBox();
                this.infobox = infoBox;
                this.infoboxMouseOverHandler = $m.Events.addHandler(
                    this,
                    "mouseover",
                    function (e) {
                        infoBox.show(e);
                    });
                this.infoboxMouseOutHandler = $m.Events.addHandler(
                    this,
                    "mouseout",
                    function (e) {
                        infoBox.hide(e);
                    });
            };
        }
        if (!proto.removeInfoBox) {
            proto.removeInfoBox = function () {
                $m.Events.removeHandler(this.infoboxMouseOverHandler);
                $m.Events.removeHandler(this.infoboxMouseOutHandler);
this.infobox = null;
            };
        }
    })($m.Pushpin.prototype);
})(Microsoft.Maps);
function DragHandler(e) {
    var loc = e.entity.getLocation();
    document.getElementById("latitude").setAttribute("value",loc.latitude.toFixed(8));
    document.getElementById("longitude").setAttribute("value", loc.longitude.toFixed(8));
}