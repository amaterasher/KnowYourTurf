using System;
using System.ComponentModel;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;
using StructureMap;

namespace Generator
{
    public class DataLoader
    {
        private IRepository _repository;
        private Field _field1;
        private Field _field2;
        private Task _task1;
        private Task _task2;
        private Vendor _vendor2;
        private Vendor _vendor1;
        private Material _materials1;
        private Material _materials2;
        private Fertilizer _fertilizer2;
        private Fertilizer _fertilizer1;
        private Chemical _chemical1;
        private Chemical _chemical2;
        private InventoryProduct _inventoryChemical1;
        private InventoryProduct _inventoryChemical2;
        private InventoryProduct _inventoryFertilizer1;
        private InventoryProduct _inventoryFertilizer2;
        private InventoryProduct _invenotyMaterial1;
        private InventoryProduct _inventoryMaterial2;
        private Equipment _equip1;
        private Equipment _equip2;
        private User _employee1;
        private User _employee2;
        private static VendorContact _contact1v1;
        private static VendorContact _contact2v1;
        private static VendorContact _contact1v2;
        private static VendorContact _contact2v2;
        private Company _company;
        private IPurchaseOrderLineItemService _purchaseOrderLineItemService;
        private User _defaultUser;
        private User _employeeAdmin1;
        private User _employeeAdmin2;
        private UserRole _userRoleAdmin;
        private UserRole _userRoleEmployee;
        private UserRole _userRoleFac;

        public void Load()
        {
            _repository = ObjectFactory.GetInstance<IRepository>();
            _repository.UnitOfWork.Initialize();

            CreateCompany();
            CreateUserRoles();
            CreateUser();
            _purchaseOrderLineItemService = new PurchaseOrderLineItemService(new UserSessionFake(_defaultUser));
            
            CreateEmployee();
            CreateField();
            CreateEquipment();
            CreateChemical();
            CreateMaterials();
            CreateFertilizer();
            CreateInventory();
            CreateTask();
            CreateEventType();

            CreateVendor();
            CreateVendorContact();
            CreateCalculator();
            CreateDocumentCategory();
            CreatePhotoCategory();
            CreateEmailTemplate();

            _repository.UnitOfWork.Commit();

        }

        private void CreateUserRoles()
        {
            _userRoleAdmin = new UserRole
            {
                Name = UserType.Administrator.ToString()
            };
            _userRoleEmployee = new UserRole
            {
                Name = UserType.Employee.ToString()
            };
            _userRoleFac = new UserRole
            {
                Name = UserType.Facilities.ToString()
            };
            _repository.Save(_userRoleAdmin);
            _repository.Save(_userRoleEmployee);
            _repository.Save(_userRoleFac);
        }

        private void CreateEmailTemplate()
        {
            var template = new EmailTemplate
                                    {
                                        Name = "EmployeeDailyTask",
                                        Template =
                                            "<p>Hi {%=name%},</p><p>Here are your tasks for {%=data%}:</p><p>{%=tasks%}</p><p>Thank you,</p><p>Management</p>"
                                    };
            _repository.Save(template);
        }


        private void CreateDocumentCategory()
        {
            var category = new DocumentCategory
            {
                Name = "Field",
                Description = "pictures of fields"
            };
            var category2 = new DocumentCategory
            {
                Name = "People",
                Description = "pictures of people"
            };
            _repository.Save(category);
            _repository.Save(category2);

        }

        private void CreatePhotoCategory()
        {
            var category = new PhotoCategory
            {
                Name = "Field",
                Description = "pictures of fields"
            };
            var category2 = new PhotoCategory
            {
                Name = "People",
                Description = "pictures of people"
            };
            _repository.Save(category);
            _repository.Save(category2);

        }


        private void CreateCalculator()
        {
            var fertilizerNeeded = new Calculator { Name = "FertilizerNeeded" };
            var materials = new Calculator { Name = "Materials" };
            var sand = new Calculator { Name = "Sand" };
            var overseedBagsNeeded = new Calculator { Name = "OverseedBagsNeeded" };
            var overseedRateNeeded = new Calculator { Name = "OverseedRateNeeded" };
            var fertilizerUsed = new Calculator { Name = "FertilizerUsed" };
            _repository.Save(fertilizerNeeded);
            _repository.Save(fertilizerUsed);
            _repository.Save(materials);
            _repository.Save(sand);
            _repository.Save(overseedBagsNeeded );
            _repository.Save(overseedRateNeeded);
        }

