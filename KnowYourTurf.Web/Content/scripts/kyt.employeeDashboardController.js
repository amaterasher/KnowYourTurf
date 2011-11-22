/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 10/9/11
 * Time: 4:05 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof kyt == "undefined") {
            var kyt = {};
}


kyt.EmployeeDashboardController  = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        $.clearPubSub();
        this.registerSubscriptions();
        this.id = "employeeDashboardController";
        var displayOptions={
            el:"#masterArea"
        };
        var _options = $.extend({},this.options,displayOptions);
        this.views.displayView = new kyt.DisplayView(_options);

        var ptgOptions = {
            el:"#pendingTaskGridContainer",
            id:"pendingTaskGrid",
            gridName:"pendingTaskGrid",
            gridContainer:"#gridContainer_pt",
            gridDef:this.options.pendingGridDef,
            addEditUrl:this.options.pendingTaskaddEditUrl
        };
        this.views.pendingTaskGridView = new kyt.GridView(ptgOptions);
        var ctgOptions = {
            el:"#completeTaskGridContainer",
            id:"completeTaskGrid",
            gridName:"completeTaskGrid",
            gridContainer:"#gridContainer_ct",
            gridDef:this.options.completeGridDef,
            // this is not used except for copy task which is why it's for the pendingGrid
            addEditUrl:this.options.pendingTaskaddEditUrl
        };
        this.views.completeTaskGridView = new kyt.GridView(ctgOptions);
    },

    registerSubscriptions: function(){
        // from grid
        $.subscribe('/grid_pendingTaskGrid/AddNewItem',$.proxy(function(url,data){this.addEditItem(url,data,"pendingTaskForm")},this), this.cid);
        $.subscribe('/grid_pendingTaskGrid/Edit',$.proxy(function(url,data){this.addEditItem(url,data,"pendingTaskForm")},this), this.cid);
        $.subscribe('/grid_pendingTaskGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"pendingTaskDisplay")},this), this.cid);

        $.subscribe('/grid_completeTaskGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"completeTaskDisplay")},this), this.cid);

        $.subscribe('/popupFormModule_pendingTaskForm/popupLoaded',$.proxy(this.loadTokenizers,this), this.cid);
        // from form
        $.subscribe('/form_pendingTaskForm/success', $.proxy(this.formSuccess,this), this.cid);
        $.subscribe('/form_pendingTaskForm/cancel', $.proxy(this.popupCancel,this), this.cid);

        // from display
        $.subscribe('/popup_pendingTaskDisplay/cancel', $.proxy(this.popupCancel,this), this.cid);
        $.subscribe('/popup_pendingTaskDisplay/edit', $.proxy(this.displayEdit,this), this.cid);
        $.subscribe('/popup_pendingTaskDisplay/copyTask', $.proxy(this.copyTask,this), this.cid);

        $.subscribe('/popup_completeTaskDisplay/cancel', $.proxy(this.popupCancel,this), this.cid);
        $.subscribe('/popup_completeTaskDisplay/copyTask', $.proxy(this.copyTask,this), this.cid);
    },

    addEditItem: function(url, data,name){
        var crudFormOptions={};
        crudFormOptions.additionalSubmitData =  {"From":"Employee","ParentId":entityId};
        var _url = url?url:this.options[name+"addEditUrl"];
        $("#masterArea").after("<div id='dialogHolder'/>");
        var moduleOptions = {
            id:name,
            el:"#dialogHolder",
            url: _url,
            data:data,
            crudFormOptions:crudFormOptions,
            buttons: kyt.popupButtonBuilder.builder(name).standardEditButons()
        };
        this.modules[name] = new kyt.PopupFormModule(moduleOptions);
    },

    displayItem: function(url, data,name){
        var _url = url?url:this.options.displayUrl;
        var builder = kyt.popupButtonBuilder.builder(name);
        var buttons = builder.standardDisplayButtons();
        if(name == "pendingTaskDisplay" || name== "completeTaskDisplay"){
            builder.clearButtons();
            builder.addButton("Copy Task", function(){$.publish("/popup_"+name+"/copyTask",[$("#AddEditUrl",this).val(),name])});
            builder.addCancelButton();
            if(name == "pendingTaskDisplay" ){
                builder.addEditButton();
            }
        buttons = builder.getButtons();
        }
        $("#masterArea").after("<div id='dialogHolder'/>");
            var moduleOptions = {
            id:name,
            el:"#dialogHolder",
            url: _url,
            buttons: buttons
        };
        this.modules[name] = new kyt.PopupDisplayModule(moduleOptions);
    },
    //from popupformmodule
    loadTokenizers:function(formOptions){
        var employeeTokenOptions = {
            id:this.id+"employee",
            el:"#employeeTokenizer",
            availableItems:formOptions.employeeOptions.availableItems,
            selectedItems:formOptions.employeeOptions.selectedItems,
            inputSelector:formOptions.employeeOptions.inputSelector
        };

        var equipmentTokenOptions = {
            id:this.id+"equipment",
            el:"#equipmentTokenizer",
            availableItems:formOptions.equipmentOptions.availableItems,
            selectedItems:formOptions.equipmentOptions.selectedItems,
            inputSelector:formOptions.equipmentOptions.inputSelector
        };
        this.views.employeeToken= new kyt.TokenView(employeeTokenOptions);
        this.views.equipmentToken = new kyt.TokenView(equipmentTokenOptions);

    },
    //from form
    formSuccess:function(result,form,id){
        this.popupCancel(id);
        this.views[this.getRootOfName(id) +"GridView"].reloadGrid();
        if(id=="pendingTaskForm"){
            this.views["completeTaskGridView"].reloadGrid();
        }

    },
    popupCancel: function(id){
        this.modules[id].destroy();
    },

    //from display
    displayEdit:function(url, name){
        this.modules[name].destroy();
        this.addEditItem(url, null,this.getRootOfName(name)+"Form");
    },

    copyTask:function(url, name){
        this.modules[name].destroy();
        this.addEditItem(url, {"Copy":"true"}, "pendingTaskForm");
    },

    getRootOfName:function(name){
        if(name.indexOf("Display")>0){
            return name.substring(0,name.indexOf("Display"));
        }else if(name.indexOf("Form")>0){
            return name.substring(0,name.indexOf("Form"));
        }
    }
});