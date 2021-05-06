using PureMVC.Interfaces;
using KILROY.Project.View;

namespace KILROY.Project.Demo
{
    public class DemoMediator : ProjectMediator
    {
        #region Constructor

        public DemoMediator(string name, ProjectBehaviour viewComponent = null) : base(name, viewComponent) { }

        #endregion

        #region Notification

        protected override void ExpansionNotification() { }

        public override void HandleNotification(INotification notification)
        {
            DemoBehaviour view = ViewComponent as DemoBehaviour;

            // if (notification.Name == FN.GetNotification(Notification.))
            // {
            //     SendNotification(FN.GetNotification(Notification.));
            // }
        }

        #endregion

        #region Cycle

        protected override void Init() { }

        #endregion
    }
}