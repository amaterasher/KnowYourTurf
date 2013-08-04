namespace DBFluentMigration.Iteration_2
{
    using KnowYourTurf.Web.Security;

    public class UpdateOperations_2001 : IUpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations_2001(IOperations operations)
        {
            this._operations = operations;
        }

        public void Update()
        {
            this.CreateMenuItemOptions();
            this.CreateControllerOptions();
            CreateOperations();
        }

        private void CreateControllerOptions()
        {
            this._operations.CreateOperationForControllerType("TDAController");
            this._operations.CreateOperationForControllerType("SiteController");
            this._operations.CreateOperationForControllerType("SiteListController");
            this._operations.CreateOperationForControllerType("GrassTypeController");
        }
        public void CreateOperations()
        {
            this._operations.CreateOperation("/EditPastTask");
        }
        public void CreateMenuItemOptions()
        {
            this._operations.CreateOperationForMenuItem("TDA");
            this._operations.CreateOperationForMenuItem("Sites");
        }
    }
}