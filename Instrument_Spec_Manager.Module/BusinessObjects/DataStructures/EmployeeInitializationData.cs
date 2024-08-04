using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instrument_Spec_Manager.Module.BusinessObjects.Enums;

namespace Instrument_Spec_Manager.Module.BusinessObjects.DataStructures
{
    public class EmployeeInitializationData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public TitleOfCourtesy TitleOfCourtesy { get; set; }
        public DepartmentEnum Department { get; set; }
        public PositionEnum Position { get; set; }
        public string ManagerName { get; set; } // Format: "FirstName LastName"
    }
}
