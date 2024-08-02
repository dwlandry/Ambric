using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects;

public class Employee : BaseObject
{
    public virtual String FirstName { get; set; }
    public virtual String LastName { get; set; }
    public virtual String MiddleName { get; set; }
}
