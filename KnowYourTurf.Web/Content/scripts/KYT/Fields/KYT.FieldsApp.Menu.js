/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 2/26/12
 * Time: 11:19 AM
 * To change this template use File | Settings | File Templates.
 */

KYT.FieldsApp.Menu = (function(KYT, Backbone, $){
    var Menu = {};

    Menu.show = function(){
        var routeToken = _.find(KYT.routeTokens,function(item){
            return item.id == "scheduleMenu";
        });
        var view = new MenuView(routeToken);
        KYT.menu.show(view);
        $("#left-navigation").show();
    };

    var MenuView =  KYT.Views.View.extend({
        render:function(){
            KYT.repository.ajaxGet(this.options.url, this.options.data, $.proxy(this.renderCallback,this));
        },
        renderCallback:function(result){
            if(result.LoggedOut){
                window.location.replace(result.RedirectUrl);
                return;
            }
            $(this.el).html(result);
            KYT.vent.bind("menuItem", this.menuItemClick,this);
            $(this.el).find(".ccMenu").ccMenu({ backLink: false, width : 220 });
            return this;
        },
        menuItemClick:function(name){
            KYT.vent.trigger("route",name,true);
        },
        onClose:function(){
            KYT.vent.unbind("menuItem");
        }
    });

    return Menu;
})(KYT, Backbone, jQuery);
