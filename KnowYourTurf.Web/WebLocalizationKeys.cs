using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web
{
    public class WebLocalizationKeys: StringToken
    {
        protected WebLocalizationKeys(string key) : this(key, null)
        {
        }

        protected WebLocalizationKeys(string key, string default_EN_US_Text)
            : base(key, default_EN_US_Text)
        {
        }

        public static readonly StringToken LOGIN_KEY = new WebLocalizationKeys("LOG_IN", "Log In");
        public static readonly StringToken LOG_OUT = new WebLocalizationKeys("LOG_OUT", "Logout");
        public static readonly StringToken WELCOME = new WebLocalizationKeys("WELCOME", "Welcome ,");
        public static readonly StringToken INADMINMODE = new WebLocalizationKeys("INADMINMODE", "INADMINMODE");
        public static readonly StringToken USER_ROLES = new WebLocalizationKeys("USER_ROLES", "UserRoles");
        public static readonly StringToken SAVE = new WebLocalizationKeys("SAVE", "Save");
        public static readonly StringToken ACCOUNT_LOGIN = new WebLocalizationKeys("ACCOUNT_LOGIN", "Account LogIn");
        public static readonly StringToken INVALID_USERNAME_OR_PASSWORD = new WebLocalizationKeys("INVALID_USERNAME_OR_PASSWORD", "Invalid Username or Password");
        public static readonly StringToken PLEASE_ENTER_YOUR_USERNAME_AND_PASSWORD_BELOW_KEY = new WebLocalizationKeys("PLEASE_ENTER_YOUR_USERNAME_AND_PASSWORD_BELOW_KEY", "Please enter your Username and Password below");
        
        public static readonly StringToken HOME = new WebLocalizationKeys("HOME", "Home");
        public static readonly StringToken EMPLOYEES = new WebLocalizationKeys("EMPLOYEES", "Employees");
        public static readonly StringToken ADMINS = new WebLocalizationKeys("ADMINS", "Admins");
        public static readonly StringToken FACILITIES = new WebLocalizationKeys("FACILITIES", "Facilities");
        public static readonly StringToken VENDORS = new WebLocalizationKeys("VENDORS", "Vendors");
        public static readonly StringToken PRODUCTS = new WebLocalizationKeys("PRODUCTS", "Products");
        public static readonly StringToken INVENTORY = new WebLocalizationKeys("INVENTORY", "Inventory");
        public static readonly StringToken MATERIALS = new WebLocalizationKeys("MATERIALS", "Materials");
        public static readonly StringToken FERTILIZERS = new WebLocalizationKeys("FERTILIZERS", "Fertilizers");
        public static readonly StringToken CHEMICALS = new WebLocalizationKeys("CHEMICALS", "Chemicals");
        public static readonly StringToken FIELDS = new WebLocalizationKeys("FIELDS", "Fields");
        public static readonly StringToken EQUIPMENT = new WebLocalizationKeys("EQUIPMENT", "Equipment");
        public static readonly StringToken TASKS = new WebLocalizationKeys("TASKS", "Tasks");
        public static readonly StringToken TASK_LIST = new WebLocalizationKeys("TASK_LIST", "Task List");
        public static readonly StringToken TASK_CALENDAR = new WebLocalizationKeys("TASK_CALENDAR", "Task Calendar");
        public static readonly StringToken EVENTS = new WebLocalizationKeys("EVENTS", "Events");
        public static readonly StringToken PURCHASE_ORDERS = new WebLocalizationKeys("PURCHASE_ORDERS", "Purchase Orders");
        public static readonly StringToken ADMIN = new WebLocalizationKeys("ADMIN", "Admin");
        public static readonly StringToken CALCULATORS = new WebLocalizationKeys("CALCULATORS", "Calculators");
        public static readonly StringToken EMAIL_TEMPLATES = new WebLocalizationKeys("EMAIL_TEMPLATES", "Email Templates");
        public static readonly StringToken EMAIL_JOBS = new WebLocalizationKeys("EMAIL_JOBS", "Email Jobs");


        public static readonly StringToken EMPLOYEE_INFORMATION = new WebLocalizationKeys("EMPLOYEE_INFORMATION", "Employee Information");
        public static readonly StringToken NEW_EMPLOYEE = new WebLocalizationKeys("NEW_EMPLOYEE", "Add New Employee");

        public static readonly StringToken VENDOR_INFORMATION = new WebLocalizationKeys("VENDOR_INFORMATION", "Vendor Information");
        public static readonly StringToken NEW_VENDOR = new WebLocalizationKeys("NEW_VENDOR", "Add New Vendor");
        
        public static readonly StringToken VENDOR_CONTACT_INFORMATION = new WebLocalizationKeys("VENDOR_CONTACT_INFORMATION", "Vendor Contact Information");
        public static readonly StringToken NEW_VENDOR_CONTACT = new WebLocalizationKeys("NEW_VENDOR_CONTACT", "Add New Vendor Contact");

        public static readonly StringToken MATERIAL_INFORMATION = new WebLocalizationKeys("MATERIAL_INFORMATION", "Material Information");
        public static readonly StringToken NEW_MATERIAL = new WebLocalizationKeys("NEW_MATERIAL", "Add New Material");

        public static readonly StringToken FERTILIZER_INFORMATION = new WebLocalizationKeys("FERILIZER_INFORMATION", "Fertilizer Information");
        public static readonly StringToken NEW_FERTILIZER = new WebLocalizationKeys("NEW_FERTILIZER", "Add New Fertilizer");

        public static readonly StringToken CHEMICAL_INFORMATION = new WebLocalizationKeys("CHEMICAL_INFORMATION", "Chemical Information");
        public static readonly StringToken NEW_CHEMICAL = new WebLocalizationKeys("NEW_CHEMICAL", "Add New Chemical");

        public static readonly StringToken SEED_INFORMATION = new WebLocalizationKeys("SEED_INFORMATION", "Seed Information");
        public static readonly StringToken NEW_SEED = new WebLocalizationKeys("NEW_SEED", "Add New Seed");

        public static readonly StringToken FIELD_INFORMATION = new WebLocalizationKeys("FIELD_INFORMATION", "Field Information");
        public static readonly StringToken NEW_FIELD = new WebLocalizationKeys("NEW_FIELD", "Add New Field");

        public static readonly StringToken EQUIPMENT_INFORMATION = new WebLocalizationKeys("EQUIPMENT_INFORMATION", "Equipment Information");
        public static readonly StringToken NEW_EQUIPMENT = new WebLocalizationKeys("NEW_EQUIPMENT", "Add New Equipment");

        public static readonly StringToken TASK_INFORMATION = new WebLocalizationKeys("TASK_INFORMATION", "Task Information");
        public static readonly StringToken NEW_TASK = new WebLocalizationKeys("NEW_TASK", "Add New Task");
        public static readonly StringToken PENDING_TASKS = new WebLocalizationKeys("PENDING_TASKS", "Pending Tasks");
        public static readonly StringToken COMPLETED_TASKS = new WebLocalizationKeys("COMPLETED_TASKS", "Completed Tasks");

        public static readonly StringToken TASK_TYPE_INFORMATION = new WebLocalizationKeys("TASK_TYPE_INFORMATION", "Task Type Information");
        public static readonly StringToken NEW_TASK_TYPE = new WebLocalizationKeys("NEW_TASK_TYPE", "Add New Task Type");
        public static readonly StringToken TASK_TYPES = new WebLocalizationKeys("TASK_TYPES", "Task Types");

        public static readonly StringToken EVENT_TYPE_INFORMATION = new WebLocalizationKeys("EVENT_TYPE_INFORMATION", "Event Type Information");
        public static readonly StringToken NEW_EVENT_TYPE = new WebLocalizationKeys("NEW_EVENT_TYPE", "Add New Event Type");
        public static readonly StringToken EVENT_TYPES = new WebLocalizationKeys("EVENT_TYPES", "Event Types");

        public static readonly StringToken EVENT_INFORMATION = new WebLocalizationKeys("EVENT_INFORMATION", "Event Information");

        public static readonly StringToken PURCHASE_ORDER_INFORMATION = new WebLocalizationKeys("PURCHASE_ORDER_INFORMATION", "Purchase Order Information");
        public static readonly StringToken NEW_PURCHASE_ORDER = new WebLocalizationKeys("NEW_PURCHASE_ORDER", "Add New Purchase Order");

        public static readonly StringToken INVENTORY_CHEMICAL_INFORMATION = new WebLocalizationKeys("INVENTORY_CHEMICAL_INFORMATION", "Inventory Chemical Information");
        public static readonly StringToken INVENTORY_MATERIAL_INFORMATION = new WebLocalizationKeys("INVENTORY_MATERIAL_INFORMATION", "Inventory Material Information");
        public static readonly StringToken INVENTORY_FERTILIZER_INFORMATION = new WebLocalizationKeys("INVENTORY_FERTILIZER_INFORMATION", "Inventory Fertilizer Information");
        public static readonly StringToken INVENTORY_SEED_INFORMATION = new WebLocalizationKeys("INVENTORY_SEED_INFORMATION", "Inventory Seed Information");

        public static readonly StringToken PURCHASE_ORDER_LIST_ITEM_INFORMATION = new WebLocalizationKeys("PURCHASE_ORDER_LIST_ITEM_INFORMATION", "Purchase Order List Item");

        public static readonly StringToken EMAIL_TEMPLATE_INFORMATION = new WebLocalizationKeys("EMAIL_TEMPLATE_INFORMATION", "Email Template Information");
        public static readonly StringToken NEW_EMAIL_TEMPLATE = new WebLocalizationKeys("NEW_EMAIL_TEMPLATE", "Add New Email Template");
        public static readonly StringToken EMAIL_JOB_INFORMATION = new WebLocalizationKeys("EMAIL_JOB_INFORMATION", "Email Job Information");
        public static readonly StringToken NEW_EMAIL_JOB = new WebLocalizationKeys("NEW_EMAIL_JOB", "Add New Email Job");

        public static readonly StringToken FERTILIZER_NEEDED_DISPLAY = new WebLocalizationKeys("FERTILIZER_NEEDED_DISPLAY", "Fertilizer Needed");
        public static readonly StringToken FERTILIZER_NEEDED = new WebLocalizationKeys("FERTILIZER_NEEDED", "FertilizerNeeded");
        public static readonly StringToken SAND = new WebLocalizationKeys("SAND", "Sand");
        public static readonly StringToken OVERSEED_BAGS_NEEEDED_DISPLAY = new WebLocalizationKeys("OVERSEED_BAGS_NEEEDED_DISPLAY", "Overseed Bags Needed");
        public static readonly StringToken OVERSEED_BAGS_NEEEDED = new WebLocalizationKeys("OVERSEED_BAGS_NEEEDED", "OverseedBagsNeeded");
        public static readonly StringToken OVERSEED_RATE_NEEEDED_DISPLAY = new WebLocalizationKeys("OVERSEED_RATE_NEEEDED_DISPLAY", "Overseed Rate Needed");
        public static readonly StringToken OVERSEED_RATE_NEEEDED = new WebLocalizationKeys("OVERSEED_RATE_NEEEDED", "OverseedRateNeeded");



        public static readonly StringToken FIELD_REQUIRED = new WebLocalizationKeys("FIELD_REQUIRED", "'the {0} field is required.'");
        public static readonly StringToken DIGITS_REQUIRED = new WebLocalizationKeys("DIGITS_REQUIRED", "the {0} field requires a digit.");
        public static readonly StringToken VALID_RANGE_REQUIRED = new WebLocalizationKeys("VALID_RANGE_REQUIRED", "the {0} field must be between {1} and {2}.");
        public static readonly StringToken POWERED_BY_KEY = new WebLocalizationKeys("POWERED_BY_KEY", "Powered by ");

        public static readonly StringToken FIELD_NAME = new WebLocalizationKeys("FIELD_NAME", "Field Name");
        public static readonly StringToken EMPLOYEE_NAMES = new WebLocalizationKeys("EMPLOYEE_NAMES", "Employee Names");
        public static readonly StringToken PO_NUMBER = new WebLocalizationKeys("PO_NUMBER", "PO Number");
        public static readonly StringToken VENDOR_PRODUCTS = new WebLocalizationKeys("VENDOR_PRODUCTS", "Vendor Products");
        public static readonly StringToken PURCHASE_ORDER_LINE_ITEMS = new WebLocalizationKeys("PURCHASE_ORDER_LINE_ITEMS", "Purchase Order Line Items");
        public static readonly StringToken LINE_ITEMS = new WebLocalizationKeys("LINE_ITEMS", "Line Items");
        public static readonly StringToken COPY_TASK = new WebLocalizationKeys("COPY_TASK", "Duplicate Task");
        public static readonly StringToken EDIT_TASK = new WebLocalizationKeys("EDIT_TASK", "Edit Task");
        public static readonly StringToken COPY_EVENT = new WebLocalizationKeys("COPY_EVENT", "Copy Event");
        public static readonly StringToken EDIT_EVENT = new WebLocalizationKeys("EDIT_EVENT", "Edit Event");
        public static readonly StringToken CLOSE_THIS_PURCHASE_ORDER = new WebLocalizationKeys("CLOSE_THIS_PURCHASE_ORDER", "Close This Purchase Order");
        public static readonly StringToken ENUMERATIONS = new WebLocalizationKeys("ENUMERATIONS", "Enumerations");
        public static readonly StringToken UPLOAD_PICTURE = new WebLocalizationKeys("UPLOAD_PICTURE", "Upload Picture");
        public static readonly StringToken FILE_ALREADY_EXISTS = new WebLocalizationKeys("FILE_ALREADY_EXISTS", "File Already Exists");
        public static readonly StringToken DELETE = new WebLocalizationKeys("DELETE", "Delete");
        public static readonly StringToken CALCULATOR = new WebLocalizationKeys("CALCULATOR", "Calculator");
        public static readonly StringToken PRINT = new WebLocalizationKeys("PRINT", "Print");
        public static readonly StringToken DELETE_ITEM = new WebLocalizationKeys("DELETE_ITEM", "Delete this item!");
        public static readonly StringToken EDIT_ITEM = new WebLocalizationKeys("EDIT_ITEM", "Edit this item!");
        public static readonly StringToken DISPLAY_ITEM = new WebLocalizationKeys("DISPLAY_ITEM", "Display this item!");
        public static readonly StringToken ID = new WebLocalizationKeys("ID", "ID");
        public static readonly StringToken ADDRESS = new WebLocalizationKeys("ADDRESS", "Address");
        public static readonly StringToken FORUM = new WebLocalizationKeys("FORUM", "Forum");

        public static readonly StringToken WEATHER = new WebLocalizationKeys("WEATHER", "Weather");
        public static readonly StringToken WEATHER_INFORMATION = new WebLocalizationKeys("WEATHER_INFORMATION", "Weather Information");

        public static readonly StringToken VENDOR_CONTACTS = new WebLocalizationKeys("VENDOR_CONTACTS", "Vendor Contacts");
        public static readonly StringToken PREVIOUS_VENDOR = new WebLocalizationKeys("PREVIOUS_VENDOR", "Previous Vendor");

        public static readonly StringToken NEW_DOCUMENT_CATEGORY = new WebLocalizationKeys("NEW_DOCUMENT_CATEGORY", "Add New Document Category");
        public static readonly StringToken DOCUMENT_CATEGORY_INFORMATION = new WebLocalizationKeys("DOCUMENT_CATEGORY_INFORMATION", "Document Category Information");
        public static readonly StringToken DOCUMENT_CATEGORY = new WebLocalizationKeys("DOCUMENT_CATEGORY", "Document Category");
        public static readonly StringToken DOCUMENT_CATEGORIES = new WebLocalizationKeys("DOCUMENT_CATEGORIES", "Document Categories");
        public static readonly StringToken DOCUMENTS = new WebLocalizationKeys("DOCUMENTS", "Documents");
        public static readonly StringToken NEW_DOCUMENT = new WebLocalizationKeys("NEW_DOCUMENT", "Add New Document");
        public static readonly StringToken DOCUMENT_INFORMATION = new WebLocalizationKeys("DOCUMENT_INFORMATION", "Document Information");

        public static readonly StringToken NEW_PHOTO_CATEGORY = new WebLocalizationKeys("NEW_PHOTO_CATEGORY", "Add New Photo Category");
        public static readonly StringToken PHOTO_CATEGORY_INFORMATION = new WebLocalizationKeys("PHOTO_CATEGORY_INFORMATION", "Photo Category Information");
        public static readonly StringToken PHOTO_CATEGORY = new WebLocalizationKeys("PHOTO_CATEGORY", "Photo Category");
        public static readonly StringToken PHOTO_CATEGORIES = new WebLocalizationKeys("PHOTO_CATEGORIES", "Photo Categories");
        public static readonly StringToken PHOTOS = new WebLocalizationKeys("PHOTOS", "Photos");
        public static readonly StringToken NEW_PHOTO = new WebLocalizationKeys("NEW_PHOTO", "Add New Photo");
        public static readonly StringToken PHOTO_INFORMATION = new WebLocalizationKeys("PHOTO_INFORMATION", "Photo Information");
        public static readonly StringToken ADD_ITEM_TO_PO = new WebLocalizationKeys("ADD_ITEM_TO_PO", "Add item to the purchase order");
         public static readonly StringToken VIEW_FILE = new WebLocalizationKeys("VIEW_FILE", "View File");
         public static readonly StringToken ADMINISTRATOR_INFORMATION = new WebLocalizationKeys("ADMINISTRATOR_INFORMATION", "Administrator Information");
         public static readonly StringToken ADD_NEW_ADMINISTRATOR = new WebLocalizationKeys("ADD_NEW_ADMINISTRATOR", "Add New Administrator");


         public static readonly StringToken COMMIT = new WebLocalizationKeys("COMMIT", "Commit");
         public static readonly StringToken RETURN = new WebLocalizationKeys("RETURN", "Return");
         public static readonly StringToken SELECT_COLOR_TO_ASSOCIATE_WITH_THIS_ITEM = new WebLocalizationKeys("SELECT_COLOR_TO_ASSOCIATE_WITH_THIS_ITEM", "Select A Color To Associate With This Field");

         public static readonly StringToken NEW_FACILITIES = new WebLocalizationKeys("NEW_FACILITIES", "New Facilities");
    
    
    
    }
}   