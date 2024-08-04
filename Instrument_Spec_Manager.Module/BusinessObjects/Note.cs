using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrument_Spec_Manager.Module.BusinessObjects
{
    [DefaultProperty(nameof(Text))]
    [ImageName("BO_Note")]
    public class Note
    {

        [Key, Browsable(false)]
        [DevExpress.ExpressApp.Data.Key]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public virtual Guid ID { get; set; }
        public virtual String Author { get; set; }
        
        [Column(TypeName = "timestamp")]
        public virtual DateTime? DateTime { get; set; }

        [FieldSize(FieldSizeAttribute.Unlimited)]
        public virtual String Text { get; set; }
    }
}
