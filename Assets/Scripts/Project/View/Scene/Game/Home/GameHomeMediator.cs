using PureMVC.Interfaces;

namespace KILROY.Project.View
{
    public class GameHomeMediator : ProjectMediator
    {
        #region Constructor

        public GameHomeMediator(string name, ProjectBehaviour viewComponent = null) : base(name, viewComponent) { }

        #endregion

        #region Notification

        protected override void ExpansionNotification()
        {
            // AddNotification(Notification.);
        }

        public override void HandleNotification(INotification notification)
        {
            GameHomeBehaviour view = ViewComponent as GameHomeBehaviour;

            // if (notification.Name == FN.GetNotification(Notification.))
            // {
            //     SendNotification(FN.GetNotification(Notification.));
            // }
        }

        #endregion
    }
}