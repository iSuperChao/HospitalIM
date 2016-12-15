using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [Serializable]
    public class PackageBase
    {
        public PackageBase() { }

        public PackageBase(EPackageHead headCode)
        {
            this._headCode = headCode;
        }

        private EPackageHead _headCode = EPackageHead.Normal;
        public EPackageHead HeadCode
        {
            get { return this._headCode; }
            set { this._headCode = value; }
        }

        public int PackageId { get; set; }
        public int SendId { get; set; }
        public int RecieveId { get; set; }

        public int PackageTypeId { get; set; }
        public int PackageState { get; set; }
        public DateTime PackageTime { get; set; }
    }
}
