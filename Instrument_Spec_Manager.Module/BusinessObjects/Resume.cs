using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [FileAttachment(nameof(File))]
    public class Resume : BaseObject
    {

        [Aggregated]
        public virtual IList<PortfolioFileData> Portfolio { get; set; } = new ObservableCollection<PortfolioFileData>();

        public virtual Employee Employee { get; set; }

        [Aggregated]
        public virtual FileData File { get; set; }
    }
}
