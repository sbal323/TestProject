using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEWS.CorpPortalIntegration;
namespace TestEWS.Tests
{
    class CorpSiteIntegrationTest : ITest
    {
        string ITest.Title
        {
            get { return "Corp Portal Integration Test"; }
        }

        void ITest.Run()
        {
            CorpPortalIntegration.CorpPortalService service = new CorpPortalIntegration.CorpPortalService();
            List<CorpPortalIntegration.License> result = service.GetClientActiveLicenses(34).ToList();
            Console.WriteLine($"There are {result.Count} active license(s).");
            foreach (CorpPortalIntegration.License lic in result)
            {
                Console.WriteLine(lic.ToString());
            }
            CorpPortalIntegration.License newLicense = result[0];
            newLicense.Name = "TEST INTEGRATION";
            newLicense.Type = CorpPortalIntegration.LicenseType.Perpetual;
            newLicense.Country = "Ukraine";
            newLicense.EmployeesCount = 1000;
            newLicense.ExpirationDate = DateTime.Today.AddMonths(1);
            newLicense.LicenseScope = LicenseScopeType.SiteCollection;
            List<Module> newModules= newLicense.Modules.ToList();
            newModules[0].IsActive = true;
            newModules[1].IsActive = false;
            newLicense.Modules = newModules;
            newLicense.RequestedBy = "Sergey Balog";
            //service.CreateLicense(111, newLicense);
            //service.DeactivateLicense(64, "6dcf9aed-9c94-4a56-ac01-469aa4c4c505");
        }
    }
}