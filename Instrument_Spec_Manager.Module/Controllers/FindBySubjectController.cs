using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Instrument_Spec_Manager.Module.BusinessObjects;

namespace Instrument_Spec_Manager.Module.Controllers {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FindBySubjectController : ViewController {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public FindBySubjectController() { 
            InitializeComponent();
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Root;
            TargetObjectType = typeof(DemoTask);

            ParametrizedAction findBySubjectAction = new ParametrizedAction(
                this, "FindBySubject", PredefinedCategory.View, typeof(string))
            { 
                ActionMeaning = ActionMeaning.Accept, 
                ImageName = "Action_Search", 
                NullValuePrompt="Find task by subject..." 
            };

            findBySubjectAction.Execute += FindBySubjectAction_Execute;

        }

        private void FindBySubjectAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            var objectType = ((ListView)View).ObjectTypeInfo.Type;
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            string paramValue = e.ParameterCurrentValue as string;
            object obj = objectSpace.FirstOrDefault<DemoTask>(task => task.Subject.IndexOf(paramValue, StringComparison.OrdinalIgnoreCase) >= 0);
            if (obj != null)
            {
                DetailView detailView = Application.CreateDetailView(objectSpace, obj);
                detailView.ViewEditMode = ViewEditMode.Edit;
                e.ShowViewParameters.CreatedView = detailView;
            }
        }

        protected override void OnActivated() {
            base.OnActivated(); 
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated(); 
            // Access and customize the target View control.
        }
        protected override void OnDeactivated() {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
