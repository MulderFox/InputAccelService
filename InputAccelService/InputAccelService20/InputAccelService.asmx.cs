using Emc.InputAccel.Management.AdministrationConsole.Caching;
using InputAccelService20.BDO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services;

namespace InputAccelService20
{
    /// <summary>
    /// Summary description for InputAccelService
    /// </summary>
    [WebService(Namespace = "http://trask.cz/InputAccelService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class InputAccelService : WebService
    {
        [WebMethod]
        public ModuleLicense[] GetAllModuleLicenses()
        {
            var acEnterpriseAdmin = CacheHelper.EnAdmin;
            var allModuleLicenses = acEnterpriseAdmin.GetAllModuleLicenses();

            var moduleLicenses = new List<ModuleLicense>();
            foreach (Emc.InputAccel.Management.Administration.ModuleLicense moduleLicense in allModuleLicenses)
            {
                moduleLicenses.Add(new ModuleLicense
                                       {
                                           Executable = moduleLicense.Executable,
                                           Used = moduleLicense.CopiesUsed,
                                           Available = moduleLicense.CopiesAvail,
                                           PercentCopiesUsed = moduleLicense.CopiesUsedPercentage,
                                           PagesUsed = moduleLicense.PagesUsed,
                                           PagesAvailable = moduleLicense.PagesAvail,
                                           PercentPagesUsed = moduleLicense.PagesUsedPercentage,
                                           Features = moduleLicense.Features,
                                           Server = moduleLicense.Server
                                       });
            }

            return moduleLicenses.ToArray();
        }

        [WebMethod]
        public LicenseCode[] GetAllLicenseCodes()
        {
            var acEnterpriseAdmin = CacheHelper.EnAdmin;
            var allLicenseCodes = acEnterpriseAdmin.GetAllLicenseCodes();

            var licenseCodes = new List<LicenseCode>();
            foreach (Emc.InputAccel.Management.Administration.LicenseCode licenseCode in allLicenseCodes)
            {
                licenseCodes.Add(new LicenseCode
                                     {
                                         Connections = licenseCode.Connections,
                                         Features = licenseCode.Features,
                                         LicenseCodeValue = licenseCode.Code,
                                         Name = licenseCode.Name,
                                         Pages = licenseCode.Pages,
                                         Server = licenseCode.ServerName,
                                         ServerId = licenseCode.ServerID,
                                         Status = licenseCode.Status,
                                         ValidUntil = licenseCode.ValidUntil
                                     });
            }

            return licenseCodes.ToArray();
        }

        [WebMethod]
        public LicenseCode[] GetLicenseCodes(int[] serverId)
        {
            var acEnterpriseAdmin = CacheHelper.EnAdmin;
            var licenseCodeEx = acEnterpriseAdmin.GetLicenseCodeEx(serverId);

            var licenseCodes = new List<LicenseCode>();
            foreach (Emc.InputAccel.Management.Administration.LicenseCode licenseCode in licenseCodeEx)
            {
                licenseCodes.Add(new LicenseCode
                                     {
                                         Connections = licenseCode.Connections,
                                         Features = licenseCode.Features,
                                         LicenseCodeValue = licenseCode.Code,
                                         Name = licenseCode.Name,
                                         Pages = licenseCode.Pages,
                                         Server = licenseCode.ServerName,
                                         ServerId = licenseCode.ServerID,
                                         Status = licenseCode.Status,
                                         ValidUntil = licenseCode.ValidUntil
                                     });
            }

            return licenseCodes.ToArray();
        }

        public string EncryptStringForLocalMachine(string value)
        {
            var sAditionalEntropy = new byte[] { 0x67, 0x11, 7, 13, 0x3d, 0x37 };
            byte[] userBase64 = Encoding.UTF8.GetBytes(value);
            byte[] protectedUserBase64 = ProtectedData.Protect(userBase64, sAditionalEntropy,
                                                               DataProtectionScope.LocalMachine);
            return String.Format("!-{0}", Convert.ToBase64String(protectedUserBase64));
        }
    }
}