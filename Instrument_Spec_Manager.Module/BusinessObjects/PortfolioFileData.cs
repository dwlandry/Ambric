using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_FileAttachment")]
    public class PortfolioFileData : BaseObject
    {

        [RuleRequiredField("PortfolioFileDataRule", "Save", "File should be assigned")]
        public virtual FileData File { get; set; }

        [Required]
        public virtual Resume Resume { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public override void OnCreated()
        {
            DocumentType = DocumentType.Unknown;
        }
    }
    public enum DocumentType
    {
        SourceCode = 1,
        Tests = 2,
        Documentation = 3,
        Diagrams = 4,
        Screenshots = 5,
        Unknown = 6
    }
}
