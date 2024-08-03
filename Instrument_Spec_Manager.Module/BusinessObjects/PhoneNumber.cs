using DevExpress.Persistent.BaseImpl.EF;
using System.ComponentModel;

namespace Instrument_Spec_Manager.Module.BusinessObjects
{
    [DefaultProperty(nameof(Number))]
    public class PhoneNumber : BaseObject
    {
        public virtual string Number { get; set; }
        public virtual string PhoneType { get; set; }

        public override string ToString()
        {
            return Number;
        }
    }
}
