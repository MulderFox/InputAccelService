using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace InputAccelService
{
    public class InputAccelService : IInputAccelService
    {
        public ModuleLicense[] GetAllModuleLicenses()
        {
            var acEnterpriseAdmin = new ACEnterpriseAdmin();
            var allModuleLicenses = acEnterpriseAdmin.GetAllModuleLicenses();

            return (from Emc.InputAccel.Management.Administration.ModuleLicense moduleLicense in allModuleLicenses
                    select new ModuleLicense
                               {
                                   Executable = moduleLicense.Executable,
                                   Used = moduleLicense.CopiesUsed,
                                   Available = moduleLicense.CopiesAvail,
                                   PercentCopiesUsed = moduleLicense.CopiesUsedPercentage,
                                   PagesUsed = moduleLicense.PagesUsed,
                                   PagesAvailable = moduleLicense.PagesAvail,
                                   PercentPagesUsed = moduleLicense.PagesUsedPercentage
                               }).ToArray();
        }

        public ModuleLicense[] GetModuleLicenses(int[] serverId)
        {
            var acEnterpriseAdmin = new ACEnterpriseAdmin();
            var modulesByServerEx = acEnterpriseAdmin.GetModulesByServerEx(serverId);

            return (from Emc.InputAccel.Management.Administration.ModuleLicense moduleLicense in modulesByServerEx
                    select new ModuleLicense
                    {
                        Executable = moduleLicense.Executable,
                        Used = moduleLicense.CopiesUsed,
                        Available = moduleLicense.CopiesAvail,
                        PercentCopiesUsed = moduleLicense.CopiesUsedPercentage,
                        PagesUsed = moduleLicense.PagesUsed,
                        PagesAvailable = moduleLicense.PagesAvail,
                        PercentPagesUsed = moduleLicense.PagesUsedPercentage
                    }).ToArray();
        }

        public LicenseCode[] GetAllLicenseCodes()
        {
            var acEnterpriseAdmin = new ACEnterpriseAdmin();
            var allLicenseCodes = acEnterpriseAdmin.GetAllLicenseCodes();

            return (from Emc.InputAccel.Management.Administration.LicenseCode licenseCode in allLicenseCodes
                    select new LicenseCode
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
                               }).ToArray();
        }

        public LicenseCode[] GetLicenseCodes(int[] serverId)
        {
            var acEnterpriseAdmin = new ACEnterpriseAdmin();
            var licenseCodeEx = acEnterpriseAdmin.GetLicenseCodeEx(serverId);

            return (from Emc.InputAccel.Management.Administration.LicenseCode licenseCode in licenseCodeEx
                    select new LicenseCode
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
                    }).ToArray();
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
