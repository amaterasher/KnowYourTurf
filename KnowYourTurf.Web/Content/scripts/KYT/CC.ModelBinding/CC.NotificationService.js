/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:43 AM
 * To change this template use File | Settings | File Templates.
 */

CC.NotificationService = function(){
    this.messageContainer = $("#messageContainer").get(0);
    this.viewmodel = {
        messages: ko.observableArray()
    };
    ko.applyBindings(this.viewmodel,this.messageContainer);
};

$.extend(CC.NotificationService.prototype,{
    add:function(msgObject){
        var exists = _.any(this.viewmodel.messages(),function(msg){
            return msgObject.elementCid() === msg.elementCid() && msgObject.message() === msg.message();
        });
        if(!exists){
            this.viewmodel.messages.push(msgObject);
            // check if errors are showing and if not show
            //
        }
    },
    remove:function(msgObject){
        this.viewmodel.messages.remove(function(item){
            return item.elementCid()===msgObject.elementCid()
            && item.message() === msgObject.message();
        });
    },
    handleResult:function(result){
        if(!result.Success){
            if(result.Message){
                this.add(new CC.NotificationMessage(this.cid, result.Message,"error"));
            }
            if(result.Errors){
                _.each(result.Errors,function(item){
                    this.add(new CC.NotificationMessage(this.cid, item.PropertyName+ " "+item.ErrorMessage,"error"));
                })
            }
        }
    }
});


CC.NotificationMessage = function(elementCid, message, status){
    this.message = ko.observable(message);
    this.elementCid = ko.observable(elementCid);
    this.status = ko.observable(status);
};