        private void CreateCompany()
        {
            _company = new Company { Name = "KYT", ZipCode = "78702", TaxRate = 8.25 };
            _repository.Save(_company);
        }

        private void CreateEquipment()
        {
            _equip1 = new Equipment
                             {
                                 Name = "Truck"
                             };
            _equip2 = new Equipment
                             {
                                 Name = "Plane"
                             };
            _repository.Save(_equip1);
            _repository.Save(_equip2);
        }

        private void CreateUser()
        {
            _defaultUser = new User()
            {
                
                FirstName = "Raif",
                LastName = "Harik",
                Company = _company
            };
            _defaultUser.UserLoginInfo = new UserLoginInfo
                                             {
                                                 LoginName = "Admin",
                                                 Password = "123",
                                                 UserType = UserType.Administrator.ToString(),
                                                 Status = "Active"
                                             };
            _defaultUser.AddUserRole(_userRoleAdmin);
            _defaultUser.AddUserRole(_userRoleEmployee);
            var altUser = new User()
            {
                FirstName = "Amahl",
                LastName = "Harik",
                Company = _company
            };
            altUser.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "alt",
                Password = "alt",
                UserType = UserType.Employee.ToString(),
            };
            altUser.AddUserRole(_userRoleAdmin);
            altUser.AddUserRole(_userRoleEmployee);

            var facilities = new User()
            {
                FirstName = "Amahl",
                LastName = "Harik",
                Company = _company
            };
            facilities.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "facilities",
                Password = "facilities",
                UserType = UserType.Facilities.ToString(),
                Status = "Active"
            };
            facilities.AddUserRole(_userRoleFac);


