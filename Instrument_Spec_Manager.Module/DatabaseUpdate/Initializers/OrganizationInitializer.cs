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
            InitializeDemoTasks();
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

        private void InitializeDemoTasks()
        {
            var taskData = new List<DemoTaskInitializationData>
            {
                new DemoTaskInitializationData {
                    Subject = "Design pressure sensor prototype",
                    Description = "Create initial design for a high-accuracy pressure sensor for industrial applications",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "John Doe", "Robert Johnson" }
                },
                new DemoTaskInitializationData {
                    Subject = "Develop control algorithm for flow regulation",
                    Description = "Implement PID control algorithm for precise flow regulation in chemical processing systems",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Robert Johnson", "William Nguyen" }
                },
                new DemoTaskInitializationData {
                    Subject = "Create PCB layout for temperature controller",
                    Description = "Design PCB layout for a new temperature controller with improved thermal management",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "Emily Chen", "Daniel Lee" }
                },
                new DemoTaskInitializationData {
                    Subject = "Conduct EMC testing on new product line",
                    Description = "Perform electromagnetic compatibility testing on the new series of industrial controllers",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Alexander Wilson", "Emma Anderson" }
                },
                new DemoTaskInitializationData {
                    Subject = "Optimize power consumption in wireless sensors",
                    Description = "Research and implement power-saving techniques for battery-operated wireless sensor nodes",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "William Nguyen", "Ethan Davis" }
                },
                new DemoTaskInitializationData {
                    Subject = "Develop HMI interface for process monitoring",
                    Description = "Design user-friendly HMI screens for real-time monitoring of manufacturing processes",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Olivia Patel", "Isabella Brown" }
                },
                new DemoTaskInitializationData {
                    Subject = "Integrate HART protocol support in firmware",
                    Description = "Add HART communication protocol support to existing instrument firmware",
                    Priority = Priority.Low,
                    AssignedEmployeeNames = new List<string> { "Ethan Davis", "David Martinez" }
                },
                new DemoTaskInitializationData {
                    Subject = "Conduct field testing of new flow meters",
                    Description = "Organize and execute field trials for recently developed ultrasonic flow meters",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "Alexander Wilson", "James Kim" }
                },
                new DemoTaskInitializationData {
                    Subject = "Update safety protocols for chemical plant installations",
                    Description = "Review and revise safety procedures for installing our instruments in hazardous environments",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "Michael O'Connor", "Emma Anderson" }
                },
                new DemoTaskInitializationData {
                    Subject = "Develop calibration software for pressure transmitters",
                    Description = "Create a user-friendly calibration software for our new line of pressure transmitters",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "David Martinez", "William Nguyen" }
                },
                new DemoTaskInitializationData {
                    Subject = "Design new enclosure for outdoor sensors",
                    Description = "Create a weatherproof and durable enclosure design for our outdoor sensor line",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Emily Chen", "Isabella Brown" }
                },
                new DemoTaskInitializationData {
                    Subject = "Implement IoT connectivity for remote monitoring",
                    Description = "Develop IoT capabilities for our instruments to enable remote monitoring and control",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "Ethan Davis", "James Kim" }
                },
                new DemoTaskInitializationData {
                    Subject = "Conduct FMEA for new valve positioner",
                    Description = "Perform Failure Mode and Effects Analysis on the new smart valve positioner design",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Michael O'Connor", "Robert Johnson" }
                },
                new DemoTaskInitializationData {
                    Subject = "Develop machine learning algorithm for predictive maintenance",
                    Description = "Create an ML model to predict equipment failures based on sensor data",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "William Nguyen", "Daniel Lee" }
                },
                new DemoTaskInitializationData {
                    Subject = "Design new logo for product line",
                    Description = "Create a modern and appealing logo for our new smart sensor product line",
                    Priority = Priority.Low,
                    AssignedEmployeeNames = new List<string> { "Olivia Patel", "Isabella Brown" }
                },
                new DemoTaskInitializationData {
                    Subject = "Develop training program for new hires",
                    Description = "Create a comprehensive training program for onboarding new engineering staff",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Jane Smith", "Sophia Garcia" }
                },
                new DemoTaskInitializationData {
                    Subject = "Conduct market research for expansion into renewable energy sector",
                    Description = "Analyze market opportunities and competition in the renewable energy instrumentation market",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "Mia Taylor", "Mary Tellitson" }
                },
                new DemoTaskInitializationData {
                    Subject = "Optimize inventory management system",
                    Description = "Improve the current inventory tracking system to reduce costs and improve efficiency",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Jane Smith", "David Martinez" }
                },
                new DemoTaskInitializationData {
                    Subject = "Develop cross-platform mobile app for field technicians",
                    Description = "Create a mobile application for field technicians to access manuals and log maintenance activities",
                    Priority = Priority.High,
                    AssignedEmployeeNames = new List<string> { "Ethan Davis", "William Nguyen" }
                },
                new DemoTaskInitializationData {
                    Subject = "Conduct annual performance reviews",
                    Description = "Schedule and conduct annual performance evaluations for all employees",
                    Priority = Priority.Normal,
                    AssignedEmployeeNames = new List<string> { "Jane Smith", "Mary Tellitson", "Ava Thompson" }
                }
            };

            foreach (var data in taskData)
            {
                CreateDemoTaskIfNotExists(data);
            }

            _objectSpace.CommitChanges();
        }

        private void CreateDemoTaskIfNotExists(DemoTaskInitializationData data)
        {
            if (_objectSpace.FindObject<DemoTask>(CriteriaOperator.Parse("Subject = ?", data.Subject)) == null)
            {
                var task = _objectSpace.CreateObject<DemoTask>();
                task.Subject = data.Subject;
                task.Description = data.Description;
                task.Priority = data.Priority;

                foreach (var employeeName in data.AssignedEmployeeNames)
                {
                    var names = employeeName.Split(' ');
                    var employee = FindEmployee(names[0], names[1]);
                    if (employee != null)
                    {
                        task.Employees.Add(employee);
                        employee.DemoTasks.Add(task);
                    }
                }
                // You might want to set other properties like DueDate, Status, etc. if they exist in your DemoTask class
            }
        }

        private Employee FindEmployee(string firstName, string lastName)
        {
            return _objectSpace.FindObject<Employee>(CriteriaOperator.Parse("FirstName = ? AND LastName = ?", firstName, lastName));
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
