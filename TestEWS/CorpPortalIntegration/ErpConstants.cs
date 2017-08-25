using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEWS.CorpPortalIntegration
{
    public class ErpConstants
    {
        public const string ErpUrl = "http://corp.lanteria.com/lanteriaerp/_layouts/LanteriaERP_HttpHandlers/CustomerJSONData.ashx";
        public class CommandNames
        {
            public const string ClientsList = "list";
            public const string LicenseNumber = "licensenumber";
        }
        public class ParameterNames
        {
            public const string Command = "cmd";
            public const string CustomerId = "customerId";
        }
    }
}
