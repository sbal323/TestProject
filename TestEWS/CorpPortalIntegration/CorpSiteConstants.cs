using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEWS.CorpPortalIntegration
{
    public class CorpSiteConstants
    {
        public const string CorpSiteUrl = "http://corp.lanteria.com/";
        public class ListNames
        {
            public const string Licenses = "Licenses";
        }
        public class LicensesFieldNames
        {
            public const string LicenseName = "Title";
            public const string SiteGuid = "SiteGUID";
            public const string Employees = "Employees";
            public const string License = "License";
            public const string Issued = "Installed";
            public const string Active = "Active";
            public const string Product = "Product";
            public const string ExpirationDate = "ExpirationDate";
            public const string LicenseScope = "LicenseScope";
            public const string Modules = "Modules";
            public const string Country = "Country";
            public const string RequestedBy = "RequestedBy";
            public const string ErpClientId = "ErpClientId";
        }
    }
}
