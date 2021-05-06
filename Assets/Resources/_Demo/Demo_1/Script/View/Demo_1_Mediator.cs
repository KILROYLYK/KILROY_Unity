using PureMVC.Interfaces;
using KILROY.Project.View;

namespace KILROY.Project.Demo
{
    public class Demo_1_Mediator : ProjectMediator
    {
        #region Constructor

        public Demo_1_Mediator(string name, ProjectBehaviour viewComponent = null) : base(name, viewComponent) { }

        #endregion

        #region Notification

        protected override void ExpansionNotification() { }

        public override void HandleNotification(INotification notification)
        {
            Demo_1_Behaviour view = ViewComponent as Demo_1_Behaviour;

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