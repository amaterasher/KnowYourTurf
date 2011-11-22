/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/6/11
 * Time: 4:52 PM
 * To change this template use File | Settings | File Templates.
 */
var kyt = kyt || {};

kyt.EmployeeController = kyt.CrudController.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    additionalSubscriptions:function(){
        $.subscribe('/form_editModule/pageLoaded', $.proxy(this.loadTokenizers,this), this.cid);
    },

    loadTokenizers: function(formOptions){
        var options = $.extend({},formOptions,{el:"#dialogHolder"});
        this.views.roles = new kyt.TokenView(formOptions.rolesOptions);
    }
});
