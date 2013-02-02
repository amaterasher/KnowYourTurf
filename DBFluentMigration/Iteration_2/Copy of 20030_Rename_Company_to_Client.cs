﻿namespace DBFluentMigration.Iteration_2
{
    using FluentMigrator;

    [Migration(20020)]
    public class Rename_Company_To_Client : Migration
    {
        public override void Up()
        {
            Rename.Table("Company").To("Client");
            Rename.Column("CompanyId").OnTable("BaseProduct").To("ClientId");
            Rename.Column("CompanyId").OnTable("Calculator").To("ClientId");
            Rename.Column("CompanyId").OnTable("Document").To("ClientId");
            Rename.Column("CompanyId").OnTable("DocumentCategory").To("ClientId");
            Rename.Column("CompanyId").OnTable("EmailJob").To("ClientId");
            Rename.Column("CompanyId").OnTable("EmailTemplate").To("ClientId");
            Rename.Column("CompanyId").OnTable("Equipment").To("ClientId");
            Rename.Column("CompanyId").OnTable("EquipmentTask").To("ClientId");
            Rename.Column("CompanyId").OnTable("EquipmentTaskType").To("ClientId");
            Rename.Column("CompanyId").OnTable("EquipmentType").To("ClientId");
            Rename.Column("CompanyId").OnTable("Event").To("ClientId");
            Rename.Column("CompanyId").OnTable("InventoryProduct").To("ClientId");
            Rename.Column("CompanyId").OnTable("LocalizedEnumeration").To("ClientId");
            Rename.Column("CompanyId").OnTable("LocalizedProperty").To("ClientId");
            Rename.Column("CompanyId").OnTable("LocalizedText").To("ClientId");
            Rename.Column("CompanyId").OnTable("Part").To("ClientId");
            Rename.Column("CompanyId").OnTable("Photo").To("ClientId");
            Rename.Column("CompanyId").OnTable("PhotoCategory").To("ClientId");
            Rename.Column("CompanyId").OnTable("PurchaseOrder").To("ClientId");
            Rename.Column("CompanyId").OnTable("PurchaseOrderLineItem").To("ClientId");
            Rename.Column("CompanyId").OnTable("Site").To("ClientId");
            Rename.Column("CompanyId").OnTable("Task").To("ClientId");
            Rename.Column("CompanyId").OnTable("TaskType").To("ClientId");
            Rename.Column("CompanyId").OnTable("User").To("ClientId");
            Rename.Column("CompanyId").OnTable("UserLoginInfo").To("ClientId");
            Rename.Column("CompanyId").OnTable("UserRole").To("ClientId");
            Rename.Column("CompanyId").OnTable("VendorBase").To("ClientId");
            Rename.Column("CompanyId").OnTable("VendorContact").To("ClientId");
            Rename.Column("CompanyId").OnTable("Weather").To("ClientId");
        }

        public override void Down()
        {
            Rename.Table("Client").To("Company");
            Rename.Column("ClientId").OnTable("BaseProduct").To("CompanyId");
            Rename.Column("ClientId").OnTable("Calculator").To("CompanyId");
            Rename.Column("ClientId").OnTable("Document").To("CompanyId");
            Rename.Column("ClientId").OnTable("DocumentCategory").To("CompanyId");
            Rename.Column("ClientId").OnTable("EmailJob").To("CompanyId");
            Rename.Column("ClientId").OnTable("EmailTemplate").To("CompanyId");
            Rename.Column("ClientId").OnTable("Equipment").To("CompanyId");
            Rename.Column("ClientId").OnTable("EquipmentTask").To("CompanyId");
            Rename.Column("ClientId").OnTable("EquipmentTaskType").To("CompanyId");
            Rename.Column("ClientId").OnTable("EquipmentType").To("CompanyId");
            Rename.Column("ClientId").OnTable("Event").To("CompanyId");
            Rename.Column("ClientId").OnTable("InventoryProduct").To("CompanyId");
            Rename.Column("ClientId").OnTable("LocalizedEnumeration").To("ClientId");
            Rename.Column("ClientId").OnTable("LocalizedProperty").To("CompanyId");
            Rename.Column("ClientId").OnTable("LocalizedText").To("CompanyId");
            Rename.Column("ClientId").OnTable("Part").To("CompanyId");
            Rename.Column("ClientId").OnTable("Photo").To("CompanyId");
            Rename.Column("ClientId").OnTable("PhotoCategory").To("CompanyId");
            Rename.Column("ClientId").OnTable("PurchaseOrder").To("CompanyId");
            Rename.Column("ClientId").OnTable("PurchaseOrderLineItem").To("CompanyId");
            Rename.Column("ClientId").OnTable("Site").To("CompanyId");
            Rename.Column("ClientId").OnTable("Task").To("CompanyId");
            Rename.Column("ClientId").OnTable("TaskType").To("CompanyId");
            Rename.Column("ClientId").OnTable("User").To("CompanyId");
            Rename.Column("ClientId").OnTable("UserLoginInfo").To("CompanyId");
            Rename.Column("ClientId").OnTable("UserRole").To("CompanyId");
            Rename.Column("ClientId").OnTable("VendorBase").To("CompanyId");
            Rename.Column("ClientId").OnTable("VendorContact").To("CompanyId");
            Rename.Column("ClientId").OnTable("Weather").To("CompanyId");
        }
    }
}