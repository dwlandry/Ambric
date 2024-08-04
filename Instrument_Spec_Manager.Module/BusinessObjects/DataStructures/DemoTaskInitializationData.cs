using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects.DataStructures
{
    public class DemoTaskInitializationData
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public List<string> AssignedEmployeeNames { get; set; } // List of "FirstName LastName"
    }
}
