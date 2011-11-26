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


kyt.FieldDashboardController  = kyt.Controller.extend({
    events:_.extend({
    }, kyt.Controller.prototype.events),

    initialize:function(){
        $.extend(this,this.defaults());
        $.clearPubSub();
        this.registerSubscriptions();
        this.id = "fieldDashboardController";
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
        var pgOptions = {
            el:"#photoGridContainer",
            id:"photoGrid",
            gridName:"photoGrid",
            gridContainer:"#gridContainer_p",
            gridDef:this.options.photoGridDef,
            addEditUrl:this.options.photoaddEditUrl
        };
        this.views.photoGridView = new kyt.GridView(pgOptions);
        var dgOptions = {
            el:"#documentGridContainer",
            id:"documentGrid",
            gridName:"documentGrid",
            gridContainer:"#gridContainer_d",
            gridDef:this.options.documentGridDef,
            addEditUrl:this.options.documentaddEditUrl
        };
        this.views.documentGridView = new kyt.GridView(dgOptions);
        if($("ul li","#photoSlide").size()>0){
            this.loadPhotoSlide();
        }
    },

    loadPhotoSlide: function(){
        $("#photoSlide").slideViewerPro({
            thumbs: 4,
            typo: true,
            galBorderWidth: 0,
            thumbsBorderOpacity: 0,
            buttonsTextColor: "#707070",
            buttonsWidth: 20,
            thumbsActiveBorderOpacity: 0.8,
            thumbsActiveBorderColor: "aqua"
        });
        this.options.photoSlideLoaded = true;
    },
    registerSubscriptions: function(){
        // from grid
        $.subscribe('/grid_pendingTaskGrid/AddNewItem',$.proxy(function(url,data){this.addEditItem(url,data,"pendingTaskForm")},this), this.cid);
        $.subscribe('/grid_pendingTaskGrid/Edit',$.proxy(function(url,data){this.addEditItem(url,data,"pendingTaskForm")},this), this.cid);
        $.subscribe('/grid_pendingTaskGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"pendingTaskDisplay")},this), this.cid);
        $.subscribe('/grid_pendingTaskGrid/Delete',$.proxy(this.deletePendingTask,this), this.cid);

        $.subscribe('/grid_completeTaskGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"completeTaskDisplay")},this), this.cid);

        $.subscribe('/grid_photoGrid/AddNewItem',$.proxy(function(url,data){this.addEditItem(url,data,"photoForm")},this), this.cid);
        $.subscribe('/grid_photoGrid/Edit',$.proxy(function(url,data){this.addEditItem(url,data,"photoForm")},this), this.cid);
        $.subscribe('/grid_photoGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"photoDisplay")},this), this.cid);
        $.subscribe('/grid_photoGrid/Delete',$.proxy(this.deletePhoto,this), this.cid);

        $.subscribe('/grid_documentGrid/AddNewItem',$.proxy(function(url,data){this.addEditItem(url,data,"documentForm")},this), this.cid);
        $.subscribe('/grid_documentGrid/Edit',$.proxy(function(url,data){this.addEditItem(url,data,"documentForm")},this), this.cid);
        $.subscribe('/grid_documentGrid/Display',$.proxy(function(url,data){this.displayItem(url,data,"documentDisplay")},this), this.cid);
        $.subscribe('/grid_documentGrid/Delete',$.proxy(this.deleteDocument,this), this.cid);

        $.subscribe('/popupFormModule_pendingTaskForm/popupLoaded',$.proxy(this.loadTokenizers,this), this.cid);

        // from form
        $.subscribe('/form_pendingTaskForm/success', $.proxy(this.formSuccess,this), this.cid);
        $.subscribe('/form_pendingTaskForm/cancel', $.proxy(this.popupCancel,this), this.cid);

        $.subscribe('/form_photoForm/success', $.proxy(this.formSuccess,this), this.cid);
        $.subscribe('/form_photoForm/cancel', $.proxy(this.popupCancel,this), this.cid);

        $.subscribe('/form_documentForm/success', $.proxy(this.formSuccess,this), this.cid);
        $.subscribe('/form_documentForm/cancel', $.proxy(this.popupCancel,this), this.cid);

        // from display
        $.subscribe('/popup_pendingTaskDisplay/cancel', $.proxy(this.popupCancel,this), this.cid);
        $.subscribe('/popup_pendingTaskDisplay/edit', $.proxy(this.displayEdit,this), this.cid);
        $.subscribe('/popup_pendingTaskDisplay/copyTask', $.proxy(this.copyTask,this), this.cid);

        $.subscribe('/popup_completeTaskDisplay/cancel', $.proxy(this.popupCancel,this), this.cid);
        $.subscribe('/popup_completeTaskDisplay/copyTask', $.proxy(this.copyTask,this), this.cid);

        $.subscribe('/popup_photoDisplay/cancel', $.proxy(this.popupCancel,this), this.cid);
        $.subscribe('/popup_photoDisplay/edit', $.proxy(this.displayEdit,this), this.cid);

        $.subscribe('/popup_documentDisplay/cancel', $.proxy(this.popupCancel,this), this.cid);
        $.subscribe('/popup_documentDisplay/edit', $.proxy(this.displayEdit,this), this.cid);
    },

    addEditItem: function(url, data,name){
        var crudFormOptions={};
        crudFormOptions.additionalSubmitData =  {"From":"Field","ParentId":entityId};
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
    deletePendingTask:function(url){
        kyt.repository.ajaxPost(url)
    },
    deletePhoto:function(url){
        kyt.repository.ajaxPost(url)
    },
    deleteDocument:function(url){
        kyt.repository.ajaxPost(url)
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
    photoFormSuccess:function(result,form,id){
        // I don't know how to reload this dumb thing for new doc.
        // will look for better slider then add the new
        //doc to slider here
        if(!this.options.PhotoslideLoaded){
            this.loadPhotoSlide();
        }
        this.popupCancel(id);
        this.views[this.getRootOfName(id) +"GridView"].reloadGrid();
        if(id=="pendingTaskForm"){
            this.views["completeTaskGridView"].reloadGrid();
        }
    },
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