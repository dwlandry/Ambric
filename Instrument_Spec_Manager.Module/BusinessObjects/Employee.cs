using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [Column(TypeName ="date")]
    public virtual DateTime? Birthday { get; set; }
    [Browsable(false)]
    public virtual int TitleOfCourtesy_Int { get; set; }
    [NotMapped]
    public virtual TitleOfCourtesy TitleOfCourtesy { get; set; }
}

public enum TitleOfCourtesy
{
    Dr,
    Mr,
    Mrs,
    Ms
}
