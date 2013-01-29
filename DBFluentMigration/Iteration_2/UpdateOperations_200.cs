﻿using KnowYourTurf.Web.Security;

namespace DBFluentMigration.Iteration_2
{
    public class UpdateOperations_200 : IUpdateOperations
    {
        private readonly IOperations _operations;

        public UpdateOperations_200(IOperations operations)
        {
            _operations = operations;
        }

        public void Update()
        {
            CreateMenuItemOptions();
        }

        public void CreateMenuItemOptions()
        {
            _operations.CreateOperationForMenuItem("Site1");
            _operations.CreateOperationForMenuItem("Site2");
            _operations.CreateOperationForMenuItem("Site3");
            _operations.CreateOperationForMenuItem("Site4");
            _operations.CreateOperationForMenuItem("Site5");
            _operations.CreateOperationForMenuItem("Site6");
            _operations.CreateOperationForMenuItem("Site7");
            _operations.CreateOperationForMenuItem("Site8");
            _operations.CreateOperationForMenuItem("Site9");
            _operations.CreateOperationForMenuItem("Site10");
        }
    }
}