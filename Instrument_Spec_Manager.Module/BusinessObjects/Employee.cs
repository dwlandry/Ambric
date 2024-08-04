using DevExpress.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects;

[DefaultClassOptions]
public class Employee : BaseObject
{
    public virtual String FirstName { get; set; }
    public virtual String LastName { get; set; }
    public virtual String MiddleName { get; set; }
    [Column(TypeName = "date")]
    public virtual DateTime? Birthday { get; set; }
    [Browsable(false)]
    public virtual int TitleOfCourtesy_Int { get; set; }
    [NotMapped]
    public virtual TitleOfCourtesy TitleOfCourtesy { get; set; }

    [SearchMemberOptions(SearchMemberMode.Exclude)]
    public string FullName => ObjectFormatter.Format(FullNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public string DisplayName => FullName;

    public static string FullNameFormat = "{FirstName} {MiddleName} {LastName}";


    [FieldSize(255)]
    public virtual string Email { get; set; }

    //[RuleRegularExpression(DefaultContexts.Save, @"(https?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", CustomMessageTemplate = "Invalid URL")]
    [RuleRegularExpression(@"(((http|https)://)?([a-zA-Z0-9]+[.])?([a-zA-Z0-9]+[.][a-zA-Z0-9]+)+([?][a-zA-Z0-9]+[=][a-zA-Z0-9]+)?([/][a-zA-Z0-9]+)*)", CustomMessageTemplate = "Invalid URL")]
    public virtual string WebPageAddress { get; set; }

    [StringLength(4096)]
    public virtual string Notes { get; set; }

    public virtual Department Department { get; set; }

    public virtual IList<PhoneNumber> PhoneNumbers { get; set; } = new ObservableCollection<PhoneNumber>();

    public virtual IList<DemoTask> DemoTasks { get; set; } = new ObservableCollection<DemoTask>();

    public virtual Position Position { get; set; }

    public virtual Address Address { get; set; }

    [DataSourceProperty("Department.Employees", DataSourcePropertyIsNullMode.SelectAll)]
    [DataSourceCriteria("Position.Title = 'Manager'")]
    public virtual Employee Manager { get; set; }

    public virtual IList<Resume> Resumes { get; set; } = new ObservableCollection<Resume>();
}

public enum TitleOfCourtesy
{
    Dr,
    Mr,
    Mrs,
    Ms
}
