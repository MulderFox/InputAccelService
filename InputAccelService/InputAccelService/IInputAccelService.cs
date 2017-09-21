using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Emc.InputAccel.Management;

namespace InputAccelService
{
    [ServiceContract]
    public interface IInputAccelService
    {
        [OperationContract]
        ModuleLicense[] GetAllModuleLicenses();

        [OperationContract]
        ModuleLicense[] GetModuleLicenses(int[] serverId);

        [OperationContract]
        LicenseCode[] GetAllLicenseCodes();

        [OperationContract]
        LicenseCode[] GetLicenseCodes(int[] serverId);
    }

    [DataContract]
    public class ModuleLicense
    {
        [DataMember]
        public string Executable { get; set; }

        [DataMember]
        public string Used { get; set; }

        [DataMember]
        public string Available { get; set; }

        [DataMember]
        public int PercentCopiesUsed { get; set; }

        [DataMember]
        public string PagesUsed { get; set; }

        [DataMember]
        public string PagesAvailable { get; set; }

        [DataMember]
        public int PercentPagesUsed { get; set; }
    }

    [DataContract]
    public class LicenseCode
    {
        [DataMember]
        public int ServerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Connections { get; set; }

        [DataMember]
        public string Pages { get; set; }

        [DataMember]
        public DateTime? ValidUntil { get; set; }

        [DataMember]
        public string LicenseCodeValue { get; set; }

        [DataMember]
        public string Server { get; set; }

        [DataMember]
        public LicenseCodeStatus Status { get; set; }

        [DataMember]
        public string Features { get; set; }
    }
}
