using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.CorpPortalIntegration
{
    public class Module
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
    public enum LicenseType
    {
        Trial = 0,
        Perpetual = 1
    }
    public class License
    {
        public string Country { get; set; }
        public int EmployeesCount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public LicenseScopeType LicenseScope { get; set; }
        public LicenseType Type { get; set; }
        public IEnumerable<Module> Modules { get; set; }
        public string Name { get; set; }
        public string ProductVersion { get; set; }
        public string RequestedBy { get; set; }
        public string SiteGuid { get; set; }

        public override string ToString()
        {
            return $"{Name}\t{ProductVersion}\t{SiteGuid}\t{LicenseScope}\t{EmployeesCount}";
        }
    }
    public enum LicenseScopeType
    {
        WebSite = 1,
        SiteCollection = 2,
    }
    public class CorpPortalService 
    {
        private string siteUser;
        private string siteUserDomain;
        private string siteUserPassword;
        public CorpPortalService()
        {
            //TODO: read form settings
            siteUser = "";
            siteUserDomain = "";
            siteUserPassword = "";
        }
        private string GetLicenseScopeString(LicenseScopeType licenseScope)
        {
            switch (licenseScope)
            {
                case LicenseScopeType.WebSite:
                    return "1. Web Site";
                case LicenseScopeType.SiteCollection:
                    return "2. Site Collection";
                default:
                    return "1. Web Site";
            }
        }
        private LicenseScopeType GetLicenseScopeFromString(string licenseScope)
        {
            switch (licenseScope)
            {
                case "1. Web Site":
                    return LicenseScopeType.WebSite;
                case "2. Site Collection":
                    return LicenseScopeType.SiteCollection;
                default:
                    return LicenseScopeType.WebSite;
            }
        }
        public void CreateLicense(int? clientId, License license)
        {
            using (ClientContext context = new ClientContext(CorpSiteConstants.CorpSiteUrl))
            {
                context.Credentials = new System.Net.NetworkCredential(siteUser, siteUserPassword, siteUserDomain);
                Web webSite = context.Web;
                context.Load(webSite);
                List list = webSite.Lists.GetByTitle(CorpSiteConstants.ListNames.Licenses);
                ListItemCreationInformation itemCreationInfo = new ListItemCreationInformation();
                ListItem itm = list.AddItem(itemCreationInfo);
                context.ExecuteQuery();
                itm[CorpSiteConstants.LicensesFieldNames.LicenseName] = license.Name;
                itm[CorpSiteConstants.LicensesFieldNames.SiteGuid] = license.SiteGuid;
                itm[CorpSiteConstants.LicensesFieldNames.Employees] = license.EmployeesCount;
                itm[CorpSiteConstants.LicensesFieldNames.Issued] = DateTime.Now;
                itm[CorpSiteConstants.LicensesFieldNames.Active] = true;
                itm[CorpSiteConstants.LicensesFieldNames.Product] = license.ProductVersion;
                if (license.Type == LicenseType.Trial)
                {
                    itm[CorpSiteConstants.LicensesFieldNames.ExpirationDate] = license.ExpirationDate;
                }
                itm[CorpSiteConstants.LicensesFieldNames.LicenseScope] = GetLicenseScopeString(license.LicenseScope);
                itm[CorpSiteConstants.LicensesFieldNames.Modules] = license.Modules.Where(x => x.IsActive).Select(x => x.Name).ToArray<object>();
                itm[CorpSiteConstants.LicensesFieldNames.Country] = license.Country;
                itm[CorpSiteConstants.LicensesFieldNames.RequestedBy] = license.RequestedBy;
                itm[CorpSiteConstants.LicensesFieldNames.ErpClientId] = clientId;
                itm.Update();
                context.ExecuteQuery();
            }
        }
        public void DeactivateLicense(int clientId, string siteGuid)
        {
            using (ClientContext context = new ClientContext(CorpSiteConstants.CorpSiteUrl))
            {
                context.Credentials = new System.Net.NetworkCredential(siteUser, siteUserPassword, siteUserDomain);
                Web webSite = context.Web;
                context.Load(webSite);
                List list = webSite.Lists.GetByTitle(CorpSiteConstants.ListNames.Licenses);
                CamlQuery query = new CamlQuery();
                query.ViewXml = $"<View>" +
                                    $"<Query>" +
                                        $"<Where>" +
                                        $"<And>" +
                                            $"<And>" +
                                                $"<Eq><FieldRef Name='{CorpSiteConstants.LicensesFieldNames.ErpClientId}'/><Value Type='Text'>{clientId}</Value></Eq>" +
                                                $"<Eq><FieldRef Name='{CorpSiteConstants.LicensesFieldNames.SiteGuid}'/><Value Type='Text'>{siteGuid}</Value></Eq>" +
                                            $"</And>" +
                                            $"<Eq><FieldRef Name='{CorpSiteConstants.LicensesFieldNames.Active}'/><Value Type='Boolean'>1</Value></Eq>" +
                                        $"</And>" +
                                        $"</Where>" +
                                    $"</Query>" +
                                $"</View>";
                ListItemCollection items = list.GetItems(query);
                context.Load(items);
                context.ExecuteQuery();
                foreach (ListItem itm in items)
                {
                    itm[CorpSiteConstants.LicensesFieldNames.Active] = false;
                    itm.Update();
                    context.ExecuteQuery();
                }
            }
        }
        private void SetModules(License license, string[] selectedModules)
        {
            if(selectedModules == null)
            {
                selectedModules = new string[] { };
            }
            List<Module> modules = new List<Module>();
            foreach (string module in Constants.ModuleNames)
            {
                if (selectedModules.Contains(module))
                {
                    modules.Add(new Module { Name = module, IsActive = true });
                }
                else
                {
                    modules.Add(new Module { Name = module, IsActive = false });
                }
            }
            license.Modules = modules;
        }
        private string GetStringValue(object value)
        {
            if(null == value)
            {
                return string.Empty;
            }
            return value.ToString();
        }
        public IEnumerable<License> GetClientActiveLicenses(int clientId)
        {
            List<License> licenses = new List<License>();
            using (ClientContext context = new ClientContext(CorpSiteConstants.CorpSiteUrl))
            {
                context.Credentials = new System.Net.NetworkCredential(siteUser, siteUserPassword, siteUserDomain);
                Web webSite = context.Web;
                context.Load(webSite);
                List list = webSite.Lists.GetByTitle(CorpSiteConstants.ListNames.Licenses);
                CamlQuery query = new CamlQuery();
                query.ViewXml = $"<View>" +
                                    $"<Query>" +
                                        $"<Where>" +
                                        $"<And>" +
                                            $"<And>" +
                                                $"<Eq><FieldRef Name='{CorpSiteConstants.LicensesFieldNames.ErpClientId}'/><Value Type='Text'>{clientId}</Value></Eq>" +
                                                $"<Eq><FieldRef Name='{CorpSiteConstants.LicensesFieldNames.Active}'/><Value Type='Boolean'>1</Value></Eq>" +
                                            $"</And>" +
                                            $"<IsNull><FieldRef Name='{CorpSiteConstants.LicensesFieldNames.ExpirationDate}' /></IsNull>" +
                                        $"</And>" +
                                        $"</Where>" +
                                    $"</Query>" +
                                $"</View>";
                ListItemCollection items = list.GetItems(query);
                context.Load(items);
                context.ExecuteQuery();
                foreach (ListItem itm in items)
                {
                    License license = new License();
                    license.Name = GetStringValue(itm[CorpSiteConstants.LicensesFieldNames.LicenseName]);
                    license.SiteGuid = GetStringValue(itm[CorpSiteConstants.LicensesFieldNames.SiteGuid]);
                    license.EmployeesCount = int.Parse(itm[CorpSiteConstants.LicensesFieldNames.Employees].ToString());
                    itm[CorpSiteConstants.LicensesFieldNames.Active] = true;
                    license.ProductVersion = GetStringValue(itm[CorpSiteConstants.LicensesFieldNames.Product]);
                    license.LicenseScope = GetLicenseScopeFromString(GetStringValue(itm[CorpSiteConstants.LicensesFieldNames.LicenseScope]));
                    string[] selectedModules = itm[CorpSiteConstants.LicensesFieldNames.Modules] as string[];
                    SetModules(license, selectedModules);
                    license.Country = GetStringValue(itm[CorpSiteConstants.LicensesFieldNames.Country]);
                    license.RequestedBy = GetStringValue(itm[CorpSiteConstants.LicensesFieldNames.RequestedBy]);
                    licenses.Add(license);
                }
            }
            return licenses;
        }
    }
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }    
    public class ErpService 
    {
        private string siteUser;
        private string siteUserDomain;
        private string siteUserPassword;
        public ErpService()
        {
            siteUser = "";
            siteUserDomain = "";
            siteUserPassword = "";
        }
        public IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            using (WebClient client = new WebClient())
            {
                client.Credentials = new System.Net.NetworkCredential(siteUser, siteUserPassword, siteUserDomain);
                Byte[] pageData = client.DownloadData(
                                        FormatCommandUrl(
                                            new List<KeyValuePair<string, string>>()
                                                {
                                                    new KeyValuePair<string, string>(ErpConstants.ParameterNames.Command, ErpConstants.CommandNames.ClientsList)
                                                }));
                string commandResponse = Encoding.UTF8.GetString(pageData);
                clients = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<Client>>(commandResponse);
            }
            return clients;
        }
        private string FormatCommandUrl(List<KeyValuePair<string,string>> parameterValues)
        {
            var result = $"{ErpConstants.ErpUrl}?{string.Join("&",parameterValues.Select(kvp => string.Format("{0}={1}", kvp.Key, System.Web.HttpUtility.UrlEncode(kvp.Value))))}";
            return result;
        }
        public int GetEmployeesCount(int clientId)
        {
            var employeesCount = 0;
            using (WebClient client = new WebClient())
            {
                client.Credentials = new System.Net.NetworkCredential(siteUser, siteUserPassword, siteUserDomain);
                Byte[] pageData = client.DownloadData(
                                        FormatCommandUrl(
                                            new List<KeyValuePair<string, string>>()
                                                {
                                                    new KeyValuePair<string, string>(ErpConstants.ParameterNames.Command, ErpConstants.CommandNames.LicenseNumber),
                                                    new KeyValuePair<string, string>(ErpConstants.ParameterNames.CustomerId, clientId.ToString())
                                                }));
                employeesCount = int.Parse(Encoding.UTF8.GetString(pageData));
            }
            return employeesCount;
        }
    }
}