            _repository.Save(_defaultUser);
            _repository.Save(altUser);
            _repository.Save(facilities);

        }

        private void CreateEmployee()
        {
            _employee1 = new User()
            {
                EmployeeId = "123",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "reharik@gmail.com",
                FirstName = "Raif",
                LastName = "Harik",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
                };
            _employee1.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "reharik@gmail.com",
                Password = "123",
                UserType = UserType.Employee.ToString(),
                Status = "Active"
            };

            _employee2 = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "amahl@gmail.com",
                FirstName = "Amahl",
                LastName = "Harik",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
                };
            _employee2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "amahl@gmail.com",
                Password = "123",
                UserType = UserType.Employee.ToString(),
                Status = "Active"
            };

            _employeeAdmin1 = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "mark@gmail.com",
                FirstName = "mark",
                LastName = "lara",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
            };
            _employeeAdmin1.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "mark@gmail.com",
                Password = "123",
                UserType = UserType.Employee.ToString(),
                Status = "Active"
            };

            _employeeAdmin2 = new User()
            {
                EmployeeId = "1234",
                Address1 = "123 street",
                Address2 = "apt a",
                BirthDate = DateTime.Parse("1/5/1972"),
                City = "Austin",
                Email = "chris@gmail.com",
                FirstName = "chris",
                LastName = "chris",
                PhoneHome = "123.123.1234",
                PhoneMobile = "123.123.1234",
                State = "Tx",
                ZipCode = "12345",
                Company = _company,
            };
            _employeeAdmin2.UserLoginInfo = new UserLoginInfo
            {
                LoginName = "chris@gmail.com",
                Password = "123",
                UserType = UserType.Employee.ToString(),
                Status = "Active"
            };

            _repository.Save(_employee1);
            _repository.Save(_employee2);
            _repository.Save(_employeeAdmin1);
            _repository.Save(_employeeAdmin2);
        }

        private void CreateField()
        {
            _field1 = new Field
            {
                Name = "field1",
                Description = "SomeField1",
                Size = 22000,
                Abbreviation = "SFF"
            };

            _field2 = new Field
            {
                Name = "field2",
                Description = "SomeField2",
                Size = 120000,
                Abbreviation = "SOFF"
            };

            _repository.Save(_field1);
            _repository.Save(_field2);
        }

        private void CreateEventType()
        {
            var eventType1 = new EventType { Name = "some event" };
            var eventType2 = new EventType { Name = "some other event" };
            _repository.Save(eventType1);
            _repository.Save(eventType2);
        }

        private void CreateTask()
        {
            var taskType1 = new TaskType
            {
                Name = "Mow"
            };
            var taskType2 = new TaskType
            {
                Name = "Water"
            };
            _repository.Save(taskType1);
            _repository.Save(taskType2);
            _task1 = new Task
            {
                TaskType = taskType1,
                Field = _field1,
                ScheduledDate = DateTime.Parse("3/3/2011"),
                ScheduledStartTime = DateTime.Parse("3/3/2011 5:30 AM"),
                ScheduledEndTime = DateTime.Parse("3/3/2011 6:30 AM"),
                Notes = "Notes1",
                InventoryProduct = _invenotyMaterial1,
                QuantityNeeded = 4,
                UnitType = UnitType.Tons.ToString()
            };

            _task2 = new Task
            {
                TaskType = taskType2,
                Field=_field2,
                ScheduledDate = DateTime.Parse("3/3/2011"),
                ScheduledStartTime = DateTime.Parse("3/3/2011 6:30 AM"),
                ScheduledEndTime = DateTime.Parse("3/3/2011 7:30 AM"),
                Notes = "Notes2",
                InventoryProduct = _inventoryChemical2,
                QuantityNeeded = 4,
                UnitType = UnitType.Tons.ToString()
            };
            _task1.AddEmployee(_employee1);
            _task1.AddEmployee(_employee2);
            _task1.AddEquipment(_equip1);
            _task1.AddEquipment(_equip2);
            _task2.AddEmployee(_employee1);
            _task2.AddEmployee(_employee2);
            _task2.AddEquipment(_equip1);
            _task2.AddEquipment(_equip2);
            
            
            _repository.Save(_task1);
            _repository.Save(_task2);

        }

        private void CreateVendor()
        {
            _vendor1 = new Vendor
            {
                Company = "Some Company1",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.somewebsite1.com",
                LogoUrl = "someurl1",
                Notes = "notes1"
            };

            _vendor2 = new Vendor
            {
                Company = "Some Company2",
                Phone = "555.123.4567",
                Fax = "123.456.7891",
                Website = "www.somewebsite2.com",
                LogoUrl = "someurl2",
                Notes = "notes2"
            };

            _vendor1.AddProduct(_fertilizer1);
            _vendor1.AddProduct(_fertilizer2);
            _vendor2.AddProduct(_fertilizer1);
            _vendor2.AddProduct(_fertilizer2);

            _vendor1.AddProduct(_chemical1);
            _vendor1.AddProduct(_chemical2);
            _vendor2.AddProduct(_chemical1);
            _vendor2.AddProduct(_chemical2);

            _vendor1.AddProduct(_materials1);
            _vendor1.AddProduct(_materials2);
            _vendor2.AddProduct(_materials1);
            _vendor2.AddProduct(_materials2);


            _repository.Save(_vendor1);
            _repository.Save(_vendor2);

            var purchaseOrder1 = new PurchaseOrder {Vendor = _vendor1, DateCreated = DateTime.Parse("1/5/2009")};
            var poli1 = new PurchaseOrderLineItem()
            {
                Product = _fertilizer1,
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = true
            };

            var poli2 = new PurchaseOrderLineItem()
            {
                Product = _fertilizer1,
                Price = 10,
                UnitType = UnitType.Bags.ToString(),
                QuantityOrdered = 5,
                SizeOfUnit = 5,
                Taxable = true
            };

            var poli3 = new PurchaseOrderLineItem()
            {
                Product = _materials1,
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = true
            };

            var poli4 = new PurchaseOrderLineItem()
            {
                Product = _materials2,
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = true
            };

            var poli5 = new PurchaseOrderLineItem()
            {
                Product = _chemical2,
                Price = 10,
                QuantityOrdered = 5,
                UnitType = UnitType.Bags.ToString(),
                SizeOfUnit = 5,
                Taxable = true
            };

            var poli6 = new PurchaseOrderLineItem()
            {
                Product = _chemical1,
                Price = 10,
                UnitType = UnitType.Bags.ToString(),
                QuantityOrdered = 5,
                SizeOfUnit = 5,
                Taxable = true
            };
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli1);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli2);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli3);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli4);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli5);
            _purchaseOrderLineItemService.AddNewItem(ref purchaseOrder1, poli6);
            _vendor1.AddPurchaseOrder(purchaseOrder1);
            _repository.Save(_vendor1);

        }

        private void CreateVendorContact()
        {
            _contact1v1 = new VendorContact
                                 {
                                     Address2 = "4600 Guadalupe St",
                                     Address1 = "B113",
                                     City = "Austin",
                                     Email = "amahl@gmail.com",
                                     FirstName = "Amahl",
                                     LastName = "Harik",
                                     Phone = "512.228.6069",
                                     Fax = "512.228.60690",
                                     State = "RI",
                                     Status = "Active",
                                 };
            _contact1v2 = new VendorContact
                             {
                                 Address2 = "4600 Guadalupe St",
                                 Address1 = "B113",
                                 City = "Austin",
                                 Email = "amahl@gmail.com",
                                 FirstName = "Amahl",
                                 LastName = "Harik",
                                 Phone = "512.228.6069",
                                 Fax = "512.228.60690",
                                 State = "RI",
                                 Status = "Active",
                             };
            _contact2v1 = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "reharik@gmail.com",
                FirstName = "Raif",
                LastName = "Harik",
                Phone = "512.228.6069",
                Fax = "512.228.60690",
                State = "Tx",
                Status = "Active",
            };
            _contact2v2 = new VendorContact
            {
                Address2 = "4600 Guadalupe St",
                Address1 = "B113",
                City = "Austin",
                Email = "reharik@gmail.com",
                FirstName = "Raif",
                LastName = "Harik",
                Phone = "512.228.6069",
                Fax = "512.228.60690",
                State = "Tx",
                Status = "Active",
            };
            _vendor1.AddContact(_contact1v1);
            _vendor1.AddContact(_contact2v1);
            _vendor2.AddContact(_contact1v2);
            _vendor2.AddContact(_contact2v2);
            
            _repository.Save(_contact1v1);
            _repository.Save(_contact2v1);
            _repository.Save(_contact1v2);
            _repository.Save(_contact2v2);
            _repository.Save(_vendor1);
            _repository.Save(_vendor2);
        }

        public void CreateInventory()
        {

            _inventoryChemical1 = new InventoryProduct() { Product = _chemical1, Quantity = 10, UnitType = UnitType.Tons.Key, SizeOfUnit = 10};
            _inventoryChemical2 = new InventoryProduct() { Product = _chemical2, Quantity = 10, UnitType = UnitType.Tons.Key, SizeOfUnit = 10 };
            _inventoryFertilizer1 = new InventoryProduct() { Product = _fertilizer1, Quantity = 10, UnitType = UnitType.Tons.Key, SizeOfUnit = 10 };
            _inventoryFertilizer2 = new InventoryProduct() { Product = _fertilizer2, Quantity = 10, UnitType = UnitType.Tons.Key, SizeOfUnit = 10 };
            _invenotyMaterial1 = new InventoryProduct() { Product = _materials1, Quantity = 10, UnitType = UnitType.Tons.Key, SizeOfUnit = 10 };
            _inventoryMaterial2 = new InventoryProduct() { Product = _materials2, Quantity = 10, UnitType = UnitType.Tons.Key, SizeOfUnit = 10 };
            _repository.Save(_inventoryChemical1);
            _repository.Save(_inventoryChemical2);
            _repository.Save(_inventoryFertilizer1);
            _repository.Save(_inventoryFertilizer2);
            _repository.Save(_invenotyMaterial1);
            _repository.Save(_inventoryMaterial2);
        }

        private void CreateMaterials()
        {
            _materials1 = new Material
            {
                Name = "Kryptonite", 
            };

            _materials2 = new Material
            {
                Name = "FoolsGold", 
            };

            _repository.Save(_materials1);
            _repository.Save(_materials2);
        }

        private void CreateFertilizer()
        {
            _fertilizer1 = new Fertilizer
            {
                Name = "cow poop",
                N = 10,
                P = 10,
                K = 10
            };

            _fertilizer2 = new Fertilizer
            {
                Name = "Chicken poop",
                N = 10,
                P = 10,
                K = 10
            };

            _repository.Save(_fertilizer1);
            _repository.Save(_fertilizer2);
        }

        private void CreateChemical()
        {
            _chemical1 = new Chemical()
            {
                Name = "Lsd",
            };

            _chemical2 = new Chemical()
            {
                Name = "PCP",
            };

            _repository.Save(_chemical1);
            _repository.Save(_chemical2);
        }



    }
}
