/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 8/5/12
 * Time: 9:53 AM
 * To change this template use File | Settings | File Templates.
 */
KYT.mixins = {};

KYT.mixin = function(target, mixin, overrides){
    if(target.events && KYT.mixins[mixin].events){
        var events = $.extend({}, target.events, KYT.mixins[mixin].events)
    }
    $.extend(target,KYT.mixins[mixin],overrides||{});
    if(events){
        target.events = events;
    }
};

KYT.mixins.modelAndElementsMixin = {
    bindModelAndElements:function(){
        // make sure to apply ids prior to ko mapping.
        this.model = ko.mapping.fromJS(this.model);
        ko.applyBindings(this.model,this.el);
        this.elementsViewmodel = CC.elementService.getElementsViewmodel(this);
        this.mappingOptions ={ ignore: _.filter(_.keys(this.model),function(item){
            return (item.indexOf('_') == 0 && item != "__ko_mapping__");
        })};
        this.mappingOptions.ignore.push("_availableItems");
        this.mappingOptions.ignore.push("_resultsItems");
        this.addIdsToModel();

    },
    addIdsToModel:function(){
        var rel = KYT.State.get("Relationships");
        if(!rel){return;}
        this.model.EntityId = rel.entityId;
        this.model.ParentId = rel.parentId;
        this.model.RootId = rel.rootId;
        this.model.Var = rel.extraVar;
    }
};

KYT.mixins.formMixin = {
    events:{
        'click #save' : 'saveItem',
        'click #cancel' : 'cancel'
    },

    saveItem:function(){
        var isValid = CC.ValidationRunner.runViewModel(this.elementsViewmodel);
        if(!isValid){return;}
        var data;
        var fileInputs = $('input:file', this.$el);
        if(fileInputs.length > 0 && _.any(fileInputs, function(item){return $(item).val();})){
            var that = this;
            data = ko.mapping.toJS(this.model,this.mappingOptions);
            var ajaxFileUpload = new CC.AjaxFileUpload(fileInputs[0],{
                action:that.model._saveUrl(),
                onComplete:function(file,response){that.successHandler(response);}
            });
            ajaxFileUpload.setData(data);
            ajaxFileUpload.submit()
        }
        else{
            data = JSON.stringify(ko.mapping.toJS(this.model,this.mappingOptions));
            var promise = KYT.repository.ajaxPostModel(this.model._saveUrl(),data);
            promise.done($.proxy(this.successHandler,this));
        }
    },
    cancel:function(){
        KYT.vent.trigger("form:"+this.id+":cancel");
        if(!this.options.noBubbleUp) {KYT.WorkflowManager.returnParentView();}
    },
    successHandler:function(_result){
        var result = typeof _result =="string" ? JSON.parse(_result) : _result;
        if(!CC.notification.handleResult(result,this.cid)){
            return;
        }
        KYT.vent.trigger("form:"+this.id+":success",result);
        if(!this.options.noBubbleUp){KYT.WorkflowManager.returnParentView(result,true);}
    }
};

KYT.mixins.ajaxFormMixin = {
    render:function(){
        $.when(KYT.loadTemplateAndModel(this))
         .done($.proxy(this.renderCallback,this));
    },
    renderCallback:function(){
        this.bindModelAndElements();
        this.viewLoaded();
        KYT.vent.trigger("form:"+this.id+":pageLoaded",this.options);
        $(this.el).find("form :input:visible:enabled:first").focus();
    }
};
