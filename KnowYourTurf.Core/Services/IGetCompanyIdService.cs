﻿using System;
using System.Web;
using KnowYourTurf.Core.Config;

namespace KnowYourTurf.Core.Services
{
    public interface IGetCompanyIdService
    {
        int Execute();
    }


    public class GetCompanyIdService : IGetCompanyIdService
    {
        public int Execute()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext!=null ? httpContext.User as CustomPrincipal:null;
            return customPrincipal !=null ? customPrincipal.CompanyId:0;
        }
    }

    public class DataLoaderGetCompanyIdService:IGetCompanyIdService
    {
        public int Execute()
        {
            return 1;
        }
    }
}