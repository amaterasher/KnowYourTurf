/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:40 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof CC == "undefined") {
    var CC = {};
}
CC.Elements={};



CC.Elements.Element = function($container){
     this.$container = $container;
     this.trimFieldName = function(){
        var name = this.$container.find("label").text();
        name = name.replace(":","");
        name = name.replace("*","");
        return $.trim(name);
    }
};
CC.Elements.Element.extend = Backbone.View.extend;
$.extend(CC.Elements.Element.prototype,{
    init:function(){
        this.cid = _.uniqueId("c");
        this.type = "Element";
        this.fieldName = this.trimFieldName();
    },
    validate:function(){
        CC.ValidationRunner.runElement(this);
    },
    getValue:function(){
        return this.$input.val();
    },
    setValidState: function(isValid){
        if(isValid){
            this.$input.removeClass("invalid");
            if(!this.$input.hasClass("valid"))
            this.$input.addClass("valid")
        }else{
            this.$input.removeClass("valid");
            if(!this.$input.hasClass("invalid"))
            this.$input.addClass("invalid")
        }
        this.isValid = isValid;
    },
    destroy:function(){
        CC.notification.removeById(this.cid);
    }
});

CC.Elements.Textbox = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "textbox";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.DateTextbox = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "datetextbox";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
        this.$input.datepicker({
            changeYear: true,
            changeMonth: true,
            onClose: function(text, inst){
                $(this).change();
            }
        });
    },
    destroy:function(){
        this.$input.off("change");
        this.$input.datepicker("destroy");
        this._super("destroy",arguments);
    }
});

CC.Elements.TimeTextbox = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "timetextbox";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
        this.$input.timepicker({showPeriod: true,showLeadingZero: false,
            onClose: function(text, inst){
                $(this).change();
            }});
    },
    destroy:function(){
        this.$input.off("change");
       // this.$input.timepicker("destroy");
        this._super("destroy",arguments);
    }
});

CC.Elements.NumberTextbox = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "numbertextbox";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Textarea = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "textarea";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Checkbox = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "checkbox";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Password= CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "textbox";
        this.$input = this.$container.find("input");
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Select = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "select";
        this.$input = this.$container.find("select");
        this.chosen = this.$input.chosen();
        this.$input.chosen().change(function(){that.validate()});
    },
    destroy:function(){
       $(".chzn-container *").unbind().remove();
        this._super("destroy",arguments);
    }
});

CC.Elements.MultiSelect = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "select";
        this.$input = this.$container.find("input.multiSelect");
        this.$container.on(this.$input.attr("id")+":tokenizer:blur",$.proxy(that.multiSelectBlur,that));

    },
    multiSelectBlur: function(e, viewmodel){
        this.selectedViewmodel = viewmodel;
        this.validate();
    },
    getValue:function(){
        return this.selectedViewmodel;
    },
    destroy:function(){
        this.$container.off(this.$input.attr("id")+":tokenizer:blur");
        $("#"+this.$input.attr("name")+"_container *").unbind().remove();
        this._super("destroy",arguments);
    }
});

_.each(["Element", "Textbox", "Password","Select", "MultiSelect"], function(klass) {
    CC.Elements[klass].prototype._super = Backbone.View.prototype._super;
});