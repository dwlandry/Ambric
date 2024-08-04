using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using Instrument_Spec_Manager.Module.BusinessObjects;
using Instrument_Spec_Manager.Module.BusinessObjects.DataStructures;
using Instrument_Spec_Manager.Module.BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.DatabaseUpdate.Initializers
{
    public class OrganizationInitializer
    {
        private readonly IObjectSpace _objectSpace;

        public OrganizationInitializer(IObjectSpace objectSpace)
        {
            _objectSpace = objectSpace;
        }

        public void Initialize()
        {
            InitializeDepartments();
            InitializePositions();
            InitializeEmployees();
        }

        private void InitializeDepartments()
        {
            foreach (DepartmentEnum dept in Enum.GetValues(typeof(DepartmentEnum)))
            {
                CreateDepartmentIfNotExists(dept.ToString(), "Concord");
            }
        }

        private void InitializePositions()
        {
            foreach (PositionEnum pos in Enum.GetValues(typeof(PositionEnum)))
            {
                CreatePositionIfNotExists(pos.ToString());
            }
        }

        private void InitializeEmployees()
        {
            var employeeData = new List<EmployeeInitializationData>
            {
                new EmployeeInitializationData { FirstName = "Mary", LastName = "Tellitson", Birthday = new DateTime(1980, 11, 27), TitleOfCourtesy = TitleOfCourtesy.Mrs, Department = DepartmentEnum.Engineering, Position = PositionEnum.Manager, ManagerName = null },
                new EmployeeInitializationData { FirstName = "John", LastName = "Doe", Birthday = new DateTime(1975, 5, 15), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Engineer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Jane", LastName = "Smith", Birthday = new DateTime(1990, 8, 22), TitleOfCourtesy = TitleOfCourtesy.Ms, Department = DepartmentEnum.HumanResources, Position = PositionEnum.Manager, ManagerName = null },
                new EmployeeInitializationData { FirstName = "Robert", LastName = "Johnson", Birthday = new DateTime(1985, 3, 12), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Engineer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Emily", LastName = "Chen", Birthday = new DateTime(1992, 7, 8), TitleOfCourtesy = TitleOfCourtesy.Ms, Department = DepartmentEnum.Design, Position = PositionEnum.Designer, ManagerName = "Ava Thompson" },
                new EmployeeInitializationData { FirstName = "Michael", LastName = "O'Connor", Birthday = new DateTime(1978, 11, 30), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.SafetyOfficer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Sophia", LastName = "Garcia", Birthday = new DateTime(1988, 4, 17), TitleOfCourtesy = TitleOfCourtesy.Mrs, Department = DepartmentEnum.HumanResources, Position = PositionEnum.Manager, ManagerName = "Jane Smith" },
                new EmployeeInitializationData { FirstName = "William", LastName = "Nguyen", Birthday = new DateTime(1983, 9, 5), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Programmer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Olivia", LastName = "Patel", Birthday = new DateTime(1995, 1, 23), TitleOfCourtesy = TitleOfCourtesy.Ms, Department = DepartmentEnum.Design, Position = PositionEnum.Designer, ManagerName = "Ava Thompson" },
                new EmployeeInitializationData { FirstName = "James", LastName = "Kim", Birthday = new DateTime(1976, 6, 14), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Engineer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Emma", LastName = "Anderson", Birthday = new DateTime(1991, 10, 9), TitleOfCourtesy = TitleOfCourtesy.Ms, Department = DepartmentEnum.Engineering, Position = PositionEnum.SafetyOfficer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "David", LastName = "Martinez", Birthday = new DateTime(1982, 2, 28), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Programmer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Ava", LastName = "Thompson", Birthday = new DateTime(1987, 12, 3), TitleOfCourtesy = TitleOfCourtesy.Mrs, Department = DepartmentEnum.Design, Position = PositionEnum.Manager, ManagerName = null },
                new EmployeeInitializationData { FirstName = "Daniel", LastName = "Lee", Birthday = new DateTime(1993, 8, 19), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Engineer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Isabella", LastName = "Brown", Birthday = new DateTime(1989, 5, 7), TitleOfCourtesy = TitleOfCourtesy.Ms, Department = DepartmentEnum.Design, Position = PositionEnum.Designer, ManagerName = "Ava Thompson" },
                new EmployeeInitializationData { FirstName = "Alexander", LastName = "Wilson", Birthday = new DateTime(1981, 11, 11), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.SafetyOfficer, ManagerName = "Mary Tellitson" },
                new EmployeeInitializationData { FirstName = "Mia", LastName = "Taylor", Birthday = new DateTime(1994, 3, 25), TitleOfCourtesy = TitleOfCourtesy.Ms, Department = DepartmentEnum.HumanResources, Position = PositionEnum.Manager, ManagerName = "Jane Smith" },
                new EmployeeInitializationData { FirstName = "Ethan", LastName = "Davis", Birthday = new DateTime(1986, 7, 2), TitleOfCourtesy = TitleOfCourtesy.Mr, Department = DepartmentEnum.Engineering, Position = PositionEnum.Programmer, ManagerName = "Mary Tellitson" }

            };

            // First, create all employees
            foreach (var data in employeeData)
            {
                CreateEmployeeIfNotExists(data);
            }

            // Then, assign managers
            foreach (var data in employeeData)
            {
                AssignManagerToEmployee(data);
            }
        }

        private void CreateDepartmentIfNotExists(string title, string office)
        {
            if (_objectSpace.FindObject<Department>(CriteriaOperator.Parse("Title = ?", title)) == null)
            {
                var department = _objectSpace.CreateObject<Department>();
                department.Title = title;
                department.Office = office;
            }
        }

        private void CreatePositionIfNotExists(string title)
        {
            if (_objectSpace.FindObject<Position>(CriteriaOperator.Parse("Title = ?", title)) == null)
            {
                var position = _objectSpace.CreateObject<Position>();
                position.Title = title;
            }
        }

        private void CreateEmployeeIfNotExists(EmployeeInitializationData data)
        {
            if (_objectSpace.FindObject<Employee>(CriteriaOperator.Parse("FirstName = ? AND LastName = ?", data.FirstName, data.LastName)) == null)
            {
                var employee = _objectSpace.CreateObject<Employee>();
                employee.FirstName = data.FirstName;
                employee.LastName = data.LastName;
                employee.Birthday = data.Birthday;
                employee.TitleOfCourtesy = data.TitleOfCourtesy;
                employee.Department = _objectSpace.FindObject<Department>(CriteriaOperator.Parse("Title = ?", data.Department.ToString()));
                employee.Position = _objectSpace.FindObject<Position>(CriteriaOperator.Parse("Title = ?", data.Position.ToString()));

                if (employee.Department == null || employee.Position == null)
                {
                    throw new InvalidOperationException($"Department or Position not found for employee {data.FirstName} {data.LastName}");
                }
            }
        }

        private void AssignManagerToEmployee(EmployeeInitializationData data)
        {
            var employee = _objectSpace.FindObject<Employee>(CriteriaOperator.Parse("FirstName = ? AND LastName = ?", data.FirstName, data.LastName));
            if (employee != null && !string.IsNullOrEmpty(data.ManagerName))
            {
                var managerNames = data.ManagerName.Split(' ');
                var manager = _objectSpace.FindObject<Employee>(CriteriaOperator.Parse("FirstName = ? AND LastName = ?", managerNames[0], managerNames[1]));
                if (manager != null)
                {
                    employee.Manager = manager;
                }
                else
                {
                    throw new InvalidOperationException($"Manager {data.ManagerName} not found for employee {data.FirstName} {data.LastName}");
                }
            }
        }

    }
}
