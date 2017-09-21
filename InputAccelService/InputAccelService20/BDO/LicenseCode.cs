using Emc.InputAccel.Management;
using System;

namespace InputAccelService20.BDO
{
    public class LicenseCode
    {
        public int ServerId;
        public string Name;
        public string Connections;
        public string Pages;
        public DateTime? ValidUntil;
        public string LicenseCodeValue;
        public string Server;
        public LicenseCodeStatus Status;
        public string Features;
    }
}