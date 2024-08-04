using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    public class DemoTask : BaseObject
    {
        [Column(TypeName = "date")]
        public virtual DateTime? DateCompleted { get; set; }
        public virtual String Subject { get; set; }
        [FieldSize(FieldSizeAttribute.Unlimited)]
        public virtual String Description { get; set; }

        [Column(TypeName = "date")]
        public virtual DateTime? DueDate { get; set; }
        [Column(TypeName = "date")]
        public virtual DateTime? StartDate { get; set; }

        public virtual int PercentCompleted { get; set; }
        private TaskStatus status;
        public virtual TaskStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                if (isLoaded)
                {
                    if (value == TaskStatus.Completed)
                    {
                        DateCompleted = DateTime.Now;
                    }
                    else
                    {
                        DateCompleted = null;
                    }
                }
            }
        }

        [Action(ImageName = "State_Task_Completed")]
        public void MarkCompleted()
        {
            Status = TaskStatus.Completed;
        }

        private bool isLoaded = false;
        public override void OnLoaded()
        {
            isLoaded = true;
        }

        public virtual IList<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public virtual Priority Priority { get; set; }

        public override void OnCreated()
        {
            base.OnCreated();
            Priority = Priority.Normal;
        }

        [Action(ToolTip = "Postpone the task to the next day", Caption = "Postpone")]
        // Shift the task's due date forward by one day
        public void Postpone()
        {
            if (DueDate == DateTime.MinValue)
            {
                DueDate = DateTime.Now;
            }
            DueDate = DueDate + TimeSpan.FromDays(1);
        }
    }
    public enum TaskStatus
    {
        [ImageName("State_Task_NotStarted")]
        NotStarted,
        [ImageName("State_Task_InProgress")]
        InProgress,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        WaitingForSomeoneElse,
        [ImageName("State_Task_Deferred")]
        Deferred,
        [ImageName("State_Task_Completed")]
        Completed
    }

    public enum Priority
    {
        [ImageName("State_Priority_Low")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        High = 2
    }
}
